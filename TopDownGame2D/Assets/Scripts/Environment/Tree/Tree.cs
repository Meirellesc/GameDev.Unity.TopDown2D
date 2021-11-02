using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private GameObject woodPrefab;

    [SerializeField] private Player player;

    [SerializeField] private ParticleSystem leafs1;
    [SerializeField] private ParticleSystem leafs2;
    [SerializeField] private ParticleSystem stack;

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

            // Play the leafs particle
            leafs1.Play();
            leafs2.Play();
        }
        else
        {
            if (!_isCut)
            {
                //Change the tree sprite to stack
                _isCutting = false;
                _isCut = true;

                // Generate a random wood quantity
                int woodQuantity = Random.Range(1, 4);

                for (int i = 0; i < woodQuantity; i++)
                {
                    // Drop the wood
                    Instantiate(woodPrefab, transform.position, transform.rotation);
                }
            }
            else
            {
                var shape = stack.shape;
                var renderer = stack.GetComponent<ParticleSystemRenderer>();

                if (player.transform.position.x > transform.position.x)
                {
                    renderer.flip = new Vector3(1, 0, 0);
                    shape.rotation = new Vector3(0, -35, 0);
                }
                else
                {
                    renderer.flip = new Vector3(0 , 0, 0);
                    shape.rotation = new Vector3(0, 35, 0);
                }

                stack.Play();
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
