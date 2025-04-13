namespace Dijkstra {
	internal class Program {
		static void Main(string[] args) {			
			DijkstraAlgorithm();
		}

		static void DijkstraAlgorithm() {
			var graph = new Dictionary<string, List<(string neighbor, int weight)>>() {
				{ "A", new List<(string, int)> { ("B", 1), ("D", 4) } },
				{ "B", new List<(string, int)> { ("A", 1), ("C", 2), ("D", 3) } },
				{ "C", new List<(string, int)> { ("B", 2) } },
				{ "D", new List<(string, int)> { ("A", 4), ("B", 3) } }
			};

			Console.WriteLine("Grafo:");
			foreach (var node in graph) {
				Console.Write($"{node.Key} - ");
				foreach (var (neighbor, weight) in node.Value) {
					Console.Write($"{neighbor}({weight}) ");
				}
				Console.WriteLine();
			}

			var start = "C";
			var distances = Dijkstra(graph, start);

			Console.WriteLine($"Distâncias a partir do nó {start}:");
			foreach (var kvp in distances) {
				Console.WriteLine($"{kvp.Key}: {kvp.Value}");
			}
		}

		static Dictionary<string, int> Dijkstra(Dictionary<string, List<(string, int)>> graph, string start) {
			var distances = new Dictionary<string, int>();
			var visited = new HashSet<string>();
			var priorityQueue = new SortedSet<(int distance, string node)>();

			foreach (var node in graph.Keys) {
				distances[node] = int.MaxValue;
			}

			distances[start] = 0;
			priorityQueue.Add((0, start));

			while (priorityQueue.Count > 0) {
				var (currentDistance, currentNode) = GetMin(priorityQueue);
				priorityQueue.Remove((currentDistance, currentNode));

				if (visited.Contains(currentNode))
					continue;

				visited.Add(currentNode);

				foreach (var (neighbor, weight) in graph[currentNode]) {
					var distance = currentDistance + weight;
					if (distance < distances[neighbor]) {
						distances[neighbor] = distance;
						priorityQueue.Add((distance, neighbor));
					}
				}
			}

			return distances;
		}

		// Helper para evitar problema de SortedSet com tuplas iguais
		static (int, string) GetMin(SortedSet<(int, string)> set) {
			foreach (var item in set)
				return item;
			return (0, "");
		}
	}
}
