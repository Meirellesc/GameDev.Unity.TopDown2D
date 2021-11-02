using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnim : MonoBehaviour
{
    private Tree tree;
    private Animator animator;

    // Triggers
    private string isCutting = "isCutting";
    private string isCut = "isCut";

    // Start is called before the first frame update
    void Start()
    {
        tree = GetComponent<Tree>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OnCutting();
        OnCut();
    }

    private void OnCutting()
    {
        if(tree.IsCutting)
        {
            animator.SetTrigger(isCutting);
            tree.IsCutting = false;
        }
    }

    private void OnCut()
    {
        if (tree.IsCut)
        {
            animator.SetTrigger(isCut);
        }
    }
}
