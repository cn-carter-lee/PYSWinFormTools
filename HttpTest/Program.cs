using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://services.theknot.com/registry/v1/affiliates/994/convertmanualregistryurl?st=WeddingWebsite&apikey=c49043b243ea978bc8667f67b1d7179a";
            RESTApiClient client = new RESTApiClient();
            ManualRegistryUrlConversionRequest req = new ManualRegistryUrlConversionRequest()
                {
                    ManualRegistryURLs = new List<ManualRegistryURL>()
                    {
                        new ManualRegistryURL(){ Position=0, Url="http://www.target.com/RegistryGiftGiverCmd?isPreview=false&status=completePageLink®istryType=BB&isAjax=false&listId=qr5aAYL6-UxlDyY8DhBlTA&clkid=wT20fuXaEX-dyTA0opSPP2wlUkQ0k6VR2QUVWU0&lnm=Online Tracking Link&afid=Thehttp://uw.thebump.com/admin/edit/34393925 Knot%2C Inc. and Subsidiaries&ref=tgt_adv_"},
                        new ManualRegistryURL(){ Position=1, Url="http://www.potterybarn.com/registry/2966556/registry-list.html"} ,                                                                                                     
           new ManualRegistryURL(){ Position=2, Url="http://www.target.com/RegistryGiftGiverCmd?isPreview=false&amp;status=completePageLink&reg;istryType=BB&amp;isAjax=false&amp;listId=qr5aAYL6-UxlDyY8DhBlTA&amp;clkid=wT20fuXaEX-dyTA0opSPP2wlUkQ0k6VR2QUVWU0&amp;lnm=Online Tracking Link&amp;afid=Thehttp://uw.thebump.com/admin/edit/34393925 Knot%2C Inc. and Subsidiaries&amp;ref=tgt_adv_"}
                        
                    }
                };
            client.Post<ManualRegistryUrlConversionRequest, ManualRegistryUrlConversionResult>(url, req);
        }
    }


    public class ManualRegistryUrlConversionResult
    {
        public IList<ConvertedManualRegistry> ConvertedRegistryUrls { get; set; }
    }

    public class ConvertedManualRegistry
    {
        public int Position { get; set; }
        public string OriginalUrl { get; set; }
        public string ConvertedUrl { get; set; }
        public int SuccessCode { get; set; }
    }

    public class ManualRegistryUrlConversionRequest
    {
        public List<ManualRegistryURL> ManualRegistryURLs { get; set; }
    }

    public class ManualRegistryURL
    {
        public int Position { get; set; }
        public string Url { get; set; }
    }
}
