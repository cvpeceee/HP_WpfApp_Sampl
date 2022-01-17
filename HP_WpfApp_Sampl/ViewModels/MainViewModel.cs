using HP_WpfApp_Sampl.Models;
using HP_WpfApp_Sampl.Services.Interfaces;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace HP_WpfApp_Sampl.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private USBDevice _usb;
        private ObservableCollection<USBDevice> _usbDevices;
        
        private IUSBService _usbService;
        
        public MainViewModel(IUSBService usbService)
        {
            _usbService = usbService;
            
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
            _usbService.LoadUSBDevices(ref _usbDevices);
            
            
        }

        private void Device_Updated(object? sender, NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged(nameof(USBDevices));
        }
    }
}
