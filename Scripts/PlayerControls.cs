﻿using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public int id;

	public ScoreHandler scoreHandler;
	private InputHandler inputHandler;
	private BuildingSpawner spawner;

	private BuildingSpawner.buildings selectedBuilding;
	
	void Start() {
		// assigns unique id to each player instance

		GamePhase.phase = GamePhase.phases.start;
		scoreHandler = new ScoreHandler();
		inputHandler = new InputHandler();
		spawner = new BuildingSpawner(scoreHandler, inputHandler, Resources.Load("Tile/Materials/PlayerMat") as Material, id);

		selectedBuilding = BuildingSpawner.buildings.house;
	}

	void Update () {
		// if only one finger is pressing the screen
		Touch[] touches = Input.touches;
		if (touches.Length == 1) {
			Touch tap = touches[0];

			// get data from screen touch
			inputHandler.check( tap);

			if (inputHandler.hold()) {
				// TODO circular UI

			} else if (inputHandler.tap()) {
				switch (GamePhase.phase) {

					// starting move
					case GamePhase.phases.start:
						spawner.spawn( BuildingSpawner.buildings.fort);

						GamePhase.phase = GamePhase.phases.play;
						GamePhase.phaseChangeTime = Time.realtimeSinceStartup;
						break;

					// regular click
					case GamePhase.phases.play:
						spawner.spawn( selectedBuilding);
						break;
				}
			}
		}
	}
}
