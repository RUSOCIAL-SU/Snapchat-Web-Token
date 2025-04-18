using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml;
using Org.BouncyCastle.Asn1.Pkcs;
using static SnapchatWebData;

public static class SnapchatWebData
{
    /// <summary>
    /// Enumeration representing different operating system types.
    /// </summary>
    public enum OsType
    {
        UNKNOWN_OS_TYPE = 0,
        OS_IOS = 1,
        OS_ANDROID = 2,
        OS_WEB = 3,
    }

    /// <summary>
    /// Represents the version information of an application.
    /// </summary>
    public class AppVersion
    {
        public VersionNumber VersionInfo { get; set; } = null;
        public string Flavor { get; set; } = "";
        public string Variant { get; set; } = "";

        /// <summary>
        /// Encodes the AppVersion instance into a byte array.
        /// </summary>
        public byte[] Encode()
        {
            // Implement encoding logic here if needed.
            return new byte[0];
        }

        /// <summary>
        /// Creates an AppVersion instance from an object.
        /// </summary>
        public static AppVersion FromObject(dynamic obj)
        {
            if (obj == null)
                return null;

            var appVersion = new AppVersion
            {
                Flavor = obj.flavor != null ? obj.flavor.ToString() : "",
                Variant = obj.variant != null ? obj.variant.ToString() : "",
            };

            if (obj.versionNumber != null)
            {
                appVersion.VersionInfo = VersionNumber.FromObject(obj.versionNumber);
            }

            return appVersion;
        }

        /// <summary>
        /// Converts the AppVersion instance into a dynamic object.
        /// </summary>
        public dynamic ToObject()
        {
            var obj = new
            {
                versionNumber = VersionInfo?.ToObject(),
                flavor = Flavor,
                variant = Variant,
            };
            return obj;
        }

        /// <summary>
        /// Nested class representing the version number components.
        /// </summary>
        public class VersionNumber
        {
            public int Major { get; set; } = 0;
            public int Minor { get; set; } = 0;
            public int Patch { get; set; } = 0;
            public int Build { get; set; } = 0;

            /// <summary>
            /// Encodes the VersionNumber instance into a byte array.
            /// </summary>
            public byte[] Encode()
            {
                // Implement encoding logic here if needed.
                return new byte[0];
            }

            /// <summary>
            /// Creates a VersionNumber instance from an object.
            /// </summary>
            public static VersionNumber FromObject(dynamic obj)
            {
                if (obj == null)
                    return null;

                var versionNumber = new VersionNumber
                {
                    Major = obj.major != null ? (int)obj.major : 0,
                    Minor = obj.minor != null ? (int)obj.minor : 0,
                    Patch = obj.patch != null ? (int)obj.patch : 0,
                    Build = obj.build != null ? (int)obj.build : 0,
                };

                return versionNumber;
            }

            /// <summary>
            /// Converts the VersionNumber instance into a dynamic object.
            /// </summary>
            public dynamic ToObject()
            {
                var obj = new
                {
                    major = Major,
                    minor = Minor,
                    patch = Patch,
                    build = Build,
                };
                return obj;
            }
        }
    }

    /// <summary>
    /// Represents a metric with associated values and custom dimensions.
    /// </summary>
    public class Metric
    {
        public string PartitionName { get; set; } = "";
        public string MetricName { get; set; } = "";
        public Dictionary<string, string> CustomDimensions { get; set; } =
            new Dictionary<string, string>();
        public List<long> Values { get; set; } = new List<long>();

        /// <summary>
        /// Encodes the Metric instance into a byte array.
        /// </summary>
        public byte[] Encode()
        {
            // Implement encoding logic here if needed.
            return new byte[0];
        }

        /// <summary>
        /// Creates a Metric instance from an object.
        /// </summary>
        public static Metric FromObject(dynamic obj)
        {
            if (obj == null)
                return null;

            var metric = new Metric
            {
                PartitionName = obj.partitionName != null ? obj.partitionName.ToString() : "",
                MetricName = obj.metricName != null ? obj.metricName.ToString() : "",
            };

            if (obj.customDimensions != null)
            {
                foreach (var key in obj.customDimensions.GetDynamicMemberNames())
                {
                    metric.CustomDimensions[key] = obj.customDimensions[key].ToString();
                }
            }

            if (obj.values != null)
            {
                foreach (var value in obj.values)
                {
                    metric.Values.Add((long)value);
                }
            }

            return metric;
        }

        /// <summary>
        /// Converts the Metric instance into a dynamic object.
        /// </summary>
        public dynamic ToObject()
        {
            var obj = new
            {
                partitionName = PartitionName,
                metricName = MetricName,
                customDimensions = CustomDimensions,
                values = Values,
            };
            return obj;
        }
    }

    /// <summary>
    /// Represents a frame of metrics, including timers, counters, and levels.
    /// </summary>
    public class MetricFrame
    {
        public List<Metric> Timers { get; set; } = new List<Metric>();
        public List<Metric> Counters { get; set; } = new List<Metric>();
        public List<Metric> Levels { get; set; } = new List<Metric>();
        public uint ProtocolVersion { get; set; } = 0;
        public ulong BufferStartTimestampMillis { get; set; } = 0;
        public ulong BufferEndTimestampMillis { get; set; } = 0;
        public AppVersion AppVersion { get; set; } = null;
        public OsType ClientOsType { get; set; } = OsType.UNKNOWN_OS_TYPE;
        public string UserId { get; set; } = "";

        /// <summary>
        /// Encodes the MetricFrame instance into a byte array.
        /// </summary>
        public byte[] Encode()
        {
            // Implement encoding logic here if needed.
            return new byte[0];
        }

        /// <summary>
        /// Creates a MetricFrame instance from an object.
        /// </summary>
        public static MetricFrame FromObject(dynamic obj)
        {
            if (obj == null)
                return null;

            var metricFrame = new MetricFrame
            {
                ProtocolVersion = obj.protocolVersion != null ? (uint)obj.protocolVersion : 0,
                BufferStartTimestampMillis =
                    obj.bufferStartTimestampMillis != null
                        ? (ulong)obj.bufferStartTimestampMillis
                        : 0,
                BufferEndTimestampMillis =
                    obj.bufferEndTimestampMillis != null ? (ulong)obj.bufferEndTimestampMillis : 0,
                UserId = obj.userId != null ? obj.userId.ToString() : "",
            };

            if (obj.appVersion != null)
            {
                metricFrame.AppVersion = AppVersion.FromObject(obj.appVersion);
            }

            if (obj.clientOsType != null)
            {
                metricFrame.ClientOsType = Enum.TryParse(
                    obj.clientOsType.ToString(),
                    out OsType osType
                )
                    ? osType
                    : OsType.UNKNOWN_OS_TYPE;
            }

            if (obj.timers != null)
            {
                foreach (var timer in obj.timers)
                {
                    metricFrame.Timers.Add(Metric.FromObject(timer));
                }
            }

            if (obj.counters != null)
            {
                foreach (var counter in obj.counters)
                {
                    metricFrame.Counters.Add(Metric.FromObject(counter));
                }
            }

            if (obj.levels != null)
            {
                foreach (var level in obj.levels)
                {
                    metricFrame.Levels.Add(Metric.FromObject(level));
                }
            }

            return metricFrame;
        }

        /// <summary>
        /// Converts the MetricFrame instance into a dynamic object.
        /// </summary>
        public dynamic ToObject()
        {
            var obj = new
            {
                timers = Timers.ConvertAll(timer => timer.ToObject()),
                counters = Counters.ConvertAll(counter => counter.ToObject()),
                levels = Levels.ConvertAll(level => level.ToObject()),
                protocolVersion = ProtocolVersion,
                bufferStartTimestampMillis = BufferStartTimestampMillis,
                bufferEndTimestampMillis = BufferEndTimestampMillis,
                appVersion = AppVersion?.ToObject(),
                clientOsType = ClientOsType.ToString(),
                userId = UserId,
            };
            return obj;
        }
    }
}

public class SnapchatWebEncryption
{
    public static string Encrypt(
        string data,
        long timestamp,
        string msgFormat = "string",
        string outFormat = "hex"
    )
    {
        // Adjust the input format based on msgFormat
        if (msgFormat == "hex-bytes")
        {
            data = HexBytesToString(data);
        }
        else
        {
            data = Utf8Encode(data);
        }

        // Append the 0x80 byte (from String.fromCharCode(128) in JavaScript)
        data += (char)128;

        // Create a new instance of SHA256
        using (SHA256 sha256 = SHA256.Create())
        {
            // Compute the hash of the data
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));

            // Convert hash to string based on the output format
            if (outFormat == "hex")
            {
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
            else if (outFormat == "base64")
            {
                return Convert.ToBase64String(hashBytes);
            }
            else
            {
                throw new ArgumentException("Unsupported output format");
            }
        }
    }

    // Supporting functions
    public static string HexBytesToString(string e)
    {
        e = e.Replace(" ", "");
        StringBuilder result = new StringBuilder();
        for (int n = 0; n < e.Length; n += 2)
        {
            result.Append((char)Convert.ToInt32(e.Substring(n, 2), 16));
        }
        return result.ToString();
    }

    public static string Utf8Encode(string str)
    {
        byte[] utf8Bytes = Encoding.UTF8.GetBytes(str);
        return Encoding.UTF8.GetString(utf8Bytes);
    }
}
