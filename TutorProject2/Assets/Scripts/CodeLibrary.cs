using UnityEngine;
//This script file holds all the little bits and pieces that we need to make or use on various scripts.
//Doesn't need a class, can't go on objects.

//Actions enum
//This is all our possible actions, keep in mind they're also numbered 0..8
public enum Action
{
    give,
    pickup,
    open,
    close,
    look,
    use,
    push,
    pull,
    talk
};

//Response struct
//This packages up information regarding our possible responses.
[System.Serializable] //This tells Unity that we can serialize our struct
public struct Response
{
    //VARIABLES
    [SerializeField] //This tells Unity to serialize the variable
    public bool success;
    [SerializeField]
    public string message;
    [SerializeField]
    public Item item;
    //Item will go here later
}

[System.Serializable]
public class Item : ScriptableObject
{
    //VARIABLES
    //name
    [SerializeField]
    public string name;
    //icon
    [SerializeField]
    public Texture2D icon;
}