Look-At Demos
Use this project to test out events based on how long you look at an object.
The brains of the technique is in the LookedAt script.  Add it to an empty object that's parented to the Camera controller (typical for VR), or directly to the camera object.  The Script will raycast out and return the first object that it hits.  The script uses delegates to trigger an event when the object is looked at, and when the camera looks away from the current selected object.
