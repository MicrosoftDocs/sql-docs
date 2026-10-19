---
title: Manage Inventory
description: Learn how to inventory SQL Server services, instances, databases, and view properties centrally, as Azure Arc-enabled resources.
author: pochiraju
ms.author: rajpo
ms.reviewer: vanto
ms.date: 10/29/2025
ms.topic: how-to
ms.custom:
  - ignite-2023
---

# Manage inventory of SQL Server resources with Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

When SQL Server engine instances or associated services are enabled by Azure Arc, you can use Azure to manage your inventory.

## Prerequisites

Verify that the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service is

- Version [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] or later.
- On a physical or virtual machine that's running the Windows operating system.
- Connected to Azure Arc. See [Connect your SQL Server to Azure Arc](connect.md).
- Connected to the internet directly or through a proxy server.

## Inventory databases

To inventory SQL Server databases, make sure that database names adhere to naming conventions and don't contain reserved words. For a list of reserved words, see [Resolve errors for reserved resource names](/azure/azure-resource-manager/troubleshooting/error-reserved-resource-name). For a complete list of naming rules and restrictions, review [naming rules and restrictions](/azure/azure-resource-manager/management/resource-name-rules).

To inventory databases:

1. Locate the instance of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] in the Azure portal.
1. Select the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] resource.
1. Under **Data management**, select **Databases**.
1. Use the **SQL Server databases - Azure Arc** area to view the databases that belong to the instance.

To view the database size and space available, make sure that the built-in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] login `NT AUTHORITY\SYSTEM` is a member of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] **sysadmin** server role for all the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances running on the machine.

### View database properties

To view properties for a specific database, select the database in the portal.

After you create, modify, or delete a database, changes appear in the Azure portal within an hour.

:::image type="content" source="media/view-databases/database-properties.png" alt-text="Screenshot of SQL Server database properties in the Azure portal." lightbox="media/view-databases/database-properties.png":::

The **Databases** pane shows the following information:

- Information about the data collection and upload:
  - Last collected time
  - Upload status
- Information about each database:
  - Name
  - Status
  - Creation time
  - Earliest restore point

When you select a specific database, all the properties for that database appear. These properties are also visible in SQL Server Management Studio.

:::image type="content" source="media/view-databases/full-property-list.png" alt-text="Screenshot of a full database property list." lightbox="media/view-databases/full-property-list.png":::

### Use Azure Resource Graph to query data

Here are some example scenarios that show how you use [Azure Resource Graph](/azure/governance/resource-graph/overview) to query data that's available when you're viewing Azure Arc-enabled SQL Server databases.

#### Scenario 1: Get 10 databases

Get 10 databases and return properties that are available to query:

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| limit 10
```

Many of the most interesting properties to query are in the `properties` property. To explore the available properties, run the following query and then select **See details** on a row. This action returns the properties in a JSON viewer on the right side.

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| project properties
```

You can navigate the hierarchy of the properties JSON by using a period between each level of the JSON.

#### Scenario 2: Get all the databases that have the database option AUTO_CLOSE set to ON

```kusto
| where (type == 'microsoft.azurearcdata/sqlserverinstances/databases' and properties.databaseOptions.isAutoCloseOn == true)
| extend isAutoCloseOn = properties.databaseOptions.isAutoCloseOn
| project name, isAutoCloseOn
```

#### Scenario 3: Obtain the count of databases that are encrypted vs. not encrypted

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| extend isEncrypted = properties.databaseOptions.isEncrypted
| summarize count() by tostring(isEncrypted)
| order by ['isEncrypted'] asc
```

#### Scenario 4: Show all the databases that aren't encrypted

```kusto
resources
| where (type == 'microsoft.azurearcdata/sqlserverinstances/databases' and properties.databaseOptions.isEncrypted == false)
| extend isEncrypted = properties.databaseOptions.isEncrypted
| project name, isEncrypted
```

#### Scenario 5: Get all the databases by region and compatibility level

This example returns all databases in the `westus3` location with a compatibility level of 160:

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| where location == "westus3"
| where properties.compatibilityLevel == "160"
```

#### Scenario 6: Show the SQL Server version distribution

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances'
| extend SQLversion = properties.version
| summarize count() by tostring(SQLversion)
```

#### Scenario 7: Show a count of databases by compatibility

This example returns the number of databases, ordered by the compatibility level:

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| summarize count() by tostring(properties.compatibilityLevel)
| order by properties_compatibilityLevel asc
```

## Inventory associated services

SQL Server associated services include:

- SQL Server Database Engine
- SQL Server Analysis Services (SSAS)
- SQL Server Integration Services (SSIS)
- SQL Server Reporting Services (SSRS)
- Power BI Report Server (PBIRS)

Each installation of an associated service is represented in Azure Resource Manager (ARM) as a SQL Server instance with `serviceType` property showing the specific service. The property is defined as follows:

```json
"serviceType":  {
         "type": "string",
         "enum": [ "Engine", "SSAS", "SSIS", "SSRS", "PBIRS" ],
         "default": "Engine"
     }
```

## Create inventory dashboard

You can also [create charts and pin them to dashboards](/azure/governance/resource-graph/first-query-portal).

:::image type="content" source="media/view-databases/database-chart.png" alt-text="Diagram of a pie chart that displays the query results for the count of databases by compatibility level.":::

## Known issues

Resources deleted on-premises might not be immediately deleted in Azure. For example, if you delete a database, the image of the database on Azure remains until the server resource synchronizes.

## Related content

- [Track migration journey by using migration dashboard - SQL Server enabled by Azure Arc](migration-inventory.md)
- [Feature availability by service type](overview.md#feature-availability-by-service-type)
- [Protect SQL Server with Microsoft Defender for Cloud](configure-advanced-data-security.md)
- [Configure best practices assessment for SQL Server enabled by Azure Arc](assess.md)
