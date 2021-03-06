﻿using ConsoleAppEngine.Log;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Course_Record_v2._0.Frames.Contacts
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = sender.SelectedItem as NavigationViewItem;

            LoggingServices.Instance.WriteLine<MainPage>("\"" + SelectedItem.Content as string + "\" is selected at Teacher Main Page.");
            if (SelectedItem == null)
            {
                return;
            }

            NavView.Header = SelectedItem.Content;

            if (SelectedItem == TeachersNavigation)
            {
                ContentFrame.Navigate(typeof(TeacherContacts));
            }
            else if (SelectedItem == StudentsNavigation)
            {
                ContentFrame.Navigate(typeof(StudentContacts));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Contacts Main Page loading...");
            NavView.SelectedItem = StudentsNavigation;
            LoggingServices.Instance.WriteLine<MainPage>("Contacts Main Page loaded.");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            LoggingServices.Instance.WriteLine<MainPage>("Contacts Main Page unloaded.");
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            this.Frame.GoBack();
        }
    }
}