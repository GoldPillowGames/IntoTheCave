public class Item22 : Item
{
    public override void Start()
    {
        id = 22;
        base.Start();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.push = player.push * 10 / 100;
    }
}
