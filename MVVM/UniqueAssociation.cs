using System;
using System.Runtime.CompilerServices;

namespace xammacmvvm
{
	// This is going to create a unique NSObject wrapper for a given managed object.
	public class UniqueAssociation
	{
        public static readonly UniqueAssociation Instance = new();

		readonly ConditionalWeakTable<object, NSObject> _map = new();

        // HACK: there are ways to make this not fail at runtime, but out of scope for ptototype.
		public T GetOrCreate<T>(object target) where T:NSObject
        {
			return (T)_map.GetValue(target, static key =>
			{
                // HACK: Only handle ViewModel in prototype.
                // Proper solution could include source generating the associations or reflection.
                return key switch
                {
                    ViewModel model => new ObservableNSObject(model),
                    _ => NSObject.FromObject(key),
                };
			});
        }
	}
}

