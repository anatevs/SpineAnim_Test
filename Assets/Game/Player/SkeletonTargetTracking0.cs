using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

namespace GameCore
{
    public class SkeletonTargetTracking0 : MonoBehaviour
    {
        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;

        private Spine.AnimationState _state;

        private const string BODY_BONE_NAME = "body";

        private Bone _bodyBone;



        private void Awake()
        {
            Init();
        }

        [SerializeField]
        private float _angle;

        [SerializeField]
        private float _rotSpeed = 0.01f;

        [SerializeField]
        private bool _isRotate;

        [SerializeField]
        private bool _isPrintAngle;

        [SerializeField]
        private bool _isRotating;

        private void Update()
        {
            //if (_isRotate)
            //{
            //    _isRotate = false;
            //    RotateBone_CntrClck(_angle);
            //}

            //if (_isPrintAngle)
            //{
            //    _isPrintAngle = false;
            //    Debug.Log(_bodyBone.WorldRotationX);
            //}

            if (_isRotating)
            {

            }
        }

        private void Init()
        {
            _state = _skeletonAnimation.state;

            var bones = _skeletonAnimation.Skeleton.Bones;

            foreach (var bone in bones)
            {
                if (bone.Data.Name == BODY_BONE_NAME)
                {
                    _bodyBone = bone;

                    break;
                }
            }

            if (_bodyBone == null)
            {
                Debug.LogWarning($"no bone with name {BODY_BONE_NAME}");
            }

            //Debug.Log($"{_bodyBone.Rotation}");
        }

        private void RotateBone_CntrClck(float angle)
        {
            _bodyBone.Rotation += angle;

            
        }
    }
}