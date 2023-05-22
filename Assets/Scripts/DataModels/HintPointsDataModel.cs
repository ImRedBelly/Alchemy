using System;
using Newtonsoft.Json;

namespace DataModels
{
    public class HintPointsDataModel
    {
        public event Action<int> ChangeHintPoints;

        [JsonProperty] private int _hintPointsAmounth = 10;

        public int GetHintPoints() => _hintPointsAmounth;

        public void OnAppendHintPoints(int appendHintPoints)
        {
            _hintPointsAmounth += appendHintPoints;
            ChangeHintPoints?.Invoke(_hintPointsAmounth);
        }

        public void OnRemoveHintPoints(int appendHintPoints)
        {
            var newHintPoints = _hintPointsAmounth - appendHintPoints;
            if (0 <= newHintPoints)
            {
                _hintPointsAmounth = newHintPoints;
                ChangeHintPoints?.Invoke(_hintPointsAmounth);
            }
        }
    }
}