using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    public enum OBJECTTYPE { PLAYERITEM ,STOREITEM}
    public OBJECTTYPE objectType;
    private UIManager uiManager;
    public Item item;


    public TextMeshProUGUI txtName;
    public Image iconImg;
    public TextMeshProUGUI txtCost;
    public Button btnBuySell;
    public bool isBuying;
    public bool enableHover;
    public GameObject storeSign;
    public GameObject equipmentSign;
    void Start()
    {
        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
        txtName.text = item._itemName;
        iconImg.sprite = item._itemSprite;

        btnBuySell.onClick.AddListener(GameManager.Instance.customMouse.SetClick);
        switch (objectType)
        {
            case OBJECTTYPE.PLAYERITEM:
                {
                    
                    btnBuySell.onClick.AddListener(BtnEquipItem);
                    equipmentSign.SetActive(true);
                    storeSign.SetActive(false);
                }
                break;
            case OBJECTTYPE.STOREITEM:
                {
                    equipmentSign.SetActive(false);
                    storeSign.SetActive(true);
                    if (isBuying)
                    {
                        btnBuySell.onClick.AddListener(BtnPurchaseItem);
                        txtCost.text = item._itemCost.ToString();
                    }
                    else
                    {
                        btnBuySell.onClick.AddListener(BtnSellItem);
                        txtCost.text = item._itemSellPrice.ToString();
                    }
                }
                break;
            default:
                break;
        }
        
        

        
    }
    public void BtnHover()
    {
        if(enableHover)
            uiManager.setCatClothes(item);
    }
    public void BtnPurchaseItem()
    {
        bool pass;

        GameManager.Instance.PurchaseItem(item._itemCost, item, out pass);

        if (!pass)
            uiManager.WarningMessage("not money");
        else
            GameManager.Instance.soundManager.TriggerSound(2, 0.5f);
       
        
    }
    public void BtnSellItem()
    {
        GameManager.Instance.SellItem(item._itemSellPrice, item);
        uiManager.RemoveEspecificItem(this.gameObject);
        GameManager.Instance.soundManager.TriggerSound(2, 0.5f);
    }
    public void BtnEquipItem()
    {
        GameManager.Instance.GetDressed(item);
        uiManager.updateEquipmentClothes();
        uiManager.setBackPack();
    }
    public void mouseHighlight(bool interac)
    {
        if(interac)
            GameManager.Instance.customMouse.SetImportant();
        else
            GameManager.Instance.customMouse.SetIdle();
        
    }
}
