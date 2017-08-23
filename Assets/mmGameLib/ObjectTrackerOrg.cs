using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public struct Tuple<T, U>
//{
//    //
//    // replace with TypeComposit to make things slightly faster
//    //
//    public readonly T CompType;
//    public readonly U ObjID;

//    public Tuple(T first, U second)
//    {
//        this.CompType = first;
//        this.ObjID = second;
//    }
//}
public struct TypeComposit<T,U>
{
    public Type CompType;
    public int ObjID;

    public TypeComposit(Type _compType, int _id)
    {
        this.CompType = _compType;
        this.ObjID = _id;

    }
}
public class ObjectTrackerOrg
{

    static Dictionary<GameObject, int> retObjects = new Dictionary<GameObject, int>();
    static ObjectTrackerOrg _instance;
    private Dictionary<TypeComposit<Type,int>, GameObject> allObjects = new Dictionary<TypeComposit<Type, int>, GameObject>();
    public ObjectTrackerOrg()
    {
        if (_instance != null)
        {
            throw new CannotHaveTwoInstancesException();
        }
        _instance = this;
    }
    /// <summary>
    /// Getter for singelton instance.
    /// </summary>
    protected static ObjectTrackerOrg instance
    {
        get
        {
            if (_instance == null)
            {
                new ObjectTrackerOrg();
            }

            return _instance;
        }
    }
    /// <summary>
    /// Register the specified service instance. Usually called in Awake(), like this:
    /// Set<ExampleService>(this);
    /// </summary>
    /// <param name="service">Service instance object.</param>
    /// <typeparam name="T">Type of the instance object.</typeparam>
    public static void Register(Type compType, GameObject go)
    {
        TypeComposit<Type, int> key = new TypeComposit<Type, int>(compType, 0);
        if (instance.allObjects.ContainsKey(key))
        {
            //
            // increase the count and add the composit key
            //
            key = new TypeComposit<Type, int>(compType, instance.allObjects.Count);
        }
        
        instance.allObjects.Add(key, go);
    }
    /// <summary>
    /// Remove a component and its game object from the collection 
    /// </summary>
    /// <param name="component"></param>
    /// <param name="gameObject"></param>
    public static void Remove(Type compType, GameObject go)
    {
        TypeComposit<Type, int> key = new TypeComposit<Type, int>(compType, 0);
        //
        // if the partial key is found by type, then remove 
        //
        if (instance.allObjects.ContainsKey(key))
        {
            foreach (KeyValuePair<TypeComposit<Type, int>, GameObject> val in instance.allObjects)
            {
                if ((val.Key.CompType == key.CompType) & (val.Value == go))
                    instance.allObjects.Remove(key);
            }
        }
    }
    private static void CompareComponents(TypeComposit<Type, int> key)
    {
        //
        // if the partial key is found by type, then get all GameObjects that 
        // have the type,  We will end up with a List that has once occurance 
        // of each GameObject
        //
        //if (instance.allObjects.ContainsKey(key))
        //{
        foreach (KeyValuePair<TypeComposit<Type, int>, GameObject> val in instance.allObjects)
            {
                if (val.Key.CompType == key.CompType)
                    if (!retObjects.ContainsKey(val.Value))     //if GameObject is NOT present
                        retObjects.Add(val.Value, 0);           //   then add it
            }
        //}
}
    public static List<GameObject> Find<C1>() where C1 : Component
    {
        TypeComposit<Type, int> key;
        retObjects.Clear();

        key = new TypeComposit<Type, int>(typeof(C1), 0);
        CompareComponents(key);

        return retObjects.Keys.ToList();
    }
    public static List<GameObject> Find<C1, C2>() where C1 : Component where C2 : Component
    {
        TypeComposit<Type, int> key;
        retObjects.Clear();
        //Dictionary<GameObject, int> retObjects = new Dictionary<GameObject, int>();


        key = new TypeComposit<Type, int>(typeof(C1), 0);
        CompareComponents(key);
        List<GameObject> temp1 = retObjects.Keys.ToList();

        key = new TypeComposit<Type, int>(typeof(C2), 0);
        CompareComponents(key);
        List<GameObject> temp2 = retObjects.Keys.ToList();

        return temp1.Intersect(temp2).ToList();
        //return retObjects.Keys.ToList() ;
    }
    public static List<GameObject> Find<C1, C2, C3>() where C1 : Component where C2 : Component where C3 : Component
    {
        TypeComposit<Type, int> key;
        retObjects.Clear();

        key = new TypeComposit<Type, int>(typeof(C1), 0);
        CompareComponents(key);
        List<GameObject> temp1 = retObjects.Keys.ToList();

        key = new TypeComposit<Type, int>(typeof(C2), 0);
        CompareComponents(key);
        List<GameObject> temp2 = retObjects.Keys.ToList();
        List<GameObject> tempInbetween = temp1.Intersect(temp2).ToList();

        key = new TypeComposit<Type, int>(typeof(C3), 0);
        CompareComponents(key);
        List<GameObject> temp3 = retObjects.Keys.ToList();
        List<GameObject> finalList = tempInbetween.Intersect(temp3).ToList();

        return finalList;
        //return retObjects.Keys.ToList();
    }
    public static List<GameObject> Find<C1, C2, C3, C4>() where C1 : Component where C2 : Component where C3 : Component where C4 : Component
    {
        TypeComposit<Type, int> key;
        retObjects.Clear();

        key = new TypeComposit<Type, int>(typeof(C1), 0);
        CompareComponents(key);
        List<GameObject> temp1 = retObjects.Keys.ToList();

        key = new TypeComposit<Type, int>(typeof(C2), 0);
        CompareComponents(key);
        List<GameObject> temp2 = retObjects.Keys.ToList();
        List<GameObject> tempInbetween = temp1.Intersect(temp2).ToList();

        key = new TypeComposit<Type, int>(typeof(C3), 0);
        CompareComponents(key);
        List<GameObject> temp3 = retObjects.Keys.ToList();
        List<GameObject> tempInbetween1 = tempInbetween.Intersect(temp3).ToList();

        key = new TypeComposit<Type, int>(typeof(C4), 0);
        CompareComponents(key);
        List<GameObject> temp4 = retObjects.Keys.ToList();
        List<GameObject> finalList = tempInbetween1.Intersect(temp4).ToList();

        return finalList;
        //return retObjects.Keys.ToList();
    }
    public static void Clear()
    {
        instance.allObjects.Clear();
    }
    /// <summary>
    /// Prints the list of components/gameobjects in the debug console
    /// </summary>
    public static void Debug()
    {
        string output = "Debug objects List:\n";
        foreach (var s in instance.allObjects)
        {
            output += "* " + s.Key.CompType.Name + " = " + s.Value.ToString() + "\n";
        }
        output += "Total: " + instance.allObjects.Count.ToString() + " services registered.";

        UnityEngine.Debug.Log(output);
    }
    public class CannotHaveTwoInstancesException : Exception
    {
        public CannotHaveTwoInstancesException() : base("There can be only one instance of a the Services class. It is a singleton...") { }
    }
    public class ServiceAlreadyRegisteredException : Exception
    {
        public ServiceAlreadyRegisteredException(string goName) : base("A service of that name (" + goName + ")  has already been registered!") { }
    }
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(System.Type T) : base("Service (" + T.ToString() + ") not found.\nAlways Register() in Awake(). Never Find() in Awake(). Check Script Execution Order.") { }
    }
}
