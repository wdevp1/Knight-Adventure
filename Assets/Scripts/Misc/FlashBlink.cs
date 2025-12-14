using System;
using UnityEngine;

public class FlashBlink : MonoBehaviour
{
    [SerializeField] private MonoBehaviour damagebleObject;
    [SerializeField] private Material blinkMaterial;
    [SerializeField] private float blinkDuration = 0.2f;

    private float _blinkTimer;
    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    private bool _isBlinking;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;

        _isBlinking = true;
    }

    private void Start()
    {
        if (damagebleObject is Player player)
        {
            player.OnFlashBlink += DamagebleObject_OnFlashBlink;
        }
    }

    void Update()
    {
        if (_isBlinking)
        {
            _blinkTimer -= Time.deltaTime;
            if (_blinkTimer < 0)
            {
                SetDefaultMaterial();
            }
        }
    }

    private void OnDestroy()
    {
        if (damagebleObject is Player player)
        {
            player.OnFlashBlink -= DamagebleObject_OnFlashBlink;
        }
    }

    public void StopBlinking()
    {
        SetDefaultMaterial();
        _isBlinking = false;
    }

    private void DamagebleObject_OnFlashBlink(object sender, EventArgs e)
    {
        SetBlinkMaterial();
    }

    private void SetBlinkMaterial()
    {
        _blinkTimer = blinkDuration;
        _spriteRenderer.material = blinkMaterial;
    }

    private void SetDefaultMaterial()
    {
        _spriteRenderer.material = _defaultMaterial;
    }
}
