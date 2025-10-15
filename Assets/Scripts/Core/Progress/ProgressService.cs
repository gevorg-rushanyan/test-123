using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Board;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Progress
{
    public class ProgressService : IProgressService
    {
        private const string FileName = "progress.json";
        private PlayerProgress _playerProgress;
        private bool _isModified;

        public int Level
        {
            get => _playerProgress.Level;
            set
            {
                _playerProgress.Level = value;
                _isModified = true;
            }
        }

        public int Score
        {
            get => _playerProgress.Score;
            set
            {
                _playerProgress.Score = value;
                _isModified = true;
            }
        }

        public int Turns => _playerProgress.Turns;
        public int Matches => _playerProgress.Matches;

        public int Columns
        {
            get => _playerProgress.Board.Columns;
            set
            {
                _playerProgress.Board.Columns = value;
                _isModified = true;
            }
        }

        public int Rows
        {
            get => _playerProgress.Board.Rows;
            set
            {
                _playerProgress.Board.Rows = value;
                _isModified = true;
            }
        }
        
        public IReadOnlyList<Vector2Int> MatchItems => _playerProgress.Board.MatchItems;

        public void Initialize()
        {
            _playerProgress = LoadProgress();
        }
        
        public void InitializeProgress(int columns, int rows)
        {
            _isModified = true;
            if (_playerProgress.Board == null)
            {
                _playerProgress.Board = new BoardProgress();
                return;
            }

            _playerProgress.Turns = 0;
            _playerProgress.Matches = 0;
            _playerProgress.Board.Columns = columns;
            _playerProgress.Board.Rows = rows;
            _playerProgress.Board.MatchItems.Clear();
        }
        
        public void AddMatchItems(List<Vector2Int> items)
        {
            _isModified = true;
            foreach (var item in items)
            {
                _playerProgress.Board.MatchItems.Add(item);
            }
        }

        public void UpdateTurnsAndMatches(int turnsDelta, int matchesDelta)
        {
            if (turnsDelta > 0)
            {
                _playerProgress.Turns += turnsDelta;
                _isModified = true;
            }

            if (matchesDelta > 0)
            {
                _playerProgress.Matches += matchesDelta;
                _isModified = true;
            }
        }

        public void SaveProgress()
        {
            if (_playerProgress == null || !_isModified)
            {
                return;
            }

            try
            {
                var json = JsonConvert.SerializeObject(_playerProgress);
                var path = Path.Combine(Application.persistentDataPath, FileName);
                File.WriteAllText(path, json, Encoding.UTF8);
                _isModified = false;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save progress: {e}");
            }
        }

        private PlayerProgress LoadProgress()
        {
            try
            {
                var path = Path.Combine(Application.persistentDataPath, FileName);
                Debug.Log($"Progress Path: {path}");
                if (!File.Exists(path))
                {
                    return new PlayerProgress();
                }

                var json = File.ReadAllText(path, Encoding.UTF8);
                if (string.IsNullOrEmpty(json))
                {
                    return new PlayerProgress();
                }

                var progress = JsonConvert.DeserializeObject<PlayerProgress>(json);
                return progress ?? new PlayerProgress();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load progress: {e}");
                return new PlayerProgress();
            }
        }
    }
}