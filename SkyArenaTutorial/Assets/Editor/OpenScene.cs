using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneItem : Editor
{
	[MenuItem( "Open Scene/MainMenu" )]
	public static void OpenMainMenu()
	{
		OpenScene( "MainMenu" );
	}

	[MenuItem( "Open Scene/RoomBrowser" )]
	public static void OpenRoomBrowser()
	{
		OpenScene( "RoomBrowser" );
	}

	[MenuItem( "Open Scene/Greenlands" )]
	public static void OpenLevel1()
	{
		OpenScene( "Greenlands" );
	}

	[MenuItem( "Open Scene/City" )]
	public static void OpenLevel2()
	{
		OpenScene( "City" );
	}

	[MenuItem( "Open Scene/Level1Offline" )]
	public static void OpenLevel1Offline()
	{
		OpenScene( "Level1Offline" );
	}

	static void OpenScene( string name )
	{
		if( EditorApplication.SaveCurrentSceneIfUserWantsTo() )
		{
			EditorApplication.OpenScene( "Assets/Scenes/" + name + ".unity" );
		}
	}
}
 