using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CVSLoader), typeof(SheetProcessor))]
public class GoogleSheetLoader : MonoBehaviour
{
    public event Action<CubesData> OnProcessData;

    [SerializeField] private string _sheetId;
    [SerializeField] private CubesData _data;

    private CVSLoader _cvsLoader;
    private SheetProcessor _sheetProcessor;
    public List<string> namesElements;

    private void Start()
    {
        _cvsLoader = GetComponent<CVSLoader>();
        _sheetProcessor = GetComponent<SheetProcessor>();
        DownloadTable();

        string[] assetGUIDs = AssetDatabase.FindAssets("", new[] {"Assets/Sprites/IconsElement"});

        foreach (var guid in assetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = System.IO.Path.GetFileName(assetPath);
            string result = fileName.ToLower().Replace(".png", "");
            namesElements.Add(result);
        }
    }


    [Button]
    private void CheckElementIcon()
    {
        string allElements = " ";
        int all = 1;
        foreach (var option in _data.CubeOptionsList)
        {
            bool isSame = false;
            foreach (var icon in namesElements)
            {
                if (icon == option.element)
                {
                    isSame = true;
                    break;
                }
            }

            if (!isSame)
            {
                allElements += $"   {all} : {option.element}";
                all++;
            }
        }

        Debug.LogError(allElements);
    }

    [Button]
    private void CheckChieldElementIcon()
    {
        Dictionary<string, int> allIcons = new Dictionary<string, int>();
        Dictionary<string, int> allElement = new Dictionary<string, int>();

        foreach (var iconName in namesElements)
        {
            if (!allIcons.ContainsKey(iconName))
                allIcons.Add(iconName, 1);
        }

        foreach (var option in _data.CubeOptionsList)
        {
            foreach (var childElement in option.childElements)
            {
                if (!allElement.ContainsKey(childElement))
                    allElement.Add(childElement, 1);
            }
        }


        int all = 1;
        string allElements = " ";

        foreach (var kv in allElement)
        {
            if (!allIcons.ContainsKey(kv.Key))
            {
                allElements += $"{all} : {kv.Key}     ";
                all++;
            }
        }

        Debug.LogError(allElements);
        Debug.LogError(all);
    }

    [Button]
    private void CreateParentElements()
    {
        foreach (var data in _data.CubeOptionsList)
        {
            foreach (var childElements in _data.CubeOptionsList)
            {
                foreach (var childElement in childElements.childElements)
                {
                    if (childElement == data.element && !data.parentElements.Contains(childElements.element))
                    {
                        data.parentElements.Add(childElements.element);
                    }
                }
            }
        }
    }

    private void DownloadTable()
    {
        _cvsLoader.DownloadTable(_sheetId, OnRawCVSLoaded);
    }

    private void OnRawCVSLoaded(string rawCVSText)
    {
        _data = _sheetProcessor.ProcessData(rawCVSText);
        OnProcessData?.Invoke(_data);
    }
}