public class Item02 : Item
{
    public override void Start()
    {
        id = 2;
        base.Start();
    }


    public override void OnPickUpItem(PlayerStatus player)
    {
        player.damage += player.damage * 5 / 100;
    }
}
