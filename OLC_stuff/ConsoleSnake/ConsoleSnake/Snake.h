#pragma once

#include "Field.h"
#include <vector>
#include <utility>

class Food
{
public:
    Food(size_t row, size_t column);

    void Draw(Field& screen);

    static wchar_t symbol;

private:
    size_t row, column;
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

    void Draw(Field& screen);

    void SetDirection(Direction direction_);

    void Move();

private:
    std::vector<std::pair<size_t, size_t>> segments = { { 20, 20 }, { 20, 21 }, { 20, 22 } };
    Direction direction = Direction::Right;
    bool isDead = false;
};

