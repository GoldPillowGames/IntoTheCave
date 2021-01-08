public class Item04 : Item
{
    public override void Awake()
    {
        id = 4;
        base.Awake();
    }

    // Gold Per Enemy
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.goldPerEnemy += 10;
    }
}
