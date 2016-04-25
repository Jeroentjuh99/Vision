#pragma once

#ifndef CUBE_H_
#define CUBE_H_

#include "Color.h"

class Cube
{
public:
	enum Side { FRONT, BACK, LEFT, RIGHT, TOP, BOTTOM };
	Cube();
	Cube(float);

	RGBColor getColor(Side);
	void setColor(Side, RGBColor);
	void setPosition(int, int);
	void draw(void);
private:
	RGBColor color[6];
	float size;
	float posX, posY = 0;
	void Init(float s);
};


#endif