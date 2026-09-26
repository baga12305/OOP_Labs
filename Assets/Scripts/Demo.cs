using System;
using UnityEngine;

public class Demo : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("       ПРОВЕРКА КОРРЕКТНОЙ РАБОТЫ         ");

        LevelGoals level = new LevelGoals(3);
        Goal g1 = new Goal("Зачистить комнату", 100, true);
        Goal g2 = new Goal("Найти ключ", 50, true);
        Goal g3 = new Goal("Собрать монеты", 30, false);

        level.AddGoal(g1);
        level.AddGoal(g2);
        level.AddGoal(g3);

        Debug.Log($"Создано целей за сессию (static): {Goal.CreatedCount}");
        Debug.Log($"Начальный прогресс: {level.GetCompletionPercentage():F1}%");

        level.CompleteGoal("Зачистить комнату");
        Debug.Log($"Состояние цели после выполнения: {g1}");
        Debug.Log($"Прогресс уровня: {level.GetCompletionPercentage():F1}%");

        Debug.Log("\n        ПРОВЕРКА try-catch            ");

        // Попытка выполнить цель повторно
        try { g1.Complete(); }
        catch (Exception e) { Debug.Log($"[Ошибка отбита]: {e.Message}"); }
        Debug.Log($"Состояние после попытки повторного выполнения: {g1}"); 

        // Попытка отменить выполнение цели
        try { g1.CancelCompletion(); }
        catch (Exception e) { Debug.Log($"[Ошибка отбита]: {e.Message}"); }
        Debug.Log($"Состояние после попытки отмены: {g1}"); 

        // Попытка создать цель с пустым описанием
        try { Goal invalidGoal = new Goal("", 100); }
        catch (Exception e) { Debug.Log($"[Ошибка отбита]: {e.Message}"); }

        // Попытка создать цель с отрицательной наградой
        try { Goal invalidReward = new Goal("Ошибка", -50); }
        catch (Exception e) { Debug.Log($"[Ошибка отбита]: {e.Message}"); }

        // Попытка добавить цель сверх лимита
        try
        {
            Goal noLimitsGoal = new Goal("Лишняя цель", 10, false);
            level.AddGoal(noLimitsGoal);
        }
        catch (Exception e) { Debug.Log($"[Ошибка отбита]: {e.Message}"); }
        Debug.Log($"Итого целей в контейнере: {level.Count}/{level.Limits}"); 
    }
}

/* 
 Поле description - описание не может быть пустым, проверяется в конструкторе Goal
 Поле reward - награда не может быть отрицательной, проверяется в конструкторе Goal
 Поле isCompleted - выполненную цель нельзя выполнить повторно, проверяется в методе Complete(float)
 Поле isCompleted - выполненную цель нельзя отменить, проверяется в методе CancelCompletion()
 Поле _goals - нельзя добавить целей больше лимита, проверяется в методе AddGoal()
 */