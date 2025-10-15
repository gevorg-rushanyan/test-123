using System;
using System.Collections.Generic;
using Board;
using Containers;
using Core.Progress;
using Core.Sound;
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
        private readonly ISoundSystem _soundSystem;
        private MatchCardsController _matchCardsController;
        private int _targetMatchCount;
        private int _maxTurnCount;
        private IReadOnlyList<MatchCombo> _matchCombos;
        private int _continuesMatchCount;
        private int _scoreMultiplier;
        
        public Action OnBackSelected;
        
        public MatchCardsState(
            IProgressService progressService,
            IUiManager uiManager,
            IBoardConfigProvider boardConfigProvider,
            ISpriteProvider spriteProvider,
            ISoundSystem soundSystem)
        {
            _progressService = progressService;
            _uiManager = uiManager;
            _boardConfigProvider = boardConfigProvider;
            _spriteProvider = spriteProvider;
            _soundSystem = soundSystem;
        }

        public void Start()
        {
            int level = _progressService.Level;
            var boardData = _boardConfigProvider.GetBoardConfig(level);
            _matchCombos = _boardConfigProvider.MatchComboConfig;
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
                _matchCardsController.BoardController.OnItemClicked += OnItemClicked;
                _matchCardsController.OnClickBack += OnClickBack;
                _matchCardsController.OnWinOrLoseClick += OnWinOrLoseClick;
            }
            
            _scoreMultiplier = GetScoreMultiplier(_continuesMatchCount);
            _matchCardsController.BoardController.Initialize(columns, rows, boardItems, _spriteProvider);
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns, _progressService.Score, _scoreMultiplier);
        }

        private void OnMatched(List<PositionInt> matchItems)
        {
            ++_continuesMatchCount;
            _scoreMultiplier = GetScoreMultiplier(_continuesMatchCount);
            _soundSystem.PlaySound(SoundType.Match);
            _progressService.AddMatchItems(matchItems);
            _progressService.UpdateTurnsAndMatches(1, 1, 1 * _scoreMultiplier);
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns, _progressService.Score, _scoreMultiplier);
            if (_progressService.Matches >= _targetMatchCount)
            {
                _matchCardsController.ShowWinOrLoseView(true);
                _progressService.LevelPassed();
                _soundSystem.PlaySound(SoundType.Win);
            }
        }

        private void OnMatchFail()
        {
            _continuesMatchCount = 0;
            _scoreMultiplier = GetScoreMultiplier(_continuesMatchCount);
            _progressService.UpdateTurnsAndMatches(1, 0, 0);
            _matchCardsController.UpdateProgress(_progressService.Matches, _progressService.Turns, _progressService.Score, _scoreMultiplier);
            if (_progressService.Turns >= _maxTurnCount)
            {
                _matchCardsController.ShowWinOrLoseView(false);
                _progressService.ResetProgress();
                _soundSystem.PlaySound(SoundType.Lose);
            }
        }

        private void OnItemClicked()
        {
            _soundSystem.PlaySound(SoundType.Flip);
        }

        public void End()
        {
            _soundSystem.StopAllSounds();
            _progressService.SaveProgress();
            _matchCardsController.OnClickBack -= OnClickBack;
            _uiManager.HideView(_matchCardsController);
        }

        private void OnClickBack()
        {
            OnBackSelected?.Invoke();
            _soundSystem.StopAllSounds();
        }

        private void OnWinOrLoseClick()
        {
            _continuesMatchCount = 0;
            _soundSystem.StopAllSounds();
            _matchCardsController.HideWinOrLoseView();
            Start();
        }

        private int GetScoreMultiplier(int continuesMatchCount)
        {
            int multiplier = 1;

            foreach (var combo in _matchCombos)
            {
                if (continuesMatchCount >= combo.MatchCount)
                {
                    multiplier = combo.Multiplier;
                }
            }
            
            return multiplier;
        }
    }
}