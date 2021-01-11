#include <iostream>
#include <fstream>
#include <iterator>
#include <iomanip>
 
int main(int argc, char** argv)
{
    if (argc != 2)
    {
        std::cout<<"Usage: hexdump file_name"<<std::endl;
        return -1;
    }

    std::ifstream input_file(argv[1]);

    if (!input_file.is_open())
    {
        std::cerr<<"Unable to open the file: "<<argv[1]<<std::endl;
        return -2;
    }

    std::istreambuf_iterator<char> bin_file_iterator(input_file);
    short offset = 0;
    std::string text;
    unsigned char byte;
    std::cout<<std::hex<<std::setfill('0')<<std::setw(5)<<offset<<":  ";;
    while(bin_file_iterator != std::istreambuf_iterator<char>())
    {
        byte = *bin_file_iterator;
        std::cout<<std::setw(2)<<static_cast<int>(byte)<<" ";
        if(byte > 0x7e || byte < 0x20)
            text += '.';
        else
            text += byte;
        ++bin_file_iterator, ++offset;
        if(offset % 16 == 0)
        {
            std::cout<<" |"<<text<<"|"<<std::endl;
            std::cout<<std::setw(5)<<offset<<":  ";
            text.clear();
        }
    }
    while((offset++ % 16) != 0) std::cout<<"   ";
    std::cout<<" |"<<text<<"|"<<std::endl;
    input_file.close();
    return 0;
}
