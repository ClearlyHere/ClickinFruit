using UnityEngine;
using Random = UnityEngine.Random;

namespace Course_Library.Scripts
{
    public class Target : MonoBehaviour
    {
        private Rigidbody _targetRb;
        private GameManager _gameManager;

        private const float MinSpeed = 9f;
        private const float MaxSpeed = 13f;
        private const float MaxTorque = 10f;
        private const float XRange = 4;
        private const float YSpawnPos = -1;
        private const int PointValue = 5;
        
        [SerializeField] private ParticleSystem particles;
    
        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
            _targetRb = GetComponent<Rigidbody>();
            transform.position = RandomPosition();
            _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
            _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        }
        
        Vector3 RandomForce()
        {
            return Vector3.up * Random.Range(MinSpeed, MaxSpeed);
        }

        float RandomTorque()
        {
            return Random.Range(-MaxTorque, MaxTorque);
        }

        Vector3 RandomPosition()
        {
            return new Vector3(Random.Range(-XRange, XRange), YSpawnPos);
        }

        private void OnMouseDown()
        {
            if (_gameManager.IsGameActive())
            {
                Destroy(gameObject);
                Instantiate(particles, transform.position, particles.transform.rotation);
                if (gameObject.CompareTag("Good"))
                {
                    _gameManager.UpdateScore(PointValue);
                }
                else
                {
                    _gameManager.LoseLife(1);
                    _gameManager.GameOver();
                }
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_gameManager.IsGameActive())
            {
                Destroy(gameObject);
                Instantiate(particles, transform.position, particles.transform.rotation);
                if (gameObject.CompareTag("Good") && other.gameObject.CompareTag("Player"))
                {
                    _gameManager.UpdateScore(PointValue);
                }
                else if (gameObject.CompareTag("Bad") && other.gameObject.CompareTag("Sensor"))
                {
                    _gameManager.GetDestroyAudio().Play();
                }
                else
                {
                    _gameManager.LoseLife(1);
                    _gameManager.GameOver();
                }
            }
        }
    }
}
