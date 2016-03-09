using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.BI.Test
{
    [TestClass]
    public class ChangeTracking
    {
        [TestMethod]
        public void mko_BI_ChangeTracking_Addresses()
        {
            var mkNetPrg = new mko.BI.Bo.Addresses.MailingAddressCompanyWithChangeTracking();


            mkNetPrg.CompanyName = "mko IT";
            mkNetPrg.City = "Stuttgart";
            mkNetPrg.Country = "de";
            mkNetPrg.PostalCode = "70599";
            mkNetPrg.Street = "Hans-Kächele-Str. 11";

            var copyAdr = new mko.BI.Bo.Addresses.MailingAddressCompanyWithChangeTracking();
            mkNetPrg.UpdateExternalBo(copyAdr);

            Assert.AreEqual("mko IT", copyAdr.CompanyName);
            Assert.AreEqual("70599", copyAdr.PostalCode);

            mkNetPrg.CompanyName = "mk-net-prg";
            mkNetPrg.UpdateExternalBo(copyAdr);

            Assert.AreEqual("mk-net-prg", copyAdr.CompanyName);
            Assert.AreEqual("70599", copyAdr.PostalCode);

        }
    }
}
