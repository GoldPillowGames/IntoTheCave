public class Item10 : Item
{
    public override void Start()
    {
        id = 10;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.luck += player.luck * 5 / 100;
    }
}
