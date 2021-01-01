public class Item22 : Item
{
    public override void Awake()
    {
        id = 22;
        base.Awake();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.push = player.push * 10 / 100;
    }
}
