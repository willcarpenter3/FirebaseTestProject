using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading.Tasks;

public class ItemSaveManager : MonoBehaviour
{
    private const string ITEM_KEY = "ITEM_KEY";
    private const string POS_KEY = "POS_KEY";
    private FirebaseDatabase _database;
    private DatabaseReference reference;

    private void Awake()
    {
        _database = FirebaseDatabase.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        //Debug.Log("yeet");
    }

    public void SaveItem(ItemData item)
    {
        //PlayerPrefs.SetString(ITEM_KEY, JsonUtility.ToJson(item));
        _database.GetReference(ITEM_KEY).SetRawJsonValueAsync(JsonUtility.ToJson(item));
    }

    public void PushItem(ItemData item)
    {
        string key = _database.GetReference(ITEM_KEY).Push().Key;
        _database.GetReference("Positions/" + key).SetRawJsonValueAsync(JsonUtility.ToJson(item));
    }

    public async Task<ItemData?> LoadItem()
    {
        var dataSnapshot = await _database.GetReference(ITEM_KEY).GetValueAsync();
        if (!dataSnapshot.Exists)
        {
            return null;
        }

        return JsonUtility.FromJson<ItemData>(dataSnapshot.GetRawJsonValue());
    }

    public async Task<bool> SaveExists()
    {
        //return PlayerPrefs.HasKey(ITEM_KEY);
        var dataSnapshot = await _database.GetReference(ITEM_KEY).GetValueAsync();
        return dataSnapshot.Exists;
    }

    public void EraseSave()
    {
        _database.GetReference(ITEM_KEY).RemoveValueAsync();
    }
}
