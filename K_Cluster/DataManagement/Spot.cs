using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_Cluster.DataManagement
{
    class Spot
    {
        public readonly float coefficient; // 계수 값
        public readonly string title; // 제목
        public readonly string word; // 단어
        public readonly int titleHash; // 빠른 실행 속도를 위한 제목에 대한 해쉬값을 캐싱
        public readonly int wordHash; // 빠른 실행 속도를 위한 제목에 대한 해쉬값을 캐싱

        public Spot(float coefficient, string title, string word)
        {
            this.coefficient = coefficient;
            this.title = title;
            this.word = word;
            this.titleHash = title.GetHashCode() / 10;
            this.wordHash = word.GetHashCode() / 10;
        }

        // 어떤 점과 이 점 사이의 거리를 계산한다.
        public float GetNormal(int word,int title)
        {
            int titleLength = title - titleHash;
            int wordLength = word - wordHash;
            return (float)Math.Sqrt(((double)titleLength * titleLength) + ((double)wordLength * wordLength));
        }
    }
}
