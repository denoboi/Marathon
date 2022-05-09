using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations;
using Dreamteck.Splines;
using System.IO;

namespace HCB.SplineMovementSystem.SplineEditor
{
#if UNITY_EDITOR
    public class SplineMovementSystemEditor : MonoBehaviour
    {
        private const string GROUND_LAYER_NAME = "Ground";
        private const string PLATFORM_LAYER_NAME = "Platform";
        private const float GROUND_CHECK_OFFSET = 0.1f;
        private const float COLLIDER_RADIUS = 0.25f;
        private const float COLLIDER_HEIGHT = 2f;

        private const int INITIAL_SPLINE_POINT_COUNT = 5;
        private const float SPLINE_POINT_OFFSET = -5f;
        private const float SPLINE_POINTS_GAP = 15f;
        private const int SPLINE_SAMPLE_RATE = 15;

        [MenuItem("Spline Movement System/Create a Spline Character")]
        public static void CreateSplineCharacter() 
        {
            GameObject character = new GameObject("Spline Character");
            character.transform.position = Vector3.zero;

            GameObject graphic = new GameObject("Graphic");
            graphic.transform.position = Vector3.zero;
            graphic.transform.SetParent(character.transform);

            GameObject armature = new GameObject("Armature");
            armature.transform.position = Vector3.zero;
            armature.transform.SetParent(graphic.transform);

            GameObject groundCheck = new GameObject("Ground Check");
            groundCheck.transform.position = new Vector3(0f, GROUND_CHECK_OFFSET, 0f);
            groundCheck.transform.SetParent(character.transform);
            SetupGroundCheck(groundCheck, graphic);

            character.AddComponent<SplineCharacter>();

            SplineCharacterMovementController splineMovementController = character.GetComponent<SplineCharacterMovementController>();
            splineMovementController.MovementData = CreateMovementData();

            Collider collider = SetupGraphic(graphic, armature);
            splineMovementController.MainCollider = collider;            
        }

        [MenuItem("Spline Movement System/Create a Spline")]
        public static void CreateSpline() 
        {
            GameObject spline = new GameObject("Spline");
            spline.transform.position = Vector3.zero;

            SetupSplineComputer(spline);
            SetupSplineMesh(spline);
        }

        private static void SetupSplineComputer(GameObject spline) 
        {
            SplineComputer splineComputer = spline.AddComponent<SplineComputer>();
            splineComputer.type = Spline.Type.BSpline;

            List<SplinePoint> splinePoints = new List<SplinePoint>();
            for (int i = 0; i < INITIAL_SPLINE_POINT_COUNT; i++)
            {
                SplinePoint splinePoint = new SplinePoint();
                Vector3 splinePosition = new Vector3(0f, 0f, SPLINE_POINT_OFFSET + i * SPLINE_POINTS_GAP);

                splinePoint.position = splinePosition;
                splinePoint.size = 1;
                splinePoints.Add(splinePoint);                
            }

            splineComputer.sampleRate = SPLINE_SAMPLE_RATE;
            splineComputer.SetPoints(splinePoints.ToArray());
            splineComputer.editorPathColor = Color.black;
            splineComputer.RebuildImmediate();
        }

        private static void SetupSplineMesh(GameObject spline) 
        {
            SplineMesh splineMesh = spline.AddComponent<SplineMesh>();
            SplineMesh.Channel channel = splineMesh.GetChannel(0);
            
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>("Packages/com.hyperboxgames.hcbsplinemovementsystem/RunTime/Models/Platform/SplineMeshModel.fbx");
            
            channel.AddMesh(mesh);            
            channel.autoCount = true;
            splineMesh.RebuildImmediate();

            spline.AddComponent<MeshCollider>();
            spline.gameObject.layer = LayerMask.NameToLayer(GROUND_LAYER_NAME);
        }

        private static void SetupGroundCheck(GameObject groundCheckGo, GameObject graphic) 
        {
            GroundCheck groundCheck = groundCheckGo.AddComponent<GroundCheck>();
            groundCheck.GroundLayer = LayerMask.GetMask(GROUND_LAYER_NAME);
            groundCheck.PlatformLayer = LayerMask.GetMask(PLATFORM_LAYER_NAME);

            ParentConstraint parentConstraint = groundCheckGo.AddComponent<ParentConstraint>();

            ConstraintSource constraintSource = new ConstraintSource();
            constraintSource.sourceTransform = graphic.transform;

            parentConstraint.AddSource(constraintSource);            
            parentConstraint.SetTranslationOffset(0, groundCheckGo.transform.position);
            parentConstraint.locked = true;
            parentConstraint.constraintActive = true;
        }
        private static Collider SetupGraphic(GameObject graphic, GameObject armature)
        {
            SplineCharacterClampController splineClampController = graphic.AddComponent<SplineCharacterClampController>();
            splineClampController.RotateBody = armature.transform;
            splineClampController.ClampData = CreateClampData();

            CapsuleCollider collider = graphic.AddComponent<CapsuleCollider>();
            collider.isTrigger = true;
            collider.center = Vector3.up * COLLIDER_HEIGHT / 2f;
            collider.radius = COLLIDER_RADIUS;            
            collider.height = COLLIDER_HEIGHT;
            return collider;
        }

        private static SplineMovementData CreateMovementData() 
        {
            string path = "Assets/Spline Movement System Data";
            if (!Directory.Exists(path))
            {
                string guid = AssetDatabase.CreateFolder("Assets", "Spline Movement System Data");
                path = AssetDatabase.GUIDToAssetPath(guid);
            }

            SplineMovementData movementData = ScriptableObject.CreateInstance<SplineMovementData>();
            string uniqueFileName = AssetDatabase.GenerateUniqueAssetPath(path + "/MovementData.asset");

            AssetDatabase.CreateAsset(movementData, uniqueFileName);
            AssetDatabase.SaveAssets();

            return movementData;
        }

        private static SplineClampData CreateClampData()
        {
            string path = "Assets/Spline Movement System Data";
            if (!Directory.Exists(path))
            {
                string guid = AssetDatabase.CreateFolder("Assets", "Spline Movement System Data");
                path = AssetDatabase.GUIDToAssetPath(guid);
            }

            SplineClampData clampData = ScriptableObject.CreateInstance<SplineClampData>();
            string uniqueFileName = AssetDatabase.GenerateUniqueAssetPath(path + "/ClampData.asset");

            AssetDatabase.CreateAsset(clampData, uniqueFileName);
            AssetDatabase.SaveAssets();

            return clampData;
        }
    }
#endif
}
