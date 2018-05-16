using System;
using System.Net;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;

namespace LIMPFileReader.Formats.Mzxml
{
    [Serializable]
    [XmlRoot("peaks")]
    public class Peaks
    {
        private scanPeaksPrecision precisionField;

        private string byteOrderField;

        private string pairOrderField;

        private byte[] valueField;
        private List<byte> valuesListField;

        public Peaks()
        {
            this.byteOrderField = "network";
            this.precisionField = scanPeaksPrecision.Item64;
            this.pairOrderField = "m/z-int";
            MzIntPeaks = new List<DataPoint>();
            valuesListField = new List<byte>();
        }
        [XmlAttribute("precision")]
        public scanPeaksPrecision Precision
        {
            get
            {
                return this.precisionField;
            }
            set
            {
                this.precisionField = value;
            }
        }
        [XmlAttribute("byteOrder")]
        public string ByteOrder
        {
            get
            {
                return this.byteOrderField;
            }
            set
            {
                this.byteOrderField = value;
            }
        }
        [XmlAttribute("pairOrder")]
        public string PairOrder
        {
            get
            {
                return this.pairOrderField;
            }
            set
            {
                this.pairOrderField = value;
            }
        }

        [XmlText(DataType = "base64Binary")]
        public byte[] Value
        {
            get
            {
                if (this.valuesListField.Count == 0)
                {
                    for (var i = 0; i < MzIntPeaks.Count; i++)
                    {
                        var dp = MzIntPeaks[i];
                        SetValue(dp.Mz, dp.Intensity);
                    }
                }
                return this.valuesListField.ToArray();
            }
            set
            {
                //this.valueField = value;
                ValuesLIst = value.ToList<byte>();
            }
        }
        [XmlIgnore]
        public List<byte> ValuesLIst
        {
            get
            {
                return this.valuesListField;
            }
            set
            {
                this.valuesListField = value;
                if (MzIntPeaks.Count > 0) return;
                for(var i = 0; i < value.Count; )
                {
                    var dp = new DataPoint();
                    var rb1 = value.GetRange(i, sizeof(Int64));
                    var rb2 = value.GetRange(i + sizeof(Int64), sizeof(Int64));
                    switch (this.PairOrder.ToLower().Trim())
                    {
                        case "m/z-int":
                            dp.Mz = NetworkOrderToDouble(rb1.ToArray());
                            dp.Intensity = NetworkOrderToDouble(rb2.ToArray());
                            break;
                        case "int-m/z":
                            dp.Mz = NetworkOrderToDouble(rb2.ToArray());
                            dp.Intensity = NetworkOrderToDouble(rb1.ToArray());
                            break;
                        default:
                            throw new Exception(string.Format("The pair order \"{0}\" is unknown.", this.PairOrder));
                    }
                    MzIntPeaks.Add(dp);
                    i = i + 2 * sizeof(Int64);
                }
            }
        }

        private float NetworkOrderToSingle(byte[] network)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(network);
            }
            return BitConverter.ToSingle(network, 0);
        }

        private double NetworkOrderToDouble(byte[] network)
        {
            //byte[] bytes = BitConverter.GetBytes(network);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(network);
            }
            return BitConverter.ToDouble(network, 0);
        }

        [XmlIgnore()]
        public List<DataPoint> MzIntPeaks { get; set; }

        internal void SetValue(double mz, double current)
        {
            var mzHost = BitConverter.GetBytes(mz);
            //for(int i = 0; i < mzHost.Length; i++)
            //{
            //    var elNetwork = IPAddress.HostToNetworkOrder((long)mzHost.GetValue(i));
            //    valuesListField.Add(elNetwork);
            //}
            valuesListField.AddRange(HostToNetworkOrder(mz));
            valuesListField.AddRange(HostToNetworkOrder(current));
        }

        //internal void SetValue()
        //{
        //    foreach(var peak in MzIntPeaks)
        //    {
        //        var mz = BitConverter.GetBytes(peak.Mz);
        //        var inten = peak.Intensity;
        //        for(var i = 0; i < mz.Length; i++)
        //        {
        //            var curBit = (long)mz.GetValue(i);
        //            var netBit = IPAddress.HostToNetworkOrder(curBit);
        //            valuesListField.Add(Convert.ToByte(netBit));
        //        }
        //        //var mzNet = 
        //    }
        //    string val = Convert.ToBase64String(valuesListField.ToArray());
        //    //var mzHost = BitConverter.GetBytes(mz);
        //    //for (int i = 0; i < mzHost.Length; i++)
        //    //{
        //    //    var elNetwork = IPAddress.HostToNetworkOrder((long)mzHost.GetValue(i));
        //    //    valuesListField.Add(elNetwork);
        //    //}
        //    //valuesListField.AddRange();
        //    //valuesListField.AddRange(BitConverter.GetBytes(current));
        //}
        /// <summary>
        /// Convert a float to network order
        /// </summary>
        /// <param name="host">Float to convert</param>
        /// <returns>Float in network order</returns>
        public static Int32[] HostToNetworkOrder(float host)
        {
            byte[] bytes = BitConverter.GetBytes(host);
            var retList = new List<Int32>();
            foreach(var b in bytes)
            {
                var t = IPAddress.HostToNetworkOrder((Int32)b);
                retList.Add((byte)t);
            }
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(bytes);

            return retList.ToArray();
        }
        /// <summary>
        /// Convert a float to network order
        /// </summary>
        /// <param name="host">Float to convert</param>
        /// <returns>Float in network order</returns>
        public static byte[] HostToNetworkOrder(double host)
        {
            byte[] bytes = BitConverter.GetBytes(host);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }
        /// <summary>
        /// Convert a float to host order
        /// </summary>
        /// <param name="network">Float to convert</param>
        /// <returns>Float in host order</returns>
        public static double NetworkToHostOrder(double network)
        {
            if (BitConverter.IsLittleEndian)
            {
                byte[] bytes = BitConverter.GetBytes(network);
                Array.Reverse(bytes);
                return BitConverter.ToDouble(bytes, 0);
            }
            return network;
        }
    }
}
