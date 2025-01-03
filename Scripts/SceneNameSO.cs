using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SceneNameSO", menuName = "Custom/SceneNameSO", order = 3)]

public class SceneNameSO : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField]
    public string sceneName;
}
