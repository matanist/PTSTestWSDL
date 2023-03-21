using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exriz.PTSCargoIntegration.Models
{
    public class Fiyat
    {
        public string serviceName { get; set; }
        public string currency { get; set; }
        public string price { get; set; }
        public string priceTL { get; set; }
        public string chargableWeight { get; set; }
    }

    public class PriceResultModel
    {
        public List<Fiyat> Fiyat { get; set; }
    }
}
