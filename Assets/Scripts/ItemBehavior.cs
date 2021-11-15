using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBehavior : MonoBehaviour
{

    [SerializeField]
    private ItemData _itemData;

    public ItemData ItemData => _itemData;

    public string Name => _itemData.name;
    public string Type => _itemData.type;

    ItemSaveManager manager;

    public InputField nameText;
    public InputField typeText;

    public GameObject container;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    
    // Start is called before the first frame update
    private void Start()
    {
        manager = FindObjectOfType<ItemSaveManager>();
    }

    public void SaveItem()
    {
        _itemData.name = nameText.text;
        _itemData.type = typeText.text;
        manager.SaveItem(_itemData);
        SpawnShape(_itemData.type);
    }

    public void pushItem()
    {
        _itemData.name = nameText.text;
        _itemData.type = typeText.text;
        manager.PushItem(_itemData);
        SpawnShape(_itemData.type);
    }

    public void LoadItemUI()
    {
        StartCoroutine("LoadItem");
    }

    public IEnumerator LoadItem()
    {
        var itemDataTask = manager.LoadItem();
        yield return new WaitUntil(() => itemDataTask.IsCompleted);
        var itemData = itemDataTask.Result;
        if (itemData.HasValue)
        {
            _itemData = itemData.Value;
            Debug.Log("Name: " + _itemData.name + ", Type: " + _itemData.type);
            nameText.text = _itemData.name;
            typeText.text = _itemData.type;
            SpawnShape(_itemData.type);
        }
        else
        {
            Debug.LogError("No Data Retrieved");
        }
    }

    

    public void DeleteItem()
    {
        if (container.transform.childCount > 0)
        {
            Destroy(container.transform.GetChild(0).gameObject);
        }

        manager.EraseSave();
    }

    private void SpawnShape(string type)
    {
        if (container.transform.childCount > 0)
        {
            Destroy(container.transform.GetChild(0).gameObject);
        }

        GameObject g = null;
        switch (type.ToLower())
        {
            case "cube":
                g = Instantiate(cubePrefab);
                break;
            case "sphere":
                g = Instantiate(spherePrefab);
                break;
        }
        g.transform.SetParent(container.transform);
    }
}
