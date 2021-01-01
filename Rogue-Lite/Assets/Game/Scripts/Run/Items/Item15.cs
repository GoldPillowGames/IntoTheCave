public class Item15 : Item
{
    public override void Awake()
    {
        id = 15;
        base.Awake();
    }

    // Survives to a letal Attack
    public override void OnPickUpItem(PlayerStatus player)
    {
        player.survivesToLetalAttack = true;
    }
}
