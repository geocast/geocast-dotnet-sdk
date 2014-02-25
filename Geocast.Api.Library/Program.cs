using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Geocast.Api.Library;

namespace Geocast.ClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiLibrary library = new ApiLibrary("355a833e745e68c0cc50777b49174eed");
            library.responseType = ApiResponseTypes.XML;
            library.method = ApiMethods.GET_SECTOR_BY_BOUNDS;
            library.northEastLatitude = -23.547382001886447;
            library.northEastLongitude = -46.629920433125676;
            library.southWestLatitude = -23.553605815531405;
            library.southWestLongitude = -46.636709739603816;

            // Response object
            ApiResponseObject response = library.Do();

            // Cleans parameters for new request
            library.CleanRequestInformation();
        }
    }
}
