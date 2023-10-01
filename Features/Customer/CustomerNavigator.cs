using Godot;

namespace Ldjam54.Features.Customer;

public partial class CustomerNavigator : CharacterBody3D
{
    public Vector3 navigationTarget = Vector3.Zero;
    private double maxMovementTimer = 10;
    private double distanceToTargetReached = 1;
    private double movementTimer = 0;
    private bool isMoving = false;
    private bool targetReached = false;
    private float customerSpeed = 400f;
    
    public override void _Process(double delta)
    	{
    		if (!targetReached && navigationTarget != Vector3.Zero)
		    {
			    this.Velocity = Position.DirectionTo(navigationTarget) * customerSpeed * (float)delta;
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
		    navigationTarget = target;
    		targetReached = false;
    		isMoving = true;
    	}
    	
    	private void TeleportToTarget()
    	{
    		Position = navigationTarget;
		    targetReached = true;
		    isMoving = false;
		    movementTimer = 0;
    	}

	    private void OnTargetReached()
	    {
		    targetReached = true;
		    isMoving = false;
		    movementTimer = 0;
		    
	    }
	    
}