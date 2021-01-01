public class Item10 : Item
{
    public override void Awake()
    {
        id = 10;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.luck += player.luck * 5 / 100;
    }
}
