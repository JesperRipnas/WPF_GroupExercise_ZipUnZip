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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> filestring = new List<string>();
        public string Fs { get; set; }

        // Declare Molka object
        Molka molka;

        public MainWindow()
        {
            InitializeComponent();
            molka = new Molka();
        }

        private void Menu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        Storyboard storyboard;

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Color background = ((SolidColorBrush)btn.BorderBrush).Color;
            storyboard = new Storyboard();
            storyboard.Children.Add(SetAnimButton(background, btn.Name));
            storyboard.Children.Add(SetAnimCirkle(background));
            storyboard.Begin(this);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            storyboard = new Storyboard();
            storyboard.Children.Add(SetAnimButton(Color.FromRgb(113, 110, 110), btn.Name));
            storyboard.Children.Add(SetAnimCirkle(Color.FromArgb(150, 67, 67, 67)));
            storyboard.Begin(this);
        }

        public ColorAnimation SetAnimButton(Color Color, string objName)
        {
            ColorAnimation anim = new ColorAnimation();
            anim.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            anim.To = Color;
            Storyboard.SetTargetName(anim, objName);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(Button.Background).(SolidColorBrush.Color)"));
            return anim;
        }

        public ColorAnimation SetAnimCirkle(Color Color)
        {
            ColorAnimation anim = new ColorAnimation();
            anim.Duration = new Duration(TimeSpan.FromSeconds(0.2));
            anim.To = Color;
            Storyboard.SetTargetName(anim, "ColorCirkle");
            Storyboard.SetTargetProperty(anim, new PropertyPath(GradientStop.ColorProperty));
            return anim;
        }
        private void btn_unzip(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openExplorer = new OpenFileDialog();
            openExplorer.Filter = "Molk files (*.Molk) | *.molk";
            openExplorer.Multiselect = true;
            openExplorer.InitialDirectory = @"c:\";
            openExplorer.ShowDialog();

            foreach (string s in openExplorer.FileNames)
            {
                filestring.Add(s);
            }
            Fs = string.Join(" ", filestring);
            MessageBox.Show(Fs);
        }
        private void btn_zip(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openExplorer = new OpenFileDialog();
            openExplorer.Filter = "All files (*.*)|*.*";
            openExplorer.Multiselect = true;
            openExplorer.InitialDirectory = @"c:\";
            openExplorer.ShowDialog();

            foreach (string s in openExplorer.FileNames)
            {
                filestring.Add(s);
            }
            Fs = string.Join(" ", filestring);
            MessageBox.Show(Fs);
        }
    }
}