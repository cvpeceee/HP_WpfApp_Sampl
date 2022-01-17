using HP_WpfApp_Sampl.Models;
using HP_WpfApp_Sampl.Services.Interfaces;
using System;
using System.Management;
using System.Collections.ObjectModel;
using System.Windows;

namespace HP_WpfApp_Sampl.Services
{
    public class USBService : IUSBService
    {

        public void LoadUSBDevices(ref ObservableCollection<USBDevice> collection)
        {

            var eventWatcher = new EventWatcherAsync(ref collection);
            
        }
    }

    public class EventWatcherAsync
    {

        private ObservableCollection<USBDevice> _usbDevices;
        public EventWatcherAsync(ref ObservableCollection<USBDevice> collection)
        {
            try
            {
                _usbDevices = collection;
                var ComputerName = "localhost";
                string WmiQuery;
                ManagementEventWatcher Watcher;
                ManagementScope scope;


                if (!ComputerName.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    var conn = new ConnectionOptions();
                    conn.Username = "";
                    conn.Password = "";
                    conn.Authority = "ntlmdomain:DOMAIN";
                    scope = new ManagementScope(string.Format("\\\\{0}\\ROOT\\cimv2", ComputerName), conn);
                }
                else
                {
                    scope = new ManagementScope(string.Format("\\\\{0}\\ROOT\\cimv2", ComputerName), null);
                }

                scope.Connect();

                WmiQuery = "Select * From Win32_DeviceChangeEvent";

                Watcher = new ManagementEventWatcher(scope, new EventQuery(WmiQuery));

                Watcher.EventArrived += WmiEventHandler;

                Watcher.Start();
                //Console.Read();
                //Watcher.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception {0} Trace {1}", e.Message, e.StackTrace);
            }
        }

        private void WmiEventHandler(object sender, EventArrivedEventArgs e)
        {
            //set the class name and namespace
            var NamespacePath = "\\\\.\\ROOT\\cimv2";
            var ClassName = "Win32_PnPDevice";

            //Create ManagementClass
            var oClass = new ManagementClass(NamespacePath + ":" + ClassName);

            //Get all instances of the class and enumerate them
            foreach (var o in oClass.GetInstances())
            //access a property of the Management object
            {
                var oObject = (ManagementObject)o;
                //Console.WriteLine("Device Path : {0}", oObject["SameElement"]);
                if (oObject["SameElement"].ToString().Contains("Win32_USBHub"))
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => this._usbDevices.Add(new USBDevice { Device = $"Device Path : {oObject["SameElement"]}" })));
                    
                }
            }
        }


    }
}
