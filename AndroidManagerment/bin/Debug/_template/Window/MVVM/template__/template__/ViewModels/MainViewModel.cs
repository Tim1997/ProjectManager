using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace template__.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        #region private properties
        private string helloWord;
        #endregion

        #region public properties
        public ICommand WindowCommand { get; set; }
        public static ObservableCollection<object> List { get; set; }
        public string HelloWord { get => helloWord; set { helloWord = value; OnPropertyChanged(); } }
        #endregion


        public MainViewModel()
        {
            initProperties();
            initCommand();
        }

        /// <summary>
        /// khởi tạo giá trị cho các biến
        /// </summary>
        void initProperties()
        {
            HelloWord = _helloWorld;
        }

        /// <summary>
        /// khởi tạo giá trị cho các sự kiện button
        /// </summary>
        void initCommand()
        {
            WindowCommand = new RelayCommand<object>(p => { return true; }, p =>
            {
                
            });
        }
    }
}
