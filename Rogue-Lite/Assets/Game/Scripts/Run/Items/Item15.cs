public class Item15 : Item
{
    public override void Start()
    {
        id = 15;
        base.Start();
    }

    // Survives to a letal Attack
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.survivesToLetalAttack = true;
    }
}
