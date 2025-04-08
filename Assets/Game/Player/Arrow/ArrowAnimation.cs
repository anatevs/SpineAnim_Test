using Spine;
using Spine.Unity;
using System;
using UnityEngine;

namespace GameCore
{
    public sealed class ArrowAnimation : MonoBehaviour
    {
        public event Action OnAttackEnded;

        [SerializeField]
        private SkeletonAnimation _animation;

        [SerializeField]
        private AnimationReferenceAsset _attack;

        [SerializeField]
        private AnimationReferenceAsset _idle;

        private Vector3 _idleScale;

        private Vector3 _defaultScale = Vector3.one;

        private void Awake()
        {
            _idleScale = _animation.transform.localScale;

            _defaultScale *= _idleScale.x;
        }

        public void SetAttack()
        {
            _animation.transform.localScale = _defaultScale;

            var trackEntry = _animation.AnimationState.SetAnimation(0, _attack, false);

            trackEntry.Complete += EndAttack;
        }

        private void EndAttack(TrackEntry trackEntry)
        {
            _animation.AnimationState.SetAnimation(0, _idle, true);

            _animation.transform.localScale = _idleScale;

            trackEntry.Complete -= EndAttack;

            OnAttackEnded?.Invoke();
        }
    }
}