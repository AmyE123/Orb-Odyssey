namespace CT6RIGPR
{
    using DG.Tweening;
    using UnityEngine;

    /// <summary>
    /// This manages the visuals for the inventory.
    /// </summary>
    public class InventoryVisualsManager : MonoBehaviour
    {
        public static InventoryVisualsManager instance;

        [SerializeField] private GameObject[] _inventorySlots;
        [SerializeField] private Transform _tubeArea;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Add the powerup to the slot.
        /// </summary>
        /// <param name="powerup"></param>
        public void AddPowerupToSlot(GameObject powerup)
        {
            foreach (GameObject slot in _inventorySlots)
            {
                if (slot.transform.childCount == 0)
                {
                    GameObject powerupVisual = Instantiate(powerup.GetComponent<PowerupObjectBase>().PowerupVisualPrefab, slot.transform.position, Quaternion.identity, slot.transform);
                    powerupVisual.transform.localScale = Vector3.one;
                    break;
                }
            }
        }

        /// <summary>
        /// Animate using the powerup pickup.
        /// </summary>
        /// <param name="slotIdx">The slot to get the powerup from.</param>
        public void UsePowerup(int slotIdx)
        {
            if (_inventorySlots[slotIdx].transform.childCount > 0)
            {
                Transform powerupVisual = _inventorySlots[slotIdx].transform.GetChild(0);

                powerupVisual.parent = _tubeArea;
                powerupVisual.localPosition = Vector3.zero;

                Sequence seq = DOTween.Sequence();

                seq.Append(powerupVisual.DOScale(Vector3.zero, 0.5f));
                seq.Join(powerupVisual.DOLocalMove(new Vector3(0.25f, -2, 0.25f), 4.0f));

                seq.OnComplete(() =>
                {
                    powerupVisual.parent = null;
                    Destroy(powerupVisual.gameObject);
                });
            }
        }
    }
}