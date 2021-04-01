#include "Snake.h"

Snake::Snake()
{
    segments.reserve(128);
}

void Snake::Draw(Field& field)
{
    char body = isDead ? 'x' : 'O';
    for (const auto& segment : segments)
        field[segment.x * field.columns + segment.y] = body;
    const auto& head = segments.back();
    field[head.x * field.columns + head.y] = '@';
}

void Snake::SetDirection(Direction direction_)
{
    direction = direction_;
}

bool Snake::IsDead() const
{
    return isDead;
}

void Snake::Die()
{
    isDead = true;
}

void Snake::Grow()
{
    Coordinate head = segments.back();
    switch (direction)
    {
    case Direction::Up:
        head.x -= 1;
        break;
    case Direction::Down:
        head.x += 1;
        break;
    case Direction::Left:
        head.y -= 1;
        break;
    case Direction::Right:
        head.y += 1;
        break;
    }
    segments.push_back(head);
}

void Snake::Move()
{
    for (auto tail = segments.begin(); tail < segments.end() - 1; ++tail)
        *tail = *(tail + 1);

    auto& head = segments.back();
    switch (direction)
    {
    case Direction::Up:
        head.x -=1;
        break;
    case Direction::Down:
        head.x += 1;
        break;
    case Direction::Left:
        head.y -= 1;
        break;
    case Direction::Right:
        head.y += 1;
        break;
    }
}

const std::vector<Coordinate>& Snake::GetSegments() const
{
    return segments;
}

wchar_t Food::symbol = L'*';

void Food::Draw(Field& screen)
{
    screen[location.x * screen.columns + location.y] = symbol;
}

const Coordinate& Food::GetLocation()
{
    return location;
}

void Food::Move(const Coordinate& newLocation)
{
    location = newLocation;
}
