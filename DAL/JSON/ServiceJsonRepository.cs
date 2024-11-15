using Core;
using System.Text.Json;

namespace DAL.JSON
{
    public class ServiceJsonRepository : IRepository<Service>
    {
        private const string _servicesSrc = "Data\\Services.json";

        public IReadOnlyCollection<Service> GetAll()
        {
            return (IReadOnlyCollection<Service>)GetServices();
        }

        public Service? GetById(int id)
        {
            var services = GetServices();
            return services.FirstOrDefault(s => s.Id == id);
        }

        public int GetCount()
        {
            return GetServices().Count();
        }

        public void Insert(Service service) 
        {
            var services = GetServices();
            services.Append(service);

            using var writer = new StreamWriter(_servicesSrc, false);
            JsonSerializer.Serialize(writer.BaseStream, services);
        }

        private static IEnumerable<Service> GetServices() 
        {
            if (!File.Exists(_servicesSrc))
            {
                using var writer = new StreamWriter(_servicesSrc);
                writer.WriteLine("[]");
            }

            using var reader = new StreamReader(_servicesSrc);
            var response = JsonSerializer.Deserialize<IEnumerable<Product>>(reader.BaseStream);

            return (IReadOnlyCollection<Service>)(response ?? []);
        }
    }
}
