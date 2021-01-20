using JDP_Airlines.Models;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JDP_Airlines
{
    public static class DataProvider
    {
        
        #region Aerdrom
        public static async Task<Aerodrom> getAerdrom(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Aerodrom aerodrom;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Aerdrom ) where a.Id='{id}' return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                aerodrom= JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (aerodrom == null)
                aerodrom = new Aerodrom();
            return aerodrom;
        }
        public static async Task<Aerodrom> getAerdromOznaka(string oznaka)
        {
            IAsyncSession session = SessionManager.GetSession();
            Aerodrom aerodrom;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Aerdrom ) where a.Oznaka='{oznaka}' return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                aerodrom = JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
           
            return aerodrom;
        }
        public static async Task<Aerodrom> updateAerdrom(Aerodrom aerodrom)
        {
            IAsyncSession session = SessionManager.GetSession();
            
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Aerdrom ) where a.Id='{aerodrom.Id}' " +
                    $"SET a.Oznaka='{aerodrom.Oznaka}',a.Grad='{aerodrom.Grad}',a.Naziv='{aerodrom.Naziv}'," +
                    $"a.Drzava='{aerodrom.Drzava}',a.BrojPisti='{aerodrom.BrojPisti}'   return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                aerodrom = JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            return aerodrom;
        }
        //MERGE JE ISTO KAO CREATE ALI NE DOPUSTA DUPLIKATE
        public static async Task createAerdrom(Aerodrom aerdrom)
        {
            IAsyncSession session = SessionManager.GetSession();
            
            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(a:Aerdrom {{Oznaka:'{aerdrom.Oznaka}'," +
                    $"Grad:'{aerdrom.Grad}',Drzava:'{aerdrom.Drzava}',Naziv:'{aerdrom.Naziv}'," +
                    $"BrojPisti:'{aerdrom.BrojPisti}',Id:'{Guid.NewGuid()}'}})");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task deleteAerdrom(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Aerodrom aerodrom = new Aerodrom();
            try
            {
                IResultCursor cursor = await session.RunAsync($"Match(a:Aerdrom) where a.Id='{id}' detach delete a");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<List<Aerodrom>> getSveAerdrome()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Aerodrom> listaAerdroma = new List<Aerodrom>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Aerdrom) RETURN a");
               var lista= await cursor.ToListAsync(r=>r["a"].As<INode>().Properties);
                
                lista.ForEach(record =>
                {

                    Aerodrom aerodrom = JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(record));
                    listaAerdroma.Add(aerodrom);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaAerdroma;

        }

        #endregion
        #region SaradnickaKompanija
        public static async Task<SaradnickaKompanija> getSaradnickaKompanija(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            SaradnickaKompanija saradnickaKompanija;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(s:SaradnickaKompanija) where s.Id='{id}' return s");
                var lista = await cursor.ToListAsync(r => r["s"].As<INode>().Properties);
                saradnickaKompanija = JsonConvert.DeserializeObject<SaradnickaKompanija>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (saradnickaKompanija == null)
                saradnickaKompanija = new SaradnickaKompanija();
            return saradnickaKompanija;
        }
        public static async Task<SaradnickaKompanija> updateSaradnickaKompanija(SaradnickaKompanija saradnickaKompanija)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(s:SaradnickaKompanija ) where s.Id='{saradnickaKompanija.Id}' " +
                    $"SET s.TipSaradnje='{saradnickaKompanija.TipSaradnje}',s.CenaUsluge='{saradnickaKompanija.CenaUsluge}',s.Naziv='{saradnickaKompanija.Naziv}'" 
                    +"return s");
                var lista = await cursor.ToListAsync(r => r["s"].As<INode>().Properties);
                saradnickaKompanija = JsonConvert.DeserializeObject<SaradnickaKompanija>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            return saradnickaKompanija;
        }
        public static async Task createSaradnickaKompanija(SaradnickaKompanija saradnickaKompanija)
        {
            IAsyncSession session = SessionManager.GetSession();
            
            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(s:SaradnickaKompanija {{TipSaradnje:'{saradnickaKompanija.TipSaradnje}'," +
                    $"CenaUsluge:'{saradnickaKompanija.CenaUsluge}',Naziv:'{saradnickaKompanija.Naziv}',Id:'{Guid.NewGuid()}'}})");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task deleteSaradnickaKompanija(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            
            try
            {
                IResultCursor cursor = await session.RunAsync($"Match(s:SaradnickaKompanija) where s.Id='{id}' detach delete s");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<List<SaradnickaKompanija>> getSveSaradnickeKompanije()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<SaradnickaKompanija> listaSaradnickaKompanija = new List<SaradnickaKompanija>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(s:SaradnickaKompanija) RETURN s");
                var lista = await cursor.ToListAsync(r => r["s"].As<INode>().Properties);

                lista.ForEach(record =>
                {

                    SaradnickaKompanija saradnickaKompanija = JsonConvert.DeserializeObject<SaradnickaKompanija>(JsonConvert.SerializeObject(record));
                    listaSaradnickaKompanija.Add(saradnickaKompanija);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaSaradnickaKompanija;

        }
        #endregion
        #region Stjuardesa
        public static async Task<Stjuardesa> getStjuardesa(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Stjuardesa stjuardesa;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(s:Stjuardesa) where s.Id='{id}' return s");
                var lista = await cursor.ToListAsync(r => r["s"].As<INode>().Properties);
                stjuardesa = JsonConvert.DeserializeObject<Stjuardesa>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (stjuardesa == null)
                stjuardesa = new Stjuardesa();
            return stjuardesa;
        }
        public static async Task<Stjuardesa> updateStjuardesa(Stjuardesa stjuardesa)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(s:Stjuardesa ) where s.Id='{stjuardesa.Id}' " +
                    $"SET s.Ime='{stjuardesa.Ime}',s.Prezime='{stjuardesa.Prezime}',s.GodineStaza='{stjuardesa.GodineStaza}'"
                    + "return s");
                var lista = await cursor.ToListAsync(r => r["s"].As<INode>().Properties);
                stjuardesa = JsonConvert.DeserializeObject<Stjuardesa>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            return stjuardesa;
        }
        public static async Task<Stjuardesa> createStjuardesa(Stjuardesa stjuardesa)
        {
            IAsyncSession session = SessionManager.GetSession();
            Stjuardesa st;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(s:Stjuardesa {{Ime:'{stjuardesa.Ime}'," +
                    $"Prezime:'{stjuardesa.Prezime}',GodineStaza:'{stjuardesa.GodineStaza}',Id:'{Guid.NewGuid()}'}}) RETURN s");
                var lista = await cursor.ToListAsync(r => r["s"].As<INode>().Properties);
                st = JsonConvert.DeserializeObject<Stjuardesa>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (st == null)
                st = new Stjuardesa();
            return st;
        }
        public static async Task deleteStjuardesa(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(k:Korisnik)-[r:KORISNIK_STJUARDESA]->(s) where s.Id='{id}' detach delete k");
                cursor = await session.RunAsync($"Match(s:Stjuardesa) where s.Id='{id}' detach delete s");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<List<Stjuardesa>> getSveStjuardese()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Stjuardesa> listaStjuardesa = new List<Stjuardesa>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(s:Stjuardesa) RETURN s");
                var lista = await cursor.ToListAsync(r => r["s"].As<INode>().Properties);

                lista.ForEach(record =>
                {

                    Stjuardesa stjuardesa = JsonConvert.DeserializeObject<Stjuardesa>(JsonConvert.SerializeObject(record));
                    listaStjuardesa.Add(stjuardesa);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaStjuardesa;

        }
        public static async Task createStjuardesaKorisnik(Guid idStjuardese, Guid idKorisnika)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (a:Korisnik),(b:Stjuardesa) WHERE a.Id = '{idKorisnika}' " +
                    $"AND b.Id = '{idStjuardese}' CREATE(a) -[r: KORISNIK_STJUARDESA]->(b) RETURN type(r)");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<Stjuardesa> getStjuardesaKorisnik(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Stjuardesa stjuardesa;
            var records = new List<IRecord>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"match (k:Korisnik)-[:KORISNIK_STJUARDESA]-(s:Stjuardesa)-[:RADI_NA]->(l:Let)-[:VRSTA_LETA]-(a:Avion)" +
                                                              $" where k.Id='{id}'" +
                                                              $"match(aer: Aerdrom) -[:LETI_SA]-(l)" +
                                                              $"match(aer2: Aerdrom) -[:LETI_NA] - (l)" +
                                                              $"return s,l,a,aer,aer2");
                while (await cursor.FetchAsync())
                {
                    records.Add(cursor.Current);
                }
                if (records.Count == 0)
                    return new Stjuardesa();
                var lista = records[0]["s"].As<INode>().Properties;
                stjuardesa = JsonConvert.DeserializeObject<Stjuardesa>(JsonConvert.SerializeObject(lista));
                stjuardesa.Letovi = new List<Let>();

                foreach (var record in records)
                {
                    lista = record["l"].As<INode>().Properties;
                    var let = JsonConvert.DeserializeObject<Let>(JsonConvert.SerializeObject(lista));



                    lista = record["a"].As<INode>().Properties;
                    var avion = JsonConvert.DeserializeObject<Avion>(JsonConvert.SerializeObject(lista));



                    lista = record["aer"].As<INode>().Properties;
                    var aer = JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(lista));



                    lista = record["aer2"].As<INode>().Properties;
                    var aer2 = JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(lista));



                    let.Avion = avion;
                    let.LetiSa = aer;
                    let.LetiNa = aer2;



                    let.SaradnickeKompanije = new List<SaradnickaKompanija>();
                    var listaSaradnji = await getLetSaradnickeKompanije(let.ID);
                    foreach (var saradnja in listaSaradnji)
                    {
                        let.SaradnickeKompanije.Add(await getSaradnickaKompanija(saradnja));
                    }

                    let.Stjuardese = new List<Stjuardesa>();
                    var listaStjuardesa = await getLetStjuardese(let.ID);
                    foreach (var st in listaStjuardesa)
                    {
                        let.Stjuardese.Add(await getStjuardesa(st));
                    }
                    stjuardesa.Letovi.Add(let);
                }
            }
            finally
            {
                await session.CloseAsync();
            }
            if (stjuardesa == null)
                stjuardesa = new Stjuardesa();
            return stjuardesa;
        }
        #endregion
        #region Avion
        public static async Task<Avion> getAvion(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Avion avion;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Avion) where a.ID='{id}' return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                avion = JsonConvert.DeserializeObject<Avion>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (avion == null)
                avion = new Avion();
            return avion;
        }
        public static async Task<List<Avion>> getSveAvione()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Avion> listaAviona = new List<Avion>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Avion) RETURN a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);

                lista.ForEach(record =>
                {

                    Avion aerodrom = JsonConvert.DeserializeObject<Avion>(JsonConvert.SerializeObject(record));
                    listaAviona.Add(aerodrom);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaAviona;
        }
        public static async Task createAvion(Avion avion)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(a:Avion {{Proizvodjac:'{avion.Proizvodjac}'," +
                    $"Putnicki:'{avion.Putnicki}',Nosivost:'{avion.Nosivost}',Kapacitet:'{avion.Kapacitet}'," +
                    $"Sirina:'{avion.Sirina}',Duzina:'{avion.Duzina}',Visina:'{avion.Visina}'," +
                    $"GodinaProizvodnje:'{avion.GodinaProizvodnje}',ID:'{Guid.NewGuid()}'}})");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<Avion> updateAvion(Avion avion)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Avion ) where a.ID='{avion.ID}' " +
                    $"SET a.Proizvodjac='{avion.Proizvodjac}',a.Putnicki='{avion.Putnicki}'," +
                    $"a.Nosivost='{avion.Nosivost}',a.Kapacitet='{avion.Kapacitet}'," +
                    $"a.Sirina='{avion.Sirina}',a.Duzina='{avion.Duzina}',a.Visina='{avion.Visina}'," +
                    $"a.GodinaProizvodnje='{avion.GodinaProizvodnje}'   return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                avion = JsonConvert.DeserializeObject<Avion>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            return avion;
        }
        public static async Task deleteAvion(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"Match(a:Avion) where a.ID='{id}' detach delete a");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        #endregion
        #region RobaKargo
        public static async Task<RobaKargo> getRobaKargo(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            RobaKargo kargo;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:RobaKargo) where a.ID='{id}' return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                kargo = JsonConvert.DeserializeObject<RobaKargo>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (kargo == null)
                kargo = new RobaKargo();
            return kargo;
        }
        public static async Task<List<RobaKargo>> getSveRobaKargo()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<RobaKargo> listaKargo = new List<RobaKargo>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:RobaKargo) RETURN a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);

                lista.ForEach(record =>
                {

                    RobaKargo kargo = JsonConvert.DeserializeObject<RobaKargo>(JsonConvert.SerializeObject(record));
                    listaKargo.Add(kargo);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaKargo;
        }
        public static async Task createRobaKargo(RobaKargo kargo)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(a:RobaKargo {{TipRoba:'{kargo.TipRoba}'," +
                    $"Tezina:'{kargo.Tezina}',Sirina:'{kargo.Sirina}',Duzina:'{kargo.Duzina}'," +
                    $"Visina:'{kargo.Visina}',ID:'{Guid.NewGuid()}'}})");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public async static Task SeedRobaKargo()
        {
            Random rand = new Random();
            List<RobaKargo> roba = new List<RobaKargo>()
            { 
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Humunatirna pomoc" } ,
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Obician prtljag" } ,
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Vojna tajna" } ,
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Lekovi i respiratori" } ,
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Zasticene zivotinje" } ,
            new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Hrana" } ,
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Areholoske iskopine" } ,
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Maticne celijije" } ,
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Nije dostupno" } ,
             new RobaKargo{Duzina=rand.Next(10,200),ID=Guid.NewGuid(),Sirina=rand.Next(1,10),Visina=rand.Next(1,20),Tezina=rand.Next(1,50),TipRoba="Sredstva za dezinfekciju" } ,
            };
            foreach (var r in roba)
            {
                await createRobaKargo(r);
            }
        }
        public static async Task<RobaKargo> updateRobaKargo(RobaKargo kargo)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:RobaKargo) where a.ID='{kargo.ID}' " +
                    $"SET a.TipRoba='{kargo.TipRoba}',a.Tezina='{kargo.Tezina}'," +
                    $"a.Sirina='{kargo.Sirina}',a.Duzina='{kargo.Duzina}'," +
                    $"a.Visina='{kargo.Visina}' return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                kargo = JsonConvert.DeserializeObject<RobaKargo>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            return kargo;
        }
        public static async Task deleteRobaKargo(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"Match(a:RobaKargo) where a.ID='{id}' detach delete a");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task createLetRobaKargo(Guid idLeta, Guid robaId)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (l:Let),(p:RobaKargo) WHERE l.ID = '{idLeta}' " +
                    $"AND p.ID = '{robaId}' MERGE(l)-[r:PREVOZI]->(p)  RETURN type(r)");
            }
            finally
            {
                await session.CloseAsync();
            }

        }
        #endregion
        #region Putnik
        public static async Task<Putnik> getPutnik(string brPasosa)
        {
            IAsyncSession session = SessionManager.GetSession();
            Putnik putnik;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Putnik) where a.BrojPasosa='{brPasosa}' return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                putnik = JsonConvert.DeserializeObject<Putnik>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (putnik == null)
                putnik = new Putnik();
            return putnik;
        }
        public static async Task<List<Putnik>> getSvePutnike()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Putnik> listaPutnika = new List<Putnik>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Putnik) RETURN a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);

                lista.ForEach(record =>
                {

                    Putnik putnik = JsonConvert.DeserializeObject<Putnik>(JsonConvert.SerializeObject(record));
                    listaPutnika.Add(putnik);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaPutnika;
        }
        public static async Task createPutnik(Putnik putnik)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(a:Putnik {{Email:'{putnik.Email}'," +
                    $"Ime:'{putnik.Ime}',Prezime:'{putnik.Prezime}',IdLeta_IdPrtljaga:'{putnik.IdLeta_IdPrtljaga}'," +
                    $"BrojBodova:'{putnik.BrojBodova}',BrojPasosa:'{putnik.BrojPasosa}'}})");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<Putnik> updatePutnik(Putnik putnik)
        {
            IAsyncSession session = SessionManager.GetSession();

            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(a:Putnik) where a.BrojPasosa='{putnik.BrojPasosa}' " +
                    $"SET a.Email='{putnik.Email}',a.Ime='{putnik.Ime}'," +
                    $"a.Prezime='{putnik.Prezime}',a.IdLeta_IdPrtljaga='{putnik.IdLeta_IdPrtljaga}'," +
                    $"a.BrojBodova='{putnik.BrojBodova}' return a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                putnik = JsonConvert.DeserializeObject<Putnik>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            return putnik;
        }
        public static async Task SeedPutnici()
        {
            List<Putnik> putnici = new List<Putnik>()
            {
                new Putnik{BrojBodova=20,BrojPasosa="009423531631534",Ime="Petar",Prezime="Petrovic",Email="per@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="008532546578636",Ime="Milica",Prezime="Velickovic",Email="veliko@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="957246843135234",Ime="Ljubomir",Prezime="Jaksic",Email="546love@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="078974746362424",Ime="Dejan",Prezime="Nenedovic",Email="footbal@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="078074346356795",Ime="Milos",Prezime="Milic",Email="basektball@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="864523584689654",Ime="Lazar",Prezime="Slavkovic",Email="lazars@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="436758578543563",Ime="Nenad",Prezime="Milovic",Email="bihjack@hotmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="465787969624235",Ime="Aleksandar",Prezime="Ilic",Email="strengthlevel@hotmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="807082352525245",Ime="Katarina",Prezime="Bojovic",Email="kacasw@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="984234254679634",Ime="Ana",Prezime="Jovanovim",Email="beautyandbeast@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="090535345234896",Ime="Milena",Prezime="Milosevic",Email="fd1000udaraca@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="804463423421987",Ime="Emilija",Prezime="Nesic",Email="moje_ime@gmail.com"},
                new Putnik{BrojBodova=20,BrojPasosa="323676875031979",Ime="Sladjan",Prezime="Jaksic",Email="neo4jgraph@gmail.com"},

            };
            foreach (var putnik in putnici)
            {
                await createPutnik(putnik);
            }
        }
        public static async Task deletePutnik(string brPasosa)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"Match(a:Putnik) where a.BrojPasosa='{brPasosa}' detach delete a");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task createLetPutnik(Guid idLeta, string brojPasosa)
        {
            IAsyncSession session = SessionManager.GetSession();
            
            try
            {
                IResultCursor cursor= await session.RunAsync($"MATCH (l:Let),(p:Putnik) WHERE l.ID = '{idLeta}' " +
                    $"AND p.BrojPasosa = '{brojPasosa}' MERGE (p)-[r:PRIPADA]->(l) SET" +
                    $" p.IdLeta_IdPrtljaga='{idLeta}::{brojPasosa}'  RETURN type(r)");
            }
            finally
            {
                await session.CloseAsync();
            }
           
        }
        #endregion
        #region Korisnik
        public static async Task<Korisnik> getKorisnik(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Korisnik korisnik;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (k:Korisnik) where k.Id='{id}'  return k");
                var lista = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);
                korisnik = JsonConvert.DeserializeObject<Korisnik>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (korisnik == null)
                korisnik = new Korisnik();
            return korisnik;
        }
        public static async Task<Korisnik> getKorisnikUsername(string username)
        {
            IAsyncSession session = SessionManager.GetSession();
            Korisnik korisnik;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (k:Korisnik) where k.UserName='{username}'  return k");
                var lista = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);
                korisnik = JsonConvert.DeserializeObject<Korisnik>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (korisnik == null)
                korisnik = new Korisnik();
            return korisnik;
        }
        public static async Task<Korisnik> updateKorisnik(Korisnik korisnik)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(k:Korisnik) where k.Id='{korisnik.Id}' " +
                    $"SET k.UserName='{korisnik.UserName}', k.Password='{korisnik.Password}'   return k");
            }
            finally
            {
                await session.CloseAsync();
            }
            return korisnik;
        }
        public static async Task<Korisnik> createKorisnik(Korisnik korisnik)
        {
            IAsyncSession session = SessionManager.GetSession();
            Korisnik kor;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(k:Korisnik {{UserName:'{korisnik.UserName}', " +
                    $"Password:'{korisnik.Password}',Id:'{Guid.NewGuid()}'}}) return k");
                var lista = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);
                kor = JsonConvert.DeserializeObject<Korisnik>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (kor == null)
                kor = new Korisnik();
            return kor;
        }
        public static async Task deleteKorisnik(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"Match(k:Korisnik) where k.Id='{id}' detach delete k");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<List<Korisnik>> getSveKorisnike()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Korisnik> listaKorisnika = new List<Korisnik>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(k:Korisnik) RETURN k");
                var lista = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);

                lista.ForEach(record =>
                {

                    Korisnik korisnik = JsonConvert.DeserializeObject<Korisnik>(JsonConvert.SerializeObject(record));
                    listaKorisnika.Add(korisnik);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaKorisnika;

        }
        public static async Task<int> daLiJeKorisnikPilotIliStjuardesa(Guid idKorisnika)
        {
            IAsyncSession session = SessionManager.GetSession();
            int p = -1; // Nema korisnika u bazi
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(k:Korisnik)-[r: KORISNIK_PILOT]->() where k.Id='{idKorisnika}' RETURN k");
                var lista = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);
                if (lista.Count > 0)
                {
                    p = 1; // Pilot
                    return p;
                }
                else
                {
                    cursor = await session.RunAsync($"MATCH(k:Korisnik)-[r: KORISNIK_STJUARDESA]->() where k.Id='{idKorisnika}' RETURN k");
                    var lista2 = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);
                    if (lista2.Count > 0)
                    {
                        p = 2; // Stjuardesa
                        return p;
                    }
                }
                cursor = await session.RunAsync($"MATCH(k:Korisnik) where k.Id='{idKorisnika}' and k.UserName='admin' RETURN k");
                var lista3 = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);
                if (lista3.Count > 0)
                {
                    p = 0; // Admin
                    return p;
                }
                cursor = await session.RunAsync($"MATCH(k:Korisnik) where k.Id='{idKorisnika}' RETURN k");
                var lista4 = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);
                if (lista4.Count > 0)
                {
                    p = 3; // Zaposleni
                    return p;
                }
            }
            finally
            {
                await session.CloseAsync();
            }
            return p;
        }



        #endregion
        #region Let
        public static async Task<Let> getLet(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Let let;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(l:Let ) where l.ID='{id}' return l");
                var lista = await cursor.ToListAsync(r => r["l"].As<INode>().Properties);
                let = JsonConvert.DeserializeObject<Let>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (let == null)
                let = new Let();
            return let;
        }
        public static async Task<Let> updateLet(Let let, List<Guid> idStjuardesa, List<Guid> idKompanija)
        {
            IAsyncSession session = SessionManager.GetSession();
            Let noviLet = new Let();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(l:Let) where l.ID='{let.ID}' " +
                    $"SET l.VremePoletanja='{let.VremePoletanja.ToString("MM/dd/yyyy HH:mm:ss")}'," +
                    $"l.VremeSletanja='{let.VremeSletanja.ToString("MM/dd/yyyy HH:mm:ss")}', l.Putnicki='{let.Putnicki}' return l");
                var lista = await cursor.ToListAsync(r => r["l"].As<INode>().Properties);
                noviLet = JsonConvert.DeserializeObject<Let>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
                //aerodromi
                cursor = await session.RunAsync($"MATCH(l:Let)-[r: LETI_SA]->() where l.ID='{let.ID}' DELETE r");
                cursor = await session.RunAsync($"MATCH(l:Let)-[r: LETI_NA]->() where l.ID='{let.ID}' DELETE r");
                await createLetAerodromi(let.ID, let.LetiSa.Id, let.LetiNa.Id);
                //avion
                cursor = await session.RunAsync($"MATCH(l:Let)<-[r: VRSTA_LETA]-() where l.ID='{let.ID}' DELETE r");
                await createLetAvion(let.ID, let.Avion.ID);
                //piloti
                cursor = await session.RunAsync($"MATCH(l:Let)<-[r: UPRAVLJA]-() where l.ID='{let.ID}' DELETE r");
                cursor = await session.RunAsync($"MATCH(l:Let)<-[r: KOPILOTIRA]-() where l.ID='{let.ID}' DELETE r");
                await createLetPiloti(let.ID, let.Pilot.Id, let.Kopilot.Id);
                //stjuardese
                cursor = await session.RunAsync($"MATCH(l:Let)<-[r: RADI_NA]-() where l.ID='{let.ID}' DELETE r");
                await createLetStjuardese(let.ID, idStjuardesa);
                //kompanije
                cursor = await session.RunAsync($"MATCH(l:Let)-[r: SARADNJA]->() where l.ID='{let.ID}' DELETE r");
                await createLetSaradnickeKompanije(let.ID, idKompanija);
            }
            finally
            {
                await session.CloseAsync();
            }
            return noviLet;
        }
        public static async Task<Let> createLet(Let let)
        {
            IAsyncSession session = SessionManager.GetSession();
            Let l;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(l:Let {{VremePoletanja:'{let.VremePoletanja.ToString("MM/dd/yyyy HH:mm:ss")}'," +
                    $"VremeSletanja:'{let.VremeSletanja.ToString("MM/dd/yyyy HH:mm:ss")}', Putnicki:'{let.Putnicki}',ID:'{Guid.NewGuid()}'}}) return l");
                var lista = await cursor.ToListAsync(r => r["l"].As<INode>().Properties);
                l = JsonConvert.DeserializeObject<Let>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (l == null)
                l = new Let();
            return l;
        }
        public static async Task deleteLet(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"Match(l:Let) where l.ID='{id}' detach delete l");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<List<Let>> getSveLetove()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Let> listaLetova = new List<Let>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(l:Let) RETURN l");
                var lista = await cursor.ToListAsync(r => r["l"].As<INode>().Properties);

                lista.ForEach(record =>
                {

                    Let let = JsonConvert.DeserializeObject<Let>(JsonConvert.SerializeObject(record));
                    listaLetova.Add(let);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaLetova;

        }

        public static async Task createLetAerodromi(Guid idLeta, Guid idAerodromaSa, Guid idAerodromaNa)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (a:Let),(b:Aerdrom) WHERE a.ID = '{idLeta}' " +
                    $"AND b.Id = '{idAerodromaSa}' CREATE(a) -[r: LETI_SA]->(b) RETURN type(r)");
                cursor = await session.RunAsync($"MATCH (a:Let),(b:Aerdrom) WHERE a.ID = '{idLeta}' " +
                    $"AND b.Id = '{idAerodromaNa}' CREATE(a) -[r: LETI_NA]->(b) RETURN type(r)");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<Aerodrom> getLetAerodrom(Guid idLeta, bool AerodromSa)
        {
            IAsyncSession session = SessionManager.GetSession();
            Aerodrom aerodrom;
            try
            {
                IResultCursor cursor;
                if (AerodromSa)
                    cursor = await session.RunAsync($"MATCH(l:Let)-[r:LETI_SA]->(a) WHERE l.ID='{idLeta}' RETURN a");
                else
                    cursor = await session.RunAsync($"MATCH(l:Let)-[r:LETI_NA]->(a) WHERE l.ID='{idLeta}' RETURN a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                aerodrom = JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (aerodrom == null)
                aerodrom = new Aerodrom();
            return aerodrom;
        }
        public static async Task createLetAvion(Guid idLeta, Guid idAviona)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (a:Let),(b:Avion) WHERE a.ID = '{idLeta}' " +
                    $"AND b.ID = '{idAviona}' CREATE(a) <-[r: VRSTA_LETA]-(b) RETURN type(r)");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<Avion> getLetAvion(Guid idLeta)
        {
            IAsyncSession session = SessionManager.GetSession();
            Avion avion;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(l:Let)<-[r: VRSTA_LETA]-(a) WHERE l.ID='{idLeta}' RETURN a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                avion = JsonConvert.DeserializeObject<Avion>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (avion == null)
                avion = new Avion();
            return avion;
        }
        public static async Task createLetPiloti(Guid idLeta, Guid idPilota, Guid idKopilota)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (a:Let),(b:Pilot) WHERE a.ID = '{idLeta}' " +
                    $"AND b.Id = '{idPilota}' CREATE(a) <-[r: UPRAVLJA]-(b) RETURN type(r)");
                cursor = await session.RunAsync($"MATCH (a:Let),(b:Pilot) WHERE a.ID = '{idLeta}' " +
                    $"AND b.Id = '{idKopilota}' CREATE(a) <-[r: KOPILOTIRA]-(b) RETURN type(r)");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<Pilot> getLetPilot(Guid idLeta, bool pilot)
        {
            IAsyncSession session = SessionManager.GetSession();
            Pilot p;
            try
            {
                IResultCursor cursor;
                if (pilot)
                    cursor = await session.RunAsync($"MATCH(l:Let)<-[r:UPRAVLJA]-(a) WHERE l.ID='{idLeta}' RETURN a");
                else
                    cursor = await session.RunAsync($"MATCH(l:Let)<-[r:KOPILOTIRA]-(a) WHERE l.ID='{idLeta}' RETURN a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                p = JsonConvert.DeserializeObject<Pilot>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (p == null)
                p = new Pilot();
            return p;
        }
        public static async Task createLetStjuardese(Guid idLeta, List<Guid> idStjuardesa)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor;
                foreach (var id in idStjuardesa)
                {
                    cursor = await session.RunAsync($"MATCH (a:Let),(b:Stjuardesa) WHERE a.ID = '{idLeta}' " +
                    $"AND b.Id = '{id}' CREATE(a) <-[r: RADI_NA]-(b) RETURN type(r)");
                }
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<List<Guid>> getLetStjuardese(Guid idLeta)
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Guid> listaStjuardesa = new List<Guid>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(l:Let)<-[r:RADI_NA]-(a) WHERE l.ID='{idLeta}' RETURN a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                lista.ForEach(record =>
                {

                    Stjuardesa stjuardesa = JsonConvert.DeserializeObject<Stjuardesa>(JsonConvert.SerializeObject(record));
                    listaStjuardesa.Add(stjuardesa.Id);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaStjuardesa;
        }
        public static async Task createLetSaradnickeKompanije(Guid idLeta, List<Guid> idSaradnickihKompanija)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor;
                foreach (var id in idSaradnickihKompanija)
                {
                    cursor = await session.RunAsync($"MATCH (a:Let),(b:SaradnickaKompanija) WHERE a.ID = '{idLeta}' " +
                    $"AND b.Id = '{id}' CREATE(a) -[r: SARADNJA]->(b) RETURN type(r)");
                }
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<List<Guid>> getLetSaradnickeKompanije(Guid idLeta)
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Guid> listaSaradnickihKompanija = new List<Guid>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(l:Let)-[r:SARADNJA]->(a) WHERE l.ID='{idLeta}' RETURN a");
                var lista = await cursor.ToListAsync(r => r["a"].As<INode>().Properties);
                lista.ForEach(record =>
                {

                    SaradnickaKompanija saradnickaKompanija = JsonConvert.DeserializeObject<SaradnickaKompanija>(JsonConvert.SerializeObject(record));
                    listaSaradnickihKompanija.Add(saradnickaKompanija.Id);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaSaradnickihKompanija;
        }



        #endregion
        #region Pilot
        public static async Task<Guid> getKorisnikPilot(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Korisnik korisnik;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (k:Korisnik)-[:KORISNIK_PILOT]-(p:Pilot) where p.Id='{id}' return k");
                var lista = await cursor.ToListAsync(r => r["k"].As<INode>().Properties);
                korisnik = JsonConvert.DeserializeObject<Korisnik>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (korisnik == null)
                korisnik = new Korisnik();
            return korisnik.Id;
        }
        public static async Task<Pilot> getPilot(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            Pilot pilot;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (p:Pilot) where p.Id='{id}'  return p");
                var lista = await cursor.ToListAsync(r => r["p"].As<INode>().Properties);
                pilot = JsonConvert.DeserializeObject<Pilot>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (pilot == null)
                pilot = new Pilot();
            return pilot;
        }
        public static async Task<Pilot> updatePilot(Pilot pilot)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(p:Pilot) where p.Id='{pilot.Id}' " +
                    $"SET p.Ime='{pilot.Ime}', p.Prezime='{pilot.Prezime}', p.GodineStaza='{pilot.GodineStaza}', " +
                    $"p.Starost='{pilot.Starost}'   return p");
            }
            finally
            {
                await session.CloseAsync();
            }
            return pilot;
        }
        public static async Task<Pilot> createPilot(Pilot pilot)
        {
            IAsyncSession session = SessionManager.GetSession();
            Pilot pil;
            try
            {
                IResultCursor cursor = await session.RunAsync($"MERGE(p:Pilot {{Ime:'{pilot.Ime}', Prezime:'{pilot.Prezime}', " +
                    $"GodineStaza:'{pilot.GodineStaza}', Starost:'{pilot.Starost}', Id:'{Guid.NewGuid()}'}}) return p");
                var lista = await cursor.ToListAsync(r => r["p"].As<INode>().Properties);
                pil = JsonConvert.DeserializeObject<Pilot>(JsonConvert.SerializeObject(lista.FirstOrDefault()));
            }
            finally
            {
                await session.CloseAsync();
            }
            if (pil == null)
                pil = new Pilot();
            return pil;
        }
        public static async Task deletePilot(Guid id)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(k:Korisnik)-[r:KORISNIK_PILOT]->(p) where p.Id='{id}' detach delete k");
                cursor = await session.RunAsync($"Match(p:Pilot) where p.Id='{id}' detach delete p");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<List<Pilot>> getSvePilote()
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Pilot> listaPilota = new List<Pilot>();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH(p:Pilot) RETURN p");
                var lista = await cursor.ToListAsync(r => r["p"].As<INode>().Properties);

                lista.ForEach(record =>
                {

                    Pilot pilot = JsonConvert.DeserializeObject<Pilot>(JsonConvert.SerializeObject(record));
                    listaPilota.Add(pilot);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return listaPilota;

        }

        public static async Task createPilotKorisnik(Guid idPilota, Guid idKorisnika)
        {
            IAsyncSession session = SessionManager.GetSession();
            try
            {
                IResultCursor cursor = await session.RunAsync($"MATCH (a:Korisnik),(b:Pilot) WHERE a.Id = '{idKorisnika}' " +
                    $"AND b.Id = '{idPilota}' CREATE(a) -[r: KORISNIK_PILOT]->(b) RETURN type(r)");
            }
            finally
            {
                await session.CloseAsync();
            }
        }
        public static async Task<Pilot> GetPilotKorisnikPom(Guid id, bool upravlja)
        {
            IAsyncSession session = SessionManager.GetSession();
            Pilot pilot;
            var records = new List<IRecord>();
            try
            {
                IResultCursor cursor;
                if (upravlja)
                {
                    cursor = await session.RunAsync($"match (k:Korisnik)-[:KORISNIK_PILOT]-(p:Pilot)-[:UPRAVLJA]-(l:Let)-[:VRSTA_LETA]-(a:Avion)" +
                                                              $" where k.Id='{id}'" +
                                                              $"match (p2:Pilot)-[:KOPILOTIRA]-(l)" +
                                                              $"match(aer: Aerdrom) -[:LETI_SA]-(l)" +
                                                              $"match(aer2: Aerdrom) -[:LETI_NA] - (l)" +
                                                              $"return p,l,a,aer,aer2,p2");
                }
                else
                {
                    cursor = await session.RunAsync($"match (k:Korisnik)-[:KORISNIK_PILOT]-(p:Pilot)-[:KOPILOTIRA]-(l:Let)-[:VRSTA_LETA]-(a:Avion)" +
                                                              $" where k.Id='{id}'" +
                                                              $"match (p2:Pilot)-[:UPRAVLJA]-(l)" +
                                                              $"match(aer: Aerdrom) -[:LETI_SA]-(l)" +
                                                              $"match(aer2: Aerdrom) -[:LETI_NA] - (l)" +
                                                              $"return p,l,a,aer,aer2,p2");
                }

                while (await cursor.FetchAsync())
                {
                    records.Add(cursor.Current);
                }

                //Pilot uvek isti zbog uslova
                if (records.Count == 0)
                    return new Pilot();
                var lista = records[0]["p"].As<INode>().Properties;
                pilot = JsonConvert.DeserializeObject<Pilot>(JsonConvert.SerializeObject(lista));
                pilot.Kopilotira = new List<Let>();
                pilot.Upravlja = new List<Let>();

                foreach (var record in records)
                {
                    lista = record["l"].As<INode>().Properties;
                    var let = JsonConvert.DeserializeObject<Let>(JsonConvert.SerializeObject(lista));

                    lista = record["a"].As<INode>().Properties;
                    var avion = JsonConvert.DeserializeObject<Avion>(JsonConvert.SerializeObject(lista));

                    lista = record["aer"].As<INode>().Properties;
                    var aer = JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(lista));

                    lista = record["aer2"].As<INode>().Properties;
                    var aer2 = JsonConvert.DeserializeObject<Aerodrom>(JsonConvert.SerializeObject(lista));

                    lista = record["p2"].As<INode>().Properties;
                    var p2 = JsonConvert.DeserializeObject<Pilot>(JsonConvert.SerializeObject(lista));

                    let.Avion = avion;
                    let.LetiSa = aer;
                    let.LetiNa = aer2;

                    let.SaradnickeKompanije = new List<SaradnickaKompanija>();
                    let.Stjuardese = new List<Stjuardesa>();
                    let.Putnici = new List<Putnik>();
                    let.Roba = new List<RobaKargo>();

                    var listaSaradnji = await getLetSaradnickeKompanijeLista(let.ID);
                    var listaStjuardesa = await getLetStjuardeseLista(let.ID);
                    var listaPutnika = await getLetPutnikLista(let.ID);
                    var listaRobe = await getLetRobaLista(let.ID);

                    let.SaradnickeKompanije = listaSaradnji;
                    let.Stjuardese = listaStjuardesa;
                    let.Putnici = listaPutnika;
                    let.Roba = listaRobe;

                    if (upravlja)
                    {
                        let.Kopilot = p2;
                        let.Pilot = pilot;
                        pilot.Upravlja.Add(let);
                    }
                    else
                    {
                        let.Pilot = p2;
                        let.Kopilot = pilot;
                        pilot.Kopilotira.Add(let);
                    }
                }

            }
            finally
            {
                await session.CloseAsync();
            }
            return pilot;
        }
        public static async Task<List<SaradnickaKompanija>> getLetSaradnickeKompanijeLista(Guid idLeta)
        {
            IAsyncSession session = SessionManager.GetSession();
            List<SaradnickaKompanija> lista = new List<SaradnickaKompanija>();
            var records = new List<IRecord>();
            try
            {
                IResultCursor cursor;
                cursor = await session.RunAsync($"MATCH(l:Let)-[r:SARADNJA]->(a) WHERE l.ID='{idLeta}' RETURN a");

                while (await cursor.FetchAsync())
                {
                    records.Add(cursor.Current);
                }

                foreach (var record in records)
                {
                    var rez = record["a"].As<INode>().Properties;
                    var kompanija = JsonConvert.DeserializeObject<SaradnickaKompanija>(JsonConvert.SerializeObject(rez));
                    lista.Add(kompanija);
                }
            }
            finally
            {
                await session.CloseAsync();
            }
            return lista;

        }
        public static async Task<List<Stjuardesa>> getLetStjuardeseLista(Guid idLeta)
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Stjuardesa> lista = new List<Stjuardesa>();
            var records = new List<IRecord>();
            try
            {
                IResultCursor cursor;
                cursor = await session.RunAsync($"MATCH(l:Let)<-[r:RADI_NA]-(a) WHERE l.ID='{idLeta}' RETURN a");

                while (await cursor.FetchAsync())
                {
                    records.Add(cursor.Current);
                }

                foreach (var record in records)
                {
                    var rez = record["a"].As<INode>().Properties;
                    var stjuardesa = JsonConvert.DeserializeObject<Stjuardesa>(JsonConvert.SerializeObject(rez));
                    lista.Add(stjuardesa);
                }
            }
            finally
            {
                await session.CloseAsync();
            }
            return lista;
        }
        public static async Task<List<Putnik>> getLetPutnikLista(Guid idLeta)
        {
            IAsyncSession session = SessionManager.GetSession();
            List<Putnik> lista = new List<Putnik>();
            var records = new List<IRecord>();
            try
            {
                IResultCursor cursor;
                cursor = await session.RunAsync($"MATCH(l:Let)<-[r:PRIPADA]-(a) WHERE l.ID='{idLeta}' RETURN a");

                while (await cursor.FetchAsync())
                {
                    records.Add(cursor.Current);
                }

                foreach (var record in records)
                {
                    var rez = record["a"].As<INode>().Properties;
                    var putnik = JsonConvert.DeserializeObject<Putnik>(JsonConvert.SerializeObject(rez));
                    lista.Add(putnik);
                }
            }
            finally
            {
                await session.CloseAsync();
            }
            return lista;
        }
        public static async Task<List<RobaKargo>> getLetRobaLista(Guid idLeta)
        {
            IAsyncSession session = SessionManager.GetSession();
            List<RobaKargo> lista = new List<RobaKargo>();
            var records = new List<IRecord>();
            try
            {
                IResultCursor cursor;
                cursor = await session.RunAsync($"MATCH(l:Let)-[r:PREVOZI]->(a) WHERE l.ID='{idLeta}' RETURN a");

                while (await cursor.FetchAsync())
                {
                    records.Add(cursor.Current);
                }

                foreach (var record in records)
                {
                    var rez = record["a"].As<INode>().Properties;
                    var roba = JsonConvert.DeserializeObject<RobaKargo>(JsonConvert.SerializeObject(rez));
                    lista.Add(roba);
                }
            }
            finally
            {
                await session.CloseAsync();
            }
            return lista;
        }
        public static async Task<Pilot> GetPilotKorisnik(Guid id)
        {
            Pilot pilot, pilot2;
            pilot = await GetPilotKorisnikPom(id, true);
            pilot2 = await GetPilotKorisnikPom(id, false);

            pilot.Kopilotira = pilot2.Kopilotira;

            return pilot;
        }
        #endregion
        public static async Task SeedDatabase()
        {
            //seedovanje baze ako ne postoji
            await SeedPutnici();
            await SeedRobaKargo();
        }

    }
}
