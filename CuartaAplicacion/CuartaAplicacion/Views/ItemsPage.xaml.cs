using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CuartaAplicacion.Models;
using CuartaAplicacion.Views;
using CuartaAplicacion.ViewModels;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using TercerAplicacion.Services;

namespace CuartaAplicacion.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;
        double Latitud = 0;
        double Longitud = 0;
        TiempoService servicio;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                txtEstadoConexion.Text = "Estado: " + (args.IsConnected ? "Conectado" : "Desconectado");
            };

            IsLocationAvailable();
            servicio = new TiempoService();
        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public async Task<Position> GetCurrentLocation()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                //Verifico si tengo una posicion en el cache
                position = await locator.GetLastKnownLocationAsync();

                if (position == null)
                {
                    if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                    {
                        txtPosicionGps.Text = "No se pudo obtener la posicion porque el GPSno está disponible.";
                        //not available or enabled
                        return null;
                    }

                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Unable to get location: " + ex);
                txtPosicionGps.Text = "No se pudo obtener la posicion: " + ex.Message;
            }

            if (position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            output = string.Format("Lat: {0} \nLong: {1} \nAccuracy: {2}",
                    position.Latitude, position.Longitude, position.Accuracy);

            Latitud = position.Latitude;
            Longitud = position.Longitude;

            //Debug.WriteLine(output);
            txtPosicionGps.Text = output;

            return position;
        }

        protected override async void OnAppearing()
        {
            txtEstadoConexion.Text = "Estado: " + (CrossConnectivity.Current.IsConnected ? "Conectado" : "Desconectado");
            await GetCurrentLocation();
            var datosDleTiempo = await servicio.GetDatosDelTiempoAsync(Latitud, Longitud);
            txtTemperatura.Text = string.Format("Temp: {0} °C", datosDleTiempo.main.temp);
        }
    }

    public class ConnectivityChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
    }

    public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);

}