using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class Menu : MonoBehaviour
    {
        public static Menu Instance { get; private set; }

        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private Button _playButton;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            GameManager.Instance.EndGameAct += GameFail;
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(ClickPlayButton);

        }
        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(ClickPlayButton);
            GameManager.Instance.EndGameAct -= GameFail;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.GameState = GameManager.Instance.GameState == Enums.GameState.Paused ? Enums.GameState.Playing : Enums.GameState.Paused;
                PauseMenuSetActive();
            }
        }

        private void PauseMenuSetActive()
        {
            if (GameManager.Instance.GameState == Enums.GameState.Paused)
            {
                pauseMenuUI.SetActive(true);
            }
            else if (GameManager.Instance.GameState == Enums.GameState.Playing)
            {
                pauseMenuUI.SetActive(false);
            }
        }

        private void ClickPlayButton()
        {
            GameManager.Instance.GameState = Enums.GameState.Playing;
            PauseMenuSetActive();
        }

        public void GameFail(bool isWinner)
        {
            GameManager.Instance.GameState = Enums.GameState.Paused;
            PauseMenuSetActive();
        }
    }

}
