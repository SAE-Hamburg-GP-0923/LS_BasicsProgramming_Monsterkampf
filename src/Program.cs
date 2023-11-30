namespace LS_BasicsProgramming_Monsterkampf;

public class Program
{
    #region Constants

    private const string CONSOLE_TITLE = "Monsterkampf";
    private const ConsoleColor DEFAULT_COLOR = ConsoleColor.DarkGray;
    private const ConsoleColor ACCENT_COLOR = ConsoleColor.Cyan;

    #endregion

    private static GameState state = GameState.Init;

    private static Monster monster1 = new Monster();
    private static Monster monster2 = new Monster();

    // Keep track of Method Call for "CreateMonster"
    private static int monsterCreationCounter = 0;

    public static void Main(string[] args)
    {
        while (state != GameState.Quit)
        {
            switch (state)
            {
                case GameState.Init:
                    Init();
                    break;

                case GameState.Running:
                    Fight();
                    break;

                case GameState.Success:
                    ShowFightResult();
                    break;

                case GameState.Quit:
                    Environment.ExitCode = 0;
                    break;
            }
        }
    }

    private static void ConsoleSetup()
    {
        Console.Title = CONSOLE_TITLE;
    }

    #region States

    private static void Init()
    {
        ConsoleSetup();

        WriteToConsole("Wilkommen zum Monsterkampf Simulator 2023");
        WriteToConsole("Bitte gebe die Stats für das erste Monster ein!\n\n");

        monster1 = CreateMonster();
        monster2 = CreateMonster();

        state = GameState.Running;
        return;
    }

    private static float Fight()
    {
        state = GameState.Success;
        return CalculateDamage(monster1.AttackPoints, monster2.DefensePoints);
    }

    private static void ShowFightResult()
    {
    }

    #endregion

    private static Monster CreateMonster()
    {
        Monster monster = new Monster();
        monsterCreationCounter++;

        // Input Variables
        string? input;
        bool validInput = false;
        int selection = -1;
        float stat = -1;

        #region Set Monster Type

        WriteToConsole("\n\nVon welcher Rasse soll dein Monster sein?\n");
        PrintMonsterTypes();

        WriteToConsole($"Bitte gebe eine Zahl von 1 - {Enum.GetValues<MonsterType>().Length} ein: ");

        input = Console.ReadLine();
        validInput = int.TryParse(input, out selection);

        while (!validInput || selection < 0 || selection > Enum.GetValues<MonsterType>().Length ||
               (monsterCreationCounter > 0 && (MonsterType)selection == monster1.Type))
        {
            WriteToConsole($"{selection.ToString()}\n");

            WriteToConsole("Eine richtige Zahl eingeben ist schon schwer oder?\n");
            WriteToConsole($"Bitte gebe eine Zahl von 1 - {Enum.GetValues<MonsterType>().Length} ein: ");

            input = Console.ReadLine();
            validInput = int.TryParse(input, out selection);
        }

        monster.Type = (MonsterType)selection;
        monster.Name = monster.Type.ToString();

        WriteToConsole($"Dein Monster ist von der Rasse {monster.Type.ToString()}\n", ACCENT_COLOR);

        #endregion

        #region Set HP

        WriteToConsole($"Bitte die Lebenspunkte deines Monsters an: ");

        input = Console.ReadLine();
        validInput = float.TryParse(input, out stat);

        while (!validInput || stat <= 0)
        {
            if (stat <= 0)
                WriteToConsole("Dein Monster würde direkt sterben. Gebe einen Wert von über 0 an!\n", ACCENT_COLOR);

            WriteToConsole($"Bitte die Lebenspunkte deines Monsters an: ");
            WriteToConsole($"{selection.ToString()}\n");

            input = Console.ReadLine();
            validInput = float.TryParse(input, out stat);
        }

        monster.HealthPoints = stat;
        WriteToConsole($"Dein Monster besitzt nun {stat} Lebenspunkte!\n", ACCENT_COLOR);

        #endregion

        #region Set AP

        WriteToConsole($"Bitte die Angriffspunkte deines Monsters an: ");

        input = Console.ReadLine();
        validInput = float.TryParse(input, out stat);

        while (!validInput || stat <= 0)
        {
            if (stat <= 0)
                WriteToConsole(
                    "Dein Monster ist ein Pazifist was in einem Kampfsimulator nicht so ganz funktioniert! Gebe einen Wert von über 0 an!\n",
                    ACCENT_COLOR);

            WriteToConsole($"Bitte die Angriffspunkte deines Monsters an: ");
            WriteToConsole($"{selection.ToString()}\n");

            input = Console.ReadLine();
            validInput = float.TryParse(input, out stat);
        }

        monster.AttackPoints = stat;
        WriteToConsole($"Dein Monster besitzt nun {stat} Angriffspunkte!\n", ACCENT_COLOR);

        #endregion

        #region Set DP

        WriteToConsole($"Bitte die Defensivpunkte deines Monsters an: ");

        input = Console.ReadLine();
        validInput = float.TryParse(input, out stat);

        while (!validInput)
        {
            WriteToConsole($"Bitte die Defensivpunkte deines Monsters an: ");
            WriteToConsole($"{selection.ToString()}\n");

            input = Console.ReadLine();
            validInput = float.TryParse(input, out stat);
        }

        monster.DefensePoints = stat;
        WriteToConsole($"Dein Monster besitzt nun {stat} Angriffspunkte!\n", ACCENT_COLOR);

        #endregion

        #region Set AS

        WriteToConsole($"Bitte die Angriffsgeschwindigkeit deines Monsters an: ");

        input = Console.ReadLine();
        validInput = float.TryParse(input, out stat);

        while (!validInput || stat <= 0)
        {
            if (stat <= 0)
                WriteToConsole("Dein Monster ist zu langsam um überhaupt anzugreifen!");

            WriteToConsole($"Bitte die Angriffsgeschwindigkeit deines Monsters an: ");
            WriteToConsole($"{selection.ToString()}\n");

            input = Console.ReadLine();
            validInput = float.TryParse(input, out stat);
        }

        monster.AttackSpeed = stat;
        WriteToConsole($"Dein Monster besitzt nun {stat} Angriffspunkte!\n", ACCENT_COLOR);

        #endregion

        WriteToConsole($"Du hast erfolgreich deinen {monster.Name} erstellt\n");
        monster.PrintInformation();

        return monster;
    }

    private static void PrintMonsterTypes()
    {
        MonsterType[] types = Enum.GetValues<MonsterType>();

        for (int i = 0; i < types.Length; i++)
        {
            WriteToConsole($"{i + 1} : \t{types[i].ToString()}\n", ACCENT_COLOR);
        }
    }

    // Schaden = Angriffsstärke - Abwehrpunkte
    private static float CalculateDamage(float ap, float dp)
    {
        return ap - dp;
    }

    private static void WriteToConsole(string content, ConsoleColor color = DEFAULT_COLOR)
    {
        if (content == String.Empty)
            return;

        Console.ForegroundColor = color;
        Console.Write(content);
        Console.ForegroundColor = DEFAULT_COLOR;
    }
}