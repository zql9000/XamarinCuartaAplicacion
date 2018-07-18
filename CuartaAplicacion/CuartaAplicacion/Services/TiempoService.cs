using CuartaAplicacion.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TercerAplicacion.Services
{
    public class TiempoService
    {
        HttpClient client;
        private const string url = "http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric&appid=fc9f6c524fc093759cd28d41fda89a1b";

        public TiempoService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<DatosDelTiempo> GetDatosDelTiempoAsync(double latitud, double longitud)
        {
            DatosDelTiempo datosDelTiempo = null;

            var uri = new Uri(string.Format(url,latitud, longitud));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                datosDelTiempo = JsonConvert.DeserializeObject<DatosDelTiempo>(content);
            }

            return datosDelTiempo;
        }
    }
}
