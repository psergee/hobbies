program_space equ 0x7e00

read_disk:
    mov bx, program_space
    mov al, 16
    mov dl, [boot_disk]
    mov ch, 0
    mov dh, 0
    mov cl, 0x02
    mov ah, 2
    int 0x13

    jnc .good
    mov ah, 0x0e
    mov al, 'E'
    int 0x10

.good:
    ret

boot_disk:
    db 0
