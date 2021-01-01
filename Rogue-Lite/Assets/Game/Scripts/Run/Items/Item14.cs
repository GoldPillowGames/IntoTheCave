public class Item14 : Item
{
    public override void Awake()
    {
        id = 14;
        base.Awake();
    }

    // Agility
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.agility += player.agility * 10 / 100;
    }
}
