using System;
using System.Collections.Generic;

namespace RulebookConversionLibrary.Helpers
{
    public static class IncrementIdHelper
    {
        private static readonly Dictionary<string, int> _typeIdDictionary = new Dictionary<string, int>();
        
        public static int GetNewId(Type type)
        {
            if (_typeIdDictionary.TryGetValue(type.ToString(), out var lastId))
            {
                _typeIdDictionary[type.ToString()]++;
            }
            else
            {
                _typeIdDictionary.Add(type.ToString(), 1);
            }
            
            return _typeIdDictionary[type.ToString()];
        }
    }
}