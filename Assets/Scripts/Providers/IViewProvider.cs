
namespace Providers
{
    public interface IViewProvider
    {
        void Initialize();
        T GetView<T>() where T : class;
    }
}