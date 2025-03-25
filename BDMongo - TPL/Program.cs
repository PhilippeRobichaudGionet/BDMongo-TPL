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


#region Database Add function

void AddBook(string titre, bool dispo, double prix, string[] exemplaires, int année, string édition, string auteur)
{
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

void AddPeriodic(string titre, bool dispo, double prix, string date, string périodicité) 
{
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

void AddBD(string titre, bool dispo, double prix, int année, string édition, string auteur,string dessinateur) 
{
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


