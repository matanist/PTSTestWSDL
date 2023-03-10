using Exriz.PTSCargoIntegration;
using Exriz.PTSCargoIntegration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Xml;

namespace PTSTestWSDL.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            PTSCargo cargo = new PTSCargo(true);
            // var result = cargo.GetFatura("AWB=2280064233", DateTime.Now.AddDays(-7), DateTime.Now);
            //faturaNO: FT-840301
            var gonderiEkleModel = new GonderiEkleModel
            {
                Adres = "Ev adresi",
                Email = "mail@mail.com",
                Eyalet = "TR",
                MalCinsi = "Evrak",
                ParaBirimi = "TL",
                PostaKodu = "34000",
                PtsNo = "0",
                Sehir = "İstanbul",
                Servis = "E",
                SiparisNo = "6666",
                SirketAdi = "Exriz LTD.",
                Telefon = "+905554555555",
                ToplamAdet = "2",
                ToplamDeger = "200",
                ToplamKg = "2",
                UlkeKodu = "TR",
                Yetkili = "Yetkili Adı",
                Etiket="1",
                
                PayType = "P",
                
                GumrukTipi="H",
                MusteriBeyanTuru = "M",
                BusinessModel=1,
                FaturaYetkili="Test Fatura Yetkilisi",
                FaturaUnvan="Test Fatura Unvan",
                FaturaTelefon="1234567890",
                FaturaEmail="email@email.com",
                FaturaAdres="Test Fatura Adres",
                FaturaUlkeKodu="TR",
                FaturaPostaKodu="34000",
                FaturaSehir="İstanbul",
            };
            var urunler = new List<Urun>();
            urunler.Add(new Urun
            {
                Aciklama = "Çikolatalı Pasta",
                Discount = 0,
                BirimFiyat = 100,
                Birim = "pcs",
                Gtip = "123456789012",
                ItemUrl = "",
                Mensei = "TR",
                Miktar = 2
            });
            gonderiEkleModel.Urunler = urunler;
            var ebatlar = new List<Ebat>();
            ebatlar.Add(new Ebat { Agirlik = 1, Boy = 100, En = 100, Yukseklik = 100 });
            gonderiEkleModel.Ebatlar = ebatlar;
            var resultGonderiEkle = cargo.GonderiEkle(gonderiEkleModel);
            //AWB=2280064201
            //"AWB=2280064233"
        }



    }
}