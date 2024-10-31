public enum ESeletableType
{
    Unit,
    Building
}

public interface ISelectable
{
    public ESeletableType SeletableType { get; }
    public void Select();
    public void Deselect();
}
