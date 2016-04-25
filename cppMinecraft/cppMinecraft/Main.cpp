/*-------------------------------------------------------------------------*/
/*				INCLUDES						                           */
/*-------------------------------------------------------------------------*/

#include "GL/freeglut.h"
#include "Cube.h"
#include "stb_image.h"

/*-------------------------------------------------------------------------*/
/*				Local Variable                                             */
/*-------------------------------------------------------------------------*/

	float width = 800;
	float height = 600;

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
		glutPostRedisplay();
	}

	void SetupWindow(void)
	{
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
		glClearColor(0.05f, 0.05f, 0.05f, 1.0f);

		glViewport(0, 0, width, height);

		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();
			
		gluPerspective(90, (width/height), 0.1f, 100);

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

	
	void KeyEvent(unsigned char key, int mouseX, int mouseY)
	{

		if (key == 27)
		{
			exit(0);
		}
		
		glutPostRedisplay();
	}

	void MouseMotionEvent(int x, int y)
	{

	}



int main(int argc, char *argv[])
{
	glutInitDisplayMode(GLUT_RGB | GLUT_DOUBLE | GLUT_DEPTH);
	glutInitWindowSize(int(width), int(height));
	glutInit(&argc, argv);
	glutCreateWindow("Minecraft");
		
	glEnable(GL_DEPTH_TEST);

	glutIdleFunc(Idle);
	glutDisplayFunc(PaintComponent);
	glutReshapeFunc(ReshapeWindow);

	glutKeyboardFunc(KeyEvent);
	glutMotionFunc(MouseMotionEvent);

	glutMainLoop();
	return 0;
}








