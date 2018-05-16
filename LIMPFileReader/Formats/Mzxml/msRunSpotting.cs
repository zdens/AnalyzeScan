using System.Collections.Generic;

namespace LIMPFileReader.Formats.Mzxml
{
    public partial class msRunSpotting
    {

        private List<msRunSpottingPlate> plateField;

        private msRunSpottingRobot robotField;

        public msRunSpotting()
        {
            this.robotField = new msRunSpottingRobot();
            this.plateField = new List<msRunSpottingPlate>();
        }

        public List<msRunSpottingPlate> plate
        {
            get
            {
                return this.plateField;
            }
            set
            {
                this.plateField = value;
            }
        }

        public msRunSpottingRobot robot
        {
            get
            {
                return this.robotField;
            }
            set
            {
                this.robotField = value;
            }
        }
    }
}
