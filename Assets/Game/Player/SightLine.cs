using UnityEngine;

namespace GameCore
{
    [RequireComponent(typeof(LineRenderer))]
    public sealed class SightLine : MonoBehaviour
    {
        private LineRenderer _line;

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }

        private void OnEnable()
        {
            _line.positionCount = 2;

            _line.SetPosition(0, Vector2.zero);
        }

        public void SetLineLocal(Vector2 position)
        {
            _line.SetPosition(1, position);
        }
    }
}