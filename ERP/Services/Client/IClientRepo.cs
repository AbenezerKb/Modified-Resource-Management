using ERP.Models;
using ERP.DTOs;


namespace ERP.Services
{
    public interface IClientRepo
    {

        bool SaveChanges();
        IEnumerable<Client> GetAllClient();
        Client GetClient(int id);
        Client CreateClient(ClientCreateDto client);
        void DeleteClient(int id);
        void UpdateClient(int id, ClientCreateDto client);
    }
}
