using System;
using BepInEx;
using UnityEngine;
using Utilla;

namespace ModTemplate
{
	// This is your mod's main class.
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
    {
		void OnEnable()
		{
		    // Set up your mod here
			// Code here runs at the start and whenever your mod is enabled.

			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
		     // Undo mod setup here.
			 // Adding disabling here adds support for toggling your mod with mods like BananaOS or ComputerInterface.

			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
			// Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null)
		}

        void Start()
        {
            GorillaTagger.OnPlayerSpawned(Init);

            // Don't touch this function, it's for making sure the event subscription happens.
        }

        private void Init()
        {
            NetworkSystem.Instance.OnJoinedRoomEvent += OnJoinedRoom;
            NetworkSystem.Instance.OnReturnedToSinglePlayer += OnLeftRoom; 

            // Don't touch this function, it's for subscribing to events.
        }

        private void OnJoinedRoom()
        {
            if (NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                // Code here runs when the player joins a modded room.
            }
			else
			{
				// Code here runs when the player joins a room that isn't modded.
			}
        }
        private void OnLeftRoom()
        {
            // Code here runs when the player leaves a room.
        }

	    // You can either use a "void Update()" function for code that runs ever frame so like input detection as seen below.
        // You can also use a "void FixedUpdate()" as shown below for physics based things since it runs every physics tick.
        // Use any of these depending on what you need to do, they can also be used in unison.
		
		void Update() // Input detection etc.(Runs every frame)
        {
			if (!NetworkSystem.Instance.InRoom)
			{
                // Code here runs every frame when the player is not in a room.
                return;
			}
			else if (!NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                // Code here runs every frame when the player is in a room that isn't modded.
                return;
            }

            // Code here runs every frame when the player is in a modded room.
        }

        void FixedUpdate() // Physics based things. (Runs every physics tick so 50 times a second)
		{
            if (!NetworkSystem.Instance.InRoom)
            {
                // Code here runs every physics tick when the player is not in a room.
                return;
            }
            else if (!NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                // Code here runs every physics tick when the player is in a room that isn't modded.
                return;
            }

            // Code here runs every physics tick when the player is in a modded room.
        }
    }
}
