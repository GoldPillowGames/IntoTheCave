public class Item04 : Item
{
    public override void Start()
    {
        id = 4;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.goldPerEnemy += player.goldPerEnemy * 10 / 100;
    }
}
