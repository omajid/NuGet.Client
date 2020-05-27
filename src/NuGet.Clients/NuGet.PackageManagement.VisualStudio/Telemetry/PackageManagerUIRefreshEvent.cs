// All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using NuGet.Common;

namespace NuGet.PackageManagement.Telemetry
{
    public sealed class PackageManagerUIRefreshEvent : TelemetryEvent
    {
        private const string EventName = "PMUIRefresh";

        private PackageManagerUIRefreshEvent() :
            base(EventName)
        {
        }

        public static void Emit(
            INuGetTelemetryService telemetryService,
            Guid parentId,
            bool isSolutionLevel,
            RefreshOperationSource refreshSource,
            RefreshOperationStatus refreshStatus,
            string tab,
            TimeSpan timeSinceLastRefresh)
        {
            var telemetryEvent = new PackageManagerUIRefreshEvent();

            telemetryEvent["ParentId"] = parentId.ToString();
            telemetryEvent["IsSolutionLevel"] = isSolutionLevel;
            telemetryEvent["RefreshSource"] = refreshSource;
            telemetryEvent["RefreshStatus"] = refreshStatus;
            telemetryEvent["Tab"] = tab;
            telemetryEvent["TimeSinceLastRefresh"] = timeSinceLastRefresh.TotalMilliseconds;

            telemetryEvent.Emit(telemetryService);
        }
    }

    public enum RefreshOperationSource
    {
        ActionsExecuted,
        CacheUpdated,
        CheckboxPrereleaseChanged,
        ClearSearch,
        ExecuteAction,
        FilterSelectionChanged,
        PackageManagerLoaded,
        PackageSourcesChanged,
        ProjectsChanged,
        RestartSearchCommand,
        SourceSelectionChanged,
        PackagesMissingStatusChanged,
    }

    public enum RefreshOperationStatus
    {
        Success,
        NotApplicable,
        NoOp,
    }
}
