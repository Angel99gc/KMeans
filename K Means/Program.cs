using System;
using System.Diagnostics;



namespace ClusteringKMeans
{
    class KMeansDemo
    {
        //AUTORS
        //Angel Gamboa Cruz 
        //Jason Barrantes Rodriguez
        static int n = 1000; // variable del tamano de la lista
        static int a = 0; //asignaciones
        static int c = 0; //comparaciones
        static void Main(string[] args)
        {
            
            //LISTA ASCENDENTE
            var tiempo = Stopwatch.StartNew(); //inicia el tiempo
            Console.WriteLine("\nK MEANS\n");

            // la lista que se envia para el kmeans
            double[][] rawData = ascendente(n); // se da la lista segun se necesita
            Console.WriteLine("Datos del arreglo sin modificar:\n");
            Console.WriteLine("    dato1 dato2");
            Console.WriteLine("-------------------");
            ShowData(rawData, 1, true, true);

            int numClusters = 3; //cantidad que sera dividida la lista
            Console.WriteLine("\n numClusters = " + numClusters);
            int[] clustering = Cluster(rawData, numClusters); // Lista de los clusters
            Console.WriteLine("\nLista de clusters completada\n");

            Console.WriteLine("Arreglo dividido por los clusters:\n");
            ShowClustered(rawData, clustering, numClusters, 1); //muestra las listas segun los clusters

            Console.WriteLine("Asignaciones = " + a);
            Console.WriteLine("Comparaciones = " + c);
            Console.WriteLine("Tiempo: " + tiempo.Elapsed.TotalMilliseconds);
            Console.WriteLine("Presione ENTER");
            Console.ReadLine();


            //LISTA DESCENDENTE
            var tiempo1 = Stopwatch.StartNew(); //inicia el tiempo
            a = 0; //asignaciones
            c = 0; //comparaciones
            Console.WriteLine("\nK MEANS\n");

            // la lista que se envia para el kmeans
            rawData = descendente(n); // se da la lista segun se necesita
            Console.WriteLine("Datos del arreglo sin modificar:\n");
            Console.WriteLine("    dato1 dato2");
            Console.WriteLine("-------------------");
            ShowData(rawData, 1, true, true);

            numClusters = 3; //cantidad que sera dividida la lista
            Console.WriteLine("\n numClusters = " + numClusters);
            clustering = Cluster(rawData, numClusters); // Lista de los clusters
            Console.WriteLine("\nLista de clusters completada\n");

            Console.WriteLine("Arreglo dividido por los clusters:\n");
            ShowClustered(rawData, clustering, numClusters, 1); //muestra las listas segun los clusters

            Console.WriteLine("Asignaciones = " + a);
            Console.WriteLine("Comparaciones = " + c);
            Console.WriteLine("Tiempo: " + tiempo1.Elapsed.TotalMilliseconds);
            Console.WriteLine("Presione ENTER");
            Console.ReadLine();

            //LISTA ALEATORIA
            var tiempo2 = Stopwatch.StartNew(); //inicia el tiempo
            a = 0; //asignaciones
            c = 0; //comparaciones
            Console.WriteLine("\nK MEANS\n");

            // la lista que se envia para el kmeans
            rawData = aleatorio(n); // se da la lista segun se necesita
            Console.WriteLine("Datos del arreglo sin modificar:\n");
            Console.WriteLine("    dato1 dato2");
            Console.WriteLine("-------------------");
            ShowData(rawData, 1, true, true);

            numClusters = 3; //cantidad que sera dividida la lista
            Console.WriteLine("\n numClusters = " + numClusters);
            clustering = Cluster(rawData, numClusters); // Lista de los clusters
            Console.WriteLine("\nLista de clusters completada\n");

            Console.WriteLine("Arreglo dividido por los clusters:\n");
            ShowClustered(rawData, clustering, numClusters, 1); //muestra las listas segun los clusters

            Console.WriteLine("Asignaciones = " + a);
            Console.WriteLine("Comparaciones = " + c);
            Console.WriteLine("Tiempo: " + tiempo2.Elapsed.TotalMilliseconds);
            Console.WriteLine("Presione ENTER");
            Console.ReadLine();
        }

        //FUNCIONES PARA CREAR LA MATRIZ
        public static double[][] ascendente(int n){ 
            double[][] rawData = new double[n][];
            double num = 1.0;
            for (int i = 0; i < n; i++)
            {
                rawData[i] = new double[] { num, num };
                num += 1;
            }
            return rawData;
        }
        public static double[][] descendente(int n)
        {
            double[][] rawData = new double[n][];
            double num = n;
            for (int i = 0; i < n; i++)
            {
                rawData[i] = new double[] { num, num };
                num -= 1;
            }
            return rawData;
        }
        public static double[][] aleatorio(int n) {
            double[][] rawData = new double[n][];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                double num = rnd.Next(0, 100);
                rawData[i] = new double[] { num, num };
            }
            return rawData;
        }
        //FUNCIONES PARA CREAR LA MATRIZ


   

        // ============================================================================

        public static int[] Cluster(double[][] rawData, int numClusters) //SACA LOS CLUSTER DEL LA LISTA SEGUN LA CANTIDAD QUE DESEE
        {
            double[][] data = Normalized(rawData); a++; //LLAMA LA FUNCION PARA NORMALIZAR LA LISTA


            bool changed = true;a++; 
            bool success = true;a++;

          
         
            int[] clustering = InitClustering(data.Length, numClusters, 0); a++; 
            double[][] means = Allocate(numClusters, data[0].Length); a++;

            int maxCount = data.Length * 10;a++; 
            int ct = 0;a++;
            while (changed == true && success == true && ct < maxCount)
            {
                c++;

                ++ct;a++; 
                success = UpdateMeans(data, clustering, means);a++; 
                changed = UpdateClustering(data, clustering, means);a++;
            }
            c++;
            return clustering;
        }

        private static double[][] Normalized(double[][] rawData) //RECIBE LA MATRIZ Y DEVUELVE LOS VALORES NORMALIZADOS PARA TRATARLOS
        {
            double[][] result = new double[rawData.Length][];a++;

            a++;
            for (int i = 0; i < rawData.Length; ++i)
            {
                c++;
                a++;
                result[i] = new double[rawData[i].Length];a++;
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }
            c++;

            a++;
            for (int j = 0; j < result[0].Length; ++j) 
            {
                c++;a++;

                double colSum = 0.0; a++;

                
                a++;
                for (int i = 0; i < result.Length; ++i)
                {
                    c++;a++;
                    colSum += result[i][j];a++;
                }
                c++;
                double mean = colSum / result.Length;a++;
                double sum = 0.0;a++;

                a++;
                for (int i = 0; i < result.Length; ++i)
                {
                    c++;a++;
                    sum += (result[i][j] - mean) * (result[i][j] - mean);a++;
                }
                c++;
                double sd = sum / result.Length;a++;

                a++;
                for (int i = 0; i < result.Length; ++i)
                {
                    c++;a++;
                    result[i][j] = (result[i][j] - mean) / sd;a++;
                }
                c++;
            }
            return result;
        }

        private static int[] InitClustering(int numTuples, int numClusters, int randomSeed) //DEVUELVE LOS NUMEROS DE LA LISTA DADA LOS CLUSTER A DIVIDIR 
        {

            Random random = new Random(randomSeed);a++;
            int[] clustering = new int[numTuples];a++;

            a++;
            for (int i = 0; i < numClusters; ++i)
            { 
                c++;a++;
                clustering[i] = i;a++;
            }
            c++;

            a++;
            for (int i = numClusters; i < clustering.Length; ++i)
            {
                c++;a++;
                clustering[i] = random.Next(0, numClusters); a++; 
            }
            c++;
            return clustering;
        }

        private static double[][] Allocate(int numClusters, int numColumns) //CREA LAS COLUMNAS DE LOS NUMEROS DE LA MATRIZ
        {
            double[][] result = new double[numClusters][]; a++;

            a++;
            for (int k = 0; k < numClusters; ++k)
            {
                c++;a++;
                result[k] = new double[numColumns];a++;
            }
            c++;
            return result;
        }

        private static bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
        {

            int numClusters = means.Length; a++;
            int[] clusterCounts = new int[numClusters]; a++;

            a++;
            for (int i = 0; i < data.Length; ++i)
            {
                c++;a++;
                int cluster = clustering[i];a++;
                ++clusterCounts[cluster];a++;
            }
            c++;

            a++;
            for (int k = 0; k < numClusters; ++k)
            {
                c++;a++;
                if (clusterCounts[k] == 0)
                {
                    c++;
                    return false; 
                }
            }
            c++;

            a++;
            for (int k = 0; k < means.Length; ++k)
            {
                c++;a++;

                a++;
                for (int j = 0; j < means[k].Length; ++j)
                {
                    c++;a++;
                    means[k][j] = 0.0;a++;
                }
                c++;
            }
            c++;

            a++;
            for (int i = 0; i < data.Length; ++i)
            {
                c++;a++;
                int cluster = clustering[i];a++;

                a++;
                for (int j = 0; j < data[i].Length; ++j)
                {
                    c++;a++;
                    means[cluster][j] += data[i][j];a++; 
                }
                c++;
            }

            a++;
            for (int k = 0; k < means.Length; ++k)
            {
                c++;a++;

                a++;
                for (int j = 0; j < means[k].Length; ++j)
                {
                    c++;a++;
                    means[k][j] /= clusterCounts[k];a++;

                }
                c++;
            }
            return true;
        }

        private static bool UpdateClustering(double[][] data, int[] clustering, double[][] means) //VERIFICA SI NECESITA ACTUALIZAR LA LISTA CORRECTAMENTE
        {

            int numClusters = means.Length;a++;
            bool changed = false;a++;

            int[] newClustering = new int[clustering.Length];a++; 
            Array.Copy(clustering, newClustering, clustering.Length);

            double[] distances = new double[numClusters];a++; 

            a++;
            for (int i = 0; i < data.Length; ++i) 
            {
                c++;a++;

                a++;
                for (int k = 0; k < numClusters; ++k)
                {
                    c++;a++;
                    distances[k] = Distance(data[i], means[k]);a++; 
                }
                c++;
                int newClusterID = MinIndex(distances);a++; 
                if (newClusterID != newClustering[i])
                {
                    c++;
                    changed = true;a++;
                    newClustering[i] = newClusterID;a++; 
                }
            }

            if (changed == false)
            {
                c++;
                return false; 
            }

            int[] clusterCounts = new int[numClusters];a++;

            a++;
            for (int i = 0; i < data.Length; ++i)
            {
                c++;a++;
                int cluster = newClustering[i];a++;
                ++clusterCounts[cluster];a++;
            }
            c++;

            a++;
            for (int k = 0; k < numClusters; ++k)
            {
                c++;a++;
                if (clusterCounts[k] == 0)
                {
                    c++;
                    return false; 
                }
            }
            c++;
            Array.Copy(newClustering, clustering, newClustering.Length); 
            return true; 
        }

        private static double Distance(double[] tuple, double[] mean) // DISTANCIA QUE EXISTE ENTRE LOS NUMEROS CON EL CLUSTER 
        {

            double sumSquaredDiffs = 0.0;a++;

            a++;
            for (int j = 0; j < tuple.Length; ++j)
            {
                c++;a++;
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);a++;
            }
            c++;
            return Math.Sqrt(sumSquaredDiffs);
        }

        private static int MinIndex(double[] distances)
        {

            int indexOfMin = 0;a++;
            double smallDist = distances[0];a++;

            a++;
            for (int k = 0; k < distances.Length; ++k)
            {
                c++;a++;
                if (distances[k] < smallDist)
                {
                    c++;
                    smallDist = distances[k];a++;
                    indexOfMin = k;a++;
                }
            }
            c++;
            return indexOfMin;
        }



        static void ShowData(double[][] data, int decimals, bool indices, bool newLine) //MUESTRA LOS DATOS
        {
            a++;
            for (int i = 0; i < data.Length; ++i)
            {
                c++;a++;
                if (indices) Console.Write(i.ToString().PadLeft(3) + " ");
                {
                    c++;

                    a++;
                    for (int j = 0; j < data[i].Length; ++j)
                    {
                        c++;a++;
                        if (data[i][j] >= 0.0) Console.Write(" ");
                        {
                            c++;
                            Console.Write(data[i][j].ToString("F" + decimals) + " ");
                        }
                    }
                    c++;
                }
                Console.WriteLine("");
            }
            if (newLine)
            {
                c++;
                Console.WriteLine("");
            }
        } 

        static void ShowVector(int[] vector, bool newLine) // MUESTRA LOS VECTORES
        {
            a++;
            for (int i = 0; i < vector.Length; ++i)
            {
                c++;a++;
                Console.Write(vector[i] + " ");
            }
            c++;
            if (newLine)
            {
                c++;
                Console.WriteLine("\n");
            }
        }

        static void ShowClustered(double[][] data, int[] clustering, int numClusters, int decimals) //MUESTRA LAS DIFERENTES LISTAS AGRUPADAS SEGUN SUS SIMILITUDES
        {
            a++;
            for (int k = 0; k < numClusters; ++k)
            {
                c++;a++;
                Console.WriteLine("===================");

                a++;
                for (int i = 0; i < data.Length; ++i)
                {
                    c++;a++;
                    int clusterID = clustering[i];a++;
                    if (clusterID != k)
                    {
                        c++;
                        continue;
                    }
                    Console.Write(i.ToString().PadLeft(3) + " ");

                    a++;
                    for (int j = 0; j < data[i].Length; ++j)
                    {
                        c++;a++;
                        if (data[i][j] >= 0.0)
                        {
                            c++;
                            Console.Write(" ");
                        }
                        Console.Write(data[i][j].ToString("F" + decimals) + " ");
                    }
                    c++;
                    Console.WriteLine("");
                }
                Console.WriteLine("===================");
            } 
        }
    } 
} 