using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollImage : MonoBehaviour
{
    private RawImage Image;

    [SerializeField] private float x, y;

    private void Start()
    {
        Image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        Image.uvRect = new Rect(Image.uvRect.position + new Vector2(x, y) * Time.unscaledDeltaTime, Image.uvRect.size);
    }
}
