namespace MISA.WebFresher032023.Demo
{
    public class Calculator
    {
        /// <summary>
        /// Hàm cộng hai số nguyên
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Tổng của hai số</returns>
        public long Add(int x, int y)
        {
            return x + (long) y;
        }
        
        /// <summary>
        /// Hàm trừ hai số nguyên
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Hiệu hai số</returns>
        public long Sub(int x, int y)
        {
            return x - (long) y;
        }
        /// <summary>
        /// Hàm nhân hai số nguyên
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Tích hai số</returns>
        public long Mul(int x, int y)
        {
            return x * (long) y;
        }
        /// <summary>
        /// Hàm trả về thương của hai số nguyên
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public double Div(int x, int y)
        {
            if (y == 0)
                throw new Exception("Không chia được cho 0");
            return (double) x / (double) y;
        }

        /// <summary>
        /// Hàm chia cho 2
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double Div2(int x)
        {
            return (double) x / 2;
        }
    }
}
