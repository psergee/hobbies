#pragma once

#include "Snake.h"

class GameLogic
{
public:
    GameLogic();

    void MainLoop();

    bool CheckWallCollission();

    void DrawCompleteScreen();

private:
    Field field;
    Food food;
    Snake snake;
};

