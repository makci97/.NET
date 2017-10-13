using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace UserManager
{
    public class MainViewModel : DependencyObject
    {
        public ICollectionView UserList
        {
            get { return (ICollectionView)GetValue(UserListProperty); }
            set { SetValue(UserListProperty, value); }
        }

        public static readonly DependencyProperty UserListProperty =
            DependencyProperty.Register("UserList", typeof(ICollectionView), typeof(MainViewModel), new PropertyMetadata(null));

        public string FilterStringName
        {
            get { return (string)GetValue(FilterStringNameProperty); }
            set { SetValue(FilterStringNameProperty, value); }
        }
        public string FilterStringPhone
        {
            get { return (string)GetValue(FilterStringPhoneProperty); }
            set { SetValue(FilterStringPhoneProperty, value); }
        }
        public string FilterStringEmail
        {
            get { return (string)GetValue(FilterStringEmailProperty); }
            set { SetValue(FilterStringEmailProperty, value); }
        }
        public int FilterBoxUserType
        {
            get { return (int)GetValue(FilterBoxUserTypeProperty); }
            set { SetValue(FilterBoxUserTypeProperty, value); }
        }

        public static readonly DependencyProperty FilterStringNameProperty =
            DependencyProperty.Register("FilterStringName", typeof(string), typeof(MainViewModel), new PropertyMetadata("", OnFilterStringChange));

        public static readonly DependencyProperty FilterStringPhoneProperty =
            DependencyProperty.Register("FilterStringPhone", typeof(string), typeof(MainViewModel), new PropertyMetadata("", OnFilterStringChange));

        public static readonly DependencyProperty FilterStringEmailProperty =
            DependencyProperty.Register("FilterStringEmail", typeof(string), typeof(MainViewModel), new PropertyMetadata("", OnFilterStringChange));

        public static readonly DependencyProperty FilterBoxUserTypeProperty =
            DependencyProperty.Register("FilterBoxUserType", typeof(int), typeof(MainViewModel), new PropertyMetadata(0, OnFilterBoxChange));

        public static void OnFilterStringChange(DependencyObject d, DependencyPropertyChangedEventArgs arg)
        {
            ((MainViewModel)d).UserList.Refresh();
        }
        public static void OnFilterBoxChange(DependencyObject d, DependencyPropertyChangedEventArgs arg)
        {
            ((MainViewModel)d).UserList.Refresh();
        }


        public SimpleCommand CreateUserCommand { get; set; }
        public SimpleCommand LoadUserCommand { get; set; }
        public OneParameterCommand<User> EditUserCommand { get; set; }

        public void CreateUser()
        {
            var obs_ls = UserList.SourceCollection as ObservableCollection<User>;
            obs_ls.Add(new User() { Name = "New User" });
        }

        public void LoadUsers()
        {
            var ls = new ObservableCollection<User>(User.GetUsers());
            UserList = CollectionViewSource.GetDefaultView(ls);
            UserList.Filter += FilterUser;
        }

        public MainViewModel()
        {
            //UserList = new List<User>(User.GetUsers());
            var ls = new ObservableCollection<User>();
            UserList = CollectionViewSource.GetDefaultView(ls);

            UserList.Filter += FilterUser;

            CreateUserCommand = new SimpleCommand(CreateUser);
            LoadUserCommand = new SimpleCommand(LoadUsers);
            EditUserCommand = new OneParameterCommand<User>(EditUser);
        }

        private void EditUser(User user)
        {
            var window = new UserWindow
            {
                DataContext = new UserWindowViewModel
                {
                    User = user
                }
            };

            window.Show();
        }

        private bool FilterUser(object obj)
        {
            var user = obj as User;

            if (user == null)
            {
                return false;
            }

            try
            {
                if ((string.IsNullOrEmpty(FilterStringName) || user.Name.Contains(FilterStringName))
                   && (string.IsNullOrEmpty(FilterStringPhone) || user.Phone.Contains(FilterStringPhone))
                   && (string.IsNullOrEmpty(FilterStringEmail) || user.Email.Contains(FilterStringEmail))
                   && ((FilterBoxUserType  == (int)UserType.None) || (int)user.Type == FilterBoxUserType))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            

            return false;
        }
    }
}
