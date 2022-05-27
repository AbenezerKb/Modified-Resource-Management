using ERP.Models;
using ERP.Context;
using ERP.Exceptions;

namespace ERP.Services
{
    public class ClientRepo: IClientRepo
    {
        private readonly DataContext _context;

        public ClientRepo(DataContext context)
        {
            _context = context;

        }

        public void CreateClient(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException();
            }
           // client.clientId = Guid.NewGuid().ToString();

            //consultant. = DateTime.Now.Date;//DateTime.Now.ToString("yyyy-MM-dd");

            _context.Clients.Add(client);
        }



        public IEnumerable<Client> GetAllClient()
        {
            return _context.Clients.ToList();
        }


        public Client GetClient(int id)
        {
            return _context.Clients.FirstOrDefault(c => c.clientId == id);
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeleteClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.clientId == id);
            if (client == null)
                throw new ItemNotFoundException($"Client not found with client Id={id}");
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }



        public void UpdateClient(int id,Client updatedClient)
        {
            if (updatedClient == null)
            {
                throw new ArgumentNullException();
            }

            Client client = _context.Clients.FirstOrDefault(c => c.clientId == id);
            if (client == null)
                throw new ItemNotFoundException($"Client not found with client Id={id}");
            client.address = updatedClient.address;
            client.clientName = updatedClient.clientName;
            client.contractorId = updatedClient.contractorId;
            client.dateOfContract = updatedClient.dateOfContract;
            client.description = updatedClient.description;
            client.estimatedCost = updatedClient.estimatedCost;
            client.estimatedDuration = updatedClient.estimatedDuration;
            client.remarks = updatedClient.remarks;            

            _context.Clients.Update(client);
            _context.SaveChanges();
        }


    }
}
