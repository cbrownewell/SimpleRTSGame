using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public GameObject moveMarker;

    [SerializeField] UnitManager unitManager;

    public List<GameObject> selectedList;
    GameObject selected { get { return selectedList.First(); } }

    [SerializeField]
    RectTransform selectionBox;

    [SerializeField]
    GameObject markerObj;

    bool drawGizmo = false;
    Vector3 center;
    float halfExtentX, halfExtentZ;
    bool dragSelecting = false;
    public bool AreSelected { get { return selectedList.Any(); } }

    [SerializeField]
    float selectionBoxFade;

    public LayerMask selectLayer;

    private void OnEnable()
    {
        InputReader.startSelect += ClearSelected;
        InputReader.startSelect += () => { dragSelecting = true; };
        InputReader.selectSingle += TrySelect;
        InputReader.selectArea += SelectInAreaWorldToScreen;
        InputReader.startSelectionBoxDraw += DrawSelectionBox;
        InputReader.cancelDrag += () => { dragSelecting = false; };
        InputReader.rightClick += HandleRightClick;
    }

    /// <summary>
    /// Clears all selected objects
    /// </summary>
    public void ClearSelected()
    {
        foreach (var item in selectedList)
        {
            item.TryGetComponent<ISelectable>(out var clearObj);
            clearObj.Highlight(false);
        }

        selectedList.Clear();
    }
    /// <summary>
    /// Check if an object implments ISelectable.  If it does add it to the selected objects list
    /// </summary>
    /// <param name="selectedObj"></param>
    void TrySelect(GameObject selectedObj)
    {
        if (selectedObj.TryGetComponent<ISelectable>(out var select))
        {
            selectedList.Add(selectedObj.gameObject);
            select.Highlight(true);
        }
    }
    /// <summary>
    /// Draws colldiers in a selected area 
    /// </summary>
    /// <param name="start">Starting corner's position</param>
    /// <param name="end">Ending corner's diagonal position</param>
    void SelectInAreaWorldToScreen(Vector2 start, Vector2 end)
    {
        SelectInAreaScreenToWorld(start, end);
        StartCoroutine(ClearSelectionBox());
    }
    /// <summary>
    /// Draws the inital selection box
    /// </summary>
    /// <param name="start">Starting corner's position</param>
    /// <param name="end">Ending corner's diagonal position</param>
    void DrawSelectionBox(Vector2 start, Vector2 end)
    {
        if (dragSelecting)
            CreateSelectionBox(start, end);
    }
    /// <summary>
    /// Select GameObjects in an area using PhysicsRaycast to figure out world space coordinates
    /// </summary>
    /// <param name="start">Starting corner's position</param>
    /// <param name="end">Ending corner's diagonal position</param>
    void SelectInAreaScreenToWorld(Vector2 start, Vector2 end)
    {
        var worldStart = InputReader.RaycastMouse(start).point; //TODO This should not be physics.  Revist projecting from world space to screen space
        var worldEnd = InputReader.RaycastMouse(end).point;

        center = Vector3.Lerp(worldStart, worldEnd, 0.5f);

        halfExtentX = Mathf.Abs((worldStart.x - worldEnd.x) / 2);
        halfExtentZ = Mathf.Abs((worldStart.z - worldEnd.z) / 2);

        var selectionColliderResults = Physics.OverlapBox(center, new Vector3(halfExtentX, 10, halfExtentZ), Quaternion.identity, selectLayer);
        drawGizmo = true;
        foreach (var item in selectionColliderResults)
        {
            if (item.TryGetComponent<ISelectable>(out var selectable))
            {
                selectable.Highlight(true);
                selectedList.Add(item.gameObject);
            }
        }
        dragSelecting = false;
    }
    /// <summary>
    /// Hide the selection box after a certain amount of time has passed
    /// </summary>
    /// <returns></returns>
    IEnumerator ClearSelectionBox()
    {
        yield return new WaitForSeconds(selectionBoxFade);
        selectionBox.gameObject.SetActive(false);
    }
    /// <summary>
    /// Draws the selection box on the UI
    /// </summary>
    /// <param name="start">Starting corner's position</param>
    /// <param name="end">Ending corner's diagonal position</param>
    void CreateSelectionBox(Vector2 start, Vector2 end)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        var width = end.x - start.x;
        var height = end.y - start.y;
        var canvasAnchorCoords = new Vector2(start.x, start.y);

        if (width < 0 && height < 0)
        {
            canvasAnchorCoords.x += width;
            canvasAnchorCoords.y += height;
        }
        else if (width < 0)
            canvasAnchorCoords.x += width;
        else if (height < 0)
            canvasAnchorCoords.y += height;

        var canvasSizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        selectionBox.anchoredPosition = canvasAnchorCoords;
        selectionBox.sizeDelta = canvasSizeDelta;
    }
    /// <summary>
    /// Orders selected gameobjects from the selected to move if they have the gameobject implements IMoveable
    /// </summary>
    /// <param name="targetPoint">Point to move to</param>
    void HandleRightClick(RaycastHit info)
    {
        if (!selectedList.Any())
            return;

        //Debug.Log(info.collider.name);



        if (selectedList.First().tag == "Unit") 
        {
            
            unitManager.AssignActions(info, selectedList);

            

            //Make right click pointer spawn in world here here
            Instantiate(moveMarker, info.point, Quaternion.identity);
        }



        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (drawGizmo)
        {
            Gizmos.DrawWireCube(center, new Vector3(halfExtentX, 10, halfExtentZ) * 2);
        }
    }
}
