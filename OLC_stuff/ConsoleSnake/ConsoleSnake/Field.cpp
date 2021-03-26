#include "Field.h"
#include <Windows.h>
#include <algorithm>

Field::Field(size_t rows_, size_t columns_): rows(rows_), columns(columns_), size (rows* columns), buffer(new wchar_t[size])
{
    console = CreateConsoleScreenBuffer(GENERIC_READ | GENERIC_WRITE, 0, nullptr, CONSOLE_TEXTMODE_BUFFER, nullptr);
    if (console == INVALID_HANDLE_VALUE)
        throw "Failed to create console buffer.";

    if (!SetConsoleActiveScreenBuffer(console))
        throw "Setting active console buffer failed.";

    std::fill_n(buffer.get(), columns, '=');
    std::fill_n(buffer.get() + columns*(rows-1), columns, '=');
}

void Field::Clear()
{
    std::fill_n(buffer.get() + columns, (rows-2) * columns, ' ');
    for (size_t i = 0; i < rows*columns; i += columns)
    {
        buffer[i] = '[';
        buffer[i+columns-1] = ']';
    }
}

void Field::Flush()
{
    DWORD charsWritten;
    WriteConsoleOutputCharacter(console, buffer.get(), static_cast<DWORD>(size), { 0, 0 }, &charsWritten);
}

Field::~Field()
{
    if (console != INVALID_HANDLE_VALUE)
        CloseHandle(console);
}
