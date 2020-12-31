public class Item14 : Item
{
    public override void Start()
    {
        id = 14;
        base.Start();
    }

    // Agility
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.agility += player.agility * 10 / 100;
    }
}
