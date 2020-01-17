using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ProductsCrawler.UtilsMethods;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;

namespace ProductsCrawler.Types
{
    public class ProductStats
    {
        #region Consts

        private const string PRICE_UP_IMAGE = @"Icons/thumbsDown.png";
        private const string PRICE_DOWN_IMAGE = @"Icons/thumbsUp.png";
        private const string PRICE_STANDARD_IMAGE = @"Icons/equals.png";

        #endregion

        [XmlIgnore]
        public Image State { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public double Score { get; set; }
        public string DeliveryPrice { get; set; }
        public string Quantity { get; set; }

        [Browsable(false)]
        [XmlElement("CurrentStateImg")]
        public byte[] ImageBuffer
        {
            get
            {
                byte[] imageBuffer = null;

                if (State != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        State.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        imageBuffer = stream.ToArray();
                    }
                }

                return imageBuffer;
            }
            set
            {
                if (value == null)
                {
                    State = null;
                }
                else
                {
                    using (var stream = new MemoryStream(value))
                    {
                        State = Bitmap.FromStream(stream);
                    }
                }
            }
        }

        public ProductStats()
        {
            State = Image.FromFile(PRICE_STANDARD_IMAGE);
        }

        public void ChangeStateImg(PriceStats priceState)
        {
	        switch (priceState)
	        {
                case PriceStats.EQUALS:
                    {
                        State = Image.FromFile(PRICE_STANDARD_IMAGE);

                        break;
                    }
                case PriceStats.UP:
                    {
                        State = Image.FromFile(PRICE_UP_IMAGE);

                        break;
                    }
                case PriceStats.DOWN:
                    {
                        State = Image.FromFile(PRICE_DOWN_IMAGE);

                        break;
                    }
                default:
                    {
                        break;
                    }
	        }
        }
    }
}
