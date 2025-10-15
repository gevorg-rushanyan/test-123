using System;
using System.Collections.Generic;
using Board;
using Core.Progress;
using Providers;
using UI;
using UI.MatchCards;
using UnityEngine;

namespace Core.GameStates
{
    public class MatchCardsState : IGameState
    {
        private readonly IProgressService _progressService;
        private readonly IUiManager _uiManager;
        private readonly IBoardConfigProvider _boardConfigProvider;
        private readonly ISpriteProvider _spriteProvider;
        private MatchCardsController _matchCardsController;
        private int _targetMatchCount;
        private int _maxTurnCount;
        
        public Action OnBackSelected;
        
        public MatchCardsState(
            IProgressService progressService,
            IUiManager uiManager,
            IBoardConfigProvider boardConfigProvider,
            ISpriteProvider spriteProvider)
        {
            _progressService = progressService;
            _uiManager = uiManager;
            _boardConfigProvider = boardConfigProvider;
            _spriteProvider = spriteProvider;
        }

        public void Start()
        {
            int level = _progressService.Level;
            // First play
            if (_progressService.Columns == 0 || _progressService.Rows == 0)
            {
                var data = _boardConfigProvider.GetBoardConfig(level);
                _progressService.InitializeProgress(data.Columns, data.Rows, data.GetMapping());
            }
            
            int columns = _progressService.Columns;
            int rows = _progressService.Rows;
            var items = _progressService.GetBoardItems();
            
            _matchCardsController = _uiManager.ShowView<MatchCardsController>(ViewType.MatchCards);
            _matchCardsController.BoardController.Initialize(columns, rows, items, _spriteProvider);
            _matchCardsController.BoardController.OnItemsMatch += OnMatched;
            _matchCardsController.BoardController.OnMatchFail += OnMatchFail;
            _matchCardsController.OnClickBack += OnClickBack;
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns);
        }

        private void OnMatched(List<Vector2Int> matchItems)
        {
            _progressService.UpdateBoardItemsType(matchItems, ItemType.None);
            _progressService.UpdateTurnsAndMatches(1, 1);
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns);
        }

        private void OnMatchFail()
        {
            _progressService.UpdateTurnsAndMatches(1, 0);
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns);
        }

        public void End()
        {
            _progressService.SaveProgress();
            _matchCardsController.OnClickBack -= OnClickBack;
            _uiManager.HideView(_matchCardsController);
        }

        private void OnClickBack()
        {
            OnBackSelected?.Invoke();
        }
    }
}