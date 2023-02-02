using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace viettel.StringExtensions
{
    public class ObjectManager : MonoBehaviour
    {
        private static ObjectManager instance;
        public static ObjectManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<ObjectManager>();
                }
                return instance;
            }
        }

        public const float X_SIZE_BOUND = 0.46f;
        public const float Y_SIZE_BOUND = 0.46f;
        public const float Z_SIZE_BOUND = 0.46f;
        public float FactorScaleInitial { get; set; }
        Bounds boundOriginObject;

        private const float TIME_SCALE_FOR_APPEARANCE = 0.04f;
        public static event Action onReadyModel;
        public static event Action<string> onChangeCurrentObject;
        public Material OriginOrganMaterial { get; set; }
        public GameObject OriginObject;
        public List<Vector3> ListchildrenOfOriginPosition = new List<Vector3>();
        public GameObject CurrentObject { get; set; }
        public Vector3 OriginPosition { get; set; }
        public Quaternion OriginRotation { get; set; }
        public Vector3 OriginScale { get; set; }
        public Vector3 OriginScaleLabel { get; set; }
        public Vector3 DeltaDistance { get; set; }

        void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            InitGameObject();
        }
        
        void Update()
        {

        }

        public void SetCollider(GameObject objectInstance)
        {
            objectInstance.tag = TagConfig.ORGAN_TAG;
            var transforms = objectInstance.GetComponentsInChildren<Transform>();
            if (transforms.Length <= 0)
            {
                return;
            }

            foreach (var item in transforms)
            {
                if (item.gameObject.GetComponent<MeshCollider>() == null)
                {
                    item.gameObject.AddComponent<MeshCollider>();
                }
                item.tag = TagConfig.ORGAN;
            }
        }

        public void InitGameObject()
        {
            OriginObject.transform.position = ModelConfig.CENTER_POSITION;
            OriginObject.transform.rotation = Quaternion.Euler(Vector3.zero);
            OriginObject.name = "Cube";
            SetCollider(OriginObject);
        
            // ScaleObjectWithBound(OriginObject);
            // XrayManager.Instance.DictionaryMaterialOriginal.Clear();
            // XrayManager.Instance.GetOriginalMaterial(OriginObject);
            OriginPosition = OriginObject.transform.position;
            OriginRotation = OriginObject.transform.rotation;
            OriginScale = OriginObject.transform.localScale;
            OriginObject.SetActive(true);

            // OriginScaleLabel = ExperienceConfig.ScaleOriginPointLabelMediumObject / FactorScaleInitial;
            ChangeCurrentObject(OriginObject);
            // onReadyModel?.Invoke();
            Debug.Log("Init GameObject successfully !");
        }

        public void ScaleObjectWithBound(GameObject objectInstance)
        {
            boundOriginObject = Helper.CalculateBounds(objectInstance);
            FactorScaleInitial = Mathf.Min(Mathf.Min(X_SIZE_BOUND / boundOriginObject.size.x, Y_SIZE_BOUND / boundOriginObject.size.y), Z_SIZE_BOUND / boundOriginObject.size.z);
            objectInstance.transform.localScale = objectInstance.transform.localScale * FactorScaleInitial;
            DeltaDistance = objectInstance.transform.position - boundOriginObject.center;
            foreach (Transform child in objectInstance.transform)
            {
                child.transform.localPosition += DeltaDistance;
            }
        }
        public void ChangeCurrentObject(GameObject newGameObject)
        {
            try
            {
                CurrentObject = newGameObject;
                ListchildrenOfOriginPosition = Helper.GetListOfInitialPositionOfChildren(CurrentObject);
                // onChangeCurrentObject?.Invoke(CurrentObject.name);
            }
            catch (Exception e)
            {
                Debug.Log($"change error {e.Message}");
            }
        }

        public void ResetStateOfOriginObject()
        {
            OriginObject.transform.rotation = OriginRotation;
            OriginObject.transform.localScale = OriginScale;
        }

        public bool CheckObjectHaveChild(GameObject obj)
        {
            if (obj.transform.childCount < 2)
                return false;
            else
            {
                int numberChildCount = 0;
                foreach(Transform child in obj.transform)
                {
                    if (child.gameObject.tag == TagConfig.ORGAN_TAG)
                    {
                        numberChildCount++;
                        if (numberChildCount >= 2)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
