using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{
    private SerializedProperty m_InteractableProperty;

    protected override void OnEnable()
    {
        m_InteractableProperty = serializedObject.FindProperty("m_Interactable");
    }
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        var changeButtonType = new PropertyField(serializedObject.FindProperty(CustomButton.ChangeButtonType));
        var curveEase = new PropertyField(serializedObject.FindProperty(CustomButton.CurveEase));
        var duration = new PropertyField(serializedObject.FindProperty(CustomButton.Duration));

        var tweenLable = new Label("Settings Tween");
        var interactableLabel = new Label("Interactable");

        root.Add(tweenLable);
        root.Add(changeButtonType);
        root.Add(curveEase);
        root.Add(duration);

        root.Add(interactableLabel);
        root.Add(new IMGUIContainer(OnInspectorGUI));

        return root;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(m_InteractableProperty);

        EditorGUI.BeginChangeCheck();

        serializedObject.ApplyModifiedProperties();
    }
}
