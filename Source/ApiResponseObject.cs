using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace Geocast.Api.Library
{
    /// <summary>
    /// Return class of the library contaning every return type supported
    /// </summary>
    public class ApiResponseObject
    {
        public string error = "Successful Request";
        public ApiDeserializedJsonObject jsonResponse;
        public XmlDocument xmlResponse;
    }

    /// <summary>
    /// Class representing the deserialized object for a json response
    /// </summary>
    public class ApiDeserializedJsonObject
    {
        [JsonProperty("sts")]
        int status { get; set; }

        [JsonProperty("usr")]
        int userId { get; set; }

        [JsonProperty("nres")]
        int totalResults { get; set; }

        [JsonProperty("time")]
        ApiResponseTime requestTime { get; set; }

        [JsonProperty("res")]
        IList<ApiResponseData> responseData { get; set; }
    }

    /// <summary>
    /// Class representing the deserialized object for the json response node "time"
    /// </summary>
    public class ApiResponseTime
    {
        public int unix;
        public string full;
    }

    /// <summary>
    /// Class representing the deserialized object for the json response node "res"
    /// </summary>
    public class ApiResponseData
    {
        public string codigoSetor;
        public string latitude;
        public string longitude;
        public string coords;
        public string nomeMunicipio;
        public string nomeDistrito;
        public double? BV001;
        public double? BV002;
        public double? BV003;
        public double? BV007;
        public double? BV009;
        public double? D01V006;
        public double? D01V007;
        public double? D01V008;
        public double? D01V009;
        public double? D01V010;
        public double? D01V011;
        public double? renda;
    }
}
