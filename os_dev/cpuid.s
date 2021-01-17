DetectCpuId:
    pushfd
    pop eax

    mov ebx, eax
    xor eax, 1 << 21
    push eax
    popfd
    pushfd
    pop eax
    xor eax, ebx
    jz NoCpuid

    ret

DetectLongMode:
    mov eax, 0x80000001
    cpuid
    test edx, 1 << 29
    jz NoLongMode

   ret

NoLongMode:
    hlt ; Long mode is not supported

NoCpuid:
    hlt ; CPUID is not supported
