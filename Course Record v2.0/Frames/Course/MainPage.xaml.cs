﻿using System.Collections.Generic;
using ConsoleAppEngine.Course;
using ConsoleAppEngine.Contacts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Course_Record_v2._0.Frames.Course
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private AllCourses allCourses;
        private AllContacts allContacts;
        private readonly NavigationViewItem GoBack = new NavigationViewItem() { Content = "Go Back" };
        private bool logtemp=true;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = sender.SelectedItem as NavigationViewItem;
            NavView.Header = SelectedItem.Content;
            if(logtemp)
                Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>(NavView.Header.ToString() + " is selected.");
            else
                Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Switched to "+ NavView.Header.ToString() );
            if (SelectedItem == AddCoursesNavigation)
            {
                LinkedList<object> temp = new LinkedList<object>();
                temp.AddLast(allCourses);
                temp.AddLast(allContacts);
                temp.AddLast(NavView);
                ContentFrame.Navigate(typeof(Add_Course), temp);
                SecNav.Visibility = Visibility.Collapsed;
            }
            else if (SelectedItem == GoBack)
            {
                this.Frame.GoBack();
                Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Entered Main Menu");
            }
            else
            {
                SecNav.SelectedItem = null;
                SecNav.SelectedItem = OverViewItem;
                SecNav.Visibility = Visibility.Visible;
            }
            logtemp = false;
        }

        private void SecNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = (sender.SelectedItem as NavigationViewItem);
            if (SelectedItem == null)
            {
                return;
            }

            CourseEntry SelectedCourse = null;
            foreach (var x in allCourses.CoursesList)
            {
                if (NavView.SelectedItem == x.navigationViewItem)
                {
                    SelectedCourse = x;
                    break;
                }
            }

            switch (SelectedItem.Content)
            {
                case "Overview":
                    ContentFrame.Navigate(typeof(Overview));
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("OverView Tab of Course " + NavView.Header.ToString());
                    break;
                case "Books":
                    ContentFrame.Navigate(typeof(Books), SelectedCourse.BookEntry);
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Books Tab of Course " + NavView.Header.ToString());
                    break;
                case "Handout":
                    ContentFrame.Navigate(typeof(Handout), SelectedCourse.HandoutEntry);
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Handout Tab of Course " + NavView.Header.ToString());
                    break;
                case "Teachers":
                    LinkedList<object> lis = new LinkedList<object>();
                    lis.AddLast(SelectedCourse);
                    lis.AddLast(allContacts.TeacherEntry);
                    ContentFrame.Navigate(typeof(Teachers), lis);
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Teachers Tab of Course " + NavView.Header.ToString());
                    break;
                case "CT log":
                    lis = new LinkedList<object>();
                    lis.AddLast(SelectedCourse.CTLog);
                    lis.AddLast(allContacts.StudentEntry);
                    ContentFrame.Navigate(typeof(CT_log), lis);
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Displaying CT Log of Course " + NavView.Header.ToString());
                    break;
                case "Time Table":
                    ContentFrame.Navigate(typeof(TimeTable), SelectedCourse);
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("TimeTable of Course " + NavView.Header.ToString());
                    break;
                case "Events":
                    ContentFrame.Navigate(typeof(Events), SelectedCourse.EventEntry);
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Events of Course " + NavView.Header.ToString());
                    break;
                case "Tests":
                    ContentFrame.Navigate(typeof(Tests), SelectedCourse.TestEntry);
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Tests Record of Course " + NavView.Header.ToString());
                    break;
                case "Files":
                    ContentFrame.Navigate(typeof(Files));
                    Course_Record_v2._0.MainPage.log.WriteLine<Course.MainPage>("Files of Course " + NavView.Header.ToString());
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            logtemp = true;
            var a = (e.Parameter as LinkedList<object>);
            allCourses = a.First.Value as AllCourses;
            allContacts = a.First.Next.Value as AllContacts;

            foreach (var x in allCourses.CoursesList)
            {
                NavView.MenuItems.Add(x.navigationViewItem);
            }

            NavView.MenuItems.Add(new NavigationViewItemSeparator());
            NavView.MenuItems.Add(new NavigationViewItemHeader() { Content = "Navigation" });
            NavView.MenuItems.Add(GoBack);
            NavView.MenuItems.Add(new NavigationViewItemSeparator());

            if (allCourses.CoursesList.Count == 0)
            {
                NavView.SelectedItem = AddCoursesNavigation;
            }
            else
            {
                NavView.SelectedItem = allCourses.CoursesList.First.Value.navigationViewItem;
            }
        }
        
        

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavView.MenuItems.Clear();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ContentFrame.ForwardStack.Clear();
            ContentFrame.BackStack.Clear();
        }
    }
}