using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public sealed class ArrowsPool
    {
        private readonly Arrow _prefab;

        private readonly Transform _poolParent;

        private readonly Queue<Arrow> _arrows = new();

        public ArrowsPool(Arrow prefab, Transform poolParent)
        {
            _prefab = prefab;
            _poolParent = poolParent;
        }

        public Arrow Spawn(Transform parent)
        {
            if (!_arrows.TryDequeue(out var arrow))
            {
                arrow = GameObject.Instantiate(_prefab);
            }

            arrow.transform.SetParent(parent);

            arrow.gameObject.SetActive(true);

            arrow.OnAttackEnded += Unspawn;

            return arrow;
        }

        public void Unspawn(Arrow arrow)
        {
            arrow.OnAttackEnded -= Unspawn;

            arrow.gameObject.SetActive(false);

            arrow.transform.SetParent(_poolParent);

            _arrows.Enqueue(arrow);
        }
    }
}