using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using TodoSampleMobile.Services.Annotations;
using TodoSampleMobile.Services.GeoLocation;

namespace TodoSampleMobile.Geolocator
{
    public class GeolocatorViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The scheduler
        /// </summary>
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();

        /// <summary>
        /// The geolocator
        /// </summary>
        private IGeolocator _geolocator;

        /// <summary>
        /// The cancel source
        /// </summary>
        private CancellationTokenSource _cancelSource;

        /// <summary>
        /// The position status
        /// </summary>
        private string _positionStatus = string.Empty;

        /// <summary>
        /// The position latitude
        /// </summary>
        private string _positionLatitude = string.Empty;

        /// <summary>
        /// The position longitude
        /// </summary>
        private string _positionLongitude = string.Empty;

        /// <summary>
        /// The get position command
        /// </summary>
        private Command _getPositionCommand;

        private Command _startListeningCommand;
        private Command _stopListeningCommand;
        
        /// <summary>
        /// Gets or sets the position status.
        /// </summary>
        /// <value>The position status.</value>
        public string PositionStatus
        {
            get { return _positionStatus; }
            set
            {
                _positionStatus = value;
                OnPropertyChanged("PositionStatus");
            }
        }

        /// <summary>
        /// Gets or sets the position latitude.
        /// </summary>
        /// <value>The position latitude.</value>
        public string PositionLatitude
        {
            get { return _positionLatitude; }
            set
            {
                _positionLatitude = value;
                OnPropertyChanged("PositionLatitude");
            }
        }

        /// <summary>
        /// Gets or sets the position longitude.
        /// </summary>
        /// <value>The position longitude.</value>
        public string PositionLongitude
        {
            get { return _positionLongitude; }
            set
            {
                _positionLongitude = value;
                OnPropertyChanged("PositionLongitude");
            }
        }

        /// <summary>
        /// Gets the get position command.
        /// </summary>
        /// <value>The get position command.</value>
        public Command GetPositionCommand
        {
            get
            {
                return _getPositionCommand ??
                       (_getPositionCommand = new Command(async () => await GetPosition(), () => Geolocator != null));
            }
        }

        public Command StartListeningCommand
        {
            get
            {
                return _startListeningCommand ??
                       (_startListeningCommand = new Command(StartListening, () => Geolocator != null));
            }
        }

        public Command StopListeningCommand
        {
            get
            {
                return _stopListeningCommand ??
                       (_stopListeningCommand = new Command(StopListening, () => Geolocator != null));
            }
        }

        /// <summary>
        /// Gets the geolocator.
        /// </summary>
        /// <value>The geolocator.</value>
        private IGeolocator Geolocator
        {
            get
            {
                if (_geolocator == null)
                {
                    _geolocator = DependencyService.Get<IGeolocator>();
                    _geolocator.PositionError += OnListeningError;
                    _geolocator.PositionChanged += OnPositionChanged;
                }
                return _geolocator;
            }
        }

        //private void Setup()
        //{
        //    if (this.geolocator != null)
        //    {
        //        return;
        //    }

        //    this.geolocator = DependencyService.Get<IGeolocator>();
        //    this.geolocator.PositionError += OnListeningError;
        //    this.geolocator.PositionChanged += OnPositionChanged;
        //}

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns>Task.</returns>
        private async Task GetPosition()
        {
            _cancelSource = new CancellationTokenSource();

            PositionStatus = string.Empty;
            PositionLatitude = string.Empty;
            PositionLongitude = string.Empty;
            var IsBusy = true;
            await
                Geolocator.GetPositionAsync(10000, _cancelSource.Token, true)
                    .ContinueWith(t =>
                    {
                        IsBusy = false;
                        if (t.IsFaulted)
                        {
                            PositionStatus = ((GeolocationException) t.Exception.InnerException).Error.ToString();
                        }
                        else if (t.IsCanceled)
                        {
                            PositionStatus = "Canceled";
                        }
                        else
                        {
                            PositionStatus = t.Result.Timestamp.ToString("G");
                            PositionLatitude = "La: " + t.Result.Latitude.ToString("N4");
                            PositionLongitude = "Lo: " + t.Result.Longitude.ToString("N4");
                        }
                    }, _scheduler);
        }

        private void StartListening()
        {
             Geolocator.StartListening(3000,10 , true);
        }
        private void StopListening()
        {
            Geolocator.StopListening();
        }

        ////private void CancelPosition ()
        ////{
        ////    CancellationTokenSource cancel = this.cancelSource;
        ////    if (cancel != null)
        ////        cancel.Cancel();
        ////}

        ////		partial void ToggleListening (NSObject sender)
        ////		{
        ////			Setup();
        ////
        ////			if (!this.geolocator.IsListening)
        ////			{
        ////				ToggleListeningButton.SetTitle ("Stop listening", UIControlState.Normal);
        ////
        ////				this.geolocator.StartListening (minTime: 30000, minDistance: 0, includeHeading: true);
        ////			}
        ////			else
        ////			{
        ////				ToggleListeningButton.SetTitle ("Start listening", UIControlState.Normal);
        ////				this.geolocator.StopListening();
        ////			}
        ////		}

        /// <summary>
        /// Handles the <see cref="E:ListeningError" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PositionErrorEventArgs"/> instance containing the event data.</param>
        private void OnListeningError(object sender, PositionErrorEventArgs e)
        {
////			BeginInvokeOnMainThread (() => {
////				ListenStatus.Text = e.Error.ToString();
////			});
        }

        /// <summary>
        /// Handles the <see cref="E:PositionChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PositionEventArgs"/> instance containing the event data.</param>
        private void OnPositionChanged(object sender, PositionEventArgs e)
        {
            Debug.WriteLine("test");
            Device.BeginInvokeOnMainThread(() =>
            {
                PositionStatus = e.Position.Timestamp.ToString("G");
                PositionLatitude = "La: " + e.Position.Latitude.ToString("N4");
                PositionLongitude = "Lo: " + e.Position.Longitude.ToString("N4");
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}