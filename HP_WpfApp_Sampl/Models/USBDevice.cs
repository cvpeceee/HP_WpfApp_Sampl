using HP_WpfApp_Sampl.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP_WpfApp_Sampl.Models
{
    public class USBDevice : ViewModelBase
    {
        private string _device;
        public string Device
        {
            get { return _device; }
            set
            {
                _device = value;
                NotifyPropertyChanged(nameof(Device));
            }
        }

    }
}
