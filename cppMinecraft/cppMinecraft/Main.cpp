/*-------------------------------------------------------------------------*/
/*				INCLUDES						                           */
/*-------------------------------------------------------------------------*/

#define _USE_MATH_DEFINES
#include "GL/freeglut.h"
#include "Cube.h"
#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"
#include <vector>
#include <cstdio>
#include <cmath>

/*-------------------------------------------------------------------------*/
/*				Local Variable                                             */
/*-------------------------------------------------------------------------*/

	float width = 800;
	float height = 600;
	float lastFrameTime = 0;
	bool keys[255];

	std::vector<Cube> cubeList;

	struct Camera
	{
		float posX = 0;
		float posY = -4;
		float rotX = 0;
		float rotY = 0;
	} camera;

/*-------------------------------------------------------------------------*/
/*				Function Prototyping                                       */
/*-------------------------------------------------------------------------*/

	void Idle(void);
	void SetupWindow(void);
	void PaintComponent(void);
	
	void ReshapeWindow(int, int);

	void KeyEvent(unsigned char, int, int);
	void MouseMotionEvent(int, int);
	void move(float, float);
	void KeyEventUp(unsigned char, int, int);

	void Generate_Cubes(void);
/*-------------------------------------------------------------------------*/
/*              Start Code												   */
/*-------------------------------------------------------------------------*/

/*-------------------------------------------------------------------------*/
/*              -- Window Functions										   */
/*-------------------------------------------------------------------------*/

	
	void Idle(void)
	{
		float frameTime = glutGet(GLUT_ELAPSED_TIME) / 1000.0f;
		float deltaTime = frameTime - lastFrameTime;
		lastFrameTime = frameTime;

		const float speed = 3;
		if (keys['a']) move(0, deltaTime*speed);
		if (keys['d']) move(180, deltaTime*speed);
		if (keys['w']) move(90, deltaTime*speed);
		if (keys['s']) move(270, deltaTime*speed);

		glutPostRedisplay();
	}

	void SetupWindow(void)
	{
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		glClearColor(0.05f, 0.05f, 0.05f, 1.0f);

		glViewport(0, 0, width, height);

		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();	
		gluPerspective(60.0f, (width/height), 0.1f, 30);

		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();

		glRotatef(camera.rotX, 1, 0, 0);
		glRotatef(camera.rotY, 0, 1, 0);
		glTranslatef(camera.posX, 0, camera.posY);
	}

	void PaintComponent(void)
	{
		// Reset window
		SetupWindow();

		//*Ground
		glColor3f(0.1f, 1.0f, 0.2f);
		glBegin(GL_QUADS);
		glVertex3f(-15, -1, -15);
		glVertex3f(15, -1, -15);
		glVertex3f(15, -1, 15);
		glVertex3f(-15, -1, 15);
		glEnd();
		
		for (std::vector<Cube>::iterator it = cubeList.begin(); it != cubeList.end(); ++it) 
		{
			it->draw();
		}
				
		glFlush();
		glutSwapBuffers();
	}
	
	void ReshapeWindow(int w, int h)
	{
		width = w;
		height = h;
	}

	void move(float angle, float fac)
	{
		camera.posX += (float)cos((camera.rotY + angle) / 180 * M_PI) * fac;
		camera.posY += (float)sin((camera.rotY + angle) / 180 * M_PI) * fac;
	}

	void Generate_Cubes(void)
	{
		for (int x = -10; x <= 10; x += 5)
		{
			for (int y = -10; y <= 10; y += 5)
			{
					Cube newCube;
					newCube.setPosition(x, y);
					cubeList.push_back(newCube);
			}
		}
	}

/*-------------------------------------------------------------------------*/
/*              -- Event Handlers										   */
/*-------------------------------------------------------------------------*/

	
	void KeyEvent(unsigned char key, int mouseX, int mouseY)
	{
		if (key == 27)
			exit(0);
		keys[key] = true;
	}

	void MouseMotionEvent(int x, int y)
	{
		int dx = x - width / 2;
		int dy = y - height / 2;
		if ((dx != 0 || dy != 0) && abs(dx) < 400 && abs(dy) < 400)
		{
			camera.rotY += dx / 10.0f;
			camera.rotX += dy / 10.0f;
			glutWarpPointer(width / 2, height / 2);
		}
	}

	void KeyEventUp(unsigned char key, int, int)
	{
		keys[key] = false;
	}

int main(int argc, char *argv[])
{
	glutInitDisplayMode(GLUT_RGB | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowSize(int(width), int(height));
	glutInit(&argc, argv);
	glutCreateWindow("Minecraft");

	memset(keys, 0, sizeof(keys));
	glEnable(GL_DEPTH_TEST);

	glutIdleFunc(Idle);
	glutDisplayFunc(PaintComponent);
	glutReshapeFunc(ReshapeWindow);
	glutKeyboardFunc(KeyEvent);
	glutKeyboardUpFunc(KeyEventUp);
	glutPassiveMotionFunc(MouseMotionEvent);
	glutWarpPointer(width / 2, height / 2);

	Generate_Cubes();
	
	glutMainLoop();
	return 0;
}








