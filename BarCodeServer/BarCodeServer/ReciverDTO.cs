using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeServer
{
    public enum RequestType
    {
        NewConnection = 0,
        AddNewBarCode = 1
    }
    public class ReciverDTO
    {
        public RequestType RequestType { get; set; }

        public string BarCode { get; set; }

        public string IpAddress { get; set; }

    }
}
