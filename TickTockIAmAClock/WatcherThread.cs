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
                    try
                    {
                        if (!Caved)
                        {
                            CaveCalls();
                        }
                        ChangeTickRates();
                        if (Program.advancedMode)
                        {
                            UpdateUserObjectAddress();
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }

                Thread.Sleep(1000);
            }
        }

        private static byte[] GetTickRateASM = {0xB8, 0x3C, 0x00, 0x00, 0x00, 0xC3};
        // MOV EAX, 60
        // RET
        private static int FreeMemoryAddress = 0x14000;
        private static int[] GetTickRateCalls =
        {
            0x5CBB3,
            0x6a6c0,
            0x6A77D,
            0x6AA4D,
            0x6AB4B,
            0x6ABF3,
            0x6B312,
            0x6BAC2,
            0x6DA2E,
            0x6DAA6,
            0x6BD39,
            0x6DB72,
            0x6DB8C,
            0x6DC78,
            0x6FDAD,
            0x7070E,
            0x7255f,
            0x751BE,
            0x7523A,
            0x75244,
            0x75BB8,
            0x75C04,
            0x75C83,
            0x75CBD,
            0xA3D6D,
            0xA4A4B,
            0xb43bc,
            0xb4457,
            0xb4473,
            0xb4481,
            0xb44c0,
            0xb461d,
            0xb5dd3,
            0xb5f93,
            0xb5fb7,
            0xb61b1,
            0xb6864,
            0xd340a,
            0xd6e24,
            0xd7135,
            0xdcfd4,
            0xe0963,
            0xf4ed8,
            0x10ae0a,
            0x10ca5f,
            0x10ca8c,
            0x10d6f0,
            0x10f719,
            0x10f97d,
            0x110493,
            0x110af2,
            0x112BC1,
            0x13e7c5,
            0x14c083,
            0x14d9d8,
            0x1544ae,
            0x1588c2,
            0x15bb41,
            0x15c1f6,
            0x161615,
            0x161626,
            0x16163f,
            0x161651,
            0x170051,
            0x170d4f,
            0x175f47,
            0x175fba,
            0x18e650,
            0x18e89a,
            0x226350,
            0x22cc72,
            0x22d4eb,
            0x22d50b,
            0x37c11a
        };
        private static int[] NewTickRates = new int[GetTickRateCalls.Length];
        private static void CaveCalls()
        {
            Program.Memory.MakeWriteable(true, FreeMemoryAddress, GetTickRateASM.Length * GetTickRateCalls.Length);
            for (var index = 0; index < GetTickRateCalls.Length; index++)
            {
                var tickRateCall = GetTickRateCalls[index];
                var offset = index * GetTickRateASM.Length;
                var newAddress = FreeMemoryAddress + offset;
                Program.Memory.WriteMemory(true, newAddress, GetTickRateASM);
                var newCallOffset = Program.Memory.ImageAddress(newAddress) -
                                    (Program.Memory.ImageAddress(tickRateCall) + 5);
                var newCall = new byte[5];
                newCall[0] = 0xE8;
                var bs = BitConverter.GetBytes(newCallOffset);
                for (var i = 0; i < bs.Length; i++)
                    newCall[i+1] = bs[i];
                var x = newCall;
               Program.Memory.WriteMemory(true, tickRateCall, newCall);
               NewTickRates[index] = newAddress + 1;
               Program.Form.addControl("Tick_" + index.ToString(), (o, args) =>
               {
                   Program.Memory.WriteInt(newAddress + 1, (int) ((NumericUpDown)o).Value, true);
               });
            }
            Caved = true;
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
            //Program.Memory.WriteFloat(0x39E2F8, 60, true);
            Program.Memory.WriteByte(NewTickRates[51], (byte) Program.TickRate, true);
            Program.Memory.WriteByte(NewTickRates[57], (byte)Program.TickRate, true);
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
                        new MemoryHandler(Process.GetProcessesByName("halo2").Single(x => x.HasExited == false));
                    Program.Memory.MakeWriteable(true, 0x39E2F8, 4);
                    Hooked = true;
                }
            }
            else if (Program.Memory.ProcessIsRunning())
            {
                Program.Memory = null;
                Hooked = false;
                ProfileName = "";
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
}
