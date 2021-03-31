mkdir ./build

nasm ./src/boot/loader.s -f bin -o ./build/loader.bin -i ./src/boot
nasm ./src/boot/extended_program.s -f elf64 -o ./build/extended_program.o -i ./src/boot

gcc -ffreestanding -mno-red-zone -m64 -c ./src/kernel/io.cpp -o ./build/io.o
gcc -ffreestanding -mno-red-zone -m64 -c ./src/kernel/text_print.cpp -o ./build/text_print.o
gcc -Ttext 0x8000 -ffreestanding -mno-red-zone -m64 -c ./src/kernel/main.cpp -o ./build/kernel_main.o
ld -Tlink.ld

cat ./build/loader.bin ./build/kernel.bin > ./build/disk.bin
