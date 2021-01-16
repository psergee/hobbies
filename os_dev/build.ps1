nasm loader.s -f bin -o loader.bin
nasm extended_program.s -f bin -o extended_program.bin
Get-Content -Encoding Byte -path "loader.bin","extended_program.bin" | Set-Content -Encoding Byte disk.bin