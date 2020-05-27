// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using NuGet.Common;
using NuGet.PackageManagement;

namespace NuGet.VisualStudio
{
    /// <summary>
    /// Telemetry event data for nuget restore operation.
    /// </summary>
    public class RestoreTelemetryEvent : ActionEventBase
    {
        public static string RestoreOperationChecks = nameof(RestoreOperationChecks);
        public static string PackagesConfigRestore = nameof(PackagesConfigRestore);
        public static string SolutionDependencyGraphSpecCreation = nameof(SolutionDependencyGraphSpecCreation);
        public static string PackageReferenceRestoreDuration = nameof(PackageReferenceRestoreDuration);

        private RestoreTelemetryEvent(
            string operationId,
            string[] projectIds,
            DateTimeOffset startTime,
            NuGetOperationStatus status,
            int packageCount,
            DateTimeOffset endTime,
            double duration)
            : base(
                  RestoreActionEventName,
                  operationId,
                  projectIds,
                  startTime,
                  status,
                  packageCount,
                  endTime,
                  duration)
        {
        }

        public static RestoreTelemetryEvent Emit(
            string operationId,
            string[] projectIds,
            RestoreOperationSource source,
            DateTimeOffset startTime,
            NuGetOperationStatus status,
            int packageCount,
            int noOpProjectsCount,
            DateTimeOffset endTime,
            double duration,
            IntervalTracker intervalTimingTracker)
        {
            var telemetryEvent = new RestoreTelemetryEvent(
                  operationId,
                  projectIds,
                  startTime,
                  status,
                  packageCount,
                  endTime,
                  duration);

            telemetryEvent[nameof(OperationSource)] = source;
            telemetryEvent[nameof(NoOpProjectsCount)] = noOpProjectsCount;

            foreach (var (intervalName, intervalDuration) in intervalTimingTracker.GetIntervals())
            {
                telemetryEvent[intervalName] = intervalDuration;
            }

            telemetryEvent.Emit();
            return telemetryEvent;
        }

        public const string RestoreActionEventName = "RestoreInformation";

        public RestoreOperationSource OperationSource => (RestoreOperationSource)base[nameof(OperationSource)];

        public int NoOpProjectsCount => (int)base[nameof(NoOpProjectsCount)];
    }
}
