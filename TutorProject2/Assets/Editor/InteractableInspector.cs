using UnityEngine;
using System.Collections;
using UnityEditor; //We need to use the editor library

//This tells our class that it's going to be replacing the inspector for Interactable
[CustomEditor(typeof(Interactable))]  
public class InteractableInspector : Editor //We need to inherit from 'Editor' not 'Monobehaviour'
{
    //We're overriding the OnInspectorGUI() of our default editor for our target class
    public override void OnInspectorGUI()
    {
        //This tells us to draw the default inspector anyway, we don't want that right now
        //Sometimes we'll just want to custom draw extra things and don't want to re-write bits that work
        //fine, so we'll want to leave this in then.
        //base.OnInspectorGUI();

        //This gives us a handy reference to the specific instance we're replacing.
        Interactable t = (Interactable)target;
        
        //We're going to make sure that we have our array and if not make one with a normal length.
        if (t.responses.Length < 1)
        {
            t.responses = new Response[9];
        }

        //Use a loop to draw the features for our inspector so we don't need to do them one at a time
        for (int i = 0; i < t.responses.Length; i++)
        {
            //Create a vertical gui section, this means all the elements will be placed vertically
            EditorGUILayout.BeginVertical();
            //Label, we're doingg a bit of wizardry here to make sure our responses match the actions.
            //Cast the current element to its matching action, then represent it as a string and use it as the label.
            EditorGUILayout.LabelField(((Action)i).ToString());
            //Success toggle button
            t.responses[i].success = EditorGUILayout.Toggle("Successful?", t.responses[i].success);
            //Message text area
            t.responses[i].message = EditorGUILayout.TextArea(t.responses[i].message);
            //Item space, haven't made this yet.
            //End the vertical section
            EditorGUILayout.EndVertical();
            //Leave a space before we draw the next one
            EditorGUILayout.Separator();
        }
    }
}
