﻿using System;
using System.Collections.Generic;
using Statiq.Common;

namespace Statiq.App
{
    public partial interface IBootstrapper
    {
        public IBootstrapper SubscribeEvent<TEvent>(AsyncEventHandler<TEvent> handler) =>
            ConfigureEngine(x => x.Events.Subscribe(handler));

        public IBootstrapper SubscribeEvent<TEvent>(Common.EventHandler<TEvent> handler) =>
            ConfigureEngine(x => x.Events.Subscribe(handler));
    }
}
