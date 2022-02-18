extends KinematicBody

var speed = 20
var acceleration = 6
var normal_acceleration = 6
var gravity = 40

var jump = 20
var double_jumped = false
var air_acceleration = 1

var mouse_sensitivity = 0.3

var direction = Vector3()
var h_velocity = Vector3()
var movement = Vector3()
var gravity_vec = Vector3()

onready var camera = $Head/Camera
onready var head = $Head

func _ready():
	Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _input(event):
	if event is InputEventMouseMotion:
		rotate_y(deg2rad(-event.relative.x * mouse_sensitivity))
		head.rotate_x(deg2rad(-event.relative.y * mouse_sensitivity))
		head.rotation.x = clamp(head.rotation.x, deg2rad(-89.5), deg2rad(89.5))

func _physics_process(delta):
	direction = Vector3()
	
	if not is_on_floor():
		gravity_vec += Vector3.DOWN * gravity * delta
		acceleration = air_acceleration
	elif is_on_floor():
		gravity_vec = -get_floor_normal() * gravity
		acceleration = normal_acceleration
		double_jumped = false
		
	if Input.is_action_just_pressed("jump") and is_on_floor():
		gravity_vec = Vector3.UP * jump
		double_jumped = false
	elif Input.is_action_just_pressed("jump") and not is_on_floor() and !double_jumped:
		gravity_vec = Vector3.UP * jump
		double_jumped = true
		
	if Input.is_action_pressed("forward"):
		direction -= transform.basis.z
		if Input.is_action_pressed("sprint") and is_on_floor():
			speed = 40
			camera.fov = 75
		if Input.is_action_just_released("sprint"):
			speed = 20
			camera.fov = 70
	elif Input.is_action_pressed("backward"):
		direction += transform.basis.z
		
	if Input.is_action_pressed("left"):
		direction -= transform.basis.x
	elif Input.is_action_pressed("right"):
		direction += transform.basis.x
		
	h_velocity = h_velocity.linear_interpolate(direction.normalized() * speed, acceleration * delta)
	movement.z = h_velocity.z + gravity_vec.z
	movement.x = h_velocity.x + gravity_vec.x
	movement.y = gravity_vec.y
	move_and_slide(movement, Vector3.UP)
