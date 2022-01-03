# unity-vr-food-fight

* package manager > install XR plugin management

* edit > project settings > XR plugin management > pc > check: oculus

* delete main camera

* handmade XR Rig => empty game object (XR Rig) -> offset -> camera

* offset -> add component "CAMERA OFFSET"
    * Requested Tracking Mode: "Floor" (quest will check the floor)

* CAMERA -> add component "Tracked Posed Driver"
    * component added by XR Plugin management
    * gets input values from headset and controllers    

* camera > clipping plane > near => 0.01

* add RigidBody to enable physics

* in case of troubles with collision detection, under rigidbody component, we can change collision detection from 
Discrete to Continuous, but it's NOT recommended

* BOUNCING COLLISSION:
    * OnCollisionEnter
    
* TRIGGER COLLISION: 
    * rigidbody > check Is Trigger > code: isTrigger = true 
    * objects will not bounce, but will go through
    * OnTriggerEnter
    * OnTriggerExit




read this later: https://medium.com/@danielcestrella/an-improved-way-of-grabbing-objects-in-vr-with-unity3d-558517a8db1