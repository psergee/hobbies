nasm ./src/boot/loader.s -f bin -o loader.bin -i ./src/boot
nasm ./src/boot/extended_program.s -f elf64 -o extended_program.o -i ./src/boot

gcc -ffreestanding -mno-red-zone -m64 -c ./src/kernel/main.cpp -o kernel_main.o
ld -o kernel.tmp -Ttext 0x7e00 extended_program.o kernel_main.o
objcopy -O binary kernel.tmp kernel.bin

cat loader.bin kernel.bin > disk.bin
