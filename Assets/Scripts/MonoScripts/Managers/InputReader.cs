using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    SimpleRTSGame playerActions;
    InputAction playerLeftClick, playerRightClick;
    InputAction mousePoint;

    //Change these to unity Events?  Actions?
    public delegate void StartSelect();
    public static StartSelect startSelect;
    public delegate void CancelDrag();
    public static CancelDrag cancelDrag;
    public delegate void SelectSingle(GameObject selectedObj);
    public static SelectSingle selectSingle;
    public delegate void SelectArea(Vector2 start, Vector2 end);
    public static SelectArea selectArea;
    public delegate void StartDrawSelection(Vector2 start, Vector2 end);
    public static StartDrawSelection startSelectionBoxDraw;
    public delegate void RightClick(RaycastHit hitInfo);
    public static RightClick rightClick;

    Vector2 clickStartPosition;

    private void Awake()
    {
        playerActions = new SimpleRTSGame();
    }
    private void OnEnable()
    {
        playerLeftClick = playerActions.Player.LeftClick;
        playerLeftClick.started += TrackClickStart;
        playerLeftClick.performed += SelectInArea;
        playerLeftClick.canceled += HandleLeftClick;
        playerLeftClick.canceled += (InputAction.CallbackContext context) => { cancelDrag?.Invoke(); };
        playerLeftClick.Enable();

        playerRightClick = playerActions.Player.RightClick;
        playerRightClick.performed += SendRightClickInput;
        playerRightClick.Enable();

        mousePoint = playerActions.Player.Point;
        mousePoint.performed += SendCurrentMousePosition;
        mousePoint.Enable();
    }
    private void OnDisable()
    {
        playerLeftClick.Disable();
        mousePoint.Disable();
    }
    /// <summary>
    /// Intitates the start of the drag select process
    /// </summary>
    /// <param name="context"></param>
    void TrackClickStart(InputAction.CallbackContext context)
    {
        startSelect?.Invoke();
        clickStartPosition = Mouse.current.position.value;
    }
    /// <summary>
    /// Main function for handling a single click selection
    /// </summary>
    /// <param name="context"></param>
    void HandleLeftClick(InputAction.CallbackContext context)
    {
        var info = RaycastMouse(Mouse.current.position.value);
        selectSingle?.Invoke(info.collider.gameObject);
    }
    /// <summary>
    /// End the process of selecting a group of objects
    /// </summary>
    /// <param name="context"></param>
    void SelectInArea(InputAction.CallbackContext context)
    {
        selectArea?.Invoke(clickStartPosition, Mouse.current.position.value);
    }
    /// <summary>
    /// Main function for handling a single right click
    /// </summary>
    /// <param name="context"></param>
    void SendRightClickInput(InputAction.CallbackContext context) 
    {
        var hitInfo = RaycastMouse(Mouse.current.position.value);
        rightClick?.Invoke(hitInfo);

    }
    /// <summary>
    /// Send the information necessary to draw the selection box without creating the box
    /// </summary>
    /// <param name="context"></param>
    void SendCurrentMousePosition(InputAction.CallbackContext context) 
    {
        startSelectionBoxDraw?.Invoke(clickStartPosition, Mouse.current.position.value);
        //Debug.Log($"Mouse Moving");
    }
    public static RaycastHit RaycastMouse(Vector2 mousePosition) 
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        var hitSomething = Physics.Raycast(ray, out RaycastHit _hitInfo, 500);

        if (!hitSomething)
            return new RaycastHit();

        return _hitInfo;
    }
}
