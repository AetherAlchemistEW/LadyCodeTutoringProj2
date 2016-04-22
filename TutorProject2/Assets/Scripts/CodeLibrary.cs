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
public struct Response
{
    //VARIABLES
    public bool success;
    public string message;
    //Item
}