using Assets.Scripts.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState GameState;
        public Action<bool> EndGameAct;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
        }

        public void EndGame(bool isFinish)
        {
            EndGameAct?.Invoke(isFinish);
        }
    }

}
