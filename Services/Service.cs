using MongoDB.Driver;
using AnOrg.Models;
using AnOrg.ViewModels;
using System;
using System.Globalization;
namespace AnOrg.Services
{
    public class Servie
    {

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public Servie()
        {
           //const string connectionUri = "mongodb://localhost:27017";
           const string connectionUri = "mongodb+srv://deepak:deepak@demo.8qznpnu.mongodb.net/?retryWrites=true&w=majority&appName=Demo";
            var settings = MongoClientSettings.FromConnectionString(connectionUri);
            _client = new MongoClient(settings);
            _database = _client.GetDatabase("demo");
        }

        public bool AddProject(ProjectDetails projectDetails)
        {
            try
            {
                if (IsCustomerIdPresent(projectDetails.CustomerId))
                {
                    Console.WriteLine(projectDetails);
                    var collection = _database.GetCollection<ProjectDetails>("projects");
                    collection.InsertOne(projectDetails);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool IsCustomerIdPresent(String id)
        {
            var collection = _database.GetCollection<CustomerDetails>("customers");
            var filter = Builders<CustomerDetails>.Filter.Eq(c => c.Id, id);
            var exists = collection.Find(filter).Any();
            return exists;
        }
        public bool pre(String id)
        {
            var collection = _database.GetCollection<AdminDetails>("admins");
            var filter = Builders<AdminDetails>.Filter.Eq(c => c.Id, id);
            var exists = collection.Find(filter).Any();
            return exists;
        }


        public CustomerDetails GetCustomerDetails(string id)
        {
            var collection = _database.GetCollection<CustomerDetails>("customers");
            var filter = Builders<CustomerDetails>.Filter.Eq(c => c.Id, id);
            CustomerDetails customer = collection.Find(filter).FirstOrDefault();
            return customer;
        }
        public AdminDetails gasd(string id)
        {
            var collection = _database.GetCollection<AdminDetails>("admins");
            var filter = Builders<AdminDetails>.Filter.Eq(c => c.Id, id);
            AdminDetails customer = collection.Find(filter).FirstOrDefault();
            return customer;
        }
        

        public bool UpdateCustomerAsClient(string id)
        {
            try
            {
                var collection = _database.GetCollection<CustomerDetails>("customers");
                var filter = Builders<CustomerDetails>.Filter.Eq(c => c.Id, id);
                var updateDefinition = Builders<CustomerDetails>.Update
                    .Set(c => c.IsClient, true);

                var options = new FindOneAndUpdateOptions<CustomerDetails>
                {
                    ReturnDocument = ReturnDocument.After
                };

                CustomerDetails updated = collection.FindOneAndUpdate(filter, updateDefinition, options);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }




        public int AddCustomer(CustomerDetails customerDetails)
        {
            try
            {
                var collection = _database.GetCollection<CustomerDetails>("customers");
                if (!IsCustomerIdPresent(customerDetails.Id))
                {
                    collection.InsertOne(customerDetails);
                    return 1;
                }
                else
                {
                    return 2;
                }
               
                
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int VerifyCustomer(CustomerLoginView customerLoginView)
        {
            try
            {
                var collection = _database.GetCollection<CustomerDetails>("customers");
                if (IsCustomerIdPresent(customerLoginView.Id))
                {
                    CustomerDetails c=GetCustomerDetails(customerLoginView.Id);
                    if (c.Password.Equals(customerLoginView.Password))
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                    
                }
                else
                {
                    return 3;
                }


            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool IsAdminIdPresent(String id)
        {
            var collection = _database.GetCollection<AdminDetails>("admins");
            var filter = Builders<AdminDetails>.Filter.Eq(c => c.Id, id);
            var exists = collection.Find(filter).Any();
            return exists;
        }


        public AdminDetails GetAdminDetails(String id)
        {
            var collection = _database.GetCollection<AdminDetails>("admins");
            var filter = Builders<AdminDetails>.Filter.Eq(c => c.Id, id);
            AdminDetails customer = collection.Find(filter).FirstOrDefault();
            return customer;
        }

        public int AddAdmin(AdminDetails a)
        {
            try
            {
                var collection = _database.GetCollection<AdminDetails>("admins");
                if (!IsAdminIdPresent(a.Id))
                {
                    collection.InsertOne(a);
                    return 1;
                }
                else
                {
                    return 2;
                }


            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int VerifyAdmin(AdminLoginView a)
        {
            try
            {
                var collection = _database.GetCollection<AdminDetails>("customers");
                if (IsAdminIdPresent(a.Id))
                {
                    AdminDetails c = GetAdminDetails(a.Id);
                    if (c.Password.Equals(a.Password))
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }

                }
                else
                {
                    return 3;
                }


            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<CustomerDetails> GetAllCustomers()
        {
            var collection = _database.GetCollection<CustomerDetails>("customers");
            var filter = Builders<CustomerDetails>.Filter.Empty; 
            List<CustomerDetails> customers = collection.Find(filter).ToList();
            return customers;
        }

        public List<AdminDetails> GetAllAdmins()
        {
            var collection = _database.GetCollection<AdminDetails>("admins");
            var filter = Builders<AdminDetails>.Filter.Empty; 
            List<AdminDetails> admin = collection.Find(filter).ToList();
            return admin;
        }


        public long AddProjectDeals(ProjectDealView p)
        {
            try
            {

                var c1 = _database.GetCollection<Organization>("organizations");
                var filter = Builders<Organization>.Filter.Empty;
                List<Organization> p1 = c1.Find(filter).ToList();
                Organization p2 = p1.FirstOrDefault();

                ProjectDeail pp = new ProjectDeail
                {
                    Id = p2.ProjectDealscnt,
                    CustomerId=p.CustomerId,
                    State="Created",
                    Platforms =p.Platforms,
                    Technologies=p.Technologies,
                    Integrations = p.Integrations,
                    Discription = p.Discription,
                    Stakeholders=p.Stakeholders,
                    ContactMedium=p.ContactMedium
                };

                 var collection = _database.GetCollection<ProjectDeail>("projectdeals");
                 collection.InsertOne(pp);

                var filter1 = Builders<Organization>.Filter.Eq(c => c.Id, 1000);
                var updateDefinition = Builders<Organization>.Update
                    .Set(c => c.ProjectDealscnt, p2.ProjectDealscnt+1);

                var options1 = new FindOneAndUpdateOptions<Organization>
                {
                    ReturnDocument = ReturnDocument.After
                };

                Organization updated = c1.FindOneAndUpdate(filter1, updateDefinition, options1);
                return p2.ProjectDealscnt;
               
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<ProjectDeail> getProjectDealCustomer(string id)
        {
            var collection = _database.GetCollection<ProjectDeail>("projectdeals");
            var filter = Builders<ProjectDeail>.Filter.Eq(c => c.CustomerId, id);
            List<ProjectDeail> lis = collection.Find(filter).ToList();
            return lis;
        }


        
        public List<ProjectDeail> getProjectDeals()
        {
            var collection = _database.GetCollection<ProjectDeail>("projectdeals");
            var filter = Builders<ProjectDeail>.Filter.Empty;
            List<ProjectDeail> lis = collection.Find(filter).ToList();
            return lis;
        }

        public ProjectDeail getProjectDeal(long id)
        {
            var collection = _database.GetCollection<ProjectDeail>("projectdeals");
            var filter = Builders<ProjectDeail>.Filter.Eq(c => c.Id, id);
            return collection.Find(filter).FirstOrDefault();
        }

        public ProjectDetails getProject(long id)
        {
            var collection = _database.GetCollection<ProjectDetails>("projects");
            var filter = Builders<ProjectDetails>.Filter.Eq(c => c.Id, id);
            return collection.Find(filter).FirstOrDefault();
        }

        public void UpdateProjectDealCallState(ProjectDealStatusCallView p)
        {
           
                ProjectDeail pr = getProjectDeal(p.Id);
                CustomerDetails cd = GetCustomerDetails(pr.CustomerId);
                CallView cv = new CallView { Catogory ="Deal",CustomerId=pr.CustomerId,ProjectId=pr.Id,Phone=cd.Phone, time = DateTime.TryParse(p.time, null, DateTimeStyles.RoundtripKind, out DateTime parsedDateTime) ? parsedDateTime : throw new ArgumentException("Invalid DateTime format", nameof(p.time)) };
               bool b = addcall(cv);
            
            var collection = _database.GetCollection<ProjectDeail>("projectdeals");
            var filter2 = Builders<ProjectDeail>.Filter.Eq(c => c.Id, p.Id);
            var updateDefinition1 = Builders<ProjectDeail>.Update
                .Set(c => c.State, "Call Scheduled");

            var options2 = new FindOneAndUpdateOptions<ProjectDeail>
            {
                ReturnDocument = ReturnDocument.After
            };
            ProjectDeail updated1 = collection.FindOneAndUpdate(filter2, updateDefinition1, options2);

        }

        public void UpdateProjectCallState(ProjectDealStatusCallView p)
        {

            ProjectDetails pr = getProject(p.Id);
            CustomerDetails cd = GetCustomerDetails(pr.CustomerId);
            CallView cv = new CallView { Catogory = "Project", CustomerId = pr.CustomerId, ProjectId = pr.Id, Phone = cd.Phone, time = DateTime.TryParse(p.time, null, DateTimeStyles.RoundtripKind, out DateTime parsedDateTime) ? parsedDateTime : throw new ArgumentException("Invalid DateTime format", nameof(p.time)) };
            bool b = addcall(cv);
            var collection = _database.GetCollection<ProjectDetails>("projects");
            var filter2 = Builders<ProjectDetails>.Filter.Eq(c => c.Id, p.Id);
            var updateDefinition1 = Builders<ProjectDetails>.Update
                .Set(c => c.State, "Call Scheduled");

            var options2 = new FindOneAndUpdateOptions<ProjectDetails>
            {
                ReturnDocument = ReturnDocument.After
            };
            ProjectDetails updated1 = collection.FindOneAndUpdate(filter2, updateDefinition1, options2);

        }


        public void UpdateProjectDealState(ProjectDealStatusView p)
        {
           
            var collection = _database.GetCollection<ProjectDeail>("projectdeals");
            var filter2 = Builders<ProjectDeail>.Filter.Eq(c => c.Id, p.Id);
            var updateDefinition1 = Builders<ProjectDeail>.Update
                .Set(c => c.State,p.Status);
            var options2 = new FindOneAndUpdateOptions<ProjectDeail>
            {
                ReturnDocument = ReturnDocument.After
            };
            ProjectDeail updated1 = collection.FindOneAndUpdate(filter2, updateDefinition1, options2);

        }




        public void UpdateProjectState(ProjectDealStatusView p)
        {
            var collection = _database.GetCollection<ProjectDetails>("projects");
            var filter2 = Builders<ProjectDetails>.Filter.Eq(c => c.Id, p.Id);
            var updateDefinition1 = Builders<ProjectDetails>.Update
                .Set(c => c.State, p.Status);

            var options2 = new FindOneAndUpdateOptions<ProjectDetails>
            {
                ReturnDocument = ReturnDocument.After
            };
            ProjectDetails updated1 = collection.FindOneAndUpdate(filter2, updateDefinition1, options2);

        }

        public bool acceptProject(IdLongView i)
        {
            try
            {
                var collection = _database.GetCollection<ProjectDeail>("projectdeals");
                var collection1 = _database.GetCollection<ProjectDetails>("projects");
                var filter = Builders<ProjectDeail>.Filter.Eq(c => c.Id, i.Id);
                ProjectDeail lis = collection.Find(filter).FirstOrDefault();
                var c1 = _database.GetCollection<Organization>("organizations");
                var f = Builders<Organization>.Filter.Empty;
                List<Organization> p1 = c1.Find(f).ToList();
                Organization p2 = p1.FirstOrDefault();
                ProjectDetails d = new ProjectDetails
                {
                    Id = p2.Projectcnt,
                    CustomerId = lis.CustomerId,
                    State = "Accepted",
                    Platforms = lis.Platforms,
                    Technologies = lis.Technologies,
                    Integrations = lis.Integrations,
                    Discription = lis.Discription,
                    Stakeholders = lis.Stakeholders,
                    Budget = 0,
                    ContactMedium = lis.ContactMedium
                };
                collection1.InsertOne(d);
                var filter1 = Builders<Organization>.Filter.Eq(c => c.Id, 1000);
                var updateDefinition = Builders<Organization>.Update
                    .Set(c => c.Projectcnt, p2.Projectcnt + 1);

                var options1 = new FindOneAndUpdateOptions<Organization>
                {
                    ReturnDocument = ReturnDocument.After
                };
                Organization updated = c1.FindOneAndUpdate(filter1, updateDefinition, options1);
                UpdateProjectDealState(new ProjectDealStatusView
                {
                    Id=lis.Id,
                    Status="Accepted"
                });
                UpdateCustomerAsClient(lis.CustomerId);
                return true;
            }catch(Exception e) { 
                return false;
            }
        }


        public List<ProjectDetails> getProjectCustomer(string id)
        {
            var collection = _database.GetCollection<ProjectDetails>("projects");
            var filter = Builders<ProjectDetails>.Filter.Eq(c => c.CustomerId, id);
            List<ProjectDetails> lis = collection.Find(filter).ToList();
            return lis;
        }



        public List<ProjectDetails> getProjects()
        {
            var collection = _database.GetCollection<ProjectDetails>("projects");
            var filter = Builders<ProjectDetails>.Filter.Empty;
            List<ProjectDetails> lis = collection.Find(filter).ToList();
            return lis;
        }


        public bool updateProject(ProjectDetails s)
        {
            try
            {
                var c = _database.GetCollection<ProjectDetails>("projects");
                var f = Builders<ProjectDetails>.Filter.Eq(c => c.Id, s.Id);
                var updateDefinition = Builders<ProjectDetails>.Update
                    .Set(c => c.Id, s.Id)
                    .Set(c => c.CustomerId, s.CustomerId)
                    .Set(c => c.State, s.State)
                    .Set(c => c.Platforms, s.Platforms)
                    .Set(c => c.Technologies, s.Technologies)
                    .Set(c => c.Integrations, s.Integrations)
                    .Set(c => c.Discription, s.Discription)
                    .Set(c => c.Stakeholders, s.Stakeholders)
                    .Set(c => c.Budget, s.Budget)
                    .Set(c => c.ContactMedium, s.ContactMedium);

                var options1 = new FindOneAndUpdateOptions<ProjectDetails>
                {
                    ReturnDocument = ReturnDocument.After
                };
                ProjectDetails updated = c.FindOneAndUpdate(f, updateDefinition, options1);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public void UpdateCallState(ProjectDealStatusView p)
        {
            var collection = _database.GetCollection<CallDetails>("calls");
            var filter2 = Builders<CallDetails>.Filter.Eq(c => c.Id, p.Id);
            var updateDefinition1 = Builders<CallDetails>.Update.Set(c => c.status, p.Status);
            var options2 = new FindOneAndUpdateOptions<CallDetails>
            {  ReturnDocument = ReturnDocument.After };
            CallDetails updated1 = collection.FindOneAndUpdate(filter2, updateDefinition1, options2);

        }



        public bool addcall(CallView c)
        {
            try
            {
                var c1 = _database.GetCollection<Organization>("organizations");
                var f = Builders<Organization>.Filter.Empty;
                List<Organization> p1 = c1.Find(f).ToList();
                Organization p2 = p1.FirstOrDefault();
                CallDetails cl = new CallDetails { Id = p2.Callcnt, Catogory = c.Catogory, CustomerId = c.CustomerId, ProjectId = c.ProjectId, Phone = c.Phone, time = c.time, status = "Scheduled" };
                var collection = _database.GetCollection<CallDetails>("calls");
                collection.InsertOne(cl);
                var filter1 = Builders<Organization>.Filter.Eq(c => c.Id, 1000);
                var updateDefinition = Builders<Organization>.Update.Set(c => c.Callcnt, p2.Callcnt + 1);
                var options1 = new FindOneAndUpdateOptions<Organization> { ReturnDocument = ReturnDocument.After };
                Organization updated = c1.FindOneAndUpdate(filter1, updateDefinition, options1);
                return true;
            }catch(Exception e)
            {
                return false;
            }
        }

        public List<CallDetails> allcalls()
        {
            var collection = _database.GetCollection<CallDetails>("calls");
            var filter = Builders<CallDetails>.Filter.Empty;
            List<CallDetails> lis = collection.Find(filter).ToList();
            return lis;

        }

        public List<CallDetails> allcustomercalls(string i)
        {
            var collection = _database.GetCollection<CallDetails>("calls");
            var filter = Builders<CallDetails>.Filter.Eq(c => c.CustomerId, i);
            List<CallDetails> lis = collection.Find(filter).ToList();
            return lis;

        }

        public void addmeet(MeetView m)
        {
            ProjectDetails p = getProject(m.ProjectId);
            var collection = _database.GetCollection<MeetDetails>("meets");

            var c1 = _database.GetCollection<Organization>("organizations");
            var f = Builders<Organization>.Filter.Empty;
            List<Organization> p1 = c1.Find(f).ToList();
            Organization p2 = p1.FirstOrDefault();

            MeetDetails mm = new MeetDetails { Id = p2.Meetcnt, CustomerId = p.CustomerId, ProjectId = p.Id, link = m.Link, time = m.Time, status = "Scheduled" };
            collection.InsertOne(mm);

            var filter1 = Builders<Organization>.Filter.Eq(c => c.Id, 1000);
            var updateDefinition = Builders<Organization>.Update.Set(c => c.Meetcnt, p2.Meetcnt + 1);
            var options1 = new FindOneAndUpdateOptions<Organization> { ReturnDocument = ReturnDocument.After };
            Organization updated = c1.FindOneAndUpdate(filter1, updateDefinition, options1);

        }

        public List<MeetDetails> allmeets()
        {
            var collection = _database.GetCollection<MeetDetails>("meets");
            var filter = Builders<MeetDetails>.Filter.Empty;
            List<MeetDetails> lis = collection.Find(filter).ToList();
            return lis;

        }

        public List<MeetDetails> allcustomermeets(string i)
        {
            var collection = _database.GetCollection<MeetDetails>("meets");
            var filter = Builders<MeetDetails>.Filter.Eq(c => c.CustomerId, i);
            List<MeetDetails> lis = collection.Find(filter).ToList();
            return lis;

        }

        public void UpdateMeetState(ProjectDealStatusView p)
        {
            var collection = _database.GetCollection<MeetDetails>("meets");
            var filter2 = Builders<MeetDetails>.Filter.Eq(c => c.Id, p.Id);
            var updateDefinition1 = Builders<MeetDetails>.Update.Set(c => c.status, p.Status);
            var options2 = new FindOneAndUpdateOptions<MeetDetails>
            { ReturnDocument = ReturnDocument.After };
            MeetDetails updated1 = collection.FindOneAndUpdate(filter2, updateDefinition1, options2);
        }

       


    }
}
