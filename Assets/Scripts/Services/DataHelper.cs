using Zenject;
using DataModels;
using UnityEngine;
using Newtonsoft.Json;

namespace Services
{
    public class DataHelper : IInitializable
    {
        public ElementsDataModel ElementsDataModel { get; private set; }
        public HintPointsDataModel HintPointsDataModel { get; private set; }

        private string _keyElementsDataModel = "ElementsDataModel";
        private string _keyHintPointsDataModel = "HintPointsDataModel";

        public void Initialize() => Init();

        private void Init()
        {
            ElementsDataModel = JsonConvert.DeserializeObject<ElementsDataModel>(PlayerPrefs.GetString(_keyElementsDataModel));
            HintPointsDataModel = JsonConvert.DeserializeObject<HintPointsDataModel>(PlayerPrefs.GetString(_keyHintPointsDataModel));

            if (ElementsDataModel == null)
            {
                ElementsDataModel = new ElementsDataModel();
                SaveElementsDataModel();
            }

            if (HintPointsDataModel == null)
            {
                HintPointsDataModel = new HintPointsDataModel();
                SaveHintPointsDataModel();
            }
        }


        public void SaveElementsDataModel()
        {
            PlayerPrefs.SetString(_keyElementsDataModel, JsonConvert.SerializeObject(ElementsDataModel));
        }

        private void SaveHintPointsDataModel()
        {
            PlayerPrefs.SetString(_keyHintPointsDataModel, JsonConvert.SerializeObject(HintPointsDataModel));
        }
    }
}