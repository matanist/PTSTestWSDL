using Exriz.PTSCargoIntegration.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Exriz.PTSCargoIntegration
{
    public class PTSCargo
    {
        private readonly string _url;
        private string _action;
        private string _userName;
        private string _password;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTest">If you set true you don't need username and password</param>
        /// <param name="userName">If you set isTest false, you have to set</param>
        /// <param name="password">If you set isTest false, you have to set</param>
        /// <exception cref="NotImplementedException"></exception>
        public PTSCargo(bool isTest, string userName = "", string password = "")
        {
            if (isTest)
            {
                _url = "https://www.pts.net:5161/api/shipmentv4/api.php";
                _userName = "DEV-27541194384867335";
                _password = "bbd47bf0-8ff8-11ec-83f4-005056b8981b";
            }
            else
            {
                _url = "https://www.pts.net/api/shipmentv4/api.php";
                if (userName == "" || password == "")
                {
                    throw new NotImplementedException();
                }
                _userName = userName;
                _password = password;
            }


        }
        public string GonderiEkle(GonderiEkleModel gonderiEkleModel)
        {
            _action = "/addShipment";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelopeForGonderiEkle(gonderiEkleModel);
            //2007-04-17
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            try
            {
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(soapResult);
                        var resultText = xmlDocument.InnerText;
                        return resultText;
                    }
                }
            }
            catch (Exception exp)
            {

                return exp.Message;
            }
            
        }
        public string GetFatura(string faturaNo, DateTime faturaBaslangicTarihi, DateTime faturaBitisTarihi)
        {
            _action = "/getFatura";
            var faturaBaslangic = faturaBaslangicTarihi.ToString("yyyy-MM-dd");
            var faturaBitis = faturaBitisTarihi.ToString("yyyy-MM-dd");
            XmlDocument soapEnvelopeXml = CreateSoapEnvelopeForGetFatura(faturaNo, faturaBaslangic, faturaBitis);
            //2007-04-17
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                    return soapResult;
                }
            }
        }
        public string GetPrices(GetPriceModel getPricesModel)
        {
            _action = "/getPrices";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelopeGetPrices(getPricesModel);
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            try
            {
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(soapResult);
                        var resultText = xmlDocument.InnerText;
                        return resultText;
                    }
                }
            }
            catch (Exception exp)
            {

                return exp.Message;
            }

        }
        public string GetTCMBKur(string currencyCode)
        {
            string exchangeRate = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(exchangeRate);
            string tl = xmlDoc.SelectSingleNode($"Tarih_Date/Currency[@Kod='{currencyCode}']/BanknoteBuying").InnerXml;
            return tl;
        }
        private HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";

            return webRequest;
        }
        private XmlDocument CreateSoapEnvelopeForGetFatura(string faturaNo, string faturaBaslangicTarihi, string faturaBitisTarihi)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();

            soapEnvelopeDocument.LoadXml(
                @$"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=
                    ""http://schemas.xmlsoap.org/soap/envelope/"" 
                    xmlns:urn=""urn:server"">
                    <soapenv:Header/>
                    <soapenv:Body>
                     <urn:getFatura soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                        <kullanici xsi:type=""xsd:string"">{_userName}</kullanici>
                        <sifre xsi:type=""xsd:string"">{_password}</sifre>
                        <faturaNo xsi:type=""xsd:string"">{faturaNo}</faturaNo>
                        <faturaBaslangicTarihi xsi:type=""xsd:string"">{faturaBaslangicTarihi}</faturaBaslangicTarihi>
                        <faturaBitisTarihi xsi:type=""xsd:string"">{faturaBitisTarihi}</faturaBitisTarihi>
                    </urn:getFatura>
                    </soapenv:Body>
                </soapenv:Envelope>");
            return soapEnvelopeDocument;
        }
        private XmlDocument CreateSoapEnvelopeForGonderiEkle(GonderiEkleModel gonderiEkleModel)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();

            var xml =
            @$"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:server"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"">
            <soapenv:Header/>
               <soapenv:Body>
                  <urn:addshipment soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                     <kullanici xsi:type=""xsd:string"">{_userName}</kullanici>
                     <sifre xsi:type=""xsd:string"">{_password}</sifre>
                     <ptsno xsi:type=""xsd:string"">{gonderiEkleModel.PtsNo}</ptsno>
                     <servis xsi:type=""xsd:string"">{gonderiEkleModel.Servis}</servis>
                     <yetkili xsi:type=""xsd:string"">{gonderiEkleModel.Yetkili}</yetkili>
                     <sirketadi xsi:type=""xsd:string"">{gonderiEkleModel.SirketAdi}</sirketadi>
                     <adres xsi:type=""xsd:string"">{gonderiEkleModel.Adres}</adres>
                     <sehir xsi:type=""xsd:string"">{gonderiEkleModel.Sehir}</sehir>
                     <eyalet xsi:type=""xsd:string"">{gonderiEkleModel.Eyalet}</eyalet>
                     <postakodu xsi:type=""xsd:string"">{gonderiEkleModel.PostaKodu}</postakodu>
                     <ulkekodu xsi:type=""xsd:string"">{gonderiEkleModel.UlkeKodu}</ulkekodu>
                     <telefon xsi:type=""xsd:string"">{gonderiEkleModel.Telefon}</telefon>
                     <email xsi:type=""xsd:string"">{gonderiEkleModel.Email}</email>
                     <toplamkg xsi:type=""xsd:decimal"">{gonderiEkleModel.ToplamKg}</toplamkg>
                     <siparisno xsi:type=""xsd:string"">{gonderiEkleModel.SiparisNo}</siparisno>
                     <malcinsi xsi:type=""xsd:string"">{gonderiEkleModel.MalCinsi}</malcinsi>
                     <toplamadet xsi:type=""xsd:string"">{gonderiEkleModel.ToplamAdet}</toplamadet>
                     <toplamdeger xsi:type=""xsd:decimal"">{gonderiEkleModel.ToplamDeger}</toplamdeger>
                     <parabirimi xsi:type=""xsd:string"">{gonderiEkleModel.ParaBirimi}</parabirimi>";
            xml += @"<fatura xsi:type=""urn:lines"" SOAP-ENC:arrayType=""urn:line[]"">";
            foreach (var product in gonderiEkleModel.Urunler)
            {
                xml += @$"<line>
                            <aciklama xsi:type=""xsd:string"">{product.Aciklama}</aciklama>
                            <miktar xsi:type=""xsd:decimal"">{product.Miktar}</miktar>
                            <birim xsi:type=""xsd:string"">{product.Birim}</birim>
                            <birimfiyat xsi:type=""xsd:decimal"">{product.BirimFiyat}</birimfiyat>
                            <gtip xsi:type=""xsd:string"">{product.Gtip}</gtip>   
                            <discount xsi:type=""xsd:decimal"">{product.Discount}</discount>
                            <vatbase xsi:type=""xsd:decimal"">{product.VatBase}</vatbase>
                            <itemUrl xsi:type=""xsd:string"">{product.ItemUrl}</itemUrl>
                            <sivv xsi:type=""xsd:decimal"">{product.Sivv}</sivv>
                            <mensei xsi:type=""xsd:string"">{product.Mensei}</mensei>              
                       </line>";
            }
            xml += @"</fatura>";

            xml += @$"<faturano xsi:type=""xsd:string"">{gonderiEkleModel.FaturaNo}</faturano>
                     <faturatarihi xsi:type=""xsd:string"">{gonderiEkleModel.FaturaTarihi}</faturatarihi>
                     <earsivpdf xsi:type=""xsd:string"">{gonderiEkleModel.EarsivPdf}</earsivpdf>
                     <etiket xsi:type=""xsd:string"">{gonderiEkleModel.Etiket}</etiket>
                     <gonderici_yetkilisi xsi:type=""xsd:string"">{gonderiEkleModel.GondericiYetkilisi}</gonderici_yetkilisi>";
            xml += @"<ebat xsi:type=""urn:dims"" SOAP-ENC:arrayType=""urn:dim[]"">";
            if(gonderiEkleModel.Ebatlar!=null)
            foreach (var ebat in gonderiEkleModel.Ebatlar)
            {
                xml += @$"
                    <dim>
                        <en xsi:type=""xsd:string"">{ebat.En}</en>
                        <boy xsi:type=""xsd:decimal"">{ebat.Boy}</boy>
                        <yukseklik xsi:type=""xsd:string"">{ebat.Yukseklik}</yukseklik>
                        <agirlik xsi:type=""xsd:decimal"">{ebat.Agirlik}</agirlik>
                    </dim>";
            }
            xml += "</ebat>";

            xml += @$"<payType xsi:type=""xsd:string"">{gonderiEkleModel.PayType}</payType>
                     <gbeyanTutar xsi:type=""xsd:string"">{gonderiEkleModel.MusteriBeyanTuru}</gbeyanTutar>
                     <shipAccessCode xsi:type=""xsd:string"">?</shipAccessCode>
                     <ygarsivpdf xsi:type=""xsd:string"">{gonderiEkleModel.YgArsivPdf}</ygarsivpdf>
                     <vatno xsi:type=""xsd:string"">?</vatno>
                     <eorino xsi:type=""xsd:string"">?</eorino>
                     <gumruktipi xsi:type=""xsd:string"">{gonderiEkleModel.GumrukTipi}</gumruktipi>
                     <ioss xsi:type=""xsd:string"">?</ioss>
                     <musteriBeyanTuru xsi:type=""xsd:string"">{gonderiEkleModel.MusteriBeyanTuru}</musteriBeyanTuru>
                     <businessmodel xsi:type=""xsd:integer"">{gonderiEkleModel.BusinessModel}</businessmodel>
                     <faturayetkili xsi:type=""xsd:string"">{gonderiEkleModel.FaturaYetkili}</faturayetkili>
                     <faturaunvan xsi:type=""xsd:string"">{gonderiEkleModel.FaturaUnvan}</faturaunvan>
                     <faturatelefon xsi:type=""xsd:string"">{gonderiEkleModel.FaturaTelefon}</faturatelefon>
                     <faturaemail xsi:type=""xsd:string"">{gonderiEkleModel.FaturaEmail}</faturaemail>
                     <faturaadres xsi:type=""xsd:string"">{gonderiEkleModel.FaturaAdres}</faturaadres>
                     <faturaulkekodu xsi:type=""xsd:string"">{gonderiEkleModel.FaturaUlkeKodu}</faturaulkekodu>
                     <faturapostakodu xsi:type=""xsd:string"">{gonderiEkleModel.FaturaPostaKodu}</faturapostakodu>
                     <faturasehir xsi:type=""xsd:string"">{gonderiEkleModel.FaturaSehir}</faturasehir>
                  </urn:addshipment>
               </soapenv:Body>
            </soapenv:Envelope>";
            soapEnvelopeDocument.LoadXml(xml);
            return soapEnvelopeDocument;
            //soapenc:arrayType=""urn:line[2]""
            //soapenc:arrayType""urn:dim[2]""
        }
        private XmlDocument CreateSoapEnvelopeGetPrices(GetPriceModel getPriceModel)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();

            var xml =
            @$"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:server"" xmlns:SOAP-ENC=""http://schemas.xmlsoap.org/soap/encoding/"">
            <soapenv:Header/>
               <soapenv:Body>
                  <urn:getPrices soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                     <kullanici xsi:type=""xsd:string"">{_userName}</kullanici>
                     <sifre xsi:type=""xsd:string"">{_password}</sifre>
                     <ulkekodu xsi:type=""xsd:string"">{getPriceModel.UlkeKodu}</ulkekodu>";

            xml += @"<ebat xsi:type=""urn:dims"" SOAP-ENC:arrayType=""urn:dim[]"">";
            if (getPriceModel.Ebatlar != null)
                foreach (var ebat in getPriceModel.Ebatlar)
                {
                    xml += @$"
                    <dim>
                        <en xsi:type=""xsd:string"">{ebat.En}</en>
                        <boy xsi:type=""xsd:decimal"">{ebat.Boy}</boy>
                        <yukseklik xsi:type=""xsd:string"">{ebat.Yukseklik}</yukseklik>
                        <agirlik xsi:type=""xsd:decimal"">{ebat.Agirlik}</agirlik>
                    </dim>";
                }
            xml += "</ebat>" +
                "</urn:getPrices>";

            xml += @$"
               </soapenv:Body>
            </soapenv:Envelope>";
            soapEnvelopeDocument.LoadXml(xml);
            return soapEnvelopeDocument;
        }
        private XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            //        soapEnvelopeDocument.LoadXml(
            //        @"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/"" 
            //           xmlns:xsi=""http://www.w3.org/1999/XMLSchema-instance"" 
            //           xmlns:xsd=""http://www.w3.org/1999/XMLSchema"">
            //    <SOAP-ENV:Body>
            //        <HelloWorld xmlns=""http://tempuri.org/"" 
            //            SOAP-ENV:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
            //            <int1 xsi:type=""xsd:integer"">12</int1>
            //            <int2 xsi:type=""xsd:integer"">32</int2>
            //        </HelloWorld>
            //    </SOAP-ENV:Body>
            //</SOAP-ENV:Envelope>");
            soapEnvelopeDocument.LoadXml(
                @$"<soapenv:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                    xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soapenv=
                    ""http://schemas.xmlsoap.org/soap/envelope/"" 
                    xmlns:urn=""urn:server"">
                    <soapenv:Header/>
                    <soapenv:Body>
                     <urn:getFatura soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
                        <kullanici xsi:type=""xsd:string"">{_userName}</kullanici>
                        <sifre xsi:type=""xsd:string"">{_password}</sifre>
                        <faturaNo xsi:type=""xsd:string"">FT-840301</faturaNo>
                        <faturaBaslangicTarihi xsi:type=""xsd:string"">2007-02-17</faturaBaslangicTarihi>
                        <faturaBitisTarihi xsi:type=""xsd:string"">2007-04-17</faturaBitisTarihi>
                    </urn:getFatura>
                    </soapenv:Body>
                </soapenv:Envelope>");
            return soapEnvelopeDocument;
        }

        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }

    }
}