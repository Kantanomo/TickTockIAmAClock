using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace TickTockIAmAClock
{
    public static class WatcherThread
    {
        public static bool Hooked = false;
        public static bool Caved = false;
        private static string ProfileName = "";
        const int ObjectTableAddress = 0x3003CF3C;
        const int ObjectTableItemSize = 12;
        const int PlayerTableDatumAddress = 0x30002B44;
        const int PlayerTableGamertagAddress = 0x30002B5C;
        const int PlayerTableItemSize = 0x204;
        public static void Run()
        {
            while (true)
            {
                EvalHooked();
                if (Hooked)
                {
                    //try
                    //{

                        ChangeTickRates();

                        if (Program.advancedMode || Program.MinimalMode)
                        {
                            if (!Caved)
                            {
                                CaveCalls();
                            }

                            if (!Program.MinimalMode)
                            {
                                UpdateUserObjectAddress();
                                Program.Form.updateControl<CheckBox>("isInterpolating",
                                    Program.Memory.ReadByte(0x4df128, true) == 1 ? true : false);
                            }
                        }
                    //}
                    //catch (Exception e)
                    //{

                    //}
                }

                Thread.Sleep(1000);
            }
        }

        private static int FreeMemoryAddress = 0x14000;
        private static int CurrentOffset = 0;
        private static byte[] GetTickRateASM = {0xB8, 0x3C, 0x00, 0x00, 0x00, 0xC3};

        private static CallBase[] GetTickRateCalls = new[]
        {
            CallBase.New("",0x5CBB3,""),
            CallBase.New("",0x6a6c0,""),
            CallBase.New("",0x6A77D,""),
            CallBase.New("",0x6AA4D,""),
            CallBase.New("",0x6AB4B,""),
            CallBase.New("",0x6ABF3,""),
            CallBase.New("",0x6B312,""),
            CallBase.New("",0x6BAC2,""),
            CallBase.New("",0x6DA2E,""),
            CallBase.New("",0x6DAA6,""),
            CallBase.New("",0x6BD39,""),
            CallBase.New("",0x6DB72,""),
            CallBase.New("",0x6DB8C,""),
            CallBase.New("",0x6DC78,""),
            CallBase.New("",0x6FDAD,""),
            CallBase.New("",0x7070E,""),
            CallBase.New("",0x7255f,""),
            CallBase.New("",0x751BE,""),
            CallBase.New("",0x7523A,""),
            CallBase.New("",0x75244,""),
            CallBase.New("",0x75BB8,""),
            CallBase.New("",0x75C04,""),
            CallBase.New("",0x75C83,""),
            CallBase.New("",0x75CBD,""),
            CallBase.New("",0xA3D6D,""),
            CallBase.New("",0xA4A4B,""),
            CallBase.New("",0xb43bc,""),
            CallBase.New("",0xb4457,""),
            CallBase.New("",0xb4473,""),
            CallBase.New("",0xb4481,""),
            CallBase.New("",0xb44c0,""),
            CallBase.New("",0xb461d,""),
            CallBase.New("",0xb5dd3,""),
            CallBase.New("",0xb5f93,""),
            CallBase.New("",0xb5fb7,""),
            CallBase.New("",0xb61b1,""),
            CallBase.New("",0xb6864,""),
            CallBase.New("",0xd340a,""),
            CallBase.New("",0xd6e24,""),
            CallBase.New("",0xd7135,""),
            CallBase.New("",0xdcfd4,""),
            CallBase.New("",0xe0963,""),
            CallBase.New("",0xf4ed8,""),
            CallBase.New("",0x10ae0a,""),
            CallBase.New("",0x10ca5f,""),
            CallBase.New("",0x10ca8c,""),
            CallBase.New("",0x10d6f0,""),
            CallBase.New("",0x10f719,""),
            CallBase.New("",0x10f97d,""),
            CallBase.New("",0x110493,""),
            CallBase.New("",0x110af2,""),
            CallBase.New("",0x112BC1,""),
            CallBase.New("",0x13e7c5,""),
            CallBase.New("",0x14c083,""),
            CallBase.New("",0x14d9d8,""),
            CallBase.New("",0x1544ae,""),
            CallBase.New("",0x1588c2,""),
            CallBase.New("",0x15bb41,""),
            CallBase.New("",0x15c1f6,""),
            CallBase.New("",0x161615,""),
            CallBase.New("",0x161626,""),
            CallBase.New("",0x16163f,""),
            CallBase.New("",0x161651,""),
            CallBase.New("",0x170051,""),
            CallBase.New("",0x170d4f,""),
            CallBase.New("",0x175f47,""),
            CallBase.New("",0x175fba,""),
            CallBase.New("",0x18e650,""),
            CallBase.New("",0x18e89a,""),
            CallBase.New("",0x226350,""),
            CallBase.New("",0x22cc72,""),
            CallBase.New("",0x22d4eb,""),
            CallBase.New("",0x22d50b,""),
            CallBase.New("",0x37c11a,"")
        };
        private static int[] NewGetTickRates = new int[GetTickRateCalls.Length];

        private static byte[] AdjustTickRateIntASM = {0xB8, 0x3C, 0x00, 0x00, 0x00, 0x0F, 0xAF, 0x44, 0x24, 0x04, 0xC3};

        private static CallBase[] AdjustTickRateIntCalls = new[]
        {
            CallBase.New("", 0x55d02, ""),
            CallBase.New("", 0x5e132, ""),
            CallBase.New("", 0x6b0ab, ""),
            CallBase.New("", 0x6da4e, ""),
            CallBase.New("", 0x6e947, ""),
            CallBase.New("", 0x71197, ""),
            CallBase.New("", 0x711ea, ""),
            CallBase.New("", 0x724e5, ""),
            CallBase.New("", 0x724f7, ""),
            CallBase.New("", 0x7250f, ""),
            CallBase.New("", 0x72aee, ""),
            CallBase.New("", 0x72B59, ""),
            CallBase.New("", 0xA6C0F, ""),
            CallBase.New("", 0xA791C, ""),
            CallBase.New("", 0xB49C7, ""),
            CallBase.New("", 0xB4A1C, ""),
            CallBase.New("", 0xB4a49, ""),
            CallBase.New("", 0xB659E, ""),
            CallBase.New("", 0xC3BF2, ""),
            CallBase.New("", 0xD46FE, ""),
            CallBase.New("", 0xD52da, ""),
            CallBase.New("", 0xD552E, ""),
            CallBase.New("", 0xD725C, ""),
            CallBase.New("", 0xD7366, ""),
            CallBase.New("", 0x10FCF8, ""),
            CallBase.New("", 0x10FE02, ""),
            CallBase.New("", 0x1109DD, ""),
            CallBase.New("", 0x1448FD, ""),
            CallBase.New("", 0x153412, ""),
            CallBase.New("", 0x15342F, ""),
            CallBase.New("", 0x165a3b, ""),
            CallBase.New("", 0x169131, ""),
            CallBase.New("", 0x22ca22, ""),
            CallBase.New("", 0x22ca9f, ""),
            CallBase.New("", 0x30d8ac, ""),
            CallBase.New("", 0x314da5, ""),
            CallBase.New("", 0x316339, ""),
            CallBase.New("", 0x316900, ""),
            CallBase.New("", 0x31694f, ""),
            CallBase.New("", 0x319124, ""),
            CallBase.New("", 0x319279, ""),
            CallBase.New("", 0x31b3ed, ""),
            CallBase.New("", 0x3209db, ""),
            CallBase.New("", 0x320fb5, ""),
            CallBase.New("", 0x32181c, ""),
            CallBase.New("", 0x323300, ""),
            CallBase.New("", 0x3241ef, ""),
            CallBase.New("", 0x324223, ""),
            CallBase.New("", 0x32f634, ""),
            CallBase.New("", 0x33490f, ""),
            CallBase.New("", 0x338048, ""),
            CallBase.New("", 0x33a7bb, ""),
            CallBase.New("", 0x33ad69, ""),
            CallBase.New("", 0x33c79c, ""),
            CallBase.New("", 0x33e7b8, ""),
            CallBase.New("", 0x33f986, ""),
            CallBase.New("", 0x3425cd, ""),
            CallBase.New("", 0x34332d, ""),
            CallBase.New("", 0x344c2e, ""),
            CallBase.New("", 0x3460b8, ""),
            CallBase.New("", 0x34899a, ""),
            CallBase.New("", 0x34cc0e, ""),
            CallBase.New("", 0x34fc34, ""),
            CallBase.New("", 0x353665, ""),
            CallBase.New("", 0x35ac2d, ""),
            CallBase.New("", 0x35b791, ""),
            CallBase.New("", 0x35b7c1, ""),
            CallBase.New("", 0x35bd3f, ""),
            CallBase.New("", 0x360632, ""),
            CallBase.New("", 0x360647, ""),
            CallBase.New("", 0x360651, ""),
            CallBase.New("", 0x361157, ""),
            CallBase.New("", 0x36bc66, ""),
            CallBase.New("", 0x36d392, ""),
            CallBase.New("", 0x36e0aa, ""),
            CallBase.New("", 0x36f89a, "")
        };
        private static int[] NewAdjustTickRateInt = new int[AdjustTickRateIntCalls.Length];


        private static byte[] GetTickDeltaASM = {0xA1, 0xE4, 0x06, 0x84, 0x01, 0xD9, 0x40, 0x14, 0xC3};
        private static CallBase[] GetTickDeltaCalls = new[]
        {
            CallBase.New("", 0x287e7, ""),
            CallBase.New("", 0x287fd, ""),
            CallBase.New("", 0x2d9d7, ""),
            CallBase.New("", 0x39bfe, ""),
            CallBase.New("", 0x3ab7e, ""),
            CallBase.New("", 0x3abce, ""),
            CallBase.New("", 0x3ac0e, ""),
            CallBase.New("", 0x91149, ""),
            CallBase.New("", 0xa281c, ""),
            CallBase.New("", 0xab24c, ""),
            CallBase.New("", 0xac63c, ""),
            CallBase.New("", 0xc8ac7, ""),
            CallBase.New("", 0xc8af8, ""),
            CallBase.New("", 0xc8B2D, ""),
            CallBase.New("", 0xc9613, ""),
            CallBase.New("", 0xdbde1, ""),
            CallBase.New("", 0xdbdf2, ""),
            CallBase.New("", 0xdbe28, ""),
            CallBase.New("", 0xe20b9, ""),
            CallBase.New("", 0xe212d, ""),
            CallBase.New("", 0xe25ea, ""),
            CallBase.New("", 0xe2630, ""),
            CallBase.New("", 0xe2876, ""),
            CallBase.New("", 0xe2ab4, ""),
            CallBase.New("", 0xe38ef, ""),
            CallBase.New("", 0x1069e7, ""),
            CallBase.New("", 0x106a62, ""),
            CallBase.New("", 0x106d9e, "Object Physics 1?"),
            CallBase.New("", 0x107244, "Object Physics 2?"),
            CallBase.New("", 0x107569, ""),
            CallBase.New("", 0x1078d8, ""),
            CallBase.New("", 0x10a372, ""),
            CallBase.New("", 0x10a5f9, ""),
            CallBase.New("", 0x10adb0, ""),
            CallBase.New("", 0x10ae87, ""),
            CallBase.New("", 0x10aebe, ""),
            CallBase.New("", 0x10b0a2, ""),
            CallBase.New("", 0x10b2a2, ""),
            CallBase.New("", 0x10b669, ""),
            CallBase.New("", 0x10b6d5, ""),
            CallBase.New("", 0x10b77a, ""),
            CallBase.New("", 0x10b9da, ""),
            CallBase.New("", 0x10bf30, ""),
            CallBase.New("", 0x10c091, ""),
            CallBase.New("", 0x10c0cb, ""),
            CallBase.New("", 0x112f68, "Animations 1"),
            CallBase.New("", 0x113284, "Animations 2"),
            CallBase.New("", 0x12d849, ""),
            CallBase.New("", 0x13a7db, ""),
            CallBase.New("", 0x13c078, ""),
            CallBase.New("", 0x13c126, ""),
            CallBase.New("", 0x13c5dc, ""),
            CallBase.New("", 0x13c607, ""),
            CallBase.New("", 0x13c64d, ""),
            CallBase.New("", 0x13c680, ""),
            CallBase.New("", 0x14003a, ""),
            CallBase.New("", 0x14061c, ""),
            CallBase.New("", 0x140655, ""),
            CallBase.New("", 0x14069b, ""),
            CallBase.New("", 0x14074b, ""),
            CallBase.New("", 0x14096e, ""),
            CallBase.New("", 0x1409a7, ""),
            CallBase.New("", 0x1462ad, ""),
            CallBase.New("", 0x148f8a, ""),
            CallBase.New("", 0x149442, ""),
            CallBase.New("", 0x14d9cd, ""),
            CallBase.New("", 0x14da0f, ""),
            CallBase.New("", 0x150c78, ""),
            CallBase.New("", 0x150f31, ""),
            CallBase.New("", 0x151284, ""),
            CallBase.New("", 0x152660, ""),
            CallBase.New("", 0x1526d4, ""),
            CallBase.New("", 0x1543a0, ""),
            CallBase.New("", 0x1556bc, ""),
            CallBase.New("", 0x157bd4, ""),
            CallBase.New("", 0x157f5c, ""),
            CallBase.New("", 0x158839, ""),
            CallBase.New("", 0x1589cb, ""),
            CallBase.New("", 0x158a5c, ""),
            CallBase.New("", 0x158aac, ""),
            CallBase.New("", 0x158afa, ""),
            CallBase.New("", 0x158b1a, ""),
            CallBase.New("", 0x15e375, ""),
            CallBase.New("", 0x15e3b2, ""),
            CallBase.New("", 0x161af6, ""),
            CallBase.New("", 0x161c2a, ""),
            CallBase.New("", 0x162941, ""),
            CallBase.New("", 0x162975, ""),
            CallBase.New("", 0x162b5d, ""),
            CallBase.New("", 0x162c27, ""),
            CallBase.New("", 0x162d3d, ""),
            CallBase.New("", 0x162d7a, ""),
            CallBase.New("", 0x163761, ""),
            CallBase.New("", 0x164410, ""),
            CallBase.New("", 0x164aac, ""),
            CallBase.New("", 0x164cf6, ""),
            CallBase.New("", 0x164f0e, ""),
            CallBase.New("", 0x16d937, ""),
            CallBase.New("", 0x16e61c, ""),
            CallBase.New("", 0x16eda7, ""),
            CallBase.New("", 0x16f16d, ""),
            CallBase.New("", 0x16f357, ""),
            CallBase.New("", 0x16f5f1, ""),
            CallBase.New("", 0x16ff1f, ""),
            CallBase.New("", 0x17011e, ""),
            CallBase.New("", 0x1701be, ""),
            CallBase.New("", 0x1704d3, ""),
            CallBase.New("", 0x1708CB, ""),
            CallBase.New("", 0x171461, ""),
            CallBase.New("", 0x172ada, ""),
            CallBase.New("", 0x172ecf, ""),
            CallBase.New("", 0x173644, ""),
            CallBase.New("", 0x1752b7, ""),
            CallBase.New("", 0x1752c6, ""),
            CallBase.New("", 0x175556, ""),
            CallBase.New("", 0x1755a2, ""),
            CallBase.New("", 0x17beb3, ""),
            CallBase.New("", 0x17c061, ""),
            CallBase.New("", 0x17c18c, ""),
            CallBase.New("", 0x18179f, ""),
            CallBase.New("", 0x181f67, ""),
            CallBase.New("", 0x182554, ""),
            CallBase.New("", 0x183194, ""),
            CallBase.New("", 0x183234, ""),
            CallBase.New("", 0x18455f, ""),
            CallBase.New("", 0x18b305, ""),
            CallBase.New("", 0x18b3a1, ""),
            CallBase.New("", 0x18b3d5, ""),
            CallBase.New("", 0x18da61, ""),
            CallBase.New("", 0x1f4435, ""),
            CallBase.New("", 0x22a8fe, ""),
            CallBase.New("", 0x22a94d, ""),
            CallBase.New("", 0x22a972, ""),
            CallBase.New("", 0x22a9a7, ""),
            CallBase.New("", 0x22a9fa, ""),
            CallBase.New("", 0x22aa47, ""),
            CallBase.New("", 0x22aeaf, ""),
            CallBase.New("", 0x22af28, ""),
            CallBase.New("", 0x30d3b5, ""),
            CallBase.New("", 0x35cf27, ""),
            CallBase.New("", 0x35cf48, ""),
            CallBase.New("", 0x35cf69, ""),
            CallBase.New("", 0x37ae29, ""),
            CallBase.New("", 0x37bc65, ""),
            CallBase.New("", 0x37cd81, "")
        };
        private static int[] NewGetTickDelta = new int[GetTickDeltaCalls.Length];


        private static byte[] GetTickDeltaMultiASM =
            {0xA1, 0xE4, 0x06, 0x03, 0x01, 0xD9, 0x40, 0x14, 0xD8, 0x4C, 0x24, 0x04, 0xC3};

        public static CallBase[] GetTickDeltaMutliCalls = new[]
        {
            CallBase.New("", 0x8287e7, ""),
            CallBase.New("", 0x8287fd, ""),
            CallBase.New("", 0x82d9d7, ""),
            CallBase.New("", 0x839bfe, ""),
            CallBase.New("", 0x83ab7e, ""),
            CallBase.New("", 0x83abce, ""),
            CallBase.New("", 0x83ac0e, ""),
            CallBase.New("", 0x891149, ""),
            CallBase.New("", 0x8a281c, ""),
            CallBase.New("", 0x8ab24c, ""),
            CallBase.New("", 0x8ac63c, ""),
            CallBase.New("", 0x8c8ac7, ""),
            CallBase.New("", 0x8c8af8, ""),
            CallBase.New("", 0x8c8b2d, ""),
            CallBase.New("", 0x8c9613, ""),
            CallBase.New("", 0x8dbde1, ""),
            CallBase.New("", 0x8dbdf2, ""),
            CallBase.New("", 0x8dbe28, ""),
            CallBase.New("", 0x8e20b9, ""),
            CallBase.New("", 0x8e212d, ""),
            CallBase.New("", 0x8e25ea, ""),
            CallBase.New("", 0x8e2630, ""),
            CallBase.New("", 0x8e2876, ""),
            CallBase.New("", 0x8e2ab4, ""),
            CallBase.New("", 0x8e38ef, ""),
            CallBase.New("", 0x9069e7, ""),
            CallBase.New("", 0x906a62, ""),
            CallBase.New("", 0x906d9e, ""),
            CallBase.New("", 0x907244, ""),
            CallBase.New("", 0x907569, ""),
            CallBase.New("", 0x9078d8, ""),
            CallBase.New("", 0x90a372, ""),
            CallBase.New("", 0x90a5f9, ""),
            CallBase.New("", 0x90adb0, ""),
            CallBase.New("", 0x90ae87, ""),
            CallBase.New("", 0x90aebe, ""),
            CallBase.New("", 0x90b0a2, ""),
            CallBase.New("", 0x90b2a2, ""),
            CallBase.New("", 0x90b669, ""),
            CallBase.New("", 0x90b6d5, ""),
            CallBase.New("", 0x90b77a, ""),
            CallBase.New("", 0x90b9da, ""),
            CallBase.New("", 0x90bf30, ""),
            CallBase.New("", 0x90c091, ""),
            CallBase.New("", 0x90c0cb, ""),
            CallBase.New("", 0x912f68, ""),
            CallBase.New("", 0x913284, ""),
            CallBase.New("", 0x92d849, ""),
            CallBase.New("", 0x93a7db, ""),
            CallBase.New("", 0x93c078, ""),
            CallBase.New("", 0x93c126, ""),
            CallBase.New("", 0x93c5dc, ""),
            CallBase.New("", 0x93c607, ""),
            CallBase.New("", 0x93c64d, ""),
            CallBase.New("", 0x93c680, ""),
            CallBase.New("", 0x94003a, ""),
            CallBase.New("", 0x94061c, ""),
            CallBase.New("", 0x940655, ""),
            CallBase.New("", 0x94069b, ""),
            CallBase.New("", 0x94074b, ""),
            CallBase.New("", 0x94096e, ""),
            CallBase.New("", 0x9409a7, ""),
            CallBase.New("", 0x9462ad, ""),
            CallBase.New("", 0x948f8a, ""),
            CallBase.New("", 0x949442, ""),
            CallBase.New("", 0x94d9cd, ""),
            CallBase.New("", 0x94da0f, ""),
            CallBase.New("", 0x950c78, ""),
            CallBase.New("", 0x950f31, ""),
            CallBase.New("", 0x951284, ""),
            CallBase.New("", 0x952660, ""),
            CallBase.New("", 0x9526d4, ""),
            CallBase.New("", 0x9543a0, ""),
            CallBase.New("", 0x9556bc, ""),
            CallBase.New("", 0x957bd4, ""),
            CallBase.New("", 0x957f5c, ""),
            CallBase.New("", 0x958839, ""),
            CallBase.New("", 0x9589cb, ""),
            CallBase.New("", 0x958a5c, ""),
            CallBase.New("", 0x958aac, ""),
            CallBase.New("", 0x958afa, ""),
            CallBase.New("", 0x958b1a, ""),
            CallBase.New("", 0x95e375, ""),
            CallBase.New("", 0x95e3b2, ""),
            CallBase.New("", 0x961af6, ""),
            CallBase.New("", 0x961c2a, ""),
            CallBase.New("", 0x962941, ""),
            CallBase.New("", 0x962975, ""),
            CallBase.New("", 0x962b5d, ""),
            CallBase.New("", 0x962c27, ""),
            CallBase.New("", 0x962d3d, ""),
            CallBase.New("", 0x962d7a, ""),
            CallBase.New("", 0x963761, ""),
            CallBase.New("", 0x964410, ""),
            CallBase.New("", 0x964aac, ""),
            CallBase.New("", 0x964cf6, ""),
            CallBase.New("", 0x964f0e, ""),
            CallBase.New("", 0x96d937, ""),
            CallBase.New("", 0x96e61c, ""),
            CallBase.New("", 0x96eda7, ""),
            CallBase.New("", 0x96f16d, ""),
            CallBase.New("", 0x96f357, ""),
            CallBase.New("", 0x96f5f1, ""),
            CallBase.New("", 0x96ff1f, ""),
            CallBase.New("", 0x97011e, ""),
            CallBase.New("", 0x9701be, ""),
            CallBase.New("", 0x9704d3, ""),
            CallBase.New("", 0x9708cb, ""),
            CallBase.New("", 0x971461, ""),
            CallBase.New("", 0x972ada, ""),
            CallBase.New("", 0x972ecf, ""),
            CallBase.New("", 0x973644, ""),
            CallBase.New("", 0x9752b7, ""),
            CallBase.New("", 0x9752c6, ""),
            CallBase.New("", 0x975556, ""),
            CallBase.New("", 0x9755a2, ""),
            CallBase.New("", 0x97beb3, ""),
            CallBase.New("", 0x97c061, ""),
            CallBase.New("", 0x97c18c, ""),
            CallBase.New("", 0x98179f, ""),
            CallBase.New("", 0x981f67, ""),
            CallBase.New("", 0x982554, ""),
            CallBase.New("", 0x983194, ""),
            CallBase.New("", 0x983234, ""),
            CallBase.New("", 0x98455f, ""),
            CallBase.New("", 0x98b305, ""),
            CallBase.New("", 0x98b3a1, ""),
            CallBase.New("", 0x98b3d5, ""),
            CallBase.New("", 0x98da61, ""),
            CallBase.New("", 0x9f4435, ""),
            CallBase.New("", 0xa2a8fe, ""),
            CallBase.New("", 0xa2a94d, ""),
            CallBase.New("", 0xa2a972, ""),
            CallBase.New("", 0xa2a9a7, ""),
            CallBase.New("", 0xa2a9fa, ""),
            CallBase.New("", 0xa2aa47, ""),
            CallBase.New("", 0xa2aeaf, ""),
            CallBase.New("", 0xa2af28, ""),
            CallBase.New("", 0xb0d3b5, ""),
            CallBase.New("", 0xb5cf27, ""),
            CallBase.New("", 0xb5cf48, ""),
            CallBase.New("", 0xb5cf69, ""),
            CallBase.New("", 0xb7ae29, ""),
            CallBase.New("", 0xb7bc65, ""),
            CallBase.New("", 0xb7cd81, "")
        };
        private static void CaveCalls()
        {
            
            Program.Memory.MakeWriteable(true, FreeMemoryAddress, GetTickRateASM.Length * GetTickRateCalls.Length + AdjustTickRateIntCalls.Length * AdjustTickRateIntASM.Length + GetTickDeltaCalls.Length * GetTickDeltaASM.Length);
            var ba = BitConverter.GetBytes(Program.Memory.ImageAddress(0x4c06e4));
            for (var i = 0; i < ba.Length; i++)
                GetTickDeltaASM[i + 1] = ba[i];
            for (var index = 0; index < GetTickRateCalls.Length; index++)
            {
                var tickRateCall = GetTickRateCalls[index];
                var offset = index * GetTickRateASM.Length;
                
                var newAddress = FreeMemoryAddress + offset;
                if(Program.Memory.ReadMemory(true, newAddress, 1)[0] == 0)
                    Program.Memory.WriteMemory(true, newAddress, GetTickRateASM);
                var newCallOffset = Program.Memory.ImageAddress(newAddress) -
                                    (Program.Memory.ImageAddress(tickRateCall.offset) + 5); //Dst - Src + 5 (5 is the size of a call instruction)
                var newCall = new byte[5];
                newCall[0] = 0xE8; //Call
                var bs = BitConverter.GetBytes(newCallOffset);
                for (var i = 0; i < bs.Length; i++)
                    newCall[i+1] = bs[i];
                var x = newCall;
               Program.Memory.WriteMemory(true, tickRateCall.offset, newCall);
               NewGetTickRates[index] = newAddress + 1;
               if (!Program.MinimalMode)
               {
                   Program.Form.addControl("Tick_g_" + index.ToString(), "get_tick_rate",
                       Program.Memory.ReadInt(newAddress + 1, true), tickRateCall.description,
                       (o, args) =>
                       {
                           Program.Memory.WriteInt(newAddress + 1, (int) ((NumericUpDown) o).Value, true);
                       });
               }
            }

            CurrentOffset = GetTickRateCalls.Length * GetTickRateASM.Length;
            for (var index = 0; index < AdjustTickRateIntCalls.Length; index++)
            {
                var adjustTickCall = AdjustTickRateIntCalls[index];
                var offset = CurrentOffset + index * AdjustTickRateIntASM.Length;
                var newAddress = FreeMemoryAddress + offset;
                if (Program.Memory.ReadMemory(true, newAddress, 1)[0] == 0)
                    Program.Memory.WriteMemory(true, newAddress, AdjustTickRateIntASM);
                var newCallOffset = Program.Memory.ImageAddress(newAddress) -
                                    (Program.Memory.ImageAddress(adjustTickCall.offset) + 5);
                var newCall = new byte[5];
                newCall[0] = 0xE8;
                var bs = BitConverter.GetBytes(newCallOffset);
                for (var i = 0; i < bs.Length; i++)
                    newCall[i + 1] = bs[i];
                Program.Memory.WriteMemory(true, adjustTickCall.offset, newCall);
                NewAdjustTickRateInt[index] = newAddress + 1;
                if (!Program.MinimalMode)
                {
                    Program.Form.addControl("Tick_i_" + index.ToString(), "adjust_tick_int",
                        Program.Memory.ReadInt(newAddress + 1, true), adjustTickCall.description,
                        (o, args) =>
                        {
                            Program.Memory.WriteInt(newAddress + 1, (int) ((NumericUpDown) o).Value, true);
                        });
                }
            }

            CurrentOffset += AdjustTickRateIntCalls.Length * AdjustTickRateIntASM.Length;
            Program.Memory.WriteMemory(true, FreeMemoryAddress + CurrentOffset, GetTickDeltaASM);

            for (int index = 0; index < GetTickDeltaCalls.Length; index++)
            {
                var getTickDeltaCall = GetTickDeltaCalls[index];
                int newCallOffset = Program.Memory.ImageAddress(FreeMemoryAddress + CurrentOffset) -
                                    (Program.Memory.ImageAddress(getTickDeltaCall.offset) + 5);
                int oldCallOffset = Program.Memory.ImageAddress(0x7c0a0) -
                                    (Program.Memory.ImageAddress(getTickDeltaCall.offset) + 5);
                byte[] newCall = new byte[5];
                byte[] bs = BitConverter.GetBytes(newCallOffset);
                newCall[0] = 0xE8;
                for (int i = 0; i < bs.Length; i++)
                    newCall[i + 1] = bs[i];
                byte[] oldCall = new byte[5];
                oldCall[0] = 0xE8;
                bs = BitConverter.GetBytes(oldCallOffset);
                for (int i = 0; i < bs.Length; i++)
                    oldCall[i + 1] = bs[i];
                //Program.Memory.WriteMemory(true, getTickDeltaCall, newCall);
                bool val = Program.Memory.ReadMemory(true, getTickDeltaCall.offset, 5).SequenceEqual(newCall);
                if (!Program.MinimalMode)
                {
                    Program.Form.addControl("Tick_d_" + index.ToString(), "get_tick_delta", val,
                        getTickDeltaCall.description, (o, args) =>
                        {
                            if (((CheckBox) o).Checked)
                            {
                                Program.Memory.WriteMemory(true, getTickDeltaCall.offset, newCall);
                            }
                            else
                            {
                                Program.Memory.WriteMemory(true, getTickDeltaCall.offset, oldCall);
                            }
                        });
                }
                else
                {
                    if (index == 27 || index == 28)
                    {
                        Program.Memory.WriteMemory(true, getTickDeltaCall.offset, newCall);
                    }
                }
            }

            CurrentOffset += GetTickDeltaCalls.Length * GetTickDeltaASM.Length;
            Program.Memory.WriteMemory(true, FreeMemoryAddress + CurrentOffset, GetTickDeltaMultiASM);

            for (var index = 0; index < GetTickDeltaMutliCalls.Length; index++)
            {
                var getDeltaMultiCall = GetTickDeltaMutliCalls[index];
                var newCallOffset = Program.Memory.ImageAddress(FreeMemoryAddress + CurrentOffset) -
                                    (Program.Memory.ImageAddress(getDeltaMultiCall.offset) + 5);
                var oldCallOffset = Program.Memory.ImageAddress(0x7C0A9) -
                                    (Program.Memory.ImageAddress(getDeltaMultiCall.offset) + 5);
                byte[] newCall = new byte[5];
                byte[] bs = BitConverter.GetBytes(newCallOffset);

                newCall[0] = 0xE8;
                for (int i = 0; i < bs.Length; i++)
                    newCall[i + 1] = bs[i];
                byte[] oldCall = new byte[5];
                oldCall[0] = 0xE8;
                bs = BitConverter.GetBytes(oldCallOffset);
                for (int i = 0; i < bs.Length; i++)
                    oldCall[i + 1] = bs[i];
                bool val = Program.Memory.ReadMemory(true, getDeltaMultiCall.offset, 5).SequenceEqual(newCall);
                if (!Program.MinimalMode)
                {
                    Program.Form.addControl("Tick_dm_" + index.ToString(), "get_delta_multi", val,
                        getDeltaMultiCall.description, (o, args) =>
                        {
                            if (((CheckBox) o).Checked)
                            {
                                Program.Memory.WriteMemory(true, getDeltaMultiCall.offset, newCall);
                            }
                            else
                            {
                                Program.Memory.WriteMemory(true, getDeltaMultiCall.offset, oldCall);
                            }
                        });
                }
            }

            Caved = true;
            if (!Program.MinimalMode)
            {
                Program.Form.updateControl<CheckBox>("tick_d_27", true);
                Program.Form.updateControl<CheckBox>("tick_d_28", true);
            }
        }
        private static void UpdateUserObjectAddress()
        {
            var PlayerTableIndex = 0;
            for (var i = 0; i < 16; i++)
                if (ProfileName == Program.Memory.ReadStringUnicode(PlayerTableGamertagAddress + i * PlayerTableItemSize, 16))
                {
                    PlayerTableIndex = i;
                    break;
                }

            var PlayerDatum = Program.Memory.ReadInt(0x30002B44 + PlayerTableIndex * PlayerTableItemSize);
            var PlayerObjectAddress = Program.Memory.ReadInt(
                ObjectTableAddress +
                ((Int16)PlayerDatum * ObjectTableItemSize) +
                8
            );
            Form1.userObjectAddress_.SetPropertyThreadSafe(() => Form1.userObjectAddress_.Text, PlayerObjectAddress.ToString("X"));
        }

        private static void ChangeTickRates()
        {
            //This is done because if it isn't swapped around during loading screens input gets all fucked.
            switch (GetGameState)
            {
                case 0: //Main Menu
                case 1: //Lobby
                    /*
                       push ebx
                       xor bl,bl
                     */
                    Program.Memory.WriteMemory(true, 0x3A938, new byte[] {0x53, 0x30, 0xDB});
                    /*
                     * Change the Particle System pointer back to default
                     */
                    Program.Memory.WriteInt(0x104950, Program.Memory.ImageAddress(0x3a03c4), true);
                    break;
                case 3: //Ingame
                    /*
                        mov al, 1
                        retn
                     */
                    Program.Memory.WriteMemory(true, 0x3A938, new byte[] {0xB0, 0x01, 0xC3});
                    /*
                     * Change the Particle System pointer to point to a blank area in the memory so we can control separately it from other systems.
                     */
                    Program.Memory.WriteInt(0x104950, Program.Memory.ImageAddress(0x39E2F8), true);
                    break;
            }

            if (Program.TrueSixty) //Removes all calculation and just runs straight at 60
            {
                Program.Memory.WriteMemory(true, 0x7C449, new byte[] {0x90, 0x90, 0x90, 0x90, 0x90});
                Program.Memory.WriteInt(0x46d820 + 0x64, 1, true);
            }
            else
            {
                Program.Memory.WriteMemory(true, 0x7C449, new byte[] {0xF3, 0x0F, 0x11, 0x49, 0x10});
                Program.Memory.WriteInt(0x46d820 + 0x64, 0, true);
            }

            //Change the game time globals tick rate
            Program.Memory.WriteShort(Program.Memory.Pointer(true, 0x4c06e4, 2), (short) Program.TickRate);
            //Change the time per tick (1 / tick rate)
            Program.Memory.WriteFloat(Program.Memory.Pointer(true, 0x4c06e4, 4), 1f / Program.TickRate);

            Program.Memory.WriteByte(0x264ABC, (byte) Program.TickRate, true);

            //Change the value in the conditional statement from 30 to the desired tick rate so it skips to the next conditional
            Program.Memory.WriteByte(0x7C389, (byte) Program.TickRate, true);
            //Change the Particle Systems multiplier to 60
            Program.Memory.WriteFloat(0x39E2F8, 60, true);
            //Program.Memory.WriteByte(NewGetTickRates[51], (byte) Program.TickRate, true);
            //Program.Memory.WriteByte(NewGetTickRates[57], (byte)Program.TickRate, true);
            Program.Memory.WriteFloat(Program.Memory.Pointer(true, 0x4c06e4, 0x14), 1f / Program.DeltaRickRate);
            //var a = (NumericUpDown)Form1.tPanel.Controls["Tick_51"];
            //a.Value = Program.TickRate;
            //a.SetPropertyThreadSafe(() => a.Value, Program.TickRate);
        }

        public static void EvalHooked()
        {
            if (Program.Memory == null)
            {
                if (Process.GetProcessesByName("halo2").Count(x => x.HasExited == false) > 0)
                {
                    Program.Memory =
                        new MemoryHandler(Process.GetProcessesByName("halo2").Last(x => x.HasExited == false));
                    Program.Memory.MakeWriteable(true, 0x39E2F8, 4);
                    Program.Memory.WriteMemory(true, 0x9355c, new byte[]{0x90, 0x90, 0x90, 0x90});
                    if (Program.MinimalMode)
                        Program.trayIcon.ShowBalloonTip(2000, "Uncapped Assistant", "Halo 2 has been hooked", ToolTipIcon.None);
                    Hooked = true;
                }
            }
            else if (Program.Memory.ProcessIsRunning())
            {
                Program.Memory = null;
                Hooked = false;
                ProfileName = "";
                if (Program.MinimalMode)
                {    
                    Program.trayIcon.ShowBalloonTip(2000, "Uncapped Assistant", "Halo 2 has closed",
                        ToolTipIcon.None);
                }
                else
                {
                    Program.Form.removeControls();
                }
            }
            else
            {
                if (ProfileName == "") ProfileName = Program.Memory.ReadStringUnicode(0x51A638, 16, true);
            }
        }

        public static int GetGameState
        {
            get
            {
                var b = Program.Memory.ImageAddress(0x420FC4);
                var a = Program.Memory.ReadInt(b);
                return a;
            }
        }

    }

    public class CallBase
    {
        public string name { get; set; }
        public int offset { get; set; }
        public string description_ { get; set; }

        public string description =>
            offset.ToString("X") + " - " + description_;

        public CallBase(string name, int offset, string description)
        {
            this.name = name;
            this.offset = offset;
            this.description_ = description;
        }
        public CallBase() { }

        public static CallBase New(string name, int offset, string description)
        {
            return new CallBase(name, offset, description);
        }
    }
}
