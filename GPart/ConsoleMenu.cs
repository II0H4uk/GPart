using System;

namespace GPart
{
    class ConsoleMenu
    {
        static string defaultPath = @"..\..\..\Graphs\Graph-1.txt";

        public static void ConsoleMenuStart()
        {
            Console.WriteLine("Начало работы меню.");

            Console.WriteLine("Алгоритмы для какой задачи использовать?");
            Console.WriteLine("1.2-разбиение графа.");
            Console.WriteLine("2.k-разбиение графа.");
            Console.WriteLine("3.Собрать статистику.");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Menu2PartAlgStart(MenuGetGraph());
                    break;
                case "2":
                    MenukPartAlgStart(MenuGetGraph(true));
                    break;
                case "3":
                    MenuGetStatistic();
                    break;
                default:
                    ConsoleMenuStart();
                    break;
            }
        }

        private static void MenuGetStatistic()
        {
            Console.WriteLine("Сколько вершин будет у графов?");

            int n = int.Parse(Console.ReadLine().Trim());

            Console.WriteLine("Сколько Циклов проводить?");

            int cycleCount = int.Parse(Console.ReadLine().Trim());

            Console.WriteLine("Какую статистику собрать?");
            Console.WriteLine("1.Среднее время работы на случайных графах с заданным количеством вершин и граней");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    ShowResult.X(TimeComplexityAnalyzer.BenchmarkAlgsByRandomGraph(GetAllAlgs.Get2PartAlgs(), n, cycleCount, 7));
                    break;
                case "2":

                    break;
                default:
                    MenuGetStatistic();
                    break;
            }
        }

        private static void Menu2PartAlgStart(int[][] graph)
        {
            Console.WriteLine("Какие алгоритмы запускать?");
            Console.WriteLine("1.Первый.");
            Console.WriteLine("2.Второй.");
            Console.WriteLine("3.Третий.");
            Console.WriteLine("4.Четвертый.");
            Console.WriteLine("5.Пятый.");
            Console.WriteLine("6.Шестой.");
            Console.WriteLine("7.Седьмой.");
            Console.WriteLine("8.Восьмой.");
            Console.WriteLine("9.Девятый.");
            Console.WriteLine("10.Десятый.");
            Console.WriteLine("11.Все.");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Benchmark.RunSingleAlg(new Alg1(), graph, "1");
                    break;
                case "2":
                    Benchmark.RunSingleAlg(new Alg2(), graph, "2");
                    break;
                case "3":
                    Benchmark.RunSingleAlg(new Alg3(), graph, "3");
                    break;
                case "4":
                    Benchmark.RunSingleAlg(new Alg4(), graph, "4");
                    break;
                case "5":
                    Benchmark.RunSingleAlg(new Alg5(), graph, "5");
                    break;
                case "6":
                    Benchmark.RunSingleAlg(new Alg6(), graph, "6");
                    break;
                case "7":
                    Benchmark.RunSingleAlg(new Alg7(), graph, "7");
                    break;
                case "8":
                    Benchmark.RunSingleAlg(new Alg8(), graph, "8");
                    break;
                case "9":
                    Benchmark.RunSingleAlg(new Alg9(), graph, "9");
                    break;
                case "10":
                    Benchmark.RunSingleAlg(new Alg10(), graph, "10");
                    break;
                case "11":
                    Benchmark.RunAllAlgs(GetAllAlgs.Get2PartAlgs(), graph);
                    break;
                default:
                    Menu2PartAlgStart(graph);
                    break;
            }
        }

        private static int[][] MenuGetGraph(bool iskPart = false)
        {
            Console.WriteLine("Как получить граф?");
            Console.WriteLine("1.Сгенерировать.");
            Console.WriteLine("2.Считать из файла.");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    int[][] graph;
                    if (iskPart)
                    {
                        graph = GraphGenerator.GenerateWeightForkPart(MenuGenerateGraph());
                        MenuSaveGraph(graph, iskPart);
                        return graph;
                    }
                    graph = MenuGenerateGraph();
                    MenuSaveGraph(graph, iskPart);
                    return graph;
                case "2":
                    return iskPart ? MenuReadGraphFromFile() : ConvertGraph.AdjMatrixToList(MenuReadGraphFromFile());
                default:
                    return MenuGetGraph(iskPart);
            }
        }

        private static int[][] MenuGenerateGraph()
        {
            Console.WriteLine("Сколько вершин будет у графа?");
            int n = int.Parse(Console.ReadLine().Trim());

            Console.WriteLine("Сколько граней будет у графа?");
            int m = int.Parse(Console.ReadLine().Trim());

            Console.WriteLine("Какой граф генерировать?");
            Console.WriteLine("1.Планарный граф.");
            Console.WriteLine("2.Не планарный граф.");

            int[][] graph;

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    graph = PlanarGraphGenerator.GenerateGraph(n, m);
                    break;
                case "2":
                    graph = GraphGenerator.GenerateAdjList(n, m);
                    break;
                default:
                    Console.WriteLine("Ошибка ввода.");
                    return MenuGenerateGraph();
            }

            return graph;
        }

        private static void MenuSaveGraph(int[][] graph, bool iskGraph = false)
        {
            int[][] graphCopy = iskGraph ? Utilities.CopyMat(graph) : ConvertGraph.AdjListToMatrix(graph);

            Console.WriteLine("1.Вывести граф на экран.");
            Console.WriteLine("2.Сохранить граф в файл.");
            Console.WriteLine("3.Продолжить.");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    ShowResult.Graph(graphCopy);
                    break;
                case "2":
                    Console.WriteLine("Введите путь до файла сохранения.");
                    Utilities.WriteFile(Utilities.WriteData(graphCopy), Console.ReadLine().Trim());
                    break;
                case "3":
                    return;
                default:
                    break;
            }

            MenuSaveGraph(graph, iskGraph);
        }

        private static int[][] MenuReadGraphFromFile()
        {
            Console.WriteLine("Как считать граф?");
            Console.WriteLine("1.Использовать путь по умолчанию.");
            Console.WriteLine("2.Задать путь до файла чтения.");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    return GraphRead.AdjMatFromFile(defaultPath);
                case "2":
                    return GraphRead.AdjMatFromFile(Console.ReadLine().Trim());
                default:
                    return MenuReadGraphFromFile();
            }
        }

        private static void MenukPartAlgStart(int[][] graph)
        {
            Console.WriteLine("На сколько подграфов разбивать?");
            int k = int.Parse(Console.ReadLine().Trim());

            Console.WriteLine("Сколько повторений алгоритма делать?");
            int repeats = int.Parse(Console.ReadLine().Trim());

            Console.WriteLine("Какие алгоритмы запускать?");
            Console.WriteLine("1.Полный перебор.");
            Console.WriteLine("2.Первый эвристический.");
            Console.WriteLine("3.Второй эвристический.");
            Console.WriteLine("4.Все.");

            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Benchmark.RunSingleAlg(new KPartBruteForce(), graph, "полного перебора", k);
                    break;
                case "2":
                    Benchmark.RunSingleAlg(new KPartHeuristic(), graph, "первого эвристического", k, repeats);
                    break;
                case "3":

                    break;
                case "4":
                    Benchmark.RunAllAlgs(GetAllAlgs.GetkPartAlgs(), graph, k, 0, repeats);
                    break;
                default:
                    MenukPartAlgStart(graph);
                    break;
            }
        }
    }
}
