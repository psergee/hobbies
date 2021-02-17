#pragma once

#include <Windows.h>
#include <cstdint>
#include <memory>

class ScreenBuffer
{
public:
    ScreenBuffer(size_t width, size_t height);

    void Clear();

    void Flush();

    size_t GetColumnsCount()
    {
        return columns;
    }

    wchar_t& operator[](size_t index)
    {
        if (index >= size)
            throw "Screen buffer overflow.";

        return buffer[index];
    }

    ~ScreenBuffer();

private:
    std::unique_ptr<wchar_t[]> buffer;
    size_t rows, columns, size;
    HANDLE console;
};

