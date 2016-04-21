/*-------------------------------------------------------------------------*/
/*				INCLUDES						                           */
/*-------------------------------------------------------------------------*/

#include "GL/freeglut.h"
#include "Cube.h"

/*-------------------------------------------------------------------------*/
/*				Local Variable                                             */
/*-------------------------------------------------------------------------*/

	float width = 800;
	float height = 600;
	float rotateX, rotateY, rotateZ = 0.0f;
	double trX, trY = 0;
	bool perspectiveFlag = true;
	Cube c1, c2, c3, c4;

/*-------------------------------------------------------------------------*/
/*				Function Prototyping                                       */
/*-------------------------------------------------------------------------*/

	void Idle(void);
	void SetupWindow(void);
	void PaintComponent(void);
	
	void ReshapeWindow(int, int);

	void KeyEvent(unsigned char, int, int);
	void MouseMotionEvent(int, int);


/*-------------------------------------------------------------------------*/
/*              Start Code												   */
/*-------------------------------------------------------------------------*/

/*-------------------------------------------------------------------------*/
/*              -- Window Functions										   */
/*-------------------------------------------------------------------------*/

	
	void Idle(void)
	{
		rotateY += 0.01f;
		rotateX += 0.01f;
		rotateZ += 0.01f;
		glutPostRedisplay();
	}

	void SetupWindow(void)
	{
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		glClearColor(0.05f, 0.05f, 0.05f, 1.0f);

		glViewport(0, 0, width, height);

		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();

		if (perspectiveFlag == true)
		{
			gluPerspective(90, (width/height), 0.1f, 100);
		}
		else
		{
			glOrtho(-3.0f*(width/height), 3.0f*(width/height), -3.0f, 3.0f, 0.1f, 100);
		}

		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();

		gluLookAt(0, 0, -4,
			0, 0, 0,
			0, 1, 0);
	}

	void PaintComponent(void)
	{
		// Reset window
		SetupWindow();
		
		// Cube 1
		glPushMatrix();
			glTranslated(-3, 0, 0);
			glRotatef(rotateX, 1.0f, 0.0f, 0.0f);
			c1.draw();
		glPopMatrix();

		// Cube 2
		glPushMatrix();
			glTranslated(0, 0, 0);
			glRotatef(rotateY, 0.0f, 1.0f, 0.0f);
			c2.draw();
		glPopMatrix();

		// Cube 3
		glPushMatrix();
			glTranslated(3, 0, 0);
			glRotatef(rotateZ, 0.0, 0.0, 1.0);
			c3.draw();
		glPopMatrix();

		// Cube 4
		glPushMatrix();
			glTranslated(trX, trY, 4);
			glRotatef(rotateZ, 0.0, 0.0, 1.0);
			glRotatef(rotateY, 0.0, 1.0, 0.0);
			glRotatef(rotateX, 1.0, 0.0, 0.0);
			c4.draw();
		glPopMatrix();

		glFlush();
		glutSwapBuffers();
	}
	
	void ReshapeWindow(int w, int h)
	{
		width = w;
		height = h;
	}


/*-------------------------------------------------------------------------*/
/*              -- Event Handlers										   */
/*-------------------------------------------------------------------------*/

	void SpecialKeyEvent(int key, int mouseX, int mouseY)
	{

		switch (key)
		{
		case GLUT_KEY_UP:
			trY += 0.1;
			break;
		case GLUT_KEY_DOWN:
			trY -= 0.1;
			break;
		case GLUT_KEY_LEFT:
			trX += 0.1;
			break;
		case GLUT_KEY_RIGHT:
			trX -= 0.1;
			break;
		case GLUT_KEY_F1:
			glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
			break;
		case GLUT_KEY_F2:
			glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
			break;
		case GLUT_KEY_F3:
			perspectiveFlag = !perspectiveFlag;
			break;
		}

		glutPostRedisplay();
	}

	void KeyEvent(unsigned char key, int mouseX, int mouseY)
	{

		if (key == 27)
		{
			exit(0);
		}
		else if (key == 32)
		{
			perspectiveFlag = !perspectiveFlag;
		}

		glutPostRedisplay();
	}

	void MouseMotionEvent(int x, int y)
	{
		c1.setColor(Cube::FRONT, RGBColor((x / width), (y / height), (x / width - y / height)));
		c1.setColor(Cube::TOP, RGBColor((x / width), (y / height), (x / width - y / height)));
		c1.setColor(Cube::BOTTOM, RGBColor((x / width), (y / height), (x / width - y / height)));
	}



int main(int argc, char *argv[])
{
	glutInitDisplayMode(GLUT_RGB | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowSize((int)width, (int)height);
	glutInit(&argc, argv);
	glutCreateWindow("3D Objects");
		
	glEnable(GL_DEPTH_TEST);

	glutIdleFunc(Idle);
	glutDisplayFunc(PaintComponent);
	glutReshapeFunc(ReshapeWindow);

	glutSpecialFunc(SpecialKeyEvent);
	glutKeyboardFunc(KeyEvent);
	glutMotionFunc(MouseMotionEvent);

	c4.setColor(Cube::FRONT,RGBColor(255,0,255));

	glutMainLoop();
	return 0;
}








