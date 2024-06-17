using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject ghost;

    public bool makeGhost = false;
    public float imageDelay;
    private float ghostDelaySeconds;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = imageDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                //Generate ghost
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                Color currentColor = GetComponent<SpriteRenderer>().color;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;

                // make instantiated sprite the same color as the original sprite
                currentGhost.GetComponent<SpriteRenderer>().color = currentColor;

                ghostDelaySeconds = imageDelay;

                Destroy(currentGhost, 0.25f);
            }
        }

    }


}
