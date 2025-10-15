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
            var boardData = _boardConfigProvider.GetBoardConfig(level);
            _maxTurnCount = boardData.MaxTurnCount;
            _targetMatchCount = boardData.TargetMatchCount;
            int columns = boardData.Columns;
            int rows = boardData.Rows;
            var boardItems = boardData.GetMapping();
            foreach (var key in _progressService.MatchItems)
            {
                if (boardItems.TryGetValue(key, out var item))
                {
                    item.Type = ItemType.None;
                    boardItems[key] = item;
                }
            }

            if (_matchCardsController == null)
            {
                _matchCardsController = _uiManager.ShowView<MatchCardsController>(ViewType.MatchCards);
                _matchCardsController.BoardController.OnItemsMatch += OnMatched;
                _matchCardsController.BoardController.OnMatchFail += OnMatchFail;
                _matchCardsController.OnClickBack += OnClickBack;
                _matchCardsController.OnWinOrLoseClick += OnWinOrLoseClick;
            }
            
            _matchCardsController.BoardController.Initialize(columns, rows, boardItems, _spriteProvider);
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns);
        }

        private void OnMatched(List<Vector2Int> matchItems)
        {
            _progressService.AddMatchItems(matchItems);
            _progressService.UpdateTurnsAndMatches(1, 1);
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns);
            if (_progressService.Matches >= _targetMatchCount)
            {
                _matchCardsController.ShowWinOrLoseView(true);
                _progressService.LevelPassed();
            }
        }

        private void OnMatchFail()
        {
            _progressService.UpdateTurnsAndMatches(1, 0);
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns);
            if (_progressService.Turns >= _maxTurnCount)
            {
                _matchCardsController.ShowWinOrLoseView(false);
                _progressService.ResetProgress();
            }
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

        private void OnWinOrLoseClick()
        {
            _matchCardsController.HideWinOrLoseView();
            Start();
        }
    }
}