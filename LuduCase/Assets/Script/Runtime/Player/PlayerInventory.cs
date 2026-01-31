// Basit Envanter Sistemi (Player üzerine eklenecek)
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> keys = new List<string>();

    public void AddKey(string keyID)
    {
        if (!keys.Contains(keyID)) keys.Add(keyID);
        Debug.Log($"Key added: {keyID}");
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID);
    }
}