using ERP.Models;


namespace ERP.Services
{
    public interface IClientRepo
    {

        bool SaveChanges();
        IEnumerable<Client> GetAllClient();
        Client GetClient(int id);
        void CreateClient(Client client);
        void DeleteClient(int id);
        void UpdateClient(int id,Client client);
    }
}
