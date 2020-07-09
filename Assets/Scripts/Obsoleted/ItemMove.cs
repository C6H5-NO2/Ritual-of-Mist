public sealed class ItemMove : Dragable {
    public bool ForceStopDrag { get; set; }

    protected override void OnMouseDown() {
        base.OnMouseDown();
        if(isUnderUI)
            return;
        ForceStopDrag = false;
    }

    protected override void OnMouseDrag() {
        if(ForceStopDrag)
            return;
        base.OnMouseDrag();
    }
}
