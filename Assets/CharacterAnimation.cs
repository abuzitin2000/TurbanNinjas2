using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
