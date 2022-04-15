using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace addressbook_web_tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allData;
        private string allNames;

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool Equals(ContactData other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname
                && Lastname == other.Lastname;
        }
        public override int GetHashCode() //для оптимизации сравнения
        {
            return Firstname.GetHashCode() + Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return Firstname + Lastname;
        }
        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Lastname.CompareTo(other.Lastname) != 0)
            {
                return Lastname.CompareTo(other.Lastname);
            }
            else
            {
                return Firstname.CompareTo(other.Firstname);
            }
        }
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Id { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone{ get; set; }

        public string WorkPhone { get; set; }

        public string Email1 { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string AllNames
        {
            get
            {
                if (allNames != null)
                {
                    return allNames;
                }
                else if (Firstname != null && Firstname != "" 
                    && Lastname != null && Lastname != "")
                {
                    return "";
                }
                else if (Firstname != null && Firstname != "")
                {
                    return Lastname;
                }
                else if (Lastname != null && Lastname != "")
                {
                    return Firstname;
                }
                else
                {
                    return Firstname + " " + Lastname;
                }
            }
            set
            {
                allNames = value;
            }
        }
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (CleanUp(Email1) + CleanUp(Email2) + CleanUp(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllData
        {
            get
            {
                if (allData != null)
                {
                    return allData;
                }
                else
                {
                    return (Person() + PhoneNumbers() + Emails()).Trim();
                }
            }
            set
            {
                allData = value;
            }
        }
        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone,"[ -()]", "") + "\r\n";
        }
        private string Person()
        {
            string result = "";

            if (AllNames != null
                && AllNames != "")
            {
                result += AllNames + "\r\n";
            }

            if (Address != null
                 && Address != "")
            {
                result += Address + "\r\n";
            }

            return result;
        }
        private string PhoneNumbers()
        {
            string result = "\r\n";

            if (HomePhone != null
                && HomePhone != "")
            {
                result += "H: " + HomePhone + "\r\n";
            }

            if (MobilePhone != null
                && MobilePhone != "")
            {
                result += "M: " + MobilePhone + "\r\n";
            }

            if (WorkPhone != null
                && WorkPhone != "")
            {
                result += "W: " + WorkPhone + "\r\n";
            }
            return result;
        }
        private string Emails()
        {
            string result = "";

            if (Email1 != null
                && Email1 != "")
            {
                result += Email1 + "\r\n";
            }

            if (Email2 != null
                 && Email2 != "")
            {
                result += Email2 + "\r\n";
            }

            if (Email3 != null
                && Email3 != "")
            {
                result += Email3 + "\r\n";
            }
            return result;
        }
    }
}
