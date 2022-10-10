using System;
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
using System.IO;


namespace AudioVideoPlay
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		/*private long CurrentPosition = 0;
		private double duration = 0;
		private bool IsPlaying = false;

		private void Play_Click(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Playing....");

			//this.media.Position = System.TimeSpan.FromSeconds(this.startPosition);
			if (this.IsPlaying == false)
			{
				this.media.Volume = 100;
				this.media.Play();
				this.IsPlaying = true;
			}
			else
			{
				this.media.Pause();
				this.IsPlaying = false;
			}
			
		}


		private void Stop_Click(object sender, RoutedEventArgs e)
		{
			this.IsPlaying = false;
			this.slider.Value = 0;
			this.media.Stop();
		}

		private void Pause_Click(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Called.");
			this.IsPlaying = false;
			this.media.Pause();
		}

		private void Media_Loaded(object sender, RoutedEventArgs e)
		{
			//this.media.Position = System.TimeSpan.FromSeconds(this.startPosition);
		}

		private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			double currentPosition = (this.duration / 10) * e.NewValue;
			this.media.Position = System.TimeSpan.FromSeconds(currentPosition);
			this.slider.Value = e.NewValue;
		}

		private void Media_MouseEnter(object sender, MouseEventArgs e)
		{
			
		}

		private void Media_MediaOpened(object sender, RoutedEventArgs e)
		{
			this.IsPlaying = true;

			this.duration = this.media.NaturalDuration.TimeSpan.TotalSeconds;
			this.changinTime.Content = "";
			this.mediaDuration.Content = this.media.NaturalDuration.ToString();
		}

		private void AddSongs_Click(object sender, RoutedEventArgs e)
		{
			var ofd = new Microsoft.Win32.OpenFileDialog()
			{
				Filter = "Video and Audio files (*.*)|*.*"
			};
			var result = ofd.ShowDialog();
			if (result == false)
			{
				return;
			}
			this.media.Source = new System.Uri(ofd.FileName);
			this.mediaName.Content = this.GetFileName(ofd.FileName);

			this.media.Play();
			

			Console.WriteLine(this.GetFileName(ofd.FileName));
		}

		private string GetFileName(string filePath)
		{
			var afterDrive = filePath.Split(':')[1];
			var separator = Path.DirectorySeparatorChar;

			var outputName = afterDrive.Split(separator)[afterDrive.Split(separator).Length - 1];
			return outputName;
		}

		private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.IsPlaying == false)
			{
				this.media.Volume = 100;
				this.media.Play();
				this.IsPlaying = true;
			}
			else
			{
				this.media.Pause();
				this.IsPlaying = false;
			}
		}*/

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			//this.media.Width = System.Windows.SystemParameters.PrimaryScreenWidth+100;
			//this.media.Height = System.Windows.SystemParameters.PrimaryScreenHeight-120;
			this.sideBar.Height = System.Windows.SystemParameters.PrimaryScreenHeight-500;
			this.bottomTab.Width = System.Windows.SystemParameters.PrimaryScreenWidth + 200;
		}

		private void Window_MouseEnter(object sender, MouseEventArgs e)
		{
			//this.mediaName.Foreground = Brushes.Red;
		}

		private void Window_MouseLeave(object sender, MouseEventArgs e)
		{
			//this.mediaName.Foreground = Brushes.Green;
		}
	}
}
