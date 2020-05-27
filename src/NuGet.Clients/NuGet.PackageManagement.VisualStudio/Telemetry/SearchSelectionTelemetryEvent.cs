// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using NuGet.Common;
using NuGet.Versioning;

namespace NuGet.PackageManagement.Telemetry
{
    public class SearchSelectionTelemetryEvent : TelemetryEvent
    {
        private SearchSelectionTelemetryEvent() :
            base("SearchSelection")
        {
        }

        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "We require lowercase package names in telemetry so that the hashes are consistent")]
        public static void Emit(
            Guid parentId,
            int recommendedCount,
            int itemIndex,
            string packageId,
            NuGetVersion packageVersion)
        {
            var telemetryEvent = new SearchSelectionTelemetryEvent();

            telemetryEvent["ParentId"] = parentId.ToString();
            telemetryEvent["RecommendedCount"] = recommendedCount;
            telemetryEvent["ItemIndex"] = itemIndex;
            telemetryEvent.AddPiiData("PackageId", packageId.ToLowerInvariant());
            telemetryEvent.AddPiiData("PackageVersion", packageVersion.ToNormalizedString().ToLowerInvariant());

            telemetryEvent.Emit();
        }
    }
}
