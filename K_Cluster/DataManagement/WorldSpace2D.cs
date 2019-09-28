using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;



namespace K_Cluster.DataManagement
{

    class WorldSpace2D
    {
        private List<Cluster> clusters = new List<Cluster>();
        private List<Color> colors = new List<Color>();
        private List<Spot> Spot;

        // 시각화 캐쉬
        private CircleShape shape = new CircleShape(0.3f);
        private CircleShape core = new CircleShape(5);
        private CircleShape hotSpote = new CircleShape(2);
        private Text text = new Text();

        private float rate = 0.7f;



        public WorldSpace2D(int cluster, List<Spot> clusts)
        {
            Console.WriteLine("[Build WorldSpace2D, Clusters : " + cluster + " ]");
            // 클러스터들의 초기 좌표를 렌덤하면서, 겹치지 않게 설정한다.
            int[] x, y, r, g, b;
            Random random = new Random();
            x = new int[cluster];
            y = new int[cluster];
            r = new int[cluster];
            g = new int[cluster];
            b = new int[cluster];
            Console.WriteLine("[WorldSpace2D, Setting RandomSeeds]");
            for (int i = 0; i < cluster; i++)
            {
                int temp;
                do
                {
                    temp = random.Next(int.MinValue / 10, int.MaxValue / 10);
                }
                while (x.Contains<int>(temp));
                x[i] = temp;
            }
            for (int i = 0; i < cluster; i++)
            {
                int temp;
                do
                {
                    temp = random.Next(int.MinValue / 10, int.MaxValue / 10);
                }
                while (y.Contains<int>(temp));
                y[i] = temp;
            }
            Console.WriteLine("[WorldSpace2D, Setting RandomSeeds Finished]");

            Console.WriteLine("[WorldSpace2D, Setting RandomColorSeeds]");
            for (int i = 0; i < cluster; i++)
            {
                int temp;
                do
                {
                    temp = random.Next(50,255);
                }
                while (r.Contains<int>(temp));
                r[i] = temp;
            }
            for (int i = 0; i < cluster; i++)
            {
                int temp;
                do
                {
                    temp = random.Next(50,255);
                }
                while (g.Contains<int>(temp));
                g[i] = temp;
            }
            for (int i = 0; i < cluster; i++)
            {
                int temp;
                do
                {
                    temp = random.Next(50,255);
                }
                while (b.Contains<int>(temp));
                b[i] = temp;
            }
            Console.WriteLine("[WorldSpace2D, Setting RandomColorSeeds Finished]");

            for (int i = 0; i < cluster; i++)
            {
                clusters.Add(new Cluster(x[i],y[i]));
                colors.Add(new Color((byte)r[i], (byte)g[i], (byte)b[i]));
            }
            //시각화 데이터 초기화
            core.OutlineColor = Color.White;
            core.OutlineThickness = 1f;
            hotSpote.OutlineColor = Color.White;
            hotSpote.OutlineThickness = 1f;
            text.OutlineColor = Color.White;
            text.FillColor = Color.Black;
            text.CharacterSize = 12;
            text.OutlineThickness = 2;
            text.Font = new Font("NanumSquare.ttf");
            Spot = clusts;
            Console.WriteLine("[Build WorldSpace2D Finished]");
        }



        //Spot들에게 자신과 가장 가까운 클러스터를 지정해 준다.
        public void Clustering(RenderWindow app) // 시각화를 위해서 점을 그릴 창을 인자로 받음(최적화를 위해서 MVC패턴 분리 없음)
        {
            Console.WriteLine("[Clustering Started : " + DateTime.Now + "]");
            for (int j = 0; j < clusters.Count; j++) // 각 클러스터 초기화
            {
                clusters[j].InitSpots();
            }
            for (int i = 0; i < Spot.Count; i++) // 각 j클러스터로부터 가장 가까운 클러스터를 지정
            {
                int index = 0;
                float distance = float.MaxValue;
                for(int j = 0; j < clusters.Count; j++)
                {
                    float tempDistance = Spot[i].GetNormal(clusters[j].X,clusters[j].Y);
                    if(distance > tempDistance)
                    {
                        index = j;
                        distance = tempDistance;
                    }
                }
                clusters[index].AddSpot(Spot[i]);
                DrawSpot(app,Spot[i],index);
            }
            for (int j = 0; j < clusters.Count; j++) // 각 클러스터의 중심을 다시 계산
            {
                Console.Write("       >JCluster " + j +": ");
                clusters[j].Restruct();
            }
            Console.WriteLine("[Clustering Finished : " + DateTime.Now + "]");
        }

        // 클러스터들의 정보를 모두 출력
        public void PrintInfo(RenderWindow app)
        {
            for (int j = 0; j < clusters.Count; j++) // 각 클러스터 초기화
            {
                //Console.WriteLine("       " + j + "번째 클러스터 정보 <" + clusters[j].X + ", " + clusters[j].Y + "  Count : " +clusters[j].Count+">");
                DrawSpot(app, j);
            }
        }

        // 클러스터별 중요한 제목 5개를 출력
        public void PrintImportantInfo(RenderWindow app)
        {
            for (int j = 0; j < clusters.Count; j++) // 각 클러스터 초기화
            {
                clusters[j].SortCluster();
                DrawImportant(app,j);
            }
        }

        public void GetRender(RenderWindow app)
        {
            for (int j = 0; j < clusters.Count; j++) // 각 클러스터 초기화
            {
                for (int i = 0; i < clusters[j].Spot.Count; i++) // 각 j클러스터로부터 가장 가까운 클러스터를 지정
                {
                    DrawSpot(app, Spot[i], j);
                }
                DrawSpot(app, j);
                DrawImportant(app, j);
            }
        }

        private void DrawSpot(RenderWindow app, Spot jClust, int idx)
        {
            Vector2f vector = new Vector2f();
            float x, y;
            x = jClust.wordHash / (391468 * rate) + 864f;
            y = jClust.titleHash / (730740 * rate) + 432f;
            vector.X = x;
            vector.Y = y;
            shape.Position = vector;
            shape.FillColor = colors[idx];
            app.Draw(shape);
        }

        private void DrawSpot(RenderWindow app, int idx)
        {
            Vector2f vector = new Vector2f();
            float x, y;
            x = clusters[idx].X / (391468 * rate) + 864f;
            y = clusters[idx].Y / (730740 * rate) + 432f;
            vector.X = x;
            vector.Y = y;
            core.Position = vector;
            core.FillColor = colors[idx];
            app.Draw(core);
        }

        private void DrawImportant(RenderWindow app, int idx)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2f vector = new Vector2f();
                float x, y;
                x = clusters[idx].Spot[i].wordHash / (391468 * rate) + 864f;
                y = clusters[idx].Spot[i].titleHash / (730740 * rate) + 432f;
                vector.X = x;
                vector.Y = y;
                hotSpote.Position = vector;
                hotSpote.FillColor = colors[idx];
                app.Draw(hotSpote);
                vector.X += 10 + (4 * i);
                vector.Y -= 10 + (9 * i);
                text.DisplayedString = clusters[idx].Spot[i].title;
                text.Position = vector;
                //text.FillColor = colors[idx];
                text.CharacterSize = (uint)(10 + (3 * i));
                app.Draw(text);
            }
        }

    }


}
