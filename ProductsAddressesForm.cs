using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductsCrawler.UtilsMethods;
using ProductsCrawler.Types;

namespace ProductsCrawler
{
    public partial class ProductsAddressesForm : Form
    {
        #region Consts

        public const string PRODUCTS_PRESENTORS_FILE_PATH = @"Presentors.xml";

        #endregion

        #region Properties

        public List<ProductPresentor> ProductsPresentors { get; set; }

        #endregion

        public ProductsAddressesForm()
        {
            InitializeComponent();

            ProductsPresentors = new List<ProductPresentor>();
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result
            DialogResult result = openFileDialog.ShowDialog();

            // Test result
            if (result == DialogResult.OK) 
            {
                // Parse products from user file
                ProductsPresentors = Utils.LoadProductsPresentorsFromFile(openFileDialog.FileName);

                // Saves locally
                Utils.SerializeToXml(ProductsPresentors, PRODUCTS_PRESENTORS_FILE_PATH);

                // Sets grid view data
                dtgPoductsAdresses.DataSource = ProductsPresentors;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (ProductsPresentors.Count == 0) 
            {
                MessageBox.Show("There is no addresses to analyze!");

                return;
            }

            this.Hide();
            var statsForm = new ProductsStatsForm(ProductsPresentors, false);
            statsForm.Show();
        }

        private void ProductsAddresses_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
