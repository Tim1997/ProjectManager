using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AndroidManagerment.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogProcess.xaml
    /// </summary>
    public partial class DialogProcess : UserControl
    {
        private string m_progressTitle;
        public string ProgressTitle
        {
            get { return m_progressTitle; }
            set { m_progressTitle = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DialogProcess(string title)
        {
            InitializeComponent();
            DataContext = this;
            ProgressTitle = title;
        }
    }
    // cach dung
    //private bool m_isShow;
    //public bool IsShow
    //{
    //    get { return m_isShow; }
    //    set { m_isShow = value; OnPropertyChanged(); }
    //}

    //private object m_dialogObject;
    //public object DialogObject
    //{
    //    get { return m_dialogObject; }
    //    set
    //    {
    //        if (m_dialogObject == value) return;
    //        m_dialogObject = value; OnPropertyChanged();
    //    }
    //}

    //DialogObject = new DialogProgress("Initializing...");
    //IsShow = !IsShow;
}
