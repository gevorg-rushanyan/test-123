using Board;

namespace Providers
{
    public interface IBoardConfigProvider
    {
        void Initialize();
        BoardData GetBoardConfig(int index);
    }
}