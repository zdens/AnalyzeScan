namespace LIMPFileReader.Formats.Mzxml
{
    public partial class msRunSpottingRobot
    {

        private ontologyEntryType robotManufacturerField;

        private ontologyEntryType robotModelField;

        private string timePerSpotField;

        private string deadVolumeField;

        public msRunSpottingRobot()
        {
            this.robotModelField = new ontologyEntryType();
            this.robotManufacturerField = new ontologyEntryType();
        }

        public ontologyEntryType robotManufacturer
        {
            get
            {
                return this.robotManufacturerField;
            }
            set
            {
                this.robotManufacturerField = value;
            }
        }

        public ontologyEntryType robotModel
        {
            get
            {
                return this.robotModelField;
            }
            set
            {
                this.robotModelField = value;
            }
        }

        public string timePerSpot
        {
            get
            {
                return this.timePerSpotField;
            }
            set
            {
                this.timePerSpotField = value;
            }
        }

        public string deadVolume
        {
            get
            {
                return this.deadVolumeField;
            }
            set
            {
                this.deadVolumeField = value;
            }
        }
    }
}
