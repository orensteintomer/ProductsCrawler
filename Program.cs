using ProductsCrawler.UtilsMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductsCrawler.Types;

namespace ProductsCrawler
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (File.Exists(ProductsAddressesForm.PRODUCTS_PRESENTORS_FILE_PATH))
            {
                var adresses = 
                    Utils.DerializeFromXml<List<ProductPresentor>>(ProductsAddressesForm.PRODUCTS_PRESENTORS_FILE_PATH);

                Application.Run(new ProductsStatsForm(adresses, true));
            }
            else
            {
                Application.Run(new ProductsAddressesForm());
            }
        }
    }
}
