using System;
using System.Linq;
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
        
        using var dbContext = new DBContext();
        TypeProblemComboBox.ItemsSource = dbContext.ProblemTypes.ToList();
        TypeProblemComboBox.DisplayMemberPath = "Name";

        StatusComboBox.ItemsSource = dbContext.RequestStatuses.ToList();
        StatusComboBox.DisplayMemberPath = "Name";
    }

    private void AddRequestButton_OnClick(object sender, RoutedEventArgs e)
    {
        using var dbContext = new DBContext();

        if (dbContext.Users.FirstOrDefault(u => u.Id.ToString() == ClientTextBox.Text) == null)
        {
            MessageBox.Show("Клиент не найден");
            return;
        }
        
        var request = new RequestList()
        {
            Number = NumberRequestTextBox.Text,
            CreatedAt = RequestDatePicker.SelectedDate,
            DeviceName = DeviceNameTextBox.Text,
            ProblemType = TypeProblemComboBox.SelectedIndex + 1,
            Description = DescriptionTextBox.Text,
            ClientId = int.Parse(ClientTextBox.Text),
            Status = StatusComboBox.SelectedIndex + 1
        };

        try
        {
            dbContext.RequestLists.Add(request);
            dbContext.SaveChanges();
        }
        catch (Exception exception)
        {
            MessageBox.Show("Ошибка при добавлении");
            return;
        }
        
        NavigationService?.Navigate(new RequestListPage());
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService?.GoBack();
    }
}