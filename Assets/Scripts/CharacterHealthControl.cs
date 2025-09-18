using Assets.Scripts.Managers;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CharacterHealthControl : MonoBehaviour
    {
        public static CharacterHealthControl Instance { get; private set; }
        private float _health = 100f;
        public float CurrentHealth => _health;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
        private void Start()
        {
            GameManager.Instance.EndGameAct += ResetCharacter;
        }

        private void OnDisable()
        {
            GameManager.Instance.EndGameAct -= ResetCharacter;
        }

        private void ResetCharacter(bool obj)
        {
            _health = 100f;
        }

        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0f, 100f);
                if (_health <= 0f)
                {
                    GameManager.Instance.EndGame(false);
                }
            }
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}