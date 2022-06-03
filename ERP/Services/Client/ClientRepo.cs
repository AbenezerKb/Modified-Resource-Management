using ERP.Models;
using ERP.DTOs;
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

        public Client CreateClient(ClientCreateDto clientCreateDto)
        {
            if (clientCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            Client client = new Client();
            client.address = clientCreateDto.address;
            client.attachmentOfContract = clientCreateDto.attachmentOfContract;
            client.clientName = clientCreateDto.clientName;
            client.dateOfContract = clientCreateDto.dateOfContract;
            client.attachmentOfContract = clientCreateDto.attachmentOfContract;
            client.clientName = clientCreateDto.clientName;
            client.dateOfContract = clientCreateDto.dateOfContract;
            client.subContractorId = clientCreateDto.subContractorId;

            var subContractor = _context.SubContractors.FirstOrDefault(c => c.SubId == clientCreateDto.subContractorId);
            if (subContractor == null)
                throw new ItemNotFoundException($"SubContractor not found with Id={clientCreateDto.subContractorId}");



            client.subContractor = subContractor;           

            _context.Clients.Add(client);
            // client.clientId = Guid.NewGuid().ToString();

            //consultant. = DateTime.Now.Date;//DateTime.Now.ToString("yyyy-MM-dd");


            return client;
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



        public void UpdateClient(int id, ClientCreateDto clientCreateDto)
        {
            if (clientCreateDto == null)
            {
                throw new ArgumentNullException();
            }

            Client client = _context.Clients.FirstOrDefault(c => c.clientId == id);
            if (client == null)
                throw new ItemNotFoundException($"Client not found with client Id={id}");
            client.address = clientCreateDto.address;
            client.attachmentOfContract = clientCreateDto.attachmentOfContract;
            client.clientName = clientCreateDto.clientName;
            client.dateOfContract = clientCreateDto.dateOfContract;
            client.attachmentOfContract = clientCreateDto.attachmentOfContract;
            client.clientName = clientCreateDto.clientName;
            client.dateOfContract = clientCreateDto.dateOfContract;
            client.subContractorId = clientCreateDto.subContractorId;

            var subContractor = _context.SubContractors.FirstOrDefault(c => c.SubId == clientCreateDto.subContractorId);
            if (subContractor == null)
                throw new ItemNotFoundException($"SubContractor not found with Id={clientCreateDto.subContractorId}");



            client.subContractor = subContractor;

            _context.Clients.Update(client);
            _context.SaveChanges();
        }


    }
}
