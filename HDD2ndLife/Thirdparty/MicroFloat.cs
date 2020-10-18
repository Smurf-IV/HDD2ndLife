using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HDD2ndLife.Thirdparty
{
    /// <summary>
    /// Sign.Exponent.Mantissa Excess   Bias to Range       Author
    /// 0.5.3 	                -8 	    1 to 16106127360 	George Spelvin
    /// 0.4.4 	                -4 	    1 to 507904 	    George Spelvin
    /// Format is 0.4.4, excess -4, which expresses integers from 0 up to 224-2×(24+1-1) = 507904
    /// In separate, earlier correspondence, Spelvin suggested other similar all-integer formats,
    /// with denormals but without infinities or NANs. The exponent excess is taken to be whatever
    /// value causes the denorms to use the same storage format as the corresponding integer.
    /// Using 0.5.3 format as an example: There is no sign bit, so all values are positive.When the
    /// exponent field is 0, the mantissa is denormalized.So the values(in binary) 00000.000 through 00000.111
    /// express the integers 0 through 7. The next exponent value is 00001 in binary, its 1 bit happens to
    /// correspond with the implicit leading 1 of the (now normalized) mantissa, so values 00001.000 through 00001.111
    /// express integers 8 through 15. Notice how all of these values for the integers 0 through 15 are the same as the normal 8-bit integer representation.
    /// After that, values scale in the normal way: 00010.000 through 00010.111 expresses the even integers 16 through 30
    /// (note that only the first of these corresponds to the integer representation);
    /// 00011.000 through 00011.111 are the multiples of 4 from 32 through 60; and so on.
    /// The highest value is 11111.111 which is 15×230 = 225-2×(23+1-1) = 16106127360.
    /// </summary>
    /// <remarks>
    /// https://mrob.com/pub/math/floatformats.html
    /// https://github.com/Ultz/Bcl/blob/master/src/Ultz.Bcl.Half.Fallback/Half.cs
    /// https://users.rust-lang.org/t/unofficial-ieee-754-reference-implementation-simple-soft-float-0-1-0-includes-optional-python-bindings/35771
    /// </remarks>
    class MicroFloat
    {
        // https://en.wikipedia.org/wiki/Minifloat
        /* 0.4.4
    ...     0000    0001    0010    0011    0100    0101    0110    0111    1000    1001    1010    1011    1100    1101    1110    1111
0000 .+0 	0 	    1 	    2 	    3 	    4 	    5 	    6 	    7       8       9       10      11      12      13      14      15
0001 .+1 	16      17      18      19      20      21      22      23      24      25      26      27      28      29      30      31
0010 .+2 	32      34      36      38      40      42      44      46      48      50      52      54      56      58      60      62
0010 .+4 	64      68      72      76      80      84      88      92      96      100     104     108     112     116     120     124 
0011 .+8 	128     136
0100 .16 	256
0101 .32 	512
0110 .64    1024	
0111 128 	2048
1000 256 	4096
1001 512 	8192
1010 1024 	16384
1011 2048	32768
1100 4096	65536
1101 8192	131072
1110 16384  262144
1111 32768  524288
        Max = 65536 * 16 - 32768 = 10150808
         */

        /* 0.3(+1).5
    ...     00000   00001   00010   00011   00100   00101   00110   00111   01000   01001   01010   01011   01100   01101   01110   01111   10000   10001   10010   10011   10100   10101   10110   10111   11000   11001   11010   11011   11100   11101   11110   11111
 000 .+1 	0 	    1 	    2 	    3 	    4 	    5 	    6 	    7       8       9       10      11      12      13      14      15      16      17      18      19      20      21      22      23      24      25      26      27      28      29      30      31
 001 .+2 	32      34      36      38      40      42      44      46      48      50      52      54      56      58      60      62      64      66      68      70      72      74      76      78      80      82      84      86      88      90      92      94      
 010 .+4 	96      100     104     108     112     116     120     124     128     132     136     140     144     148     152     156     160     164     168     172     176     180     184     188     192     196     200     204     208     212     216     220
 010 .+8 	224
 011 .16 	480
 101 .32 	992
 110 .64    2016	
 111 128 	4096
        Max = 256 * 32 - 128 = 8064
        At time of writing (In case you wanted to use NVME PCIExpress Gen 4 * 4) Max Read 5000MB/s and Write 4400MB/s
        https://www.scan.co.uk/products/2tb-seagate-firecuda-520-m2-2280-pcie-40-x4-nvme-ssd-3d-tlc-5000mb-s-read-4400mb-s-750k-700k-iops
          */
        // [MethodImpl(MethodImplOptions.AggressiveInlining)]

        // Find the log2 fast:
        // https://stackoverflow.com/questions/15967240/fastest-implementation-of-log2int-and-log2float
        // And then modify to also return the remainder
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int Log2Rem(ushort value, out ushort rem)
        {
            var inc = 1;
            for (var i = 0; i < 15; ++i)
            {
                if ((value >>= 1) < 1)
                {
                    rem = (ushort) (value - inc);
                    return i;
                }

                inc <<= 1;
            }
            rem = (ushort)(value - inc);
            return 15;
        }
    }
}
