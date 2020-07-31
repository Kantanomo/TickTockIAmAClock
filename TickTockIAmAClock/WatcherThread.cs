using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TickTockIAmAClock
{
    public static class WatcherThread
    {
        public static bool Hooked = false;

        public static void Run()
        {
            while (true)
            {
                EvalHooked();
                if (Hooked)
                {
                    try
                    {
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
                            case 3://Ingame
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
                            Program.Memory.WriteMemory(true, 0x7C449, new byte[] { 0x90, 0x90, 0x90, 0x90, 0x90 });
                            Program.Memory.WriteInt(0x46d820 + 0x64, 1, true);
                        }
                        else
                        {
                            Program.Memory.WriteMemory(true, 0x7C449, new byte[] { 0xF3, 0x0F, 0x11, 0x49, 0x10 });
                            Program.Memory.WriteInt(0x46d820 + 0x64, 0, true);
                        }
                        //Change the game time globals tick rate
                        Program.Memory.WriteShort(Program.Memory.Pointer(true, 0x4c06e4, 2), (short)Program.TickRate);
                        //Change the time per tick (1 / tick rate)
                        Program.Memory.WriteFloat(Program.Memory.Pointer(true, 0x4c06e4, 4),1f / Program.TickRate);
                        
                        Program.Memory.WriteByte(0x264ABC, (byte) Program.TickRate, true);

                        //Change the value in the conditional statement from 30 to the desired tick rate so it skips to the next conditional
                        Program.Memory.WriteByte(0x7C389, (byte) Program.TickRate, true);
                        //Change the Particle Systems multiplier to 60
                        Program.Memory.WriteFloat(0x39E2F8, 60, true);
                    }
                    catch (Exception e)
                    {

                    }
                }

                Thread.Sleep(1000);
            }
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
