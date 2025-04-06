using System;
using UnityEngine;

namespace GameCore
{
    public sealed class SightView : MonoBehaviour
    {
        public event Action<Vector2> OnDrawLine;

        [SerializeField]
        private SightLine _sightLine;

        public void SetupLine(Vector2 endPoint)
        {
            var lineVector = endPoint - (Vector2)transform.position;

            _sightLine.SetLineLocal(lineVector);

            OnDrawLine?.Invoke(lineVector);
        }
    }
}