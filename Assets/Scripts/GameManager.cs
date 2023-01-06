using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public enum GAMESTATE {RESUME,PAUSE};
    [Header("General Settings")]
    public GAMESTATE gameState = GAMESTATE.RESUME;

    public PlayerMovement playerMovement;
    public UIManager uiManager;
    public SoundManager soundManager;
    public CustomMouse customMouse;

   

    public SpriteRenderer anchorHat;
    public SpriteRenderer anchorPet;
    public SpriteRenderer anchorGlasses;

    [HideInInspector]
    public Item itemAnchorHat;
    [HideInInspector]
    public Item itemAnchorPet;
    [HideInInspector]
    public Item itemAnchorGlasses;

    public float delayClickTime = 0.5f;
    private bool onCooldown = false;


    private int _currency; 
    public int currency
    {
        get { return _currency; }
        set{            
            _currency = value;
            OnCurrencyChange();}
    }
    public UnityEvent OnEventCurrencyChange;

    
    
    [Header("Inventory Settings")]
    public List<Item> playerItems;

    [Header("MapCreation")]
    public GameObject player; 
    [SerializeField] private int x;
    [SerializeField] private int y;
    [SerializeField] private int cellsize;
    [SerializeField] private Vector3 origin;
    [SerializeField] public Pathfinding pathfinding;

    
    [HideInInspector]
    public bool EnableMovent;
    [Header("Miscalenous")]
    [SerializeField] private GameObject BlockRaycast;
    [SerializeField] private GameObject SquareSelector;
    
    

    void Start()
    {
        Cursor.visible = false;
        currency = 10000;
        CreateGrid();
    }
    
    private void Update()
    {
        switch (gameState)
        {
            case GAMESTATE.RESUME:
                {
                    SquareSelector.SetActive(true);
                    SquareSelectorMechanism(EnableMovent);
                    BlockRaycast.SetActive(false);
                    if(EnableMovent)
                        FindPath();
                }
                break;
            case GAMESTATE.PAUSE:
                {
                    
                    playerMovement.AbortMovement();
                    SquareSelector.SetActive(false);
                    BlockRaycast.SetActive(true);
                }
                break;
            default:
                break;
        }
        

    }
    #region GENERAL MECHANISM
    public void PauseGame()
    {
        gameState = GAMESTATE.PAUSE;
    }
    public void ResumeGame()
    {
        gameState = GAMESTATE.RESUME;
    }
    public void EnablePath(bool enable)
    {
        EnableMovent = enable;
    }
    public void SquareSelectorMechanism(bool show)
    {
        SquareSelector.SetActive(show);

        Vector3 MousePos = GetMouseWorldPosition();
        pathfinding.GetGrid().GetXY(MousePos, out int x, out int y);
        Vector3 FinalPos = new Vector3(x + 0.5f, y, 0) + origin;

        SquareSelector.transform.position = FinalPos;


    }

    #endregion
    #region MAP MECHANISM
    public void CreateGrid()
    {
        pathfinding = new Pathfinding(false, x, y, cellsize, origin);

    }
    public void FindPath()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!onCooldown)
            {
                onCooldown = true;
                Vector3 mouseWorldPosition = GetMouseWorldPosition();
                pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
                List<Vector3> PlayerPosition = pathfinding.FindPath(player.transform.position, mouseWorldPosition);
                if (PlayerPosition != null)
                {
                    //playerMovent.FixCurrentPos();
                    playerMovement.AbortMovement();
                    playerMovement.positions.AddRange(PlayerPosition);

                }
                soundManager.TriggerSound(1, 0.2f);
                StartCoroutine(Cooldown());
            }
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(delayClickTime);
        onCooldown = false;
    }
   
    public void DisableAtPosition(Vector3 DisableLocation)
    {
        pathfinding.GetGrid().GetXY(DisableLocation, out int x, out int y);
        pathfinding.GetNode(x, y).SetIsWalkable(false, !pathfinding.GetNode(x, y).isWalkable);
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    #endregion
    #region EQUIPEMENT MECHANISM
    public void updateClotherDir(int dir)
    {
        setClotherPosition(dir);
    }
    public void setClotherPosition(int dir)
    {
        switch (dir)
        {
            case 1:
                {
                    if (itemAnchorHat != null)
                        anchorHat.transform.localScale = itemAnchorHat.scaleRight;
                    if (itemAnchorPet != null)
                        anchorPet.transform.localScale = itemAnchorPet.scaleRight;
                    if (itemAnchorGlasses != null)
                        anchorGlasses.transform.localScale = itemAnchorGlasses.scaleRight;
                }
                break;
            case 2:
                {
                    if (itemAnchorHat != null)
                        anchorHat.transform.localScale = itemAnchorHat.scaleLeft;
                    if (itemAnchorPet != null)
                        anchorPet.transform.localScale = itemAnchorPet.scaleLeft;
                    if (itemAnchorGlasses != null)
                        anchorGlasses.transform.localScale = itemAnchorGlasses.scaleLeft;
                }
                break;
            case 3:
                {
                    if (itemAnchorHat != null)
                        anchorHat.transform.localScale = itemAnchorHat.scaleDown;
                    if (itemAnchorPet != null)
                        anchorPet.transform.localScale = itemAnchorPet.scaleDown;
                    if (itemAnchorGlasses != null)
                        anchorGlasses.transform.localScale = itemAnchorGlasses.scaleDown;
                }
                break;
            case 4:
                {
                    if (itemAnchorHat != null)
                        anchorHat.transform.localScale = itemAnchorHat.scaleTop;
                    if (itemAnchorPet != null)
                        anchorPet.transform.localScale = itemAnchorPet.scaleTop;
                    if (itemAnchorGlasses != null)
                        anchorGlasses.transform.localScale = itemAnchorGlasses.scaleTop;
                }
                break;
            default:
                break;
        }
    }
    public void PurchaseItem(int cost, Item item, out bool pass)
    {
        pass = CheckIfAbleToAffort(cost);

        if (CheckIfAbleToAffort(cost))
        {
            currency -= cost;
            Item itemCopy = Item.CreateInstance(item.GetType()) as Item;
            item.Copy(itemCopy);
            itemCopy.name = itemCopy._itemName + "_" + playerItems.Count;
            itemCopy._itemID = playerItems.Count;
            playerItems.Add(itemCopy);
        }


    }
    public void SellItem(int sellPrice, Item item)
    {
        currency += sellPrice;
        playerItems.Remove(item);
        playerItems.RemoveAll(Item => Item == null);
    }
    public bool CheckIfAbleToAffort(int cost)
    {
        return cost < currency;
    }
    public void OnCurrencyChange()
    {
        OnEventCurrencyChange.Invoke();
    }
    public void GetDressed(Item item)
    {
        switch (item._itemType)
        {
            case 0:
                {
                    if (itemAnchorPet != null)
                        GetUndressed(0);
                    anchorPet.sprite = item._itemSprite;
                    itemAnchorPet = item;
                    playerItems.Remove(item);
                    playerItems.RemoveAll(Item => Item == null);
                    updateClotherDir(playerMovement.lastDir);
                }
                break;
            case 1:
                {
                    if (itemAnchorGlasses != null)
                        GetUndressed(1);
                    anchorGlasses.sprite = item._itemSprite;
                    itemAnchorGlasses = item;
                    playerItems.Remove(item);
                    playerItems.RemoveAll(Item => Item == null);
                    updateClotherDir(playerMovement.lastDir);
                }
                break;
            case 2:
                {
                    if (itemAnchorHat != null)
                        GetUndressed(2);
                    anchorHat.sprite = item._itemSprite;
                    itemAnchorHat = item;
                    playerItems.Remove(item);
                    playerItems.RemoveAll(Item => Item == null);
                    updateClotherDir(playerMovement.lastDir);
                }
                break;
            default:
                break;
        }
    }
    public void GetUndressed(int type)
    {
        switch (type)
        {
            case 0:
                {
                    anchorPet.sprite = null;

                    Item itemCopy = Item.CreateInstance(itemAnchorPet.GetType()) as Item;
                    itemAnchorPet.Copy(itemCopy);
                    playerItems.Add(itemCopy);

                    itemAnchorPet = null;


                }
                break;
            case 1:
                {
                    anchorGlasses.sprite = null;

                    Item itemCopy = Item.CreateInstance(itemAnchorGlasses.GetType()) as Item;
                    itemAnchorGlasses.Copy(itemCopy);
                    playerItems.Add(itemCopy);

                    itemAnchorGlasses = null;
                }
                break;
            case 2:
                {
                    anchorHat.sprite = null;

                    Item itemCopy = Item.CreateInstance(itemAnchorHat.GetType()) as Item;
                    itemAnchorHat.Copy(itemCopy);
                    playerItems.Add(itemCopy);

                    itemAnchorHat = null;
                }
                break;
            default:
                break;
        }
    }
    #endregion








}
