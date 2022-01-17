using HP_WpfApp_Sampl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP_WpfApp_Sampl.Services.Interfaces
{
    public interface IUSBService
    {
        public void LoadUSBDevices(ref ObservableCollection<USBDevice> collection);
    }
}
