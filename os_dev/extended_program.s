[org 0x7e00]

jmp EnterProtectedMode

%include "gdt.s"

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

%include "cpuid.s"
%include "paging.s"

ProtectedModeMain:
    mov ax, dataseg
    mov ds, ax
    mov ss, ax
    mov es, ax
    mov fs, ax
    mov gs, ax

    call DetectCpuId
    call DetectLongMode
    call SetUpIdentityPaging
    call EditGDT

    jmp codeseg:Main64Bit

[bits 64]

Main64Bit:
    mov edi, 0xb8000
    mov rax, 0x1f201f201f201f20
    mov ecx, 512
    rep stosq

    hlt

times 2048-($-$$) db 0
