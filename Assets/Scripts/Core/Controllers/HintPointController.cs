using Zenject;
using Services;
using Core.Views;
using UnityEngine;

namespace Core.Controllers
{
    public class HintPointController : MonoBehaviour
    {
        [SerializeField] private HintPointView _hintPointView;

        [Inject] private DataHelper _dataHelper;

        private void Start()
        {
            _dataHelper.HintPointsDataModel.ChangeHintPoints += UpdateViewHintPoint;
            UpdateViewHintPoint(_dataHelper.HintPointsDataModel.GetHintPoints());
        }

        private void OnDisable()
        {
            _dataHelper.HintPointsDataModel.ChangeHintPoints -= UpdateViewHintPoint;
        }

        private void UpdateViewHintPoint(int hintAmount)
        {
            _hintPointView.UpdateTextHintPoint(hintAmount.ToString());
        }
    }
}