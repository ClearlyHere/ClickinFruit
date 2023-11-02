using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

namespace Course_Library.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] objects;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button restartButton;

        [SerializeField] private GameObject titleScreen;
        
        private int _score;
        private bool _isGameActive;

        private float _spawnRate = 2f;
    
        IEnumerator SpawnObjectsCoroutine()
        {
            while (_isGameActive)
            {
                yield return new WaitForSeconds(_spawnRate);
                int index = Random.Range(0, objects.Length);
                Instantiate(objects[index]);
            }
        }

        public void UpdateScore(int addScoreValue)
        {
            _score += addScoreValue;
            scoreText.SetText("Score:" + _score);
        }

        public void GameOver()
        {
            _isGameActive = false;
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
            if (!other.CompareTag("Bad"))
            {
                GameOver();
            }
        }

        public bool IsGameActive()
        {
            return _isGameActive;
        }
        
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void StartGame(int difficulty)
        {
            titleScreen.gameObject.SetActive(false);
            _isGameActive = true;
            _spawnRate /= difficulty;
            StartCoroutine(SpawnObjectsCoroutine());
            UpdateScore(0);
        }
    }
}
