public class Item13 : Item
{
    public override void Awake()
    {
        id = 13;
        base.Awake();
    }

    // +50% health -50% damage
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.health += player.health * 50 / 100;
        player.damage -= player.damage * 50 / 100;
    }
}
