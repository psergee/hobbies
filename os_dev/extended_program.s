[org 0x7e00]

%include "gdt.s"

jmp EnterProtectedMode

EnterProtectedMode:
    call EnableA20Line
    cli
    lgdt [gdt_descriptor]
    mov eax, cr0
    or eax, 1
    mov cr0, eax
    jmp codeseg:ProtectedModeMain

EnableA20Line:
    in al, 0x92
    or al, 2
    out 0x92, al
    ret

[bits 32]

ProtectedModeMain:
    mov ax, dataseg
    mov ds, ax
    mov ss, ax
    mov es, ax
    mov fs, ax
    mov gs, ax

    mov [0xb8000], byte 'H'
    mov [0xb8001], byte 'e'

    jmp $

times 2048-($-$$) db 0