using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    PlayerInput GameInput;
    InputAction m_input;
    Animator m_ani;
    private int moveDir;
    private float navPos;
    [HideInInspector] public bool onNav = false;
    private bool canMove => GameManager.Instance.inGame;
    private ParticleSystem particle => GetComponentInChildren<ParticleSystem>();
    public int MoveDir()
    {
        return moveDir;
    }
    private void Awake()
    {
        GameInput = new PlayerInput();
        m_ani = GetComponentInChildren<Animator>();
    }
    
    private void OnEnable()
    {
        m_input = GameInput.InGame.Movement;
        m_input.Enable();
    }
    private void OnDisable()
    {
        m_input.Disable();
    }
    void InputUpdate()
    {
        if (onNav) moveDir = Navigate();
        else moveDir = (int) m_input.ReadValue<float>();
    }
    void SetAnimator()
    {
        m_ani.SetFloat("Speed", moveDir * moveDir);
        if (moveDir == 0)  particle.Stop(); 
        else
        {
            particle.Play();
            Vector3 scale = transform.localScale;
            scale.x = moveDir * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }    
    }
    public void SetNavigate(float pos)
    {
        if(!canMove) return;
        navPos = pos;
        onNav = true;
    }
    int Navigate()
    {
        float dis = navPos - transform.position.x;
        int dir = 0;
        if (dis * dis < 0.5f) { onNav = false; }
        else if (dis > 0f) dir = 1;
        else dir = -1;
        return dir;
    }
    void Update()
    {
        if(!canMove) return;
        InputUpdate();
        SetAnimator();
    }
}
