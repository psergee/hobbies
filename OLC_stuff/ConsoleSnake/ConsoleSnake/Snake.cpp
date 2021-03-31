#include "Snake.h"

Snake::Snake()
{
    segments.reserve(128);
}

void Snake::Draw(Field& field)
{
    char body = isDead ? 'x' : 'O';
    for (const auto& segment : segments)
        field[segment.row * field.columns + segment.column] = body;
    const auto& head = segments.back();
    field[head.row * field.columns + head.column] = '@';
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

void Snake::Move()
{
    for (auto tail = segments.begin(); tail < segments.end() - 1; ++tail)
        *tail = *(tail + 1);

    auto& head = segments.back();
    switch (direction)
    {
    case Direction::Up:
        head.row -=1;
        break;
    case Direction::Down:
        head.row += 1;
        break;
    case Direction::Left:
        head.column -= 1;
        break;
    case Direction::Right:
        head.column += 1;
        break;
    }
}

const std::vector<Coordinate>& Snake::GetSegments()
{
    return segments;
}

wchar_t Food::symbol = L'*';

Food::Food(size_t row, size_t column) : location{ row, column }
{
}

void Food::Draw(Field& screen)
{
    screen[location.row * screen.columns + location.column] = symbol;
}

const Coordinate& Food::GetLocation()
{
    return location;
}
