[Serializable]
public class Vendor : Monobehaviour
{
    #region Fields
    bool isInConversation = false;
    MyItem[] buyItems;
    MyItem[] sellItems;
    int[] storeItemIdsList;
    static ItemDatabase database;
    [SerializeField] GameObject shopWindow;
    #endregion Fields

    #region Queries
    public bool IsInConversation { get{ return isInConvo; } set{ isInConvo  = value; } };
    public MyItem[] GetBuyItemsList { get{ return buyItems;} }
    public MyItem[] GetSellItemsList { get{ return sellItems;} }
    #endregion Queries

    #region Unity Methods
    void OnEnable()
    {
        if(shopwindow == null)
          shopWindow = GameObject.Find("ShopWindow");
       if(shopWindow == null)
       {
          GameObject temp = new GameObect(shopWindow.name);
          temp.tag = shopWindow.name;
          temp = Instatiate(Resources.Load<GameObject>("Windows/ShopWindow"), GameObject.FindGemObjectWithTag("MainCanvas"));
          if(temp != null)
            shopWindow = temp;
        }

        buyItems = new List<MyItem>(storeItemIdsList.Count)
        for(int i = 0; i < storeItemIdsList.Count; i++)
            buyItems[i] = database.defaultGameItemsList[i].GetCopy();
        sellItems = new List<MyItem>(database.defaultGameItemsList.Count)
        for(int i = 0; i < database.defaultGameItemsList.Count; i++)
            sellItems[i] = database.defaultGameItemsList.ToArray();
    }
    #endregion Unity Methods
    public void StartConversation(Player player)
    {
        player.IsInConversation = true;
        player.SetVendor(this.transform);
        shopwindow.SetActive(true);
        this.enable = true;
    }
    public void LeaveConversation(Player player)
    {
        player.IsInConversation = false;
        player.SetVendor(null);
        shopwindow.SetActive(false);
        this.enable = false;
    }
#if UNITY_EDITOR
    public void OnSetStoreItemIdsList(List<int> ids) { storeItemIdsList = ids; }
#endif
}
