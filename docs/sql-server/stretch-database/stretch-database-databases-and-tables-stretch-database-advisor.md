---
title: "Identify databases and tables"
description: "Identify databases and tables for Stretch Database with Data Migration Assistant"
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/04/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Stretch Database, identifying databases"
  - "Stretch Database, identifying tables"
  - "identifying databases for Stretch Database"
  - "identifying tables for Stretch Database"
---
# Identify databases and tables for Stretch Database with Data Migration Assistant

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssnotedepfuturedontuse-md](../../includes/ssnotedepfuturedontuse-md.md)]

To identify databases and tables that are candidates for Stretch Database, along with potential blocking issues, download and run Microsoft Data Migration Assistant.

## Get Data Migration Assistant

Download and install Data Migration Assistant from [here](https://www.microsoft.com/download/details.aspx?id=53595). This tool isn't included on the SQL Server installation media.

## Run Data Migration Assistant

1. Run Microsoft Data Migration Assistant.

1. Create a new project of type **Assessment** and give it a name.

1. Select **SQL Server** as both the **Source server type** and the **Target server type**.

1. Select **Create**.

1. On the **Options** page (step 1), select **New features recommendation**. Optionally, clear the selection for **Compatibility issues**.

1. On the **Select sources** page (step 2), connect to a server, select a database, and then select **Add**.

1. Select **Start Assessment**.

## Review the results

1. When the analysis is finished, on the **Review results** page (step 3), select the **Feature recommendations** option, and then select the **Storage** tab.

1. Review the recommendations related to Stretch Database. Each recommendation lists the tables for which Stretch Database may be appropriate, along with any potential blocking issues.

## Historical note

Stretch Database Advisor was previously a component of SQL Server 2016 Upgrade Advisor. At that time, you had to select and run Stretch Database Advisor as a separate action.

With the release of Data Migration Assistant, which replaces and extends Upgrade Advisor, the functionality of Stretch Database Advisor is incorporated into this new tool. You don't have to select any options to get recommendations related to Stretch Database. When you run an Assessment in Data Migration Assistant, the results related to Stretch Database appear on the **Storage** tab of the **Feature recommendations**.

## Next steps

- [Enable Stretch Database for a database](enable-stretch-database-for-a-database.md)
- [Enable Stretch Database for a table](enable-stretch-database-for-a-table.md)

## See also

- [Limitations for Stretch Database](limitations-for-stretch-database.md)
