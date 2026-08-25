---
title: Migration Dashboard
description: Track the SQL Server estate migration journey in the Azure Arc center.
author: ajithkr-ms
ms.author: ajithkr
ms.date: 04/16/2026
ms.topic: how-to
---

# Track migration journey by using migration dashboard - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

The migration dashboard is a convenient view that shows all instances of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] and their migration readiness. [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] automatically produces an assessment for migration to Azure. This assessment plays a vital role in the success of your cloud migration and modernization journey. With this dashboard, you can track the migration journey at scale. The readiness is projected as properties into the Azure management plane, which allows the use of organizational, tagging, and querying capabilities native to Azure.

The dashboard provides:

- An overview of discovered SQL Server instances and databases.
- An overview of the SQL Server instances with generated assessments.
- A migration-readiness summary for each Azure SQL offering.
- Rich filtering capabilities that you can use to tailor the view to your needs.

## Review migration assessment

You can access the migration dashboard in the following ways:

- In the Azure portal, search for **Azure Arc** and go to the Azure Arc center.
- On the left pane, expand **Data services** and go to **SQL servers**.
- Select the **Migration Dashboard** tab.

:::image type="content" source="media/migration-assessment/migration-dashboard.png" alt-text="Screenshot that shows the migration dashboard for SQL Server enabled by Azure Arc." lightbox="media/migration-assessment/migration-dashboard.png":::

### Summary of discovered SQL Server instances and databases

The first section of the dashboard provides an overview of all SQL Server instances and databases that are accessible to you. You can also see the distribution of the instances by version and edition.

:::image type="content" source="media/migration-assessment/dashboard-inventory.png" alt-text="Screenshot that shows the migration dashboard showing SQL Server instances and databases and distribution by version and edition." lightbox="media/migration-assessment/dashboard-inventory.png":::

### Summary of SQL Server migration assessment

This section of the dashboard provides you with an overview of the migration assessment and migration readiness of the instances of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]. You can see how many instances have assessments available. The migration readiness for each Azure SQL offering is shown separately.

:::image type="content" source="media/migration-assessment/dashboard-assessment-overview.png" alt-text="Screenshot that shows the migration dashboard with an overview of migration assessments and readiness of instances and databases for Azure SQL offerings." lightbox="media/migration-assessment/dashboard-assessment-overview.png":::

## Azure Resource Graph query

Azure Resource Graph provides efficient and performant means to query the readiness properties of the SQL Server instances enabled by Azure Arc. Here are some sample queries.

```kusto
resources
 | where type == 'microsoft.azurearcdata/sqlserverinstances'
 | where properties.migration.assessment.assessmentUploadTime > ago(14d) and properties.migration.assessment.enabled == true and isnotnull(parse_json(properties.migration.assessment.skuRecommendationResults))
 | extend azureSqlDatabaseRecommendationStatus = tostring(properties.migration.assessment.skuRecommendationResults.azureSqlDatabase.recommendationStatus)
 | extend azureSqlManagedInstanceRecommendationStatus = tostring(properties.migration.assessment.skuRecommendationResults.azureSqlManagedInstance.recommendationStatus)
 | extend azureSqlVirtualMachineRecommendationStatus = tostring(properties.migration.assessment.skuRecommendationResults.azureSqlVirtualMachine.recommendationStatus)
 | extend serverAssessments = tostring(properties.migration.assessment.serverAssessments)
 | extend subscriptionId = extract(@"/subscriptions/([^/]+)", 1, id)
 | extend resourceGroup = extract(@"/resource[g/G]roups/([^/]+)", 1, id)
 | mv-expand platformStatus = pack_array(
     pack("platform", "Azure SQL Database", "status", azureSqlDatabaseRecommendationStatus),
     pack("platform", "Azure SQL Managed Instance", "status", azureSqlManagedInstanceRecommendationStatus),
     pack("platform", "Azure SQL Virtual Machine", "status", azureSqlVirtualMachineRecommendationStatus)
   )
 | extend platformIncludedString = strcat('"AppliesToMigrationTargetPlatform":', strcat('"', replace(" ", "", tolower(tostring(platformStatus["platform"]))), '"'))
 | extend platformHasIssues = tolower(serverAssessments) has tolower(platformIncludedString)
 | project Platform = tostring(platformStatus["platform"]), status = tostring(platformStatus["status"]), tostring(serverAssessments), id, platformHasIssues
 | extend finalStatus = case(
     status == "Ready" and platformHasIssues, "Ready with Conditions",
     status == "Ready", "Ready",
     status == "NotReady", "NotReady",
     isnull(status) or status !in ("Ready", "NotReady", "Ready with Conditions"), "Unknown",
     "Unknown")
 | summarize TotalAssessed = count(), Ready = countif(finalStatus == "Ready"), NotReady = countif(finalStatus == "NotReady"),
     ReadyWithConditions = countif(finalStatus == "Ready with Conditions"), Unknown = countif(finalStatus == "Unknown")
     by Platform
```

# [Azure CLI](#tab/azure-cli)

```azurecli-interactive
az graph query -q "resources | where type =~ 'microsoft.hybridcompute/machines' | extend machineId = tolower(tostring(id)), datacenter = iif(isnull(tags.Datacenter), '', tags.Datacenter), status = tostring(properties.status) | extend mssqlinstalled = coalesce(tobool(properties.detectedProperties.mssqldiscovered),false) | extend pgsqlinstalled = coalesce(tobool(properties.detectedProperties.pgsqldiscovered),false) | extend mysqlinstalled = coalesce(tobool(properties.detectedProperties.mysqldiscovered),false) | extend osSku = properties.osSku, osName = properties.osName, osVersion = properties.osVersion | extend coreCount = tostring(properties.detectedProperties.logicalCoreCount), totalPhysicalMemoryinGB = tostring(properties.detectedProperties.totalPhysicalMemoryInGigabytes)  | extend operatingSystem = iif(isnotnull(osSku), osSku, osName) | where mssqlinstalled or mysqlinstalled or pgsqlinstalled | project id ,name, type, resourceGroup, subscriptionId, location, kind, osVersion, status, osSku,coreCount,totalPhysicalMemoryinGB,tags, mssqlinstalled, mysqlinstalled, pgsqlinstalled | sort by (tolower(tostring(name))) asc"
```

# [Azure PowerShell](#tab/azure-powershell)

```azurepowershell-interactive
Search-AzGraph -Query @"
resources
| where type =~ 'microsoft.hybridcompute/machines'
| extend machineId = tolower(tostring(id))
| extend datacenter = iif(isnull(tags.Datacenter), '', tags.Datacenter)
| extend status = tostring(properties.status)
| extend mssqlinstalled = coalesce(tobool(properties.detectedProperties.mssqldiscovered), false)
| extend pgsqlinstalled = coalesce(tobool(properties.detectedProperties.pgsqldiscovered), false)
| extend mysqlinstalled = coalesce(tobool(properties.detectedProperties.mysqldiscovered), false)
| extend osSku = properties.osSku
| extend osName = properties.osName
| extend osVersion = properties.osVersion
| extend coreCount = tostring(properties.detectedProperties.logicalCoreCount)
| extend totalPhysicalMemoryinGB = tostring(properties.detectedProperties.totalPhysicalMemoryInGigabytes)
| extend operatingSystem = iif(isnotnull(osSku), osSku, osName)
| where mssqlinstalled or mysqlinstalled or pgsqlinstalled
| project id, name, type, resourceGroup, subscriptionId, location, kind, osVersion, status, osSku, coreCount, totalPhysicalMemoryinGB, tags, mssqlinstalled, mysqlinstalled, pgsqlinstalled
| sort by (tolower(tostring(name))) asc
"@
```

# [Portal](#tab/azure-portal)

- [Azure portal](https://portal.azure.com/#blade/HubsExtension/ArgQueryBlade/query/resources%20%0A%20%7C%20where%20type%20%3D%3D%20%27microsoft.azurearcdata%2Fsqlserverinstances%27%20%0A%20%7C%20where%20properties.migration.assessment.assessmentUploadTime%20%3E%20ago%2814d%29%20and%20properties.migration.assessment.enabled%20%3D%3D%20true%20and%20isnotnull%28parse_json%28properties.migration.assessment.skuRecommendationResults%29%29%0A%20%7C%20extend%20azureSqlDatabaseRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlDatabase.recommendationStatus%29%0A%20%7C%20extend%20azureSqlManagedInstanceRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlManagedInstance.recommendationStatus%29%0A%20%7C%20extend%20azureSqlVirtualMachineRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlVirtualMachine.recommendationStatus%29%0A%20%7C%20extend%20serverAssessments%20%3D%20tostring%28properties.migration.assessment.serverAssessments%29%0A%20%7C%20extend%20subscriptionId%20%3D%20extract%28%40%22%2Fsubscriptions%2F%28%5B%5E%2F%5D%2B%29%22%2C%201%2C%20id%29%0A%20%7C%20extend%20resourceGroup%20%3D%20extract%28%40%22%2Fresource%5Bg%2FG%5Droups%2F%28%5B%5E%2F%5D%2B%29%22%2C%201%2C%20id%29%20%0A%20%7C%20mv-expand%20platformStatus%20%3D%20pack_array%28%20%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Database%22%2C%20%22status%22%2C%20azureSqlDatabaseRecommendationStatus%29%2C%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Managed%20Instance%22%2C%20%22status%22%2C%20azureSqlManagedInstanceRecommendationStatus%29%2C%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Virtual%20Machine%22%2C%20%22status%22%2C%20azureSqlVirtualMachineRecommendationStatus%29%0A%20%20%20%29%0A%20%7C%20extend%20platformIncludedString%20%3D%20strcat%28%27%22AppliesToMigrationTargetPlatform%22%3A%27%2C%20strcat%28%27%22%27%2C%20replace%28%22%20%22%2C%20%22%22%2C%20tolower%28tostring%28platformStatus%5B%22platform%22%5D%29%29%29%2C%20%27%22%27%29%29%0A%20%7C%20extend%20platformHasIssues%20%3D%20tolower%28serverAssessments%29%20has%20tolower%28platformIncludedString%29%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%0A%20%7C%20project%20Platform%20%3D%20tostring%28platformStatus%5B%22platform%22%5D%29%2C%20status%20%3D%20tostring%28platformStatus%5B%22status%22%5D%29%2C%20tostring%28serverAssessments%29%2C%20id%2C%20platformHasIssues%0A%20%7C%20extend%20finalStatus%20%3D%20case%28%0A%20%20%20%20%20status%20%3D%3D%20%22Ready%22%20and%20platformHasIssues%2C%20%22Ready%20with%20Conditions%22%2C%0A%20%20%20%20%20status%20%3D%3D%20%22Ready%22%2C%20%22Ready%22%2C%0A%20%20%20%20%20status%20%3D%3D%20%22NotReady%22%2C%20%22NotReady%22%2C%0A%20%20%20%20%20isnull%28status%29%20or%20status%20%21in%20%28%22Ready%22%2C%20%22NotReady%22%2C%20%22Ready%20with%20Conditions%22%29%2C%20%22Unknown%22%2C%0A%20%20%20%20%20%22Unknown%22%29%0A%20%7C%20summarize%20TotalAssessed%20%3D%20count%28%29%2C%20Ready%20%3D%20countif%28finalStatus%20%3D%3D%20%22Ready%22%29%2C%20NotReady%20%3D%20countif%28finalStatus%20%3D%3D%20%22NotReady%22%29%2C%20%0A%20%20%20%20%20ReadyWithConditions%20%3D%20countif%28finalStatus%20%3D%3D%20%22Ready%20with%20Conditions%22%29%2C%20Unknown%20%3D%20countif%28finalStatus%20%3D%3D%20%22Unknown%22%29%0A%20%20%20%20%20by%20Platform)
- [Azure Government portal](https://portal.azure.us/#blade/HubsExtension/ArgQueryBlade/query/resources%20%0A%20%7C%20where%20type%20%3D%3D%20%27microsoft.azurearcdata%2Fsqlserverinstances%27%20%0A%20%7C%20where%20properties.migration.assessment.assessmentUploadTime%20%3E%20ago%2814d%29%20and%20properties.migration.assessment.enabled%20%3D%3D%20true%20and%20isnotnull%28parse_json%28properties.migration.assessment.skuRecommendationResults%29%29%0A%20%7C%20extend%20azureSqlDatabaseRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlDatabase.recommendationStatus%29%0A%20%7C%20extend%20azureSqlManagedInstanceRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlManagedInstance.recommendationStatus%29%0A%20%7C%20extend%20azureSqlVirtualMachineRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlVirtualMachine.recommendationStatus%29%0A%20%7C%20extend%20serverAssessments%20%3D%20tostring%28properties.migration.assessment.serverAssessments%29%0A%20%7C%20extend%20subscriptionId%20%3D%20extract%28%40%22%2Fsubscriptions%2F%28%5B%5E%2F%5D%2B%29%22%2C%201%2C%20id%29%0A%20%7C%20extend%20resourceGroup%20%3D%20extract%28%40%22%2Fresource%5Bg%2FG%5Droups%2F%28%5B%5E%2F%5D%2B%29%22%2C%201%2C%20id%29%20%0A%20%7C%20mv-expand%20platformStatus%20%3D%20pack_array%28%20%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Database%22%2C%20%22status%22%2C%20azureSqlDatabaseRecommendationStatus%29%2C%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Managed%20Instance%22%2C%20%22status%22%2C%20azureSqlManagedInstanceRecommendationStatus%29%2C%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Virtual%20Machine%22%2C%20%22status%22%2C%20azureSqlVirtualMachineRecommendationStatus%29%0A%20%20%20%29%0A%20%7C%20extend%20platformIncludedString%20%3D%20strcat%28%27%22AppliesToMigrationTargetPlatform%22%3A%27%2C%20strcat%28%27%22%27%2C%20replace%28%22%20%22%2C%20%22%22%2C%20tolower%28tostring%28platformStatus%5B%22platform%22%5D%29%29%29%2C%20%27%22%27%29%29%0A%20%7C%20extend%20platformHasIssues%20%3D%20tolower%28serverAssessments%29%20has%20tolower%28platformIncludedString%29%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%0A%20%7C%20project%20Platform%20%3D%20tostring%28platformStatus%5B%22platform%22%5D%29%2C%20status%20%3D%20tostring%28platformStatus%5B%22status%22%5D%29%2C%20tostring%28serverAssessments%29%2C%20id%2C%20platformHasIssues%0A%20%7C%20extend%20finalStatus%20%3D%20case%28%0A%20%20%20%20%20status%20%3D%3D%20%22Ready%22%20and%20platformHasIssues%2C%20%22Ready%20with%20Conditions%22%2C%0A%20%20%20%20%20status%20%3D%3D%20%22Ready%22%2C%20%22Ready%22%2C%0A%20%20%20%20%20status%20%3D%3D%20%22NotReady%22%2C%20%22NotReady%22%2C%0A%20%20%20%20%20isnull%28status%29%20or%20status%20%21in%20%28%22Ready%22%2C%20%22NotReady%22%2C%20%22Ready%20with%20Conditions%22%29%2C%20%22Unknown%22%2C%0A%20%20%20%20%20%22Unknown%22%29%0A%20%7C%20summarize%20TotalAssessed%20%3D%20count%28%29%2C%20Ready%20%3D%20countif%28finalStatus%20%3D%3D%20%22Ready%22%29%2C%20NotReady%20%3D%20countif%28finalStatus%20%3D%3D%20%22NotReady%22%29%2C%20%0A%20%20%20%20%20ReadyWithConditions%20%3D%20countif%28finalStatus%20%3D%3D%20%22Ready%20with%20Conditions%22%29%2C%20Unknown%20%3D%20countif%28finalStatus%20%3D%3D%20%22Unknown%22%29%0A%20%20%20%20%20by%20Platform)
- [Azure operated by 21Vianet portal](https://portal.azure.cn/#blade/HubsExtension/ArgQueryBlade/query/resources%20%0A%20%7C%20where%20type%20%3D%3D%20%27microsoft.azurearcdata%2Fsqlserverinstances%27%20%0A%20%7C%20where%20properties.migration.assessment.assessmentUploadTime%20%3E%20ago%2814d%29%20and%20properties.migration.assessment.enabled%20%3D%3D%20true%20and%20isnotnull%28parse_json%28properties.migration.assessment.skuRecommendationResults%29%29%0A%20%7C%20extend%20azureSqlDatabaseRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlDatabase.recommendationStatus%29%0A%20%7C%20extend%20azureSqlManagedInstanceRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlManagedInstance.recommendationStatus%29%0A%20%7C%20extend%20azureSqlVirtualMachineRecommendationStatus%20%3D%20tostring%28properties.migration.assessment.skuRecommendationResults.azureSqlVirtualMachine.recommendationStatus%29%0A%20%7C%20extend%20serverAssessments%20%3D%20tostring%28properties.migration.assessment.serverAssessments%29%0A%20%7C%20extend%20subscriptionId%20%3D%20extract%28%40%22%2Fsubscriptions%2F%28%5B%5E%2F%5D%2B%29%22%2C%201%2C%20id%29%0A%20%7C%20extend%20resourceGroup%20%3D%20extract%28%40%22%2Fresource%5Bg%2FG%5Droups%2F%28%5B%5E%2F%5D%2B%29%22%2C%201%2C%20id%29%20%0A%20%7C%20mv-expand%20platformStatus%20%3D%20pack_array%28%20%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Database%22%2C%20%22status%22%2C%20azureSqlDatabaseRecommendationStatus%29%2C%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Managed%20Instance%22%2C%20%22status%22%2C%20azureSqlManagedInstanceRecommendationStatus%29%2C%0A%20%20%20%20%20pack%28%22platform%22%2C%20%22Azure%20SQL%20Virtual%20Machine%22%2C%20%22status%22%2C%20azureSqlVirtualMachineRecommendationStatus%29%0A%20%20%20%29%0A%20%7C%20extend%20platformIncludedString%20%3D%20strcat%28%27%22AppliesToMigrationTargetPlatform%22%3A%27%2C%20strcat%28%27%22%27%2C%20replace%28%22%20%22%2C%20%22%22%2C%20tolower%28tostring%28platformStatus%5B%22platform%22%5D%29%29%29%2C%20%27%22%27%29%29%0A%20%7C%20extend%20platformHasIssues%20%3D%20tolower%28serverAssessments%29%20has%20tolower%28platformIncludedString%29%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%20%0A%20%7C%20project%20Platform%20%3D%20tostring%28platformStatus%5B%22platform%22%5D%29%2C%20status%20%3D%20tostring%28platformStatus%5B%22status%22%5D%29%2C%20tostring%28serverAssessments%29%2C%20id%2C%20platformHasIssues%0A%20%7C%20extend%20finalStatus%20%3D%20case%28%0A%20%20%20%20%20status%20%3D%3D%20%22Ready%22%20and%20platformHasIssues%2C%20%22Ready%20with%20Conditions%22%2C%0A%20%20%20%20%20status%20%3D%3D%20%22Ready%22%2C%20%22Ready%22%2C%0A%20%20%20%20%20status%20%3D%3D%20%22NotReady%22%2C%20%22NotReady%22%2C%0A%20%20%20%20%20isnull%28status%29%20or%20status%20%21in%20%28%22Ready%22%2C%20%22NotReady%22%2C%20%22Ready%20with%20Conditions%22%29%2C%20%22Unknown%22%2C%0A%20%20%20%20%20%22Unknown%22%29%0A%20%7C%20summarize%20TotalAssessed%20%3D%20count%28%29%2C%20Ready%20%3D%20countif%28finalStatus%20%3D%3D%20%22Ready%22%29%2C%20NotReady%20%3D%20countif%28finalStatus%20%3D%3D%20%22NotReady%22%29%2C%20%0A%20%20%20%20%20ReadyWithConditions%20%3D%20countif%28finalStatus%20%3D%3D%20%22Ready%20with%20Conditions%22%29%2C%20Unknown%20%3D%20countif%28finalStatus%20%3D%3D%20%22Unknown%22%29%0A%20%20%20%20%20by%20Platform)

---

## Related content

- [Assess migration readiness - SQL Server enabled by Azure Arc](migration-assessment.md)
- [Assessment rules for SQL Server to Azure SQL Managed Instance migration](/data-migration/sql-server/managed-instance/assessment-rules)
- [Assessment rules for SQL Server to Azure SQL Database migration](/data-migration/sql-server/database/assessment-rules)
- [Migrate SQL Server to Azure SQL](/azure/dms/dms-overview)
- [SQL Server enabled by Azure Arc](overview.md)
- [Deployment options for SQL Server enabled by Azure Arc](deployment-options.md)
