﻿using System;
using UnityEngine;
using IPA.Config.Data;
using IPA.Config.Stores;
using Enhancements.Misc;
using Enhancements.Clock;
using Enhancements.Timers;
using Enhancements.Volume;
using IPA.Config.Stores.Attributes;
using Enhancements.Breaktime;

namespace Enhancements
{
    public class Config
    {
        [NonNullable]
        public virtual ClockSettings Clock { get; set; } = new ClockSettings();
        [NonNullable]
        public virtual TimerSettings Timer { get; set; } = new TimerSettings();
        [NonNullable]
        public virtual BreaktimeSettings Breaktime { get; set; } = new BreaktimeSettings();
        [NonNullable]
        public virtual VolumeSettings Volume { get; set; } = new VolumeSettings();
        [NonNullable]
        public virtual MiscSettings Misc { get; set; } = new MiscSettings();
        [NonNullable]
        public virtual OptidraSettings Optidra { get; set; } = new OptidraSettings();

        public virtual void Changed()
        {
            Clock.MarkDirty();
        }
    }

    public class VectorConverter : ValueConverter<Vector3>
    {
        public override Vector3 FromValue(Value value, object parent)
        {
            if (value is Map m)
            {
                m.TryGetValue("x", out Value x);
                m.TryGetValue("y", out Value y);
                m.TryGetValue("z", out Value z);
                Vector3 vec = Vector3.zero;
                if (x is FloatingPoint pointX)
                {
                    vec.x = (float)pointX.Value;
                }
                if (y is FloatingPoint pointY)
                {
                    vec.y = (float)pointY.Value;
                }
                if (z is FloatingPoint pointZ)
                {
                    vec.z = (float)pointZ.Value;
                }
                return vec;
            }
            throw new ArgumentException("Value cannot be parsed into a Vector", nameof(value));
        }

        public override Value ToValue(Vector3 obj, object parent)
        {
            var map = Value.Map();
            map.Add("x", Value.Float((decimal)obj.x));
            map.Add("y", Value.Float((decimal)obj.y));
            map.Add("z", Value.Float((decimal)obj.z));
            return map;
        }
    }
}