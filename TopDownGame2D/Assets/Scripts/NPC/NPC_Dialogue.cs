using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Dialogue : MonoBehaviour
{
    [SerializeField]
    private float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSettings dSettings;
    public Image actorSprite;
    public string actorName;

    public bool playerHit;

    private void Update()
    {
        // Starts the dialogue
        if(Input.GetKeyDown(KeyCode.F) &&
           playerHit &&
           !DialogueControl.instance.isDialogueVisible)
        {
            DialogueControl.instance.Speech(dSettings.dialogues);
        }

        // Continue the dialogue
        if (Input.GetKeyDown(KeyCode.F) &&
            playerHit &&
            DialogueControl.instance.isDialogueVisible)
        {
            DialogueControl.instance.NextSentence();
        }
    }
    
    private void FixedUpdate()
    {
        ShowDialogue();
    }
    
    private void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if(hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;

            if (DialogueControl.instance.isDialogueVisible)
            {
                DialogueControl.instance.ResetSentence();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
