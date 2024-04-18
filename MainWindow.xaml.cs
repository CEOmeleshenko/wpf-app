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
using ExamApp.Entities;
using ExamApp.Pages;

namespace ExamApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToLoginPage();
            App.RoleTextBlock = RoleTextBlock;
        }

        private void NavigateToLoginPage()
        {
            MainFrame.Navigate(new LoginPage());
            MainFrame.Navigated += MainFrameOnNavigated;
        }

        private void MainFrameOnNavigated(object sender, NavigationEventArgs e)
        {
            Title = "ООО \"Техносервис\" - " + (MainFrame.Content as Page)?.Title;
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            App.User = null;
            App.RoleTextBlock.Text = "";
            MainFrame.Navigate(new LoginPage());
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}