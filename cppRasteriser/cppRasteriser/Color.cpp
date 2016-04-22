#include "Color.h"

RGBColor::RGBColor()
{
	RGBColor(0, 0, 0);
}
RGBColor::RGBColor(float r, float g, float b)
{
	setRGBColor(r, g, b);
}

void RGBColor::setRGBColor(float r, float g, float b)
{
	red = r;
	green = g;
	blue = b;
}

float RGBColor::getRed() const
{
	return red;
}
float RGBColor::getGreen() const
{
	return green;
}
float RGBColor::getBlue() const
{
	return blue;
}
