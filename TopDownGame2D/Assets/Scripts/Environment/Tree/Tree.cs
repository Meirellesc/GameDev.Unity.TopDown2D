using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private GameObject woodPrefab;

    // Cutting Control
    private bool _isCutting;
    public bool IsCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }

    // Cut Control
    private bool _isCut;
    public bool IsCut
    {
        get { return _isCut; }
        set { _isCut = value; }
    }

    private void OnHit()
    {
        // Check if tree still has life
        if(treeHealth > 1)
        {
            treeHealth--;

            _isCutting = true;
        }
        else
        {
            if (!_isCut)
            {
                //Change the tree sprite to stack
                _isCutting = false;
                _isCut = true;

                int woodQuantity = Random.Range(1, 4);

                for (int i = 0; i < woodQuantity; i++)
                {
                    // Drop the wood
                    Instantiate(woodPrefab, transform.position, transform.rotation);
                }
            }
        }
    }

    /// <summary>
    /// Detect trigger between two colliders
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Axe"))
        {
            OnHit();
        }
    }
}
