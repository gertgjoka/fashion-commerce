using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class ADDRESS
    {
        public ADDRESS()
        {

        }

        public ADDRESS(ADDRESSINFO Address)
        {
            this.Name = Address.Name;
            this.Address1 = Address.Address;
            this.City = Address.City;
            this.TypeID = Address.Type;
            this.Location = Address.Location;
            this.PostCode = Address.PostCode;
            this.Telephone = Address.Telephone;
            this.CityName = Address.D_CITY.Name;
        }

        public string AddressSummary
        {
            get {
                StringBuilder temp = new StringBuilder();

                if (!String.IsNullOrEmpty(Name))
                {
                    temp.Append(" " + Name);
                }

                if (!String.IsNullOrEmpty(Address1))
                {
                    temp.Append(" " + Address1);
                }

                if (!String.IsNullOrEmpty(Location))
                {
                    temp.Append(" " + Location);
                }

                if (!String.IsNullOrEmpty(PostCode))
                {
                    temp.Append(" " + PostCode);
                }
                if (D_CITY != null && !String.IsNullOrEmpty(D_CITY.Name))
                {
                    temp.Append(" " + D_CITY.Name);
                }

                return temp.ToString();
            }
        }

        private string _cityName;
        public string CityName
        {
            get {
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

        private string _stateName;
        public string StateName
        {
            get
            {
                if (String.IsNullOrEmpty(_stateName))
                {
                    if (D_CITY != null && D_CITY.D_STATE != null && !String.IsNullOrEmpty(D_CITY.D_STATE.Name))
                    {
                        return D_CITY.D_STATE.Name;
                    }
                    else
                    {
                        return String.Empty;
                    }
                }
                else
                    return _stateName;
            }
            set { _stateName = value; }
        }
    }
}
