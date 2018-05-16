using System;
using System.Xml.Serialization;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("maldi")]
    public class ScanMaldi
    {
        private string plateIDField;

        private string spotIDField;

        private string laserShootCountField;

        private string laserFrequencyField;

        private string laserIntensityField;

        private bool collisionGasField;

        private bool collisionGasFieldSpecified;

        [XmlAttribute("plateID")]
        public string PlateID
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
        [XmlAttribute("spotID")]
        public string SpotID
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
        [XmlAttribute("laserShootCount")]
        public string LaserShootCount
        {
            get
            {
                return this.laserShootCountField;
            }
            set
            {
                this.laserShootCountField = value;
            }
        }

        [XmlAttribute("laserFrequency")]
        public string LaserFrequency
        {
            get
            {
                return this.laserFrequencyField;
            }
            set
            {
                this.laserFrequencyField = value;
            }
        }
        [XmlAttribute("laserIntensity")]
        public string LaserIntensity
        {
            get
            {
                return this.laserIntensityField;
            }
            set
            {
                this.laserIntensityField = value;
            }
        }
        [XmlAttribute("collisionGas")]
        public bool CollisionGas
        {
            get
            {
                return this.collisionGasField;
            }
            set
            {
                this.collisionGasField = value;
            }
        }

        [XmlIgnore()]
        public bool CollisionGasSpecified
        {
            get
            {
                return this.collisionGasFieldSpecified;
            }
            set
            {
                this.collisionGasFieldSpecified = value;
            }
        }
    }
}
