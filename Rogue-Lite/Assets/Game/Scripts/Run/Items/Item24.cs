public class Item24 : Item
{
    public override void Start()
    {
        id = 24;
        base.Start();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.enemiesThreshold -= 3;
    }
}
