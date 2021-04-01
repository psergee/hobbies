#include <thread>
#include <chrono>
#include <Windows.h>
#include "GameLogic.h"

using namespace std::chrono_literals;

GameLogic::GameLogic(): field(30, 100), xDist(1, field.rows-1), yDist(1, field.columns-1)
{
    food.Move({ 20, 50 });
}

void GameLogic::MainLoop()
{
    while (!snake.IsDead())
    {
        switch (snake.GetDirection())
        {
        case Direction::Up:
        case Direction::Down:
            std::this_thread::sleep_for(200ms);
        case Direction::Left:
        case Direction::Right:
            std::this_thread::sleep_for(120ms);
        }
        
        if (GetAsyncKeyState(VK_UP) & 0x1)
            snake.SetDirection(Direction::Up);
        else if (GetAsyncKeyState(VK_RIGHT) & 0x1)
            snake.SetDirection(Direction::Right);
        else if (GetAsyncKeyState(VK_DOWN) & 0x1)
            snake.SetDirection(Direction::Down);
        else if (GetAsyncKeyState(VK_LEFT) & 0x1)
            snake.SetDirection(Direction::Left);
        snake.Move();

        DetectCollision();
        CheckFood();

        field.Clear();
        snake.Draw(field);
        food.Draw(field);

        field.Flush();
    }

    DrawCompleteScreen();
}

void GameLogic::DetectCollision()
{
    const auto snakeSegments = snake.GetSegments();
    const auto head = snakeSegments.back();
    if (head.x <= 0 || head.x >= field.rows-1 || head.y <= 0 || head.y >= field.columns-1)
        snake.Die();

    if (std::find(snakeSegments.cbegin(), snakeSegments.cend() - 1, head) != snakeSegments.cend()-1)
        snake.Die();
}

void GameLogic::CheckFood()
{
    const auto& head = snake.GetSegments().back();
    const auto& foodLocation = food.GetLocation();
    if (head == foodLocation)
    {
        snake.Grow();
        RandomizeFoodLocation();
    }
}

void GameLogic::RandomizeFoodLocation()
{
    Coordinate newFoodLocation;
    const auto& snakeSegments = snake.GetSegments();
    newFoodLocation.x = xDist(rndEngine);
    newFoodLocation.y = yDist(rndEngine);
    while (std::find(snakeSegments.cbegin(), snakeSegments.cend(), newFoodLocation) != snakeSegments.cend())
    {
        newFoodLocation.x = xDist(rndEngine);
        newFoodLocation.y = yDist(rndEngine);
    }
    food.Move(newFoodLocation);
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
