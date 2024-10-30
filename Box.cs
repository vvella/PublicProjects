using System;

class Program
{
    static Random random = new Random();
    static int playerHP, enemyHP;

    static void Main()
    {
        while (true)
        {
            if (!StartGame()) return;

            playerHP = 100;
            enemyHP = 100;

            while (playerHP > 0 && enemyHP > 0)
            {
                PlayRound();
            }

            if (!PlayAgain()) break;
        }

        Console.WriteLine("Спасибо за игру!");
    }

    static bool StartGame()
    {
        int startChoice = GetValidatedInput("Приветствуем нового бойца! Правила игры просты: блокируй удары противника и угадывай, какое место не заблокировал он. Начнем?\n1: Летс ГОУ\n2: Выход");
        return startChoice == 1;
    }

    static void PlayRound()
    {
        int playerBlock = GetValidatedInput("Выберите блок:\n1: Голова\n2: Тело");
        int playerAttack = GetValidatedInput("Выберите место для удара:\n1: Голова\n2: Тело");

        int enemyBlock = random.Next(1, 3);
        int enemyAttack = random.Next(1, 3);

        int playerDamage = (enemyBlock == playerAttack) ? 0 : random.Next(10, 21);
        int enemyDamage = (playerBlock == enemyAttack) ? 0 : random.Next(10, 21);

        playerHP -= enemyDamage;
        enemyHP -= playerDamage;

        DisplayRoundResult(playerBlock, playerAttack, enemyBlock, enemyAttack, playerDamage, enemyDamage);
        CheckGameOver();
    }

    static void DisplayRoundResult(int playerBlock, int playerAttack, int enemyBlock, int enemyAttack, int playerDamage, int enemyDamage)
    {
        Console.WriteLine($"\nВы заблокировали удар в {(playerBlock == 1 ? "голову" : "тело")}. Вам нанесли удар в {(enemyAttack == 1 ? "голову" : "тело")} и нанесли {enemyDamage} урона. Осталось {playerHP} HP.");
        Console.WriteLine($"Враг заблокировал удар в {(enemyBlock == 1 ? "голову" : "тело")}. Вы нанесли удар в {(playerAttack == 1 ? "голову" : "тело")} и нанесли {playerDamage} урона. У врага осталось {enemyHP} HP.");
    }

    static void CheckGameOver()
    {
        if (playerHP <= 0 && enemyHP <= 0)
            Console.WriteLine("\nЭто ничья!");
        else if (playerHP <= 0)
            Console.WriteLine("\nВы проиграли!");
        else if (enemyHP <= 0)
            Console.WriteLine("\nВы победили!");
    }

    static bool PlayAgain()
    {
        int replayChoice = GetValidatedInput("\nСыграть ещё раз?\n1: Да\n2: Нет");
        return replayChoice == 1;
    }

    static int GetValidatedInput(string message)
    {
        int choice;
        while (true)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            if (int.TryParse(input, out choice) && (choice == 1 || choice == 2))
                return choice;

            Console.WriteLine("Не было таких вариантов, брат!");
        }
    }
}
