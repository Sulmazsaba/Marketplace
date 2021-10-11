using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Framework
{
    public abstract class Entity<TId> :IInternalEventHandler where TId:Value<TId>
    {
        private readonly Action<object> _applier;
        public TId Id { get; protected set; }
        protected abstract void When(object @event);
        protected Entity(Action<object> applier) => _applier = applier;

        protected void Apply(object @event)
        {
            When(@event);
            _applier(@event);
        }


        //public IEnumerable<object> GetChanges() => _events.AsEnumerable();
        //public void ClearChanges() => _events.Clear();

        //protected abstract void EnsureValidState();
        public void Handle(object @event) => When(@event);
    }
}
