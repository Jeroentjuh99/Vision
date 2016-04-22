#pragma once

#ifndef COLOR_H_
#define COLOR_H_

#include <string>

class RGBColor
{
public:
	RGBColor();
	RGBColor(float r, float g, float b);
	void setRGBColor(float r, float g, float b);

	float getRed() const;
	float getGreen() const;
	float getBlue() const;
private:
	float red;
	float green;
	float blue;
};

#endif