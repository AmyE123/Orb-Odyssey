namespace CT6RIGPR
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// A class for hovering over buttons.
    /// </summary>
    public class HoverButtonSelectionEffect : MonoBehaviour
    {
        [SerializeField] private Image _selectionEffectImg;

        public void OnHoverEnter()
        {
            if (_selectionEffectImg != null)
            {
                _selectionEffectImg.enabled = true;
            }
        }

        public void OnHoverExit()
        {
            if (_selectionEffectImg != null)
            {
                _selectionEffectImg.enabled = false;
            }
        }
    }
}