#### Look-At Demos
One strategy for VR navigation, especially if you don't have any external imput device, is to trigger events based on how long the user has directly stared at an object or GUI Element.  This project shows one implementation, and 3 examples.

1. Simple Scene - This is a bare bones scene that you can use as a template.
2. Light Scene - In this demo you can switch lights on and off by staring at the target next to each bulb.
3. Box Pick Scene - In this demo you can select boxes by staring at them, then move them to another part of the board.

The "Looking At" script is the brains of the technique.  You can use the look-sensor prefab in your scenes by simply parenting it to the game object that's controlling camera location and orientation.

If you add a line renderer to the look-sensor then it will draw a line to the object you're looking at.
