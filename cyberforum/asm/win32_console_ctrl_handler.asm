extern SetConsoleCtrlHandler:proc
extern MessageBoxA:proc
extern Sleep:proc

.data

	ctr_c_event_msg        DB "Ctr-C pressed",0
	ctr_break_event_msg    DB "Ctr-break pressed",0
	event_handler_caption  DB "Event handler",0

.code

console_handler proc
	sub rsp, 28h
	test rcx, rcx
	jz ctrlc
	cmp rcx, 1
	jz ctrlbreak
	ctrlc:
		mov rdx, offset ctr_c_event_msg
		jmp msg
	ctrlbreak:
		mov rdx, offset ctr_break_event_msg
	msg:
		xor rcx, rcx
		mov r8, offset event_handler_caption
		mov r9, 0
		call MessageBoxA
	mov rax, 1
	add rsp, 28h
	ret
console_handler endp

main proc
	sub rsp, 28h
	mov rcx, offset console_handler
	mov rdx, 1
	call SetConsoleCtrlHandler
	test rax, rax
	jz error
	mov rcx, 10000
	call Sleep

	error:
	add rsp, 28h
	ret
main endp

end
