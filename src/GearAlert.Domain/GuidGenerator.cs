using System;

namespace GearAlert.Domain
{
    public static class GuidGenerator {
        [System.Runtime.InteropServices.DllImport("rpcrt4.dll", SetLastError = true)]
        static extern int UuidCreateSequential(byte[] buffer);
        public static Guid Guid() {

            byte[] raw = new byte[16];
            if (UuidCreateSequential(raw) != 0)
                throw new System.ComponentModel.Win32Exception(System.Runtime.InteropServices.Marshal.GetLastWin32Error());

            byte[] fix = new byte[16];

            // reverse 0..3
            fix[0x0] = raw[0x3];
            fix[0x1] = raw[0x2];
            fix[0x2] = raw[0x1];
            fix[0x3] = raw[0x0];

            // reverse 4 & 5
            fix[0x4] = raw[0x5];
            fix[0x5] = raw[0x4];

            // reverse 6 & 7
            fix[0x6] = raw[0x7];
            fix[0x7] = raw[0x6];

            // all other are unchanged
            fix[0x8] = raw[0x8];
            fix[0x9] = raw[0x9];
            fix[0xA] = raw[0xA];
            fix[0xB] = raw[0xB];
            fix[0xC] = raw[0xC];
            fix[0xD] = raw[0xD];
            fix[0xE] = raw[0xE];
            fix[0xF] = raw[0xF];

            return new Guid(fix);
        }
    }
}