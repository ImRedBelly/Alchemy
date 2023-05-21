using DataModels;
using UnityEngine;
using Zenject;

namespace Services
{
    public class DataHelper : IInitializable
    {
        public ElementsDataModel ElementsDataModel { get; private set; }

        private string _keyElementsDataModel = "ElementsDataModel";
        public void Initialize()
        {
            Init();
        }
        public void Init()
        {
            ElementsDataModel = JsonUtility.FromJson<ElementsDataModel>(PlayerPrefs.GetString(_keyElementsDataModel));
            if (ElementsDataModel == null)
            {
                ElementsDataModel = new ElementsDataModel();
                SaveElementsDataModel();
            }
        }

        public void SaveElementsDataModel()
        {
            PlayerPrefs.SetString(_keyElementsDataModel, JsonUtility.ToJson(ElementsDataModel));
        }

     
    }
}