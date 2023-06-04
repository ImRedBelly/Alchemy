using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CubesData
{
    public List<CubeOptions> CubeOptionsList;

    public override string ToString()
    {
        string result = "";
        CubeOptionsList.ForEach(o =>
        {
            result += o.ToString();
        });
        return result;
    }
}
[System.Serializable]
public class CubeOptions
{
    public List<string> childElements;
    public List<string> parentElements = new List<string>();
    public string element;

    public override string ToString()
    {
        return $"childElements {childElements} \nElements {element}";
    }
}