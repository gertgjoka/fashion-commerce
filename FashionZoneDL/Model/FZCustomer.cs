using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FashionZone.DataLayer.Model
{
    [Serializable]
    public partial class CUSTOMER
    {
        public CUSTOMER(object Customer)
        {
            dynamic customer = Customer;
            this.FBId = customer.id;
            this.Email = customer.email;
            this.Name = customer.first_name;
            this.Surname = customer.last_name;
            this.Gender = customer.gender == "male" ? "M" : "F";
            this.Active = true;
            this.Newsletter = true;
            CultureInfo ci = new CultureInfo("en-US");
            DateTime date = DateTime.Today;
            DateTime.TryParse(customer.birthday, ci, DateTimeStyles.None, out date);
            this.BirthDate = date;
        }

        public CUSTOMER()
        {
        }
    }

    [Serializable]
    public partial class SessionCustomer
    {
        public SessionCustomer(CUSTOMER customer)
        {
            Id = customer.ID;
            Name = customer.Name;
            Surname = customer.Surname;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName
        {
            get { return Name + " " + Surname; }
        }
    }

    [Serializable]
    public partial class CUSTOMER_AUDIT
    {

    }
}
