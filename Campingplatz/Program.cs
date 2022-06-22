class Campingplatz
{
    private List<Stellplatz> stellplaetze;
    private List<Buchung> buchungen;

    public Shop Shop { get; set; }

    public Campingplatz()
    {
        stellplaetze = new List<Stellplatz>();
        stellplaetze.Add(new Premiumstellplatz(301));
        stellplaetze.Add(new Premiumstellplatz(302));
        stellplaetze.Add(new Premiumstellplatz(303));
        stellplaetze.Add(new Standardstellplatz(201));
        stellplaetze.Add(new Standardstellplatz(202));
        stellplaetze.Add(new Standardstellplatz(203));
        stellplaetze.Add(new Zeltparzelle(404));
        stellplaetze.Add(new Zeltparzelle(420));

        Shop = new Shop();

        buchungen = new List<Buchung>();
    }

    public Stellplatz GetStellplatz(int nummer)
    {
        foreach (Stellplatz sp in stellplaetze)
        {
            if (sp.Nummer == nummer) return sp;
        }
        return null;
    }

    public double Abrechnen(Buchung b)
    {
        int days = (b.Abreise - b.Anreise).Days;
        double preis = days * b.Stellplatz.Preis;
        
        foreach(Produkt p in b.GetGekaufteProdukte())
        {
            preis += p.Preis;
        }

        return preis;
    }

    static void Main()
    {
        Campingplatz cp = new Campingplatz();
        Kunde kunde = new Kunde();
        Buchung b = kunde.buchen(cp, DateTime.Parse("01.07.2022"), DateTime.Parse("10.07.2022"), 303);

        foreach(Produkt p in cp.Shop.GetProdukte())
        {
            if(p.Name == "Brötchen")
            {
                kunde.KaufeProdukt(p, 303);
            }
        }

        double preis = cp.Abrechnen(b);
        
        Console.WriteLine("Gesamtkosten: "+preis);
    }
}

abstract class Stellplatz
{
    public int Preis { get; set; }
    public int Nummer { get; set; }

    public Stellplatz(int nummer)
    {
        Nummer = nummer;

    }
}

class Premiumstellplatz : Stellplatz
{
    public Premiumstellplatz(int nummer) : base(nummer)
    {
        Preis = 40;
    }
}

class Standardstellplatz : Stellplatz
{
    public Standardstellplatz(int nummer) : base(nummer)
    {
        Preis = 30;
    }
}

class Zeltparzelle : Stellplatz
{
    public Zeltparzelle(int nummer) : base(nummer)
    {
        Preis = 20;
    }
}

class Buchung
{
    public DateTime Anreise { get; set; }
    public DateTime Abreise { get; set; }
    public Stellplatz Stellplatz { get; set; }
    
    private List<Produkt> gekaufteProdukte;
    public Buchung(DateTime anreise, DateTime abreise, Stellplatz sp)
    {
        Anreise = anreise;
        Abreise = abreise;
        Stellplatz = sp;
        gekaufteProdukte = new List<Produkt>();
    }

    public void ProduktHinzufügen(Produkt p)
    {
        gekaufteProdukte.Add(p);
    }

    public List<Produkt> GetGekaufteProdukte()
    {
        return gekaufteProdukte;
    }
}

class Kunde
{
    private List<Buchung> buchungen;

    public Kunde()
    {
        buchungen = new List<Buchung>();
    }

    public Buchung buchen(Campingplatz cp, DateTime anreise, DateTime abreise, int nummer)
    {
        Stellplatz sp = cp.GetStellplatz(nummer);
        Buchung b = new Buchung(anreise, abreise, sp);
        buchungen.Add(b);
        return b;
    }

    public void KaufeProdukt(Produkt p, int nummer)
    {
        //Buchung finden
        Buchung aktuelleBuchung;
        foreach(Buchung b in buchungen)
        {
            if (b.Stellplatz.Nummer == nummer)
            {
                aktuelleBuchung = b;
                aktuelleBuchung.ProduktHinzufügen(p);
                break;
            }
        }
    }
}

class Shop
{
    private List<Produkt> produkte;

    public Shop()
    {
        produkte = new List<Produkt>();
        produkte.Add(new Produkt("Brötchen",0.50));
        produkte.Add(new Produkt("Brötchen", 0.50));
        produkte.Add(new Produkt("Brötchen", 0.50));
        produkte.Add(new Produkt("Brötchen", 0.50));
        produkte.Add(new Produkt("Brötchen", 0.50));
        produkte.Add(new Produkt("Croissant", 0.99));
        produkte.Add(new Produkt("Croissant", 0.99));
        produkte.Add(new Produkt("Croissant", 0.99));
        produkte.Add(new Produkt("Campinggas", 2.00));
        produkte.Add(new Produkt("Campinggas", 2.00));
        produkte.Add(new Produkt("Limo", 1.00));
        produkte.Add(new Produkt("Limo", 1.00));
    }

    public List<Produkt> GetProdukte()
    {
        return produkte;
    }
}

class Produkt
{
    public string Name { get; set; }
    public double Preis { get; set; }

    public Produkt(string name, double preis)
    {
        Name = name;
        Preis = preis;
    }

}