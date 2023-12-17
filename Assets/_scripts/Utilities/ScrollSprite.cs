using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSprite : MonoBehaviour
{
    private Material _material;
    [SerializeField] private float speed = 0.5f;
    private float currentscroll = 0f;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        currentscroll += speed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(0, currentscroll);
    }
}
