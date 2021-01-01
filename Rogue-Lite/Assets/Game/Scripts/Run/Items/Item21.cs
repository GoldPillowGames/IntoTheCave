public class Item21 : Item
{
    public override void Awake()
    {
        id = 21;
        base.Awake();
    }

    // Random Object
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.canRoll = false;
        player.health += player.health * 75 / 100;
    }
}
