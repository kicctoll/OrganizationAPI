using System;
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

        #region ManageOrganizations;

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


        public static IReadOnlyCollection<Country> GetAllCountries()
        {
            if (_countries == null)
            {
                InitializeCountries();
            }

            return _countries as IReadOnlyCollection<Country>;
        }

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
                    new Country($"Country {i + 1}", i + 1)
                );
            }
        }

        #endregion
    }
}
