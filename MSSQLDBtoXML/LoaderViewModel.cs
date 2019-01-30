using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLDBtoXML
{
    public class LoaderViewModel: INotifyPropertyChanged
    {

        private string _statusMesssages;

        public string StatusMessages
        {
            get { return _statusMesssages; }
            set {

                _statusMesssages = value;
                onPropertyChanged("StatusMessages");
            }
        }
        #region "Notify Implementation"

        public event PropertyChangedEventHandler PropertyChanged;

        public void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
