using System;

public class Goal
{
    //Статические поля (static - пересенная пренадлежит всему классу, а не отдельной цели, геттер, чтобы можно было только прочитать)
    private static int createdCount = 0;
    public const int MAX_GOALS = 5;

    public static int CreatedCount => createdCount; // Геттер

    // Поля состояния (readonly - задаётся один раз про создании цели и после не меняется)
    private readonly string description;
    private readonly int reward;
    private readonly bool isMandatory;
    private bool isCompleted;

    // Свойства только для чтения (чтобы позволять читать извне)
    public string Description => description;
    public int Reward => reward;
    public bool IsMandatory => isMandatory;
    public bool IsCompleted => isCompleted;

    // Конструкторы
    public Goal(string description, int reward, bool isMandatory)
    {
        if (string.IsNullOrWhiteSpace(description)) // если строка null или пустая или чисто пробелы 
            throw new ArgumentException("Описание не может быть пустым");

        if (reward < 0)
            throw new ArgumentException("Награда не может быть отрицательной");

        this.description = description;
        this.reward = reward;
        this.isMandatory = isMandatory;
        this.isCompleted = false;

        createdCount++;
    }

    // Упрощённый конструктор
    public Goal(string description, int reward) : this(description, reward, true)
    {
    }

    // Методы предметной области и перегрузка
    public void Complete()
    {
        Complete(1.0f);
    }

    public void Complete(float rewardX) // (множитель)
    {
        if (isCompleted)
            throw new InvalidOperationException("Цель уже выполнена");

        if (rewardX <= 0)
            throw new ArgumentException("Множитель награды <= 0");

        isCompleted = true;
    }

    public void CancelCompletion()
    {
        throw new InvalidOperationException("Выполненную цель нельзя отменить");
    }

    // Переопределение ToString (override - означает "переопределить", заменяем стандартное поведение на наше)
    public override string ToString()
    {
        string status = isCompleted ? "выполнено" : "в процессе";
        string type = isMandatory ? "обязательная" : "второстепенная";
        return $"{description} (Награда: {reward}, {type}) — {status}";
    }
}