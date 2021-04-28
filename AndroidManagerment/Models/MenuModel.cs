using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Models
{
    public class Menu : Base.BaseBinding
    {
        private string name;
        private string source;
        private ObservableCollection<Member> members;

        public string Name { get => name; set { name = value; } }
        public string Source { get => source; set => source = value; }
        public ObservableCollection<Member> Members { get => members; set { members = value; OnPropertyChanged(); } }

        public Menu()
        {
            Members = new ObservableCollection<Member>();
        }
        public Menu(string name)
        {
            this.Name = name;
            this.Source = @"/Icons/" + name.ToLower().Trim() + ".png";
            this.Members = new ObservableCollection<Member>();
        }
    }
    public class Member : Base.BaseBinding
    {
        public string Name { get; set; }
        public int Count { get => count; set { count = value; OnPropertyChanged(); } }

        private int count;
    }
}
