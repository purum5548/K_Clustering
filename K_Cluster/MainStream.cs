using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Threading;
using K_Cluster.FileIO;
using K_Cluster.DataManagement;
using SFML;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


namespace K_Cluster
{
    class MainStream
    {

        static void Main(string[] args)
        {
            int times = 20;
            int clursters = 3;
            List<Spot> jClusts = (new SpotBuilder()).ExcutePharshing();
            Console.WriteLine("[Total Allocated Classes : " + jClusts.Count + "]");
            Console.Write("[Receive Number of Clursters] : ");
            clursters = int.Parse(Console.ReadLine());
            Console.WriteLine("[ >" + clursters +"<  Clursters Setted ]");
            Console.Write("[How Many Times to Clurstering] : ");
            times = int.Parse(Console.ReadLine());
            Console.WriteLine("[ >" + times + "< Time Iterate Clurstering ]");
            RenderWindow app = new RenderWindow(new VideoMode(1728, 864), "Clustering", Styles.Default);
            WorldSpace2D worldSpace2D = new WorldSpace2D(clursters, jClusts);
            worldSpace2D.PrintInfo(app);
            for (int i = 0; i < times; i++)
            {
                app.Clear();
                // Process events
                app.DispatchEvents();
                Console.WriteLine("[" + i + " Times Iteration]");
                worldSpace2D.Clustering(app);
                worldSpace2D.PrintInfo(app);
                worldSpace2D.PrintImportantInfo(app);

                // Update the window
                app.Display();
            }
            Console.WriteLine("[ Clurstering Completed, Press EnterKey to Close ]");
            Console.ReadLine();
        }
    }
}
