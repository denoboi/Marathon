using Sirenix.OdinInspector;
using UnityEngine;
using Dreamteck.Splines;

namespace HCB.SplineMovementSystem
{
    public class SplineItem : MonoBehaviour
    {
        public SplineComputer SplineComputer;

        [Space(10)]

        [Header("Cache")]        
        [Tooltip("ReadOnly for caching purposes")]
        [SerializeField]
        [ReadOnly]
        private Vector3 _splineOffset;
        public Vector3 SplineOffset { get { return _splineOffset; } private set { _splineOffset = value; } }
       
        [Tooltip("ReadOnly for caching purposes")]
        [SerializeField]
        [ReadOnly]
        private double _splinePercent;
        public double SplinePercent { get { return _splinePercent; } private set { _splinePercent = value; } }        

        public void Initialize(Vector2 offset, float distanceFromStart, SplineComputer spline)
        {
            SplineOffset = offset;
            SplineComputer = spline;

            double percent = SplineComputer.Travel(0, distanceFromStart);            
            SetPercent(percent);            
        }

        public void SetOffset()
        {
            double percent = SplinePercent;
            Vector3 splinePosition = SplineComputer.EvaluatePosition(percent);

            Vector2 offset = new Vector2(transform.position.x - splinePosition.x, transform.position.y - splinePosition.y);
            SplineOffset = offset;

            SetPercent(GetPercent(transform.position));
        }

        public void Travel(float distance, bool isForward)
        {            
            Spline.Direction splineDirection = isForward ? Spline.Direction.Forward : Spline.Direction.Backward;
            double percent = SplineComputer.Travel(SplinePercent, distance, splineDirection);            
            SetPercent(percent);
        }

        public double GetPercent(Vector3 position)
        {
            double percent = SplineComputer.Project(position - SplineOffset).percent;
            return percent;
        }

        private void SetPercent(double percent)
        {
            SplinePercent = Mathf.Clamp((float)percent, 0f, 1f);
            SplineSample splineSample = SplineComputer.Evaluate(SplinePercent);
            transform.SetPositionAndRotation(splineSample.position + SplineOffset, splineSample.rotation);
        }

        private void Setup()
        {
            if (SplineComputer == null)
                return;

            SetOffset();

            double percent = SplineComputer.Project(transform.position).percent;
            SetPercent(percent);
        }

        private void SetSpline()
        {
            SplineComputer spline = FindObjectOfType<SplineComputer>();
            if (spline != null)
            {
                SplineComputer = spline;
            }
        }

        private void Reset()
        {
            SetSpline();
            Setup();
        }
    }
}
