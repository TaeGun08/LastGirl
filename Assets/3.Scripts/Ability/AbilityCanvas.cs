using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCanvas : MonoBehaviour
{
    [Header("AbilityCanvas Settings")]
    [SerializeField] private GameObject storeUI;
    public GameObject StoreUI => storeUI;
    [SerializeField] private GameObject upgradeUI;
    public GameObject UpgradeUI => upgradeUI;
    [SerializeField] private GameObject inventoryUI;
    public GameObject InventoryUI => inventoryUI;
}
