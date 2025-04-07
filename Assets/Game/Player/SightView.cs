using UnityEngine;

namespace GameCore
{
    public sealed class SightView : MonoBehaviour
    {
        [SerializeField]
        private SightLine _sightLine;

        public Vector2 CalculateLine(Vector2 endPoint)
        {
            var lineVector = endPoint - (Vector2)transform.position;

            return lineVector;
        }

        public void SetupLine(Vector2 endPoint)
        {
            var lineVector = CalculateLine(endPoint);

            _sightLine.SetLineLocal(lineVector);
        }

        public void SetupLineDirection(Vector2 direction)
        {
            _sightLine.SetLineLocal(direction);
        }
    }
}