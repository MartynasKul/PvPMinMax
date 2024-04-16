using Windows.Devices.Bluetooth.Advertisement;

namespace MinMaxApp.Bluetooth
{
    /// <summary>
    /// Wraps and makes use of the <see cref="BluetoothLEAdvertisementWatcher "/>
    /// for easier usage
    /// </summary>
    public class MinMaxBluetoothLEAdvertisementWatcher
    {
        #region Private Members

        /// <summary>
        /// The underlying bluetooth watcher class
        /// </summary>
        private readonly BluetoothLEAdvertisementWatcher mWatcher;
        #endregion

        #region Public Properties
        
        public bool Listening => mWatcher.Status == BluetoothLEAdvertisementWatcherStatus.Started;

        #endregion

        #region Public Events
        /// <summary>
        /// Fired when the bluetooth watcher stops listening
        /// </summary>
        public event Action StoppedListening = () =>{};

        /// <summary>
        /// Fired when the bluetooth watcher starts listening
        /// </summary>
        public event Action StartedListening = () => { };


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MinMaxBluetoothLEAdvertisementWatcher() 
        {
            // Create bluetooth listener
            mWatcher = new BluetoothLEAdvertisementWatcher
            {
                ScanningMode = BluetoothLEScanningMode.Active
            };

            mWatcher.Received += WatcherAdvertisementReceived;

            //Listen out for stop events or when watcher stops listening
            mWatcher.Stopped += (watcher, e) => 
            {
                // Inform listeners
                StoppedListening();
            };
        
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Listens out for watcher advertisements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void WatcherAdvertisementReceived(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            //TODO
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Starts
        /// </summary>
        public void StartListening() 
        {
            // If already listening
            if (Listening) 
            {
                return;//good
            }
            // Start the watcher
            mWatcher.Start();

            // Inform listeners
            StartedListening();
        }

        #endregion
    }
}
