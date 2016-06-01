using MetroFramework;
using MetroFramework.Controls;
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
        int[,] white = new int[15, 15];

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

        public void makeWhite()
        {
            // init first
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    white[i, j] = 0;
                }
            }

            // make array for Black
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field[i, j].Value != null)
                    {
                        if (String.Compare(field[i, j].Value.ToString(), "W") == 0)
                        {
                            field[i, j].Selected = true;
                            white[i, j] = 1;
                        }
                    }
                }
            }
        }

        public string adventureField(int r, int c)
        {
            if (r >= 0 && r < 15 && c >= 0 && c < 15)
            {
                if (field[r, c].Value != null)
                {
                    return field[r, c].Value.ToString();
                }
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

        public int helpMeJarvis(int helpFor, MetroLabel blackValue, MetroLabel whiteValue)
        {
            string[,] bVal = new string[15, 15];
            string[,] wVal = new string[15, 15];
            int tempB = 0, tempW = 0;
            int totValB = 0, totValW = 0;

            // init
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    bVal[i, j] = null;
                    wVal[i, j] = null;
                }
            }

            // Help For Black
            if (helpFor == 0)
            {
                // 상대에게 2턴 내로 끝낼 수 있는 수가 있다면 생각할 것 없이 바로 막아야 한다.
                // 반대로 나에게 2턴 내로 끝낼 수 있는 수가 있다면 생각할 것 없이 바로 두면 된다.
                #region 킬러
                // 내꺼 먼저 확인
                valueFunctionForBlack();
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (field[i, j].Value != null)
                        {
                            if (field[i, j].Value.ToString() != "B" && field[i, j].Value.ToString() != "W")
                            {
                                tempB += Convert.ToInt32(field[i, j].Value.ToString());
                            }

                            if (field[i, j].Value.ToString() == "100")
                            {
                                // WIN
                                resetValue();
                                field[i, j].Value = "B";
                                blackValue.Text = "Win";
                                return 999;
                            }
                            else if (field[i, j].Value.ToString() == "99" || field[i, j].Value.ToString() == "98" ||
                                field[i, j].Value.ToString() == "97" || field[i, j].Value.ToString() == "96")
                            {
                                resetValue();
                                field[i, j].Value = "B";
                            }
                        }
                    }
                }

                // 상대꺼 확인
                resetValue();
                valueFunctionForWhite();
                for (int i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (field[i, j].Value != null)
                        {
                            if (field[i, j].Value.ToString() != "B" && field[i, j].Value.ToString() != "W")
                            {
                                tempW += Convert.ToInt32(field[i, j].Value.ToString());
                            }

                            if (field[i, j].Value.ToString() == "100")
                            {
                                // RESIGN
                                resetValue();
                                blackValue.Text = "Resign";
                                return -999;
                            }
                            else if (field[i, j].Value.ToString() == "99" || field[i, j].Value.ToString() == "98" ||
                                field[i, j].Value.ToString() == "97" || field[i, j].Value.ToString() == "96")
                            {
                                resetValue();
                                field[i, j].Value = "B";
                            }
                        }
                    }
                }
                resetValue();
                #endregion

                // 킬러 수가 없다고 판단되면 현재 판의 가치를 계산해야한다.
                // 흑의 총점 = 흑 가치 총점 - 백 가치 총점
                // 백의 총점 = 백 가치 총점 - 흑 가치 총점
                // (내 가치 총점과 상대 가치 총점은 킬러 단계에서 모두 계산되어 있다.)
                totValB = tempB - tempW;
                totValW = tempW - tempB;
                blackValue.Text = totValB.ToString();
                whiteValue.Text = totValW.ToString();

                // 2수 이상의 앞을 보는 미래를 보는 수는 경험에 의해 수를 두도록 해야할 것 같다.
                // 좋은 아이디어가 떠오르지 않는다. 
            }

            // Help For White
            else if (helpFor == 1)
            {

            }

            return -1;
        }

        //흑백이 없으면 1, 있으면 0
        public Boolean isEmpty(int a, int b)
        {
            if (field[a, b].Value == null || (field[a, b].Value != null && field[a, b].Value.ToString() != "W" && field[a, b].Value.ToString() != "B"))
            {
                return true;
            }
            else return false;
        }
        //흰돌이있으면 1, 없으면 0
        public Boolean isWhite(int a, int b)
        {
            if (field[a, b].Value != null && field[a, b].Value.ToString() == "W")
            {
                return true;
            }
            else return false;
        }
        //검은돌이 있으면 1, 없으면 0
        public Boolean isBlack(int a, int b)
        {
            if (field[a, b].Value != null && field[a, b].Value.ToString() == "B")
            {
                return true;
            }
            else return false;
        }

        public int valueFunctionForBlack()
        { 
            string piece = null;
            int row = 0, col = 0;
            bool found = false;

            int cnt = 0, totCnt = 0;
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
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    black[i, j] = 1;
                }
            }
            #region 사사
            while (true)
            {
                found = false;
                piece = "B";

                // 모든 셀에 대하여 8방향 탐색 후 3줄짜리가 2개 이상이면 해당 셀은 사사 가치를 가진다.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (black[i, j] == 1)
                        {
                            black[i, j] = 0;
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
                totCnt = 0;
                i = 1;
                ret = null;
                // (1) 오른쪽으로 탐색
                if (row + 1 < 15)
                {
                    ret = adventureField(row + i, col);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }
                // (2) 오른쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col + 1 < 15)
                {
                    ret = adventureField(row + i, col + i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (3) 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col + 1 < 15)
                {
                    ret = adventureField(row, col + i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (4) 왼쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col + 1 < 15)
                {
                    ret = adventureField(row - i, col + i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (5) 왼쪽으로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0)
                {
                    ret = adventureField(row - i, col);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (6) 왼쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    ret = adventureField(row - i, col - i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (7) 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col - 1 >= 0)
                {
                    ret = adventureField(row, col - i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (8) 오른쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col - 1 >= 0)
                {
                    ret = adventureField(row + i, col - i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }
                if (totCnt > 1)
                {
                    if (adv[row, col] == 0)
                    {
                        adv[row, col] = 1;
                        showValue(row, col, 99);
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
                    if (field[i, j].Value == null)
                    {
                        int checkfour = 0;
                        int checkthree = 0;
                        /*******************4가 있는지 확인*************/
                        //위아래
                        if ((i > 3) && (i < 14))
                        {
                            if (field[i + 1, j].Value == null && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 13))
                        {
                            if (field[i + 2, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 1) && (i < 12))
                        {
                            if (field[i + 3, j].Value == null && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 11))
                        {
                            if (field[i - 1, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 4, j].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 14))
                        {
                            if (field[i, j + 1].Value == null && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 2) && (j < 13))
                        {
                            if (field[i, j + 2].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 1) && (j < 12))
                        {
                            if (field[i, j + 3].Value == null && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((j > 0) && (j < 11))
                        {
                            if (field[i, j - 1].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 4].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i < 14) && (j < 11) && (j > 0))
                        {
                            if (field[i + 1, j - 1].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 13) && (j < 12) && (j > 1))
                        {
                            if (field[i - 3, j + 3].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 1) && (i < 12) && (j < 13) && (j > 2))
                        {
                            if (field[i - 2, j + 2].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 11) && (j < 14) && (j > 3))
                        {
                            if (field[i - 1, j + 1].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 4, j - 4].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 14) && (j > 3) && (j < 14))
                        {
                            if (field[i + 1, j + 1].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 4, j - 4].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 2) && (i < 13) && (j > 2) && (j < 13))
                        {
                            if (field[i - 3, j - 3].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 1) && (i < 12) && (j > 1) && (j < 12))
                        {
                            if (field[i - 2, j - 2].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        if ((i > 0) && (i < 11) && (j > 0) && (j < 11))
                        {
                            if (field[i - 1, j - 1].Value == null && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value == null)
                            {
                                checkfour = 1;
                            }
                        }
                        /*******************************************************/
                        /******************3이 있는지 확인**********************/
                        //위아래
                        if ((i > 2) && (i < 14))
                        {
                            if (field[i + 1, j].Value == null && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 13))
                        {
                            if (field[i + 2, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 12))
                        {
                            if (field[i + 3, j].Value == null && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 2) && (j < 14))
                        {
                            if (field[i, j + 1].Value == null && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((j > 1) && (j < 13))
                        {
                            if (field[i, j + 2].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((j > 0) && (j < 12))
                        {
                            if (field[i, j + 3].Value == null && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 2) && (i < 14) && (j < 12) && (j > 0))
                        {
                            if (field[i + 1, j - 1].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j < 13) && (j > 1))
                        {
                            if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j < 14) && (j > 2))
                        {
                            if (field[i - 1, j + 1].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 2) && (i < 14) && (j > 2) && (j < 14))
                        {
                            if (field[i + 1, j + 1].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 3, j - 3].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j > 1) && (j < 13))
                        {
                            if (field[i - 2, j - 2].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j > 0) && (j < 12))
                        {
                            if (field[i - 1, j - 1].Value == null && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value == null)
                            {
                                checkthree = 1;
                            }
                        }
                        /*******************************************************/
                        if ((checkfour + checkthree) == 2)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 98);
                            }
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
                    if (field[i, j].Value == null)
                    {
                        int checkfour = 0;
                        int checkthree = 0;
                        /*******************4가 있는지 확인*************/
                        //위아래
                        if ((i > 3) && (i < 14))
                        {
                            if (field[i + 1, j].Value == null && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i + 1, j].Value == null && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                            else if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "W" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value == null)
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 2) && (i < 13))
                        {
                            if (field[i + 2, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i + 2, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                            else if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "W" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value == null)
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 1) && (i < 12))
                        {
                            if (field[i + 3, j].Value == null && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i + 3, j].Value == null && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                            else if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "W" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value == null)
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 0) && (i < 11))
                        {
                            if (field[i - 1, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 4, j].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i - 1, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                            else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "W" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 4, j].Value == null)
                            {
                                checkfour = 2;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 14))
                        {
                            if (field[i, j + 1].Value == null && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i, j + 1].Value == null && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                            else if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "W" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value == null)
                            {
                                checkfour = 2;
                            }
                        }
                        if ((j > 2) && (j < 13))
                        {
                            if (field[i, j + 2].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i, j + 2].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                            else if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "W" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value == null)
                            {
                                checkfour = 2;
                            }
                        }
                        if ((j > 1) && (j < 12))
                        {
                            if (field[i, j + 3].Value == null && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i, j + 3].Value == null && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                            else if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "W" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value == null)
                            {
                                checkfour = 2;
                            }
                        }
                        if ((j > 0) && (j < 11))
                        {
                            if (field[i, j - 1].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 4].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "W" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 4].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i, j - 1].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i < 14) && (j < 11) && (j > 0))
                        {
                            if (field[i + 1, j - 1].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "W" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i + 1, j - 1].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 2) && (i < 13) && (j < 12) && (j > 1))
                        {
                            if (field[i - 3, j + 3].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "W" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i - 3, j + 3].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 1) && (i < 12) && (j < 13) && (j > 2))
                        {
                            if (field[i - 2, j + 2].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "W" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i - 2, j + 2].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 0) && (i < 11) && (j < 14) && (j > 3))
                        {
                            if (field[i - 1, j + 1].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 4, j - 4].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "W" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 4, j - 4].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i - 1, j + 1].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 14) && (j > 3) && (j < 14))
                        {
                            if (field[i + 1, j + 1].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 4, j - 4].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "W" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 4, j - 4].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i + 1, j + 1].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 2) && (i < 13) && (j > 2) && (j < 13))
                        {
                            if (field[i - 3, j - 3].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "W" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i - 3, j - 3].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 1) && (i < 12) && (j > 1) && (j < 12))
                        {
                            if (field[i - 2, j - 2].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "W" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i - 2, j - 2].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        if ((i > 0) && (i < 11) && (j > 0) && (j < 11))
                        {
                            if (field[i - 1, j - 1].Value == null && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value == null)
                            {
                                checkfour = 1;
                            }
                            else if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "W" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value == null)
                            {
                                checkfour = 2;
                            }
                            else if (field[i - 1, j - 1].Value == null && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "W")
                            {
                                checkfour = 2;
                            }
                        }
                        /*******************************************************/
                        /******************3이 있는지 확인**********************/
                        //위아래
                        if ((i > 2) && (i < 14))
                        {
                            if (field[i + 1, j].Value == null && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "W" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i + 1, j].Value == null && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        if ((i > 1) && (i < 13))
                        {
                            if (field[i + 2, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "W" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i + 2, j].Value == null && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        if ((i > 0) && (i < 12))
                        {
                            if (field[i + 3, j].Value == null && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "W" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i + 3, j].Value == null && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 2) && (j < 14))
                        {
                            if (field[i, j + 1].Value == null && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "W" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i, j + 1].Value == null && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        if ((j > 1) && (j < 13))
                        {
                            if (field[i, j + 2].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "W" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i, j + 2].Value == null && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        if ((j > 0) && (j < 12))
                        {
                            if (field[i, j + 3].Value == null && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "W" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i, j + 3].Value == null && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 2) && (i < 14) && (j < 12) && (j > 0))
                        {
                            if (field[i + 1, j - 1].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "W" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i + 1, j - 1].Value == null && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j < 13) && (j > 1))
                        {
                            if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "W" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j < 14) && (j > 2))
                        {
                            if (field[i - 1, j + 1].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "W" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i - 1, j + 1].Value == null && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 2) && (i < 14) && (j > 2) && (j < 14))
                        {
                            if (field[i + 1, j + 1].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 3, j - 3].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "W" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 3, j - 3].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i + 1, j + 1].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j > 1) && (j < 13))
                        {
                            if (field[i - 2, j - 2].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "W" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i - 2, j - 2].Value == null && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j > 0) && (j < 12))
                        {
                            if (field[i - 1, j - 1].Value == null && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value == null)
                            {
                                checkthree = 1;
                            }
                            else if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "W" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value == null)
                            {
                                checkthree = 2;
                            }
                            else if (field[i - 1, j - 1].Value == null && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "W")
                            {
                                checkthree = 2;
                            }
                        }
                        /*******************************************************/
                        if ((checkfour + checkthree) == 3)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 70);
                            }
                        }//3이나 4쪽 둘중에 하나가 1일경우                        
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
                    if (field[i, j].Value == null)
                    {
                        int check = 0;
                        //위아래
                        if ((i > 4) && (i < 14))
                        {
                            if ((field[i + 1, j].Value == null || (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() != "W" && field[i + 1, j].Value.ToString() != "B")) && (field[i - 5, j].Value == null || (field[i-5, j].Value != null && field[i-5, j].Value.ToString() != "W" && field[i-5, j].Value.ToString() != "B")))
                            {
                                if ((field[i - 1, j].Value == null || (field[i-1, j].Value != null && field[i-1, j].Value.ToString() != "W" && field[i-1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && (field[i - 2, j].Value == null || (field[i-2, j].Value != null && field[i-2, j].Value.ToString() != "W" && field[i-2, j].Value.ToString() != "B")) && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && (field[i - 3, j].Value == null || (field[i-3, j].Value != null && field[i-3, j].Value.ToString() != "W" && field[i-3, j].Value.ToString() != "B")) && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }

                            }
                        }
                        if ((i > 3) && (i < 13))
                        {
                            if ((field[i + 2, j].Value == null || (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() != "W" && field[i + 2, j].Value.ToString() != "B")) && (field[i - 4, j].Value == null || (field[i-4, j].Value != null && field[i-4, j].Value.ToString() != "W" && field[i-4, j].Value.ToString() != "B")))
                            {
                                if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && (field[i - 1, j].Value == null || (field[i-1, j].Value != null && field[i-1, j].Value.ToString() != "W" && field[i-1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && (field[i - 2, j].Value == null || (field[i-2, j].Value != null && field[i-2, j].Value.ToString() != "W" && field[i-2, j].Value.ToString() != "B")) && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12))
                        {
                            if ((field[i + 3, j].Value == null || (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() != "W" && field[i + 3, j].Value.ToString() != "B")) && (field[i - 3, j].Value == null || (field[i-3, j].Value != null && field[i-3, j].Value.ToString() != "W" && field[i-3, j].Value.ToString() != "B")))
                            {
                                if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i+1, j].Value != null && field[i+1, j].Value.ToString() != "W" && field[i+1, j].Value.ToString() != "B")) && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && (field[i - 1, j].Value == null || (field[i-1, j].Value != null && field[i-1, j].Value.ToString() != "W" && field[i-1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11))
                        {
                            if ((field[i + 4, j].Value == null || (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() != "W" && field[i + 4, j].Value.ToString() != "B")) && (field[i - 2, j].Value == null || (field[i-2, j].Value != null && field[i-2, j].Value.ToString() != "W" && field[i-2, j].Value.ToString() != "B")))
                            {
                                if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && (field[i + 2, j].Value == null || (field[i+2, j].Value != null && field[i+2, j].Value.ToString() != "W" && field[i+2, j].Value.ToString() != "B")) && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i+1, j].Value != null && field[i+1, j].Value.ToString() != "W" && field[i+1, j].Value.ToString() != "B")) && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 10))
                        {
                            if ((field[i + 5, j].Value == null || (field[i + 5, j].Value != null && field[i + 5, j].Value.ToString() != "W" && field[i + 5, j].Value.ToString() != "B")) && (field[i - 1, j].Value == null || (field[i-1, j].Value != null && field[i-1, j].Value.ToString() != "W" && field[i-1, j].Value.ToString() != "B")))
                            {
                                if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && (field[i + 3, j].Value == null || (field[i+3, j].Value != null && field[i+3, j].Value.ToString() != "W" && field[i+3, j].Value.ToString() != "B")) && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && isEmpty(i + 2, j) && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && isEmpty(i+1,j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 4) && (j < 14))
                        {
                            if (isEmpty(i,j + 1) && isEmpty(i,j - 5))
                            {
                                if (isEmpty(i,j-1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && isEmpty(i,j - 2) && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && isEmpty(i,j - 3) && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 3) && (j < 13))
                        {
                            if (isEmpty(i,j + 2) && isEmpty(i,j - 4))
                            {
                                if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && isEmpty(i,j - 1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && isEmpty(i,j - 2) && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 2) && (j < 12))
                        {
                            if (isEmpty(i,j + 3) && isEmpty(i,j - 3))
                            {
                                if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i,j + 1) && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && isEmpty(i,j - 1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 1) && (j < 11))
                        {
                            if (isEmpty(i,j + 4) && isEmpty(i,j - 2))
                            {
                                if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && isEmpty(i,j + 2) && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i,j + 1) && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 0) && (j < 10))
                        {
                            if (isEmpty(i,j + 5) && isEmpty(i,j - 1))
                            {
                                if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && isEmpty(i,j + 3) && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && isEmpty(i,j + 2) && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i,j + 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i < 14) && (i > 4) && (j > 0) && (j < 10))
                        {
                            if (isEmpty(i+1,j -1) && isEmpty(i - 5,j + 5))
                            {
                                if (isEmpty(i - 1,j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && isEmpty(i - 2,j + 2) && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && isEmpty(i - 3,j + 3) && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }

                            }
                        }
                        if ((i < 13) && (i > 3) && (j > 1) && (j < 11))
                        {
                            if (isEmpty(i + 2,j - 2) && isEmpty(i - 4,j + 4))
                            {
                                if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && isEmpty(i - 1,j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && isEmpty(i - 2,j + 2) && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12) && (j > 2) && (j < 12))
                        {
                            if (isEmpty(i + 3,j - 3) && isEmpty(i - 3,j + 3))
                            {
                                if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1,j - 1) && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && isEmpty(i - 1,j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11) && (j > 3) && (j < 13))
                        {
                            if (isEmpty(i + 4,j - 4) && isEmpty(i - 2,j + 2))
                            {
                                if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && isEmpty(i + 2,j - 2) && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1,j - 1) && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 10) && (j > 4) && (j < 14))
                        {
                            if (isEmpty(i + 5,j - 5) && isEmpty(i - 1,j + 1))
                            {
                                if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && isEmpty(i + 3,j - 3) && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && isEmpty(i + 2,j - 2) && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1,j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 0) && (i < 10) && (j > 0) && (j < 10))
                        {
                            if (isEmpty(i - 1,j - 1) && isEmpty(i + 5,j + 5))
                            {
                                if (isEmpty(i + 1,j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && isEmpty(i + 2,j + 2) && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && isEmpty(i + 3,j + 3) && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11) && (j > 1) && (j < 11))
                        {
                            if (isEmpty(i - 2,j - 2) && isEmpty(i + 4,j + 4))
                            {
                                if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && isEmpty(i+1,j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && isEmpty(i+2,j + 2) && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12) && (j > 2) && (j < 12))
                        {
                            if (isEmpty(i - 3,j - 3) && isEmpty(i + 3,j + 3))
                            {
                                if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1,j - 1) && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && isEmpty(i + 1,j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 3) && (i < 13) && (j > 3) && (j < 13))
                        {
                            if (isEmpty(i - 4,j - 4) && isEmpty(i + 2,j + 2))
                            {
                                if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && isEmpty(i - 2,j - 2) && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1,j - 1) && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 4) && (i < 14) && (j > 4) && (j < 14))
                        {
                            if (isEmpty(i - 5,j - 5) && isEmpty(i+1,j + 1))
                            {
                                if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && isEmpty(i - 3,j - 3) && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && isEmpty(i - 2,j - 2) && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1,j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if (check == 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 33);
                            }
                        }
                    }
                }
            }
            #endregion

            // (14) 상대편의 방어가 있는 삼삼 - 8점
            makeBlack();
            #region 방어 있는 삼삼
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (isEmpty(i, j))
                    {
                        int blocked = 0, nonblocked = 0;
                        /*****************3개되는 줄찾기*****************************/
                        //위아래
                        if ((i > 2) && (i < 14))
                        {
                            if (isBlack(i - 1, j) && isBlack(i - 2, j))
                            {
                                if (isEmpty(i + 1, j) && isEmpty(i - 3, j))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 1, j) && isEmpty(i - 3, j))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 1, j) && isWhite(i - 3, j))
                                {
                                    blocked++;
                                }
                            }
                        }
                        if ((i > 1) && (i < 13))
                        {
                            if (isBlack(i + 1, j) && isBlack(i - 1, j))
                            {
                                if (isEmpty(i + 2, j) && isEmpty(i - 2, j))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 2, j) && isEmpty(i - 2, j))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 2, j) && isWhite(i - 2, j))
                                {
                                    blocked++;
                                }
                            }
                        }
                        if ((i > 0) && (i < 12))
                        {
                            if (isBlack(i + 2, j) && isBlack(i + 1, j))
                            {
                                if (isEmpty(i + 3, j) && isEmpty(i - 1, j))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 3, j) && isEmpty(i - 1, j))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 3, j) && isWhite(i - 1, j))
                                {
                                    blocked++;
                                }
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 2) && (j < 14))
                        {
                            if (isBlack(i, j - 1) && isBlack(i, j - 2))
                            {
                                if (isEmpty(i, j + 1) && isEmpty(i, j - 3))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i, j + 1) && isEmpty(i, j - 3))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i, j + 1) && isWhite(i, j - 3))
                                {
                                    blocked++;
                                }
                            }
                        }
                        if ((j > 1) && (j < 13))
                        {
                            if (isBlack(i, j + 1) && isBlack(i, j - 1))
                            {
                                if (isEmpty(i, j + 2) && isEmpty(i, j - 2))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i, j + 2) && isEmpty(i, j - 2))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i, j + 2) && isWhite(i, j - 2))
                                {
                                    blocked++;
                                };
                            }
                        }
                        if ((j > 0) && (j < 12))
                        {
                            if (isBlack(i, j + 2) && isBlack(i, j + 1))
                            {
                                if (isEmpty(i, j + 3) && isEmpty(i, j - 1))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i, j + 3) && isEmpty(i, j - 1))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i, j + 3) && isWhite(i, j - 1))
                                {
                                    blocked++;
                                }
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 2) && (i < 14) && (j < 12) && (j > 0))
                        {
                            if (isBlack(i - 1, j + 1) && isBlack(i - 2, j + 2))
                            {
                                if (isEmpty(i + 1, j - 1) && isEmpty(i - 3, j + 3))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 1, j - 1) && isEmpty(i - 3, j + 3))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 1, j - 1) && isWhite(i - 3, j + 3))
                                {
                                    blocked++;
                                }
                            }
                        }
                        if ((i > 1) && (i < 13) && (j < 13) && (j > 1))
                        {
                            if (isBlack(i - 1, j + 1) && isBlack(i + 1, j - 1))
                            {
                                if (isEmpty(i + 2, j - 2) && isEmpty(i - 2, j + 2))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 2, j - 2) && isEmpty(i - 2, j + 2))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 2, j - 2) && isWhite(i - 2, j + 2))
                                {
                                    blocked++;
                                }
                            }
                        }
                        if ((i > 0) && (i < 12) && (j < 14) && (j > 2))
                        {
                            if (isBlack(i + 1, j - 1) && isBlack(i + 2, j - 2))
                            {
                                if (isEmpty(i + 3, j - 3) && isEmpty(i - 1, j + 1))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 3, j - 3) && isEmpty(i - 1, j + 1))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 3, j - 3) && isWhite(i - 1, j + 1))
                                {
                                    blocked++;
                                }
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 2) && (i < 14) && (j > 2) && (j < 14))
                        {
                            if (isBlack(i - 1, j - 1) && isBlack(i - 2, j - 2))
                            {
                                if (isEmpty(i + 1, j + 1) && isEmpty(i - 3, j - 3))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 1, j + 1) && isEmpty(i - 3, j - 3))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 1, j + 1) && isWhite(i - 3, j - 3))
                                {
                                    blocked++;
                                }
                            }
                        }
                        if ((i > 1) && (i < 13) && (j > 1) && (j < 13))
                        {
                            if (isBlack(i - 1, j - 1) && isBlack(i + 1, j + 1))
                            {
                                if (isEmpty(i + 2, j + 2) && isEmpty(i - 2, j - 2))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 2, j + 2) && isEmpty(i - 2, j - 2))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 2, j + 2) && isWhite(i - 2, j - 2))
                                {
                                    blocked++;
                                }
                            }
                        }
                        if ((i > 0) && (i < 12) && (j > 0) && (j < 12))
                        {
                            if (isBlack(i + 1, j + 1) && isBlack(i + 2, j + 2))
                            {
                                if (isEmpty(i + 3, j + 3) && isEmpty(i - 1, j - 1))
                                {
                                    nonblocked++;
                                }
                                else if (isWhite(i + 3, j + 3) && isEmpty(i - 1, j - 1))
                                {
                                    blocked++;
                                }
                                else if (isEmpty(i + 3, j + 3) && isWhite(i - 1, j - 1))
                                {
                                    blocked++;
                                }
                            }
                        }
                        /*******************************************************/
                        if ((nonblocked == 1) && (blocked > 0))
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 8);
                            }
                        }
                    }
                }
            }
            #endregion

            // (9) 상대편의 방어가 없는 삼삼 - 96점
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    black[i, j] = 1;
                }
            }
            #region 방어 없는 삼삼
            while (true)
            {
                found = false;
                piece = "B";

                // 모든 셀에 대하여 8방향 탐색 후 2줄짜리가 2개 이상이면 해당 셀은 삼삼 가치를 가진다.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (black[i, j] == 1)
                        {
                            black[i, j] = 0;
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
                totCnt = 0;
                i = 1;
                ret = null;
                // (1) 오른쪽으로 탐색
                if (row + 1 < 15)
                {
                    ret = adventureField(row + i, col);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }
                // (2) 오른쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col + 1 < 15)
                {
                    ret = adventureField(row + i, col + i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (3) 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col + 1 < 15)
                {
                    ret = adventureField(row, col + i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (4) 왼쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col + 1 < 15)
                {
                    ret = adventureField(row - i, col + i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (5) 왼쪽으로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0)
                {
                    ret = adventureField(row - i, col);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (6) 왼쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    ret = adventureField(row - i, col - i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (7) 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col - 1 >= 0)
                {
                    ret = adventureField(row, col - i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (8) 오른쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col - 1 >= 0)
                {
                    ret = adventureField(row + i, col - i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }
                if (totCnt > 1)
                {
                    if (adv[row, col] == 0)
                    {
                        adv[row, col] = 1;
                        showValue(row, col, 96);
                    }
                }
            }
            #endregion
            
            // (7) 한쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 사목 - 32점
            makeBlack();
            #region 방어 있고 중간에 하나의 빈칸이 있는 사목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field[i, j].Value == null)
                    {
                        int check = 0;
                        //위아래
                        if ((i > 4) && (i < 14))
                        {
                            if (isWhite(i + 1, j) && isEmpty(i - 5, j))
                            {
                                if ((field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && (field[i - 2, j].Value == null || (field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() != "W" && field[i - 2, j].Value.ToString() != "B")) && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && (field[i - 3, j].Value == null || (field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() != "W" && field[i - 3, j].Value.ToString() != "B")) && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 1, j) && isWhite(i - 5, j))
                            {
                                if ((field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && (field[i - 2, j].Value == null || (field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() != "W" && field[i - 2, j].Value.ToString() != "B")) && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && (field[i - 3, j].Value == null || (field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() != "W" && field[i - 3, j].Value.ToString() != "B")) && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 3) && (i < 13))
                        {
                            if (isWhite(i + 2, j) && isEmpty(i - 4, j))
                            {
                                if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && (field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && (field[i - 2, j].Value == null || (field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() != "W" && field[i - 2, j].Value.ToString() != "B")) && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 2, j) && isWhite(i - 4, j))
                            {
                                if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && (field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && (field[i - 2, j].Value == null || (field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() != "W" && field[i - 2, j].Value.ToString() != "B")) && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12))
                        {
                            if (isWhite(i + 3, j) && isEmpty(i - 3, j))
                            {
                                if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() != "W" && field[i + 1, j].Value.ToString() != "B")) && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && (field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 3, j) && isWhite(i - 3, j))
                            {
                                if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() != "W" && field[i + 1, j].Value.ToString() != "B")) && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && (field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11))
                        {
                            if (isWhite(i + 4, j) && isEmpty(i - 2, j))
                            {
                                if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && (field[i + 2, j].Value == null || (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() != "W" && field[i + 2, j].Value.ToString() != "B")) && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() != "W" && field[i + 1, j].Value.ToString() != "B")) && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 4, j) && isWhite(i - 2, j))
                            {
                                if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && (field[i + 2, j].Value == null || (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() != "W" && field[i + 2, j].Value.ToString() != "B")) && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() != "W" && field[i + 1, j].Value.ToString() != "B")) && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 10))
                        {
                            if ( isWhite(i + 5, j) && isEmpty(i - 1, j))
                            {
                                if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && (field[i + 3, j].Value == null || (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() != "W" && field[i + 3, j].Value.ToString() != "B")) && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && isEmpty(i + 2, j) && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && isEmpty(i + 1, j))
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 5, j) && isWhite(i - 1, j))
                            {
                                if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && (field[i + 3, j].Value == null || (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() != "W" && field[i + 3, j].Value.ToString() != "B")) && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && isEmpty(i + 2, j) && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && isEmpty(i + 1, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 4) && (j < 14))
                        {
                            if (isWhite(i, j + 1) && isEmpty(i, j - 5))
                            {
                                if (isEmpty(i, j - 1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && isEmpty(i, j - 2) && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && isEmpty(i, j - 3) && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i, j + 1) && isWhite(i, j - 5))
                            {
                                if (isEmpty(i, j - 1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && isEmpty(i, j - 2) && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && isEmpty(i, j - 3) && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 3) && (j < 13))
                        {
                            if (isWhite(i, j + 2) && isEmpty(i, j - 4))
                            {
                                if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && isEmpty(i, j - 1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && isEmpty(i, j - 2) && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i, j + 2) && isWhite(i, j - 4))
                            {
                                if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && isEmpty(i, j - 1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && isEmpty(i, j - 2) && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 2) && (j < 12))
                        {
                            if (isWhite(i, j + 3) && isEmpty(i, j - 3))
                            {
                                if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i, j + 1) && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && isEmpty(i, j - 1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i, j + 3) && isWhite(i, j - 3))
                            {
                                if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i, j + 1) && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && isEmpty(i, j - 1) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 1) && (j < 11))
                        {
                            if (isWhite(i, j + 4) && isEmpty(i, j - 2))
                            {
                                if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && isEmpty(i, j + 2) && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i, j + 1) && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i, j + 4) && isWhite(i, j - 2))
                            {
                                if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && isEmpty(i, j + 2) && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i, j + 1) && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 0) && (j < 10))
                        {
                            if (isWhite(i, j + 5) && isEmpty(i, j - 1))
                            {
                                if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && isEmpty(i, j + 3) && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && isEmpty(i, j + 2) && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i, j + 1))
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i, j + 5) && isWhite(i, j - 1))
                            {
                                if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && isEmpty(i, j + 3) && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && isEmpty(i, j + 2) && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && isEmpty(i, j + 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i < 14) && (i > 4) && (j > 0) && (j < 10))
                        {
                            if (isWhite(i + 1, j - 1) && isEmpty(i - 5, j + 5))
                            {
                                if (isEmpty(i - 1, j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && isEmpty(i - 2, j + 2) && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && isEmpty(i - 3, j + 3) && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 1, j - 1) && isWhite(i - 5, j + 5))
                            {
                                if (isEmpty(i - 1, j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && isEmpty(i - 2, j + 2) && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && isEmpty(i - 3, j + 3) && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i < 13) && (i > 3) && (j > 1) && (j < 11))
                        {
                            if (isWhite(i + 2, j - 2) && isEmpty(i - 4, j + 4))
                            {
                                if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && isEmpty(i - 1, j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && isEmpty(i - 2, j + 2) && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 2, j - 2) && isWhite(i - 4, j + 4))
                            {
                                if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && isEmpty(i - 1, j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && isEmpty(i - 2, j + 2) && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12) && (j > 2) && (j < 12))
                        {
                            if (isWhite(i + 3, j - 3) && isEmpty(i - 3, j + 3))
                            {
                                if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1, j - 1) && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && isEmpty(i - 1, j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 3, j - 3) && isWhite(i - 3, j + 3))
                            {
                                if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1, j - 1) && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && isEmpty(i - 1, j + 1) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11) && (j > 3) && (j < 13))
                        {
                            if (isWhite(i + 4, j - 4) && isEmpty(i - 2, j + 2))
                            {
                                if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && isEmpty(i + 2, j - 2) && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1, j - 1) && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 4, j - 4) && isWhite(i - 2, j + 2))
                            {
                                if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && isEmpty(i + 2, j - 2) && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1, j - 1) && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 10) && (j > 4) && (j < 14))
                        {
                            if (isWhite(i + 5, j - 5) && isEmpty(i - 1, j + 1))
                            {
                                if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && isEmpty(i + 3, j - 3) && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && isEmpty(i + 2, j - 2) && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1, j - 1))
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i + 5, j - 5) && isWhite(i - 1, j + 1))
                            {
                                if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && isEmpty(i + 3, j - 3) && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && isEmpty(i + 2, j - 2) && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && isEmpty(i + 1, j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 0) && (i < 10) && (j > 0) && (j < 10))
                        {
                            if (isWhite(i - 1, j - 1) && isEmpty(i + 5, j + 5))
                            {
                                if (isEmpty(i + 1, j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && isEmpty(i + 2, j + 2) && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && isEmpty(i + 3, j + 3) && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i - 1, j - 1) && isWhite(i + 5, j + 5))
                            {
                                if (isEmpty(i + 1, j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && isEmpty(i + 2, j + 2) && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && isEmpty(i + 3, j + 3) && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11) && (j > 1) && (j < 11))
                        {
                            if (isWhite(i - 2, j - 2) && isEmpty(i + 4, j + 4))
                            {
                                if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && isEmpty(i + 1, j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && isEmpty(i + 2, j + 2) && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i - 2, j - 2) && isWhite(i + 4, j + 4))
                            {
                                if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && isEmpty(i + 1, j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && isEmpty(i + 2, j + 2) && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12) && (j > 2) && (j < 12))
                        {
                            if (isWhite(i - 3, j - 3) && isEmpty(i + 3, j + 3))
                            {
                                if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1, j - 1) && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && isEmpty(i + 1, j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i - 3, j - 3) && isWhite(i + 3, j + 3))
                            {
                                if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1, j - 1) && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && isEmpty(i + 1, j + 1) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 3) && (i < 13) && (j > 3) && (j < 13))
                        {
                            if (isWhite(i - 4, j - 4) && isEmpty(i + 2, j + 2))
                            {
                                if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && isEmpty(i - 2, j - 2) && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1, j - 1) && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i - 4, j - 4) && isWhite(i + 2, j + 2))
                            {
                                if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && isEmpty(i - 2, j - 2) && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1, j - 1) && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 4) && (i < 14) && (j > 4) && (j < 14))
                        {
                            if (isWhite(i - 5, j - 5) && isEmpty(i + 1, j + 1))
                            {
                                if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && isEmpty(i - 3, j - 3) && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && isEmpty(i - 2, j - 2) && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1, j - 1))
                                {
                                    check = 1;
                                }
                            }
                            else if (isEmpty(i - 5, j - 5) && isWhite(i + 1, j + 1))
                            {
                                if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && isEmpty(i - 3, j - 3) && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && isEmpty(i - 2, j - 2) && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && isEmpty(i - 1, j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if (check == 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 32);
                            }
                        }
                    }
                }
            }
            #endregion

            // (8) 양쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 사목 - 31점
            makeBlack();
            #region 양쪽에 방어 있고 중간에 하나의 빈칸이 있는 사목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (field[i, j].Value == null)
                    {
                        int check = 0;
                        //위아래
                        if ((i > 4) && (i < 14))
                        {
                            if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "W" && field[i - 5, j].Value != null && field[i - 5, j].Value.ToString() == "W")
                            {
                                if ((field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() !="B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && (field[i - 2, j].Value == null || (field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() != "W" && field[i - 2, j].Value.ToString() != "B")) && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && (field[i - 3, j].Value == null || (field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() != "W" && field[i - 3, j].Value.ToString() != "B")) && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }

                            }
                        }
                        if ((i > 3) && (i < 13))
                        {
                            if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "W" && field[i - 4, j].Value != null && field[i - 4, j].Value.ToString() == "W")
                            {
                                if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && (field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && (field[i - 2, j].Value == null || (field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() != "W" && field[i - 2, j].Value.ToString() != "B")) && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12))
                        {
                            if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "W" && field[i - 3, j].Value != null && field[i - 3, j].Value.ToString() == "W")
                            {
                                if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() != "W" && field[i + 1, j].Value.ToString() != "B")) && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && (field[i - 1, j].Value == null || (field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() != "W" && field[i - 1, j].Value.ToString() != "B")) && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11))
                        {
                            if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "W" && field[i - 2, j].Value != null && field[i - 2, j].Value.ToString() == "W")
                            {
                                if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && (field[i + 2, j].Value == null || (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() != "W" && field[i + 2, j].Value.ToString() != "B")) && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() != "W" && field[i + 1, j].Value.ToString() != "B")) && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 10))
                        {
                            if (field[i + 5, j].Value != null && field[i + 5, j].Value.ToString() == "W" && field[i - 1, j].Value != null && field[i - 1, j].Value.ToString() == "W")
                            {
                                if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && (field[i + 3, j].Value == null || (field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() != "W" && field[i + 3, j].Value.ToString() != "B")) && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && (field[i + 2, j].Value == null || (field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() != "W" && field[i + 2, j].Value.ToString() != "B")) && field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j].Value != null && field[i + 4, j].Value.ToString() == "B" && field[i + 3, j].Value != null && field[i + 3, j].Value.ToString() == "B" && field[i + 2, j].Value != null && field[i + 2, j].Value.ToString() == "B" && (field[i + 1, j].Value == null || (field[i + 1, j].Value != null && field[i + 1, j].Value.ToString() != "W" && field[i + 1, j].Value.ToString() != "B")))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 4) && (j < 14))
                        {
                            if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "W" && field[i, j - 5].Value != null && field[i, j - 5].Value.ToString() == "W")
                            {
                                if ((field[i, j - 1].Value == null || (field[i, j- 1].Value != null && field[i, j - 1].Value.ToString() != "W" && field[i, j - 1].Value.ToString() != "B")) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && (field[i, j - 2].Value == null || (field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() != "W" && field[i, j - 2].Value.ToString() != "B")) && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && (field[i, j - 3].Value == null || (field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() != "W" && field[i, j - 3].Value.ToString() != "B")) && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 3) && (j < 13))
                        {
                            if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "W" && field[i, j - 4].Value != null && field[i, j - 4].Value.ToString() == "W")
                            {
                                if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && (field[i, j - 1].Value == null || (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() != "W" && field[i, j - 1].Value.ToString() != "B")) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && (field[i, j - 2].Value == null || (field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() != "W" && field[i, j - 2].Value.ToString() != "B")) && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 2) && (j < 12))
                        {
                            if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "W" && field[i, j - 3].Value != null && field[i, j - 3].Value.ToString() == "W")
                            {
                                if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && (field[i, j + 1].Value == null || (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() != "W" && field[i, j + 1].Value.ToString() != "B")) && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && (field[i, j - 1].Value == null || (field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() != "W" && field[i, j - 1].Value.ToString() != "B")) && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 1) && (j < 11))
                        {
                            if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "W" && field[i, j - 2].Value != null && field[i, j - 2].Value.ToString() == "W")
                            {
                                if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && (field[i, j + 2].Value == null || (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() != "W" && field[i, j + 2].Value.ToString() != "B")) && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && (field[i, j + 1].Value == null || (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() != "W" && field[i, j + 1].Value.ToString() != "B")) && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 0) && (j < 10))
                        {
                            if (field[i, j + 5].Value != null && field[i, j + 5].Value.ToString() == "W" && field[i, j - 1].Value != null && field[i, j - 1].Value.ToString() == "W")
                            {
                                if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && (field[i, j + 3].Value == null || (field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() != "W" && field[i, j + 3].Value.ToString() != "B")) && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && (field[i, j + 2].Value == null || (field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() != "W" && field[i, j + 2].Value.ToString() != "B")) && field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i, j + 4].Value != null && field[i, j + 4].Value.ToString() == "B" && field[i, j + 3].Value != null && field[i, j + 3].Value.ToString() == "B" && field[i, j + 2].Value != null && field[i, j + 2].Value.ToString() == "B" && (field[i, j + 1].Value == null || (field[i, j + 1].Value != null && field[i, j + 1].Value.ToString() != "W" && field[i, j + 1].Value.ToString() != "B")))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i < 14) && (i > 4) && (j > 0) && (j < 10))
                        {
                            if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "W" && field[i - 5, j + 5].Value != null && field[i - 5, j + 5].Value.ToString() == "W")
                            {
                                if ((field[i - 1, j + 1].Value == null || (field[i - 1, j + 1].Value != null && field[i-1, j + 1].Value.ToString() != "W" && field[i-1, j + 1].Value.ToString() != "B")) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && (field[i - 2, j + 2].Value == null || (field[i-2, j +2].Value != null && field[i-2, j +2].Value.ToString() != "W" && field[i-2, j +2].Value.ToString() != "B")) && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && (field[i - 3, j + 3].Value == null || (field[i - 3, j + 3].Value != null && field[i-3, j +3].Value.ToString() != "W" && field[i-3, j +3].Value.ToString() != "B")) && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }

                            }
                        }
                        if ((i < 13) && (i > 3) && (j > 1) && (j < 11))
                        {
                            if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "W" && field[i - 4, j + 4].Value != null && field[i - 4, j + 4].Value.ToString() == "W")
                            {
                                if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && (field[i - 1, j + 1].Value == null || (field[i-1, j +1].Value != null && field[i-1, j +1].Value.ToString() != "W" && field[i-1, j +1].Value.ToString() != "B")) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && (field[i - 2, j + 2].Value == null || (field[i-2, j +2].Value != null && field[i-2, j +2].Value.ToString() != "W" && field[i-2, j +2].Value.ToString() != "B")) && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12) && (j > 2) && (j < 12))
                        {
                            if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "W" && field[i - 3, j + 3].Value != null && field[i - 3, j + 3].Value.ToString() == "W")
                            {
                                if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && (field[i + 1, j - 1].Value == null || (field[i+1, j - 1].Value != null && field[i+1, j - 1].Value.ToString() != "W" && field[i+1, j - 1].Value.ToString() != "B")) && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && (field[i - 1, j + 1].Value == null || (field[i-1, j + 1].Value != null && field[i-1, j + 1].Value.ToString() != "W" && field[i-1, j + 1].Value.ToString() != "B")) && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11) && (j > 3) && (j < 13))
                        {
                            if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "W" && field[i - 2, j + 2].Value != null && field[i - 2, j + 2].Value.ToString() == "W")
                            {
                                if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && (field[i + 2, j - 2].Value == null || (field[i+2, j - 2].Value != null && field[i+2, j - 2].Value.ToString() != "W" && field[i+2, j - 2].Value.ToString() != "B")) && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && (field[i + 1, j - 1].Value == null || (field[i+1, j - 1].Value != null && field[i+1, j - 1].Value.ToString() != "W" && field[i+1, j - 1].Value.ToString() != "B")) && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 10) && (j > 4) && (j < 14))
                        {
                            if (field[i + 5, j - 5].Value != null && field[i + 5, j - 5].Value.ToString() == "W" && field[i - 1, j + 1].Value != null && field[i - 1, j + 1].Value.ToString() == "W")
                            {
                                if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && (field[i + 3, j - 3].Value == null || (field[i+3, j - 3].Value != null && field[i+3, j - 3].Value.ToString() != "W" && field[i+3, j - 3].Value.ToString() != "B")) && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && (field[i + 2, j - 2].Value == null || (field[i+2, j - 2].Value != null && field[i+2, j - 2].Value.ToString() != "W" && field[i+2, j - 2].Value.ToString() != "B")) && field[i + 1, j - 1].Value != null && field[i + 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 4, j - 4].Value != null && field[i + 4, j - 4].Value.ToString() == "B" && field[i + 3, j - 3].Value != null && field[i + 3, j - 3].Value.ToString() == "B" && field[i + 2, j - 2].Value != null && field[i + 2, j - 2].Value.ToString() == "B" && (field[i + 1, j - 1].Value == null || (field[i+1, j - 1].Value != null && field[i+1, j - 1].Value.ToString() != "W" && field[i+1, j - 1].Value.ToString() != "B")))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 0) && (i < 10) && (j > 0) && (j < 10))
                        {
                            if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "W" && field[i + 5, j + 5].Value != null && field[i + 5, j + 5].Value.ToString() == "W")
                            {
                                if ((field[i + 1, j + 1].Value == null || (field[i+1, j + 1].Value != null && field[i+1, j + 1].Value.ToString() != "W" && field[i+1, j + 1].Value.ToString() != "B")) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && (field[i + 2, j + 2].Value == null || (field[i+2, j +2].Value != null && field[i+2, j +2].Value.ToString() != "W" && field[i+2, j +2].Value.ToString() != "B")) && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && (field[i + 3, j + 3].Value == null || (field[i+3, j +3].Value != null && field[i+3, j +3].Value.ToString() != "W" && field[i+3, j +3].Value.ToString() != "B")) && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 11) && (j > 1) && (j < 11))
                        {
                            if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "W" && field[i + 4, j + 4].Value != null && field[i + 4, j + 4].Value.ToString() == "W")
                            {
                                if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && (field[i + 1, j + 1].Value == null || (field[i+1, j +1].Value != null && field[i+1, j +1].Value.ToString() != "W" && field[i+1, j +1].Value.ToString() != "B")) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && (field[i + 2, j + 2].Value == null || (field[i+2, j +2].Value != null && field[i+2, j +2].Value.ToString() != "W" && field[i+2, j +2].Value.ToString() != "B")) && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 12) && (j > 2) && (j < 12))
                        {
                            if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "W" && field[i + 3, j + 3].Value != null && field[i + 3, j + 3].Value.ToString() == "W")
                            {
                                if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && (field[i - 1, j - 1].Value == null || (field[i-1, j - 1].Value != null && field[i-1, j - 1].Value.ToString() != "W" && field[i-1, j - 1].Value.ToString() != "B")) && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && (field[i + 1, j + 1].Value == null || (field[i+1, j + 1].Value != null && field[i+1, j + 1].Value.ToString() != "W" && field[i+1, j + 1].Value.ToString() != "B")) && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 3) && (i < 13) && (j > 3) && (j < 13))
                        {
                            if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "W" && field[i + 2, j + 2].Value != null && field[i + 2, j + 2].Value.ToString() == "W")
                            {
                                if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && (field[i - 2, j - 2].Value == null || (field[i-2, j - 2].Value != null && field[i-2, j - 2].Value.ToString() != "W" && field[i-2, j - 2].Value.ToString() != "B")) && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && (field[i - 1, j - 1].Value == null || (field[i-1, j - 1].Value != null && field[i-1, j - 1].Value.ToString() != "W" && field[i-1, j - 1].Value.ToString() != "B")) && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 4) && (i < 14) && (j > 4) && (j < 14))
                        {
                            if (field[i - 5, j - 5].Value != null && field[i - 5, j - 5].Value.ToString() == "W" && field[i + 1, j + 1].Value != null && field[i + 1, j + 1].Value.ToString() == "W")
                            {
                                if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && (field[i - 3, j - 3].Value == null || (field[i-3, j - 3].Value != null && field[i-3, j - 3].Value.ToString() != "W" && field[i-3, j - 3].Value.ToString() != "B")) && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && (field[i - 2, j - 2].Value == null || (field[i-2, j - 2].Value != null && field[i-2, j - 2].Value.ToString() != "W" && field[i-2, j - 2].Value.ToString() != "B")) && field[i - 1, j - 1].Value != null && field[i - 1, j - 1].Value.ToString() == "B")
                                {
                                    check = 1;
                                }
                                else if (field[i - 4, j - 4].Value != null && field[i - 4, j - 4].Value.ToString() == "B" && field[i - 3, j - 3].Value != null && field[i - 3, j - 3].Value.ToString() == "B" && field[i - 2, j - 2].Value != null && field[i - 2, j - 2].Value.ToString() == "B" && (field[i - 1, j - 1].Value == null || (field[i-1, j - 1].Value != null && field[i-1, j - 1].Value.ToString() != "W" && field[i-1, j - 1].Value.ToString() != "B")))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if (check == 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 31);
                            }
                        }
                    }
                }
            }
            #endregion

            // (13) 한쪽에 상대편의 방어가 있는 사목 - 9점
            makeBlack();
            #region 방어 있는 사목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (isEmpty(i, j))
                    {
                        int check = 0;
                        //위아래
                        if ((i > 3) && (i < 14))
                        {
                            if (isBlack(i - 1, j) && isBlack(i - 2, j) && isBlack(i - 3, j))
                            {
                                if (isWhite(i + 1, j) && isEmpty(i - 4, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j) && isWhite(i - 4, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 13))
                        {
                            if (isBlack(i + 1, j) && isBlack(i - 1, j) && isBlack(i - 2, j))
                            {
                                if (isWhite(i + 2, j) && isEmpty(i - 3, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j) && isWhite(i - 3, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 12))
                        {
                            if (isBlack(i + 2, j) && isBlack(i + 1, j) && isBlack(i - 1, j))
                            {
                                if (isWhite(i + 3, j) && isEmpty(i - 2, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 3, j) && isWhite(i - 2, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 11))
                        {
                            if (isBlack(i + 1, j) && isBlack(i + 2, j) && isBlack(i + 3, j))
                            {
                                if (isWhite(i + 4, j) && isEmpty(i - 1, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 4, j) && isWhite(i - 1, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 14))
                        {
                            if (isBlack(i, j - 1) && isBlack(i, j - 2) && isBlack(i, j - 3))
                            {
                                if (isWhite(i, j + 1) && isEmpty(i, j - 4))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 1) && isWhite(i, j - 4))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 2) && (j < 13))
                        {
                            if (isBlack(i, j + 1) && isBlack(i, j - 1) && isBlack(i, j - 2))
                            {
                                if (isWhite(i, j + 2) && isEmpty(i, j - 3))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 2) && isWhite(i, j - 3))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 1) && (j < 12))
                        {
                            if (isBlack(i, j + 2) && isBlack(i, j + 1) && isBlack(i, j - 1))
                            {
                                if (isWhite(i, j + 3) && isEmpty(i, j - 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 3) && isWhite(i, j - 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 0) && (j < 11))
                        {
                            if (isBlack(i, j + 1) && isBlack(i, j + 2) && isBlack(i, j + 3))
                            {
                                if (isWhite(i, j + 4) && isEmpty(i, j - 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 4) && isWhite(i, j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i < 14) && (j < 11) && (j > 0))
                        {
                            if (isBlack(i - 1, j + 1) && isBlack(i - 2, j + 2) && isBlack(i - 3, j + 3))
                            {
                                if (isWhite(i + 1, j - 1) && isEmpty(i - 4, j + 4))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j - 1) && isWhite(i - 4, j + 4))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 13) && (j < 12) && (j > 1))
                        {
                            if (isBlack(i - 1, j + 1) && isBlack(i - 2, j + 2) && isBlack(i + 1, j - 1))
                            {
                                if (isWhite(i + 2, j - 2) && isEmpty(i - 3, j + 3))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j - 2) && isWhite(i - 3, j + 3))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 12) && (j < 13) && (j > 2))
                        {
                            if (isBlack(i - 1, j + 1) && isBlack(i + 1, j - 1) && isBlack(i + 2, j - 2))
                            {
                                if (isBlack(i + 3, j - 3) && isEmpty(i - 2, j + 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 3, j - 3) && isWhite(i - 2, j + 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 11) && (j < 14) && (j > 3))
                        {
                            if (isBlack(i + 1, j - 1) && isBlack(i + 2, j - 2) && isBlack(i + 3, j - 3))
                            {
                                if (isWhite(i + 4, j - 4) && isEmpty(i - 1, j + 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 4, j - 4) && isWhite(i - 1, j + 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 14) && (j > 3) && (j < 14))
                        {
                            if (isBlack(i - 1, j - 1) && isBlack(i - 2, j - 2) && isBlack(i - 3, j - 3))
                            {
                                if (isWhite(i + 1, j + 1) && isEmpty(i - 4, j - 4))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j + 1) && isWhite(i - 4, j - 4))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 2) && (i < 13) && (j > 2) && (j < 13))
                        {
                            if (isBlack(i - 1, j - 1) && isBlack(i - 2, j - 2) && isBlack(i + 1, j + 1))
                            {
                                if (isWhite(i + 2, j + 2) && isEmpty(i - 3, j - 3))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j + 2) && isWhite(i - 3, j - 3))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 12) && (j > 1) && (j < 12))
                        {
                            if (isBlack(i - 1, j - 1) && isBlack(i + 1, j + 1) && isBlack(i + 2, j + 2))
                            {
                                if (isWhite(i + 3, j + 3) && isEmpty(i - 2, j - 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 3, j + 3) && isWhite(i - 2, j - 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 11) && (j > 0) && (j < 11))
                        {
                            if (isBlack(i + 1, j + 1) && isBlack(i + 2, j + 2) && isBlack(i + 3, j + 3))
                            {
                                if (isWhite(i + 4, j + 4) && isEmpty(i - 1, j - 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 4, j + 4) && isWhite(i - 1, j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if (check == 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 9);
                            }
                        }
                    }
                }
            }
            #endregion

            // (5) 상대편의 방어가 없는 사목 - 97점
            makeBlack();
            #region 방어 없는 사목
            while (true)
            {
                found = false;

                // 돌 3개 이어진거 찾으면 끝.
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 97);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 97);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 97);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 97);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 97);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 97);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 97);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 97);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 97);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion

            // (10) 상대편의 방어가 없는 이삼 - 21점
            makeBlack();
            #region 방어 없는 이삼
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (isEmpty(i, j))
                    {
                        int x=0, y = 0;
                        /********************3개되는 줄의 유무***********/
                        //위아래
                        if ((i > 2) && (i < 14))
                        {
                            if (isEmpty(i + 1, j) && isBlack(i - 1, j) && isBlack(i - 2, j) && isEmpty(i - 3, j))
                            {
                                x = 1;
                            }
                        }
                        if ((i > 1) && (i < 13))
                        {
                            if (isEmpty(i + 2, j) && isBlack(i + 1, j) && isBlack(i - 1, j) && isEmpty(i - 2, j))
                            {
                                x = 1;
                            }
                        }
                        if ((i > 0) && (i < 12))
                        {
                            if (isEmpty(i + 3, j) && isBlack(i + 2, j) && isBlack(i + 1, j) && isEmpty(i - 1, j))
                            {
                                x = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 2) && (j < 14))
                        {
                            if (isEmpty(i, j + 1) && isBlack(i, j - 1) && isBlack(i, j - 2) && isEmpty(i, j - 3))
                            {
                                x = 1;
                            }
                        }
                        if ((j > 1) && (j < 13))
                        {
                            if (isEmpty(i, j + 2) && isBlack(i, j + 1) && isBlack(i, j - 1) && isEmpty(i, j - 2))
                            {
                                x = 1;
                            }
                        }
                        if ((j > 0) && (j < 12))
                        {
                            if (isEmpty(i, j + 3) && isBlack(i, j + 2) && isBlack(i, j + 1) && isEmpty(i, j - 1))
                            {
                                x = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 2) && (i < 14) && (j < 12) && (j > 0))
                        {
                            if (isEmpty(i + 1, j - 1) == null && isBlack(i - 1, j + 1) && isBlack(i - 2, j + 2) && isEmpty(i - 3, j + 3))
                            {
                                x = 1;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j < 13) && (j > 1))
                        {
                            if (isBlack(i - 1, j + 1) && isEmpty(i - 2, j + 2) && isBlack(i + 1, j - 1) && isEmpty(i + 2, j - 2))
                            {
                                x = 1;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j < 14) && (j > 2))
                        {
                            if (isEmpty(i - 1, j + 1) && isBlack(i + 1, j - 1) && isBlack(i + 2, j - 2) && isEmpty(i + 3, j - 3))
                            {
                                x = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 2) && (i < 14) && (j > 2) && (j < 14))
                        {
                            if (isEmpty(i + 1, j + 1) && isBlack(i - 1, j - 1) && isBlack(i - 2, j - 2) && isEmpty(i - 3, j - 3))
                            {
                                x = 1;
                            }
                        }
                        if ((i > 1) && (i < 13) && (j > 1) && (j < 13))
                        {
                            if (isEmpty(i - 2, j - 2) && isBlack(i - 1, j - 1) && isBlack(i + 1, j + 1) && isEmpty(i + 2, j + 2))
                            {
                                x = 1;
                            }
                        }
                        if ((i > 0) && (i < 12) && (j > 0) && (j < 12))
                        {
                            if (isEmpty(i - 1, j - 1) && isBlack(i + 1, j + 1) && isBlack(i + 2, j + 2) && isEmpty(i + 3, j + 3))
                            {
                                x = 1;
                            }
                        }
                        /*******************************************************/
                        /********************2개되는 줄의 유무***********/
                        //위아래
                        if ((i > 1) && (i < 14))
                        {
                            if (isEmpty(i + 1, j) && isBlack(i - 1, j) && isEmpty(i - 2, j))
                            {
                                y = 1;
                            }
                        }
                        if ((i > 0) && (i < 13))
                        {
                            if (isEmpty(i + 2, j) && isBlack(i + 1, j) && isEmpty(i - 1, j))
                            {
                                y = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 1) && (j < 14))
                        {
                            if (isEmpty(i, j + 1) && isBlack(i, j - 1) && isEmpty(i, j - 2))
                            {
                                y = 1;
                            }
                        }
                        if ((j > 0) && (j < 13))
                        {
                            if (isEmpty(i, j + 2) && isBlack(i, j + 1) && isEmpty(i, j - 1))
                            {
                                y = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 1) && (i < 14) && (j < 13) && (j > 0))
                        {
                            if (isEmpty(i + 1, j - 1) && isBlack(i - 1, j + 1) && isEmpty(i - 2, j + 2))
                            {
                                y = 1;
                            }
                        }
                        if ((i > 0) && (i < 13) && (j < 14) && (j > 1))
                        {
                            if (isEmpty(i + 2, j - 2) && isBlack(i + 1, j - 1) && isEmpty(i - 1, j + 1))
                            {
                                y = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 1) && (i < 14) && (j > 1) && (j < 14))
                        {
                            if (isEmpty(i + 1, j + 1) && isBlack(i - 1, j - 1) && isEmpty(i - 2, j - 2))
                            {
                                y = 1;
                            }
                        }
                        if ((i > 0) && (i < 13) && (j > 0) && (j < 13))
                        {
                            if (isEmpty(i + 2, j + 2) && isBlack(i + 1, j + 1) && isEmpty(i - 1, j - 1))
                            {
                                y = 1;
                            }
                        }
                        /*******************************************************/
                        if ((x + y) > 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 21);
                            }
                        }
                    }
                }
            }
            #endregion

            // (11) 상대편의 방어가 없는 이이이 - 20점
            makeBlack();
            #region 방어 없는 이이이
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (isEmpty(i, j))
                    {
                        int count = 0;
                        /********************2개되는 줄의 유무***********/
                        //위아래
                        if ((i > 1) && (i < 14))
                        {
                            if (isEmpty(i + 1, j) && isBlack(i - 1, j) && isEmpty(i - 2, j))
                            {
                                count++;
                            }
                        }
                        if ((i > 0) && (i < 13))
                        {
                            if (isEmpty(i + 2, j) && isBlack(i + 1, j) && isEmpty(i - 1, j))
                            {
                                count++;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 1) && (j < 14))
                        {
                            if (isEmpty(i, j + 1) && isBlack(i, j - 1) && isEmpty(i, j - 2))
                            {
                                count++;
                            }
                        }
                        if ((j > 0) && (j < 13))
                        {
                            if (isEmpty(i, j + 2) && isBlack(i, j + 1) && isEmpty(i, j - 1))
                            {
                                count++;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 1) && (i < 14) && (j < 13) && (j > 0))
                        {
                            if (isEmpty(i + 1, j - 1) && isBlack(i - 1, j + 1) && isEmpty(i - 2, j + 2))
                            {
                                count++;
                            }
                        }
                        if ((i > 0) && (i < 13) && (j < 14) && (j > 1))
                        {
                            if (isEmpty(i + 2, j - 2) && isBlack(i + 1, j - 1) && isEmpty(i - 1, j + 1))
                            {
                                count++;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 1) && (i < 14) && (j > 1) && (j < 14))
                        {
                            if (isEmpty(i + 1, j + 1) && isBlack(i - 1, j - 1) && isEmpty(i - 2, j - 2))
                            {
                                count++;
                            }
                        }
                        if ((i > 0) && (i < 13) && (j > 0) && (j < 13))
                        {
                            if (isEmpty(i + 2, j + 2) && isBlack(i + 1, j + 1) && isEmpty(i - 1, j - 1))
                            {
                                count++;
                            }
                        }
                        /*******************************************************/
                        if (count > 2)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 20);
                            }
                        }
                    }
                }
            }
            #endregion

            // (12) 방어 없는 삼목 - 10점
            makeBlack();
            #region 방어 없는 삼목
            while (true)
            {
                found = false;

                // 돌 2개 이어진거 찾으면 끝.
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 10);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 10);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 10);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 10);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 10);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 10);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 10);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 10);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 10);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion
           
            // (15) 상대편의 방어가 없고 중간에 하나의 빈칸이 있는 삼목 - 7점
            makeBlack();
            #region 방어 없고 중간에 하나의 빈칸이 있는 삼목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (isEmpty(i, j))
                    {
                        int check = 0;
                        //위아래
                        if ((i > 3) && (i < 14))
                        {
                            if (isEmpty(i + 1, j) && isEmpty(i - 1, j) && isBlack(i - 2, j) && isBlack(i - 3, j) && isEmpty(i-4,j))
                            {
                                check = 1;
                            }
                            else if(isEmpty(i + 1, j) && isBlack(i - 1, j) && isEmpty(i - 2, j) && isBlack(i - 3, j) && isEmpty(i-4,j))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 2) && (i < 13))
                        {
                            if (isEmpty(i + 2, j) && isBlack(i + 1, j) && isEmpty(i - 1, j) && isBlack(i - 2, j) && isEmpty(i - 3, j))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 1) && (i < 12))
                        {
                            if (isEmpty(i + 3, j) && isBlack(i + 2, j) && isEmpty(i + 1, j) && isBlack(i - 1, j) && isEmpty(i - 2, j))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 0) && (i < 11))
                        {
                            if (isEmpty(i + 4, j) && isBlack(i + 3, j) && isEmpty(i + 2, j) && isBlack(i + 1, j) && isEmpty(i - 1, j))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j) && isBlack(i + 3, j) && isBlack(i + 2, j) && isEmpty(i + 1, j) && isEmpty(i - 1, j))
                            {
                                check = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 14))
                        {
                            if (isEmpty(i, j + 1) && isEmpty(i, j - 1) && isBlack(i, j - 2) && isBlack(i, j - 3) && isEmpty(i, j - 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i, j + 1) && isBlack(i, j - 1) && isEmpty(i, j - 2) && isBlack(i, j - 3) && isEmpty(i, j - 4))
                            {
                                check = 1;
                            }
                        }
                        if ((j > 2) && (j < 13))
                        {
                            if (isEmpty(i, j + 2) && isBlack(i, j + 1) && isEmpty(i, j - 1) && isBlack(i, j - 2) && isEmpty(i, j - 3))
                            {
                                check = 1;
                            }
                        }
                        if ((j > 1) && (j < 12))
                        {
                            if (isEmpty(i, j + 3) && isBlack(i, j + 2) && isEmpty(i, j + 1) && isBlack(i, j - 1) && isEmpty(i, j - 2))
                            {
                                check = 1;
                            }
                        }
                        if ((j > 0) && (j < 11))
                        {
                            if (isEmpty(i, j + 4) && isBlack(i, j + 3) && isEmpty(i, j + 2) && isBlack(i, j + 1) && isEmpty(i, j - 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i, j + 4) && isBlack(i, j + 3) && isBlack(i, j + 2) && isEmpty(i, j + 1) && isEmpty(i, j - 1))
                            {
                                check = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i < 14) && (j > 0) && (j < 11))
                        {
                            if (isEmpty(i + 1, j - 1) && isEmpty(i - 1, j + 1) && isBlack(i - 2, j + 2) && isBlack(i - 3, j + 3) && isEmpty(i - 4, j + 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 1, j -1) && isBlack(i - 1, j + 1) && isEmpty(i - 2, j + 2) && isBlack(i - 3, j + 3) && isEmpty(i - 4, j + 4))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 2) && (i < 13) && (j > 1) && (j < 12))
                        {
                            if (isEmpty(i + 2, j - 2) && isBlack(i + 1, j - 1) && isEmpty(i - 1, j + 1) && isBlack(i - 2, j + 2) && isEmpty(i - 3, j + 3))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 1) && (i < 12) && (j > 2) && (j < 13))
                        {
                            if (isEmpty(i + 3, j - 3) && isBlack(i + 2, j - 2) && isEmpty(i + 1, j - 1) && isBlack(i - 1, j + 1) && isEmpty(i - 2, j + 2))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 0) && (i < 11) && (j>3) && (j<14))
                        {
                            if (isEmpty(i + 4, j - 4) && isBlack(i + 3, j - 3) && isEmpty(i + 2, j - 2) && isBlack(i + 1, j - 1) && isEmpty(i - 1, j + 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j - 4) && isBlack(i + 3, j - 3) && isBlack(i + 2, j - 2) && isEmpty(i + 1, j - 1) && isEmpty(i - 1, j + 1))
                            {
                                check = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 14) && (j > 3) && (j < 14))
                        {
                            if (isEmpty(i + 1, j + 1) && isEmpty(i - 1, j - 1) && isBlack(i - 2, j - 2) && isBlack(i - 3, j - 3) && isEmpty(i - 4, j - 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 1, j + 1) && isBlack(i - 1, j - 1) && isEmpty(i - 2, j - 2) && isBlack(i - 3, j - 3) && isEmpty(i - 4, j - 4))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 2) && (i < 13) && (j > 2) && (j < 13))
                        {
                            if (isEmpty(i + 2, j + 2) && isBlack(i + 1, j + 1) && isEmpty(i - 1, j - 1) && isBlack(i - 2, j - 2) && isEmpty(i - 3, j - 3))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 1) && (i < 12) && (j > 1) && (j < 12))
                        {
                            if (isEmpty(i + 3, j + 3) && isBlack(i + 2, j + 2) && isEmpty(i + 1, j + 1) && isBlack(i - 1, j - 1) && isEmpty(i - 2, j - 2))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 0) && (i < 11) && (j > 0) && (j < 11))
                        {
                            if (isEmpty(i + 4, j + 4) && isBlack(i + 3, j + 3) && isEmpty(i + 2, j + 2) && isBlack(i + 1, j + 1) && isEmpty(i - 1, j - 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j + 4) && isBlack(i + 3, j + 3) && isBlack(i + 2, j + 2) && isEmpty(i + 1, j + 1) && isEmpty(i - 1, j - 1))
                            {
                                check = 1;
                            }
                        }
                        if(check == 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 7);
                            }
                        }
                    }
                }
            }
            #endregion


            // (16) 한쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 삼목 - 6점
            makeBlack();
            #region 방어 있고 중간에 하나의 빈칸이 있는 삼목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (isEmpty(i, j))
                    {
                        int check = 0;
                        //위아래
                        if ((i > 3) && (i < 14))
                        {
                            if (isWhite(i + 1, j) && isEmpty(i - 1, j) && isBlack(i - 2, j) && isBlack(i - 3, j) && isEmpty(i - 4, j))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 1, j) && isEmpty(i - 1, j) && isBlack(i - 2, j) && isBlack(i - 3, j) && isWhite(i - 4, j))
                            {
                                check = 1;
                            }
                            else if (isWhite(i + 1, j) && isBlack(i - 1, j) && isEmpty(i - 2, j) && isBlack(i - 3, j) && isEmpty(i - 4, j))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 1, j) && isBlack(i - 1, j) && isEmpty(i - 2, j) && isBlack(i - 3, j) && isWhite(i - 4, j))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 2) && (i < 13))
                        {
                            if (isWhite(i + 2, j) && isBlack(i + 1, j) && isEmpty(i - 1, j) && isBlack(i - 2, j) && isEmpty(i - 3, j))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 2, j) && isBlack(i + 1, j) && isEmpty(i - 1, j) && isBlack(i - 2, j) && isWhite(i - 3, j))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 1) && (i < 12))
                        {
                            if (isWhite(i + 3, j) && isBlack(i + 2, j) && isEmpty(i + 1, j) && isBlack(i - 1, j) && isEmpty(i - 2, j))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 3, j) && isBlack(i + 2, j) && isEmpty(i + 1, j) && isBlack(i - 1, j) && isWhite(i - 2, j))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 0) && (i < 11))
                        {
                            if (isWhite(i + 4, j) && isBlack(i + 3, j) && isEmpty(i + 2, j) && isBlack(i + 1, j) && isEmpty(i - 1, j))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j) && isBlack(i + 3, j) && isEmpty(i + 2, j) && isBlack(i + 1, j) && isWhite(i - 1, j))
                            {
                                check = 1;
                            }
                            else if (isWhite(i + 4, j) && isBlack(i + 3, j) && isBlack(i + 2, j) && isEmpty(i + 1, j) && isEmpty(i - 1, j))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j) && isBlack(i + 3, j) && isBlack(i + 2, j) && isEmpty(i + 1, j) && isWhite(i - 1, j))
                            {
                                check = 1;
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 3) && (j < 14))
                        {
                            if (isWhite(i, j + 1) && isEmpty(i, j - 1) && isBlack(i, j - 2) && isBlack(i, j - 3) && isEmpty(i, j - 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i, j + 1) && isEmpty(i, j - 1) && isBlack(i, j - 2) && isBlack(i, j - 3) && isWhite(i, j - 4))
                            {
                                check = 1;
                            }
                            else if (isWhite(i, j + 1) && isBlack(i, j - 1) && isEmpty(i, j - 2) && isBlack(i, j - 3) && isEmpty(i, j - 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i, j + 1) && isBlack(i, j - 1) && isEmpty(i, j - 2) && isBlack(i, j - 3) && isWhite(i, j - 4))
                            {
                                check = 1;
                            }
                        }
                        if ((j > 2) && (j < 13))
                        {
                            if (isWhite(i, j + 2) && isBlack(i, j + 1) && isEmpty(i, j - 1) && isBlack(i, j - 2) && isEmpty(i, j - 3))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i, j + 2) && isBlack(i, j + 1) && isEmpty(i, j - 1) && isBlack(i, j - 2) && isWhite(i, j - 3))
                            {
                                check = 1;
                            }
                        }
                        if ((j > 1) && (j < 12))
                        {
                            if (isWhite(i, j + 3) && isBlack(i, j + 2) && isEmpty(i, j + 1) && isBlack(i, j - 1) && isEmpty(i, j - 2))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i, j + 3) && isBlack(i, j + 2) && isEmpty(i, j + 1) && isBlack(i, j - 1) && isWhite(i, j - 2))
                            {
                                check = 1;
                            }
                        }
                        if ((j > 0) && (j < 11))
                        {
                            if (isWhite(i, j + 4) && isBlack(i, j + 3) && isEmpty(i, j + 2) && isBlack(i, j + 1) && isEmpty(i, j - 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i, j + 4) && isBlack(i, j + 3) && isEmpty(i, j + 2) && isBlack(i, j + 1) && isWhite(i, j - 1))
                            {
                                check = 1;
                            }
                            else if (isWhite(i, j + 4) && isBlack(i, j + 3) && isBlack(i, j + 2) && isEmpty(i, j + 1) && isEmpty(i, j - 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i, j + 4) && isBlack(i, j + 3) && isBlack(i, j + 2) && isEmpty(i, j + 1) && isWhite(i, j - 1))
                            {
                                check = 1;
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 3) && (i < 14) && (j > 0) && (j < 11))
                        {
                            if (isWhite(i + 1, j - 1) && isEmpty(i - 1, j + 1) && isBlack(i - 2, j + 2) && isBlack(i - 3, j + 3) && isEmpty(i - 4, j + 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 1, j - 1) && isEmpty(i - 1, j + 1) && isBlack(i - 2, j + 2) && isBlack(i - 3, j + 3) && isWhite(i - 4, j + 4))
                            {
                                check = 1;
                            }
                            else if (isWhite(i + 1, j - 1) && isBlack(i - 1, j + 1) && isEmpty(i - 2, j + 2) && isBlack(i - 3, j + 3) && isEmpty(i - 4, j + 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 1, j - 1) && isBlack(i - 1, j + 1) && isEmpty(i - 2, j + 2) && isBlack(i - 3, j + 3) && isWhite(i - 4, j + 4))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 2) && (i < 13) && (j > 1) && (j < 12))
                        {
                            if (isWhite(i + 2, j - 2) && isBlack(i + 1, j - 1) && isEmpty(i - 1, j + 1) && isBlack(i - 2, j + 2) && isEmpty(i - 3, j + 3))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 2, j - 2) && isBlack(i + 1, j - 1) && isEmpty(i - 1, j + 1) && isBlack(i - 2, j + 2) && isWhite(i - 3, j + 3))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 1) && (i < 12) && (j > 2) && (j < 13))
                        {
                            if (isWhite(i + 3, j - 3) && isBlack(i + 2, j - 2) && isEmpty(i + 1, j - 1) && isBlack(i - 1, j + 1) && isEmpty(i - 2, j + 2))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 3, j - 3) && isBlack(i + 2, j - 2) && isEmpty(i + 1, j - 1) && isBlack(i - 1, j + 1) && isWhite(i - 2, j + 2))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 0) && (i < 11) && (j > 3) && (j < 14))
                        {
                            if (isWhite(i + 4, j - 4) && isBlack(i + 3, j - 3) && isEmpty(i + 2, j - 2) && isBlack(i + 1, j - 1) && isEmpty(i - 1, j + 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j - 4) && isBlack(i + 3, j - 3) && isEmpty(i + 2, j - 2) && isBlack(i + 1, j - 1) && isWhite(i - 1, j + 1))
                            {
                                check = 1;
                            }
                            else if (isWhite(i + 4, j - 4) && isBlack(i + 3, j - 3) && isBlack(i + 2, j - 2) && isEmpty(i + 1, j - 1) && isEmpty(i - 1, j + 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j - 4) && isBlack(i + 3, j - 3) && isBlack(i + 2, j - 2) && isEmpty(i + 1, j - 1) && isWhite(i - 1, j + 1))
                            {
                                check = 1;
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 3) && (i < 14) && (j > 3) && (j < 14))
                        {
                            if (isWhite(i + 1, j + 1) && isEmpty(i - 1, j - 1) && isBlack(i - 2, j - 2) && isBlack(i - 3, j - 3) && isEmpty(i - 4, j - 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 1, j + 1) && isEmpty(i - 1, j - 1) && isBlack(i - 2, j - 2) && isBlack(i - 3, j - 3) && isWhite(i - 4, j - 4))
                            {
                                check = 1;
                            }
                            else if (isWhite(i + 1, j + 1) && isBlack(i - 1, j - 1) && isEmpty(i - 2, j - 2) && isBlack(i - 3, j - 3) && isEmpty(i - 4, j - 4))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 1, j + 1) && isBlack(i - 1, j - 1) && isEmpty(i - 2, j - 2) && isBlack(i - 3, j - 3) && isWhite(i - 4, j - 4))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 2) && (i < 13) && (j > 2) && (j < 13))
                        {
                            if (isWhite(i + 2, j + 2) && isBlack(i + 1, j + 1) && isEmpty(i - 1, j - 1) && isBlack(i - 2, j - 2) && isEmpty(i - 3, j - 3))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 2, j + 2) && isBlack(i + 1, j + 1) && isEmpty(i - 1, j - 1) && isBlack(i - 2, j - 2) && isWhite(i - 3, j - 3))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 1) && (i < 12) && (j > 1) && (j < 12))
                        {
                            if (isWhite(i + 3, j + 3) && isBlack(i + 2, j + 2) && isEmpty(i + 1, j + 1) && isBlack(i - 1, j - 1) && isEmpty(i - 2, j - 2))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 3, j + 3) && isBlack(i + 2, j + 2) && isEmpty(i + 1, j + 1) && isBlack(i - 1, j - 1) && isWhite(i - 2, j - 2))
                            {
                                check = 1;
                            }
                        }
                        if ((i > 0) && (i < 11) && (j > 0) && (j < 11))
                        {
                            if (isWhite(i + 4, j + 4) && isBlack(i + 3, j + 3) && isEmpty(i + 2, j + 2) && isBlack(i + 1, j + 1) && isEmpty(i - 1, j - 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j + 4) && isBlack(i + 3, j + 3) && isEmpty(i + 2, j + 2) && isBlack(i + 1, j + 1) && isWhite(i - 1, j - 1))
                            {
                                check = 1;
                            }
                            else if (isWhite(i + 4, j + 4) && isBlack(i + 3, j + 3) && isBlack(i + 2, j + 2) && isEmpty(i + 1, j + 1) && isEmpty(i - 1, j - 1))
                            {
                                check = 1;
                            }
                            else if (isEmpty(i + 4, j + 4) && isBlack(i + 3, j + 3) && isBlack(i + 2, j + 2) && isEmpty(i + 1, j + 1) && isWhite(i - 1, j - 1))
                            {
                                check = 1;
                            }
                        }
                        if (check == 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 7);
                            }
                        }
                    }
                }
            }
            #endregion
            // (19) 한쪽에 상대편의 방어가 있는 삼목 - 3점

            makeBlack();
            #region 방어 있는 삼목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (isEmpty(i, j))
                    {
                        int check = 0;
                        //위아래
                        if ((i > 2) && (i < 15))
                        {
                            if (isBlack(i - 1, j) && isBlack(i - 2, j))
                            {
                                if (isWhite(i + 1, j) && isEmpty(i - 3, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j) && isWhite(i - 3, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 14))
                        {
                            if (isBlack(i + 1, j) && isBlack(i - 1, j))
                            {
                                if (isWhite(i + 2, j) && isEmpty(i - 2, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j) && isWhite(i - 2, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 13))
                        {
                            if (isBlack(i + 2, j) && isBlack(i + 1, j))
                            {
                                if (isWhite(i + 3, j) && isEmpty(i - 1, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 3, j) && isWhite(i - 1, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 2) && (j < 15))
                        {
                            if (isBlack(i, j - 1) && isBlack(i, j - 2))
                            {
                                if (isWhite(i, j + 1) && isEmpty(i, j - 3))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 1) && isWhite(i, j - 3))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 1) && (j < 14))
                        {
                            if (isBlack(i, j + 1) && isBlack(i, j - 1))
                            {
                                if (isWhite(i, j + 2) && isEmpty(i, j - 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 2) && isWhite(i, j - 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 0) && (j < 13))
                        {
                            if (isBlack(i, j + 2) && isBlack(i, j + 1))
                            {
                                if (isWhite(i, j + 3) && isEmpty(i, j - 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 3) && isWhite(i, j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 2) && (i < 15) && (j < 13) && (j > 0))
                        {
                            if (isBlack(i - 1, j + 1) && isBlack(i - 2, j + 2))
                            {
                                if (isWhite(i + 1, j - 1) && isEmpty(i - 3, j + 3))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j - 1) && isWhite(i - 3, j + 3))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 14) && (j < 14) && (j > 1))
                        {
                            if (isBlack(i + 1, j - 1) && isBlack(i - 1, j + 1))
                            {
                                if (isWhite(i + 2, j - 2) && isEmpty(i - 2, j + 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j - 2) && isWhite(i - 2, j + 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 13) && (j < 15) && (j > 2))
                        {
                            if (isBlack(i + 2, j - 2) && isBlack(i + 1, j - 1))
                            {
                                if (isWhite(i + 3, j - 3) && isEmpty(i - 1, j + 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 3, j - 3) && isWhite(i - 1, j + 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 2) && (i < 15) && (j > 2) && (j < 15))
                        {
                            if (isBlack(i - 1, j - 1) && isBlack(i - 2, j - 2))
                            {
                                if (isWhite(i + 1, j + 1) && isEmpty(i - 3, j - 3))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j + 1) && isWhite(i - 3, j - 3))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 1) && (i < 14) && (j > 1) && (j < 14))
                        {
                            if (isBlack(i + 1, j + 1) && isBlack(i - 1, j - 1))
                            {
                                if (isWhite(i + 2, j + 2) && isEmpty(i - 2, j - 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j + 2) && isWhite(i - 2, j - 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 13) && (j > 0) && (j < 13))
                        {
                            if (isBlack(i + 1, j + 1) && isBlack(i + 2, j + 2))
                            {
                                if (isWhite(i + 3, j + 3) && isEmpty(i - 1, j - 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 3, j + 3) && isWhite(i - 1, j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if (check == 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 3);
                            }
                        }
                    }
                }
            }
            #endregion
            
            // (17) 상대편의 방어가 없는 이이 - 5점
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    black[i, j] = 1;
                }
            }
            #region 방어 없는 이이
            while (true)
            {
                found = false;
                piece = "B";

                // 모든 셀에 대하여 8방향 탐색 후 1줄짜리가 2개 이상이면 해당 셀은 이이 가치를 가진다.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (black[i, j] == 1)
                        {
                            black[i, j] = 0;
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
                totCnt = 0;
                i = 1;
                ret = null;
                // (1) 오른쪽으로 탐색
                if (row + 1 < 15)
                {
                    ret = adventureField(row + i, col);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }
                // (2) 오른쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col + 1 < 15)
                {
                    ret = adventureField(row + i, col + i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (3) 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col + 1 < 15)
                {
                    ret = adventureField(row, col + i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (4) 왼쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col + 1 < 15)
                {
                    ret = adventureField(row - i, col + i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (5) 왼쪽으로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0)
                {
                    ret = adventureField(row - i, col);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (6) 왼쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    ret = adventureField(row - i, col - i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (7) 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col - 1 >= 0)
                {
                    ret = adventureField(row, col - i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (8) 오른쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col - 1 >= 0)
                {
                    ret = adventureField(row + i, col - i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }
                if (totCnt > 1)
                {
                    if (adv[row, col] == 0)
                    {
                        adv[row, col] = 1;
                        showValue(row, col, 5);
                    }
                }
            }
            #endregion

            // (18) 상대편의 방어가 없는 삼목(12랑 겹침) - 4점
            makeBlack();
            #region 방어 없는 삼목
            while (true)
            {
                found = false;

                // 돌 2개 이어진거 찾으면 끝.
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 4);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 4);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 4);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 4);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 4);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 4);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 4);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 4);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 4);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion

            // (21) 한쪽에 상대편의 방어가 있는 이목 - 1점
            makeBlack();
            #region 방어 있는 이목
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (isEmpty(i, j))
                    {
                        int check = 0;
                        /********************2개되는 줄의 유무***********/
                        //위아래
                        if ((i > 1) && (i < 14))
                        {
                            if (isBlack(i - 1, j))
                            {
                                if (isWhite(i + 1, j) && isEmpty(i - 2, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j) && isWhite(i - 2, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 13))
                        {
                            if (isBlack(i + 1, j))
                            {
                                if (isWhite(i + 2, j) && isEmpty(i - 1, j))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j) && isWhite(i - 1, j))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //왼쪽오른쪽
                        if ((j > 1) && (j < 14))
                        {
                            if (isBlack(i, j - 1))
                            {
                                if (isWhite(i, j + 1) && isEmpty(i, j - 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 1) && isWhite(i, j - 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((j > 0) && (j < 13))
                        {
                            if (isBlack(i, j + 1))
                            {
                                if (isWhite(i, j + 2) && isEmpty(i, j - 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i, j + 2) && isWhite(i, j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위오른쪽방향대각선
                        if ((i > 1) && (i < 14) && (j < 13) && (j > 0))
                        {
                            if (isBlack(i - 1, j + 1))
                            {
                                if (isWhite(i + 1, j - 1) && isEmpty(i - 2, j + 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j - 1) && isWhite(i - 2, j + 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 13) && (j < 14) && (j > 1))
                        {
                            if (isBlack(i + 1, j - 1))
                            {
                                if (isWhite(i + 2, j - 2) && isEmpty(i - 1, j + 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j - 2) && isWhite(i - 1, j + 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        //위왼쪽방향대각선
                        if ((i > 1) && (i < 14) && (j > 1) && (j < 14))
                        {
                            if (isBlack(i - 1, j - 1))
                            {
                                if (isWhite(i + 1, j + 1) && isEmpty(i - 2, j - 2))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 1, j + 1) && isWhite(i - 2, j - 2))
                                {
                                    check = 1;
                                }
                            }
                        }
                        if ((i > 0) && (i < 13) && (j > 0) && (j < 13))
                        {
                            if (isBlack(i + 1, j + 1))
                            {
                                if (isWhite(i + 2, j + 2) && isEmpty(i - 1, j - 1))
                                {
                                    check = 1;
                                }
                                else if (isEmpty(i + 2, j + 2) && isWhite(i - 1, j - 1))
                                {
                                    check = 1;
                                }
                            }
                        }
                        /*******************************************************/
                        if (check == 1)
                        {
                            if (adv[i, j] == 0)
                            {
                                adv[i, j] = 1;
                                showValue(i, j, 1);
                            }
                        }
                    }
                }
            }
            #endregion
            
            // (20) 상대편의 방어가 없는 이목 - 2점
            makeBlack();
            #region 방어 없는 이목
            while (true)
            {
                found = false;

                // 돌 1개 이어진거 찾으면 끝.
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 2);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 2);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 2);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 2);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 2);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 2);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 2);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 2);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 2);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion
            
            return -1;
        }

        public int valueFunctionForWhite()
        {
            string piece = null;
            int row = 0, col = 0;
            bool found = false;

            int cnt = 0, totCnt = 0;
            int i = 1;
            string ret = null;

            initAdv();

            // (1) 오목 - 100점
            makeWhite();
            #region 오목
            while (true)
            {
                found = false;

                // 돌 4개 이어진거 찾으면 끝.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (white[i, j] == 1)
                        {
                            white[i, j] = 0;
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
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    white[i, j] = 1;
                }
            }
            #region 사사
            while (true)
            {
                found = false;
                piece = "W";

                // 모든 셀에 대하여 8방향 탐색 후 3줄짜리가 2개 이상이면 해당 셀은 사사 가치를 가진다.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (white[i, j] == 1)
                        {
                            white[i, j] = 0;
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
                totCnt = 0;
                i = 1;
                ret = null;
                // (1) 오른쪽으로 탐색
                if (row + 1 < 15)
                {
                    ret = adventureField(row + i, col);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }
                // (2) 오른쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col + 1 < 15)
                {
                    ret = adventureField(row + i, col + i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (3) 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col + 1 < 15)
                {
                    ret = adventureField(row, col + i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (4) 왼쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col + 1 < 15)
                {
                    ret = adventureField(row - i, col + i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (5) 왼쪽으로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0)
                {
                    ret = adventureField(row - i, col);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (6) 왼쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    ret = adventureField(row - i, col - i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (7) 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col - 1 >= 0)
                {
                    ret = adventureField(row, col - i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }

                // (8) 오른쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col - 1 >= 0)
                {
                    ret = adventureField(row + i, col - i);
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        totCnt += 1;
                    }
                }
                if (totCnt > 1)
                {
                    if (adv[row, col] == 0)
                    {
                        adv[row, col] = 1;
                        showValue(row, col, 99);
                    }
                }
            }
            #endregion

            // (3) 상대편의 방어가 없는 사삼 - 98점
            makeWhite();
            #region 방어 없는 사삼
            #endregion

            // (5) 상대편의 방어가 없는 사목 - 97점
            makeWhite();
            #region 방어 없는 사목
            while (true)
            {
                found = false;

                // 돌 3개 이어진거 찾으면 끝.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (white[i, j] == 1)
                        {
                            white[i, j] = 0;
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 97);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 97);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 97);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 97);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 97);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 97);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 97);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 97);
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
                    while (ret != null && cnt < 4)
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
                    if (cnt == 3)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 97);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 97);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion

            // (9) 상대편의 방어가 없는 삼삼 - 96점
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    white[i, j] = 1;
                }
            }
            #region 방어 없는 삼삼
            while (true)
            {
                found = false;
                piece = "W";

                // 모든 셀에 대하여 8방향 탐색 후 2줄짜리가 2개 이상이면 해당 셀은 삼삼 가치를 가진다.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (white[i, j] == 1)
                        {
                            white[i, j] = 0;
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
                totCnt = 0;
                i = 1;
                ret = null;
                // (1) 오른쪽으로 탐색
                if (row + 1 < 15)
                {
                    ret = adventureField(row + i, col);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }
                // (2) 오른쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col + 1 < 15)
                {
                    ret = adventureField(row + i, col + i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (3) 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col + 1 < 15)
                {
                    ret = adventureField(row, col + i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (4) 왼쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col + 1 < 15)
                {
                    ret = adventureField(row - i, col + i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (5) 왼쪽으로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0)
                {
                    ret = adventureField(row - i, col);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (6) 왼쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    ret = adventureField(row - i, col - i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (7) 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col - 1 >= 0)
                {
                    ret = adventureField(row, col - i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }

                // (8) 오른쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col - 1 >= 0)
                {
                    ret = adventureField(row + i, col - i);
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        totCnt += 1;
                    }
                }
                if (totCnt > 1)
                {
                    if (adv[row, col] == 0)
                    {
                        adv[row, col] = 1;
                        showValue(row, col, 96);
                    }
                }
            }
            #endregion

            // (4) 상대편의 방어가 있는 사삼 - 70점
            makeWhite();
            #region 방어 있는 사삼
            #endregion

            // (6) 상대편의 방어가 없고 중간에 하나의 빈칸이 있는 사목 - 33점
            makeWhite();
            #region 방어 없고 중간에 하나의 빈칸이 있는 사목
            #endregion

            // (7) 한쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 사목 - 32점
            makeWhite();
            #region 방어 있고 중간에 하나의 빈칸이 있는 사목
            #endregion

            // (8) 양쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 사목 - 31점
            makeWhite();
            #region 양쪽에 방어 있고 중간에 하나의 빈칸이 있는 사목
            #endregion

            // (10) 상대편의 방어가 없는 이삼 - 21점
            makeWhite();
            #region 방어 없는 이삼
            #endregion

            // (11) 상대편의 방어가 없는 이이이 - 20점
            makeWhite();
            #region 방어 없는 이이이
            #endregion

            // (12) 방어 없는 삼목 - 10점
            makeWhite();
            #region 방어 없는 삼목
            while (true)
            {
                found = false;

                // 돌 2개 이어진거 찾으면 끝.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (white[i, j] == 1)
                        {
                            white[i, j] = 0;
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 10);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 10);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 10);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 10);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 10);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 10);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 10);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 10);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 10);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 10);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion

            // (13) 한쪽에 상대편의 방어가 있는 사목 - 9점
            makeWhite();
            #region 방어 있는 사목
            #endregion

            // (14) 상대편의 방어가 있는 삼삼 - 8점
            makeWhite();
            #region 방어 있는 삼삼
            #endregion

            // (15) 상대편의 방어가 없고 중간에 하나의 빈칸이 있는 삼목 - 7점
            makeWhite();
            #region 방어 없고 중간에 하나의 빈칸이 있는 삼목
            #endregion

            // (16) 한쪽에 상대편의 방어가 있고 중간에 하나의 빈칸이 있는 삼목 - 6점
            makeWhite();
            #region 방어 있고 중간에 하나의 빈칸이 있는 삼목
            #endregion

            // (17) 상대편의 방어가 없는 이이 - 5점
            for (i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    white[i, j] = 1;
                }
            }
            #region 방어 없는 이이
            while (true)
            {
                found = false;
                piece = "W";

                // 모든 셀에 대하여 8방향 탐색 후 1줄짜리가 2개 이상이면 해당 셀은 이이 가치를 가진다.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (white[i, j] == 1)
                        {
                            white[i, j] = 0;
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
                totCnt = 0;
                i = 1;
                ret = null;
                // (1) 오른쪽으로 탐색
                if (row + 1 < 15)
                {
                    ret = adventureField(row + i, col);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }
                // (2) 오른쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col + 1 < 15)
                {
                    ret = adventureField(row + i, col + i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (3) 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col + 1 < 15)
                {
                    ret = adventureField(row, col + i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (4) 왼쪽 아래로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col + 1 < 15)
                {
                    ret = adventureField(row - i, col + i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (5) 왼쪽으로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0)
                {
                    ret = adventureField(row - i, col);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (6) 왼쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    ret = adventureField(row - i, col - i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (7) 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (col - 1 >= 0)
                {
                    ret = adventureField(row, col - i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }

                // (8) 오른쪽 위로 탐색
                cnt = 0;
                i = 1;
                ret = null;
                if (row + 1 < 15 && col - 1 >= 0)
                {
                    ret = adventureField(row + i, col - i);
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        totCnt += 1;
                    }
                }
                if (totCnt > 1)
                {
                    if (adv[row, col] == 0)
                    {
                        adv[row, col] = 1;
                        showValue(row, col, 5);
                    }
                }
            }
            #endregion

            // (18) 상대편의 방어가 없는 삼목 - 4점
            makeWhite();
            #region 방어 없는 삼목
            while (true)
            {
                found = false;

                // 돌 2개 이어진거 찾으면 끝.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (white[i, j] == 1)
                        {
                            white[i, j] = 0;
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 4);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 4);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 4);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 4);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 4);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 4);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 4);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 4);
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
                    while (ret != null && cnt < 3)
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
                    if (cnt == 2)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 4);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 4);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion
            
            // (19) 한쪽에 상대편의 방어가 있는 삼목 - 3점
            makeWhite();
            #region 방어 있는 삼목
            #endregion

            // (20) 상대편의 방어가 없는 이목 - 2점
            makeWhite();
            #region 방어 없는 이목
            while (true)
            {
                found = false;

                // 돌 1개 이어진거 찾으면 끝.
                for (i = 0; i < 15; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (white[i, j] == 1)
                        {
                            white[i, j] = 0;
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row - 1 >= 0)
                        {
                            if (adv[row - 1, col] == 0)
                            {
                                showValue(row - 1, col, 2);
                                adv[row - 1, col] = 1;
                            }
                        }
                        if (row + i < 15)
                        {
                            if (adv[row + i, col] == 0)
                            {
                                showValue(row + i, col, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row - 1 >= 0 && col - 1 >= 0)
                        {
                            if (adv[row - 1, col - 1] == 0)
                            {
                                showValue(row - 1, col - 1, 2);
                                adv[row - 1, col - 1] = 1;
                            }
                        }
                        if (row + i < 15 && col + i < 15)
                        {
                            if (adv[row + i, col + i] == 0)
                            {
                                showValue(row + i, col + i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (col - 1 >= 0)
                        {
                            if (adv[row, col - 1] == 0)
                            {
                                showValue(row, col - 1, 2);
                                adv[row, col - 1] = 1;
                            }
                        }
                        if (col + i < 15)
                        {
                            if (adv[row, col + i] == 0)
                            {
                                showValue(row, col + i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row + 1 < 15 && col - 1 >= 0)
                        {
                            if (adv[row + 1, col - 1] == 0)
                            {
                                showValue(row + 1, col - 1, 2);
                                adv[row + 1, col - 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col + i < 15)
                        {
                            if (adv[row - i, col + i] == 0)
                            {
                                showValue(row - i, col + i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row + 1 < 15)
                        {
                            if (adv[row + 1, col] == 0)
                            {
                                showValue(row + 1, col, 2);
                                adv[row + 1, col] = 1;
                            }
                        }
                        if (row - i >= 0)
                        {
                            if (adv[row - i, col] == 0)
                            {
                                showValue(row - i, col, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row + 1 < 15 && col + 1 < 15)
                        {
                            if (adv[row + 1, col + 1] == 0)
                            {
                                showValue(row + 1, col + 1, 2);
                                adv[row + 1, col + 1] = 1;
                            }
                        }
                        if (row - i >= 0 && col - i >= 0)
                        {
                            if (adv[row - i, col - i] == 0)
                            {
                                showValue(row - i, col - i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (col + 1 < 15)
                        {
                            if (adv[row, col + 1] == 0)
                            {
                                showValue(row, col + 1, 2);
                                adv[row, col + 1] = 1;
                            }
                        }
                        if (col - i >= 0)
                        {
                            if (adv[row, col - i] == 0)
                            {
                                showValue(row, col - i, 2);
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
                    while (ret != null && cnt < 2)
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
                    if (cnt == 1)
                    {
                        if (row - 1 >= 0 && col + 1 < 15)
                        {
                            if (adv[row - 1, col + 1] == 0)
                            {
                                showValue(row - 1, col + 1, 2);
                                adv[row - 1, col + 1] = 1;
                            }
                        }
                        if (row + i < 15 && col - i >= 0)
                        {
                            if (adv[row + i, col - i] == 0)
                            {
                                showValue(row + i, col - i, 2);
                                adv[row + i, col - i] = 1;
                            }
                        }
                    }
                }
            }
            #endregion

            // (21) 한쪽에 상대편의 방어가 있는 이목 - 1점
            makeWhite();
            #region 방어 있는 이목
            #endregion
            
            return -1;
        }
    }
}
