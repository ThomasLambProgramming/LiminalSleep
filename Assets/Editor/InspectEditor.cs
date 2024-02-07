using UnityEngine;
using UnityEditor;
using Interactables;

[CustomEditor(typeof(InteractManage))]
public class InspectEditor : Editor
{
    private string[] _tabOptions = { "Interactable", "Inspect" };

    //The current instance of the editor (the object we want to edit)
    InteractManage _interactManage;

    private void OnEnable()
    {
        //as keyword is from c#, it is the equivalent to 
        //expression is type ? (type)expression : (type)null
        //Notes for future thomas to understand how the hell editor windows work when I eventually forget.
        _interactManage = target as InteractManage;
    }
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();
        _interactManage._editorSelection = GUILayout.Toolbar(_interactManage._editorSelection, _tabOptions);
        EditorGUILayout.EndVertical();

        switch(_interactManage._editorSelection)
        {
            case 0:
                Interactable();
                break;
            case 1:
                Inspect();
                break;
        }
        //Run the base gui
        //base.OnInspectorGUI();
    }

    private void Interactable()
    {
        _interactManage._throwForce = EditorGUILayout.FloatField("Throw Force", _interactManage._throwForce);
        _interactManage._nonCarryThrowForce = EditorGUILayout.FloatField("Non Carry Force", _interactManage._nonCarryThrowForce);
        _interactManage._interactRange = EditorGUILayout.FloatField("Interact Range", _interactManage._interactRange);
        _interactManage._interactHoldLocation = EditorGUILayout.ObjectField("Interact Hold Location", _interactManage._interactHoldLocation, typeof(Transform), true) as Transform;
    }
    private void Inspect()
    {
        _interactManage._rotateSpeed = EditorGUILayout.FloatField("Rotate Speed", _interactManage._rotateSpeed);
        _interactManage._inspectSpeedIn = EditorGUILayout.FloatField("Inspect Speed In", _interactManage._inspectSpeedIn);
        _interactManage._inspectSpeedOut = EditorGUILayout.FloatField("Inspect Speed Out", _interactManage._inspectSpeedOut);
        _interactManage._inspectHoldLocation = EditorGUILayout.ObjectField("Inspect Location", _interactManage._inspectHoldLocation, typeof(Transform), true) as Transform;
    }
}
