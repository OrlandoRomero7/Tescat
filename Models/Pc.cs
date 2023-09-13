using System;
using System.Collections.Generic;

namespace Tescat.Models
{
    public class Pc
    {
        public Guid IdPc { get; set; }

        public int? IdUser { get; set; }

        public string? PcType { get; set; }

        public string? Ip { get; set; }

        public string? PcName { get; set; }

        public string? Model { get; set; }

        public string? UsedRamSlots { get; set; }

        public DateTime? LastMaint { get; set; }

        public int? LastUser { get; set; }

        public virtual Cpu? Cpu { get; set; }

        public virtual Gpu? Gpu { get; set; }

        public virtual User? IdUserNavigation { get; set; }

        public virtual LaptopBattery? LaptopBattery { get; set; }

        public virtual ICollection<MemoryRam> MemoryRams { get; set; } = new List<MemoryRam>();

        public virtual Motherboard? Motherboard { get; set; }

        public virtual PcCredential? PcCredential { get; set; }

        public virtual PowerSupply? PowerSupply { get; set; }

        public virtual ICollection<Storage> Storages { get; set; } = new List<Storage>();

    }
}


