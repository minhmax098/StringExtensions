using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace viettel.StringExtensions
{
    public class LabelManager : MonoBehaviour
    {
        public static LabelManager instance;
        public static LabelManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<LabelManager>();
                }
                return null;
            }
        }
        
        private const float RADIUS = 18f;
        private const float RADIUS_IN_AR = 22f;
        private const float LONG_LINE_FACTOR = 6f;
        private string SEPARATE_LEVEL_SYMBOL = "-";
        // UI 
        public Button btnLabel;
        private bool isShowingLabel;
        public bool IsShowingLabel
        {
            get
            {
                return isShowingLabel;
            }
            set
            {
                isShowingLabel = value;
                btnLabel.GetComponent<Image>().sprite = isShowingLabel ? Resources.Load<Sprite>(PathConfig.LABEL_CLICKED_IMAGE) : Resources.Load<Sprite>(PathConfig.LABEL_UNCLICK_IMAGE);
            }
        }

        public void HandleLabelView(bool currentLabelStatus)
        {
            IsShowingLabel = currentLabelStatus;
            if (IsShowingLabel)
            {
                btnLabel.interactable = false;
                CreateLabel();
                btnLabel.interactable = true;
            }
            else
            {
                btnLabel.interactable = false;
                ClearLabel();
                btnLabel.interactable = true;
            }
        }

        public void CreateLabel()
        {
            ClearLabel();
            string levelObject = Helper.GetLevelObjectInLevelParent(ObjectManager.Instance.CurrentObject);

            int subLevel = 0;
            int lastIndex;
            GameObject subObject = null;
            Vector3 point;

            List<Label> labels = new List<Label>();
            List<GameObject> objects = new List<GameObject>();

            Vector3 centerLabelPosition = Vector3.zero;

            foreach(Label itemInfoLabel in StaticLesson.ListLabel)
            {
                if (itemInfoLabel.level == levelObject)
                {
                    if (itemInfoLabel.level == itemInfoLabel.subLevel)
                    {
                        subObject = ObjectManager.Instance.CurrentObject;
                    }
                    else
                    {
                        lastIndex = itemInfoLabel.subLevel.LastIndexOf(SEPARATE_LEVEL_SYMBOL, StringComparison.OrdinalIgnoreCase);
                        subLevel = Convert.ToInt32(itemInfoLabel.subLevel.Remove(0, lastIndex + 1));
                        subObject = ObjectManager.Instance.CurrentObject.transform.GetChild(subLevel).gameObject;
                    }
                    centerLabelPosition += subObject.transform.TransformPoint(new Vector3(itemInfoLabel.coordinates.x, itemInfoLabel.coordinates.y, itemInfoLabel.coordinates.z));
                    labels.Add(itemInfoLabel);
                    objects.Add(subObject);
                }
            }
        }

        public void ClearLabel()
        {
            foreach (LabelObjectInfo item in TagHandler.Instance.addedTags)
            {
                Destroy(item.point);
            }
            TagHandler.Instance.DeleteLabel();
        }
    }
}
