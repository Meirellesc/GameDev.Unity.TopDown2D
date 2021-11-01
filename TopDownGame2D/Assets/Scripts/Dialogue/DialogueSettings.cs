using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence;

    public List<Dialogue> dialogues = new List<Dialogue>();
}

[Serializable]
public class Dialogue
{
    public string actorName;
    public Sprite actorSprite;
    public Languages language;
}

[Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings dSettings = (DialogueSettings)target;

        Languages languages = new Languages()
        {
            portuguese = dSettings.sentence
        };

        Dialogue dialogue = new Dialogue()
        {
            actorSprite = dSettings.speakerSprite,
            language = languages
        };

        if(!String.IsNullOrEmpty(dSettings.sentence))
        {
            if(GUILayout.Button("Create Dialogue"))
            {
                dSettings.dialogues.Add(dialogue);

                dSettings.speakerSprite = null;
                dSettings.sentence = "";
            }
        }
    }
}
#endif