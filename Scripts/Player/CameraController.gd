extends Spatial


var mouse_sensitivity = 0.3

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)
	pass

func _input(event):
	if event is InputEventMouseMotion:
		self.rotate_y(deg2rad(-event.relative.x * mouse_sensitivity))
		self.rotate_x(deg2rad(-event.relative.y * mouse_sensitivity))
		self.rotation.x = clamp(self.rotation.x, deg2rad(-89.5), deg2rad(89.5))
