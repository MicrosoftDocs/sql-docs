---
title: Use Azure Resource Health to monitor database health
description: Use Azure Resource Health to monitor Azure SQL Database and Azure SQL Managed Instance health, helps you diagnose and get support when an Azure issue impacts your SQL resources.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, mathoma
ms.date: 08/28/2023
ms.service: sql-db-mi
ms.subservice: performance
ms.topic: conceptual
ms.custom: sqldbrb=2
---
# Use Resource Health to troubleshoot connectivity for Azure SQL Database and Azure SQL Managed Instance
[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

[Resource Health](/azure/service-health/resource-health-overview#get-started) for Azure SQL Database and Azure SQL Managed Instance helps you diagnose and get support when an Azure issue impacts your SQL resources. It informs you about the current and past health of your resources and helps you mitigate issues. The **Resource health** page provides technical support when you need help with Azure service issues.

:::image type="content" source="./media/resource-health-to-troubleshoot-connectivity/sql-resource-health-overview.jpg" alt-text="A screenshot of the Azure portal showing the Resource Health page for an Azure SQL Database.":::

## Health checks

**Resource health** determines the health of your SQL resource by examining the success and failure of logins to the resource. Currently, **Resource health** for your SQL Database resource only examines login failures due to system error and not user error. The health status is updated every 1 to 2 minutes.

## Health states

### Available

A status of **Available** means that **Resource health** has not detected login failures due to system errors on your SQL resource.

:::image type="content" source="./media/resource-health-to-troubleshoot-connectivity/sql-resource-health-available.jpg" alt-text="A screenshot of the Azure portal showing the status message for the state of Available.":::

### Degraded

A status of **Degraded** means that **Resource health** has detected a majority of successful logins, but some failures as well. These are most likely transient login errors. To reduce the impact of connection issues caused by transient login errors, implement [retry logic](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors) in your code.

:::image type="content" source="./media/resource-health-to-troubleshoot-connectivity/sql-resource-health-degraded.jpg" alt-text="A screenshot of the Azure portal showing the status message for the state of Degraded.":::

### Unavailable

A status of **Unavailable** means that **Resource health** has detected consistent login failures to your SQL resource. If your resource remains in this state for an extended period of time, contact support.

:::image type="content" source="./media/resource-health-to-troubleshoot-connectivity/sql-resource-health-unavailable.jpg" alt-text="A screenshot of the Azure portal showing the status message for the state of Unavailable.":::

### Unknown

The health status of **Unknown** indicates that **Resource health** hasn't received information about this resource for more than 10 minutes. Although this status isn't a definitive indication of the state of the resource, it is an important data point in the troubleshooting process. If the resource is running as expected, the status of the resource will change to Available after a few minutes. If you're experiencing problems with the resource, the Unknown health status might suggest that an event in the platform is affecting the resource.

:::image type="content" source="./media/resource-health-to-troubleshoot-connectivity/sql-resource-health-unknown.jpg" alt-text="A screenshot of the Azure portal showing the status message for the state of Unknown.":::

## Historical information

You can access up to 30 days of health history in the **Health history** section of **Resource health**. The section will also contain the reason (when available) for downtimes. Currently, Azure shows the downtime for your database resource at a two-minute granularity. The actual downtime is likely less than a minute. The average is 8 seconds.

### Downtime reasons

When your database experiences downtime, analysis is performed to determine a reason. When available, the downtime reason is reported in the **Health history** section of **Resource health**. Downtime reasons are typically published within 45 minutes after an event.

#### Planned maintenance

The Azure infrastructure periodically performs planned maintenance – the upgrade of hardware or software components in the datacenter. While the database undergoes maintenance, Azure SQL may terminate some existing connections and refuse new ones. The login failures experienced during planned maintenance are typically transient, and [retry logic for occasional network errors](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors) helps reduce the impact. If you continue to experience login errors, contact support.

#### Reconfiguration

Reconfigurations are considered transient conditions and are expected from time to time. These events can be triggered by load balancing or software/hardware failures. Any client production application that connects to a cloud database should implement a robust connection [retry logic for transient errors](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors), as it would help mitigate these situations and should generally make the errors transparent to the end user.

## Next steps

- Learn more about [retry logic for transient errors](troubleshoot-common-connectivity-issues.md#retry-logic-for-transient-errors).
- [Troubleshoot, diagnose, and prevent SQL connection errors](troubleshoot-common-connectivity-issues.md).
- Learn more about [configuring Resource Health alerts](/azure/service-health/resource-health-alert-arm-template-guide).
- Get an overview of [Resource Health](/azure/service-health/resource-health-overview).
- Review [Resource Health FAQ](/azure/service-health/resource-health-faq).
