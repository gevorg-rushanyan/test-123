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

        public int Level
        {
            get => _playerProgress.Level;
            set => _playerProgress.Level = value;
        }

        public int Score
        {
            get => _playerProgress.Score;
            set => _playerProgress.Score = value;
        }

        public int Columns
        {
            get => _playerProgress.Board.Columns;
            set => _playerProgress.Board.Columns = value;
        }

        public int Rows
        {
            get => _playerProgress.Board.Rows; 
            set => _playerProgress.Board.Rows = value;
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
            foreach (var item in boardItems)
            {
                var key = KeyToString(item.Key);
                _playerProgress.Board.Items.Add(key, item.Value.Type);
            }
        }
        
        public void UpdateBoardData(int columns, int rows, Dictionary<Vector2Int, BoardItemData> board)
        {
            Dictionary<string, ItemType> boardData = new Dictionary<string, ItemType>();
            foreach (var item in board)
            {
                var key = KeyToString(item.Key);
                boardData.Add(key, item.Value.Type);
            }

            if (_playerProgress.Board == null)
            {
                _playerProgress.Board = new BoardProgress();
            }
            _playerProgress.Board.Columns = columns;
            _playerProgress.Board.Rows = rows;
            _playerProgress.Board.Items = boardData;
            
            SaveProgress();
        }

        private void SaveProgress()
        {
            if (_playerProgress == null)
            {
                return;
            }

            try
            {
                var json = JsonConvert.SerializeObject(_playerProgress);
                var path = Path.Combine(Application.persistentDataPath, FileName);
                File.WriteAllText(path, json, Encoding.UTF8);
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