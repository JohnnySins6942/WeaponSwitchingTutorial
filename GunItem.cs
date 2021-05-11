using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Guns", menuName = "ScriptableObjects/gunManagerScriptableObjects", order = 1)]
public class GunItem : ScriptableObject
{
    public GameObject Prefab;
    public GameObject DroppedPrefab;
    public Sprite gunIcon;
    public new string name;
}
