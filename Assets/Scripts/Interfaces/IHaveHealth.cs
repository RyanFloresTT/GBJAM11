public interface IHaveHealth {
    public int Health { get; set; }

    public void ModifyHealth(int health);
    public void OnDeath();
}
