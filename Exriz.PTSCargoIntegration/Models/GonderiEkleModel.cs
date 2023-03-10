using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exriz.PTSCargoIntegration.Models
{
    public class GonderiEkleModel
    {
        /// <summary>
        ///0 gönderebilirsiniz; gönderi numarasını oluşturup geri döndürür. Düzeltme yapılacaksa daha önceden alınmış PTS Numarsı gönderilemelidir. - OPTIONAL
        /// </summary>
        public string PtsNo { get; set; }
        /// <summary>
        /// ecoPTS ise “E” ekspres ise “X” - MANDATORY
        /// </summary>
        public string Servis { get; set; }
        /// <summary>
        /// Alıcı yetkilisinin adı - OPTIONAL
        /// </summary>
        public string Yetkili { get; set; }
        /// <summary>
        /// Alıcı şirketin adı; alıcı şahıssa şahıs adı - MANDATORY
        /// </summary>
        public string SirketAdi { get; set; }
        /// <summary>
        /// Alıcı adresi - MANDATORY
        /// </summary>
        public string Adres { get; set; }
        /// <summary>
        /// Alıcı şehri - MANDATORY
        /// </summary>
        public string Sehir { get; set; }
        /// <summary>
        /// Bazı ülkelerde geçerlidir - MANDATORY
        /// </summary>
        public string Eyalet { get; set; }
        /// <summary>
        /// Alıcı posta kodu - MANDATORY
        /// </summary>
        public string PostaKodu { get; set; }
        /// <summary>
        /// ISO 3166-1 alpha-2 tanımlamasına göre - MANDATORY
        /// </summary>
        public string UlkeKodu { get; set; }
        /// <summary>
        /// Alıcı telefon bilgisi - OPTIONAL
        /// </summary>
        public string Telefon { get; set; }
        /// <summary>
        /// Alıcı eposta adresi	- OPTIONAL
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gönderinin kg cinsinden toplam ağırlığı (String(6,2)) - OPTIONAL
        /// </summary>
        public string ToplamKg { get; set; }
        /// <summary>
        /// Gönderinizin size ait barkodu olan tekil no - MANDATORY
        /// </summary>
        public string SiparisNo { get; set; }
        /// <summary>
        /// Awb üzerinde görünecek olan mal tanımı - MANDATORY
        /// </summary>
        public string MalCinsi { get; set; }
        /// <summary>
        /// Koli adedi - MANDATORY
        /// </summary>
        public string ToplamAdet { get; set; }
        /// <summary>
        ///Malın toplam dağeri - MANDATORY
        /// </summary>
        public string ToplamDeger { get; set; }
        /// <summary>
        /// Kullanılan para birimi - MANDATORY
        /// </summary>
        public string ParaBirimi { get; set; }
        /// <summary>
        /// Fatura numarası
        /// </summary>
        public string FaturaNo { get; set; }
        /// <summary>
        /// Fatura tarihi DATE Format YYYYMMDD şeklinde olmalıdır. 
        /// </summary>
        public string FaturaTarihi { get; set; }
        /// <summary>
        /// Pdf faturanın web adresi (tıklanınca açılacak)
        /// </summary>
        public string EarsivPdf { get; set; }
        /// <summary>
        /// Gönderim yaptığınız ürünleri bu listede tanımlayın.
        /// </summary>
        public List<Urun> Urunler { get; set; }
        /// <summary>
        /// Öndeğer 0, üzeri değerler yetkiye tabii - OPTIONAL
        /// </summary>
        public string Etiket { get; set; }
        /// <summary>
        /// İsim ya da “Customer Service” yazılabilir - OPTIONAL
        /// </summary>
        public string GondericiYetkilisi { get; set; }
        /// <summary>
        /// Gönderinin ebatları - OPTIONAL
        /// </summary>
        public List<Ebat> Ebatlar { get; set; }
        /// <summary>
        /// Gönderinin ödeme türü (string(1)) - OPTIONAL
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// Müşterinin satış türünü belirleme(online satış=1, online satış değil=2) - OPTIONAL
        /// </summary>
        public int BusinessModel { get; set; }
        /// <summary>
        /// Micro ihracat için M, Numune için N, Döküman için D - OPTIONAL (string(1))
        /// </summary>
        public string MusteriBeyanTuru { get; set; }
        /// <summary>
        /// Fatura yetkilisinin adı - OPTIONAL
        /// </summary>
        public string FaturaYetkili { get; set; }
        /// <summary>
        /// Faturadaki ünvan adı - OPTIONAL
        /// </summary>
        public string FaturaUnvan { get; set; }
        /// <summary>
        /// Faturadaki telefon - OPTIONAL
        /// </summary>
        public string FaturaTelefon { get; set; }
        /// <summary>
        /// Faturadaki email - OPTIONAL
        /// </summary>
        public string FaturaEmail { get; set; }
        /// <summary>
        /// Faturadaki adres - OPTIONAL
        /// </summary>
        public string FaturaAdres { get; set; }
        /// <summary>
        /// Faturadaki ülke kodu - OPTIONAL
        /// </summary>
        public string FaturaUlkeKodu { get; set; }
        /// <summary>
        /// Faturadaki posta kodu - OPTIONAL
        /// </summary>
        public string FaturaPostaKodu { get; set; }
        /// <summary>
        /// Faturadaki sehir - OPTIONAL
        /// </summary>
        public string FaturaSehir { get; set; }
        /// <summary>
        /// varış gümrüğe iletilecek commercial invoice pdf linki - OPTIONAL
        /// </summary>
        public string YgArsivPdf { get; set; }
        /// <summary>
        /// Incoterm Flag : DDP için D DAP(DDU) için H - MANDATORY
        /// </summary>
        public string GumrukTipi { get; set; }

    }
    public class Urun
    {
        /// <summary>
        /// Her bir kalem malın tanımı - MANDATORY
        /// </summary>
        public string Aciklama { get; set; }
        /// <summary>
        ///İlgili satırda yer alan kalemim adedi  - MANDATORY
        /// </summary>
        public decimal Miktar { get; set; }
        /// <summary>
        /// İlgili satırda yer alan kalem birimi (pcs,kg)  - MANDATORY
        /// </summary>
        public string Birim { get; set; }
        /// <summary>
        /// İlgili satırda yer alan kalemin birim fiyatı  - MANDATORY
        /// </summary>
        public decimal BirimFiyat { get; set; }
        /// <summary>
        /// Gümrük tarife istatistik pozisyon numarası (20 Karakter)  - OPTIONAL
        /// </summary>
        public string Gtip { get; set; }
        public decimal Discount { get; set; }
        public decimal VatBase { get; set; }
        public string ItemUrl { get; set; }
        public decimal Sivv { get; set; }
        public string Mensei { get; set; } = "TR";

    }
    public class Ebat
    {
        /// <summary>
        /// Gönderinin ebat bilgisi cm cinsinden - OPTIONAL
        /// </summary>
        public decimal En { get; set; }
        /// <summary>
        /// Gönderinin ebat bilgisi cm cinsinden - OPTIONAL
        /// </summary>
        public decimal Boy { get; set; }
        /// <summary>
        /// Gönderinin ebat bilgisi cm cinsinden - OPTIONAL
        /// </summary>
        public decimal Yukseklik { get; set; }
        /// <summary>
        /// Gönderinin ebat bilgisi kg cinsinden - OPTIONAL
        /// </summary>
        public decimal Agirlik { get; set; }
    }
}
