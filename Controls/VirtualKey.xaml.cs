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

namespace VirtualMouseKeyboard.Controls
{
    /// <summary>
    /// Логика взаимодействия для VirtualKey.xaml
    /// </summary>
    public partial class VirtualKey : UserControl
    {
        public static readonly new DependencyProperty ContentProperty =
            DependencyProperty.Register(
                nameof(Content),
                typeof(string),
                typeof(VirtualKey),
                new PropertyMetadata(string.Empty));

        public new string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }


        public static readonly DependencyProperty KeyProperty =
           DependencyProperty.Register(
               nameof(Key),
               typeof(string),
               typeof(VirtualKey),
               new PropertyMetadata(string.Empty));

        public string Key
        {
            get => (string)GetValue(KeyProperty);
            set => SetValue(KeyProperty, value);
        }


        public bool _partiallySelected;

        public VirtualKey()
        {
            InitializeComponent();
        }

        public void ChangeState(bool keyState)
        {
            if(_partiallySelected && !keyState)
            {
               SetKeyUp();
            }
            if(keyState)
            {
                SetKeyDown();
            }    
        }

        private void SetKeyDown()
        {
            _partiallySelected = true;

            DefaultBackground.Visibility = Visibility.Collapsed;
            ClickedBackground.Visibility = Visibility.Visible;

            App.Instance.InputFocusedListener.SetFocusOnLast();

            App.Instance.KeyboardInterop.KeyDown(Key);
        }

        private void SetKeyUp()
        {
            _partiallySelected = false;

            DefaultBackground.Visibility = Visibility.Visible;
            ClickedBackground.Visibility = Visibility.Collapsed;

            App.Instance.InputFocusedListener.SetFocusOnLast();

            App.Instance.KeyboardInterop.KeyUp(Key);
        }

        private void Key_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeState(true);
        }

        private void Key_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangeState(false);
        }

        private void Key_TouchDown(object sender, TouchEventArgs e)
        {
            ChangeState(true);
        }

        private void Key_TouchUp(object sender, TouchEventArgs e)
        {
            ChangeState(false);
        }

        private void Key_TouchLeave(object sender, TouchEventArgs e)
        {
            ChangeState(false);
        }

        private void Key_MouseLeave(object sender, MouseEventArgs e)
        {
            ChangeState(false);
        }
    }
}