namespace LIMPFileReader.Formats.Mzxml
{
    public partial class msRunSpottingPlatePatternOrientation
    {

        private string firstSpotIDField;

        private string secondSpotIDField;

        public string firstSpotID
        {
            get
            {
                return this.firstSpotIDField;
            }
            set
            {
                this.firstSpotIDField = value;
            }
        }

        public string secondSpotID
        {
            get
            {
                return this.secondSpotIDField;
            }
            set
            {
                this.secondSpotIDField = value;
            }
        }
    }
}
