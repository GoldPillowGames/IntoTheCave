public class Item03 : Item
{
    public override void Awake()
    {
        id = 3;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.movementSpeed += player.movementSpeed * 5 / 100;
    }
}
