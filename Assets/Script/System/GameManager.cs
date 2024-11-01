using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{

    internal List<Cat> cats = new List<Cat>();
    internal List<Dog> dogs = new List<Dog>();


}
