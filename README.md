# AnimationSpeedController
Adapt walk/run animation speed in Unity3d to your character's speed

Use this script on your player or non-player character and it will adapt the animation speed according to the effective speed. It works with navigation agents, character controllers or physics controlled rigidbodies. You need to expose a speed parameter in your animator which this script will control and set a nominal speed.
Optionally it can calculate a walk/run ratio and set a backward flag.
If you don't want to use a feature just blank the respective animator parameter name.

How does it work?

It calculates the speed from either the navigation agent, character controller or rigidbody and compares it to the configured nominal speed (that is the speed the animation is expected to be built for).
The found factor is then updated each frame and sent to the animator effectively adapting the animation speed to the real speed.

The speed of a rigidbody is assumed to be measured on the xz-plane whereas navigation agent and character controller speed is in 3d. That seems to be the usual way to control a character and its speed.

The additional parameter named animation speed multiplier can be used to adapt the speed further in case your nominal speed is off. Normally you can keep it at 1 but I've created a method to record actual speed vs. animation walk speed and then this parameter comes in handy.

