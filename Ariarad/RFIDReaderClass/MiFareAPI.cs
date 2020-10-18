using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Ariarad.MyClass
{
    class MiFareAPI
    {
        [DllImport("MF_API.dll")]
        public static extern short MF_GetDLL_Ver(ref Byte rVER);
        [DllImport("MF_API.dll")]
        public static extern short MF_Halt(short DeviceAddr);
        [DllImport("MF_API.dll")]
        public static extern short MF_InitComm(String portname, short baud);
        [DllImport("MF_API.dll")]
        public static extern short MF_ExitComm();
        //عملیات بر روی کارت
        [DllImport("MF_API.dll")]
        public static extern short MF_Request(short DeviceAddrress, short mode, ref Byte CardType);
        [DllImport("MF_API.dll")]
        public static extern short MF_Anticoll(short DeviceAddrress, ref Byte snr);
        [DllImport("MF_API.dll")]
        public static extern short MF_Select(short DeviceAddrress, ref Byte snr);
        [DllImport("MF_API.dll")]
        public static extern short MF_LoadKey(short DeviceAddrress, ref Byte key);
        [DllImport("MF_API.dll")]
        public static extern short MF_LoadKeyFromEE(short DeviceAddrress, short KeyType, short KeyNum);
        [DllImport("MF_API.dll")]
        public static extern short MF_Authentication(short DeviceAddrress, short AuthType, short block, ref Byte snr);
        [DllImport("MF_API.dll")]
        public static extern short MF_Read(short DeviceAddrress, short block, short numbers, ref short databuff);
        [DllImport("MF_API.dll")]
        public static extern short MF_Write(short DeviceAddrress, short block, short numbers, ref short databuff);
        [DllImport("MF_API.dll")]
        public static extern short MF_Value(short DeviceAddrress, short valoption, ref Byte value);
        [DllImport("MF_API.dll")]
        public static extern short MF_transfer(short DeviceAddrress, short block);
        //اتمام عملیات 
        [DllImport("MF_API.dll")]
        public static extern short MF_SetDeviceBaud(short DeviceAddrress, int baud);
        [DllImport("MF_API.dll")]
        public static extern short MF_SetDeviceAddrres(short DeviceAddrress, short address);
        [DllImport("MF_API.dll")]
        public static extern short MF_GetDeviceAddr(short DeviceAddrress, ref byte snr);
        [DllImport("MF_API.dll")]
        public static extern short MF_DeviceReset(short DeviceAddrress);
        [DllImport("MF_API.dll")]
        public static extern short MF_ControlBuzzer(short DeviceAddrress, short BeepTime);
        [DllImport("MF_API.dll")]
        public static extern short MF_ControlLED(short DeviceAddrress, short LED1, short LED2);
        [DllImport("MF_API.dll")]
        public static extern short MF_SetDeviceSNR(short DeviceAddrress, string SNR);
        [DllImport("MF_API.dll")]
        public static extern short MF_GetDeviceSNR(short DeviceAddrress, ref byte adre);
        [DllImport("MF_API.dll")]
        public static extern short MF_SetRF_ON(short DeviceAddrress);
        [DllImport("MF_API.dll")]
        public static extern short MF_SetRF_OFF(short DeviceAddrress);


        //variable
        public static byte[] Dsn = new byte[8];
    }
}
