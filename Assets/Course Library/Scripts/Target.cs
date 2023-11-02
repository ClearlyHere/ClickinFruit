using UnityEngine;

namespace Course_Library.Scripts
{
    public class Target : MonoBehaviour
    {
        private Rigidbody _targetRb;

        private const float MinSpeed = 12f;
        private const float MaxSpeed = 16f;
        private const float MaxTorque = 10f;
        private const float XRange = 4;
        private const float YSpawnPos = -4;
    
        // Start is called before the first frame update
        void Start()
        {
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
    }
}
