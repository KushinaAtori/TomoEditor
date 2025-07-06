namespace TomoEditor
{
    public
    static class SaveDataModifier
    {
        public
         static void ApplyMoneyChange(FileStream fs, int moneyValue, string region)
        {
            int scaled = moneyValue * 100;
            byte[] moneyBytes = BitConverter.GetBytes((uint)scaled);
            if (!BitConverter.IsLittleEndian) Array.Reverse(moneyBytes);

            if (region == "JP")
            {
                byte[] padded = new byte[16];
                Array.Copy(moneyBytes, 0, padded, 8, moneyBytes.Length);
                fs.Position = 0x14BCA0;
                fs.Write(padded);
            }
            else if (region == "USA")
            {
                fs.Position = 0x1E4BB8;
                fs.Write(moneyBytes);
            }
        }

        public
         static void ApplyClothesChange(FileStream fs, byte clothesValue,
                                        byte ssClothesValue)
        {
            // Large clothes block
            for (int i = 0; i <= 3575; i++)
            {
                fs.Position = 0x30 + i;
                fs.WriteByte(clothesValue);
            }

            // SS clothes blocks
            var writes = new List<(int offset, int count)>{
        (0x40, 8),  (0x80, 8),   (0xC8, 1),   (0xC9, 1),   (0xD0, 8),
        (0x108, 8), (0x168, 8),  (0x188, 8),  (0x198, 8),  (0x1E0, 8),
        (0x258, 8), (0x288, 16), (0x2C8, 8),  (0x2D8, 8),  (0x308, 8),
        (0x480, 8), (0x498, 5),  (0x4C0, 8),  (0x4F8, 8),  (0x508, 8),
        (0x520, 8), (0x538, 8),  (0x568, 8),  (0x5A0, 5),  (0x5B8, 8),
        (0x660, 8), (0x670, 8),  (0x6F0, 16), (0x730, 8),  (0x7B0, 5),
        (0x7B8, 8), (0x7D0, 8),  (0x860, 8),  (0x880, 8),  (0x8F0, 8),
        (0x900, 8), (0x928, 8),  (0x978, 8),  (0x998, 8),  (0x9C0, 8),
        (0x9D0, 8), (0xA00, 8),  (0xA10, 8),  (0xA40, 8),  (0xA78, 32),
        (0xB00, 5), (0xB10, 16), (0xB70, 8),  (0xD88, 16), (0xDA8, 7),
        (0xDD8, 8)};

            foreach (var (offset, count) in writes)
            {
                for (int i = 0; i < count; i++)
                {
                    fs.Position = offset + i;
                    fs.WriteByte(ssClothesValue);
                }
            }
        }

        public
         static void ApplyHatChanges(FileStream fs)
        {
            byte value = 10;

            var hatOffsets = new List<(int offset, int count)>{
        (0x0FF8, 48), (0x1030, 14), (0x1040, 8),  (0x1058, 6),  (0x1060, 6),
        (0x1080, 8),  (0x10C0, 16), (0x10F8, 8),  (0x1108, 8),  (0x1128, 8),
        (0x1138, 30), (0x1158, 5),  (0x1160, 6),  (0x1168, 4),  (0x1170, 6),
        (0x1178, 40), (0x11A8, 39), (0x11D0, 23), (0x11E8, 7),  (0x11F0, 13),
        (0x1200, 15), (0x1210, 6),  (0x1218, 32), (0x1240, 30), (0x1260, 1),
        (0x1268, 47), (0x1298, 6),  (0x12A0, 15), (0x12B0, 71), (0x12F8, 7),
        (0x1300, 30), (0x1320, 7),  (0x1328, 47), (0x1358, 7),  (0x1360, 13),
        (0x1370, 14), (0x1390, 6),  (0x1398, 31), (0x13B8, 14), (0x13C8, 31),
        (0x13E8, 7),  (0x13F0, 6),  (0x13F8, 15), (0x1410, 5),  (0x1418, 5),
        (0x1420, 8),  (0x1430, 13), (0x1440, 4),  (0x1448, 12), (0x1458, 7),
        (0x1460, 6),  (0x1468, 8),  (0x1490, 48), (0x14D0, 8),  (0x14E0, 12),
        (0x14F0, 6),  (0x14F8, 6),  (0x1508, 7),  (0x1510, 7),  (0x1518, 6),
        (0x1530, 9),  (0x1550, 22), (0x1568, 6),  (0x1570, 15), (0x1580, 5),
        (0x1588, 7),  (0x1598, 5),  (0x15B0, 8),  (0x15C0, 7),  (0x15C8, 7),
        (0x15D8, 8),  (0x15E0, 8),  (0x15F8, 8),  (0x1618, 6),  (0x1650, 6),
        (0x1668, 7),  (0x1670, 6),  (0x1678, 6),  (0x1680, 6),  (0x1688, 6),
        (0x1698, 6),  (0x16A0, 5),  (0x16A8, 5),  (0x16B8, 7),  (0x16C8, 8),
    };

            foreach (var (offset, count) in hatOffsets)
            {
                for (int i = 0; i < count; i++)
                {
                    fs.Position = offset + i;
                    fs.WriteByte(value);
                }
            }
        }

        public
         static void ApplyTimePenaltyPatch(FileStream fs, string region)
        {
            byte[] patch =
                region == "JP"
                    ? new byte[]{0x40, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x03, 0x03, 0x02, 0x00}
                    : new byte[]{0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x03, 0x03, 0x02, 0x00};

            long offset = region == "JP" ? 0x14BD40 : 0x1E4C70;
            fs.Position = offset;
            fs.Write(patch);
        }
    }
}  // namespace TomoEditor
