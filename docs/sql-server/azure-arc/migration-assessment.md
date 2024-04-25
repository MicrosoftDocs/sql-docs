---
title: Assess migration readiness (preview)
description: Explains how SQL Server enabled by Azure Arc helps assess the SQL Servers migrating to Azure SQL.
author: pochiraju
ms.author: rajpo
ms.reviewer: mikeray, randolphwest
ms.date: 02/13/2024
ms.topic: conceptual
---

# Select the optimal Azure SQL target using Migration assessment (preview) - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

SQL Server enabled by Azure Arc, automatically produces an assessment for migration to Azure. This assessment plays a vital role in the success of your cloud migration and modernization journey. Azure Arc simplifies the discovery process and readiness assessment for migration.

The assessment:

- Provides cloud readiness, identifies risks, and offers mitigation strategies.
- Provides the specific service tier and Azure SQL configuration (SKU size) for each Azure SQL deployment option, best fits the workload needs.
- Is generated automatically.
- Runs continuously on a default schedule of once per week.
- Is free, and available for all SQL Server editions.

You can obtain a migration assessment for SQL Servers located anywhere:

- In your data center
- At edge site locations, such as retail stores
- Any public cloud or hosting provider

The assessment is available for any instance of SQL Server enabled by Azure Arc.

[!INCLUDE [azure-arc-sql-preview](includes/azure-arc-sql-preview.md)]

## SQL Server migration assessment features

**Azure SQL readiness assessment:** Evaluate and measure the readiness of SQL Servers for migration to Azure SQL. This process

- Discovers and assesses the SQL Server instance and databases
- Pinpoints SQL Server workloads that are ready for migration
- Identifies potential compatibility issues with the target environment
- Assesses migration risks
- Provides recommendations to mitigate these risks

**Azure SQL size recommendations:** Provides best-fit recommendations, including the service tier and right-sizing based on performance history.

## Prerequisites

To assess SQL Server, the SQL Server instance needs to:

- Run on Windows-based SQL Server instance is connected to Azure. Follow the instructions at [Automatically connect SQL Server machines to Azure Arc.](automatically-connect.md)

- Have Azure Extension for SQL Server (`WindowsAgent.SqlServer`) version **1.1.2594.118** or later. 

   Learn how to [check the ](/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions)**[Azure Extension for SQL Server](/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions)**[ version and update to the latest.](/azure/azure-arc/servers/manage-vm-extensions-portal#upgrade-extensions)

- The server has connectivity to telemetry.{region}.arcdataservices.com (for more information, see [Network Requirements ](/azure/azure-arc/servers/network-requirements?tabs=azure-cloud))

- To view the assessment reports in the Azure Portal, you must have Contributor access or higher to the SQL Server - Azure Arc resource.

## Permissions

The Azure SQL extension for SQL Server performs the assessment data collection by default under the service account, NT AUTHORITY\SYSTEM. However, you can [configure the agent extension service to run with an account that has the least privileges](configure-least-privilege.md).

## View migration assessment results

1. Sign into the [Azure portal](https://portal.azure.com/) and go to your [SQL Server enabled by Azure Arc](https://portal.azure.com/#view/Microsoft_Azure_HybridCompute/AzureArcCenterBlade/~/sqlServers)

1. Open your SQL Server resource and select **Assessments (preview)** under **Migration** folder in the left pane.

   :::image type="content" source="media/migration-assessment/assessment-main.png" alt-text="Screenshot showing how to get to the SQL Server migration assessment report an SQL Server resource.":::

## Review readiness

The assessment indicates the different migration strategies that you can consider for your SQL Server deployments:

- Azure SQL managed instances
- SQL Server on Azure virtual machines
- Azure SQL databases

Review the readiness for target deployment types and the Azure SQL size recommendation. The readiness is based on the performance evaluation for the SQL Server instances and databases that are marked ready or ready with conditions.

- **Ready**: The SQL Server instance or database is ready to be migrated to the specific Azure SQL target deployment option without any migration blockers. Should there be any warnings, address these issues using the provided remediation guidance.

- **Not ready**: The assessment couldn't find a SQL Server on Azure Virtual Machine, Azure SQL Managed Instance, or Azure SQL Database configuration meeting the desired compatibility, configuration, and performance characteristics. Select the hyperlink to review the recommendation to make the SQL Server instance/databases ready for the desired target deployment type.

   :::image type="content" source="media/migration-assessment/not-ready.png" alt-text="Screenshot showing how to get to the mitigation guidance when SQL Server isn't ready to migrate.":::

- **Unknown**: Azure Migrate can't assess readiness. This result can happen because the discovery is in progress or there are issues during discovery that need to be fixed. Check the notifications pane. If the issue persists, contact [Microsoft support](https://support.microsoft.com).

## Review confidence rating

SQL Migration assessment assigns a confidence rating to SQL Server migration assessment based on the availability of the performance/utilization data points needed to compute the assessment for all the assessed SQL instances and databases. Rating is from one star (lowest) to five stars (highest). The confidence rating is projected to reach its peak (five stars) approximately after 30 days of continuous data collection. It should increase by one star for each week of data collection. The confidence rating helps you estimate the reliability of size recommendations in the assessment. Confidence ratings are as follows:

| **Data point availability**	 | **Confidence rating** |
| ---------------------------- | --------------------- |
|0%-20% |1 star|
|21%-40%|2 stars|
|41%-60%|3 stars|
|61%-80%|4 stars|
|81%-100%|5 stars|

## Performance-based Azure SQL configuration (SKU size) calculation

The assessment aggregates all the configuration and performance data and tries to find the best match across various Azure SQL service tiers and configurations and picks a configuration that can match or exceed the SQL instance performance requirements, optimizing the cost.

SQL Server extension for Azure collects performance data for compute settings with these steps

1. The assessment collects a performance data sample point every 30 seconds.
1. Aggregates the sample data points collected every 30 seconds over 10 minutes. To create the data point, the size assessment selects the peak values from all samples. It gets the max, mean and variance for performance each counter.
1. We store all the 10-minute data points for the last month.
1. The assessment identifies the appropriate data point to use for right-sizing. Identification is based on the 95% percentile values for performance history.

   For example, if the performance history is one week, the assessment sorts the 10-minute sample points for the last week. It sorts them in ascending order and picks the 95th percentile value for right-sizing.  The 95th percentile value makes sure you ignore any outliers.

1. The high level metrics collected to decide the optimal Azure SQL target include.

    [!INCLUDE [extension-logs](includes/extension-logs.md)]

## Disable migration assessment

The SQL Server migration assessment automatically gets generated for every SQL Server enabled by Arc. You can disable the assessment by using, **Disable** option on the top menu bar.

## Re-enable migration assessment

Use **Enable Assessment** button to re-enable the SQL Server migration assessment.

## Limitations

- SQL Server migration assessment is currently limited to SQL Server running on Windows machines, doesn't  apply to SQL on Linux machines.
- SQL Server running on Windows Server 2012 and older versions aren't supported.
- SQL Server version must be 2012 or above.
- Failover cluster instances (FCI) aren't supported at this time.

## Known issues

 When the `xp_commandShell` is enabled and utilized, it's recorded as a warning for SQL Managed Instance. This issue is considered a migration blocker. It disrupts the functionality of the object that specifically leverages  `xp_commandShell`. Use the remediation guidance provided in the assessment to mitigate the issue.

## Troubleshooting

Contact [Microsoft support](/azure/azure-portal/supportability/how-to-create-azure-support-request) if you run into any of the issues below.

- The assessment reports are not appearing on the portal, even after the scheduled time of Sunday 11:00 PM (2300), according to the local time of the SQL Server machine.
- Confidence rating doesn't increase after one week of data gathering. The confidence should increase after the first week.

## Related content

- [Assessment rules for SQL Server to Azure SQL Managed Instance migration](/azure/azure-sql/migration-guides/managed-instance/sql-server-to-sql-managed-instance-assessment-rules)
- [Assessment rules for SQL Server to Azure SQL Database migration](/azure/azure-sql/migration-guides/database/sql-server-to-sql-database-assessment-rules)
- [Migrate SQL Server to Azure SQL](/azure/dms/dms-overview)
- [SQL Server enabled by Azure Arc](overview.md)
- [Automatically connect SQL Server machines to Azure Arc](automatically-connect.md)
