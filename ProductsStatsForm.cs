using ProductsCrawler.UtilsMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductsCrawler.Types;
using System.IO;

namespace ProductsCrawler
{
    public partial class ProductsStatsForm : Form
    {
        #region Consts

        public const string PRODUCTS_STATS_FILE_PATH = @"Stats.xml";

        #endregion

        #region Properties

        public List<ProductPresentor> ProductsAddresses { get; set; }
        public List<ProductStats> ProductsStats { get; set; }

        #endregion

        public ProductsStatsForm(List<ProductPresentor> productsAddresses, bool isOldFiles)
        {
            InitializeComponent();
            ProductsAddresses = productsAddresses;

            if (File.Exists(PRODUCTS_STATS_FILE_PATH) && isOldFiles)
            {
                ProductsStats =
                    Utils.DerializeFromXml<List<ProductStats>>(PRODUCTS_STATS_FILE_PATH);

                // Sets grid view data
                dtgPoductsStats.DataSource = ProductsStats;
            }
            else
            {
                LoadStats();
            }
        }

        private void LoadStats()
        {
            // Parse products stats from internet
            ProductsStats = Utils.LoadProductsStatsFromInternet(ProductsAddresses);

            // Saves locally
            Utils.SerializeToXml(ProductsStats, PRODUCTS_STATS_FILE_PATH);

            // Sets grid view data
            dtgPoductsStats.DataSource = ProductsStats;
        }

        private void ProductsStats_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLoadNewFile_Click(object sender, EventArgs e)
        {
            this.Hide();
            var adressesForm = new ProductsAddressesForm();
            adressesForm.Show();
        }

        private void btnRefreshStats_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            RefreshStats();
            this.Cursor = Cursors.Default;
        }

        private void RefreshStats()
        {
            var oldStats = ProductsStats.ToList();

            // Loads new stats
            LoadStats();

            // Sets new state
            SetNewStatState(oldStats, ProductsStats);
        }

        private void SetNewStatState(List<ProductStats> oldStats, List<ProductStats> currentProductsStats)
        {
            for (int index = 0; index < oldStats.Count; index++)
            {
                if (currentProductsStats[index].Price > oldStats[index].Price)
                {
                    currentProductsStats[index].ChangeStateImg(PriceStats.UP);
                }
                else if (currentProductsStats[index].Price < oldStats[index].Price)
                {
                    currentProductsStats[index].ChangeStateImg(PriceStats.DOWN);
                }
                else
                {
                    currentProductsStats[index].ChangeStateImg(PriceStats.EQUALS);
                }
            }
        }


        private void customSortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            int a = int.Parse(e.CellValue1.ToString()), b = int.Parse(e.CellValue2.ToString());

            // If the cell value is already an integer, just cast it instead of parsing

            e.SortResult = a.CompareTo(b);

            e.Handled = true;
        }
    }
}
