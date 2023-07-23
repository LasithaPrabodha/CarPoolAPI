
using System;
using System.Linq.Expressions;
using Ardalis.GuardClauses;
using CarPool.Application.Contracts;
using CarPool.Domain.Common;
using CarPool.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Infrastructure.Repositories;

internal abstract class EFRepository<TEntity, TId> : IAggregateRepository<TEntity, TId>
    where TEntity : class, IAggregateRoot
{

    private readonly DbSet<TEntity> _entities;
    private readonly ApplicationDbContext _context;


    public EFRepository(ApplicationDbContext context)
    {
        _entities = context.Set<TEntity>();
        _context = context;
    }

    public void Add(TEntity entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        _entities.Add(entity);
    }

    public async ValueTask AddAsync(TEntity entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        await _entities.AddAsync(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _entities.AddRange(entities);
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        return _entities.AddRangeAsync(entities);
    }

    public void Delete(TEntity entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        _entities.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        Guard.Against.Null(entities, nameof(entities));
        _entities.RemoveRange(entities);
    }

    public bool Exists(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.Any(predicate);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.AnyAsync(predicate);
    }

    public IQueryable<TEntity> GetAll()
    {
        return GetEntities();
    }

    public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
    {
        return GetEntities().Where(predicate);
    }

    public TEntity? GetFirst(Expression<Func<TEntity, bool>> predicate)
    {
        return GetEntities().FirstOrDefault(predicate);
    }

    public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await GetEntities().FirstOrDefaultAsync(predicate);
    }

    public TEntity? GetSingle(Expression<Func<TEntity, bool>> predicate)
    {
        return GetEntities().SingleOrDefault(predicate);
    }

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await GetEntities().SingleOrDefaultAsync(predicate);
    }

    public void Update(TEntity entity)
    {
        Guard.Against.Null(entity, nameof(entity));
        _entities.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        Guard.Against.Null(entities, nameof(entities));
        foreach (var entity in entities)
        {
            Update(entity);
        }
    }

    public TEntity? Find(TId id)
    {
        return _entities.Find(id);
    }

    public async Task<TEntity?> FindAsync(TId id)
    {
        return await _entities.FindAsync(id);
    }

    private IQueryable<TEntity> GetEntities(bool asNoTracking = true)
    {
        return asNoTracking ? _entities.AsNoTracking() : _entities;
    }


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}