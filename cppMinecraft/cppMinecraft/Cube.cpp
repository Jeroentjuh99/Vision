/*-------------------------------------------------------------------------*/
/*				INCLUDES						                           */
/*-------------------------------------------------------------------------*/
#include <GL/freeglut.h>
#include "Cube.h"


/*-------------------------------------------------------------------------*/
/*              Start Code												   */
/*-------------------------------------------------------------------------*/
Cube::Cube()
{
	Init(2);
}
Cube::Cube(float s)
{
	Init(s);
}

void Cube::Init(float s)
{
	color[0].setRGBColor(0.8f, 0.01f, 0.02f);	// FRONT
	color[1].setRGBColor(0.4f, 0.6f, 0.2f);		// BACK
	color[2].setRGBColor(0.0f, 1.0f, 0.5f);		// LEFT
	color[3].setRGBColor(0.4f, 0.6f, 0.8f);		// RIGHT
	color[4].setRGBColor(1.0f, 0.6f, 0.0f);		// TOP
	color[5].setRGBColor(0.7f, 0.6f, 0.7f);		// BOTTOM
	size = s / 2;
}

RGBColor Cube::getColor(Side cSide)
{
	return color[cSide];
}

void Cube::setColor(Side cSide, RGBColor colorValue)
{
	color[cSide] = colorValue;
}

void Cube::setPosition(int x, int y)
{
	posX = x;
	posY = y;
}


void Cube::draw()
{
		// FRONT
		glBegin(GL_QUADS);
		glColor3f(color[0].getRed(), color[0].getGreen(), color[0].getBlue());
		glVertex3f(size + posX, -size, -size + posY);
		glVertex3f(size + posX, size, -size + posY);
		glVertex3f(-size + posX, size, -size + posY);
		glVertex3f(-size + posX, -size, -size + posY);

		// BACK
		glColor3f(color[1].getRed(), color[1].getGreen(), color[1].getBlue());
		glVertex3f(size + posX, -size, size + posY);
		glVertex3f(size + posX, size, size + posY);
		glVertex3f(-size + posX, size, size + posY);
		glVertex3f(-size + posX, -size, size + posY);

		// LEFT
		glColor3f(color[2].getRed(), color[2].getGreen(), color[2].getBlue());
		glVertex3f(-size + posX, -size, size + posY);
		glVertex3f(-size + posX, size, size + posY);
		glVertex3f(-size + posX, size, -size + posY);
		glVertex3f(-size + posX, -size, -size + posY);

		// RIGHT
		glColor3f(color[3].getRed(), color[3].getGreen(), color[3].getBlue());
		glVertex3f(size + posX, -size, -size + posY);
		glVertex3f(size + posX, size, -size + posY);
		glVertex3f(size + posX, size, size + posY);
		glVertex3f(size + posX, -size, size + posY);

		// TOP
		glColor3f(color[4].getRed(), color[4].getGreen(), color[4].getBlue());
		glVertex3f(size + posX, size, size + posY);
		glVertex3f(size + posX, size, -size + posY);
		glVertex3f(-size + posX, size, -size + posY);
		glVertex3f(-size + posX, size, size + posY);

		// BOTTOM
		glColor3f(color[5].getRed(), color[5].getGreen(), color[5].getBlue());
		glVertex3f(size + posX, -size, -size + posY);
		glVertex3f(size + posX, -size, size + posY);
		glVertex3f(-size + posX, -size, size + posY);
		glVertex3f(-size + posX, -size, -size + posY);
		glEnd();
}

	