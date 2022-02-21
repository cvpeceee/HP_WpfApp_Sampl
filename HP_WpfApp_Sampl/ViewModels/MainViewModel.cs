using System;
using HP_WpfApp_Sampl.Models;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using UsbEventWatcher;
using System.Windows;

namespace HP_WpfApp_Sampl.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private USBDevice _usb;
        private ObservableCollection<USBDevice> _usbDevices;

        private EventWatcherAsync eventWatcherAsync;

        public MainViewModel()
        {
            Initialize();
        }


        public USBDevice USBDevice
        {
            get
            {
                return _usb;
            }
            set
            {
                _usb = value;
                NotifyPropertyChanged(nameof(USBDevice));
            }
        }
        public ObservableCollection<USBDevice> USBDevices
        {
            get
            {
                return _usbDevices;
            }
            set
            {
                _usbDevices = value;
                NotifyPropertyChanged(nameof(USBDevices));
            }
        }


        private void Initialize()
        {
            USBDevices = new ObservableCollection<USBDevice>();
            USBDevices.CollectionChanged += new NotifyCollectionChangedEventHandler(Device_Updated);

            eventWatcherAsync = new EventWatcherAsync();
            eventWatcherAsync.OnEventArrived += LoadDevices;
        }

        private void LoadDevices(object? sender, DeviceEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() => USBDevices.Add(new USBDevice() { Device = e.Data }));
            
        }

        private void Device_Updated(object? sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(USBDevices));
        }


    }
}
