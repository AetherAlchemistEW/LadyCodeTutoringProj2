using UnityEngine;
using System.Collections;

//This will just hold a basic path system and draw it in the scene, later it'll hold most of our math for our avatar's end point as well
public class PathSystem : MonoBehaviour
{
    //Our path as a public array we can modify. 
    //we need to use an array and not a list as Vector3s are value types but lists are technically a class so it's a reference type.
    //If we use a list we won't be passing a copy of the values to the avatar, we'll be passing a reference to this exact path on this script (we don't want that because we need to change the path).
    public Vector3[] path;
    
    //Draw the path in the scene so we can see it
    void OnDrawGizmos()
    {
        for(int i = 0; i < path.Length; i++)
        {
            //Draw the point
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(path[i], 0.5f);
            //Draw the path
            if(i+1 < path.Length)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }

    //Finds the point along the path and passes it to the interaction system to pass on to the avatar
    public Vector3 PathPoint(float givenX)
    {
        //find the points either side of the provided point
        Vector3[] points = Points(givenX);

        //The vector from point 0 towards point 1
        Vector3 lineVec = points[1] - points[0];
        //we're going to do a linear interpolation along this vector

        //find 't', the percentage from point a to point b (from 0..1)
        //the difference from the left point to the given point over the total distance along the vector
        float t = (givenX - points[0].x) / lineVec.x;
        //then apply t to the line, to find the final point
        Vector3 pathPoint = points[0] + lineVec * t;

        //error correct, if we click outside of the path we'll get a t that's <0 or >1 and it'll break our system
        if(t < 0 || t > 1)
        {
            //setting path point to 0, it's still a valid position but we'll look for it when we're making our path
            pathPoint = Vector3.zero;
        }

        //return the value to the interaction system
        return pathPoint;
    }

    //Finds the path points either side of the clicked point
    Vector3[] Points(float givenX)
    {
        //array of 2 vector 3s
        Vector3[] points = new Vector3[2];

        //start point 0 at the start of the path, this represents the point to the left of the click
        points[0] = path[0];
        //start point 1 at the end of the path, this represents the point to the right of the click
        points[1] = path[path.Length - 1];

        //loop through the path points
        foreach (Vector3 p in path)
        {
            //if the x value of the current path point is > the given X && < point 1's x
            //this means that the current point we're checking is to the right of the click but to the left of our current 'rightmost' point
            if (p.x > givenX && p.x < points[1].x)
            {
                //the current path point is point 1
                points[1] = p;
            }

            //if the x value of the current path point is < the given x && > point 0's x
            //this means the current point is to the left of the click but the right of our current leftmost point
            if (p.x < givenX && p.x > points[0].x)
            {
                //the current path point is point 0
                points[0] = p;
            }
        }

        //return our points for the line vector check
        return points;
    }
}
