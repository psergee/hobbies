#pragma once

#include "Field.h"
#include <vector>
#include <utility>

struct Coordinate
{
    size_t x;
    size_t y;

    bool operator==(const Coordinate& other) const
    {
        return ((x == other.x) && (y == other.y));
    }
};

class Food
{
public:
    void Draw(Field& screen);

    const Coordinate& GetLocation();

    void Move(const Coordinate& newLocation);

private:
    Coordinate location = { 0, 0 };
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

    Direction GetDirection();

    void Move();

    bool IsDead() const;

    void Die();

    const std::vector<Coordinate>& GetSegments() const;

    void Grow();

private:
    std::vector<Coordinate> segments = { { 20, 20 }, { 20, 21 }, { 20, 22 } };
    Direction direction = Direction::Right;
    bool isDead = false;
};

