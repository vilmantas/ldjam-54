using Godot;

namespace Ldjam54.Features.Customer;

public partial class CustomerNavigator : CharacterBody3D
{
    public Vector3 navigationTarget = Vector3.Zero;
    private double maxMovementTimer = 20;
    private double distanceToTargetReached = 0.2;
    private double movementTimer = 0;
    private bool isMoving = false;
    private bool targetReached = false;
    private float customerSpeed = 150f;
    
    public override void _Process(double delta)
    	{
    		if (!targetReached && navigationTarget != Vector3.Zero)
		    {
			    Velocity = Position.DirectionTo(navigationTarget) * customerSpeed * (float)delta;
			    
			    LookAt(navigationTarget);
			    MoveAndSlide();
			    
    			movementTimer += delta;
    			if (movementTimer >= maxMovementTimer)
    			{
    				TeleportToTarget();
    			}
    		}
    
    		if (targetReached == false && navigationTarget != Vector3.Zero)
    		{
    			if (Position.DistanceTo(navigationTarget) < distanceToTargetReached)
    			{
    				OnTargetReached();
    			}
    		}
	    }
    
    	public void NavigateTo(Vector3 target)
	    {
		    if (IsTargetReached(target)) return;
		    
		    navigationTarget = target;
    		targetReached = false;
    		isMoving = true;
    	}
    	
    	private void TeleportToTarget()
    	{
		    GD.Print("TELEPORT");
    		Position = navigationTarget;
		    targetReached = true;
		    isMoving = false;
		    movementTimer = 0;
    	}

	    private void OnTargetReached()
	    {
		    GD.Print("REACHED");
		    targetReached = true;
		    isMoving = false;
		    movementTimer = 0;
	    }

	    private bool IsTargetReached(Vector3 target)
	    {
		    return Position.DistanceTo(target) < distanceToTargetReached;
	    }
}