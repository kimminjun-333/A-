using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : SingletonManager<GameManager>
{

    internal List<Cat> cats = new List<Cat>();
    internal List<Dog> dogs = new List<Dog>();

    public Image Win;
    public Image Lose;

}
