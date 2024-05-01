---
title: View SQL Server databases
description: View databases in Azure from an instance of SQL Server enabled by Azure Arc. Use to inventory databases, and view properties of databases centrally, as Arc-enabled resources.
author: ntakru
ms.author: nikitatakru
ms.reviewer: mikeray, randolphwest
ms.date: 12/12/2023
ms.topic: conceptual
ms.custom: ignite-2023
---

# View SQL Server databases - Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

You can inventory and view [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] databases in Azure.

## Prerequisites

Before you begin, verify that the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance that hosts the databases:

- Is hosted on a physical or virtual machine running Windows operating system.
- Is [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] or later.
- Is connected to Azure Arc. See [Connect your SQL Server to Azure Arc](connect.md).
- Is connected to the internet directly or through a proxy server.
- Make sure that database names adhere to naming conventions and don't contain reserve words. For a list of reserved words, see [Resolve errors for reserved resource names](/azure/azure-resource-manager/troubleshooting/error-reserved-resource-name).
- To view the database size and space available, provide the following permission:
  - The built-in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] login **NT AUTHORITY\SYSTEM** must be a member of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] **sysadmin** server role, for all the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances running on the machine.

## Inventory databases

1. Locate the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance in the Azure portal.
1. **Select** the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] resource.
1. Under **Data management**, select **Databases**.

The Azure portal shows **SQL Server databases - Azure Arc**. Use this area to view the databases that belong to the instance.

## View database properties

To view database properties for a specific database, select the database on the portal.

After you create, modify, or delete a database, changes are visible in the Azure portal within an hour.

:::image type="content" source="media/view-databases/database-properties.png" alt-text="Screenshot of Azure portal, SQL Server database properties." lightbox="media/view-databases/database-properties.png":::

On the Database page, you see the following information:

- Information about the data collection and upload:
  - Last collected time
  - Upload status
- Information about each database:
  - Name
  - Status
  - Creation time
  - Earliest restore point

When you select a specific database, you see all the properties for that database, which are also visible in SQL Server Management Studio (SSMS).

:::image type="content" source="media/view-databases/full-property-list.png" alt-text="Screenshot of full database property list." lightbox="media/view-databases/full-property-list.png":::

## How to use Azure Resource Graph to query data

Here are some example scenarios showing how you use [Azure Resource Graph](/azure/governance/resource-graph/overview) to query data that is available when viewing [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] databases.

### Scenario 1: Get 10 databases

Get 10 databases and return what properties are available to query:

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| limit 10
```

Many of the most interesting properties to query are in the `properties` property. To explore the available properties, run this query and then select **See details** on a row.  This returns the properties in a json viewer on the right side.

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| project properties
```

You can navigate the hierarchy of the properties json by using a period in between each level of the properties json.

### Scenario 2: Get all the databases that have database option AUTO_CLOSE set to ON

```kusto
| where (type == 'microsoft.azurearcdata/sqlserverinstances/databases' and properties.databaseOptions.isAutoCloseOn == true)
| extend isAutoCloseOn = properties.databaseOptions.isAutoCloseOn
| project name, isAutoCloseOn
```

### Scenario 3: Obtain the count of databases that are encrypted vs not encrypted

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| extend isEncrypted = properties.databaseOptions.isEncrypted
| summarize count() by tostring(isEncrypted)
| order by ['isEncrypted'] asc
```

### Scenario 4: Show all the databases that aren't encrypted

```kusto
resources
| where (type == 'microsoft.azurearcdata/sqlserverinstances/databases' and properties.databaseOptions.isEncrypted == false)
| extend isEncrypted = properties.databaseOptions.isEncrypted
| project name, isEncrypted
```

### Scenario 5: Get all the databases by region and compatibility level

This example returns all databases in `westus3` location with compatibility level of 160:

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| where location == "westus3"
| where properties.compatibilityLevel == "160"
```

### Scenario 6: Show the SQL Server version distribution

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances'
| extend SQLversion = properties.version
| summarize count() by tostring(SQLversion)
```

### Scenario 7: Show a count of databases by compatibility

This example returns the number of databases, ordered by the compatibility level:

```kusto
resources
| where type == 'microsoft.azurearcdata/sqlserverinstances/databases'
| summarize count() by tostring(properties.compatibilityLevel)
| order by properties_compatibilityLevel asc
```

You can also [create charts and pin them to dashboards](/azure/governance/resource-graph/first-query-portal).

:::image type="content" source="media/view-databases/database-chart.png" alt-text="Diagram of a pie chart that displays the query results for the count of databases by compatibility level.":::

## Known issues

Databases deleted on-premises might not be immediately deleted on Azure. There's no impact on how database CRUD operations happen on-premises.

## Related content

- [Protect [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] with Microsoft Defender for Cloud](configure-advanced-data-security.md)
- [Configure best practices assessment on a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance](assess.md)
