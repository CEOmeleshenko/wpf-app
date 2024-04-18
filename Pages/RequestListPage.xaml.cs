using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExamApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Pages;

public partial class RequestListPage : Page
{
    public RequestListPage()
    {
        InitializeComponent();
        LoadRequest();
        App.RoleTextBlock.Text = $"{App.User?.Login} ({App.User?.RoleNavigation.Name})";
    }

    private async void LoadRequest()
    {
        await using var dbContext = new DBContext();
        var requests = await dbContext.RequestLists.ToListAsync();
        ItemsDataGrid.ItemsSource = requests;
    }

    private void FilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // throw new System.NotImplementedException();
    }

    private void AddRequestButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new AddRequestPage());
    }
}