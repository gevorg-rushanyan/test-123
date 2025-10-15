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
        private const char KeySeparator = '-';
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
        
        public void Initialize()
        {
            _playerProgress = LoadProgress();
        }
        
        public IReadOnlyDictionary<Vector2Int, BoardItemData> GetBoardItems()
        {
            Dictionary<Vector2Int, BoardItemData> items = new Dictionary<Vector2Int, BoardItemData>();
            
            foreach (var item in _playerProgress.Board.Items)
            {
                Vector2Int key = KeyFromString(item.Key);
                items.Add(key, new BoardItemData { Position = key, Type = item.Value});
            }
            
            return items;
        }

        public void SetBoardItems(Dictionary<Vector2Int, BoardItemData> boardItems)
        {
            _isModified = true;
            _playerProgress.Board.Items.Clear();
            foreach (var item in boardItems)
            {
                var key = KeyToString(item.Key);
                _playerProgress.Board.Items.Add(key, item.Value.Type);
            }
        }
        
        public void UpdateBoardItemsType(List<Vector2Int> items, ItemType targetType)
        {
            _isModified = true;
            foreach (var item in items)
            {
                var key = KeyToString(item);
                if (_playerProgress.Board.Items.ContainsKey(key))
                {
                    _playerProgress.Board.Items[key] = targetType;
                }
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

        public void InitializeProgress(int columns, int rows, Dictionary<Vector2Int, BoardItemData> board)
        {
            if (_playerProgress.Board == null)
            {
                _playerProgress.Board = new BoardProgress();
            }

            _playerProgress.Turns = 0;
            _playerProgress.Matches = 0;
            _playerProgress.Board.Columns = columns;
            _playerProgress.Board.Rows = rows;
            
            SetBoardItems(board);
            _isModified = true;
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

        private string KeyToString(Vector2Int key)
        {
            return $"{key.x}{KeySeparator}{key.y}";
        }

        private Vector2Int KeyFromString(string key)
        {
            var result = key.Split(KeySeparator);
            if (result.Length != 2)
            {
                Debug.LogError($"ProgressService Can't parse key {key}");
                return Vector2Int.zero;
            }
            return new Vector2Int(int.Parse(result[0]), int.Parse(result[1]));
        }
    }
}