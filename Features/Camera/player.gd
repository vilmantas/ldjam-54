extends Node3D
class_name Player
var can_move = true

@onready var _camera_arm: Node3D = $CameraArm
@onready var _model_container: Node3D = $ModelContainer

func _ready():
	pass # Replace with function body.


func _process(delta):
	handle_input()
	_camera_arm.position = position
	

func handle_input():
	if can_move:
		if  Input.is_action_pressed("Forward"):
			smooth_move(Vector3(position.x + 1, position.y, position.z))
			can_move = false
		elif Input.is_action_pressed("Backwards"):
			smooth_move(Vector3(position.x - 1, position.y, position.z))
			can_move = false
		elif Input.is_action_pressed("Left"):
			smooth_move(Vector3(position.x, position.y, position.z - 1))
			can_move = false
		elif Input.is_action_pressed("Right"):
			smooth_move(Vector3(position.x, position.y, position.z + 1))
			can_move = false

func smooth_move(new_position: Vector3):
	var movement_tween: Tween = create_tween()
	movement_tween.tween_property(self, 'position',new_position, Settings.animation_speed)
	var hop_tween: Tween = create_tween()
	
	hop_tween.tween_property(_model_container, "position", Vector3(_model_container.position.x, 0.5, _model_container.position.z), Settings.animation_speed / 2)
	hop_tween.tween_property(_model_container, "position", Vector3(_model_container.position.x, 0, _model_container.position.z), Settings.animation_speed / 2)
	

func _on_timer_timeout():
	can_move = true
