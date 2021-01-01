public class Item17 : Item
{
    public override void Awake()
    {
        id = 17;
        base.Awake();
    }

    // Increases Player Heal
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.heal += player.heal * 10/100;
    }
}
