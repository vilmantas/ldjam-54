extends Node3D
@export var mouse_sensitivity := 0.5
@onready var camera := $Camera as Camera3D
var rotating: bool = false


func _ready():
	top_level = true
	

func _unhandled_input(event: InputEvent) -> void:
	if event is InputEventMouseMotion and rotating:
		rotation_degrees.x -= event.relative.y * mouse_sensitivity
		rotation_degrees.x = clamp(rotation_degrees.x, -90.0, 0)
		
		rotation_degrees.y -= event.relative.x * mouse_sensitivity
		rotation_degrees.y = wrapf(rotation_degrees.y, 0.0, 360.0)
	elif event is InputEventMouseButton:
		if event.button_index == MOUSE_BUTTON_RIGHT:
			if event.pressed:
				Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
				rotating = true
			else:
				Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
				rotating = false
		elif event.button_index == MOUSE_BUTTON_WHEEL_DOWN:
			camera.position.z -= 0.1
		elif event.button_index == MOUSE_BUTTON_WHEEL_UP:
			camera.position.z += 0.1
			


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
