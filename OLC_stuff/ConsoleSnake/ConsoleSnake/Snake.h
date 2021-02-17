#pragma once

#include "ScreenBuffer.h"
#include <vector>
#include <utility>

class Food
{
public:
    Food(size_t row, size_t column);

    void Draw(ScreenBuffer& screen);

private:
    size_t row, column;
    wchar_t symbol = '*';
};

enum class Direction
{
    Left,
    Up,
    Right,
    Down
};

class Snake
{
public:
    Snake();

    void Draw(ScreenBuffer& screen);

    void SetDirection(Direction direction_);

    void Move();

private:
    std::vector<std::pair<size_t, size_t>> segments = { { 30, 30 }, { 30, 31 }, { 30, 32 } };
    Direction direction = Direction::Right;
    bool isDead = false;
};

