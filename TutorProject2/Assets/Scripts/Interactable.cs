using UnityEngine;
using System.Collections;

//This is going to serve as the base class for everything we can interact with.
public class Interactable : MonoBehaviour
{
    //this is our array of possible responses.
    public Response[] responses;
    /*0-give,
    1-pickup,
    2-open,
    3-close,
    4-look,
    5-use,
    6-push,
    7-pull,
    8-talk*/

    //This method takes an action from our interaction system and returns a response.
    public Response Interact(Action act)
    {
        //convert the action to its number and return the corresponding response from the array
        return responses[(int)act];
    }
}
