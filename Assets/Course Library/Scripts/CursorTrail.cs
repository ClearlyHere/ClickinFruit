using UnityEngine;

namespace Course_Library.Scripts
{
    public class CursorTrail : MonoBehaviour
    {

        private TrailRenderer _mouseTrail;

        private void Start()
        {
            _mouseTrail = GetComponent<TrailRenderer>();
            _mouseTrail.emitting = false;
        }
        // Update is called once per frame
        void Update()
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = pos;
        }

        private void OnMouseDown()
        {
            RenderTrail();
        }
        private void RenderTrail()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseTrail.emitting = true;
            }
            else _mouseTrail.emitting = false;
        }
    }
}
