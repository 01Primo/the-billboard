﻿using Npgsql;
using System.Data;
using TheBillboard.Models;

namespace TheBillboard.Abstract;

public interface IReader
{
    public IAsyncEnumerable<TEntity> QueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector);
    public Task<TEntity?> SingleQueryAsync<TEntity>(string query, Func<IDataReader, TEntity> selector);
}