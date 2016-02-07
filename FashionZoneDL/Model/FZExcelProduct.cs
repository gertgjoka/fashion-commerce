using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public class FZExcelProduct
    {
        //constructor to set the properties omitted
        public string Code { get; set; }
        public string Title { get; set; }
        public string Retail { get; set; }
        public string Buy { get; set; }
        public string Sell { get; set; }
        public string Desc { get; set; }
        public string Sex { get; set; }
        public string Category { get; set; }
        public string ImgList { get; set; }
        public string SizeXS { get; set; }
        public string SizeS { get; set; }
        public string SizeM { get; set; }
        public string SizeL { get; set; }
        public string SizeXL { get; set; }
        public string SizeXXL { get; set; }
        public string OneSize { get; set; }
        public string Quantity { get; set; }
    }
}
