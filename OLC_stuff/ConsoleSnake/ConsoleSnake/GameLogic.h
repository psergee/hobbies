#pragma once

#include <random>
#include "Snake.h"

class GameLogic
{
public:
    GameLogic();

    void MainLoop();

private:
    void DetectCollision();

    void CheckFood();

    void RandomizeFoodLocation();

    void DrawCompleteScreen();

    Field field;
    Food food;
    Snake snake;
    std::default_random_engine rndEngine;
    std::uniform_int_distribution<size_t> xDist, yDist;
};

