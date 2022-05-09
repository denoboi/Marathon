using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HCB.SplineMovementSystem.Samples
{
    public class BlendShapeJumper : Jumper
    {
        public string BlendShapeID;
        public float BlendShapeEndValue;
        public SkinnedMeshRenderer SkinnedMeshRenderer;

        private const float DURATION = 1f;
        private const Ease EASE = Ease.OutBounce;

        protected override void OnTriggered()
        {
            base.OnTriggered();
            JumpTween();
        }

        private void JumpTween() 
        {
            int blendShapeIndex = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(BlendShapeID);
            float progressValue = SkinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);

            DOTween.Kill(transform);
            DOTween.To(() => progressValue, x => progressValue = x, BlendShapeEndValue, DURATION).SetEase(EASE).SetTarget(transform).OnUpdate(() =>
            {
                SkinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, progressValue);
            });
        }
    }
}
