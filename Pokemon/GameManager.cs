using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms.Design;

namespace Pokemon
{

    public class GameManager
    {
        #region Nhap du lieu
        public List<ImgIndex> imgIndex = new List<ImgIndex>();
        private List<ImgIndex> listbansao = new List<ImgIndex>();
        private GameBoard gameBoard = new GameBoard();
        public GameBoard isGameBoard { get { return gameBoard; } set { gameBoard = value; } }
        public Sound OpenSound =  new Sound(Lib.ClickSoundPath);
        public Sound WinSound = new Sound(Lib.WinSoundPath);
        public Sound NoEat = new Sound(Lib.NoEatSoundPath);
        public GameManager (GameBoard gameBoard)
        {
            this.isGameBoard = gameBoard;
            //this.myLabel = myLabels;
            
        }
        
        private List<List<Pokemon>> myLabel = new List<List<Pokemon>>();
        Dictionary<int, List<Point>> keyValue = new Dictionary<int, List<Point>>();

        #endregion

        #region Load khoi dong
        public void LoadMyLabel()
        {
            myLabel = new List<List<Pokemon>>();
            for (int i = 0; i < Lib.numPikachuCol + 2; i++)
            {
                myLabel.Add(new List<Pokemon>());
                for (int j = 0; j < Lib.numPikachuRow + 2; j++)
                {
                    Pokemon lbl = new Pokemon()
                    {
                        Location = new Point(j * Lib.PikachuWidth, i * Lib.PikachuHeight),
                    };
                    gameBoard.Controls.Add(lbl);
                    lbl.X = j; lbl.Y = i;
                    myLabel[i].Add(lbl);

                }
            }
        }

        #endregion


        #region Ve Ban co game


        public void DrawBoard()
        {
            //listbansao = new List<ImgIndex>();
            listbansao = imgIndex.ToList();
            for (int i = 0; i <= Lib.numPikachuCol + 1; i++)
            {
                for (int j = 0; j <= Lib.numPikachuRow + 1; j++)
                {
                    
                    if (i == 0 || i == Lib.numPikachuCol + 1 || j == 0 || j == Lib.numPikachuRow + 1)
                    {
                        myLabel[i][j].imgIndex = new ImgIndex
                        {
                            img = new Bitmap(1, 1),
                            Index = 0
                        };
                        //myLabel[i][j].Text = myLabel[i][j].imgIndex.Index.ToString();
                        continue;
                    }
                    
                    if (Lib.IsGetEvent == false)
                    myLabel[i][j].Click += Lbl_Click;

                    myLabel[i][j].Enabled = true;



                    GetImageRandom(myLabel[i][j]);

                    if (keyValue.ContainsKey(myLabel[i][j].imgIndex.Index))
                        keyValue[myLabel[i][j].imgIndex.Index].Add(new Point(myLabel[i][j].X, myLabel[i][j].Y));
                    else
                    {
                        keyValue[myLabel[i][j].imgIndex.Index] = new List<Point>() { new Point(myLabel[i][j].X, myLabel[i][j].Y) };
                    }
                }
            }
            listbansao = imgIndex.ToList();
            Lib.IsGetEvent = true;
        }
        #endregion

        #region Kiem tra an

        #region CHECK LINE 
        List<Line> lines = new List<Line>();

        private bool checkLineDoc(int y1, int y2, int x)
        {
            int max = Math.Max(y1, y2);
            int min = Math.Min(y1, y2);
            
            for (int i = min; i <= max; i++)
            {
                if (myLabel[i][x].imgIndex.Index != 0) return false; 
            }
            return true;
        }

        private bool checkLineNgang(int x1, int x2, int y)
        {
            int max = Math.Max(x1,x2);
            int min = Math.Min (x1,x2);
            for (int i = min; i <= max; i++)
            {
                if (myLabel[y][i].imgIndex.Index != 0) return false;
            }
            return true;
        }

        private bool checkVitri(Point p1, Point p2, int maxX, int maxY, int minX, int minY)
        {
            if (p1.X == maxX && p1.Y == maxY && p2.X == minX && p2.Y == minY) return true;
            else if (p2.X == maxX && p2.Y == maxY && p1.X == minX && p1.Y == minY) return true;
            return false;
        }

        private bool checkRectDoc (Point p1, Point p2) 
        {
            int maxX = Math.Max (p1.X, p2.X);
            int minX = Math.Min(p1.X, p2.X);
            int maxY = Math.Max(p1.Y, p2.Y);
            int minY = Math.Min(p1.Y, p2.Y);
            for(int i = minX; i <= maxX; i++)
            {
                if (checkLineDoc(minY, maxY, i))
                {
                    if (!checkVitri(p1, p2, maxX, maxY, minX, minY) && checkLineNgang(i, maxX, minY) && checkLineNgang(minX, i, maxY))
                    {
                        Line line = new Line(myLabel[minY][i].GetPointCenter(), myLabel[maxY][i].GetPointCenter());
                        lines.Add(line);
                        Line line2 = new Line(myLabel[minY][i].GetPointCenter(), myLabel[minY][maxX].GetPointCenter());
                        lines.Add(line2);
                        Line line3 = new Line(myLabel[maxY][i].GetPointCenter(), myLabel[maxY][minX].GetPointCenter());
                        lines.Add(line3);
                        return true;
                    }
                    else if (checkVitri(p1, p2, maxX, maxY, minX, minY) && checkLineNgang(i, maxX, maxY) && checkLineNgang(minX, i, minY))
                    {
                        Line line = new Line(myLabel[minY][i].GetPointCenter(), myLabel[maxY][i].GetPointCenter());
                        lines.Add(line);
                        Line line2 = new Line(myLabel[minY][i].GetPointCenter(), myLabel[minY][minX].GetPointCenter());
                        lines.Add(line2);
                        Line line3 = new Line(myLabel[maxY][i].GetPointCenter(), myLabel[maxY][maxX].GetPointCenter());
                        lines.Add(line3);
                        return true;
                    }
                }
            }
            
            return false;
        }

        private bool checkRectNgang(Point p1, Point p2)
        {
            int maxX = Math.Max(p1.X, p2.X);
            int minX = Math.Min(p1.X, p2.X);
            int maxY = Math.Max(p1.Y, p2.Y);
            int minY = Math.Min(p1.Y, p2.Y);

            for (int i = minY; i <= maxY; i++)
            {
                if (checkLineNgang(minX, maxX, i))
                {
                    if (checkVitri(p1,p2,maxX, maxY, minX, minY) && checkLineDoc(minY, i, minX) && checkLineDoc(i, maxY, maxX))
                    {
                        Line line = new Line(myLabel[i][maxX].GetPointCenter(), myLabel[i][minX].GetPointCenter());
                        lines.Add(line);
                        Line line2 = new Line(myLabel[minY][minX].GetPointCenter(), myLabel[i][minX].GetPointCenter());
                        lines.Add(line2);
                        Line line3 = new Line(myLabel[maxY][maxX].GetPointCenter(), myLabel[i][maxX].GetPointCenter());
                        lines.Add(line3);
                        return true;
                    }
                    else if (!checkVitri(p1, p2, maxX, maxY, minX, minY) && checkLineDoc(i, maxY, minX) && checkLineDoc(minY, i, maxX))
                    {
                        Line line = new Line(myLabel[i][maxX].GetPointCenter(), myLabel[i][minX].GetPointCenter());
                        lines.Add(line);
                        Line line2 = new Line(myLabel[maxY][minX].GetPointCenter(), myLabel[i][minX].GetPointCenter());
                        lines.Add(line2);
                        Line line3 = new Line(myLabel[minY][maxX].GetPointCenter(), myLabel[i][maxX].GetPointCenter());
                        lines.Add(line3);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool checkMoreDoc (Point p1, Point p2)
        {
            int maxX = Math.Max(p1.X, p2.X);
            bool isRight = false;
            while(maxX < Lib.numPikachuRow + 1)
            {
                maxX++;
                if (checkLineDoc(p1.Y,p2.Y, maxX))
                {
                    if (checkLineNgang(maxX, p1.X, p1.Y) && checkLineNgang(maxX, p2.X,p2.Y))
                    { 
                        isRight = true;
                        
                        Line line = new Line(myLabel[p1.Y][maxX].GetPointCenter(), myLabel[p2.Y][maxX].GetPointCenter());
                        lines.Add(line);
                        Line line2 = new Line(myLabel[p1.Y][maxX].GetPointCenter(), myLabel[p1.Y][p1.X].GetPointCenter());
                        lines.Add(line2);
                        Line line3 = new Line(myLabel[p2.Y][maxX].GetPointCenter(), myLabel[p2.Y][p2.X].GetPointCenter());
                        lines.Add(line3);
                        break;
                    }
                }
            }

            if (!isRight)
            {
                int minX = Math.Min(p1.X, p2.X);
                while(minX > 0)
                {
                    minX--;
                    if (checkLineDoc(p1.Y, p2.Y, minX))
                    {
                        if (checkLineNgang(minX, p1.X, p1.Y) && checkLineNgang(minX, p2.X, p2.Y))
                        {
                            isRight = true;

                            Line line = new Line(myLabel[p1.Y][minX].GetPointCenter(), myLabel[p2.Y][minX].GetPointCenter());
                            lines.Add(line);
                            Line line2 = new Line(myLabel[p1.Y][minX].GetPointCenter(), myLabel[p1.Y][p1.X].GetPointCenter());
                            lines.Add(line2);
                            Line line3 = new Line(myLabel[p2.Y][minX].GetPointCenter(), myLabel[p2.Y][p2.X].GetPointCenter());
                            lines.Add(line3);
                            break;
                        }
                    }
                }
            }

            return isRight;
        }

        private bool checkMoreNgang(Point p1,  Point p2)
        {
            bool isDown = false;
            int maxY = Math.Max(p1.Y, p2.Y);
            while(maxY < Lib.numPikachuCol + 1)
            {
                maxY++;
                if (checkLineNgang(p1.X, p2.X, maxY))
                {
                    if (checkLineDoc(p1.Y, maxY, p1.X) && checkLineDoc(p2.Y, maxY, p2.X))
                    {
                        isDown = true;

                        Line line = new Line(myLabel[maxY][p2.X].GetPointCenter(), myLabel[maxY][p1.X].GetPointCenter());
                        lines.Add(line);
                        Line line2 = new Line(myLabel[maxY][p1.X].GetPointCenter(), myLabel[p1.Y][p1.X].GetPointCenter());
                        lines.Add(line2);
                        Line line3 = new Line(myLabel[maxY][p2.X].GetPointCenter(), myLabel[p2.Y][p2.X].GetPointCenter());
                        lines.Add(line3);
                        break;
                    }
                }
            }

            if (!isDown)
            {
                int minY = Math.Min(p1.Y, p2.Y);
                while (minY > 0)
                {
                    minY--;
                    if (checkLineNgang(p1.X, p2.X, minY))
                    {
                        if (checkLineDoc(minY, p1.Y, p1.X) && checkLineDoc(minY, p2.Y, p2.X))
                        {
                            isDown = true;

                            Line line = new Line(myLabel[minY][p2.X].GetPointCenter(), myLabel[minY][p1.X].GetPointCenter());
                            lines.Add(line);
                            Line line2 = new Line(myLabel[minY][p1.X].GetPointCenter(), myLabel[p1.Y][p1.X].GetPointCenter());
                            lines.Add(line2);
                            Line line3 = new Line(myLabel[minY][p2.X].GetPointCenter(), myLabel[p2.Y][p2.X].GetPointCenter());
                            lines.Add(line3);
                            break;
                        }
                    }
                }
            }
            return isDown;
        }

        #endregion
        private void DrawLine()
        {
            foreach(Line line in lines)
            {
                
                    isGameBoard.Controls.Add(line);
                    line.BringToFront();
                
            }
        }

        private void DelLine()
        {
            
            foreach(Line line in lines)
            {
                line.Dispose();
                isGameBoard.Controls.Remove(line);
            }
            lines = new List<Line>();
        }

        int k, m;
        private bool IsEat(Point p1, Point p2)
        {
            k = myLabel[p1.Y][p1.X].imgIndex.Index;
            m = myLabel[p2.Y][p2.X].imgIndex.Index;
            myLabel[p1.Y][p1.X].imgIndex.Index = 0;
            myLabel[p2.Y][p2.X].imgIndex.Index = 0;

            if (p1.X == p2.X)
            {
                if (checkLineDoc(p1.Y, p2.Y, p1.X))
                { 
                    Line line = new Line(myLabel[p1.Y][p1.X].GetPointCenter(), myLabel[p2.Y][p2.X].GetPointCenter());
                    lines.Add(line);
                    //DeletePokemon(p1, p2); 
                    //DrawLine();
                    return true; 
                } else if (checkMoreDoc(p1,p2))
                {
                    //DeletePokemon(p1, p2);
                    //DrawLine();
                    return true;
                }
            }
            else if (p1.Y == p2.Y)
            {
                if (checkLineNgang(p1.X, p2.X,p1.Y) ){
                    Line line = new Line(myLabel[p1.Y][p1.X].GetPointCenter(), myLabel[p2.Y][p2.X].GetPointCenter());
                    lines.Add(line);
                    //DeletePokemon(p1, p2);
                    //DrawLine();
                    return true;
                }
                else if (checkMoreNgang(p1, p2))    
                { 
                    //DeletePokemon(p1, p2);
                    //DrawLine();
                    return true; 
                } 

            }
            else 
            {
                if (checkRectDoc(p1, p2) ){
                    //DeletePokemon(p1, p2);
                    //DrawLine();
                    return true;
                }
                else if (checkRectNgang(p1, p2))
                {
                    //DeletePokemon(p1, p2);
                    //DrawLine();
                    return true;
                }
                else if (checkMoreDoc(p1, p2))
                {
                    //DeletePokemon(p1, p2);
                    //DrawLine();
                    return true;
                }
                else if (checkMoreNgang(p1,p2))
                {
                    //DeletePokemon(p1, p2);
                    //DrawLine();
                    return true;
                }

            }
            myLabel[p1.Y][p1.X].imgIndex.Index = k;
            myLabel[p2.Y][p2.X].imgIndex.Index = m;
            return false;
            
        }

        private void Del(Point p1, Point p2)
        {
            if (IsEat(p1, p2))
            {
                myLabel[p1.Y][p1.X].imgIndex.Index = k;
                myLabel[p2.Y][p2.X].imgIndex.Index = m;
                OpenSound.PlaySound();

                DeletePokemon(p1, p2);
            } else NoEat.PlaySound();
            

            if (listbansao.Count == 0) { WinSound.PlaySound(); ResetGame(); }
           
        }

        

        public void ResetGame()
        {
            //


            //MessageBox.Show("WIN");
            this.ClearGame();
            this.DrawBoard();
            if (Lib.temp != null!) 
            { Lib.temp.BackColor = Lib.BackColorPikachu; }

            Lib.temp = null!;
            
        }
        #endregion

        #region Xoa Pokemon giong nhau
        private void DeletePokemon(Point p1, Point p2)
        {
            int indexToRemove = listbansao.FindIndex(item => item.Index == myLabel[p1.Y][p1.X].imgIndex.Index);

            listbansao.RemoveAt(indexToRemove);
            int indexToRemove2 = listbansao.FindIndex(item => item.Index == myLabel[p2.Y][p2.X].imgIndex.Index);

            listbansao.RemoveAt(indexToRemove2);
            DrawLine();
            Application.DoEvents();
            Thread.Sleep(200);
            myLabel[p1.Y][p1.X].imgIndex.img = new Bitmap(1,1);
            myLabel[p1.Y][p1.X].BackColor = Lib.BackColorPikachu;
            keyValue[myLabel[p1.Y][p1.X].imgIndex.Index].Remove(p1);

            myLabel[p1.Y][p1.X].imgIndex.Index = 0;
            myLabel[p1.Y][p1.X].Enabled = false;
            myLabel[p1.Y][p1.X].ReloadLabel();
           

            myLabel[p2.Y][p2.X].imgIndex.img = new Bitmap(1,1);
            myLabel[p2.Y][p2.X].BackColor = Lib.BackColorPikachu;

            keyValue[myLabel[p2.Y][p2.X].imgIndex.Index].Remove(p2);
            myLabel[p2.Y][p2.X].imgIndex.Index = 0;
            myLabel[p2.Y][p2.X].Enabled = false;
            myLabel[p2.Y][p2.X].ReloadLabel();
            DelLine();

        }

      
        #endregion

        #region Gan hinh anh cho pokemon
        
        private void GetImageRandom(Pokemon myLabel)
        {
            if (imgIndex.Count == 0) return;
            
            Random random = new Random();
            int index = random.Next(0, listbansao.Count - 1);
            myLabel.imgIndex = new ImgIndex
            {
                img = listbansao[index].img,
                Index = listbansao[index].Index,
            };
            myLabel.ReloadLabel();
            listbansao.RemoveAt(index);
        }


        #endregion
      
        #region Xao tron 
        public void XaoTron()
        {
            List<ImgIndex>  tempkk = listbansao.ToList();
            //MessageBox.Show(listbansao.Count.ToString());
            keyValue.Clear();
            foreach (List<Pokemon> listMylabel in myLabel)
            {
                foreach (Pokemon myLabels in listMylabel)
                {
                    if (myLabels.imgIndex.Index != 0)
                    {
                        
                        Random random = new Random();
                        int k = random.Next(0, tempkk.Count - 1);
                        //MessageBox.Show(tempkk[k].Index.ToString());
                        myLabels.imgIndex = new ImgIndex { Index = tempkk[k].Index, img = tempkk[k].img };
                        tempkk.RemoveAt(k);
                        myLabels.ReloadLabel();
                        if (keyValue.ContainsKey(myLabels.imgIndex.Index))
                            keyValue[myLabels.imgIndex.Index].Add(new Point(myLabels.X, myLabels.Y));
                        else
                        {
                            keyValue[myLabels.imgIndex.Index] = new List<Point>() { new Point(myLabels.X, myLabels.Y) };
                        }
                    }
                }
            }
            tempkk.Clear();
            
        }
        #endregion
        

        #region Goi y
        public void GoiY ()
        {

            for(int i = 1; i < Lib.numPikachuCol + 1; i++)
            {
                for(int j = 1; j < Lib.numPikachuRow + 1; j++)
                {
                    if (myLabel[i][j].imgIndex.Index != 0)
                    {
                        foreach(Point p in keyValue[myLabel[i][j].imgIndex.Index])
                        {
                            if (p.Y != myLabel[i][j].Y || p.X != myLabel[i][j].X)
                            {
                                if (IsEat(p, new Point(myLabel[i][j].X, myLabel[i][j].Y)))
                                {
                                    myLabel[p.Y][p.X].BackColor = Lib.ChoosePikachu;
                                    myLabel[i][j].BackColor = Lib.ChoosePikachu;
                                    myLabel[p.Y][p.X].imgIndex.Index = k;
                                    myLabel[i][j].imgIndex.Index = m;
                                    DelLine();
                                    //MessageBox.Show(keyValue[myLabel[i, j].imgIndex.Index].Count.ToString());
                                    //Del(p, new Point(myLabel[i, j].X, myLabel[i, j].Y));
                                    return;
                                }
                                
                                
                            }

                        }
                    }
                }
            }
        }

        public void AutoPlay()
        {
            
            int count = 0;
            while (listbansao.Count > 0) {
            
                for (int i = 1; i < Lib.numPikachuCol + 1; i++)
                {
                    for (int j = 1; j < Lib.numPikachuRow + 1; j++)
                    {
                        if (myLabel[i][j].imgIndex.Index != 0)
                        {
                            try
                            {
                                foreach (Point p in keyValue[myLabel[i][j].imgIndex.Index])
                                {
                                    //Point p = keyValue[myLabel[i, j].imgIndex.Index][k];
                                    if (p.Y != myLabel[i][j].Y || p.X != myLabel[i][j].X)
                                    {
                                        if (IsEat(p, new Point(myLabel[i][j].X, myLabel[i][j].Y)))
                                        {
                                            myLabel[p.Y][p.X].BackColor = Lib.ChoosePikachu;
                                            myLabel[i][j].BackColor = Lib.ChoosePikachu;
                                            myLabel[p.Y][p.X].imgIndex.Index = k;
                                            myLabel[i][j].imgIndex.Index = m;
                                            //DelLine();
                                            count++;
                                            DeletePokemon(p, new Point(myLabel[i][j].X, myLabel[i][j].Y));
                                            //Del(p, new Point(myLabel[i][j].X, myLabel[i][j].Y));
                                            //return;
                                        }


                                    }
                                }
                            }
                            catch
                            {
                                continue;
                            }

                        }

                    }
                }
                if (listbansao.Count == 0)
                {
                    WinSound.PlaySound();
                    ResetGame(); return;
                }
                if (count == 0) XaoTron();
                count = 0;
            }
        }
        #endregion


        #region Thuc hien an nut

        
        private void Lbl_Click(object sender, EventArgs e) 
        {

           //MessageBox.Show(temp.imgIndex.Index.ToString());
            Pokemon lbl = sender as Pokemon;
            
            if (lbl != null!)
            {
                
                lbl.BackColor = Lib.ChoosePikachu;
                if (Lib.temp != null!)
                {
                    
                    //MessageBox.Show(Lib.temp.imgIndex.Index.ToString());

                    
                    lbl.BackColor = Lib.ChoosePikachu;

                    if (myLabel[Lib.temp.Y][Lib.temp.X].imgIndex.Index == myLabel[lbl.Y][lbl.X].imgIndex.Index && Lib.temp != lbl)
                    {
                        Del(new Point(lbl.X, lbl.Y), new Point(Lib.temp.X, Lib.temp.Y));

                    }
                    else NoEat.PlaySound();
                    if (listbansao.Count > 0)
                    {
                        Lib.temp.BackColor = Lib.BackColorPikachu;
                        lbl.BackColor = Lib.BackColorPikachu;
                        Lib.temp = null!;
                    }
                }
                else
                {
                    Lib.temp = lbl;
                    Lib.temp.BackColor = Lib.ChoosePikachu;
                    
                }
            }
        }
        #endregion

        #region Clear
        public void ClearGame()
        {
            
            keyValue.Clear();
            lines.Clear();
            listbansao.Clear();
        }
        #endregion
    }
}
