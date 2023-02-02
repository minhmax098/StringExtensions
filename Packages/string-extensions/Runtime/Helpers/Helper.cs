using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace viettel.StringExtensions
{
    public static class Helper
    {
        public const float DETA_TIME_AVARAGRE = 0.0001f;
        const int BlockSize_16Bit = 2;
        public static float mZCoord = 1.3f;
        public static float MIN = 0.01f;
        public static List<Transform> listTransformLabel = new List<Transform>();
        public enum TYPE_RENDERER
        {
            MESH_RENDERER,
            SKINNED_MESH_RENDERER,
        }

        public static Bounds CalculateBounds(GameObject obj)
        {
            Renderer[] renderers =  obj.GetComponentsInChildren<SkinnedMeshRenderer>();
            if (renderers.Length <= 0)
            {
                renderers =  obj.GetComponentsInChildren<MeshRenderer>();
            }

            if (renderers.Length <= 0)
            {
                return new Bounds(Vector3.zero, Vector3.zero);
            }
            Bounds bounds = renderers[0].bounds;
            for (var i = 1; i < renderers.Length; ++i)
            {
                if (renderers[i].gameObject.tag == TagConfig.ORGAN_TAG)
                {
                    bounds.Encapsulate(renderers[i].bounds);
                }
            }
            return bounds;
        }

        public static Vector3 GetTouchPositionAsWorldPoint(Touch touch)
        {
            Vector3 touchPoint = touch.position;
            touchPoint.z = mZCoord;
            return Camera.main.ScreenToWorldPoint(touchPoint);
        }

        public static GameObject GetChildOrganOnTouchByTag(Vector3 position)
        {
            var cam = Camera.main;
            if (cam == null) return null;
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit[] raycastHits;
            raycastHits = Physics.RaycastAll(ray);
            float minDistance = Mathf.Infinity;
            GameObject selectedObject = null;
            foreach (RaycastHit hit in raycastHits)
            {
                if (hit.collider.transform.root.gameObject.tag == TagConfig.ORGAN_TAG)
                {
                    if (hit.distance < minDistance)
                    {
                        minDistance = hit.distance;
                        selectedObject = hit.collider.gameObject;
                    }
                }
            }
            if (selectedObject != null)
            {
                while (selectedObject.transform.parent != ObjectManager.Instance.CurrentObject.transform &&  selectedObject.transform.parent != null)
                {
                    selectedObject = selectedObject.transform.parent.gameObject;
                }
                return selectedObject;
            }
            else
            {
                return null;
            }
        }

        public static List<Vector3> GetListOfInitialPositionOfChildren(GameObject obj)
        {
            int childCount = obj.transform.childCount;

            List<Vector3> listchildrenOfOriginPosition = new List<Vector3>();

            for (int i = 0; i < childCount; i++)
            {
                if (obj.transform.GetChild(i).gameObject.tag == TagConfig.ORGAN_TAG)
                    listchildrenOfOriginPosition.Add(obj.transform.GetChild(i).localPosition);
            }
            
            return listchildrenOfOriginPosition;
        }

        public static bool IsHaveChildObject(GameObject obj)
        {
            int childCount = obj.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                if (obj.transform.GetChild(i).gameObject.tag != TagConfig.LABEL_TAG)
                {
                    return true;
                }
            }
            return false;
        }

        public static string GetLevelObjectInLevelParent(GameObject obj)
        {
            Transform currentObjectTransform = obj.transform;
            Transform rootTransform = currentObjectTransform.root;
            Transform parentTransform = null;
            string levelObject = StringConfig.levelParentObjectAppend;
            List<string> ListLevel = new List<string>();

            // Get List level object inside - outside, sub - root
            while (currentObjectTransform != rootTransform)
            {
                ListLevel.Add(currentObjectTransform.transform.GetSiblingIndex().ToString());
                parentTransform = currentObjectTransform.parent;
                currentObjectTransform = parentTransform;
            }

            // append string level invers level
            for (int i = ListLevel.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                    levelObject += ListLevel[i];
                else
                    levelObject += ListLevel[i] + StringConfig.letterAppend;
            }
            return levelObject != StringConfig.levelParentObjectAppend ? levelObject : StringConfig.levelParentObject;
        }
    }
}
