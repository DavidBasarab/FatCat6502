using FatCat.Nes.OpCodes.AddressingModes;

namespace FatCat.Nes.OpCodes
{
	public abstract class OpCode
	{
		protected readonly IAddressMode addressMode;
		protected readonly ICpu cpu;
		protected byte fetched;

		public abstract string Name { get; }

		protected OpCode(ICpu cpu, IAddressMode addressMode)
		{
			this.cpu = cpu;
			this.addressMode = addressMode;
		}

		public abstract int Execute();

		protected void Fetch() => fetched = addressMode.Fetch();
	}
}