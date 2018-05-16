namespace LIMPFileReader.Formats.Mzxml
{
    public partial class msRunSpottingPlatePattern
    {

        private ontologyEntryType spottingPatternField;

        private msRunSpottingPlatePatternOrientation orientationField;

        public msRunSpottingPlatePattern()
        {
            this.orientationField = new msRunSpottingPlatePatternOrientation();
            this.spottingPatternField = new ontologyEntryType();
        }

        public ontologyEntryType spottingPattern
        {
            get
            {
                return this.spottingPatternField;
            }
            set
            {
                this.spottingPatternField = value;
            }
        }

        public msRunSpottingPlatePatternOrientation orientation
        {
            get
            {
                return this.orientationField;
            }
            set
            {
                this.orientationField = value;
            }
        }
    }
}
