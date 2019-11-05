using System.Collections.Generic;
using InventorySystem.PgItems;
using UnityEngine;

public class TreasureCollision : MonoBehaviour
{

    static string DefaultText = "Check the treasure chest!";
    static string ReceivedItemConditionName = "receivedItem";

    private Animator anim;
    private ItemGenerator itemGenerator;
    private string ItemsDbContent;
    private string PrefixDbContent;
    private string SuffixDbContent;
    private string DropSourceDbContent;
    private List<Item> items;
    private List<Affix> prefixes;
    private List<Affix> suffixes;
    private DropSource treasureChest;
    private bool receivedItem;
    private bool isCollided;
    private AudioSource[] audSrc;
    private string text;
    
    void Start()
    {
        ItemsDbContent = Resources.Load<TextAsset>("Items").text;
        PrefixDbContent = Resources.Load<TextAsset>("Prefixes").text;
        SuffixDbContent = Resources.Load<TextAsset>("Suffixes").text;
        DropSourceDbContent = Resources.Load<TextAsset>("TreasureChests").text;

        audSrc = GetComponents<AudioSource>();
        items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(ItemsDbContent);
        prefixes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Affix>>(PrefixDbContent);
        suffixes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Affix>>(SuffixDbContent);
        treasureChest = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DropSource>>(DropSourceDbContent)[0];
        itemGenerator = new ItemGenerator(items, prefixes, suffixes);
        anim = GetComponent<Animator>();

        anim.SetBool(ReceivedItemConditionName, receivedItem);

        text = DefaultText;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isCollided = true;    
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isCollided = false;
        receivedItem = false;
        anim.SetBool(ReceivedItemConditionName, receivedItem);

        text = DefaultText;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 680, 400, 20), text, new GUIStyle()
        {
            fontSize = 24,
            fontStyle = FontStyle.Bold
        });
    }

    // Update is called once per frame
    void Update()
    {
        var openKeyPressed = Input.GetKey(KeyCode.Space);

        if(openKeyPressed && isCollided && !receivedItem)
        {
            var item = itemGenerator.GenerateItem(treasureChest);
            if (item.Name.Equals("NoDrop"))
            {
                var clip = audSrc[1];
                clip.Play();
                text = "Aww, you didn't get anything!";
            }
            else
            {
                var clip = audSrc[0];
                clip.Play();
                text = $"You have received a: {item.Name}";
            }

            receivedItem = true;
        }

        anim.SetBool(ReceivedItemConditionName, receivedItem);
    }
}
