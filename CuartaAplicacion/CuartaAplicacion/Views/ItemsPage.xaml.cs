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
using PrimerAplicacion.Model;

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

            var jugadores = AgregarJugadores();

            lvJugadores.ItemsSource = jugadores;
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

        private List<Jugador> AgregarJugadores()
        {
            var jugadores = new List<Jugador>();

            jugadores.Add(new Jugador()
            {
                Nombre = "Wilfredo",
                Apellido = "Caballero",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/181440_sq-300_jpg",
                Pais = "Argentina",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/arg"
            });

            jugadores.Add(new Jugador()
            {
                Nombre = "Eden",
                Apellido = "Hazard",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/273996_sq-300_jpg",
                Pais = "Bélgica",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/bel"
            });

            jugadores.Add(new Jugador()
            {
                Nombre = "James",
                Apellido = "Rodriguez",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/269058_sq-300_jpg",
                Pais = "Colombia",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/col"
            });

            jugadores.Add(new Jugador()
            {
                Nombre = "Andrés",
                Apellido = "Iniesta",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/183857_sq-300_jpg",
                Pais = "España",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/esp"
            });

            jugadores.Add(new Jugador()
            {
                Nombre = "Guillermo",
                Apellido = "Ochoa",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/215285_sq-300_jpg",
                Pais = "México",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/mex"
            });

            jugadores.Add(new Jugador()
            {
                Nombre = "Ahmed",
                Apellido = "Musa",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/344714_sq-300_jpg",
                Pais = "Nigeria",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/nga"
            });

            jugadores.Add(new Jugador()
            {
                Nombre = "Luis",
                Apellido = "Advíncula",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/349697_sq-300_jpg",
                Pais = "Perú",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/per"
            });

            jugadores.Add(new Jugador()
            {
                Nombre = "Shinwook",
                Apellido = "Kim",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/321745_sq-300_jpg",
                Pais = "Corea del sur",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/kor"
            });

            jugadores.Add(new Jugador()
            {
                Nombre = "Fernando",
                Apellido = "Muslera",
                Foto = "https://api.fifa.com/api/v1/picture/players/2018fwc/229498_sq-300_jpg",
                Pais = "Uruguay",
                Bandera = "https://api.fifa.com/api/v1/picture/flags-fwc2018-4/uru"
            });

            return jugadores;
        }
    }

    public class ConnectivityChangedEventArgs : EventArgs
    {
        public bool IsConnected { get; set; }
    }

    public delegate void ConnectivityChangedEventHandler(object sender, ConnectivityChangedEventArgs e);
}