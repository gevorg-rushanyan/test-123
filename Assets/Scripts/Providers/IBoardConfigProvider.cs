using System.Collections.Generic;
using Board;
using Containers;

namespace Providers
{
    public interface IBoardConfigProvider
    {
        void Initialize();
        BoardData GetBoardConfig(int index);
        IReadOnlyList<MatchCombo> MatchComboConfig { get; }
    }
}