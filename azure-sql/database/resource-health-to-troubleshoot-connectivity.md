---
title: Use Azure Resource Health to monitor database health
description: Use Azure Resource Health to monitor Azure SQL Database health, helps you diagnose and get support when an Azure issue impacts your resources.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 01/04/2024
ms.service: azure-sql-database
ms.subservice: performance
ms.topic: conceptual
ms.custom:
  - sqldbrb=2
---
# Use Resource Health to troubleshoot connectivity for Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> - [Azure SQL Database](resource-health-to-troubleshoot-connectivity.md?view=azuresql-db&preserve-view=true)
> - [Azure SQL Managed Instance](../managed-instance/resource-health-to-troubleshoot-connectivity.md?view=azuresql-mi&preserve-view=true)

[Resource Health](/azure/service-health/resource-health-overview#get-started) for Azure SQL Database helps you diagnose and get support when an Azure issue impacts your resources. It informs you about the current and past health of your resources and helps you mitigate issues. The **Resource health** page provides technical support when you need help with Azure service issues.

:::image type="content" source="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-overview.jpg" alt-text="Screenshot of the Azure portal showing the Resource Health page for an Azure SQL Database." lightbox="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-overview.jpg":::

## Health checks

**Resource health** determines the health of your SQL database by examining the success and failure of logins to the resource. Currently, **Resource health** for your SQL Database resource only examines login failures due to system error and not user error. The health status is updated every 1 to 2 minutes.

## Health states

### Available

A status of **Available** means that **Resource health** didn't detect login failures due to system errors on your SQL database, or that there were some login failures but they didn't meet the alerting threshold. The following sections provide more details on the alerting threshold.

:::image type="content" source="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-available.jpg" alt-text="Screenshot of the Azure portal showing the status message for the state of Available." lightbox="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-available.jpg":::

### Degraded

A status of **Degraded** means that, in any two of the last three minutes, **Resource health** detected:

- a majority of successful logins, but there was more than one login failure (due to system errors) as well, or
- more than one login failure (due to system errors) but there were fewer than six total login attempts.

These are most likely transient login errors. To reduce the effect of connection issues caused by transient login errors, implement [retry logic](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors) in your code.

:::image type="content" source="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-degraded.jpg" alt-text="Screenshot of the Azure portal showing the status message for the state of Degraded." lightbox="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-degraded.jpg":::

### Unavailable

A status of **Unavailable** means that **Resource health** detected that there were more than five login attempts in the last minute, and more than a quarter of them were failing for system reasons. If your resource remains in this state for an extended period of time, contact Microsoft Support.

:::image type="content" source="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-unavailable.jpg" alt-text="Screenshot of the Azure portal showing the status message for the state of Unavailable." lightbox="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-unavailable.jpg":::

### Unknown

The health status of **Unknown** indicates that **Resource health** hasn't received information about this resource for more than 10 minutes. Although this status isn't a definitive indication of the state of the resource, it's an important data point in the troubleshooting process. If the resource is running as expected, the status of the resource will change to Available after a few minutes. If you're experiencing problems with the resource, the Unknown health status might suggest that an event in the platform is affecting the resource.

:::image type="content" source="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-unknown.jpg" alt-text="Screenshot of the Azure portal showing the status message for the state of Unknown." lightbox="media/resource-health-to-troubleshoot-connectivity/sql-resource-health-unknown.jpg":::

## Alert time

The time shown by the **Resource health** alert doesn't line up with the times of the login failures that caused the alert. This is because it takes several minutes for the telemetry to be collected and analyzed, to determine that there's a **Resource health** issue. So, the time indicated in the **Resource health** alert will be several minutes after the login failures.

Additionally, the time interval when the login failures were occurring can often be shorter than the time interval in the resource health alert.

## Historical information

You can access up to 30 days of health history in the **Health history** section of **Resource health**. The section also contains the reason (when available) for downtimes. Currently, Azure shows the downtime for your database resource at a two-minute granularity. The actual downtime is likely less than a minute. The average is 8 seconds.

## Downtime reasons

When your database experiences downtime, analysis is performed to determine a reason. When available, the downtime reason is reported in the **Health history** section of **Resource health**. Downtime reasons are typically published within 45 minutes after an event.

### Select a maintenance window

You can configure your [maintenance window](maintenance-window.md?view=azuresql-db&preserve-view=true) to make impactful maintenance events predictable and less disruptive for your workload. The maintenance window feature helps you plan around predictable upgrades or scheduled maintenance. [Advance notifications](advance-notifications.md?view=azuresql-db&preserve-view=true) are available for databases configured to use a non-default maintenance window. Advance notifications enable customers to configure notifications to be sent up to 24 hours in advance of any planned event.

### Planned maintenance

The Azure infrastructure periodically performs planned maintenance – the upgrade of hardware or software components in the datacenter. While the database undergoes maintenance, Azure SQL can terminate some existing connections and refuse new ones. The login failures experienced during planned maintenance are typically transient, and [retry logic for occasional network errors](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors) helps reduce the effect. If you continue to experience login errors, contact support.

### Reconfiguration

Reconfigurations are considered transient conditions and are expected from time to time. These events can be triggered by load balancing or software/hardware failures. Any client production application that connects to a cloud database should implement a robust connection [retry logic for transient errors](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors), as it would help mitigate these situations and should generally make the errors transparent to the end user.

## Related content

- Learn more about [retry logic for transient errors](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors)
- [Troubleshoot, diagnose, and prevent SQL connection errors](troubleshoot-common-connectivity-issues.md?view=azuresql-db&preserve-view=true)
- Learn more about [configuring Resource Health alerts](/azure/service-health/resource-health-alert-arm-template-guide)
- Get an overview of [Resource Health](/azure/service-health/resource-health-overview)
- Review [Resource Health FAQ](/azure/service-health/resource-health-faq)
- Configure a [maintenance window](maintenance-window.md?view=azuresql-db&preserve-view=true) and [advance notifications](advance-notifications.md?view=azuresql-db&preserve-view=true)
