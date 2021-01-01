public class Item06 : Item
{
    public override void Awake()
    {
        id = 6;
        base.Awake();
    }

    public override void OnPickUpItem(PlayerStatus player)
    {
        player.healthSteal += 5;
    }
}
