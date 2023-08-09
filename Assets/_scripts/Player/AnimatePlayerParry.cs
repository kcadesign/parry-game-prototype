using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerParry : MonoBehaviour
{
    private Animator _playerAnimator;

    private void OnEnable()
    {
        PlayerParry.OnParryActive += PlayerParry_OnParryActive;
    }

    private void OnDisable()
    {
        PlayerParry.OnParryActive -= PlayerParry_OnParryActive;
    }

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void PlayerParry_OnParryActive(bool parryPressed)
    {
        Debug.Log(parryPressed);
        _playerAnimator.SetBool("Parry", parryPressed);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
