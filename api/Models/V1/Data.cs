namespace DiffProject.Models.V1
{
    using System.Threading.Tasks;
    using DiffProject.Infrastructure.V1;
    using DiffProject.Models.V1;

    public struct Data
    {
        public string Id { get; private set; }
        public string Text { get; private set; }
        public SideEnum Side { get; private set; }

        private string _hash;

        public Data(string id, SideEnum side, string value, IHashStrategy hashStrategy)
        {
            this.Id = id;
            this.Side = side;
            this.Text = value;
            this._hash = hashStrategy.GetHashAsync(value).Result;
        }

        public string GetHash()
		{
            if (!string.IsNullOrEmpty(this._hash))
                return _hash;

            return null;
		}

		public override string ToString()
		{
            if (string.IsNullOrEmpty(Text))
                return string.Empty;

            return this.Text;
        }

		public override bool Equals(object obj)
		{
            switch(obj)
            {
                case Data d:
                    return this._hash == d.GetHash();
                default: return false;
            }
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
	}
}