// compile cmd: g++ <cpp file> -g -masm=intel -std=c++11 -Wall -Werror

#include <iostream>
#include <random>

using namespace std;

int main()
{
    long x[12] = {0};
    constexpr long mask {1};
    constexpr char perm1 = 0b10110001, perm2 = 0b00001111;
    long min {1L<<63};
    random_device rd;
    mt19937_64 mt(rd());
    uniform_int_distribution<long> dist(-128, 128);

    for(int i = 0; i < 5; ++i)
    {
        cout<<endl;
        for(int j = 0; j < 12; ++j)
            cout<<(x[j] = dist(mt))<<" ";

        long max{min};
        asm
        (
            "mov rsi, %1;\n"
            "vpbroadcastq ymm15, %2\n"
            "vpbroadcastq ymm14, %3\n"
            "vpbroadcastq ymm3, %3\n"
            "mov rcx, 3\n"

            "l1: vmovdqu ymm0, [rsi]\n"
            "vpand ymm1, ymm15, ymm0\n"
            "vpsllq ymm1, ymm1, 63\n"
            "vblendvpd ymm2, ymm14, ymm0, ymm1\n"
            "vpmaxsq ymm3, ymm2, ymm3\n"
            "add rsi, 32\n"
            "loop l1\n"
            "vpermq ymm0, ymm3, %4\n"
            "vpmaxsq ymm3, ymm0, ymm3\n"
            "vpermq ymm0, ymm3, %5\n"
            "vpmaxsq ymm3, ymm0, ymm3\n"
            "movq %0, xmm3\n"
            : "=m"(max)
            : "g"(x), "m"(mask), "m"(max), "i"(perm1), "i"(perm2)
        );

        if (max != min)
            cout<<endl<<"Max odd: "<<max<<endl;
        else
            cout<<endl<<"There are no odd numbers."<<endl;
    }
}
