using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace viettel.StringExtensions
{
    public class ExperienceManager : MonoBehaviour
    {
        // private static ExperienceManager instance;
        // public static ExperienceManager Instance 
        // {
        //     get
        //     {
        //         if (instance == null)
        //         {
        //             instance = FindOjectOfType<ExperienceManager>();
        //         }
        //         return;
        //     }
        // }
        public Button btnHold;
        public Button btnLabel;
        public Button btnSeparate;
        public Button btnXray;
        public Button btnAnimation;

        void OnEnable()
        {
            // TouchManager.onSelectChildObject += OnSelectChildObject;
            // AnimationManager.onAnimationStateChanged += OnAnimationStateChanged;
        }

        void OnDisable()
        {
            // TouchManager.onSelectChildObject -= OnSelectChildObject;
            // AnimationManager.onAnimationStateChanged -= OnAnimationStateChanged;
        }

        void Start()
        {
            InitInteractions();
            InitEvents();
            ObjectManager.Instance.InitGameObject();
        }

        void Update()
        {
            EnableFeature();
        }

        void EnableFeature()
        {
            btnHold.interactable = (ObjectManager.Instance.CurrentObject != null && Helper.IsHaveChildObject(ObjectManager.Instance.CurrentObject));
            // btnLabel.interactable = (ObjectManager.Instance.CurrentObject != null &&
            //                             LabelManager.Instance.CheckAvailableLabel(ObjectManager.Instance.CurrentObject));
            btnSeparate.interactable = (ObjectManager.Instance.CurrentObject != null &&
                                        ObjectManager.Instance.CheckObjectHaveChild(ObjectManager.Instance.CurrentObject));
            btnXray.interactable = (ObjectManager.Instance.CurrentObject != null);
            // btnAnimation.interactable = (ObjectManager.Instance.CurrentObject != null && Helper.IsHaveAnimation(ObjectManager.Instance.CurrentObject));
        }

        void InitInteractions()
        {
            TouchManager.Instance.IsClickedHoldBtn = false;
            // LabelManager.Instance.IsShowingLabel = false;
            SeparationManager.Instance.IsSeparating = false;
            XrayManager.Instance.IsMakingXRay = false;
            // AnimationManager.Instance.IsShowingAnimation = false;
        }

        void InitEvents()
        {
            btnHold.onClick.AddListener(HandleHoldAction);
            btnLabel.onClick.AddListener(HandleLabelView);
            btnSeparate.onClick.AddListener(HandleSeparation);
            btnXray.onClick.AddListener(HandleXrayView);
            // btnAnimation.onClick.AddListener(HandleAnimation);
        }

        void HandleHoldAction()
        {
            TouchManager.Instance.IsClickedHoldBtn = !TouchManager.Instance.IsClickedHoldBtn;
        }

        void HandleLabelView()
        {
            // LabelManager.Instance.IsShowingLabel = !LabelManager.Instance.IsShowingLabel;
            // LabelManager.Instance.HandleLabelView(LabelManager.Instance.IsShowingLabel);
        }

        void HandleSeparation()
        {
            SeparationManager.Instance.IsSeparating = !SeparationManager.Instance.IsSeparating;
            SeparationManager.Instance.HandleSeparate(SeparationManager.Instance.IsSeparating);
        }

        void HandleXrayView()
        {
            XrayManager.Instance.IsMakingXRay = !XrayManager.Instance.IsMakingXRay;
            XrayManager.Instance.HandleXRayView(XrayManager.Instance.IsMakingXRay);
        }
        // void HandleAnimation()
        // {
        //     AnimationManager.Instance.IsShowingAnimation = !AnimationManager.Instance.IsShowingAnimation;
        // }
    }
}
