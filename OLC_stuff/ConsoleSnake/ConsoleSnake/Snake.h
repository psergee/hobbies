#pragma once

#include "Field.h"
#include <vector>
#include <utility>

struct Coordinate
{
    size_t row;
    size_t column;
};

class Food
{
public:
    Food(size_t row, size_t column);

    void Draw(Field& screen);

    const Coordinate& GetLocation();

private:
    Coordinate location;
    static wchar_t symbol;
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

    bool IsDead() const;

    void Die();

    const std::vector<Coordinate>& GetSegments();

private:
    std::vector<Coordinate> segments = { { 20, 20 }, { 20, 21 }, { 20, 22 } };
    Direction direction = Direction::Right;
    bool isDead = false;
};

