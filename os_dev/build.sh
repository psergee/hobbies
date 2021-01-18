nasm ./src/boot/loader.s -f bin -o loader.bin -i ./src/boot
nasm ./src/boot/extended_program.s -f bin -o extended_program.bin -i ./src/boot
cat loader.bin extended_program.bin > disk.bin
