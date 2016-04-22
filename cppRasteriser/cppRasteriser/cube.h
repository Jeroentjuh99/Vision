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

	RGBColor getColor(Side cSide);
	void setColor(Side cSide, RGBColor colorValue);
	void draw(void);
private:
	RGBColor color[6];
	float size;
	void Init(float s);
};


#endif