using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

public static class DataProvider {

    static HttpSessionState Session { get { return HttpContext.Current.Session; } }

    static DemoDataObject DemoData {
        get {
            const string key = "FB1EB35F-86F5-4FFE-BB23-CBAAF1514C49";
            if (Session[key] == null) {
                var obj = new DemoDataObject();
                obj.FillObj();
                Session[key] = obj;
            }
            return (DemoDataObject)Session[key];
        }
    }

    public static IEnumerable GetCustomers() {
        return DemoData.Customers;
    }

    public static void InsertCustomer(string customerName, int countryID, int cityID) {
        var c = new Customer() {
            CustomerID = DemoData.Customers.Count,
            CustomerName = customerName,
            CountryID = countryID,
            CityID = cityID
        };

        DemoData.Customers.Add(c);
    }

    public static void UpdateCustomer(int customerID, string customerName, int countryID, int cityID) {
        var c = DemoData.Customers.First(i => i.CustomerID == customerID);

        c.CustomerName = customerName;
        c.CountryID = countryID;
        c.CityID = cityID;
    }

    public static IEnumerable GetCountries() {
        return DemoData.Countries;
    }

    public static IEnumerable GetCities() {
        return DemoData.Cities;
    }

    public static IEnumerable GetCities(int countryID) {
        return from c in DemoData.Cities
               where c.CountryID == countryID
               select c;
    }
}

public class DemoDataObject {
    public List<Customer> Customers { get; set; }
    public List<Country> Countries { get; set; }
    public List<City> Cities { get; set; }

    public void FillObj() {
        Customers = new List<Customer>();
        Countries = new List<Country>();
        Cities = new List<City>();

        var uk = CreateCountry("UK");
        var usa = CreateCountry("USA");

        CreateCustomer("Jacob", CreateCity("Brighton", uk.CountryID));
        CreateCustomer("Michael", CreateCity("Glasgow", uk.CountryID));
        CreateCustomer("Emily", CreateCity("London", uk.CountryID));
        CreateCustomer("Joshua", CreateCity("Bath", uk.CountryID));
        CreateCustomer("Emma", CreateCity("Manchester", uk.CountryID));
        CreateCustomer("Madison", CreateCity("Wells", uk.CountryID));
        CreateCustomer("Matthew", CreateCity("York", uk.CountryID));
        CreateCustomer("Olivia", CreateCity("Dallas", usa.CountryID));
        CreateCustomer("Ethan", CreateCity("Las Vegas", usa.CountryID));
        CreateCustomer("Hannah", CreateCity("Los Angeles", usa.CountryID));
        CreateCustomer("Abigail", CreateCity("New York City", usa.CountryID));
        CreateCustomer("Isabella", CreateCity("San Francisco", usa.CountryID));
        CreateCustomer("Andrew", CreateCity("Washington D.C.", usa.CountryID));
        CreateCustomer("Daniel", CreateCity("Miami", usa.CountryID));

        CreateCity("Cardiff", uk.CountryID);
        CreateCity("Liverpool", uk.CountryID);
        CreateCity("Oxford", uk.CountryID);
        CreateCity("Atlanta", usa.CountryID);
        CreateCity("Houston", usa.CountryID);
        CreateCity("Phoenix", usa.CountryID);
    }

    Customer CreateCustomer(string name, City city) {
        var c = new Customer() {
            CustomerName = name,
            CityID = city.CityID,
            CountryID = city.CountryID,
            CustomerID = Customers.Count
        };

        Customers.Add(c);

        return c;
    }

    Country CreateCountry(string name) {
        var c = new Country() { CountryName = name };
        c.CountryID = Countries.Count;
        Countries.Add(c);
        return c;
    }

    City CreateCity(string name, int countryID) {
        var c = new City() {
            CityName = name,
            CountryID = countryID,
            CityID = Cities.Count
        };

        Cities.Add(c);

        return c;
    }
}

public class Customer {
    public int CustomerID { get; set; }
    public string CustomerName { get; set; }
    public int CountryID { get; set; }
    public int CityID { get; set; }
}

public class Country {
    public int CountryID { get; set; }
    public string CountryName { get; set; }
}

public class City {
    public int CityID { get; set; }
    public string CityName { get; set; }
    public int CountryID { get; set; }
}