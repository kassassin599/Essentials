using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test : MonoBehaviour
{
  public long num = 12345678;


  void Start()
  {

	string numberString = num.ToString();

    string resultString = AddCommasIndian(numberString);

    Debug.Log(resultString);
  }

  public string AddCommas(string numberString)
  {
    string str = numberString;
    int count = numberString.Length;
    string result = "";

    for (int i = count - 1; i >= 0; i--)
    {
      string temp = str.Substring(i, 1);
      if ((count - 1) - i != 0 && ((count - 1) - i) % 3 == 0)
      {
        result = temp + "," + result;
      }
      else
      {
        result = temp + result;
      }
    }

    return result;
  }

  public string AddCommasIndian(string numberString)
  {
    string str = numberString;
    int count = numberString.Length;
    string result = "";

    string lastThree = numberString.Substring(count - 4, 3);

    for (int i = count - 4; i >= 0; i--)
    {
      string temp = str.Substring(i, 1);
      if (((count - 4) - i) % 2 == 0)
      {
        result = temp + "," + result;
      }
      else
      {
        result = temp + result;
      }
    }

    result = result + lastThree;

    return result;
  }
}
