using System.Collections.Immutable;

namespace TreeIndex;

public class TreeIndexTests
{
	static readonly Node root =
		new Node(
			0, [
				new(1, [
					new(2, [
						new(3, []),
						new(4, []),
						new(5, [])
						]),
					new(6, [
						new(7, []),
						new(8, []),
						new(9, [])
						])
					]),
				new(10, [
					new(11, [
						new(12, []),
						new(13, []),
						new(14, [])
						])
					]),
				new(15, [
					new(16, [
						new(17, []),
						new(18, []),
						new(19, [
							new (20, []),
							new (21, []),
						])
						]),
					new(22, [
						new(23, []),
						new(24, []),
						new(25, [])
						])
					]),
				new(26, [
					new(27, [
						new(28, []),
						new(29, [
							new(30, []),
							]),
						new(31, [])
						]),
					new(32, [
						new(33, []),
						new(34, []),
						new(35, [])
						])
					])
				]);

	[Fact]
	public void Test34()
	{
		var _34 = root[3][1][1];

		var result = root.FindTreeIndex(_34);

		Assert.Equal(34, result);
	}

	[Fact]
	public void Test27()
	{
		var _27 = root[3][0];

		var result = root.FindTreeIndex(_27);

		Assert.Equal(27, result);
	}

	[Fact]
	public void Test13()
	{
		var _13 = root[1][0][1];

		var result = root.FindTreeIndex(_13);

		Assert.Equal(13, result);
	}

	[Fact]
	public void Test0()
	{
		var _0 = root;

		var result = root.FindTreeIndex(_0);

		Assert.Equal(0, result);
	}

	[Fact]
	public void Test0_27_34()
	{
		var _0 = root;
		var _27 = root[3][0];
		var _34 = root[3][1][1];

		var result = root.FindIndices([_0, _27, _34]);

		Assert.Equal([0, 27, 34], result);
	}

	[Fact]
	public void Test34_27_0()
	{
		var _0 = root;
		var _27 = root[3][0];
		var _34 = root[3][1][1];

		var result = root.FindIndices([_0, _27, _34]);

		Assert.Equal([0, 27, 34], result);
	}

	[Fact]
	public void TestNone()
	{
		var __ = new Node(77, []);

		var result = root.FindTreeIndex(__);

		Assert.Equal(-1, result);
	}
}

public record Node(int Index, Node[] Children)
{
	public Node this[int index] => Children[index];

	public int FindTreeIndex(Node candidate)
	{
		var results = FindIndices([candidate]);
		return results.FirstOrDefault(-1);
	}

	public int[] FindIndices(Node[] candidates)
	{
		var index = 0;
		var result = FindIndices(this, candidates, ref index);

		return result.Values.ToArray();
	}

	private IDictionary<Node, int> FindIndices(Node root, Node[] candidates, ref int currentIndex, IDictionary<Node, int>? previousResults = null)
	{
		previousResults ??= ImmutableDictionary<Node, int>.Empty;

		if (!candidates.Any())
		{
			return previousResults;
		}

		IDictionary<Node, int> returnedResults = new Dictionary<Node, int>(previousResults);
		if (candidates.Contains(root))
		{
			returnedResults[root] = currentIndex;
			candidates = candidates.Except([root]).ToArray();
		}

		foreach (var child in root.Children)
		{
			currentIndex++;
			returnedResults = FindIndices(child, candidates, ref currentIndex, returnedResults);
		}

		return returnedResults;
	}

	public int FindTreeIndex_(Node candidate)
	{
		(bool Found, int Index) FindTreeIndexImpl(Node tree)
		{
			var index = 0;

			if (this == candidate)
			{
				return (true, index);
			}

			foreach (var child in Children)
			{
				index++;

				var result = FindTreeIndexImpl(child);
				index += result.Index;

				if (result.Found)
				{
					return (true, index);
				}
			}

			return (false, index);
		}

		var result = FindTreeIndexImpl(candidate);
		return result.Found ? result.Index : -1;
	}
}