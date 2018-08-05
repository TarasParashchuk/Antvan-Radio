using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Antvan_Radio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : MasterDetailPage
    {
        public List<MasterPageItem> menuList { get; set; }

        public class MasterPageItem
        {
            public string Title { get; set; }
            public string Icon { get; set; }
            public Type TargetType { get; set; }
        }

        public Menu()
        {
            InitializeComponent();
            menuList = new List<MasterPageItem>();

            var page1 = new MasterPageItem() { Title = "Избранное", TargetType = typeof(Favorites) };
            var page3 = new MasterPageItem() { Title = "Порекомендовать друзьям", TargetType = typeof(Favorites) };
            var page4 = new MasterPageItem() { Title = "Оставить отзыв", TargetType = typeof(Favorites) };
            var page5 = new MasterPageItem() { Title = "Контакты", TargetType = typeof(Favorites) };
            var page6 = new MasterPageItem() { Title = "Настройки", TargetType = typeof(Favorites) };
            var page7 = new MasterPageItem() { Title = "Antvan.ru", TargetType = typeof(MainPage) };

            menuList.Add(page1);
            menuList.Add(page3);
            menuList.Add(page4);
            menuList.Add(page5);
            menuList.Add(page6);
            menuList.Add(page7);

            navigationList.ItemsSource = menuList;

            var Navigation_Page = new NavigationPage(new MainPage());
            Navigation_Page.BarBackgroundColor = Color.Black;
            Detail = Navigation_Page;
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;

            Detail = new NavigationPage((Page)Activator.CreateInstance(page)) { BarBackgroundColor = Color.Black };
            IsPresented = false;
        }
    }
}