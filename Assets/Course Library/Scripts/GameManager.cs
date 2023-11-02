using System.Collections;
using UnityEngine;
using TMPro;

namespace Course_Library.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] objects;
        [SerializeField] private TextMeshProUGUI scoreText;
        private int _score = 0;

        private float _spawnRate = 1f;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnObjectsCoroutine());
            UpdateScore(0);
        }
    
        IEnumerator SpawnObjectsCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnRate);
                int index = Random.Range(0, objects.Length);
                Instantiate(objects[index]);
                UpdateScore(5);
            }
        }

        private void UpdateScore(int addScoreValue)
        {
            _score += addScoreValue;
            scoreText.SetText("Score:" + _score);
        }
    }
}
