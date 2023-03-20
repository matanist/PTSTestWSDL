using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exriz.PTSCargoIntegration.Models
{
    public class GetPriceModel
    {
        public string UlkeKodu { get;  set; }
        public List<Ebat> Ebatlar { get; set; }
    }
}
