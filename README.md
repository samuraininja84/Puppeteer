# Puppeteer - A Framework For Taking Control From The Player

- Purpose:
	- Allows the user to dynamically set up where a character's input can come from.
	- Example:
 		- Overriding the movement direction of a character with another source, like an AI Controller.
   		- Real-time action cutscene that makes the player jump, shoot, or dodge.

- Based on this video by aartificial: https://www.youtube.com/watch?v=pOEyYwKtHJo
   
# Editor Set-Up:
- On your Player Script:
	- Add a [SerializeField] Private / Public Puppet to your Script.
	- Add a [SerializeField] Private / Public Input Thread to your Script.
		- If you need a new Puppet ScriptableObject, you can create one by right-clicking in the Project window and selecting Create -> Puppeteer -> New Puppet.
	- To set the move direction on the Input Thread, you can do something like this:
		```csharp
		moveDirection.x = UnityEngine.Input.GetAxis("Horizontal");
	  	moveDirection.y = UnityEngine.Input.GetAxis("Vertical");
	  	thread.SetMoveDirection(moveDirection);
		```
	- To get the move direction back after any potential modifications, you can do something like this.
	  	```csharp
		puppet.GetState();
	  	rb.velocity = puppet.GetMoveDirection().normalized * moveSpeed;
		```
  
# Inspector Set-Up:
- In the Inspector, add the Input Thread to your Player.
- Drag the Puppet and Input Thread into their respective fields on your Player script.
- Drag the Puppet you use for your Player Script into the Puppet field on the Input Thread.

# Making It Your Own:
- While the Input Thread may fit most of your needs, you may want more out of the system's capabilities; as such, I've made it easier for you to do so.
- Input Thread is a subtype of Thread Base Abstract Class.
- Thread Bases are Monobehaviors that set the Input State for the Puppet ScriptableObject
	- By default, there are three different types of Thread Bases:
		- Input Thread: Used to take in Player Input or Set A Custom Direction for a character to move in.
		- Directional Thread: Used to set a cardinal direction for a character to move in.
		- Navigation Thread: Used to make a character move toward a target in a straight line.
	- If need be, you can make your own threads by inheriting them from Thread Base and implementing their methods.
- You can also make your own Puppets by implementing the PuppetBase interface and doing the same
- On top of the Puppets & Threads, you can also make the Input State struct for your own needs.
	- By default, the Input State struct that they use contains:
		```csharp
	  	public Vector2 moveDirection;
	  	public bool crouching;
	  	public bool sprinting;
	  	public bool canJump;
	  	```
	- And it has a Custom Property Drawer for these fields called Input State Property Drawer;
	- If you require more or less inputs than this:
		- You can either add/remove them directly from each script.
		- Make a new Input State struct for your systems using this as a reference.
  
