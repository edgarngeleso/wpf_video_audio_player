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
using System.Data;
using AudioVideoPlay.screens;

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

		private long CurrentPosition = 0;
		private int CurrentFileIndex = 0;
		private double duration = 0;
		private bool IsPlaying = false;
		private DataTable filesDataTable = new DataTable();
		
		private double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
		private double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

		private void CreateListData()
		{
			filesDataTable.Columns.Add("ID", typeof(int));
			filesDataTable.Columns.Add("fileName");
			filesDataTable.Columns.Add("filePath");
		}


		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.CreateListData();
			this.media.Width = this.screenWidth+100;
			this.media.Height = this.screenHeight;
			this.sideBar.Height = this.screenHeight;
			this.sideBar.Margin = new Thickness(0,-this.screenHeight,0,0);
			this.bottomTab.Width = this.screenWidth - 200;
			this.bottomTab.Margin = new Thickness(200, -190, 0, 0);
		}

		private void Window_MouseEnter(object sender, MouseEventArgs e)
		{
			
			this.bottomTab.Opacity = 1;
			this.sideBar.Opacity = 1;
			this.mediaName.Opacity = 1;
		}

		private void Window_MouseLeave(object sender, MouseEventArgs e)
		{
			this.bottomTab.Opacity = 0;
			this.sideBar.Opacity = 0;
			this.mediaName.Opacity = 0;
		}


		private void PlayPause_Click(object sender, RoutedEventArgs e)
		{
			this.Play_Pause();
		}

		private void Play_Pause()
		{
			if (this.IsPlaying)
			{
				this.media.Pause();
				this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/play.png", UriKind.Relative));
				this.IsPlaying = false;
			}
			else
			{

				this.media.Play();
				this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/pause.png", UriKind.Relative));
				this.IsPlaying = true;
			}
		}

		private void Stop_Click(object sender, RoutedEventArgs e)
		{
			this.media.Stop();
			this.IsPlaying = false;
			this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/play.png", UriKind.Relative));
			this.media.Position = System.TimeSpan.FromSeconds(0);
		}

		private void Media_Loaded(object sender, RoutedEventArgs e)
		{
			this.volumeSlider.Value = 2.5;
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			this.Play_Next();
		}

		private void Play_Next()
		{
			this.CurrentFileIndex++;
			if (this.CurrentFileIndex > this.files.Items.Count - 1)
			{
				this.CurrentFileIndex = 0;
			}
			this.media.Source = new System.Uri(filesDataTable.Rows[this.CurrentFileIndex]["filePath"].ToString());
			this.mediaName.Content = filesDataTable.Rows[this.CurrentFileIndex]["fileName"].ToString();
			this.mediaSlider.Value = 0;

			this.media.Play();
			this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/pause.png", UriKind.Relative));
			this.IsPlaying = true;
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			this.Play_Previous();
		}

		private void Play_Previous()
		{
			if (this.CurrentFileIndex == 0)
			{
				this.CurrentFileIndex = this.files.Items.Count - 1;
			}
			else
			{
				this.CurrentFileIndex--;
			}


			this.media.Source = new System.Uri(filesDataTable.Rows[this.CurrentFileIndex]["filePath"].ToString());
			this.mediaName.Content = filesDataTable.Rows[this.CurrentFileIndex]["fileName"].ToString();
			this.mediaSlider.Value = 0;

			this.media.Play();
			this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/pause.png", UriKind.Relative));
			this.IsPlaying = true;
		}


		private void Media_MediaOpened(object sender, RoutedEventArgs e)
		{
			this.IsPlaying = true;

			this.duration = this.media.NaturalDuration.TimeSpan.TotalSeconds;
			this.changinTime.Content = "00:00:00";
			this.mediaDuration.Content = this.media.NaturalDuration.ToString();

			//Add key down event to the window
			this.window.KeyDown += new KeyEventHandler(Window_KeyDown);
		}

		private void MediaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			double currentPosition = (this.duration / 10) * e.NewValue;
			this.media.Position = System.TimeSpan.FromSeconds(currentPosition);
			this.mediaSlider.Value = e.NewValue;
		}

		private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			this.media.Volume = e.NewValue/10;
			this.volumeSlider.Value = e.NewValue;
		}

		private void AddMedia_Click(object sender, RoutedEventArgs e)
		{
			this.selectedFiles.Opacity = 1;
			/*var ofd = new Microsoft.Win32.OpenFileDialog()
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
			this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/pause.png", UriKind.Relative));
			this.IsPlaying = true;
			*/
		}

		private string GetFileName(string filePath)
		{
			var afterDrive = filePath.Split(':')[1];
			var separator = Path.DirectorySeparatorChar;

			var outputName = afterDrive.Split(separator)[afterDrive.Split(separator).Length - 1];
			return outputName;
		}

		private void Container_KeyDown(object sender, KeyEventArgs e)
		{
			
			
		}

		private void Playlist_Click(object sender, RoutedEventArgs e)
		{
			this.selectedFiles.Opacity = 1;


			var ofd = new Microsoft.Win32.OpenFileDialog()
			{
				Filter = "Video and Audio files (*.*)|*.*",
				Multiselect = true,
				FilterIndex = 2,
				RestoreDirectory = true
			};

			var result = ofd.ShowDialog();
			Stream stream;
			if (result == true)
			{
				
				for(var i = 0; i < ofd.FileNames.Length; i++)
				{
					try
					{
						String file = ofd.FileNames[i];
						if ((stream = ofd.OpenFile()) != null)
						{
							using (stream)
							{
								this.filesDataTable.Rows.Add(i,this.GetFileName(file),file);
								var num = this.files.Items.Count > 0 ? this.files.Items.Count + 1 : 1;
								this.files.Items.Add($"{num}.{this.GetFileName(file)}");
							}
						}
					}catch(Exception ex)
					{
						MessageBox.Show($"Ooops!Unable to read file.Error={ex.Message}");
						return;
					}
				}

				//Play first file if the player isn't playing when a new file is added.
				if (!IsPlaying && this.CurrentFileIndex==0)
				{
					this.media.Source = new System.Uri(filesDataTable.Rows[0]["filePath"].ToString());
					this.mediaName.Content = filesDataTable.Rows[0]["fileName"].ToString();

					this.media.Play();
					this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/pause.png", UriKind.Relative));
					this.mediaSlider.Value = 0;
					this.IsPlaying = true;
				}
				
			}
		}

		private void CloseFileList_Click(object sender, RoutedEventArgs e)
		{
			this.selectedFiles.Opacity = 0;
		}

		private void Files_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Console.WriteLine(this.files.SelectedIndex);
			this.CurrentFileIndex = this.files.SelectedIndex;
			this.mediaSlider.Value = 0;
			//MessageBox.Show($"{filesDataTable.Rows[this.files.SelectedIndex]["fileName"]}");

			this.media.Source = new System.Uri(filesDataTable.Rows[this.CurrentFileIndex]["filePath"].ToString());
			this.mediaName.Content = filesDataTable.Rows[this.CurrentFileIndex]["fileName"].ToString();

			this.media.Play();
			this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/pause.png", UriKind.Relative));
			this.IsPlaying = true;

			this.selectedFiles.Opacity = 0;
		}

		private void Media_MediaEnded(object sender, RoutedEventArgs e)
		{
			this.CurrentFileIndex++;
			if (this.CurrentFileIndex > this.files.Items.Count-1)
			{
				this.CurrentFileIndex = 0;
			}
			
			this.media.Source = new System.Uri(filesDataTable.Rows[this.CurrentFileIndex]["filePath"].ToString());
			this.mediaName.Content = filesDataTable.Rows[this.CurrentFileIndex]["fileName"].ToString();

			this.media.Play();
			this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/pause.png", UriKind.Relative));
			this.mediaSlider.Value = 0;
			this.IsPlaying = true;
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Right)
			{
				this.mediaSlider.Value+=0.5;
			}
			else if (e.Key == Key.Left)
			{
				this.mediaSlider.Value-=0.5;
			}
			else if (e.Key == Key.Up)
			{
				this.volumeSlider.Value += 0.5;
			}
			else if (e.Key == Key.Down)
			{
				this.volumeSlider.Value -= 0.5;
			}else if(e.Key == Key.Space)
			{
				this.Play_Pause();
			}else if(e.Key == Key.N)
			{
				this.Play_Next();
			}else if(e.Key == Key.P)
			{
				this.Play_Previous();
			}else if(e.Key == Key.L)
			{

				if(this.selectedFiles.Opacity == 1)
				{
					this.selectedFiles.Opacity = 0;
				}
				else
				{
					this.selectedFiles.Opacity = 1;
				}
			}
		}

		private void SettingsPage_Click(object sender, RoutedEventArgs e)
		{
			Settings settings = new Settings();
			settings.Show();
		}

		private void AboutPage_Click(object sender, RoutedEventArgs e)
		{
			About about = new About();
			about.Show();
		}

		private void GoToPlaylist_Click(object sender, RoutedEventArgs e)
		{
			Playlist playlist = new Playlist();
			playlist.Show();
		}

		private void AddFiles_Click(object sender, RoutedEventArgs e)
		{
			var ofd = new Microsoft.Win32.OpenFileDialog()
			{
				Filter = "Video and Audio files (*.*)|*.*",
				Multiselect = true,
				FilterIndex = 2,
				RestoreDirectory = true
			};

			var result = ofd.ShowDialog();
			Stream stream;
			if (result == true)
			{

				for (var i = 0; i < ofd.FileNames.Length; i++)
				{
					try
					{
						String file = ofd.FileNames[i];
						if ((stream = ofd.OpenFile()) != null)
						{
							using (stream)
							{
								this.filesDataTable.Rows.Add(i, this.GetFileName(file), file);
								var num = this.files.Items.Count > 0 ? this.files.Items.Count + 1 : 1;
								this.files.Items.Add($"{num}.{this.GetFileName(file)}");
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Ooops!Unable to read file.Error={ex.Message}");
						return;
					}
				}



				//Play first file if the player isn't playing when a new file is added.
				if (!IsPlaying && this.CurrentFileIndex == 0)
				{
					this.media.Source = new System.Uri(filesDataTable.Rows[0]["filePath"].ToString());
					this.mediaName.Content = filesDataTable.Rows[0]["fileName"].ToString();

					this.media.Play();
					this.playPauseImage.Source = new BitmapImage(new Uri(@"/assets/pause.png", UriKind.Relative));
					this.mediaSlider.Value = 0;
					this.IsPlaying = true;
				}
			}
		}
	}
}
