using System.Collections.Generic;
using ApplicationCore.Entities;

namespace ApplicationCore.Tests
{
    internal static class TestData
    {

        private static List<Organization> _organizations;
        private const int _countOrganizations = 10;

        private static List<Country> _countries;
        private const int _countCountries = 10;

        private static List<Business> _businesses;
        private const int _countBusinesses = 10;

        private static List<Family> _families;
        private const int _countFamilies = 10;

        private static List<Offering> _offerings;
        private const int _countOfferings = 10;

        private static List<Department> _departments;
        private const int _countDepartments = 10;

        #region Organizations;

        public static IReadOnlyCollection<Organization> GetAllOrganizations()
        {
            if (_organizations == null)
            {
                InitializeOrganizations();
            }

            return _organizations as IReadOnlyCollection<Organization>;
        }

        public static Organization GetOrganizationById(int id)
        {
            if (_organizations == null)
            {
                InitializeOrganizations();
            }

            return _organizations.Find(o => o.Id == id);
        }

        #endregion;

        #region Countries;

        public static IReadOnlyCollection<Country> GetAllCountries()
        {
            if (_countries == null)
            {
                InitializeCountries();
            }

            return _countries as IReadOnlyCollection<Country>;
        }

        public static Country GetCountyById(int id)
        {
            if (_countries == null)
            {
                InitializeCountries();
            }

            return _countries.Find(c => c.Id == id);
        }

        #endregion;

        #region Businesses;

        public static IReadOnlyCollection<Business> GetAllBusinesses()
        {
            if (_businesses == null)
            {
                InitializeBusinesses();
            }

            return _businesses as IReadOnlyCollection<Business>;
        }

        public static Business GetBusinessById(int id)
        {
            if (_businesses == null)
            {
                InitializeBusinesses();
            }

            return _businesses.Find(b => b.Id == id);
        }

        #endregion;

        #region Families;

        public static IReadOnlyCollection<Family> GetAllFamilies()
        {
            if (_families == null)
            {
                InitializeFamilies();
            }

            return _families as IReadOnlyCollection<Family>;
        }

        public static Family GetFamilyById(int id)
        {
            if (_families == null)
            {
                InitializeFamilies();
            }

            return _families.Find(f => f.Id == id);
        }

        #endregion;

        #region Offerings;

        public static IReadOnlyCollection<Offering> GetAllOfferings()
        {
            if (_offerings == null)
            {
                InitializeOfferings();
            }

            return _offerings as IReadOnlyCollection<Offering>;
        }

        public static Offering GetOfferingById(int id)
        {
            if (_offerings == null)
            {
                InitializeOfferings();
            }

            return _offerings.Find(o => o.Id == id);
        }

        #endregion;

        #region Departments;

        public static IReadOnlyCollection<Department> GetAllDepartments()
        {
            if (_departments == null)
            {
                InitializeDepartments();
            }

            return _departments as IReadOnlyCollection<Department>;
        }

        public static Department GetDepartmentById(int id)
        {
            if (_departments == null)
            {
                InitializeDepartments();
            }

            return _departments.Find(d => d.Id == id);
        }

        #endregion;

        #region Initializing

        private static void InitializeOrganizations()
        {
            _organizations = new List<Organization>();

            for (int i = 0; i < _countOrganizations; i++)
            {
                _organizations.Add(
                    new Organization($"Organization {i + 1}", i + 1, $"Type {i + 1}", $"Owner {i + 1}") { Id = i + 1 }
                );
            }
        }

        private static void InitializeCountries()
        {
            _countries = new List<Country>();

            for (int i = 0; i < _countCountries; i++)
            {
                _countries.Add(
                    new Country($"Country {i + 1}", i + 1) { Id = i + 1 }
                );
            }
        }

        private static void InitializeBusinesses()
        {
            _businesses = new List<Business>();

            for (int i = 0; i < _countBusinesses; i++)
            {
                _businesses.Add(
                    new Business($"Business {i + 1}") { Id = i + 1 }
                );
            }
        }

        private static void InitializeFamilies()
        {
            _families = new List<Family>();

            for (int i = 0; i < _countFamilies; i++)
            {
                _families.Add(
                    new Family($"Family {i + 1}") { Id = i + 1 }
                );
            }
        }

        private static void InitializeOfferings()
        {
            _offerings = new List<Offering>();

            for (int i = 0; i < _countOfferings; i++)
            {
                _offerings.Add(
                    new Offering($"Offering {i + 1}") { Id = i + 1 }
                );
            }
        }

        private static void InitializeDepartments()
        {
            _departments = new List<Department>();

            for (int i = 0; i < _countDepartments; i++)
            {
                _departments.Add(
                    new Department($"Department {i + 1}") { Id = i + 1 }
                );
            }
        }

        #endregion
    }
}
