#include <windows.h>
#include <iostream>
#include <vector>
#include <thread>
#include "Snake.h"

using namespace std;

int main()
{
    ScreenBuffer screen(50, 150);
    Food food(20, 20);
    Snake snake;

    while (true)
    {
        std::this_thread::sleep_for(200ms);

        screen.Clear();

        food.Draw(screen);

        if (GetAsyncKeyState(VK_UP) & 0x8000)
            snake.SetDirection(Direction::Up);
        else if(GetAsyncKeyState(VK_RIGHT) & 0x8000)
            snake.SetDirection(Direction::Right);
        else if (GetAsyncKeyState(VK_DOWN) & 0x8000)
            snake.SetDirection(Direction::Down);
        else if (GetAsyncKeyState(VK_LEFT) & 0x8000)
            snake.SetDirection(Direction::Left);
        snake.Move();
        snake.Draw(screen);

        screen.Flush();
    }
}
