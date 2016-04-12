using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChangeTrackingTest
{
    [TestClass]
    public class TrackingTest
    {
        [TestMethod]
        public void ChangeTrackingMitKlassenLibVomAuthor()
        {
            // Die Klasse MailinAddressCompanyWithChangeTracking ist abgeleitet von der mko.BI.ChangeTracking.BoWithChangeTracking 
            // aus der mk-prg-net.lib Projektmappe (mko.BI) 
            var mkNetPrg = new mko.BI.Bo.Addresses.MailingAddressCompanyWithChangeTracking();

            mkNetPrg.CompanyName = "mko IT";
            mkNetPrg.City = "Stuttgart";
            mkNetPrg.Country = "de";
            mkNetPrg.PostalCode = "70599";
            mkNetPrg.Street = "Hans-Kächele-Str. 11";


            // Alle Änderungen wurden aufgezeichnet, und können erneut auf einem anderen MailingAddress- Objekt 
            // angewendet werden
            var copyAdr = new mko.BI.Bo.Addresses.MailingAddressCompany();
            mkNetPrg.UpdateExternalBo(copyAdr, );

            Assert.AreEqual("mko IT", copyAdr.CompanyName);
            Assert.AreEqual("70599", copyAdr.PostalCode);

            mkNetPrg.CompanyName = "mk-net-prg";
            mkNetPrg.UpdateExternalBo(copyAdr);

            Assert.AreEqual("mk-net-prg", copyAdr.CompanyName);
            Assert.AreEqual("70599", copyAdr.PostalCode);
        }
    }
}
