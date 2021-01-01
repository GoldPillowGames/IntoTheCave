public class Item19 : Item
{
    public override void Awake()
    {
        id = 19;
        base.Awake();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.gold = 0;
        player.health += player.health * 100 / 100;
        player.damage += player.damage * 100 / 100;
    }
}
