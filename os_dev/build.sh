nasm loader.s -f bin -o loader.bin
nasm extended_program.s -f bin -o extended_program.bin
cat loader.bin extended_program.bin > disk.bin
