using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace Geocast.Api.Library
{
    public class ApiLibrary
    {
        private string url = "http://geocast.com.br/system/index.php/api?api=$key$&frm=$frm$&act=$act$&nelat=$nelat$&nelng=$nelng$&swlat=$swlat$&swlng=$swlng$";
        public string key { get; set; }
        public double northEastLatitude { get; set; }
        public double northEastLongitude { get; set; }
        public double southWestLatitude { get; set; }
        public double southWestLongitude { get; set; }
        public string responseType { get; set; }
        public string method { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">API key (development or production)</param>
        public ApiLibrary(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// Executes the request to the API
        /// </summary>
        /// <returns>Response from server with formatted string data</returns>
        public ApiResponseObject Do()
        {
            ApiResponseObject apiResponseObject = new ApiResponseObject();

            this.url = this.url.Replace("$key$", this.key);
            this.url = this.url.Replace("$frm$", this.responseType);
            this.url = this.url.Replace("$act$", this.method);
            this.url = this.url.Replace("$nelat$", this.northEastLatitude.ToString().Replace(',', '.'));
            this.url = this.url.Replace("$nelng$", this.northEastLongitude.ToString().Replace(',', '.'));
            this.url = this.url.Replace("$swlat$", this.southWestLatitude.ToString().Replace(',', '.'));
            this.url = this.url.Replace("$swlng$", this.southWestLongitude.ToString().Replace(',', '.'));

            try
            {
                // Create a request using a URL that can receive a post. 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);

                // Read the content fully up to the end.
                string responseFromServer = reader.ReadToEnd();

                // Toggle return types to see what how to treat response
                switch (this.responseType)
                {
                    case "json":
                        apiResponseObject.jsonResponse = new ApiDeserializedJsonObject();
                        apiResponseObject.jsonResponse = JsonConvert.DeserializeObject<ApiDeserializedJsonObject>(responseFromServer);
                        break;
                    case "xml":
                        apiResponseObject.xmlResponse = new XmlDocument();
                        apiResponseObject.xmlResponse.LoadXml(responseFromServer);
                        break;
                }
            }
            catch (Exception ex)
            {
                apiResponseObject.error = ex.Message;
                return apiResponseObject;
            }

            return apiResponseObject;
        }

        /// <summary>
        /// Cleans the object for new request
        /// </summary>
        public void CleanRequestInformation()
        {
            this.method = string.Empty;
            this.responseType = string.Empty;
            this.northEastLatitude = 0;
            this.northEastLongitude = 0;
            this.southWestLatitude = 0;
            this.southWestLongitude = 0;
        }
    }

    /// <summary>
    /// <summary>
    /// Style class for enumerating API response types
    /// </summary>
    public class ApiResponseTypes
    {
        public static string XML = "xml";
        public static string JSON = "json";
    }

    /// <summary>
    /// Style class for enumerating API methods
    /// </summary>
    public class ApiMethods
    {
        public static string GET_SECTOR_BY_BOUNDS = "census.get_sectors_by_bounds";
    }
}
