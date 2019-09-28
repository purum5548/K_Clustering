using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace K_Cluster.DataManagement
{
    class Cluster
    {
        private int word, title; // 인덱스들(x,y)
        public List<Spot> Spot = new List<Spot>(); // 이 클러스터에 소속된 Spot들

        // Getter Setter
        public int X
        {
            get
            {
                return word;
            }
        }

        public int Y
        {
            get
            {
                return title;
            }
        }

        public int Count
        {
            get
            {
                return Spot.Count;
            }
        }

        // 소속된 Spot들의 중앙을 계산하고, 이 클러스터의 좌표로 설정한다.
        public void Restruct()
        {
            BigInteger x,y;
            float distx, disty;
            x = new BigInteger(0);
            y = new BigInteger(0);
            for(int i = 0; i < Spot.Count; i++)
            {
                x += Spot[i].wordHash;
                y += Spot[i].titleHash;
            }
            if (Spot.Count > 0)
            {
                x /= Spot.Count;
                y /= Spot.Count;
            }
            distx = word;
            disty = title;
            word = (int)x;
            title = (int)y;
            distx -= (int)x;
            disty -= (int)y;
            distx = (float)(distx / (730740));
            disty = (float)(disty / (730740));
            Console.WriteLine(Math.Sqrt((disty * disty) + (distx * distx)));
        }

        public Cluster(int x, int y)
        {
            word = x;
            title = y;
        }

        // 소속된 모든 Spot를 제거한다.
        public void InitSpots()
        {
            Spot.Clear();
        }

        // Spot를 이 클러스터에 추가한다
        public void AddSpot(Spot jClust)
        {
            Spot.Add(jClust);
        }

        // 계수를 기반으로 정렬한다.
        public void SortCluster()
        {
            Spot.Sort((p1, p2) => {return p2.coefficient.CompareTo(p1.coefficient); });
        }
    }
}
