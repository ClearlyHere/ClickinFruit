using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Course_Library.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] objects;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI livesText;
        [SerializeField] private TextMeshProUGUI gameOverText;
        [SerializeField] private Button restartButton;
        [SerializeField] private GameObject titleScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private AudioSource destroyAudio;
        private GameObject _playerCursor;

        private int _score;
        private bool _isGameActive;
        private bool _isGamePaused;

        private int _lives = 3;
        private float _spawnRate = 2f;

        private PlayerInputs _playerInputs;
        private InputAction _pauseAction;

        public AudioSource GetDestroyAudio()
        {
            return destroyAudio;
        }

        private void Awake()
        {
            _playerInputs = new PlayerInputs();
            _playerCursor = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnEnable()
        {
            _pauseAction = _playerInputs.Player.Pause;
            _pauseAction.Enable();
            _pauseAction.performed += PauseGame;

        }

        private void OnDisable()
        {
            _pauseAction.Disable();
        }

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
            destroyAudio.Play();
            _score += addScoreValue;
            scoreText.SetText("Score:" + _score);
        }

        public void GameOver()
        {
            if (_lives <= 0)
            {
                _isGameActive = false;
                restartButton.gameObject.SetActive(true);
                gameOverText.gameObject.SetActive(true);
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
            scoreText.gameObject.SetActive(true);
            livesText.gameObject.SetActive(true);
            titleScreen.gameObject.SetActive(false);
            _isGameActive = true;
            _spawnRate /= difficulty;
            StartCoroutine(SpawnObjectsCoroutine());
            UpdateScore(0);
            LoseLife(0);
        }

        public void LoseLife(int amount)
        {
            destroyAudio.Play();
            _lives -= amount;
            string livesIndicators = "";
            for (int i = 0; i < _lives; i++)
                livesIndicators += "I";
            livesText.SetText("Lives:" + livesIndicators);
        }

        private void PauseGame(InputAction.CallbackContext context)
        {
            if (_isGameActive && !_isGamePaused)
            {
                pauseScreen.gameObject.SetActive(true);
                _playerCursor.SetActive(false);
                _isGamePaused = true;
                _isGameActive = false;
                Time.timeScale = 0;
            }
            else if (!_isGameActive && _isGamePaused)
            {
                pauseScreen.gameObject.SetActive(false);
                _playerCursor.SetActive(true);
                _isGamePaused = false;
                _isGameActive = true;
                Time.timeScale = 1;
            }
        }
    }
}