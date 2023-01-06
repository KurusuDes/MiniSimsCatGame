using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update.
    [Header("General Settings")]
    public TextMeshProUGUI txtMoney;
    public Sprite EmptyImage;
    

    [Header("Bubble Dialog")]
    public GameObject bubbleDialog;
    public TextMeshPro bubbleText;
    public float bubbleShowTime;
    [Header("Large Dialog")]

    public GameObject panelDialogBox;
    public Image imgDialogBox;
    public TextMeshProUGUI txt_LargeBox;
    public float letterSetp;

    [Header("ItemManager")]
    public GameObject backpack;
    public GameObject backpackContainer;
    public List<GameObject> itemsBackpack;
    public Image equipmentAnchorPet;
    public Image equipmentAnchorHat;
    public Image equipmentAnchorGlasses;


    [Header("Store Manager")]
    public GameObject clothesStore;
    public GameObject _prefabStoreItem;
    public Image anchorPet;
    public Image anchorHat;
    public Image anchorGlasses;

    public GameObject ItemContainer;
    public List<GameObject> itemsContainer;

    public List<Item> itemsPet;
    public List<Item> itemsGlasses;
    public List<Item> itemsHat;


    
    void Start()
    {

        txtMoney.text = GameManager.Instance.currency.ToString();
    }
    public void SetLargeDialogBox(DialogText info)
    {
        if (!panelDialogBox.activeSelf)
        {
            imgDialogBox.sprite = info.icon;
            imgDialogBox.color = info.color;
            StartCoroutine(ShowMessage(info.randomDialogs[Random.Range(0, info.randomDialogs.Count - 1)], letterSetp));
        }

    }
    IEnumerator ShowMessage(string message, float letterDelay)
    {
        panelDialogBox.SetActive(true);
        txt_LargeBox.text = "";
        foreach (char letter in message.ToCharArray())
        {
            txt_LargeBox.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
        yield return new WaitForSeconds(letterDelay * 10);
        panelDialogBox.SetActive(false);
    }

    public void SetStoreItems(int code)
    {
        if(itemsContainer != null)
        {
            foreach (GameObject gameObject in itemsContainer)
            {
                Destroy(gameObject);
            }
            itemsContainer.Clear();
        }
        switch (code)
        {
            case 0:
                {
                    for (int i = 0; i < itemsPet.Count; i++)
                    {
                        GameObject item = Instantiate(_prefabStoreItem, ItemContainer.transform);
                        item.GetComponent<ItemUI>().objectType = ItemUI.OBJECTTYPE.STOREITEM;
                        item.GetComponent<ItemUI>().item = itemsPet[i];
                        item.GetComponent<ItemUI>().isBuying = true;
                        item.GetComponent<ItemUI>().enableHover = true;
                        item.name = "item " + i;
                        itemsContainer.Add(item);
                    }
                }
                break;
            case 1:
                {
                    for (int i = 0; i < itemsGlasses.Count; i++)
                    {
                        GameObject item = Instantiate(_prefabStoreItem, ItemContainer.transform);
                        item.GetComponent<ItemUI>().objectType = ItemUI.OBJECTTYPE.STOREITEM;
                        item.GetComponent<ItemUI>().item = itemsGlasses[i];
                        item.GetComponent<ItemUI>().isBuying = true;
                        item.GetComponent<ItemUI>().enableHover = true;
                        item.name = "item " + i;
                        itemsContainer.Add(item);
                    }
                }
                break;
            case 2:
                {
                    for (int i = 0; i < itemsHat.Count; i++)
                    {
                        GameObject item = Instantiate(_prefabStoreItem, ItemContainer.transform);
                        item.GetComponent<ItemUI>().objectType = ItemUI.OBJECTTYPE.STOREITEM;
                        item.GetComponent<ItemUI>().item = itemsHat[i];
                        item.GetComponent<ItemUI>().isBuying = true;
                        item.GetComponent<ItemUI>().enableHover = true;
                        item.name = "item " + i;
                        itemsContainer.Add(item);
                    }
                }
                break;
            case 3:
                {
                    if (GameManager.Instance.playerItems == null & GameManager.Instance.playerItems.Count == 0)
                        return;
                    for (int i = 0; i < GameManager.Instance.playerItems.Count; i++)
                    {
                        GameObject item = Instantiate(_prefabStoreItem, ItemContainer.transform);
                        item.GetComponent<ItemUI>().objectType = ItemUI.OBJECTTYPE.STOREITEM;
                        item.GetComponent<ItemUI>().item = GameManager.Instance.playerItems[i];
                        item.GetComponent<ItemUI>().isBuying = false;
                        item.GetComponent<ItemUI>().enableHover = true;
                        item.name = "item " + i;
                        
                        itemsContainer.Add(item);
                    }
                }
                break;
            default:
                break;
        }

    }
    public void RemoveEspecificItem(GameObject item)
    {
        Destroy(item);
        itemsContainer.Remove(item);
        itemsContainer.RemoveAll(gameObject => gameObject == null);
    }
   
    public void SetBubble(string txt)
    {
        bubbleDialog.SetActive(true);
        bubbleText.text = txt;
        Invoke("HideBubble", bubbleShowTime);
    }
    private void HideBubble()
    {
        bubbleDialog.SetActive(false);
        bubbleText.text = "...";
    }

    public void setCatClothes(Item item)
    {
        switch (item._itemType)
        {
            case 0:
                {
                    anchorPet.sprite = item._itemSprite;
                    Color color = anchorPet.color;
                    color.a = 255;
                    anchorPet.color = color;
                }
                break;
            case 1:
                {
                    anchorGlasses.sprite = item._itemSprite;
                    Color color = anchorGlasses.color;
                    color.a = 255;
                    anchorGlasses.color = color;
                }
                break;
            case 2:
                {
                    anchorHat.sprite = item._itemSprite;
                    Color color = anchorHat.color;
                    color.a = 255;
                    anchorHat.color = color;
                }
                break;
            default:
                break;
        }
    }

    public void setBackPack()
    {
        if (itemsBackpack != null)
        {
            foreach (GameObject gameObject in itemsBackpack)
            {
                Destroy(gameObject);
            }
            itemsBackpack.Clear();
        }
        for (int i = 0; i < GameManager.Instance.playerItems.Count; i++)
        {
            GameObject item = Instantiate(_prefabStoreItem, backpackContainer.transform);
            item.GetComponent<ItemUI>().objectType = ItemUI.OBJECTTYPE.PLAYERITEM;
            item.GetComponent<ItemUI>().item = GameManager.Instance.playerItems[i];     
            item.GetComponent<ItemUI>().enableHover = false;
            item.name = "item " + i;

            itemsBackpack.Add(item);
        }
    }
    public void updateEquipmentClothes()
    {
        if (GameManager.Instance.itemAnchorPet != null)
            equipmentAnchorPet.sprite = GameManager.Instance.itemAnchorPet._itemSprite;
        else
            equipmentAnchorPet.sprite = EmptyImage;       

        if (GameManager.Instance.itemAnchorHat != null)
            equipmentAnchorHat.sprite = GameManager.Instance.itemAnchorHat._itemSprite;
        else
            equipmentAnchorHat.sprite = EmptyImage;

        if (GameManager.Instance.itemAnchorGlasses != null)
            equipmentAnchorGlasses.sprite = GameManager.Instance.itemAnchorGlasses._itemSprite;
        else
            equipmentAnchorGlasses.sprite = EmptyImage;
    }

    public void setEmptyClothes()
    {
        Color color = Color.white;
        color.a = 0;

        anchorPet.sprite = null;
        anchorGlasses.sprite = null;
        anchorHat.sprite = null;

        anchorPet.color = color;
        anchorGlasses.color = color;
        anchorHat.color = color;
    }
    public void WarningMessage(string txt)
    {
        print(txt);
    }

    public void BtnOpenStore(bool isOpen)
    {
        clothesStore.SetActive(isOpen);
        GameManager.Instance.playerMovement.setMovement(!isOpen);
        SetStoreItems(0);
    }
    public void BtnOpenBackpack(bool isOpen)
    {
        backpack.SetActive(isOpen);
        GameManager.Instance.playerMovement.setMovement(!isOpen);
    }
    public void SetActualCurrency()
    {
        txtMoney.text = GameManager.Instance.currency.ToString();
    }
}
