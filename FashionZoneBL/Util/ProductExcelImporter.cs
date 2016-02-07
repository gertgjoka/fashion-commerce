using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashionZone.DataLayer.Model;

namespace FashionZone.BL.Util
{
    /// <summary>
    /// The input line will be of the form (Name of column [index])
    /// Nr[0]	Kodi[1]  Titulli[2]	 Retail[3] 	 Blen Fzone[4] 	 Shet Fzone[5] 	Pershkrimi[6]	F/M/U[7]	
    /// Kategorite[8]	Lista foto[9]	xS[10]	S[11]  M[12] L[13]  XL[14]  XXL[15]	XXL[16] Sasia[17]
    /// </summary>
    public static class ProductExcelImporter
    {
        public static List<FZExcelProduct> ReadProductExcel(string path)
        {
            var book = new LinqToExcel.ExcelQueryFactory(path);
            var query =
                from row in book.Worksheet()
                let item = new FZExcelProduct
                {
                    Code = row["Kodi"].Cast<string>(),
                    Title = row["Titulli"].Cast<string>(),
                    Retail = row["RETAIL"].Cast<string>(),
                    Buy = row["Blen Fzone"].Cast<string>(),
                    Sell = row["Shet Fzone"].Cast<string>(),
                    Desc = row["Pershkrimi"].Cast<string>(),
                    Sex = row["F/M/U"].Cast<string>(),
                    Category = row["Kategoria"].Cast<string>(),
                    //ImgList = row["Lista foto"].Cast<string>(),
                    SizeXS = row["XS"].Cast<string>(),
                    SizeS = row["S"].Cast<string>(),
                    SizeM = row["M"].Cast<string>(),
                    SizeL = row["L"].Cast<string>(),
                    SizeXL = row["XL"].Cast<string>(),
                    SizeXXL = row["XXL"].Cast<string>(),
                    //OneSize = row["Mase Unike"].Cast<string>(),
                    //Quantity = row["Sasia"].Cast<string>()
                }
                select item;
            return query.ToList();
        }
    }
}
