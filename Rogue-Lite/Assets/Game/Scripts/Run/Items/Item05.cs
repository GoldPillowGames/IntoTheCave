public class Item05 : Item
{
    public override void Start()
    {
        id = 5;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.gold += player.gold * 25 / 100;
    }
}
