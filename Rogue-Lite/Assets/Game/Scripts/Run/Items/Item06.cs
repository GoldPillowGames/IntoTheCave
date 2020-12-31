public class Item06 : Item
{
    public override void Start()
    {
        id = 6;
        base.Start();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.healthSteal += 5;
    }
}
