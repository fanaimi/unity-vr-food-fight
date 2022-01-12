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
    * collider > check Is Trigger > code: isTrigger = true 
    * objects will not bounce, but will go through
    * OnTriggerEnter
    * OnTriggerExit

* hands in controllers: 
    * add sphere collider
        * is trigger
    * rigid body    
        * no gravity
        * is kinematic


* changing colour to an object
    ```c#
        private MeshRenderer rend;
        private Color defaultColor;
        private Rigidbody rb;


        void Start()
        {
                rend = GetComponent<MeshRenderer>();
                defaultColor = rend.material.color;
                rb = GetComponent<Rigidbody>();
                // changing colour immediately
                rend.material.color = hoverColor;
        }


    ```


* TRANSITIONING colour to an object
    ```c#
        private MeshRenderer rend;
        private Color defaultColor;
        private Rigidbody rb;


        void Start()
        {
                rend = GetComponent<MeshRenderer>();
                defaultColor = rend.material.color;
                rb = GetComponent<Rigidbody>();
                
        }

        void Update()
        {
            
            // transitioning colour
            rend.material.color =
                Color.Lerp(
                    targetRend.material.color,
                    grabbedObject.hoverColor,
                    lerpTime * Time.deltaTime);
                    
        }
    ```


* grip button interaction
    ```c#
        public string grabButton; // will be given the "XRI_Right_GripButton" value for Input manager
    ```
    * edit > project settings > Input Manager 
        * as a default no input fo XR controllers

    * Assets > seed XR Input Bindings     
        this will seed and add all XR controllers inputs in edit > project settings > Input Manager 

    * we will use "XRI_Right_GripButton"      

    * paste this string in the grabButton field


* detect grip button actions
    ```c#
        // start grab
        if (Input.GetButtonDown(grabButton))
        { ... }

        // release grab
        if(Input.GetButtonUp(grabButton))
        { ... }

        // keep grabbing
        if(Input.GetButton(grabButton))
        { ... }
    ```


* read this later about grabbing objects: 
https://medium.com/@danielcestrella/an-improved-way-of-grabbing-objects-in-vr-with-unity3d-558517a8db1


* if needed we can activate camera > rendering > post processing (check box)





## singleton 

* prevents the creation of duplicates
* only one instance is possible at the same time

```c#
	/// <summary>
    /// singleton start
    /// </summary>
    private static Target _instance;
    public static Target Instance { get { return _instance; } }
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
```


## rotating rig (Locomotion.cs)
```c#
   void Update()
    {
        HandleRotation();
    }

    /// <summary>
    /// Handling snap turns
    /// </summary>
    private void HandleRotation()
    {
        // projects settings > input manager > XRI_LEFT/RIGHT_Primary2DAxisClick
        // checking if thumbstick is pressed
        if (Input.GetButtonDown($"XRI_{controller.hand}_Primary2DAxisClick"))
        {
            // XRI_Left_Primary2D
            float rotation =
                Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Horizontal") > 0 ? 30 : -30;

            
            
            xrRig.Rotate(0, rotation, 0);

        }
    }

```


## moving rig around (Locomotion.cs)
```c#
   void Update()
    {
        HandleMovement();
    }


    /// <summary>
    /// this will handle smooth motion forward and sideways
    /// </summary>
    private void HandleMovement()
    {
        Vector3 forwardDirection = new Vector3(xrRig.forward.x,0 ,xrRig.forward.z);

        Vector3 rightDirection = new Vector3(xrRig.right.x, 0, xrRig.right.z);
        
        // getting values between 0 and 1
        forwardDirection.Normalize();
        rightDirection.Normalize();
        
        // getting directions
        float horizontal = Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Horizontal");
        float vertical = Input.GetAxis($"XRI_{controller.hand}_Primary2DAxis_Vertical");
        

        // moving forward and backward
        xrRig.position += (vertical * Time.deltaTime * -forwardDirection * playerSpeed);

        // left and right 
        xrRig.position += (horizontal * Time.deltaTime * rightDirection * playerSpeed);


    } // HandleMovement


```


## raycasting 
* add Line Renderer component to hand controller
* check WORLD SPACE
* decrease width to get a line instead of a plan

```c#
   void Update()
    {
        HandleRaycast();
    }

    /// <summary>
    /// handling raycast for teleportation
    /// </summary>
    void HandleRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit  hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hitInfo.point);
        }
        else
        {
            line.enabled = false;
        }

    } // HandleRaycast  
```

## to avoid motion sickness and movements that are too quick

* edit > project settings > Input Manager
* set values for
    **XRI_Left_ Primary2DAxis_Vertical** 
    and 
    **XRI_Right_ Primary2DAxis_Vertical**
    * Gravity : 0.3
    * Dead : 0.6
    * Sensitivity : 0.7

* DEAD : Size of the analog dead zone. ALl analog device values within thiss range will map to neutral
