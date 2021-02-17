nasm ./src/boot/loader.s -f bin -o loader.bin -i ./src/boot
nasm ./src/boot/extended_program.s -f elf64 -o extended_program.o -i ./src/boot

gcc -Ttext 0x8000 -ffreestanding -mno-red-zone -m64 -c ./src/kernel/main.cpp -o kernel_main.o
ld -Tlink.ld

cat loader.bin kernel.bin > disk.bin