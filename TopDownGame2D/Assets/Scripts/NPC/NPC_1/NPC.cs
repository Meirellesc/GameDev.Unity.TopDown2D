using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float initialSpeed;

    private int index;
    public List<Transform> paths = new List<Transform>();

    private Animator animator;
    private string isWalking = "isWalking";

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsNPCDialogue();

        // Get the positions
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = paths[index].position;

        // Move to the target position
        MoveToPath(currentPosition, targetPosition);

        // Check if arrived to target path and get the next one
        if(Vector2.Distance(currentPosition, targetPosition) <= 0.1f)
        {
            // Walks randomly
            index = Random.Range(0, paths.Count);

            // Walks a order path
            //if(index < paths.Count - 1)
            //{
            //    index++;                
            //}
            //else
            //{
            //    index = 0;
            //}

        }

        SetDirection(currentPosition, targetPosition);
    }

    private void CheckIsNPCDialogue()
    {
        if (DialogueControl.instance.isDialogueVisible)
        {
            speed = 0f;
            animator.SetBool(isWalking, false);
        }
        else
        {
            speed = initialSpeed;
            animator.SetBool(isWalking, true);
        }
    }

    private void MoveToPath(Vector3 currentPosition, Vector3 targetPosition)
    {
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
    }

    private void SetDirection(Vector3 currentPosition, Vector3 targetPosition)
    {
        // Get the NPC direction
        Vector2 direction = targetPosition - currentPosition;

        // Check if NPC is going to right or left direction
        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
