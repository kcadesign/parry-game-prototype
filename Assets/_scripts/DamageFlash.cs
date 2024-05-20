using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color _flashColor = Color.white;
    [SerializeField] private float _flashDuration = 0.25f;

    public Material FlashMaterial;

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _materials;

    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        // set all sprite renderers to use the flash material
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
        {
            spriteRenderer.material = FlashMaterial;
        }
        InitialiseMaterials();
    }

    private void OnEnable()
    {
        HandleBossHealth.OnBossHealthChange += HandleBossHealth_OnBossHealthChange;
    }

    private void OnDisable()
    {
        HandleBossHealth.OnBossHealthChange -= HandleBossHealth_OnBossHealthChange;
    }

    private void HandleBossHealth_OnBossHealthChange(int currentHealth, int maxHealth)
    {
        StartCoroutine(Flash());
    }

    private void InitialiseMaterials()
    {
        _materials = new Material[_spriteRenderers.Length];

        // assign the material of the sprite renderer to the material array
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _materials[i] = _spriteRenderers[i].material;
        }
    }

    private IEnumerator Flash()
    {
        SetFlashColor();

        float currentFlashAmount = 0;
        float elapsedTime = 0;
        while (elapsedTime < _flashDuration)
        {
            elapsedTime += Time.deltaTime;
            currentFlashAmount = Mathf.Lerp(1, 0, elapsedTime / _flashDuration);
            SetFlashAmount(currentFlashAmount);

            yield return null;
        }
    }

    private void SetFlashColor()
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetColor("_FlashColor", _flashColor);
        }
    }

    private void SetFlashAmount(float amount)
    {
        for (int i = 0; i < _materials.Length; i++)
        {
            _materials[i].SetFloat("_FlashAmount", amount);
        }
    }
}
