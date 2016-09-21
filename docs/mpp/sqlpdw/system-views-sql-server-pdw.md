---
title: "System Views (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1c83332e-8ac2-4383-840e-80061808173d
caps.latest.revision: 37
author: BarbKess
---
# System Views (SQL Server PDW)
This topic explains basics about the system views in SQL Server PDW and points to the individual system view topics, and examples of common metadata queries. System views includes dynamic  management views (DMVs), catalog views, and information schema views. Use system views to view the current state of the system, as well as values, objects, and settings.  
  
## Contents  
[Basics](#Basics)  
  
**System View Topics**  
  
-   [Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
-   [External Operations Catalog Views &#40;SQL Server PDW&#41;](../sqlpdw/external-operations-catalog-views-sql-server-pdw.md)  
  
-   [SQL Server PDW Catalog Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-pdw-catalog-views-sql-server-pdw.md)  
  
-   [SQL Server PDW Dynamic Management Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-pdw-dynamic-management-views-sql-server-pdw.md)  
  
-   [SQL Server Catalog Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-catalog-views-sql-server-pdw.md)  
  
-   [SQL Server Dynamic Management Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-dynamic-management-views-sql-server-pdw.md)  
  
-   [SQL Server Information_Schema Views &#40;SQL Server PDW&#41;](../sqlpdw/sql-server-information-schema-views-sql-server-pdw.md)  
  
## <a name="Basics"></a>System View Basics  
System views describe the metadata, the system catalog, and the dynamic processes for SQL Server PDW.  
  
### Key Terms  
There are three types of views within system views: dynamic management views (DMVs), catalog views, and information schema views.  
  
Dynamic Management Views (DMVs)  
Dynamic Management Views (DMVs) show information about dynamic processes, such as the queries in progress and memory usage on each appliance node.  
  
Catalog Views  
Catalog views show information about metadata and static elements of SQL Server PDW, such as table and column names and principals.  
  
Information Schema Views  
Information schema view show metadata for the data objects stored in a database. The information schema views are defined in a special schema named **INFORMATION_SCHEMA**. This schema is contained in each database.  
  
### Understanding System Views in  SQL Server PDW  
In SQL Server PDW you will find the following views:  
  
SQL Server PDW Views  
These views describe the metadata, system state, and dynamic processes that are specific to the MPP SQL Server PDW system.  
  
In SQL Server PDW:  
  
-   The name of each SQL Server PDW catalog view begins with **sys.pdw_**.  
  
-   The name of each SQL Server PDW DMV begins with **sys.dm_pdw_**  
  
SQL Server Views  
These views are provided by SQL Server running on the Compute nodes. For each SQL Server view, SQL Server PDW combines the views from each of the Compute nodes into one view.  
  
In SQL Server PDW:  
  
-   The name of each combined  SQL Server DMV begins with **‘sys.dm_pdw_nodes’**.  
  
-   The name of each combined SQL Server catalog view is the same as the SQL Server name.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
