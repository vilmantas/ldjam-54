using Godot;
using System;

public partial class GameCamera : Node3D
{
	private Camera3D _camera;
	private SpringArm3D _springArm;
	
	private float _zoomSpeed = 0.5f;
	private float _minimumZoom = 1f;
	private float _maximumZoom = 20f;
	
	
	private float _rotationspeed = 0.005f;
	private float _movementSpeed = 0.01f;
	
	
	
	

	private bool _rotating = false;

	private bool _moving = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Camera Ready");
		this._camera = GetNode<Camera3D>("SpringArm/Camera");
		this._springArm = GetNode<SpringArm3D>("SpringArm");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public override void _UnhandledInput(InputEvent unhandledEvent)
	{
		if (unhandledEvent  is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Right)
			{
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
				float targetZoom = _springArm.SpringLength + _zoomSpeed;
				_springArm.SpringLength = targetZoom > _maximumZoom ? _maximumZoom : targetZoom;
			} else if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
			{
				float targetZoom = _springArm.SpringLength - _zoomSpeed;
				_springArm.SpringLength = targetZoom < _minimumZoom ? _minimumZoom : targetZoom;
			}
			
			
		}

		if (unhandledEvent is InputEventMouseMotion mouseMotionEvent)
		{
			if (_rotating)
			{
				float rotateBy = mouseMotionEvent.Relative.X * _rotationspeed;
				Rotate(new Vector3(0, 1, 0), rotateBy);

			}else if (_moving)
			{
				Translate(new Vector3(mouseMotionEvent.Relative.X * -_movementSpeed, 0 , mouseMotionEvent.Relative.Y * -_movementSpeed));
			}
			
		}
	}
}
