using System;
using KSPModAdmin.Core.Utils.Logging;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Helper class to compare versions.
    /// </summary>
    public class VersionComparer
    {
        /// <summary>
        /// Possible results of the CompareVersions function.
        /// </summary>
        public enum Result
        {
            AisSmallerB = -1,
            AIsEqualsB = 0,
            AisBiggerB = 1,
        }

        /// <summary>
        /// Compares the two version.
        /// Tries to create a Version class from both versions and compares them.
        /// If Version class creation failed it will be a string compare only.
        /// </summary>
        /// <returns>One of the VersionComparer.Result enum options depending on the result.</returns>
        public static Result CompareVersions(string versionA, string versionB)
        {
            if (string.IsNullOrEmpty(versionA) && string.IsNullOrEmpty(versionB))
                return Result.AIsEqualsB;
            else if (!string.IsNullOrEmpty(versionA) && string.IsNullOrEmpty(versionB))
                return Result.AisBiggerB;
            else if (string.IsNullOrEmpty(versionA) && !string.IsNullOrEmpty(versionB))
                return Result.AisSmallerB;

            try
            {
                Version v1 = new Version(versionA.Replace(",", "."));
                Version v2 = new Version(versionB.Replace(",", "."));
                return (Result)v1.CompareTo(v2);
            }
            catch (Exception ex)
            {
                Log.AddErrorS("Error in VersionComparer.CompareVersions()", ex);
            }

            try
            {
                int i = versionA.CompareTo(versionB);
                if (i == 0)
                    return Result.AIsEqualsB;
                if (i < 0)
                    return Result.AisSmallerB;
                
                return Result.AisBiggerB;
            }
            catch (Exception ex)
            {
                Log.AddErrorS("Error in VersionComparer.CompareVersions()", ex);
            }

            return Result.AIsEqualsB;
        }
    }
}