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

    //We'll find the end point here later
}
