﻿using TheBillboard.Models;

namespace TheBillboard.Abstract;

public interface IMessageGateway
{
    Task<IEnumerable<Message>> GetAll();
    Task<Message?> GetById(int id);
    Task<bool> Create(Message message);
    Task<bool> Update(Message message);
    Task<bool> Delete(int id);
}