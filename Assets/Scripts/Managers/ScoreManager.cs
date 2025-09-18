using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public float CurrentScore => _currentScore;
        public static ScoreManager Instance;
        private float _highScore;
        public float HighScore => _highScore;
        private float _currentScore;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }



        private void Start()
        {
            _highScore = PlayerPrefs.GetFloat("HighScore", 0);
            GameManager.Instance.EndGameAct += ResetCharacter;
        }
        
        private void OnDisable()
        {
            GameManager.Instance.EndGameAct -= ResetCharacter;
        }

        private void ResetCharacter(bool obj)
        {
            SetHighScore(_currentScore);
            _currentScore = 0;
        }

        private void Update()
        {
            _currentScore = CharacterControl.Instance.transform.transform.position.x;
        }

        private void SetHighScore(float currentScore)
        {
            if (currentScore > _highScore)
            {
                _highScore = currentScore;
                PlayerPrefs.SetFloat("HighScore", _highScore);
            }
        }
    }
}