using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;
    public static AnimationManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AnimationManager>();
            }
            return null;
        }
    }
}
