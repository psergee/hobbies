[org 0x7c00]

mov [boot_disk], dl

mov bp, 0x7c00
mov sp, bp

call read_disk

jmp program_space

%include "disk_read.s"

times 510-($-$$) db 0

dw 0xaa55
