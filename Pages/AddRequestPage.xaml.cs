using System;
using System.Windows;
using System.Windows.Controls;
using ExamApp.Entities;

namespace ExamApp.Pages;

public partial class AddRequestPage : Page
{
    public AddRequestPage()
    {
        InitializeComponent();
        
        RequestDatePicker.SelectedDate = DateTime.Now;
    }

    private void AddRequestButton_OnClick(object sender, RoutedEventArgs e)
    {
        using var dbContext = new DBContext();
        var request = new RequestList()
        {
            Number = NumberRequestTextBox.Text,
            CreatedAt = RequestDatePicker.SelectedDate,
            DeviceName = DeviceNameTextBox.Text,
            Description = DescriptionTextBox.Text,
            ClientId = 1,
            Status = StatusComboBox.SelectedIndex + 1
        };
        
        dbContext.RequestLists.Add(request);
        dbContext.SaveChanges();
        
        NavigationService?.Navigate(new RequestListPage());
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService?.GoBack();

    }
}