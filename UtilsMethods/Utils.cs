using ProductsCrawler.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Net;
using Microsoft.Office.Interop.Excel;

namespace ProductsCrawler.UtilsMethods
{
    public enum PriceStats
    {
        EQUALS,
        UP,
        DOWN
    }

    public static class Utils
    {
        public static List<ProductPresentor> LoadProductsPresentorsFromFile(string productsFilePath)
        {
            var adresses = ColumnFromExcel(productsFilePath, 1);
            var names = ColumnFromExcel(productsFilePath, 2);

            var presentors = adresses.Zip(names, (adress, name) => 
                (new ProductPresentor()
                {
                    InternetAddress = adress,
                    Name = name
                })).ToList();

            //var presentors = File.ReadAllLines(productsFilePath).
            //    Select(p => new ProductPresentor()
            //    {
            //        InternetAddress = p,
            //        Name = GetProductsNameFromAddress(p)
            //    }).ToList();

            return presentors;
        }

        private static string[] ColumnFromExcel(string filename, int columnId)
        {
            Application xlsApp = new Application();

            Workbook wb = xlsApp.Workbooks.Open(filename,
                0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true);
            Sheets sheets = wb.Worksheets;
            Worksheet ws = (Worksheet)sheets.get_Item(1);

            Range wantedColumn = ws.UsedRange.Columns[columnId];
            System.Array myvalues = (System.Array)wantedColumn.Cells.Value;
            string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();
            return strArray;
        }

        private static string GetProductsNameFromAddress(string productAddress)
        {
            try
            {
                int startIndex = productAddress.IndexOf("ip/") + "/ip".Length;
                int endIndex = productAddress.IndexOf("/", startIndex);


                return productAddress.Substring(startIndex, endIndex - startIndex);
            }
            catch (Exception)
            {
                return "Unable to parse product name from address";
            }
        }

        public static void SerializeToXml<T>(T obj, string outputFilePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);

            using (var writer = new StreamWriter(outputFilePath))
            {
                xmlSerializer.Serialize(writer, obj);
            }
        }

        public static T DerializeFromXml<T>(string filePath)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            T returnVal = default(T);

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    returnVal = (T)xmlSerializer.Deserialize(reader);
                }
            }

            return returnVal;
        }

        public static List<ProductStats> LoadProductsStatsFromInternet(List<ProductPresentor> productsAddresses)
        {
            var retVal = new List<ProductStats>();

            using (WebClient client = new WebClient())
            {
                foreach (var currentProduct in productsAddresses)
                {
                    string htmlPage = client.DownloadString(currentProduct.InternetAddress);

                    retVal.Add(ParseHtmlToStatsObject(htmlPage, currentProduct.Name));
                }
            }

            return retVal;
        }

        private static ProductStats ParseHtmlToStatsObject(string htmlPage, string productName)
        {
            return new ProductStats()
            {
                Name = productName,
                Price = ExtractPriceFromHtml(htmlPage),
                DeliveryPrice = ExtractDeliveryPriceFromHtml(htmlPage),
                Quantity = ExtractQuantityFromHtml(htmlPage),
                Score = ExtractScoreFromHtml(htmlPage)
            };
        }

        private static double ExtractScoreFromHtml(string htmlPage)
        {
            string strPattern = @"<span class=""ReviewsHeader-ratingPrefix"">";
            int a = htmlPage.IndexOf(strPattern);
            int b = htmlPage.IndexOf("</span>", a);
            return double.Parse(htmlPage.Substring(a+strPattern.Length,b-a));
        }

        private static string ExtractQuantityFromHtml(string htmlPage)
        {
            if (htmlPage.Contains(@"<p class=""price-oos"">Out of stock</p>"))
            {
                return "Out of stock";
            }
            else
            {
                return "Exists";
            }
        }

        private static string ExtractDeliveryPriceFromHtml(string htmlPage)
        {
            if (htmlPage.Contains(@"<span class=""free-shipping-message"">FREE shipping</span>"))
            {
                return "Free shipping";
            }
            else
            {
                return "Not Free";
            }
        }

        private static double ExtractPriceFromHtml(string htmlPage)
        {
            try 
	        {	        
		        string aStarter = @"title=""$";

                int aStarterIndex = htmlPage.IndexOf(aStarter) + aStarter.Length;
                double aPart = double.Parse(htmlPage.Substring(aStarterIndex, htmlPage.IndexOf('"', aStarterIndex+1) - aStarterIndex));

                //string bStarter = @"<span class=""sup"">";

                //int bStarterIndex = htmlPage.IndexOf(bStarter, aStarterIndex) + bStarter.Length;
                //double bPart = double.Parse("0." + htmlPage.Substring(bStarterIndex, htmlPage.IndexOf("<", bStarterIndex + 1) - bStarterIndex));

                return aPart;
	        }
	        catch (Exception)
	        {
		        return 0;
	        }
        }
    }
}
