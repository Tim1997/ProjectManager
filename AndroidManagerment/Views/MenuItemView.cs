using Constants;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Views
{
    public class MenuItemView : Grid
    {
        public Action Click { get; set; }
        public MenuItemView(string title, string des)
        {
            this.Width = MainConstant.WIDTH * 0.65;
            var st = new StackPanel { Margin = new System.Windows.Thickness(2)};

            //BindingOperations.SetBinding(LbTitle, ContentControl.ContentProperty,
            //    new Binding { Path = new PropertyPath("Title"), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            //title.SetBinding(ContentControl.ContentProperty, 
            //    new Binding { Path = new PropertyPath("Title"), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
            st.Children.Add(new Label { Content = title, FontSize = 13 });
            st.Children.Add(new Label
            {
                Content = des,
                FontSize = 10,
                Foreground = Brushes.Gray,
            });
            this.Children.Add(st);

            
            var btnDelete = new Button
            {
                Content = "Delete",
                HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
            };

            btnDelete.Click += (s, e) =>
            {
                Click?.Invoke();
            };
            this.Children.Add(btnDelete);
        }
    }
}
