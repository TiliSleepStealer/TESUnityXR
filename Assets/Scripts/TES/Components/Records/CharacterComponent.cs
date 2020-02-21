﻿using TESUnity.ESM;

namespace TESUnity.Components.Records
{
    public class CharacterComponent : RecordComponent
    {
        public CLASRecord Class { get; protected set; }
        public FACTRecord Faction { get; protected set; }
        public RACERecord Race { get; protected set; }
        public SKILRecord Skills { get; protected set; }
        public BSGNRecord BirthSign { get; protected set; }
    }
}