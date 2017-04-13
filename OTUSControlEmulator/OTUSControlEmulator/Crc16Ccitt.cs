using System;
using System.Linq;
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace OTUSControlEmulator
{
    // CRC-16-CCITT chech sum with initial value NonZero1 (0xFFFF)
    public enum InitialCrcValue { Zeros, NonZero1 = 0xffff, NonZero2 = 0x1D0F }
    public class Crc16Ccitt
    {
        const ushort Poly = 4129;
        ushort[] _table = new ushort[256];
        ushort _initialValue;

        public ushort ComputeChecksum(byte[] bytes)
        {
            var crc = _initialValue;
            return bytes.Aggregate(crc, (current, t) => (ushort) ((current << 8) ^ _table[((current >> 8) ^ (0xff & t))]));
        }

        public byte[] ComputeChecksumBytes(byte[] bytes)
        {
            var crc = ComputeChecksum(bytes);
            return BitConverter.GetBytes(crc);
        }

        public Crc16Ccitt(InitialCrcValue initialValue)
        {
            _initialValue = (ushort)initialValue;
            for (int i = 0; i < _table.Length; ++i)
            {
                ushort temp = 0;
                var a = (ushort)(i << 8);
                for (int j = 0; j < 8; ++j)
                {
                    if (((temp ^ a) & 0x8000) != 0)
                    {
                        temp = (ushort)((temp << 1) ^ Poly);
                    }
                    else
                    {
                        temp <<= 1;
                    }
                    a <<= 1;
                }
                _table[i] = temp;
            }
        }

        //Создадим массив для расчета контрольной суммы из структуры
        public byte[] CreateCheckSumBuffer(object package)
        {
            var data = (Package)package;

            // Рассчет контрольной суммы 
            int argumentLenght = 0;
            if (data.Argument != null)
            argumentLenght = data.Argument.Length;

            // Формирование массива для расчета контрольной суммы
            byte[] buffer = new byte[4 + argumentLenght];
            buffer[0] = data.IdentityOfResiver;
            buffer[1] = data.IdentityOfTransmitter;
            buffer[2] = data.ClassCode;
            buffer[3] = data.MessageCode;

            int j = 0;
            for (int i = 4; i < argumentLenght + 4; i++)
            {
                if (data.Argument != null) buffer[i] = data.Argument[j];
                j++;
            }

            // Конец расчета контрольной суммы

            return buffer;
        }
    }
}
