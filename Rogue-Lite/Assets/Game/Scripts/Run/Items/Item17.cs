public class Item17 : Item
{
    public override void Start()
    {
        id = 17;
        base.Start();
    }

    // Increases Player Heal
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.heal += player.heal * 10/100;
    }
}
