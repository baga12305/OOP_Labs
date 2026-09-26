using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelGoalsView : MonoBehaviour
{
    [SerializeField] private int maxGoals = 5;

    private LevelGoals _levelGoals;

    public float Progress => _levelGoals != null ? _levelGoals.GetCompletionPercentage() : 0f;

    private void Start()
    {
        _levelGoals = new LevelGoals(maxGoals);

        try
        {
            _levelGoals.AddGoal(new Goal("Победить босса", 500, true));
            _levelGoals.AddGoal(new Goal("Спасти пленника", 200, true));
            _levelGoals.AddGoal(new Goal("Открыть сундук", 50, false));
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при инициализации целей: {e.Message}");
        }
    }

    private void Update()
    {
        // По нажатию на клавишу E выполняем следующую невыполненную цель
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Goal[] goals = _levelGoals.GetGoals();

            foreach (var goal in goals)
            {
                if (!goal.IsCompleted)
                {
                    try
                    {
                        _levelGoals.CompleteGoal(goal.Description);
                        Debug.Log($"Выполнена цель: '{goal.Description}'. Общий прогресс: {Progress:F1}%");

                        if (_levelGoals.IsLevelCompleted())
                        {
                            Debug.Log("Уровень успешно завершен! Все обязательные цели выполнены.");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"Не удалось выполнить цель: {e.Message}");
                    }

                    break; // За одно нажатие выполняем только одну цель
                }
            }
        }
    }
}