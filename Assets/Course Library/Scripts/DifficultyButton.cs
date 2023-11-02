using UnityEngine;
using UnityEngine.UI;

namespace Course_Library.Scripts
{
    public class DifficultyButton : MonoBehaviour
    {

        [SerializeField] private int difficulty;
        private Button _button;
        private GameManager _gameManager;
    
        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SetDifficulty);
            _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        }

        void SetDifficulty()
        {
            _gameManager.StartGame(difficulty);
        }
    }
}
