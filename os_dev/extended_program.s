[org 0x7e00]

mov ah, 0x0e
mov al, 'A'
int 0x10

call EnterProtectedMode

jmp $

EnterProtectedMode:
    ret


times 2048 db 0
