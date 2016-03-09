using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mko.BI.Test
{
    [TestClass]
    public class IntervalTests
    {
        [TestMethod]
        public void mko_BI_Interval_int_Test()
        {
            // Leeres Intervall
            var iEmpty = new mko.BI.Bo.Interval<int>();

            // Achtung: Intervall nicht leer, da genau ein Element (0) enthalten
            Assert.IsFalse(iEmpty.Empty);

            iEmpty.End = 10;

            Assert.IsFalse(iEmpty.Empty);

            iEmpty.End = -10;

            Assert.IsTrue(iEmpty.Empty);

            // Einelementiges Intervall
            var i1 = new mko.BI.Bo.Interval<int>(1, 1);

            Assert.IsFalse(i1.Empty);

            // Intervall von 0-10
            var i0_10 = new mko.BI.Bo.Interval<int>(0, 10);

            Assert.IsFalse(i0_10.Empty);
            Assert.IsTrue(i0_10.Contains(0));
            Assert.IsTrue(i0_10.Contains(5));
            Assert.IsTrue(i0_10.Contains(10));

            // Intervalle schneiden
            var iInter1 = i0_10.Intersect(i1);

            Assert.IsFalse(iInter1.Empty);
            Assert.AreEqual(1, iInter1.Begin);
            Assert.AreEqual(1, iInter1.End);

            // Intervalle schneiden
            var iInter2 = i0_10.Intersect(new mko.BI.Bo.Interval<int>(3, 6));

            Assert.IsFalse(iInter2.Empty);
            Assert.AreEqual(3, iInter2.Begin);
            Assert.AreEqual(6, iInter2.End);

            // Intervalle schneiden
            var iInter3 = i0_10.Intersect(new mko.BI.Bo.Interval<int>(-1, 6));

            Assert.IsFalse(iInter3.Empty);
            Assert.AreEqual(0, iInter3.Begin);
            Assert.AreEqual(6, iInter3.End);

            // Intervalle schneiden
            var iInter4 = i0_10.Intersect(new mko.BI.Bo.Interval<int>(3, 12));

            Assert.IsFalse(iInter4.Empty);
            Assert.AreEqual(3, iInter4.Begin);
            Assert.AreEqual(10, iInter4.End);


            // Intervalle schneiden
            var iInter5 = i0_10.Intersect(new mko.BI.Bo.Interval<int>(11, 12));

            Assert.IsTrue(iInter5.Empty);
            Assert.AreEqual(11, iInter5.Begin);
            Assert.AreEqual(10, iInter5.End);

            // Intervalle vereinigen
            var sum = i0_10.Union(new BI.Bo.Interval<int>(11, 12));

            Assert.IsFalse(sum.Empty);
            Assert.AreEqual(0, sum.Begin);
            Assert.AreEqual(12, sum.End);

            var sum2 = i0_10.Union(new BI.Bo.Interval<int>(-12, -1));

            Assert.IsFalse(sum2.Empty);
            Assert.AreEqual(-12, sum2.Begin);
            Assert.AreEqual(10, sum2.End);

            var sum3 = i0_10.Union(new BI.Bo.Interval<int>(-12, 5));

            Assert.IsFalse(sum3.Empty);
            Assert.AreEqual(-12, sum3.Begin);
            Assert.AreEqual(10, sum3.End);


            // 10.7.2015: Interval als Struct - neues Verhalten beim Kopieren testen
            var i0_10copy = i0_10;

            i0_10copy.Begin = 1;
            Assert.AreNotEqual(i0_10.Begin, i0_10copy.Begin);



        }

        [TestMethod]
        public void mko_BI_Interval_DateTime_Test()
        {
            // Leeres Intervall
            var iEmpty = new mko.BI.Bo.Interval<DateTime>();

            Assert.IsFalse(iEmpty.Empty);

            iEmpty.End = DateTime.Now;

            Assert.IsFalse(iEmpty.Empty);

            iEmpty.Begin = DateTime.Now.AddSeconds(1);

            Assert.IsTrue(iEmpty.Empty);

            // Einelementiges Intervall
            var Now = DateTime.Now;
            var i1 = new mko.BI.Bo.Interval<DateTime>(Now, Now);

            Assert.IsFalse(i1.Empty);

            // Intervall von 0-10
            var i0_10 = new mko.BI.Bo.Interval<DateTime>(Now, Now.AddHours(10));

            Assert.IsFalse(i0_10.Empty);
            Assert.IsTrue(i0_10.Contains(Now));
            Assert.IsTrue(i0_10.Contains(Now.AddHours(5)));
            Assert.IsTrue(i0_10.Contains(Now.AddHours(10)));

            // Intervalle schneiden
            var iInter1 = i0_10.Intersect(i1);

            Assert.IsFalse(iInter1.Empty);
            Assert.AreEqual(Now, iInter1.Begin);
            Assert.AreEqual(Now, iInter1.End);

            // Intervalle schneiden
            var iInter2 = i0_10.Intersect(new mko.BI.Bo.Interval<DateTime>(Now.AddHours(3), Now.AddHours(6)));

            Assert.IsFalse(iInter2.Empty);
            Assert.AreEqual(Now.AddHours(3), iInter2.Begin);
            Assert.AreEqual(Now.AddHours(6), iInter2.End);

            // Intervalle schneiden
            var iInter3 = i0_10.Intersect(new mko.BI.Bo.Interval<DateTime>(Now.AddHours(-1), Now.AddHours(6)));

            Assert.IsFalse(iInter3.Empty);
            Assert.AreEqual(Now.AddHours(0), iInter3.Begin);
            Assert.AreEqual(Now.AddHours(6), iInter3.End);

            // Intervalle schneiden
            var iInter4 = i0_10.Intersect(new mko.BI.Bo.Interval<DateTime>(Now.AddHours(3), Now.AddHours(12)));

            Assert.IsFalse(iInter4.Empty);
            Assert.AreEqual(Now.AddHours(3), iInter4.Begin);
            Assert.AreEqual(Now.AddHours(10), iInter4.End);


            // Intervalle schneiden
            var iInter5 = i0_10.Intersect(new mko.BI.Bo.Interval<DateTime>(Now.AddHours(11), Now.AddHours(12)));

            Assert.IsTrue(iInter5.Empty);
            Assert.AreEqual(Now.AddHours(11), iInter5.Begin);
            Assert.AreEqual(Now.AddHours(10), iInter5.End);

            // var Intervalle vereinigen
            var sum = i0_10.Union(new BI.Bo.Interval<DateTime>(Now.AddHours(-24), Now));

            Assert.IsFalse(sum.Empty);
            Assert.AreEqual(Now.AddHours(-24), sum.Begin);
            Assert.AreEqual(Now.AddHours(10), sum.End);


        }
    }
}
