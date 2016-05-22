﻿using MetroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_Jarvis
{
    class ActionManager
    {
        DataGridView field;
        int[,] adv = new int[15, 15];
        int[,] black = new int[15, 15];

        public ActionManager(DataGridView field)
        {
            this.field = field;
        }

        public void resetField()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    field[i, j].Value = null;
                }
            }
        }

        public void resetValue()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field[i, j].Value != null)
                    {
                        if (String.Compare(field[i, j].Value.ToString(), "B") != 0 && String.Compare(field[i, j].Value.ToString(), "W") != 0)
                        {
                            field[i, j].Value = null;
                        }
                    }
                }
            }
        }

        public void initAdv()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    adv[i, j] = 0;
                }
            }
        }

        public void makeBlack()
        {
            // init first
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    black[i, j] = 0;
                }
            }

            // make array for Black
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field[i, j].Value != null)
                    {
                        if (String.Compare(field[i, j].Value.ToString(), "B") == 0)
                        {
                            field[i, j].Selected = true;
                            black[i, j] = 1;
                        }
                    }
                }
            }
        }

        public string adventureField(int r, int c)
        {
            if (field[r, c].Value != null)
            {
                return field[r, c].Value.ToString();
            }
            return null;
        }

        public void showValue(int r, int c, int value)
        {
            if (field[r, c].Value != null)
            {
                if (String.Compare(field[r, c].Value.ToString(), "B") != 0 && String.Compare(field[r, c].Value.ToString(), "W") != 0)
                {
                    field[r, c].Value = value.ToString();
                }
            }
            else
            {
                field[r, c].Value = value.ToString();
            }
        }

        public int valueFunctionForBlack()
        { 
            string piece = null;
            int row = 0, col = 0;
            bool found = false;

            int cnt = 0;
            int i = 1;
            string ret = null;

            initAdv();
            
            // (1) 오목 - 100점
            makeBlack();
            #region 오목
            while (true)
            {
                found = false;
                
                // 돌 4개 이어진거 찾으면 끝.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (black[i, j] == 1)
                        {
                            black[i, j] = 0;
                            field[i, j].Selected = true;
                            piece = field[i, j].Value.ToString();
                            row = i;
                            col = j;
                            found = true;
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
                if (!found)
                {
                    // end loop
                    break;
                }

                if (piece == null)
                {
                    return -1;
                }

                // 8방향 탐색
                cnt = 0;
                i = 1;
                ret = null;
                // (1) 오른쪽으로 탐색
                if (row + 1 < 15)
                {
                    ret = adventureField(row + i, col);
                    if (ret != null)
                    {
                        cnt += 1;
                    }
                    while (ret != null && cnt < 5)
                    {
                        if (String.Compare(ret, piece) == 0)
                        {
                            cnt += 1;
                        }
                        else
                        {
                            break;
                        }
                        i += 1;
                        ret = adventureField(row + i, col);
                    }
                    if (cnt == 4)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 100);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 100);
                                adv[row + i, col] = 1;
                            }
                        }
                    }
                }
                // (2) 오른쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col + 1 < 15)
                {
                    ret = adventureField(row + i, col + i);
                    if (ret != null)
                    {
                        cnt += 1;
                    }
                    while (ret != null && cnt < 5)
                    {
                        if (String.Compare(ret, piece) == 0)
                        {
                            cnt += 1;
                        }
                        else
                        {
                            break;
                        }
                        i += 1;
                        ret = adventureField(row + i, col + i);
                    }
                    if (cnt == 4)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 100);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 100);
                                adv[row + i, col + i] = 1;
                            }
                        }
                    }
                }

                // (3) 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col + 1 < 15)
                {
                    ret = adventureField(row, col + i);
                    if (ret != null)
                    {
                        cnt += 1;
                    }
                    while (ret != null && cnt < 5)
                    {
                        if (String.Compare(ret, piece) == 0)
                        {
                            cnt += 1;
                        }
                        else
                        {
                            break;
                        }
                        i += 1;
                        ret = adventureField(row, col + i);
                    }
                    if (cnt == 4)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 100);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 100);
                                adv[row, col + i] = 1;
                            }
                        }
                    }
                }

                // (4) 왼쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col + 1 < 15)
                {
                    ret = adventureField(row - i, col + i);
                    if (ret != null)
                    {
                        cnt += 1;
                    }
                    while (ret != null && cnt < 5)
                    {
                        if (String.Compare(ret, piece) == 0)
                        {
                            cnt += 1;
                        }
                        else
                        {
                            break;
                        }
                        i += 1;
                        ret = adventureField(row - i, col + i);
                    }
                    if (cnt == 4)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 100);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 100);
                                adv[row - i, col + i] = 1;
                            }
                        }
                    }
                }

                // (5) 왼쪽으로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0)
                {
                    ret = adventureField(row - i, col);
                    if (ret != null)
                    {
                        cnt += 1;
                    }
                    while (ret != null && cnt < 5)
                    {
                        if (String.Compare(ret, piece) == 0)
                        {
                            cnt += 1;
                        }
                        else
                        {
                            break;
                        }
                        i += 1;
                        ret = adventureField(row - i, col);
                    }
                    if (cnt == 4)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 100);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 100);
                                adv[row - i, col] = 1;
                            }
                        }
                    }
                }

                // (6) 왼쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    ret = adventureField(row - i, col - i);
                    if (ret != null)
                    {
                        cnt += 1;
                    }
                    while (ret != null && cnt < 5)
                    {
                        if (String.Compare(ret, piece) == 0)
                        {
                            cnt += 1;
                        }
                        else
                        {
                            break;
                        }
                        i += 1;
                        ret = adventureField(row - i, col - i);
                    }
                    if (cnt == 4)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 100);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 100);
                                adv[row - i, col - i] = 1;
                            }
                        }
                    }
                }

                // (7) 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col - 1 >= 0)
                {
                    ret = adventureField(row, col - i);
                    if (ret != null)
                    {
                        cnt += 1;
                    }
                    while (ret != null && cnt < 5)
                    {
                        if (String.Compare(ret, piece) == 0)
                        {
                            cnt += 1;
                        }
                        else
                        {
                            break;
                        }
                        i += 1;
                        ret = adventureField(row, col - i);
                    }
                    if (cnt == 4)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 100);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 100);
                                adv[row, col - i] = 1;
                            }
                        }
                    }
                }

                // (8) 오른쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col - 1 >= 0)
                {
                    ret = adventureField(row + i, col - i);
                    if (ret != null)
                    {
                        cnt += 1;
                    }
                    while (ret != null && cnt < 5)
                    {
                        if (String.Compare(ret, piece) == 0)
                        {
                            cnt += 1;
                        }
                        else
                        {
                            break;
                        }
                        i += 1;
                        ret = adventureField(row + i, col - i);
                    }
                    if (cnt == 4)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 100);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 100);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion

            // (2) 사사 - 99점
            makeBlack();
            #region 사사
           
            int count;
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {    
                    if(field(i,j)==null)
                    {
                        count = 0;
                        //위3
                        if (i > 3)
                        {
                            if (field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'b')
                            {
                                count++;
                            }
                        }
                        //오른쪽위3
                        if ((i > 3) && (j < 12))
                        {
                            if (field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == 'b')
                            {
                                count++;
                            }
                        }
                        //오른쪽3
                        if (j < 12)
                        {
                            if (field(i, j + 1) == 'b' && field(i, j + 2) == 'b' && field(i, j + 3) == 'b')
                            {
                                count++;
                            }
                        }
                        //오른쪽아래3
                        if ((i<12) && (j < 12))
                        {
                            if (field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == 'b')
                            {
                                count++;
                            }
                        }
                        //아래3
                        if (i < 12)
                        {
                            if (field(i + 1, j) == 'b' && field(i + 2, j) == 'b' && field(i + 3, j) == 'b')
                            {
                                count++;
                            }
                        }
                        //왼쪽아래3
                        if ((i<12) && (j > 3))
                        {
                            if (field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == 'b')
                            {
                                count++;
                            }
                        }
                        //왼쪽3
                        if (j > 3)
                        {
                            if (field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'b')
                            {
                                count++;
                            }
                        }
                        //왼쪽위3
                        if ((i>3) && (j >3))
                        {
                            if (field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == 'b')
                            {
                                count++;
                            }
                        }

                        //만약 2줄이상이 해당되면 사사
                        if(count>1)
                        {
                            showValue(i,j,99);
                        }
                    }
                }
            }
            #endregion

            // (3) 상대편의 방어가 없는 사삼 - 98점
            makeBlack();
            #region 방어 없는 사삼
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field(i, j) == null)
                    {
                        int checkfour = 0;
                        int checkthree = 0;
                        /*******************4가 있는지 확인*************/
                        //위아래
                        if ((i > 3) && (i<15))
                        {
                            if (field(i + 1, j) == null && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'b' && field(i - 4, j) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 14))
                        {
                            if (field(i + 2, j) == null && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == null)
                            {
                                checkfour = 1; 
                            }
                        }
                        if ((i > 1) && (i < 13))
                        {
                            if (field(i + 3, j) == null && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 12))
                        {
                            if (field(i - 1, j) == null && field(i + 1, j) == 'b' && field(i + 2, j) == 'b' && field(i + 3, j) == 'b' && field(i + 4, j) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 15))
                        {
                            if (field(i, j + 1) == null && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'b' && field(i, j - 4) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 2) && (j < 14))
                        {
                            if (field(i, j + 2) == null && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 1) && (j < 13))
                        {
                            if (field(i, j + 3) == null && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 0) && (j < 12))
                        {
                            if (field(i, j - 1) == null && field(i, j + 1) == 'b' && field(i, j + 2) == 'b' && field(i, j + 3) == 'b' && field(i, j + 4) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i<15) && (j < 12) && (j>0))
                        {
                            if (field(i + 1, j - 1) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == 'b' && field(i - 4, j + 4) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 14) && (j < 13) && (j > 1))
                        {
                            if (field(i - 3, j + 3) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j < 14) && (j > 2))
                        {
                            if (field(i - 2, j + 2) == null && field(i - 1, j + 1) == 'b' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j<15) && (j > 3))
                        {
                            if (field(i - 1, j + 1) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == 'b' && field(i + 4, j - 4) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 15) && (j > 3) && (j < 15))
                        {
                            if (field(i + 1, j + 1) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == 'b' && field(i - 4, j - 4) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 14) && (j > 2) && (j < 14))
                        {
                            if (field(i - 3, j - 3) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j > 1) && (j < 13))
                        {
                            if (field(i - 2, j - 2) == null && field(i - 1, j - 1) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j > 0) && (i < 12))
                        {
                            if (field(i - 1, j - 1) == null && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == 'b' && field(i + 4, j + 4) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        /*******************************************************/
                        /******************3이 있는지 확인**********************/
                        //위아래
                        if ((i > 2) && (i < 15))
                        {
                            if (field(i + 1, j) == null && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) ==  null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 14))
                        {
                            if (field(i + 2, j) == null && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 13))
                        {
                            if (field(i + 3, j) == null && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 2) && (j < 15))
                        {
                            if (field(i, j + 1) == null && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((j > 1) && (j < 14))
                        {
                            if (field(i, j + 2) == null && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((j > 0) && (j < 13))
                        {
                            if (field(i, j + 3) == null && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 2) && (i < 15) && (j < 13) && (j > 0))
                        {
                            if (field(i + 1, j - 1) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 14) && (j < 14) && (j > 1))
                        {
                            if (field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 13) && (j < 15) && (j > 2))
                        {
                            if (field(i - 1, j + 1) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 2) && (i < 15) && (j > 2) && (j < 15))
                        {
                            if (field(i + 1, j + 1) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 14) && (j > 1) && (j < 14))
                        {
                            if (field(i - 2, j - 2) == null && field(i - 1, j - 1) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 13) && (j > 0) && (j < 13))
                        {
                            if (field(i - 1, j - 1) == null && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == null)
                            {
                                checkthree = 1;
                            }
                        }
                        /*******************************************************/
                        if ((checkfour + checkthree) == 2)
                        {
                            showValue(i, j, 98); 
                        }
                    }
                }
            }
            #endregion

                // (4) 상대편의 방어가 있는 사삼 - 70점
                makeBlack();
            #region 방어 있는 사삼
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field(i, j) == null)
                    {
                        int checkfour = 0;
                        int checkthree = 0;
                        /*******************4가 있는지 확인*************/
                        //위아래
                        if ((i > 3) && (i < 15))
                        {
                            if (field(i + 1, j) == null && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'b' && field(i - 4, j) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i + 1, j) == null && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'b' && field(i - 4, j) == 'w')
                            {
                                checkfour = 1;
                            }
                            else if (field(i + 1, j) == 'w' && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'b' && field(i - 4, j) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 14))
                        {
                            if (field(i + 2, j) == null && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i + 2, j) == null && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'w')
                            {
                                checkfour = 1;
                            }
                            else if (field(i + 2, j) == 'w' && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == null)
                            {
                                checkfour = 0;
                            }
                        }
                        if ((i > 1) && (i < 13))
                        {
                            if (field(i + 3, j) == null && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i + 3, j) == null && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == 'w')
                            {
                                checkfour = 1;
                            }
                            else if (field(i + 3, j) == 'w' && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 12))
                        {
                            if (field(i - 1, j) == null && field(i + 1, j) == 'b' && field(i + 2, j) == 'b' && field(i + 3, j) == 'b' && field(i + 4, j) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i - 1, j) == null && field(i + 1, j) == 'b' && field(i + 2, j) == 'b' && field(i + 3, j) == 'b' && field(i + 4, j) == 'w')
                            {
                                checkfour = 1;
                            }
                            else if (field(i - 1, j) == 'w' && field(i + 1, j) == 'b' && field(i + 2, j) == 'b' && field(i + 3, j) == 'b' && field(i + 4, j) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 15))
                        {
                            if (field(i, j + 1) == null && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'b' && field(i, j - 4) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i, j + 1) == null && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'b' && field(i, j - 4) == 'w')
                            {
                                checkfour = 1;
                            }
                            else if (field(i, j + 1) == 'w' && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'b' && field(i, j - 4) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 2) && (j < 14))
                        {
                            if (field(i, j + 2) == null && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i, j + 2) == null && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'w')
                            {
                                checkfour = 1;
                            }
                            else if (field(i, j + 2) == 'w' && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 1) && (j < 13))
                        {
                            if (field(i, j + 3) == null && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i, j + 3) == null && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == 'w')
                            {
                                checkfour = 1;
                            }
                            else if (field(i, j + 3) == 'w' && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 0) && (j < 12))
                        {
                            if (field(i, j - 1) == null && field(i, j + 1) == 'b' && field(i, j + 2) == 'b' && field(i, j + 3) == 'b' && field(i, j + 4) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i, j - 1) == 'w' && field(i, j + 1) == 'b' && field(i, j + 2) == 'b' && field(i, j + 3) == 'b' && field(i, j + 4) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i, j - 1) == null && field(i, j + 1) == 'b' && field(i, j + 2) == 'b' && field(i, j + 3) == 'b' && field(i, j + 4) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i < 15) && (j < 12) && (j > 0))
                        {
                            if (field(i + 1, j - 1) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == 'b' && field(i - 4, j + 4) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i + 1, j - 1) == 'w' && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == 'b' && field(i - 4, j + 4) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i + 1, j - 1) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == 'b' && field(i - 4, j + 4) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 14) && (j < 13) && (j > 1))
                        {
                            if (field(i - 3, j + 3) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i - 3, j + 3) == 'w' && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i - 3, j + 3) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j < 14) && (j > 2))
                        {
                            if (field(i - 2, j + 2) == null && field(i - 1, j + 1) == 'b' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i - 2, j + 2) == 'w' && field(i - 1, j + 1) == 'b' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i - 2, j + 2) == null && field(i - 1, j + 1) == 'b' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j < 15) && (j > 3))
                        {
                            if (field(i - 1, j + 1) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == 'b' && field(i + 4, j - 4) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i - 1, j + 1) == 'w' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == 'b' && field(i + 4, j - 4) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i - 1, j + 1) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == 'b' && field(i + 4, j - 4) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 15) && (j > 3) && (j < 15))
                        {
                            if (field(i + 1, j + 1) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == 'b' && field(i - 4, j - 4) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i + 1, j + 1) == 'w' && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == 'b' && field(i - 4, j - 4) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i + 1, j + 1) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == 'b' && field(i - 4, j - 4) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 14) && (j > 2) && (j < 14))
                        {
                            if (field(i - 3, j - 3) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i - 3, j - 3) == 'w' && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i - 3, j - 3) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j > 1) && (j < 13))
                        {
                            if (field(i - 2, j - 2) == null && field(i - 1, j - 1) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i - 2, j - 2) == 'w' && field(i - 1, j - 1) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i - 2, j - 2) == null && field(i - 1, j - 1) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j > 0) && (i < 12))
                        {
                            if (field(i - 1, j - 1) == null && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == 'b' && field(i + 4, j + 4) == null)
                            {
                                checkfour = 0;
                            }
                            else if (field(i - 1, j - 1) == 'w' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == 'b' && field(i + 4, j + 4) == null)
                            {
                                checkfour = 1;
                            }
                            else if (field(i - 1, j - 1) == null && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == 'b' && field(i + 4, j + 4) == 'w')
                            {
                                checkfour = 1;
                            }
                        }
                        /*******************************************************/
                        /******************3이 있는지 확인**********************/
                        //위아래
                        if ((i > 2) && (i < 15))
                        {
                            if (field(i + 1, j) == null && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i + 1, j) == 'w' && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i + 1, j) == null && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 14))
                        {
                            if (field(i + 2, j) == null && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i + 2, j) == 'w' && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i + 2, j) == null && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 13))
                        {
                            if (field(i + 3, j) == null && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i + 3, j) == 'w' && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i + 3, j) == null && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 2) && (j < 15))
                        {
                            if (field(i, j + 1) == null && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i, j + 1) == 'w' && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i, j + 1) == null && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        if ((j > 1) && (j < 14))
                        {
                            if (field(i, j + 2) == null && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i, j + 2) == 'w' && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i, j + 2) == null && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        if ((j > 0) && (j < 13))
                        {
                            if (field(i, j + 3) == null && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i, j + 3) == 'w' && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i, j + 3) == null && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 2) && (i < 15) && (j < 13) && (j > 0))
                        {
                            if (field(i + 1, j - 1) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i + 1, j - 1) == 'w' && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i + 1, j - 1) == null && field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'b' && field(i - 3, j + 3) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 14) && (j < 14) && (j > 1))
                        {
                            if (field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == 'w' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i - 1, j + 1) == 'b' && field(i - 2, j + 2) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 13) && (j < 15) && (j > 2))
                        {
                            if (field(i - 1, j + 1) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i - 1, j + 1) == 'w' && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i - 1, j + 1) == null && field(i + 1, j - 1) == 'b' && field(i + 2, j - 2) == 'b' && field(i + 3, j - 3) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 2) && (i < 15) && (j > 2) && (j < 15))
                        {
                            if (field(i + 1, j + 1) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i + 1, j + 1) == 'w' && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i + 1, j + 1) == null && field(i - 1, j - 1) == 'b' && field(i - 2, j - 2) == 'b' && field(i - 3, j - 3) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 14) && (j > 1) && (j < 14))
                        {
                            if (field(i - 2, j - 2) == null && field(i - 1, j - 1) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i - 2, j - 2) == 'w' && field(i - 1, j - 1) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i - 2, j - 2) == null && field(i - 1, j - 1) == 'b' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 13) && (j > 0) && (j < 13))
                        {
                            if (field(i - 1, j - 1) == null && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == null)
                            {
                                checkthree = 0;
                            }
                            else if (field(i - 1, j - 1) == 'w' && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == null)
                            {
                                checkthree = 1;
                            }
                            else if (field(i - 1, j - 1) == null && field(i + 1, j + 1) == 'b' && field(i + 2, j + 2) == 'b' && field(i + 3, j + 3) == 'w')
                            {
                                checkthree = 1;
                            }
                        }
                        /*******************************************************/
                        if ((checkfour + checkthree) == 1)
                        {
                            showValue(i, j, 70);
                        }//3이나 4쪽 둘중에 하나가 1일경우
                        if ((checkfour + checkthree) == 2)
                        {
                            showValue(i, j, 70);
                        }//3이나 4쪽 둘다 1일경우                        
                    }
                }
            }
            #endregion

            // (5) 상대편의 방어가 없는 사목 - 97점
            makeBlack();
            #region 방어 없는 사목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field(i, j) == null)
                    {
                        //위아래
                        if ((i > 3) && (i < 15))
                        {
                            if (field(i + 1, j) == null && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'b' && field(i - 4, j) == null)
                            {
                                showValue(i, j, 97); 
                            }
                        }
                        if ((i > 2) && (i < 14))
                        {
                            if (field(i + 2, j) == null && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 1) && (i < 13))
                        {
                            if (field(i + 3, j) == null && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 0) && (i < 12))
                        {
                            if (field(i - 1, j) == null && field(i + 1, j) == 'b' && field(i + 2, j) == 'b' && field(i + 3, j) == 'b' && field(i + 4, j) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 15))
                        {
                            if (field(i, j + 1) == null && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'b' && field(i, j - 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((j > 2) && (j < 14))
                        {
                            if (field(i, j + 2) == null && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((j > 1) && (j < 13))
                        {
                            if (field(i, j + 3) == null && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((j > 0) && (j < 12))
                        {
                            if (field(i, j - 1) == null && field(i, j + 1) == 'b' && field(i, j + 2) == 'b' && field(i, j + 3) == 'b' && field(i, j + 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i < 15) && (j < 12) && (j > 0))
                        {
                            if (field(i + 1, j - 1) == null && field(i - 1, j + 1) == 1 && field(i - 2, j + 2) == 1 && field(i - 3, j + 3) == 1 && field(i - 4, j + 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 2) && (i < 14) && (j < 13) && (j > 1))
                        {
                            if (field(i - 3, j + 3) == null && field(i - 1, j + 1) == 1 && field(i - 2, j + 2) == 1 && field(i + 1, j - 1) == 1 && field(i + 2, j - 2) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 1) && (i < 13) && (j < 14) && (j > 2))
                        {
                            if (field(i - 2, j + 2) == null && field(i - 1, j + 1) == 1 && field(i + 1, j - 1) == 1 && field(i + 2, j - 2) == 1 && field(i + 3, j - 3) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 0) && (i < 12) && (j < 15) && (j > 3))
                        {
                            if (field(i - 1, j + 1) == null && field(i + 1, j - 1) == 1 && field(i + 2, j - 2) == 1 && field(i + 3, j - 3) == 1 && field(i + 4, j - 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 15) && (j > 3) && (j < 15))
                        {
                            if (field(i + 1, j + 1) == null && field(i - 1, j - 1) == 1 && field(i - 2, j - 2) == 1 && field(i - 3, j - 3) == 1 && field(i - 4, j - 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 2) && (i < 14) && (j > 2) && (j < 14))
                        {
                            if (field(i - 3, j - 3) == null && field(i - 1, j - 1) == 1 && field(i - 2, j - 2) == 1 && field(i + 1, j + 1) == 1 && field(i + 2, j + 2) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 1) && (i < 13) && (j > 1) && (j < 13))
                        {
                            if (field(i - 2, j - 2) == null && field(i - 1, j - 1) == 1 && field(i + 1, j + 1) == 1 && field(i + 2, j + 2) == 1 && field(i + 3, j + 3) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 0) && (i < 12) && (j > 0) && (i < 12))
                        {
                            if (field(i - 1, j - 1) == null && field(i + 1, j + 1) == 1 && field(i + 2, j + 2) == 1 && field(i + 3, j + 3) == 1 && field(i + 4, j + 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                    }
                }
            }
            #endregion

            // (6) 상대편의 방어가 없고 중간에 하나의 빈칸이 있는 사목 - 33점
            makeBlack();
            #region 방어 없고 중간에 하나의 빈칸이 있는 사목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field(i, j) == null)
                    {
                        //위아래
                        if ((i > 3) && (i < 15))
                        {
                            if (field(i + 1, j) == null && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == 'b' && field(i - 4, j) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 2) && (i < 14))
                        {
                            if (field(i + 2, j) == null && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == 'b' && field(i - 3, j) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 1) && (i < 13))
                        {
                            if (field(i + 3, j) == null && field(i + 2, j) == 'b' && field(i + 1, j) == 'b' && field(i - 1, j) == 'b' && field(i - 2, j) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 0) && (i < 12))
                        {
                            if (field(i - 1, j) == null && field(i + 1, j) == 'b' && field(i + 2, j) == 'b' && field(i + 3, j) == 'b' && field(i + 4, j) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 15))
                        {
                            if (field(i, j + 1) == null && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == 'b' && field(i, j - 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((j > 2) && (j < 14))
                        {
                            if (field(i, j + 2) == null && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == 'b' && field(i, j - 3) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((j > 1) && (j < 13))
                        {
                            if (field(i, j + 3) == null && field(i, j + 2) == 'b' && field(i, j + 1) == 'b' && field(i, j - 1) == 'b' && field(i, j - 2) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((j > 0) && (j < 12))
                        {
                            if (field(i, j - 1) == null && field(i, j + 1) == 'b' && field(i, j + 2) == 'b' && field(i, j + 3) == 'b' && field(i, j + 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i < 15) && (j < 12) && (j > 0))
                        {
                            if (field(i + 1, j - 1) == null && field(i - 1, j + 1) == 1 && field(i - 2, j + 2) == 1 && field(i - 3, j + 3) == 1 && field(i - 4, j + 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 2) && (i < 14) && (j < 13) && (j > 1))
                        {
                            if (field(i - 3, j + 3) == null && field(i - 1, j + 1) == 1 && field(i - 2, j + 2) == 1 && field(i + 1, j - 1) == 1 && field(i + 2, j - 2) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 1) && (i < 13) && (j < 14) && (j > 2))
                        {
                            if (field(i - 2, j + 2) == null && field(i - 1, j + 1) == 1 && field(i + 1, j - 1) == 1 && field(i + 2, j - 2) == 1 && field(i + 3, j - 3) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 0) && (i < 12) && (j < 15) && (j > 3))
                        {
                            if (field(i - 1, j + 1) == null && field(i + 1, j - 1) == 1 && field(i + 2, j - 2) == 1 && field(i + 3, j - 3) == 1 && field(i + 4, j - 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 15) && (j > 3) && (j < 15))
                        {
                            if (field(i + 1, j + 1) == null && field(i - 1, j - 1) == 1 && field(i - 2, j - 2) == 1 && field(i - 3, j - 3) == 1 && field(i - 4, j - 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 2) && (i < 14) && (j > 2) && (j < 14))
                        {
                            if (field(i - 3, j - 3) == null && field(i - 1, j - 1) == 1 && field(i - 2, j - 2) == 1 && field(i + 1, j + 1) == 1 && field(i + 2, j + 2) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 1) && (i < 13) && (j > 1) && (j < 13))
                        {
                            if (field(i - 2, j - 2) == null && field(i - 1, j - 1) == 1 && field(i + 1, j + 1) == 1 && field(i + 2, j + 2) == 1 && field(i + 3, j + 3) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                        if ((i > 0) && (i < 12) && (j > 0) && (i < 12))
                        {
                            if (field(i - 1, j - 1) == null && field(i + 1, j + 1) == 1 && field(i + 2, j + 2) == 1 && field(i + 3, j + 3) == 1 && field(i + 4, j + 4) == null)
                            {
                                showValue(i, j, 97);
                            }
                        }
                    }
                }
            }
            #endregion

            // (7) 한쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 사목 - 32점
            makeBlack();
            #region 방어 있고 중간에 하나의 빈칸이 있는 사목
            #endregion

            // (8) 양쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 사목 - 31점
            makeBlack();
            #region 양쪽에 방어 있고 중간에 하나의 빈칸이 있는 사목
            #endregion

            // (9) 상대편의 방어가 없는 삼삼 - 96점
            makeBlack();
            #region 방어 없는 삼삼
            #endregion

            // (10) 상대편의 방어가 없는 이삼 - 21점
            makeBlack();
            #region 방어 없는 이삼
            #endregion

            // (11) 상대편의 방어가 없는 이이이 - 20점
            makeBlack();
            #region 방어 없는 이이이
            #endregion

            // (12) 방어 없는 삼목 - 10점
            makeBlack();
            #region 방어 없는 삼목
            #endregion

            // (13) 한쪽에 상대편의 방어가 있는 사목 - 9점
            makeBlack();
            #region 방어 있는 사목
            #endregion
            
            // (14) 상대편의 방어가 있는 삼삼 - 8점
            makeBlack();
            #region 방어 있는 삼삼
            #endregion

            // (15) 상대편의 방어가 없고 중간에 하나의 빈칸이 있는 삼목 - 7점
            makeBlack();
            #region 방어 없고 중간에 하나의 빈칸이 있는 삼목
            #endregion

            // (16) 한쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 삼목 - 6점
            makeBlack();
            #region 방어 있고 중간에 하나의 빈칸이 있는 삼목
            #endregion

            // (17) 상대편의 방어가 없는 이이 - 5점
            makeBlack();
            #region 방어 없는 이이
            #endregion

            // (18) 상대편의 방어가 없는 삼목 - 4점
            makeBlack();
            #region 방어 없는 삼목
            #endregion

            // (19) 한쪽에 상대편의 방어가 있는 삼목 - 3점
            makeBlack();
            #region 방어 있는 삼목
            #endregion
            
            // (20) 상대편의 방어가 없는 이목 - 2점
            makeBlack();
            #region 방어 없는 이목
            #endregion
            
            // (21) 한쪽에 상대편의 방어가 있는 이목 - 1점
            makeBlack();
            #region 방어 있는 이목
            #endregion

            return -1;
        }
    }
}
