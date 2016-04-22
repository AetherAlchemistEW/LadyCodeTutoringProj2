using UnityEngine;
using System.Collections;
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
                    Debug.Log("Interactable");
                    //pass our current action to the interactable and retrieve a response
                    lastResponse = clickHit.transform.GetComponent<Interactable>().Interact(currentAction);
                }
                else
                {
                    //vector3 point
                    clickedPoint = clickHit.point;
                    Debug.Log("Not Interactable");
                }
            }
        }
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
