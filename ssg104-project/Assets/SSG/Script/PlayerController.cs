using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Camera cam;
    PlayerInput GameInput;
    InputAction m_input;
    private int moveDir;
    private float navPos;
    private bool onNav = false;
    public int MoveDir()
    {
        return moveDir;
    }
    private void Awake()
    {
        cam = Camera.main;
        GameInput = new PlayerInput();
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
    public void SetNavigate(float pos)
    {
        navPos = pos;
        onNav = true;
    }
    int Navigate()
    {
        float dis = navPos - transform.position.x;
        int dir = 0;
        if (dis * dis < 0.1f) { onNav = false; }
        else if (dis > 0f) dir = 1;
        else dir = -1;
        return dir;
    }
    //Test navigate
    void MoveByClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            SetNavigate(mousePos.x);
        }
    }
    void Update()
    {
        InputUpdate();
        MoveByClick();
    }
}
