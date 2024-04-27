using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ExamApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Pages;

public partial class EditRequestPage : Page
{
    private RequestList _request;
    public EditRequestPage(RequestList request)
    {
        InitializeComponent();

        _request = request;
        
        NumberTextBlock.Text = request.Number;
        DescriptionTextBox.Text = request.Description ?? string.Empty;
        
        using var dbContext = new DBContext();
        StatusComboBox.ItemsSource = dbContext.RequestStatuses.ToList();
        StatusComboBox.DisplayMemberPath = "Name";
        StatusComboBox.SelectedIndex = request.Status - 1;

        WorkerComboBox.ItemsSource = dbContext.Users.Where(u => u.Role == 4).ToList();
        WorkerComboBox.DisplayMemberPath = "Login";
        WorkerComboBox.SelectedIndex = (int)(request.WorkerId - 1)!;

    }

    private void ApplyButton_OnClick(object sender, RoutedEventArgs e)
    {
        using var dbContext = new DBContext();
        _request.Description = DescriptionTextBox.Text;
        _request.Status = StatusComboBox.SelectedIndex + 1;
        if (WorkerComboBox.SelectedIndex != -1) _request.WorkerId = WorkerComboBox.SelectedIndex + 1;
        dbContext.Entry(_request).State = EntityState.Modified;
        dbContext.SaveChanges();
        
        NavigationService?.Navigate(new RequestListPage());
    }
    
    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService?.GoBack();
    }
}