using Constants;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Utilities;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AndroidManagerment.Dialogs;
using System.Windows.Media;
using System.Windows.Data;
using Microsoft.Win32;
using Repository;
using System.Collections.Generic;
using DAO;
using System.IO;

namespace AndroidManagerment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            init();
            initSource();
        }
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.Title = MainConstant.TITLE;
            this.Width = MainConstant.WIDTH;
            this.Height = MainConstant.HEIGHT;

            this.lList.Content = MainConstant.LIST.ToString();
            this.lTitle.Content = MainConstant.TITLE.ToString();
        }

        private ObservableCollection<Models.Menu> listMenu;
        private ObservableCollection<ProjectInfoDAO> _projectInfoDAOs;

        public ObservableCollection<Models.Menu> ListMenu { get => listMenu; set => listMenu = value; }
        public Dictionary<string,ProjectRepository> _projectRepository { get; set; }
        public ObservableCollection<ProjectInfoDAO> ProjectInfoDAOs { get => _projectInfoDAOs; set => _projectInfoDAOs = value; }

        string _currentTemplate;
        string _currentProject;

        /// <summary>
        /// khởi tạo giá trị 
        /// </summary>
        void init()
        {
            DataContext = this;
            ListMenu = new ObservableCollection<Models.Menu>();
            
            _projectRepository = new Dictionary<string, ProjectRepository>();
        }

        /// <summary>
        /// lấy giữ liệu cho menu
        /// </summary>
        void initSource()
        {
            foreach (var nameProject in FolderHelper.GetDirectories(Constants.BaseConstant.BASE_PATH_FOLDER_PROJECT))
            {
                _projectRepository.Add(nameProject, new ProjectRepository(nameProject));
                var proj = new Models.Menu(nameProject);

                foreach (var nameTemplate in FolderHelper.GetDirectories(Constants.BaseConstant.BASE_PATH_FOLDER_PROJECT + @"\" + nameProject))
                {
                    var count = 0;
                    try
                    {
                        var projInfoDAO = _projectRepository[nameProject].Find(x => x.Name == nameTemplate);
                        if (projInfoDAO != null && projInfoDAO.ProjectInfoDAOs != null)
                        {
                            count = projInfoDAO.ProjectInfoDAOs.Count;
                        }
                    }
                    catch (Exception) { }

                    proj.Members.Add(new Member
                    {
                        Name = nameTemplate,
                        Count = count
                    });
                }
                ListMenu.Add(proj);
            }

            tvMenu.Items.Clear();
            tvMenu.ItemsSource = ListMenu;
            tvMenu.MouseLeftButtonUp += TvMenu_MouseLeftButtonUp;
            tvMenu.MouseDoubleClick += TvMenu_MouseDoubleClick;
        }

        /// <summary>
        /// Mở list item trong menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvMenu_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var mem = (sender as TreeView).SelectedItem as Member;
            foreach (var menu in ListMenu)
            {
                foreach (var item in menu.Members)
                {
                    if (mem == item)
                        getProjectsTemplate(menu.Name, mem.Name);
                }
            }

        }

        /// <summary>
        /// lấy ra tất cả các project trong debug
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="temp"></param>
        void getProjectsTemplate(string proj, string temp)
        {
            _currentTemplate = temp;
            _currentProject = proj;

            var result = _projectRepository[proj].Find(x => x.Name == temp);
            if(result == null)
                ProjectInfoDAOs = new ObservableCollection<ProjectInfoDAO>();
            else
            {
                if (result.ProjectInfoDAOs == null)
                    result.ProjectInfoDAOs = new List<ProjectInfoDAO>();
                ProjectInfoDAOs = new ObservableCollection<ProjectInfoDAO>(result.ProjectInfoDAOs);
            }

            this.lvContent.ItemsSource = ProjectInfoDAOs;
            this.lvContent.MouseDoubleClick += LvContent_MouseDoubleClick;
        }

        /// <summary>
        /// mở project, mở theo đường dẫn path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvContent_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView item = sender as ListView;
            var name = ProjectInfoDAOs[item.SelectedIndex].Title;
            var pathfolder = ProjectInfoDAOs[item.SelectedIndex].Path;

            var file = pathfolder + "\\" + name + "\\" + name + ".sln";
            if (File.Exists(file))
                System.Diagnostics.Process.Start(file);
            else
                MessageBox.Show("File doesn't exist");
        }

        /// <summary>
        /// tạo mới Project theo chuẩn template đã chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvMenu_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var member = (sender as TreeView).SelectedItem as Member;
            if(member != null)
                dialog(member);
        }

        /// <summary>
        /// hiện ra dialog thêm mới project
        /// </summary>
        async void dialog(Member member)
        {
            #region tạo view cho dialog
            var gridMain = new Grid() { Margin = new Thickness(5), Width = 300 };
            var st = new StackPanel();
            var name = new TextBox { Margin = new Thickness(2), ToolTip = "Name project" }; st.Children.Add(name);
            HintAssist.SetHint(name, "Name project");

            var gridChil = new Grid();
            var path = new TextBox { Margin = new Thickness(4, 2, 2, 2), ToolTip = "Path project", Padding = new Thickness(0, 0, 20, 0), IsEnabled = false};
            HintAssist.SetHint(path, "Path project");

            var btnPath = new Button
            {
                Width = 30,
                Padding = new Thickness(5),
                Content = new PackIcon { Kind = PackIconKind.CreateNewFolder },
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center
            };

            btnPath.Style = (Style)Application.Current.TryFindResource("MaterialDesignToolButton");
            gridChil.Children.Add(path);
            gridChil.Children.Add(btnPath);
            st.Children.Add(gridChil);
            gridMain.Children.Add(st);
            #endregion

            #region chọn folder 
            btnPath.Click += (s, e) =>
            {
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult rs = dialog.ShowDialog();

                    if (rs == System.Windows.Forms.DialogResult.OK)
                    {
                        path.Text = dialog.SelectedPath;
                    }
                }
            };
            #endregion

            await DialogHost.Show(new DialogBase("New Project", gridMain), "RootDialog", (object s, DialogClosingEventArgs e) =>
            {
                
                bool isOk = Convert.ToBoolean(e.Parameter);
                if (isOk && !string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(path.Text))
                {
                    // thêm dữ liệu vào file json
                    var result = _projectRepository[_currentProject].Find(x => x.Name == _currentTemplate);
                    var info = new ProjectInfoDAO
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreateDate = DateTime.Now,
                        Path = path.Text,
                        Title = name.Text,
                    };

                    // nếu không tìm thấy cái template nào thì tạo mới
                    if (result == null || result.ProjectInfoDAOs == null)
                    {
                        result = new ProjectDAO
                        {
                            Id = Guid.NewGuid().ToString(),
                            CreateDate = DateTime.Now,
                            Name = _currentTemplate,
                            ProjectInfoDAOs = new List<ProjectInfoDAO> { info }
                        };
                    }
                    else // nếu tìm thấy thì add thêm project con vào trong danh sách đã có
                    {
                        result.ProjectInfoDAOs.Add(info);
                    }

                    // thêm dữ liệu xuống DB
                    _projectRepository[_currentProject].Add(result);

                    // thêm item vào content
                    ProjectInfoDAOs.Add(info);
                    // tăng count
                    foreach (var proj in ListMenu)
                    {
                        foreach (var temp in proj.Members)
                        {
                            if (temp.Name == _currentTemplate)
                            {
                                temp.Count++;
                            }
                        }
                    }

                    // copy hết project base vào project mới
                    var src = Constants.BaseConstant.BASE_PATH_FOLDER_PROJECT + _currentProject + "\\" + _currentTemplate;
                    var dest = path.Text;
                    FileHelper.CopyReplaceFolder(src, dest, name.Text);
                }
            });

        }

        /// <summary>
        /// xóa project trong từng template 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var proInfo = (sender as Button).DataContext as ProjectInfoDAO;
            // trừ count đi 1
            foreach (var proj in ListMenu)
            {
                foreach (var temp in proj.Members)
                {
                    if (temp.Name == _currentTemplate)
                    {
                        temp.Count--;
                    }
                }
            }
            // cập nhật giao diện
            ProjectInfoDAOs.Remove(proInfo);
            // xóa dữ liệu project dưới DB
            var result = _projectRepository[_currentProject].Find(x => x.Name == _currentTemplate);
            result.ProjectInfoDAOs.Remove(proInfo);

            _projectRepository[_currentProject].Update(result);
        }

        /// <summary>
        /// sửa project trong từng template
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var proInfo = (sender as Button).DataContext as ProjectInfoDAO;
            #region tạo view cho dialog
            var gridMain = new Grid() { Margin = new Thickness(5), Width = 300 };
            var st = new StackPanel();
            var name = new TextBox { Margin = new Thickness(2), ToolTip = "Name project" }; st.Children.Add(name);
            name.Text = proInfo.Title;
            HintAssist.SetHint(name, "Name project");

            var path = new TextBox { Margin = new Thickness(4,2,2,2), ToolTip = "Path project", Padding = new Thickness(0, 0, 20, 0)}; st.Children.Add(path);
            path.Text = proInfo.Path;
            HintAssist.SetHint(path, "Path project");

            gridMain.Children.Add(st);
            #endregion

            await DialogHost.Show(new DialogBase("Edit project", gridMain), "RootDialog", (object s, DialogClosingEventArgs ex) =>
            {
                bool isOk = Convert.ToBoolean(ex.Parameter);
                if (isOk && !string.IsNullOrEmpty(name.Text) && !string.IsNullOrEmpty(path.Text))
                {
                    // thêm dữ liệu vào file json
                    var result = _projectRepository[_currentProject].Find(x => x.Name == _currentTemplate);
                    var item = result.ProjectInfoDAOs.Find(x => x.Id == proInfo.Id);
                    item.Title = name.Text;
                    item.Path = path.Text;

                    // thêm dữ liệu xuống DB
                    _projectRepository[_currentProject].Update(result);

                    // thêm item vào content
                    var index = ProjectInfoDAOs.IndexOf(proInfo);
                    ProjectInfoDAOs[index].Title = name.Text;
                    ProjectInfoDAOs[index].Path = path.Text;
                }
            });
        }

        /// <summary>
        /// copy path vào clipbroad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            var proInfo = (sender as Button).DataContext as ProjectInfoDAO;
            System.Windows.Forms.Clipboard.SetText(proInfo.Path);
        }
    }
}
