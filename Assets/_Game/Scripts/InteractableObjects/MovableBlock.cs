using UnityEngine;

public class MovableBlock : MonoBehaviour
{
    [SerializeField] private bool isLocked;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private string switchNameToUnlock;
    [SerializeField] private bool toggleSwitch = false;
    [SerializeField] protected Vector2 lastRespawnPosition;
    [SerializeField] private string sfx;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Animator _anim;
    public Vector2 LastRespawnPosition { get => lastRespawnPosition; set => lastRespawnPosition = value; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        if (isLocked)
            Lock();
        else
            Unlock();
        lastRespawnPosition = transform.position;
    }
    
    private void OnEnable()
    {
        SwitchController.OnSwitchActivated += OnSwitchActivated;
        SwitchController.OnSwitchDeactivated += OnSwitchDeactivated;
    }

    private void OnDisable()
    {
        SwitchController.OnSwitchActivated -= OnSwitchActivated;
        SwitchController.OnSwitchDeactivated -= OnSwitchDeactivated;
    }
    private void OnSwitchActivated(SwitchController switchController)
    {
        if (switchController.switchName.Equals(switchNameToUnlock))
        {
            if (toggleSwitch)
            {
                ToggleLock();
            }
            else
            {
                isLocked = false;
                Unlock();
            }
        }
    }
    private void OnSwitchDeactivated(SwitchController switchController)
    {
        if (switchController.switchName.Equals(switchNameToUnlock))
        {
            if (toggleSwitch)
            {
                ToggleLock();
            }
            else
            {
                isLocked = true;
                Lock();
            }
        }
    }

    [ContextMenu("ToggleLock")]
    public void ToggleLock()
    {
        isLocked = !isLocked;
        if (isLocked)
            Lock();
        else
            Unlock();
    }

    private void Lock()
    {
        _anim.SetTrigger("Lock");
        _spriteRenderer.sprite = lockedSprite;
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    private void Unlock()
    {
        _anim.SetTrigger("Unlock");
        _spriteRenderer.sprite = unlockedSprite;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    public void Respawn()
    {
        _anim.SetTrigger("Respawn");
        AudioManager.instance.PlaySound(sfx);
        transform.position = lastRespawnPosition;
    }
}
