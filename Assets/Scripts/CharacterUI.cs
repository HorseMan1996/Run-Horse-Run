using Assets.Scripts;
using Assets.Scripts.Managers;
using TMPro;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _scoreText;

    private void Update()
    {
        _scoreText.text = "Score: " + ScoreManager.Instance.CurrentScore.ToString("0");
        _healthText.text = "Health: " + CharacterHealthControl.Instance.CurrentHealth.ToString("0");
        _highScoreText.text = "High Score: " + ScoreManager.Instance.HighScore.ToString("0");
    }
}
