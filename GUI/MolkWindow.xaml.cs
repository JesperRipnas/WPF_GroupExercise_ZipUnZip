﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
namespace GUI
{
    /// <summary>
    /// Interaction logic for MolkWindow.xaml
    /// </summary>
    public partial class MolkWindow : Window
    {
        Storyboard storyboard;
        public MolkWindow()
        {
            InitializeComponent();
            CollectionViewSource itemCollectionViewSource;
            itemCollectionViewSource = (CollectionViewSource)FindResource("ItemCollectionViewSource");
            itemCollectionViewSource.Source = MainWindow.molkFiles;
        }
        private void Btn_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Btn_AddFile(object sender, RoutedEventArgs e)
        {
            MainWindow.OpenExplorerMolkFiles();
        }

        private void ButtonPref_Click(object sender, RoutedEventArgs e)
        {
            PrefWindow win2 = new PrefWindow();
            win2.ShowDialog();
        }

        private void Btn_Molk(object sender, RoutedEventArgs e)
        {
            if(MainWindow.molkFiles.Count > 0)
            {
                // Ask user to choose archive name & path
                MolkPathPickerWindow picker = new MolkPathPickerWindow();
                picker.ShowDialog();
            }
            else
            {
                MessageBox.Show(GeneralError.needAtLeastOneFile,
                "Error: Add file(s)",
                MessageBoxButton.OK,
                MessageBoxImage.Warning
                );
            }
        }
        
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            Color background = ((SolidColorBrush)btn.BorderBrush).Color;
            storyboard = new Storyboard();
            storyboard.Children.Add(SetAnimButton(background, btn.Name));
            storyboard.Begin(this);
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            storyboard = new Storyboard();
            storyboard.Children.Add(SetAnimButton(Color.FromRgb(255, 255, 255), btn.Name));
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
        private void Btn_ClearMolkList(object sender, RoutedEventArgs e)
        {
            if (MainWindow.molkFiles.Count > 0)
            {
                if (MessageBox.Show(GeneralError.confirmationRemoveFilesInList, "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    MainWindow.molkFiles.Clear();
                }
            }
        }

        private void RemoveItem_Press(object sender, RoutedEventArgs e)
        {
            MainWindow.molkFiles.Remove((MolkFile)((Button)e.OriginalSource).DataContext);
        }
    }
}