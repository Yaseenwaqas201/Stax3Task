using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OutFitContainer", menuName = "Scriptable_Objects/OutFitContainer", order = 1)]
public class OutFitSC_Container: ScriptableObject
{
    public List<OutFitSC> OutFitList=new List<OutFitSC>();
}


