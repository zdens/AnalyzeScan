namespace LIMPFileReader.Formats.Mzxml
{
    public partial class msRunSpottingPlateSpot
    {

        private ontologyEntryType maldiMatrixField;

        private string spotIDField;

        private string spotXPositionField;

        private string spotYPositionField;

        private string spotDiameterField;

        public msRunSpottingPlateSpot()
        {
            this.maldiMatrixField = new ontologyEntryType();
        }

        public ontologyEntryType maldiMatrix
        {
            get
            {
                return this.maldiMatrixField;
            }
            set
            {
                this.maldiMatrixField = value;
            }
        }

        public string spotID
        {
            get
            {
                return this.spotIDField;
            }
            set
            {
                this.spotIDField = value;
            }
        }

        public string spotXPosition
        {
            get
            {
                return this.spotXPositionField;
            }
            set
            {
                this.spotXPositionField = value;
            }
        }

        public string spotYPosition
        {
            get
            {
                return this.spotYPositionField;
            }
            set
            {
                this.spotYPositionField = value;
            }
        }

        public string spotDiameter
        {
            get
            {
                return this.spotDiameterField;
            }
            set
            {
                this.spotDiameterField = value;
            }
        }
    }
}
