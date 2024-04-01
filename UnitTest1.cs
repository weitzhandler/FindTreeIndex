using System.Diagnostics.Metrics;
using Xunit.Sdk;

namespace TestProject1
{
	public class UnitTest1
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

			int index = 0;
			var result = root.FindIndex(_34, ref index);

			Assert.True(result);
			Assert.Equal(34, index);
		}

		[Fact]
		public void Test27()
		{
			var _27 = root[3][0];

			int index = 0;
			var result = root.FindIndex(_27, ref index);

			Assert.True(result);
			Assert.Equal(27, index);
		}

		[Fact]
		public void Test13()
		{
			var _13 = root[1][0][1];

			int index = 0;
			var result = root.FindIndex(_13, ref index);

			Assert.True(result);
			Assert.Equal(13, index);
		}

		[Fact]
		public void Test0()
		{
			var _0 = root;

			int index = 0;
			var result = root.FindIndex(_0, ref index);

			Assert.True(result);
			Assert.Equal(0, index);
		}


	}

	public record Node(int Index, Node[] Children)
	{
		public Node this[int index] => Children[index];

		public bool FindIndex(Node candidate, ref int index)
		{
			if (this == candidate)
			{
				return true;
			}

			foreach (var child in Children)
			{
				index++;

				if (child.FindIndex(candidate, ref index))
				{
					return true;
				}
			}

			return false;
		}
	}
}