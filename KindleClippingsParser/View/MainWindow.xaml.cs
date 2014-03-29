﻿using System;
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
using KindleClippingsParser.Controller;

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

        #endregion Private methods
        #region Event handlers

        private void menuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            m_Controller.MenuItemOpenClick();
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

        #endregion Event handlers
    }
}
