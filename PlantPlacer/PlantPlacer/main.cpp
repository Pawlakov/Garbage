#include <SFML/Graphics.hpp>
using namespace sf;

enum Species
{

};

class Plant
{
	int x;
	int y;
	Species species;

	Plant()
	{
		//
	}
	void draw()
	{
		//
	}
};

int main()
{
	RenderWindow window(VideoMode(200, 200), "SFML works!");
	Vector2u windowSize;
	Vector2u origin;
	unsigned int scale;
	CircleShape shape(100.f);

	shape.setFillColor(Color::Green);
	while (window.isOpen())
	{
		Event event;
		while (window.pollEvent(event))
		{
			if (event.type == Event::Closed)
				window.close();
		}

		window.clear();
		window.draw(shape);
		window.display();
	}

	return 0;
}