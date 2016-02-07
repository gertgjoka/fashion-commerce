using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionZone.BL.Util;
using FashionZone.BL;
using FashionZone.DataLayer.Model;

namespace TestFE
{
    class Program
    {
        static void Main(string[] args)
        {
            var productList = ProductExcelImporter.ReadProductExcel(@"D:\fzone.xlsx");
            foreach (var p in productList.ToList())
            {
                Console.WriteLine("Type: " + p.GetType());
                Console.WriteLine(p.Code);
                Console.WriteLine(p.Title);
                Console.WriteLine(p.Retail);
                Console.WriteLine(p.Buy);
                Console.WriteLine(p.Sell);
                Console.WriteLine(p.Desc);
                Console.WriteLine(p.Sex);
                Console.WriteLine(p.Category);
                Console.WriteLine(p.ImgList);
                Console.WriteLine(p.SizeXS);
                Console.WriteLine(p.SizeS);
                Console.WriteLine(p.SizeM);
                Console.WriteLine(p.SizeL);
                Console.WriteLine(p.SizeXL);
                Console.WriteLine(p.SizeXXL);
                Console.WriteLine(p.OneSize);
                Console.WriteLine(p.Quantity);
            }
            ApplicationContext.Current.Products.ImportProducts(32, @"D:\fzone.xlsx");
            
            Console.Read();
        }
    }
}
