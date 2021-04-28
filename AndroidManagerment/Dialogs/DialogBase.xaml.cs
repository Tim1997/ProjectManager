using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
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
using static Base.BaseViewModel;

namespace AndroidManagerment.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogAsk.xaml
    /// </summary>
    public partial class DialogBase : UserControl
    {
        public enum ButtonType { OK, CancelOK, NoYes };

        public DialogBase(string title, Grid grid)
        {
            InitializeComponent();
            InitializeGUI(title, grid);
            
        }

        private void InitializeGUI(string title, Grid grid, ButtonType buttonType = ButtonType.CancelOK)
        {
            Title.Text = title;
            dialogContent.Children.Add(grid);
            
            switch (buttonType)
            {
                case ButtonType.OK:
                    ButtonTrue.Content = "OK";
                    ButtonFalse.Content = null;
                    ButtonFalse.Visibility = Visibility.Hidden;
                    break;
                case ButtonType.CancelOK:
                    ButtonTrue.Content = "OK";
                    ButtonFalse.Content = "CANCEL";
                    break;
                case ButtonType.NoYes:
                    ButtonTrue.Content = "YES";
                    ButtonFalse.Content = "NO";
                    break;
            }
        }
    }
}
