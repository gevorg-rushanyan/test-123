using Board;

namespace Providers
{
    public interface IBoardDataProvider
    {
        void Initialize();
        BoardData GetBoardData(int index);
    }
}