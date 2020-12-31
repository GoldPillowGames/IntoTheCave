public class Item03 : Item
{
    public override void Start()
    {
        id = 3;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.movementSpeed += player.movementSpeed * 5 / 100;
    }
}
