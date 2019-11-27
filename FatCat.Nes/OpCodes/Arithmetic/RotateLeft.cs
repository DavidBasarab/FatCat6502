using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes.Arithmetic
{
	public class RotateLeft : OpCode
	{
		public override string Name => "ROL";

		public RotateLeft(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }

		public override int Execute()
		{
			Fetch();

			var value = (fetched << 1) | GetCarryFlagValue();

			ApplyFlag(CpuFlag.CarryBit, value.ApplyHighMask() > 0);

			if (ImpliedAddressMode) cpu.Accumulator = value.ApplyLowMask();
			else cpu.Write(cpu.AbsoluteAddress, value.ApplyLowMask());

			return -1;
		}

		private int GetCarryFlagValue() => cpu.GetFlag(CpuFlag.CarryBit) ? 1 : 0;
	}
}