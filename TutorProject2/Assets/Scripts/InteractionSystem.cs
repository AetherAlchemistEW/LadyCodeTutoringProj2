﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//Zoom at 145% (note for arcade projector)

//This class will handle all our interactions with our level as well as all our inventory and UI manipulation
public class InteractionSystem : MonoBehaviour
{
    //VARIABLES
    private Vector3 clickedPoint;
    //our current response returned from an interactable object we interact with
    private Response lastResponse;
    //the current action we're giving to an interactable object
    private Action currentAction;
    private Interactable currentTarget;
    public AvatarMovement avatar;
    public List<Item> inventory;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if we click the mouse
        if (Input.GetMouseButtonDown(1))
        {
            //make a raycast from the camera to the world
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit clickHit; //this holds information about what we clicked on
            //fire the raycast and retrieve the data
            if(Physics.Raycast(clickRay, out clickHit))
            {
                //if we hit an interactable
                if(clickHit.transform.GetComponent<Interactable>())
                {
                    //interact
                    //Debug.Log("Interactable");
                    //pass our current action to the interactable and retrieve a response
                    //lastResponse = clickHit.transform.GetComponent<Interactable>().Interact(currentAction);
                    currentTarget = clickHit.transform.GetComponent<Interactable>(); //we're going to store the interactable as our target
                }
                else if(clickHit.transform.GetComponent<PathSystem>()) //If we've clicked on something with a path system (the backdrop)
                {
                    //vector3 point
                    clickedPoint = clickHit.point; //we'll want this soon
                    PathSystem p = clickHit.transform.GetComponent<PathSystem>(); //Store a reference to the path system for convenience
                    //pass to avatar
                    Vector3 point = p.PathPoint(clickedPoint.x); //get the end point of the path from the path system by providing the click
                    avatar.SetPath(p.path, point); //pass the path to our avatar as well as the end point
                }
            }
        }
	}

    //Our interact method passes our current action to our target and retrieves a response.
    void Interact()
    {
        if(currentTarget != null) //make sure we have a target before we try and interact with it
        {
            //last response is where we store the response
            //it is returned from the interact method on our current target (an Interactable)
            //it takes our current action as the argument to determine the response
            lastResponse = currentTarget.Interact(currentAction);
            if(lastResponse.item != null && inventory.Count < 8)
            {
                //collect item
                if (lastResponse.success)
                {
                    inventory.Add(lastResponse.item);
                    currentTarget.responses[(int)currentAction].success = false;
                }
                //remove item from response

            }
            Debug.Log(lastResponse.message); //for now we'll just output the message from the response
        }
    }

    //---------UI Methods----------//
    //These methods serve only to allow our button to change our action
    //and to run our interact method so we can interact with objects.
    public void Give()
    {
        currentAction = Action.give;
        Interact();
    }
    public void PickUp()
    {
        currentAction = Action.pickup;
        Interact();
    }
    public void Open()
    {
        currentAction = Action.open;
        Interact();
    }
    public void Close()
    {
        currentAction = Action.close;
        Interact();
    }
    public void Look()
    {
        currentAction = Action.look;
        Interact();
    }
    public void Use()
    {
        currentAction = Action.use;
        Interact();
    }
    public void Push()
    {
        currentAction = Action.push;
        Interact();
    }
    public void Pull()
    {
        currentAction = Action.pull;
        Interact();
    }
    public void Talk()
    {
        currentAction = Action.talk;
        Interact();
    }

    //Gizmos to help us visualise what's happening.
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(clickedPoint, 0.5f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, clickedPoint);
    }
}
