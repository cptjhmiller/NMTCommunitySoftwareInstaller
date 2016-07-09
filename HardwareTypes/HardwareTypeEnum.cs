using System;
using System.Collections.Generic;
using System.Text;

namespace com.nmtinstaller.csi.HardwareTypes
{
    /*
     * Select hardware type
Popcorn Hour A-100
Popcorn Hour A-110
Popcorn Hour A-200
Popcorn Hour A-200 (USB)
Popcorn Hour A-210
Popcorn Hour A-210 (USB)
Popcorn Hour S-210
Popcorn Hour S-210 (USB)
Popcorn Hour B-110
Popcorn Hour C-200
Popcorn Hour C-200 (USB)
Popcorn Hour A-300
Popcorn Hour A-300 (USB)
Popcorn Hour C-300
Popcorn Hour C-300 (USB)
Popcorn Hour A-400
Popcorn Hour A-400 (USB)
Popcorn Hour A-400 (SD Card)
Popcorn Hour A-410
Popcorn Hour A-410 (USB)
Popcorn Hour A-410 (SD Card)
Popcorn Hour A-500
Popcorn Hour A-500 (USB)
Popcorn Hour A-500 (SD Card)
Popcorn Hour A-500 (Esata)
Popcorn Hour VTEN
Popcorn Hour VTEN (USB)
Popcorn Hour VTEN (SD Card)
Popcorn Hour VTEN (Esata)
eGreat
Digitek HDX
Kaiboer
All others
     */

    public enum HardwareTypeEnum
    {
        A100 = 1,
        A110 = 2,
        A200 = 3,
        A200_USB = 4,
        A210 = 5,
        A210_USB = 6,
        S210 = 7,
        S210_USB = 8,
		B110 = 9,
        C200 = 10,
        C200_USB = 11,
		A300 = 12,
		A300_USB = 13,
		C300 = 14,
		C300_USB = 15,
		A400 = 16,
        A400_USB = 17,
        A400_SDC = 18,
		A410 = 19,
        A410_USB = 20,
        A410_SDC = 21,
        A500 = 22,
        A500_USB = 23,
        A500_SDC = 24,
        A500_ESAT = 25,
        VTEN = 26,
        VTEN_USB = 27,
        VTEN_SDC = 28,
        VTEN_ESAT = 29,
        EGREAT = 30,
        HDX = 31,
        KAI = 32,
        OTHERS = 33
    }
}
