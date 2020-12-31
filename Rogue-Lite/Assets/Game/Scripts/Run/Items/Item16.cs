public class Item16 : Item
{
    public override void Start()
    {
        id = 16;
        base.Start();
    }

    // Less Initial Life For Enemies
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.lessInitialLifeForEnemies += 10;
    }
}
