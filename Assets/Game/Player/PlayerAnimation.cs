using Spine;
using Spine.Unity;
using UnityEngine;

namespace GameCore
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation _animation;

        [SerializeField]
        private SkeletonUtility _skeletonUtility;

        [SerializeField]
        private AnimationReferenceAsset _attackFinish;

        [SerializeField]
        private AnimationReferenceAsset _attackTarget;

        [SerializeField]
        private AnimationReferenceAsset _idle;

        public void SetSighting(bool isSighting)
        {
            _skeletonUtility.enabled = isSighting;

            if (isSighting)
            {
                _animation.AnimationState.SetAnimation(0, _attackTarget, true);
            }
            else
            {
                _animation.AnimationState.SetAnimation(0, _idle, true);
            }
        }

        public void Shoot()
        {
            SetSighting(false);

            var trackEntry = _animation.AnimationState.SetAnimation(0, _attackFinish, false);

            trackEntry.Complete += OnShootingFinished;
        }

        private void OnShootingFinished(TrackEntry trackEntry)
        {
            trackEntry.End -= OnShootingFinished;

            _animation.AnimationState.SetAnimation(0, _idle, true);
        }
    }
}