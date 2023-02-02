using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
// using TMPro.TextMeshProUGUI;

namespace viettel.StringExtensions
{
    public class TagHandler : MonoBehaviour
    {
        private static TagHandler instance;
        public static TagHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TagHandler>();
                }
                return instance;
            }
        }
    
        Camera camera;
        RaycastHit depthCheck;

        Vector3 directionBetween;
        float distance;
        Vector3 point;
        public List<LabelObjectInfo> addedTags = new List<LabelObjectInfo>();

        void Update()
        {
            OnMoveLabel();
        }

        public void AddLabel(LabelObjectInfo labelObjectInfor)
        {
            addedTags.Add(labelObjectInfor);
        }

        public void DeleteLabel()
        {
            addedTags.Clear();
        }

        public void OnMoveLabel()
        {
            foreach (LabelObjectInfo item in addedTags)
            {
                if (item.point != null)
                {
                    DenoteLabel(item);
                    MoveLabel(item);
                }
            }
        }

        public static bool Is01(float a) 
        {
            return a > 0 && a < 1;
        }

        public void DenoteLabel(LabelObjectInfo labelObjectInfo)
        {
            point = labelObjectInfo.point.transform.position;
            camera = Camera.main;
            
            directionBetween = (point - camera.transform.position);
            directionBetween = directionBetween.normalized;
            distance = Vector3.Distance(camera.transform.position, point);

            if(Physics.Raycast(camera.transform.position, directionBetween, out depthCheck, distance)) {
                if (depthCheck.point != point && depthCheck.collider.gameObject.tag == TagConfig.ORGAN_TAG)
                {
                    labelObjectInfo.point.SetActive(false);
                }
                else
                {
                    labelObjectInfo.point.SetActive(true);
                }
            }
        
            if (labelObjectInfo.point.transform.GetChild(0).GetChild(labelObjectInfo.indexSideDisplay).position.x > labelObjectInfo.point.transform.position.x)
            {
                labelObjectInfo.indexSideDisplay = 1;      
                labelObjectInfo.point.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                labelObjectInfo.point.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = labelObjectInfo.labelName;
                labelObjectInfo.point.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);          
            }
            else
            {
                labelObjectInfo.indexSideDisplay = 0;
                labelObjectInfo.point.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                labelObjectInfo.point.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = labelObjectInfo.labelName;
                labelObjectInfo.point.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
        }

        public void MoveLabel(LabelObjectInfo labelObjectInfo)
        {
            labelObjectInfo.point.transform.GetChild(0).GetChild(labelObjectInfo.indexSideDisplay).transform.LookAt(
                    labelObjectInfo.point.transform.GetChild(0).GetChild(labelObjectInfo.indexSideDisplay).position + Camera.main.transform.rotation * Vector3.forward, 
                    Camera.main.transform.rotation * Vector3.up);
        }
    }
}
