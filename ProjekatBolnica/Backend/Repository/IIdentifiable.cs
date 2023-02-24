
namespace ProjekatBolnica.Backend.Repository
{
    public interface IIdentifiable<T>
    {
        T GetId();
        void SetId(T id);
    }
}
