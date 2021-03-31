#define VGA_MEM_ADDR 0xb8000

#include "io.h"

void SetCursorPosition(unsigned short position)
{
    outb(0x3D4, 0x0F);
    outb(0x3D5, static_cast<unsigned char>(position & 0xFF));
    outb(0x3D4, 0x0E);
    outb(0x3D5, static_cast<unsigned char>((position >> 8) & 0xFF));
}
