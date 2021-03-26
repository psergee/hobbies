#include "Snake.h"

Snake::Snake()
{
    segments.reserve(128);
}

void Snake::Draw(Field& field)
{
    for (const auto& segment : segments)
        field[segment.first * field.GetColumnsCount() + segment.second] = 'O';
    const auto& head = segments.back();
    field[head.first * field.GetColumnsCount() + head.second] = '@';
}

void Snake::SetDirection(Direction direction_)
{
    direction = direction_;
}

void Snake::Move()
{
    for (auto tail = segments.begin(); tail < segments.end() - 1; ++tail)
        *tail = *(tail + 1);

    auto& head = segments.back();
    switch (direction)
    {
    case Direction::Up:
        head.first -=1;
        break;
    case Direction::Down:
        head.first += 1;
        break;
    case Direction::Left:
        head.second -= 1;
        break;
    case Direction::Right:
        head.second += 1;
        break;
    }
}

wchar_t Food::symbol = L'*';

Food::Food(size_t row_, size_t column_): row(row_), column(column_)
{
}

void Food::Draw(Field& screen)
{
    screen[row * screen.GetColumnsCount() + column] = symbol;
}
