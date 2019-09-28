using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K_Cluster.DataManagement;

namespace K_Cluster.FileIO
{
    class SpotBuilder
    {
        private float[,] coffients = new float[4423,500];
        public List<Spot> Spots = new List<Spot>();
        private string[] titles = new string[500];
        private string[] word = new string[4423];
        string matPath = "termDocMatrix.txt";
        string twPath = "wordDict-docTitle.txt";

        // 제목과 단어들을 문자열로 파싱한다.
        private void PharseWordsAndTitle()
        {
            string[] Lines = System.IO.File.ReadAllLines(twPath);
            int enmm = 1;
            while (Lines[enmm] != "]")
            {
                word[enmm - 1] = (Lines[enmm].Replace("\"", "")).Trim();
                enmm++;
            }
            enmm+=3; // 엔터와 ], 시작 문자열을 건너뛴다.
            int tenmm = 0; // 제목 전용 인덱스 할당
            while (Lines[enmm] != "]")
            {
                titles[tenmm] = (Lines[enmm].Replace("\"", "")).Trim();
                enmm++;
                tenmm++;
            }
        }

        // 실수값을 문자열을 바탕으로 행렬에 파싱한다.
        private void BuildMatrix()
        {
            string[] Lines = System.IO.File.ReadAllLines(matPath);
            Lines[0].Remove(0, 1); // 첫 공백글자 제거
            int j = 0;
            foreach(string s in Lines)
            {
                string[] spLine = s.Split(new string[] { "  " }, StringSplitOptions.None); // "  "기준으로 분해
                int i = 0;
                foreach (string t in spLine)
                {
                    this.coffients[j,i] = float.Parse(t); // 문자열을 실수로 파싱한 뒤, 인덱스에 맞는 행렬에 대입한다.
                    i++;
                }
                j++;
            }
        }

        // 파싱한 데이터들을 바탕으로 Spot를 빌드한다
        public void BuildClass()
        {
            for(int x = 0; x < 4423; x++)
            {
                for (int y = 0; y < 500; y++)
                {
                    Spot clust = new Spot(coffients[x,y],titles[y],word[x]);
                    Spots.Add(clust);
                }
            }
        }

        // 파일로부터 Spot들을 빌드하고, 이 리스트를 반환한다.
        public List<Spot> ExcutePharshing()
        {
            Console.WriteLine("[StartPharse]");
            PharseWordsAndTitle();
            BuildMatrix();
            Console.WriteLine("[EndPharse]");
            Console.WriteLine("[Build Class Started...]");
            BuildClass();
            Console.WriteLine("[Build Class Finished]");
            return Spots;
        }


    }
}
