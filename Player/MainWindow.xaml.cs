using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;

namespace Player
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        public void Timer_Tick(object sendler, EventArgs e)
        {
            seek.Value = mediaElement.Position.TotalSeconds;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
           // OpenFileDialog ofd = new OpenFileDialog();
           // ofd.DefaultExt
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = (double)volume.Value;
        }

        private void seek_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Position = TimeSpan.FromSeconds(seek.Value);
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string fileName = (string)((DataObject)e.Data).GetFileDropList()[0];
            mediaElement.Source = new Uri(fileName);
            mediaElement.LoadedBehavior = MediaState.Manual;
            mediaElement.UnloadedBehavior = MediaState.Manual;
            mediaElement.Volume = (double)volume.Value;
            mediaElement.Play();
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaElement.NaturalDuration.TimeSpan;
            seek.Maximum = ts.TotalSeconds;
            timer.Start();
        }
    }
}