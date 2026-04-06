using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LevelFactory
{
    private readonly Transform _root;
    private readonly IResourcesLoaderService _resourceLoader;
    private readonly IObjectResolver _container;


    public LevelFactory(IResourcesLoaderService resourceLoader,
                        IObjectResolver container,
                        Transform root)
    {
        _resourceLoader = resourceLoader;
        _container = container;
        _root = root;
    }

    public async UniTask<LevelContext> CreateLevelContext(string levelPath)
    {

        var levelPrefab = await _resourceLoader.LoadAsync<LevelContext>(levelPath);
        var level = Object.Instantiate(levelPrefab, _root);
        _container.InjectGameObject(level.gameObject);
        return level;
    }

    public void ReleaseLevelContext(string levelPath)
    {
        _resourceLoader.Release(levelPath);
    }
}
