using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ6
{
    public enum PlayerQueueValue 
    {
        CROSS = -1,
        ZERO = 0
    }

    internal class Game
    {
        private sbyte[][] data;
        private const int size = 3;
        private bool gameover;
        private PlayerQueueValue queueValue;
        private sbyte _value;

        public bool GameOver { get { return gameover; } set { gameover = value; } }
        public sbyte[][] Data { get { return data; }}
        public sbyte Value { get { return _value; } set { _value = value; } }
        public Game()
        {
            data = new sbyte[size][];
            for (int i = 0; i < size; ++i)
            {
                data[i] = new sbyte[size];
            }
            gameover = false;
            queueValue = PlayerQueueValue.CROSS;
            _value = 1;
        }

        public void RestartGame()
        {
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; ++j)
                {
                    data[i][j] = 0;
                }
            }
            gameover = false;
            queueValue = PlayerQueueValue.CROSS;
            _value = 1;
        }


        public void SwitchQueue()
        {
            queueValue = ~queueValue;

            switch (queueValue)
            {
                case PlayerQueueValue.CROSS:
                    _value = 1;
                    break;
                case PlayerQueueValue.ZERO:
                    _value = -1;
                    break;
                default:
                    _value = 0;
                    break;
            }
        }

        public bool GameOverCheck()
        {
            bool flag = false;

            for (int i = 0; i < size; ++i)
            {
                flag = CheckArray(data[i][0], data[i][1], data[i][2])
                    || CheckArray(data[0][i], data[1][i], data[2][i]);
                if (flag) return flag;
            }

            flag = CheckArray(data[0][0], data[1][1], data[2][2])
                || CheckArray(data[0][2], data[1][1], data[2][0]);
          
            return flag;
        }

        public bool SetValue(int i, int j)
        {
            if (i < 0 || j < 0)
                return false;

            if (i >= size || j >= size)
                return false;

            if (data[i][j] != 0)
                return false;

            data[i][j] = _value;
            return true;
        }

        public bool CheckArray(params sbyte[] args)
        {

            bool flag = true;
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[0] != args[i] || args[i] == 0)
                {
                    flag = false;
                    break;
                }
            }
            return flag;
        }
    }
}