using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davids_Example;

namespace Davids_Example_Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void CheckCountNeighboursReturnsCorrectCounts()
        {
            /*
                Set up a simple test board with the following structure:
                         X....
                         .XXX.
                         ...X.
            */
            var board = new Board(5, 3);
            board.SetCell(0, 0);
            board.SetCell(1, 1);
            board.SetCell(2, 1);
            board.SetCell(3, 1);
            board.SetCell(3, 2);

            // These data tests should be defined elsewhere such
            // as within a csv file.  However for clarity I've
            // written them out in full.
            Assert.AreEqual(1, board.CountNeighbours(0, 0));
            Assert.AreEqual(3, board.CountNeighbours(1, 0));
            Assert.AreEqual(3, board.CountNeighbours(2, 0));
            Assert.AreEqual(2, board.CountNeighbours(3, 0));
            Assert.AreEqual(1, board.CountNeighbours(4, 0));

            Assert.AreEqual(2, board.CountNeighbours(0, 1));
            Assert.AreEqual(2, board.CountNeighbours(1, 1));
            Assert.AreEqual(3, board.CountNeighbours(2, 1));
            Assert.AreEqual(2, board.CountNeighbours(3, 1));
            Assert.AreEqual(2, board.CountNeighbours(4, 1));

            Assert.AreEqual(1, board.CountNeighbours(0, 2));
            Assert.AreEqual(2, board.CountNeighbours(1, 2));
            Assert.AreEqual(4, board.CountNeighbours(2, 2));
            Assert.AreEqual(2, board.CountNeighbours(3, 2));
            Assert.AreEqual(2, board.CountNeighbours(4, 2));
        }

        [TestMethod]
        public void CheckPeriod2Pattern()
        {
            //Arrange
            /*
            Set up a simple test board with the following structure:
             .....
             .XXX.
             .....
            */
            var board = new Board(5, 3);
            board.SetCell(1, 1);
            board.SetCell(2, 1);
            board.SetCell(3, 1);

            /* We expect the next generation to look like this
                 ..X..
                 ..X..
                 ..X..
            */
            var expectedResult = new Board(board.Width, board.Height);
            expectedResult.SetCell(2, 0);
            expectedResult.SetCell(2, 1);
            expectedResult.SetCell(2, 2);

            //Act
            var actualResult = board.NextGeneration();

            //Assert
            CompareBoards(expectedResult, actualResult);
        }

        /// <summary>
        /// Checks that two boards are equal
        /// </summary>
        /// <param name="expectedResult"></param>
        /// <param name="actualResult"></param>
        private static void CompareBoards(Board expectedResult, Board actualResult)
        {
            // Check dimensions
            Assert.AreEqual(expectedResult.Width, actualResult.Width);
            Assert.AreEqual(expectedResult.Height, actualResult.Height);

            // Check cell by cell
            for (int x = 0; x < expectedResult.Width; x++)
            {
                for (int y = 0; y < expectedResult.Height; y++)
                {
                    Assert.AreEqual(expectedResult.GetCell(x, y), actualResult.GetCell(x, y));
                }
            }
        }
    }
}
