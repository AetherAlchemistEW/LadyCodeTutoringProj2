using UnityEngine;
using System.Collections;
using System.Collections.Generic; //necessary library for incorporating lists

//This class will handle moving the player avatar through the level
public class AvatarMovement : MonoBehaviour
{
    //this list will serve as the path the avatar will follow
    public List<Vector3> ourPath;
    public float speed;

    //Coroutine, this will handle moving the player through their path
    IEnumerator MoveToPoint()
    {
        //we can use while loops in our coroutines safely because of yield, this basically makes a temporary Update method.
        while (ourPath.Count > 0) //This loop will run as long as we have nodes in our path list, we're handling waypointing a bit differently this time.
        {
            //while we're not near the current (in this case, first) waypoint
            while (ourPath.Count > 0) 
            {
                //find the direction to the point
                Vector3 dir = (ourPath[0] - transform.position).normalized;
                //move towards it
                //we're using the direction instead of looking at it so that our avatar doesn't rotate
                transform.Translate(dir * speed * Time.smoothDeltaTime);
                //remember to yield, yielding tells the coroutine to let everything else run and come back when specified
                //we'll check if we're near the first waypoint and if we are we'll remove it from our list
                if(Vector3.Distance(transform.position, ourPath[0]) < 0.5f)
                {
                    //Because lists are dynamic, it'll bump the next point up into the 0th slot in the list.
                    ourPath.Remove(ourPath[0]);
                }
                //returning null is the same as saying 'until next frame' but we can also specify a WaitForSeconds to wait for a set amount of time
                yield return null;
            }
            //we're removing the first (essentially current) node from our path, because lists are dynamic this means the next element moves up into the 'first' slot            ourPath.Remove(ourPath[0]);
            //yield again so that we don't have any issues
            yield return null;
        }
    }

    //This method gets passed an array of Vector3s for our path and adds them into our list
    //Later it will also take the end point along the path as a final node
	public void SetPath(Vector3[] path, Vector3 endPoint)
    {
        //clear our current list and stop our MoveToPoint first
        ourPath.Clear();
        StopAllCoroutines();

        //add all the path points to ourPath
        if(endPoint.x < transform.position.x) //right to left, because the end point is lower in the x axis than we are.
        {
            for (int i = path.Length-1; i >= 0; i--) //because we're going right to left and our path system is populated left to right, we need to count down
            {
                if(path[i].x > endPoint.x && path[i].x < transform.position.x) //we only want to add the points to the right of the end point and to the left of us
                {
                    ourPath.Add(path[i]);
                }
            }
        }
        if(endPoint.x > transform.position.x) //left to right
        {
            for (int i = 0; i < path.Length; i++) //counting up because we're going left to right
            {
                if(path[i].x < endPoint.x && path[i].x > transform.position.x) //add points to the left of the end and the right of us.
                {
                    ourPath.Add(path[i]);
                }
            }
        }

        if (endPoint != Vector3.zero) //check that we didn't 0 out the end point (invalid point)
        {
            ourPath.Add(endPoint); //add our end point as the final point
        }

        //Start up our MoveToPoint coroutine
        StartCoroutine("MoveToPoint");
    }
}
