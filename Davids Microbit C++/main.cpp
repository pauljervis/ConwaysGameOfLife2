#include "MicroBit.h"
#include "cstdint"


// Board size
const uint8_t width = 5;
const uint8_t height = 5;

MicroBit uBit;
MicroBitImage universe(width, height);


// The board is array of booleans (x,y)
typedef bool board[width][height];

bool readCell(uint8_t x, uint8_t y)
{
	return (universe.getPixelValue(x, y) == 255);
}

uint8_t countNeighbours(uint8_t x, uint8_t y)
{
	uint8_t number = 0;

	//Up
	if (y > 0) number += readCell(x, y - 1) ? 1 : 0;
	if (y > 0 && x > 0) number += readCell(x - 1, y - 1) ? 1 : 0;
	if (y > 0 && x < width - 1) number += readCell(x + 1, y - 1) ? 1 : 0;

	//Down
	if (y < height - 1) number += readCell(x, y + 1) ? 1 : 0;
	if (y < height - 1 && x > 0) number += readCell(x - 1, y + 1) ? 1 : 0;
	if (y < height - 1 && x < width - 1) number += readCell(x + 1, y + 1) ? 1 : 0;

	//Left/Right
	if (x > 0) number += readCell(x - 1, y) ? 1 : 0;
	if (x < width - 1) number += readCell(x + 1, y) ? 1 : 0;

	return number;
}

void nextGeneration()
{
	MicroBitImage clone(width, height);
	for (int x = 0; x < width; x++) {
		for (int y = 0; y < height; y++) {
			bool liveCell = readCell(x, y);
			uint8_t numberOfNeighbours = countNeighbours(x, y);

			//Any live cell with fewer than two live neighbours dies, as if caused by underpopulation
			if (liveCell && numberOfNeighbours < 2)
				clone.setPixelValue(x, y, 0);

			//Any live cell with more than three live neighbours dies, as if by overcrowding.
			if (liveCell && numberOfNeighbours > 3)
				clone.setPixelValue(x, y, 0);

			//Any live cell with two or three live neighbours lives on to the next generation.
			if (liveCell && (numberOfNeighbours == 2 || numberOfNeighbours == 3))
				clone.setPixelValue(x, y, 255);

			//Any dead cell with exactly three live neighbours becomes a live cell.
			if (!liveCell && numberOfNeighbours == 3)
				clone.setPixelValue(x, y, 255);
		}
	}

	for (int x = 0; x < width; x++) {
		for (int y = 0; y < height; y++) {
			universe.setPixelValue(x, y, clone.getPixelValue(x, y));
		}
	}


}

void displayBoard()
{
	uBit.display.image.paste(universe);
}

int main()
{
	// Initialise the micro:bit runtime.
	uBit.init();

	// Blinker Period 2
	//universe.setPixelValue(1,1,255);
	//universe.setPixelValue(1,2,255);
	//universe.setPixelValue(1,3,255);

	// Glider
	universe.setPixelValue(0, 2, 255);
	universe.setPixelValue(1, 2, 255);
	universe.setPixelValue(2, 2, 255);
	universe.setPixelValue(2, 1, 255);
	universe.setPixelValue(1, 0, 255);

	while (true)
	{
		displayBoard();
		nextGeneration();
		uBit.sleep(1000);
	}



	// If main exits, there may still be other fibers running or registered event handlers etc.
	// Simply release this fiber, which will mean we enter the scheduler. Worse case, we then
	// sit in the idle task forever, in a power efficient sleep.
	release_fiber();
}

