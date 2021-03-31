#pragma once

#include <Windows.h>
#include <cstdint>
#include <memory>

class Field
{
public:
    Field(size_t width, size_t height);

    void Clear();

    void Flush();

    wchar_t& operator[](size_t index)
    {
        if (index >= size)
            throw "Screen buffer overflow.";

        return buffer[index];
    }

    ~Field();

    const size_t rows, columns, size;

private:
    std::unique_ptr<wchar_t[]> buffer;
    HANDLE console;
};

