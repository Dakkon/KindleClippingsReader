﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KindleClippingsParser.Controller;
using KindleClippingsParser.Helpers;
using System.Threading;

namespace KindleClippingsParser.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>   

    public partial class MainWindow : Window
    {
        #region Private fields

        SelectedBookView m_SelectedBookView;
        MCFileView m_MCFileView;

        KindleClippingsParserController m_Controller;

        #endregion Private fields
        #region Properties

        public SelectedBookView SelectedBookViewInstance
        {
            get
            {
                return m_SelectedBookView;
            }
        }

        public MCFileView MCFileViewInstance
        {
            get
            {
                return m_MCFileView;
            }
        }

        #endregion Properties
        #region Ctors

        public MainWindow()
        {
            InitializeComponent();
            m_Controller = new KindleClippingsParserController(this);

            InitializeViews();

            SubscribeToEvents();

            SetLanguageDictionary();

            SetWelcomeScreen();
        }

        #endregion Ctors
        #region Private methods

        private void InitializeViews()
        {
            m_SelectedBookView = new SelectedBookView(this);
            m_MCFileView = new MCFileView(this);
        }

        private void SubscribeToEvents()
        {
            menuItemMCFileView.Click += menuItemMCFileView_Click;
            menuItemSelectedBookView.Click += menuItemSelectedBookView_Click;
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "pl-PL":
                    dict.Source = new Uri("..\\Resources\\StringResources.pl-PL.xaml",
                           UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\StringResources.xaml",
                        UriKind.Relative);
                    break;
            }
            Resources.MergedDictionaries.Add(dict);
        }

        private void SetWelcomeScreen()
        {
            string fullPathToMyClippingsFile = DiskIOHelper.FindMyClippingsFile();
            string welcomeText;

            if (!string.IsNullOrEmpty(fullPathToMyClippingsFile))
            {
                welcomeText = Resources["welcomeTxtWhenFileFound"].ToString();
                textBlockWelcomeText.Text = string.Format(welcomeText, Environment.NewLine, fullPathToMyClippingsFile);
            }
            else
            {
                welcomeText = Resources["welcomeTxtWhenFileNotFound"].ToString();

                textBlockWelcomeText.Text = welcomeText;
                HideButtonOpenFound();
            }
        }

        private void HideButtonOpenFound()
        {
            buttonOpenFound.Visibility = Visibility.Collapsed;
            Grid.SetColumn(buttonOpenDifferent, 0);
            Grid.SetColumnSpan(buttonOpenDifferent, 2);

            buttonOpenDifferent.Content = Resources["searchAndOpen"].ToString();
        }

        #endregion Private methods
        #region Event handlers

        private void buttonOpenFound_Click(object sender, RoutedEventArgs e)
        {
            m_Controller.ButtonOpenFoundClick(textBlockWelcomeText.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)[1]);
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            m_Controller.OpenFileClick();
        }

        private void menuItemExit_Click(object sender, RoutedEventArgs e)
        {
            m_Controller.MenuItemExitClick();
        }

        private void menuItemMCFileView_Click(object sender, RoutedEventArgs e)
        {
            m_Controller.MenuItemMCFileViewClick();
        }

        private void menuItemSelectedBookView_Click(object sender, RoutedEventArgs e)
        {
            m_Controller.MenuItemSelectedBookViewClick();
        }

        private void comboBoxClippingHeaders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m_Controller.ComboBoxClippingHeadersSelectionChanged();
        }

        private void comboBoxClippingHeaders_KeyUp(object sender, KeyEventArgs e)
        {
            m_Controller.ComboBoxClippingHeadersKeyUp(e);
        }

        private void comboBoxClippingHeaders_KeyDown(object sender, KeyEventArgs e)
        {
            m_Controller.ComboBoxClippingHeadersKeyDown(e);
        }

        private void comboBoxClippingHeaders_DropDownClosed(object sender, EventArgs e)
        {
            m_Controller.ComboBoxClippingHeadersDropDownClosed();
        }

        #endregion Event handlers
    }
}
