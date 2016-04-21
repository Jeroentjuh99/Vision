#include "GL/freeglut.h"
#include "cube.h"
#include "main.h"

void Display(void);
void Keyboard(unsigned char, int, int);
void Idle(void);
void reshape(int, int);
void mouseEvent(int, int);

float width = 800;
float height = 600;
double rotateX, rotateY, rotateZ = 0;
double r, g, b = 70;


int main(int argc, char *argv[])
{
	glutInitDisplayMode(GLUT_RGB | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowSize((int)width, (int)height);
	glutInit(&argc, argv);
	glutCreateWindow("hurrr blurrr durr");
	glEnable(GL_DEPTH_TEST);
	glutDisplayFunc(Display);
	glutKeyboardFunc(Keyboard);
	glutReshapeFunc(reshape);
	glutIdleFunc(Idle);
	glutMotionFunc(mouseEvent);
	glutMainLoop();
	return 0;
}

void Display(void)
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glClearColor(0.45f, 0.45f, 0.45f, 0.7f);

	glViewport(0, 0, width, height);

	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(90, (width/height), 0.1f, 100);

	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();

	gluLookAt(0, 0, -4,
		0, 0, 0,
		0, 1, 0);

	glPushMatrix();
	glTranslated(-3, 0, 0);
	glRotatef(rotateX, 1.0, 0.0, 0.0);
	draw();
	glPopMatrix();
	
	glPushMatrix();
	glTranslated(0, 0, 0);
	glRotatef(rotateY, 0.0, 1.0, 0.0);
	draw();
	glPopMatrix();
	
	glPushMatrix();
	glTranslated(3, 0, 0);
	glRotatef(rotateZ, 0.0, 0.0, 1.0);
	draw();
	glPopMatrix();

	glPushMatrix();
	glTranslated(0, 0, 4);
	glScaled(2.5, 2.5, 2.5);
//	glRotatef(rotateZ, 0.0, 0.0, 1.0);
	draw(r,g,b);
	glPopMatrix();

	
	glFlush();

	glutSwapBuffers();
}

void Idle()
{
	rotateY += 0.05;
	rotateX += 0.05;
	rotateZ += 0.05;
	glutPostRedisplay();
}

void Keyboard(unsigned char key, int mouseX, int mouseY)
{
	if(key == 27)
	{
		exit(0);
	} 

	//  Request display update
	glutPostRedisplay();
}

void reshape(int w, int h)
{
	width = w;
	height = h;
}

void mouseEvent(int x, int y)
{
	r = x;
	g = y;
	b = x - y;
}