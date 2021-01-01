public class Item11 : Item
{
    public override void Awake()
    {
        id = 11;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.enemiesExplodes = true;
    }
}
