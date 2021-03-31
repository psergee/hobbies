#include <thread>
#include <chrono>
#include <Windows.h>
#include "GameLogic.h"

using namespace std::chrono_literals;

GameLogic::GameLogic(): field(30, 100), food(20, 20)
{
}

void GameLogic::MainLoop()
{
    while (!snake.IsDead())
    {
        std::this_thread::sleep_for(200ms);

        if (GetAsyncKeyState(VK_UP) & 0x8000)
            snake.SetDirection(Direction::Up);
        else if (GetAsyncKeyState(VK_RIGHT) & 0x8000)
            snake.SetDirection(Direction::Right);
        else if (GetAsyncKeyState(VK_DOWN) & 0x8000)
            snake.SetDirection(Direction::Down);
        else if (GetAsyncKeyState(VK_LEFT) & 0x8000)
            snake.SetDirection(Direction::Left);
        snake.Move();

        CheckWallCollission();

        field.Clear();
        snake.Draw(field);
        food.Draw(field);

        field.Flush();
    }

    DrawCompleteScreen();
}

bool GameLogic::CheckWallCollission()
{
    const auto snakeSegments = snake.GetSegments();
    const auto head = snakeSegments.back();
    if (head.row <= 0 || head.row >= field.rows-1 || head.column <= 0 || head.column >= field.columns-1)
    {
        snake.Die();
        return true;
    }
    return false;
}

void GameLogic::DrawCompleteScreen()
{
    while (!(GetAsyncKeyState(VK_ESCAPE) & 0x1))
    {
        std::this_thread::sleep_for(200ms);
        field.Clear();
        snake.Draw(field);
        field.Flush();
    }
}
