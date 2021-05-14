includelib msvcrtd
includelib legacy_stdio_definitions.lib
includelib ucrt.lib

extern printf:proc
extern scanf:proc

.data

	arr               DWORD 6 dup (?)
	array_input_msg   DB "A[%d] = ",0
	scanf_fmt         DB "%d",0

	value_input_msg   DB "Value = ",0
	value             DWORD 0

	count_msg   DB "There are %d values greater than %d ",0

.code

main proc
	sub rsp, 28h
	xor rdi, rdi
	lea rbx, arr

	array_input:
		mov rcx, offset array_input_msg
		mov rdx, rdi
		call printf

		mov rcx, offset scanf_fmt
		lea rdx, [rbx + 4*rdi]
		call scanf

		inc rdi
		cmp rdi, 6
		jne array_input

; value input
	mov rcx, offset value_input_msg
	call printf

	mov rcx, offset scanf_fmt
	lea rdx, value
	call scanf

; count calculation
	xor rcx, rcx
	mov edx, [value]

	calc:
		dec rdi
		lea rax, [rbx + 4*rdi]
		cmp edx, [rax]
		jge cont
		inc rcx
		cont:
		test rdi, rdi
		jnz calc

	mov rdx, rcx
	mov rcx, offset count_msg
	mov r8d, value
	call printf

	add rsp, 28h
	ret
main endp

end
