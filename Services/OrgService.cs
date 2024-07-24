using AnOrg.Models;
using MongoDB.Driver;

namespace AnOrg.Services
{
    public class OrgService
    {

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public OrgService()
        {
            //const string connectionUri = "mongodb://localhost:27017";
            const string connectionUri = "mongodb+srv://deepak:deepak@demo.8qznpnu.mongodb.net/?retryWrites=true&w=majority&appName=Demo";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            _client = new MongoClient(settings);
            _database = _client.GetDatabase("demo");
        }
        public void startapp()
        {           
            var collection = _database.GetCollection<Organization>("organizations");
            Organization organization = new Organization
            {
                Id=1000,
                ProjectDealscnt = 1000,
                Projectcnt = 1000,
                Callcnt = 1,
                Meetcnt = 1
            };
            
            collection.InsertOne(organization);

        }

        public Analatics getanalatics()
        {
            long guests, clients, admins;
            long dealsAccepted = 0, dealsRejected = 0,dealsCreated = 0;
            long projectCompleted = 0, projectProgress = 0;
            long budgetEarned = 0, budgetProgress = 0;
            long meetsScheduled = 0, meetsSuccess = 0, meetsFailed = 0;
            long callsScheduled = 0, callsSuccess = 0, callsFailed = 0;

            var c1 = _database.GetCollection<CustomerDetails>("customers");
            var f1 = Builders<CustomerDetails>.Filter.Empty;
            List<CustomerDetails> customers = c1.Find(f1).ToList();
            clients = customers.Count(c => c.IsClient);
            guests = customers.Count(c => !c.IsClient);
            var c2 = _database.GetCollection<AdminDetails>("admins");
            var f2 = Builders<AdminDetails>.Filter.Empty;
            List<AdminDetails> admin = c2.Find(f2).ToList();
            admins = admin.Count();


            var collection = _database.GetCollection<ProjectDeail>("projectdeals");
            var filter = Builders<ProjectDeail>.Filter.Empty;
            List<ProjectDeail> lis = collection.Find(filter).ToList();
            foreach (var projectDeail in lis)
            {
                switch (projectDeail.State)
                {
                    case "Accepted":
                        dealsAccepted++;
                        break;
                    case "Canceled":
                        dealsRejected++;
                        break;
                    default:
                        dealsCreated++;
                        break;
                }
            }

            var c3 = _database.GetCollection<ProjectDetails>("projects");
            var f3 = Builders<ProjectDetails>.Filter.Empty;
            List<ProjectDetails> lis2 = c3.Find(f3).ToList();
            foreach (var p in lis2)
            {
                switch (p.State)
                {
                    case "Completed":
                        projectCompleted++;
                        budgetEarned = budgetEarned + p.Budget;
                        break;
                    default:
                        projectProgress++;
                        budgetProgress = budgetProgress + p.Budget;
                        break;
                }
            }

            var c4 = _database.GetCollection<MeetDetails>("meets");
            var f4 = Builders<MeetDetails>.Filter.Empty;
            List<MeetDetails> lis3 = c4.Find(f4).ToList();
            foreach (var p in lis3)
            {
                switch (p.status)
                {
                    case "Scheduled":
                        meetsScheduled++;
                        break;
                    case "Success":
                        meetsSuccess++;
                        break;
                    default:
                        meetsFailed++;
                        break;
                }
            }

            var c5 = _database.GetCollection<CallDetails>("calls");
            var f5 = Builders<CallDetails>.Filter.Empty;
            List<CallDetails> lis4 = c5.Find(f5).ToList();
            foreach (var p in lis4)
            {
                switch (p.status)
                {
                    case "Scheduled":
                        callsScheduled++;
                        break;
                    case "Success":
                        callsSuccess++;
                        break;
                    default:
                        callsFailed++;
                        break;
                }
            }
            return new Analatics
            {
                Guests = guests,
                Clients = clients,
                Admins = admins,
                DealsAccepted = dealsAccepted,
                DealsRejected = dealsRejected,
                DealsCreated = dealsCreated,
                ProjectCompleted = projectCompleted,
                ProjectProgress = projectProgress,
                BudgetEarned = budgetEarned,
                BudgetProgress = budgetProgress,
                MeetsScheduled = meetsScheduled,
                MeetsFailed = meetsFailed,
                MeetsSuccess = meetsSuccess,
                CallsFailed = callsFailed,
                CallsSuccess = callsSuccess,
                CallsScheduled = callsScheduled,
            };

        }
    }
}
