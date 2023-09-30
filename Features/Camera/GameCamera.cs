using Godot;
using System;

public partial class GameCamera : Node3D
{
	private Camera3D _camera;
	private SpringArm3D _springArm;
	private float _zoomSpeed = 0.5f;
	private float _rotationspeed = 0.05f;

	private bool _rotating = false;

	private bool _moving = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this._camera = GetNode<Camera3D>("SpringArm/Camera");
		this._springArm = GetNode<SpringArm3D>("SpringArm");
		GD.Print(_springArm);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _UnhandledInput(InputEvent unhandledEvent)
	{
		// GD.Print(unhandledEvent);
		if (unhandledEvent  is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Right)
			{
				GD.Print("Right click");
				if (mouseEvent.IsPressed())
				{
					_rotating = true;
				}
				else
				{
					_rotating = false;
				}
			} else if (mouseEvent.ButtonIndex == MouseButton.Middle)
			{
				if (mouseEvent.IsPressed())
				{
					_moving = true;
				}
				else
				{
					_moving = false;
				}
			} else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
			{
				_springArm.SpringLength += _zoomSpeed;
			} else if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
			{
				_springArm.SpringLength -= _zoomSpeed;
			}
			GD.Print(_springArm.SpringLength);
			
			
		}

		if (unhandledEvent is InputEventMouseMotion mouseMotionEvent)
		{
			if (_rotating)
			{
				float rotateBy = mouseMotionEvent.Relative.X * _rotationspeed;
				Rotate(new Vector3(0, 1, 0), rotateBy);

			}else if (_moving)
			{
				Translate(new Vector3(mouseMotionEvent.Relative.X * -0.1f, 0 , mouseMotionEvent.Relative.Y * -0.1f));
	
			}
			
		}
	}
}
