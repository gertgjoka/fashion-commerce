using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class ADDRESSINFO
    {
        public ADDRESSINFO()
        {

        }

        private string _cityName;
        public string CityName
        {
            get
            {
                if (String.IsNullOrEmpty(_cityName))
                {
                    if (D_CITY != null && !String.IsNullOrEmpty(D_CITY.Name))
                    {
                        return D_CITY.Name;
                    }
                    else
                    {
                        return String.Empty;
                    }
                }
                else
                    return _cityName;
            }
            set { _cityName = value; }
        }

        public ADDRESSINFO(ADDRESS Address)
        {
            this.ID = Address.ID;
            this.Name = Address.Name;
            this.Address = Address.Address1;
            this.City = Address.City;
            this.Type = Address.TypeID;
            this.Location = Address.Location;
            this.PostCode = Address.PostCode;
            this.Telephone = Address.Telephone;
        }
    }
}
