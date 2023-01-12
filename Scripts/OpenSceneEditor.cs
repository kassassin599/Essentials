using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class OpenSceneEditor : MonoBehaviour
{

  [MenuItem("OpenScene/LogoScene")]
  public static void OpenScene_LogoScene()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/LogoScene.unity");
    }
  }


  [MenuItem("OpenScene/Login_Screen")]
  public static void OpenScene_Login_Screen()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/Login_Screen.unity");
    }
  }

  [MenuItem("OpenScene/Main_Menu")]
  public static void OpenScene_Main_Menu()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/Main_Menu.unity");
    }
  }

  [MenuItem("OpenScene/Login_Screen")]
  public static void OpenScene_Home()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/Login_Screen.unity");
    }
  }

  [MenuItem("OpenScene/Mode_Gameplay")]
  public static void OpenScene_Mode_Gameplay()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/Mode_Gameplay.unity");
    }
  }

  [MenuItem("OpenScene/Mode_Nets")]
  public static void OpenScene_Mode_Nets()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/Mode_Nets.unity");
    }
  }

  [MenuItem("OpenScene/UI_MenuScreens")]
  public static void OpenScene_UI_MenuScreens()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/UI_MenuScreens.unity");
    }
  }

  [MenuItem("OpenScene/UI_Multiplayer_Home")]
  public static void OpenScene_UI_Multiplayer_Home()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/UI_Multiplayer_Home.unity");
    }
  }

  [MenuItem("OpenScene/BowlerAnimTest.unity")]
  public static void OpenScene_BowlerAnimTest()
  {
      if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
      {
          EditorSceneManager.SaveOpenScenes();
          EditorSceneManager.OpenScene("Assets/Scenes/TestScenes/BowlerAnimTest.unity");
          EditorSceneManager.OpenScene("Assets/$StadiumURP/Ahmedabad/High/Morning/Stadium_Ahmedabad_Morning_High.unity", OpenSceneMode.Additive);
        }
  }


  [MenuItem("OpenScene/FieldingTest")]
  public static void OpenScene_FieldingTest_Scene()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/Fielding.unity");
      EditorSceneManager.OpenScene("Assets/$StadiumURP/Mumbai/High/Morning/Stadium_Mumbai_Morning_High.unity", OpenSceneMode.Additive);
    }
  }

  [MenuItem("OpenScene/TimelineScene")]
  public static void OpenScene_TimelineTest_Scene()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/Scenes/TestScenes/TimelineCutsceneScene.unity");
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Mumbai_New/Ultra/Morning/Stadium_Mumbai_Morning_Ultra.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Capetown/High/Afternoon/Stadium_CapeTown_Afternoon_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Chennai/High/Dusk/Stadium_Chennai_Dusk_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Dubai/High/Night/Stadium_Dubai_Night_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Melbourne/High/Evening/Stadium_Melbourne_Evening_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/London/High/Afternoon/Stadium_London_Afternoon_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Chennai/High/Morning/Stadium_Chennai_Morning_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Ahmedabad/High/Morning/Stadium_Ahmedabad_Morning_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Pune/High/Morning/Stadium_Pune_Morning_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Sydney/High/Morning/Stadium_Sydney_Morning_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Hobart/High/Morning/Stadium_Hobart_Morning_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Adelaide/High/Morning/Stadium_Adelaide_Morning_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Brisbane/High/Morning/Stadium_Brisbane_Morning_High.unity", OpenSceneMode.Additive);
      //EditorSceneManager.OpenScene("Assets/$StadiumURP/Pert/High/Morning/Stadium_Pert_Morning_High.unity", OpenSceneMode.Additive);
      EditorSceneManager.OpenScene("Assets/$StadiumURP/AbuDhbai/Ultra/Morning/Stadium_Abudhabi_Morning_Ultra.unity", OpenSceneMode.Additive);
      EditorSceneManager.OpenScene("Assets/$StadiumURP/Sharjah/Ultra/Morning/Stadium_Sharjah_Morning_Ultra.unity", OpenSceneMode.Additive);
      EditorSceneManager.OpenScene("Assets/$StadiumURP/Bengaluru/Ultra/Morning/Stadium_Bengaluru_Morning_Ultra.unity", OpenSceneMode.Additive);
        }
  }

  [MenuItem("OpenScene/ShotTestingScene")]
  public static void OpenScene_ShotTest_Scene()
  {
    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
    {
      EditorSceneManager.SaveOpenScenes();
      EditorSceneManager.OpenScene("Assets/BattingShotTesting/ShotTestingScene.unity");
      EditorSceneManager.OpenScene("Assets/$StadiumURP/Mumbai_New/Ultra/Morning/Stadium_Mumbai_Morning_Ultra.unity", OpenSceneMode.Additive);
    }
  }
}
