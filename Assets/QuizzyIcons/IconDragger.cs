using UnityEngine;
using UnityEngine.UIElements;

public class IconDragger : MouseManipulator
{
    Controller controller;

    private Vector2 startPos;
    private Vector2 elemStartPosGlobal;
    private Vector2 elemStartPosLocal;

    VisualElement dragArea;
    VisualElement iconContainer;
    VisualElement dropZone;

    bool isActive;

    public IconDragger(VisualElement root, Controller controller)
    {
        this.controller = controller;

        dragArea = root.Q("DragArea");
        dropZone = root.Q("DropBox");

        isActive = false;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected void OnMouseDown(MouseDownEvent e)
    {
        // get reference to the icon container for later
        iconContainer = target.parent;

        // get mouse start pos
        startPos = e.localMousePosition;

        // get both target start pos
        //elemStartPosGlobal = new Vector2(target.worldBound.xMin, target.worldBound.yMin);
        elemStartPosGlobal = target.worldBound.position;
        elemStartPosLocal = target.layout.position;

        // enable dragArea
        dragArea.style.display = DisplayStyle.Flex;
        dragArea.Add(target);

        // correct pos after repositioning
        target.style.top = elemStartPosGlobal.y;
        target.style.left = elemStartPosGlobal.x;

        isActive = true;
        target.CaptureMouse();
        e.StopPropagation();
    }

    protected void OnMouseMove(MouseMoveEvent e)
    {
        if (!isActive || !target.HasMouseCapture())
            return;

        Vector2 diff = e.localMousePosition - startPos;

        target.style.top = target.layout.y + diff.y;
        target.style.left = target.layout.x + diff.x;

        e.StopPropagation();
    }

    protected void OnMouseUp(MouseUpEvent e)
    {
        if (!isActive || !target.HasMouseCapture())
            return;

        if (target.worldBound.Overlaps(dropZone.worldBound))
        {
            dropZone.Add(target);

            target.style.top = dropZone.contentRect.center.y - target.layout.height / 2;
            target.style.left = dropZone.contentRect.center.x - target.layout.width / 2;

            Debug.Log("The provided answer is: " + ((Question)target.userData).display_answer);

            controller.CheckAnswer(((Question)target.userData).answer);

        } else
        {
            iconContainer.Add(target);

            target.style.top = elemStartPosLocal.y - iconContainer.contentRect.position.y;
            target.style.left = elemStartPosLocal.x - iconContainer.contentRect.position.x;
        }

        

        isActive = false;
        target.ReleaseMouse();
        e.StopPropagation();

        dragArea.style.display = DisplayStyle.None;
    }
}
