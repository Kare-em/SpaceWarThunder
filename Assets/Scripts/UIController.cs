using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _gameOver;
    private int _score;

    private void Start()
    {
        Time.timeScale = 1f;
        _score = 0;
        UpdateScoreText();
        HealthController.OnDead += OnDead;
    }

    private void OnDestroy()
    {
        HealthController.OnDead -= OnDead;
    }

    private void OnDead(string obj)
    {
        switch (obj)
        {
            case "Enemy":
                EnemyDestroyed();
                break;
            case "Player":
                _gameOver.SetActive(true);
                StartCoroutine(Restart());
                break;
        }
    }

    private IEnumerator Restart()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void EnemyDestroyed()
    {
        _score++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = "Score: " + _score;
    }
}