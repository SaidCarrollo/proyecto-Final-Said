using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BeastTimes", menuName = "ScriptableObjects/TiemposData", order = 1)]
public class Beast3 : ScriptableObject
{
    public SimplyLinkedList<float> shortestTimesList = new SimplyLinkedList<float>();

}
