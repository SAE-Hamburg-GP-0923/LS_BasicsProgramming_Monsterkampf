namespace LS_BasicsProgramming_Monsterkampf;

public class Monster
{
    public string Name = "Not Set";

    public MonsterType Type;
    
    public float HealthPoints;
    public float AttackPoints;
    public float DefensePoints;
    public float AttackSpeed;

    public void PrintInformation()
    {
        Console.WriteLine($"{Name}");
        Console.WriteLine($"Lebenspunkte: {HealthPoints}");
        Console.WriteLine($"Angriffspunkte: {AttackPoints}");
        Console.WriteLine($"Defensivepunkte: {DefensePoints}");
        Console.WriteLine($"Angriffsgeschwindigkeit: {AttackSpeed}");
    }
}