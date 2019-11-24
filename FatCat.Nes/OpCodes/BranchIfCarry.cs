using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public class BranchIfCarry : BranchOpCode
	{
		public override string Name => "BCS";

		protected override CpuFlag Flag => CpuFlag.CarryBit;

		protected override bool FlagState => true;

		public BranchIfCarry(ICpu cpu, IAddressMode addressMode) : base(cpu, addressMode) { }
	}
}