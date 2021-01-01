public class Item04 : Item
{
    public override void Awake()
    {
        id = 4;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.goldPerEnemy += player.goldPerEnemy * 10 / 100;
    }
}
