using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace mko.BI.Test.DB.Kepler
{
    public class HimmelskoerperRepository : mko.BI.Repositories.BoCoBase<global::DB.Kepler.EF60.Himmelskoerper, int> 
    {
        global::DB.Kepler.EF60.KeplerDBEntities ORM = new global::DB.Kepler.EF60.KeplerDBEntities();

        /// <summary>
        ///  Sortierfuktoren
        /// </summary>
        public class SortByMass : mko.BI.Repositories.DefSortOrderCol<global::DB.Kepler.EF60.Himmelskoerper, double>  
        {
            public SortByMass(bool Descending) : base(r => r.Masse_in_kg, Descending) { }
        }
        public class SortByName : mko.BI.Repositories.DefSortOrderCol<global::DB.Kepler.EF60.Himmelskoerper, string> 
        {
            public SortByName(bool Descending) : base(r => r.Name, Descending) { }
        }

        public class SortByZentralkorper : mko.BI.Repositories.DefSortOrderCol<global::DB.Kepler.EF60.Himmelskoerper, string>
        {
            public SortByZentralkorper(bool Descending) : base(r => r.Umlaufbahn.Zentralobjekt.Name, Descending) { }
        }

        /// <summary>
        /// Filterfunktoren
        /// </summary>
        public class NameLikeFlt : global::mko.BI.Repositories.FilterFunctor<global::DB.Kepler.EF60.Himmelskoerper, string>
        {

            public override System.Linq.IQueryable<global::DB.Kepler.EF60.Himmelskoerper> filterImpl(System.Linq.IQueryable<global::DB.Kepler.EF60.Himmelskoerper> srcTab)
            {
                return srcTab.Where(r => r.Name.StartsWith(RValue));
            }
        }

        public class HkTypeFlt : global::mko.BI.Repositories.FilterFunctor<global::DB.Kepler.EF60.Himmelskoerper, int>
        {

            public override System.Linq.IQueryable<global::DB.Kepler.EF60.Himmelskoerper> filterImpl(System.Linq.IQueryable<global::DB.Kepler.EF60.Himmelskoerper> srcTab)
            {
                return srcTab.Where(r => r.HimmelskoerperTyp_ID == RValue);
            }
        }

        public class HatUmlaufbahnFlt : global::mko.BI.Repositories.FilterFunctor<global::DB.Kepler.EF60.Himmelskoerper, bool>
        {

            public override System.Linq.IQueryable<global::DB.Kepler.EF60.Himmelskoerper> filterImpl(System.Linq.IQueryable<global::DB.Kepler.EF60.Himmelskoerper> srcTab)
            {
                return srcTab.Where(r => r.Umlaufbahn != null);
            }
        }

        /// <summary>
        /// Liefert zu einem Himmelskörpertypnamen die ID in der Datenbanktabelle
        /// </summary>
        /// <param name="HkName"></param>
        /// <returns></returns>
        public int GetHimmelkoerperTypID(string HkName)
        {
            return ORM.HimmelskoerperTypenTab.Single(r => r.Name == HkName).ID;
        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        public HimmelskoerperRepository() : base(new SortByMass(false)) { }


        public override IQueryable<global::DB.Kepler.EF60.Himmelskoerper> BoCollection
        {
            get { 
                return ORM.HimmelskoerperTab;
            }
        }

        public override global::DB.Kepler.EF60.Himmelskoerper CreateBo()
        {
            return ORM.HimmelskoerperTab.Create();
        }

        public override Func<global::DB.Kepler.EF60.Himmelskoerper, bool> GetBoIDTest(int id)
        {
            return r => r.ID == id;
        }        

        public override void  AddToCollection(global::DB.Kepler.EF60.Himmelskoerper entity)
        {
            ORM.HimmelskoerperTab.Add(entity);
        }

        public override void RemoveFromCollection(global::DB.Kepler.EF60.Himmelskoerper entity)
        {
            ORM.HimmelskoerperTab.Remove(entity);
        }

        public override void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public override void SubmitChanges()
        {
            ORM.SaveChanges();
        }

        public override global::DB.Kepler.EF60.Himmelskoerper CreateBoAndAddToCollection()
        {
            var hk = ORM.HimmelskoerperTab.Create();
            ORM.HimmelskoerperTab.Add(hk);
            return hk;
        }
    }
}
