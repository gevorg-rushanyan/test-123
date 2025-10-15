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

        public int Level => _playerProgress.Level;
        public int Turns => _playerProgress.Turns;
        public int Matches => _playerProgress.Matches;
        public int Score => _playerProgress.Score;
        public IReadOnlyList<Vector2Int> MatchItems => _playerProgress.MatchItems;

        public void Initialize()
        {
            _playerProgress = LoadProgress();
        }
        
        public void AddMatchItems(List<Vector2Int> items)
        {
            _isModified = true;
            foreach (var item in items)
            {
                _playerProgress.MatchItems.Add(item);
            }
        }

        public void UpdateTurnsAndMatches(int turnsDelta, int matchesDelta, int scoreDelta)
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

            if (scoreDelta > 0)
            {
                _playerProgress.Score += scoreDelta;
                _isModified = true;
            }
        }

        public void LevelPassed()
        {
            _isModified = true;
            _playerProgress.Level += 1;
            ResetProgress();
        }

        public void ResetProgress()
        {
            _playerProgress.Score = 0;
            _playerProgress.Turns = 0;
            _playerProgress.Matches = 0;
            _playerProgress.MatchItems.Clear();
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