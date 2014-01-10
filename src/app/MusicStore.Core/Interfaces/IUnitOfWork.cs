using System;

namespace MusicStore.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void RollBack();
    }
}
