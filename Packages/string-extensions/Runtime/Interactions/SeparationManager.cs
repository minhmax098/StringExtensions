using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace viettel.StringExtensions
{
    public class SeparationManager : MonoBehaviour
    {
        private static SeparationManager instance;
        public static SeparationManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SeparationManager>();
                }
                return instance;
            }
        }
        private float r = 0.18f;
        private const float RADIUS = 8f;
        private const float DELTA_FACTOR = 1.7f;
        private const float DISTANCE_FACTOR = 1.5f;
        // variable
        private int childCount;
        private Vector3 centerPosition;
        private Vector3 centerPosCurrentObject;
        private Vector3 centerPosChildObject;
        private Vector3 targetPosition;
        private float angle;
        private float maxDegree = 360f;
        public Button btnSeparate;
        private bool isSeparating;
        private bool isDisplayLabel;
        public bool IsSeparating
        {
            get
            {
                return isSeparating;
            }
            set
            {
                isSeparating = value;
                btnSeparate.GetComponent<Image>().sprite = isSeparating ? Resources.Load<Sprite>(PathConfig.SEPARATE_CLICKED_IMAGE) : Resources.Load<Sprite>(PathConfig.SEPARATE_UNCLICK_IMAGE);
            }
        }
        public void HandleSeparate(bool isSeparating)
        {
            Debug.Log("Handle separate: ");
            IsSeparating = isSeparating;
            if (IsSeparating)
            {
                btnSeparate.interactable = false;
                SeparateOrganModel();
                btnSeparate.interactable = true;
            }
        }

        public void SeparateOrganModel()
        {
            int childCount = ObjectManager.Instance.CurrentObject.transform.childCount;
            if (childCount < 1)
            {
                return;
            }

            // reset origin object 
            ObjectManager.Instance.ResetStateOfOriginObject();
            Vector3 centerPosition = Vector3.zero;
            List<GameObject> childObjects = new List<GameObject>();
            List<Bounds> boundObjects = new List<Bounds>();

            foreach(Transform child in ObjectManager.Instance.CurrentObject.transform)
            {
                Renderer[] renderers =  child.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
                if (renderers.Length <= 0)
                {
                    renderers =  child.gameObject.GetComponentsInChildren<MeshRenderer>();
                }

                if (renderers.Length <= 0)
                {
                    continue;
                }
                Bounds bounds = renderers[0].bounds;
                for (var i = 1; i < renderers.Length; ++i)
                {
                    if (renderers[i].gameObject.tag == TagConfig.ORGAN_TAG)
                    {
                        bounds.Encapsulate(renderers[i].bounds);
                    }
                }
                childObjects.Add(child.gameObject);
                boundObjects.Add(bounds);

                centerPosition += bounds.center;
            }
            if (childObjects.Count > 0)
            {
                centerPosition /= childObjects.Count;
            }

            Bounds parentBound = Helper.CalculateBounds(ObjectManager.Instance.CurrentObject);
            float movingFactor = 1f;
            for(int i = 0; i < boundObjects.Count; i++)
            {
                Vector3 delta = boundObjects[i].center - centerPosition;
                movingFactor = Mathf.Min(parentBound.size.magnitude / boundObjects[i].size.magnitude, DELTA_FACTOR);
                delta = delta * RADIUS * movingFactor / delta.magnitude / ObjectManager.Instance.FactorScaleInitial;
                childObjects[i].transform.localPosition += delta;
            }
        }
    }
}
