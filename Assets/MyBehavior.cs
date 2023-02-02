using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using viettel.StringExtensions;

namespace viettel.StringExtensions
{
    public class MyBehavior : MonoBehaviour
    {
        void Update()
        {
            TouchManager.Instance.HandleTouchInteraction();
        }

        void Start()
        {
            Debug.Log("Test: ".Bold());
            Debug.Log("com.viettel.string-extensions: ".Italic());
        } 
    }
}
