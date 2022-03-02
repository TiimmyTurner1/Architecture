using System;
using System.Collections.Generic;
using CodeBase.Hero;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject HeroGameObject { get; }

        event Action HeroCreated;
        GameObject CreateHero(GameObject at);
        void CreateHud();
        void Cleanup();
    }
}