public class Item11 : Item
{
    public override void Start()
    {
        id = 11;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.enemiesExplodes = true;
    }
}
