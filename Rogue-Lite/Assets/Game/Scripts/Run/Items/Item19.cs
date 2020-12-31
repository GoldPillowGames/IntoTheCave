public class Item19 : Item
{
    public override void Start()
    {
        id = 19;
        base.Start();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.gold = 0;
        player.health += player.health * 100 / 100;
        player.damage += player.damage * 100 / 100;
    }
}
