using System.Collections.Generic;

namespace LIMPFileReader.Formats.Mzxml
{
    public partial class msRunSpottingPlate
    {

        private ontologyEntryType plateManufacturerField;

        private ontologyEntryType plateModelField;

        private msRunSpottingPlatePattern patternField;

        private List<msRunSpottingPlateSpot> spotField;

        private string plateIDField;

        private string spotXCountField;

        private string spotYCountField;

        public msRunSpottingPlate()
        {
            this.spotField = new List<msRunSpottingPlateSpot>();
            this.patternField = new msRunSpottingPlatePattern();
            this.plateModelField = new ontologyEntryType();
            this.plateManufacturerField = new ontologyEntryType();
        }

        public ontologyEntryType plateManufacturer
        {
            get
            {
                return this.plateManufacturerField;
            }
            set
            {
                this.plateManufacturerField = value;
            }
        }

        public ontologyEntryType plateModel
        {
            get
            {
                return this.plateModelField;
            }
            set
            {
                this.plateModelField = value;
            }
        }

        public msRunSpottingPlatePattern pattern
        {
            get
            {
                return this.patternField;
            }
            set
            {
                this.patternField = value;
            }
        }

        public List<msRunSpottingPlateSpot> spot
        {
            get
            {
                return this.spotField;
            }
            set
            {
                this.spotField = value;
            }
        }

        public string plateID
        {
            get
            {
                return this.plateIDField;
            }
            set
            {
                this.plateIDField = value;
            }
        }

        public string spotXCount
        {
            get
            {
                return this.spotXCountField;
            }
            set
            {
                this.spotXCountField = value;
            }
        }

        public string spotYCount
        {
            get
            {
                return this.spotYCountField;
            }
            set
            {
                this.spotYCountField = value;
            }
        }
    }
}
