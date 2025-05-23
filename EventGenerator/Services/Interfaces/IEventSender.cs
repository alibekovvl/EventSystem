﻿using EventGenerator.Models;

namespace EventGenerator.Services.Interfaces;

public interface IEventSender
{
    Task<bool> SendEventAsync(Event @event);
}