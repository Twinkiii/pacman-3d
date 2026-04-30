namespace Pacman
{
    public enum AbilityType
    {
        Invincibility,  
        ExtraLife       
    }

    public class AbilityModel
    {
        public AbilityType Type { get; }
        public int Cost { get; }
        public float Cooldown { get; }
        public bool IsOnCooldown { get; private set; }

        public AbilityModel(AbilityType type, int cost, float cooldown)
        {
            Type = type;
            Cost = cost;
            Cooldown = cooldown;
        }

        public void SetCooldown(bool value) => IsOnCooldown = value;
    }
}