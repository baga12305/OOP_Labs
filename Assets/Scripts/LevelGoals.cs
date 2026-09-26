using System;

public class LevelGoals
{
    private readonly Goal[] _goals;
    private int _count;

    public int Count => _count;
    public int Limits => _goals.Length;

    public LevelGoals(int goalsLimits = Goal.MAX_GOALS)
    {
        if (goalsLimits <= 0)
            throw new ArgumentOutOfRangeException(nameof(goalsLimits), "Емкость должна быть больше 0.");

        _goals = new Goal[goalsLimits];
        _count = 0;
    }

    public void AddGoal(Goal goal)
    {
        if (goal == null)
            throw new ArgumentNullException(nameof(goal), "Цель не может быть null.");

        if (_count >= _goals.Length)
            throw new InvalidOperationException("Достигнут лимит целей на уровне.");

        _goals[_count] = goal;
        _count++;
    }

    public Goal FindGoal(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            return null;

        for (int i = 0; i < _count; i++)
        {
            if (_goals[i].Description.Equals(description, StringComparison.OrdinalIgnoreCase))
                return _goals[i];
        }

        return null;
    }

    public bool CompleteGoal(string description, float multiplier = 1.0f)
    {
        Goal goal = FindGoal(description);

        if (goal == null)
            return false;

        goal.Complete(multiplier);
        return true;
    }

    public float GetCompletionPercentage()
    {
        if (_count == 0)
            return 0f;

        int completedCount = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_goals[i].IsCompleted)
                completedCount++;
        }

        return ((float)completedCount / _count) * 100f;
    }

    public Goal[] GetIncompleteMandatoryGoals()
    {
        // Сначала считаем, сколько таких целей, чтобы создать массив нужного размера
        int count = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_goals[i].IsMandatory && !_goals[i].IsCompleted)
                count++;
        }

        // Создаем массив и заполняем его
        Goal[] incompleteGoals = new Goal[count];
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (_goals[i].IsMandatory && !_goals[i].IsCompleted)
            {
                incompleteGoals[index] = _goals[i];
                index++;
            }
        }

        return incompleteGoals;
    }

    public bool IsLevelCompleted()
    {
        if (_count == 0)
            return false;

        for (int i = 0; i < _count; i++)
        {
            if (_goals[i].IsMandatory && !_goals[i].IsCompleted)
                return false;
        }

        return true;
    }

    public Goal[] GetGoals()
    {
        Goal[] copy = new Goal[_count];
        Array.Copy(_goals, copy, _count);
        return copy;
    }
}