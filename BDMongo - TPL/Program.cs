using MongoDB.Driver;
using MongoDB.Bson;
using static System.Runtime.InteropServices.JavaScript.JSType;

#region Initiate MongoDB data
const string connectionString = "mongodb://localhost:27017";
MongoClient mong;
IMongoDatabase db;
IMongoCollection<BsonDocument>? TPLDoc = null;
#endregion

#region Connect data Try-catch
try
{
    mong = new MongoClient(connectionString);
    db = mong.GetDatabase("Library-TPL");
    TPLDoc = db.GetCollection<BsonDocument>("ouvrages");
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
#endregion


var critere = Builders<BsonDocument>.Filter.Empty;
var resultat = TPLDoc.Find(critere).ToList();
foreach (var doc in resultat)
{
    Console.WriteLine(doc + " \n");
}
AffichageMenu();
void AffichageMenu()
{
    bool fermer = true;
    while (!fermer)
    {
        Console.WriteLine("\nMenu :");
        Console.WriteLine("1. Ajouter un livre");
        Console.WriteLine("2. Ajouter un périodique");
        Console.WriteLine("3. Ajouter une BD");
        Console.WriteLine("4. Rechercher les périodiques (prix décroissant)");
        Console.WriteLine("5. Rechercher une BD par dessinateur");
        Console.WriteLine("6. Calculer le prix moyen d’un type d’ouvrage");
        Console.WriteLine("7. Quitter");
        Console.Write("Choisissez une option : ");

        string choix = Console.ReadLine();
        switch (choix)
        {
            case "1": AddBook(); break;
            case "2": AddPeriodic(); break;
            case "3": AddBD(); break;
            case "4": //Function(); break;
            case "5": //Function(); break;
            case "6": //Function(); break;
            case "7": return;
            default: Console.WriteLine("Option invalide."); break;
        }
    }
}

#region Database Add function

void AddBook()
{
    Console.Write("Titre : ");
    string titre = Console.ReadLine();
    Console.Write("Disponible (1/0) : ");
    bool dispo = Console.ReadLine() == "1";
    Console.Write("Prix : ");
    double prix = double.Parse(Console.ReadLine());
    Console.Write("Année : ");
    int année = int.Parse(Console.ReadLine());
    Console.Write("Maison d'édition : ");
    string édition = Console.ReadLine();
    Console.Write("Auteur : ");
    string auteur = Console.ReadLine();
    Console.Write("Nombre d'exemplaires : ");
    int nbExemplaires = int.Parse(Console.ReadLine());
    string[] exemplaires = new string[nbExemplaires];
    for (int i = 0; i < nbExemplaires; i++)
    {
        Console.Write($"Exemplaire {i + 1} : ");
        exemplaires[i] = Console.ReadLine();
    }
    var doc = new BsonDocument
    {
        {"titre",titre},
        {"dispo",dispo ? 1 : 0 },
        {"prix",prix},
        {"type","livre"},
        {"exemplaires", new BsonArray(exemplaires)},
        {"détails", new BsonDocument{{"année",année },{"maison d'édition",édition },{"auteur",auteur } } }
    };

    TPLDoc.InsertOne(doc);
}

void AddPeriodic() 
{
    Console.Write("Titre : ");
    string titre = Console.ReadLine();
    Console.Write("Disponible (1/0) : ");
    bool dispo = Console.ReadLine() == "1";
    Console.Write("Prix : ");
    double prix = double.Parse(Console.ReadLine());
    Console.Write("Date : ");
    string date = Console.ReadLine();
    Console.Write("Périodicité : ");
    string périodicité = Console.ReadLine();
    var doc = new BsonDocument
    {
        {"titre",titre},
        {"dispo",dispo ? 1 : 0 },
        {"prix",prix},
        {"type","périodique"},
        {"détails", new BsonDocument{{ "date", date },{ "périodicité", périodicité } } }
    };

    TPLDoc.InsertOne(doc);
}

void AddBD() 
{
    Console.Write("Titre : ");
    string titre = Console.ReadLine();
    Console.Write("Disponible (1/0) : ");
    bool dispo = Console.ReadLine() == "1";
    Console.Write("Prix : ");
    double prix = double.Parse(Console.ReadLine());
    Console.Write("Année : ");
    int année = int.Parse(Console.ReadLine());
    Console.Write("Maison d'édition : ");
    string édition = Console.ReadLine();
    Console.Write("Auteur : ");
    string auteur = Console.ReadLine();
    Console.Write("Dessinateur : ");
    string dessinateur = Console.ReadLine();
    var doc = new BsonDocument
    {
        {"titre",titre},
        {"dispo",dispo ? 1 : 0 },
        {"prix",prix},
        {"type","BD"},
        {"détails", new BsonDocument{{ "année", année },{ "maison d'édition", édition},{"auteur", auteur },{ "dessinateur", dessinateur } } }
    };

    TPLDoc.InsertOne(doc);
}
#endregion


