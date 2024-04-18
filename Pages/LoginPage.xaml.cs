using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExamApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Pages;

public partial class LoginPage : Page
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private  void LoginButton_OnClick(object sender, RoutedEventArgs e)
    {
        var login = LoginTextBox.Text;
        var password = PasswordTextBox.Text;

        using var dbContext = new DBContext();
        var user = dbContext.Users.Include(user => user.RoleNavigation).FirstOrDefault(
            user =>
                user.Login == login && user.Password == password
        );

        if (user != null)
        {
            App.User = user;
            NavigationService?.Navigate(new RequestListPage());
        }
        else
        {
            MessageBox.Show("Неверный логин или пароль");    
        }
    }
}