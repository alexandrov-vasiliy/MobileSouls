using _Game.CodeBase.Cameralogic;
using UnityEngine;

namespace _Game.CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string Playerinitialpoint = "PlayerInitialPoint";
        private const string ControlsHud = "Controls/Hud";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private string _heroHero;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _heroHero = "Hero/hero";
        }

        public void Enter(string payload)
        {
            _curtain.Show();
            _sceneLoader.Load(payload, OnLoaded);
        }
        
        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindGameObjectWithTag(Playerinitialpoint);
            GameObject hero = Instaniate(_heroHero, at: initialPoint.transform.position);
            
            Instaniate(ControlsHud);
            CameraFollow(hero);
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }

        private static GameObject Instaniate(string path)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab);
        }

        private static GameObject Instaniate(string path, Vector3 at)
        {
            var heroPrefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(heroPrefab, at, Quaternion.identity);
        }

       
    }
}