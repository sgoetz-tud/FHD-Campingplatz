class Campingplatz
{
    private List<Stellplatz> stellplaetze;
    private List<Buchung> buchungen;

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

    public int Abrechnen(Buchung b)
    {
        int days = (b.Abreise - b.Anreise).Days;
        return days * b.Stellplatz.Preis;
    }

    static void Main()
    {
        Campingplatz cp = new Campingplatz();
        Kunde kunde = new Kunde();
        Buchung b = kunde.buchen(cp, DateTime.Parse("01.07.2022"), DateTime.Parse("10.07.2022"), 303);

        int preis = cp.Abrechnen(b);
        
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
    public Buchung(DateTime anreise, DateTime abreise, Stellplatz sp)
    {
        Anreise = anreise;
        Abreise = abreise;
        Stellplatz = sp;
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
}