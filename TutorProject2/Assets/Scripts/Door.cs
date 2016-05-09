using UnityEngine;
using System.Collections;

//The door class inherits from Interactable, this means it starts the same as an Interactable script
//It has all of Interactables variables and methods, they're just not visible.
//It's worth noting that the inheriting object does not inherit our custom inspector however...
public class Door : Interactable
{
    //We use the new keyword here to override the Interact method on Interactable.
    //This is one of a couple of different ways to do this, this approach serves our purpose best.
    new public Response Interact(Action act)
    {
        //Because door now has its own way of handling 'Interact' we can extend it a bit.
        //In this case we're going to influence the open and close responses if they're passed in.
        if(act == Action.open)
        {
            responses[(int)Action.open].success = false;
            responses[(int)Action.close].success = true;
        }
        if(act == Action.close)
        {
            responses[(int)Action.close].success = false;
            responses[(int)Action.open].success = true;
        }

        //we still have to return a response.
        return responses[(int)act];
    }
}
