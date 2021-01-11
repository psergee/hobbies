; int to str conversion works for positive numbers only
extern MessageBoxA:proc

.data

	matrix DD 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20
	selector DB 2 
	number_str DB 16 dup(?)

.code

; r8 - number, rdx - str buffer
number_to_string proc
	push rdi
	push rsi

	mov rdi, rdx
	mov rsi, rdx
	mov eax, ecx
	mov r8d, 10

	cvt:
		test eax, eax
		jz converted
		cdq
		idiv r8d
		mov r9d, '0'
		add r9d, edx
		mov byte ptr [rdi], r9b
		inc rdi
	jmp cvt

	converted:
	mov byte ptr [rdi], 0
	dec rdi
	
	reverse:
		cmp rdi, rsi
		jl reversed
		movzx r11, byte ptr[rdi]
		movzx r10, byte ptr[rsi]
		mov byte ptr[rdi], r10b
		mov byte ptr[rsi], r11b
		inc rsi
		dec rdi
		jmp reverse
	reversed:

	pop rsi
	pop rdi
	ret
number_to_string endp


main proc
	push r12

	sub rsp, 128
	mov rsi, offset matrix
	mov r12, 5

	l:
		phaddd xmm0, [rsi]
		phaddd xmm0, xmm0
		psrad xmm0, 2
		pextrd ecx, xmm0, 1
		mov rdx, offset number_str
		call number_to_string
		xor rcx, rcx
		mov rdx, offset number_str
		xor r8, r8
		mov r9, 0
		call MessageBoxA
		add rsi, 16
		dec r12
	jnz l
	add rsp, 128
	pop r12

	ret
main endp

end
