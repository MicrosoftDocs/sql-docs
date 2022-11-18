---
title: Remote table copy
description: Using remote table copy in Analytics Platform System Parallel Data Warehouse.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Remote Table Copy
Describes how to use the remote table copy feature to copy  tables from SQL Server PDW databases to remote (non-appliance) SMP SQL Server databases. Use remote table copy to enables hub and spoke scenarios for SQL Server PDW.  
  
## <a name="BasicsPDE"></a>Understand Remote Table Copy for SQL Server PDW  
Remote table copy is a feature of SQL Server PDW that enables Hub and Spoke scenarios by copying the results of a SQL SELECT statement to a table in an SMP database. The remote table copy is initiated with the [CREATE REMOTE TABLE AS SELECT](../t-sql/statements/create-remote-table-as-select-parallel-data-warehouse.md) statement.  
  
## <a name="BasicsPrerequisites"></a>Requirements for Using Remote Table Copy  
You can use remote table copy to copy tables from SQL Server PDW to a SQL Server database when these conditions are met:  
  
-   The destination database must be an instance of Microsoft® SQL Server® that is running on a Microsoft Windows® system that can connect to the SQL Server PDW appliance but does not reside on a server within the appliance. The remote SQL Server can be connected to the SQL Server PDW using the InfiniBand network or through the Ethernet network.  
  
-   The data to be copied must be selectable using a single valid SQL Server PDW [SELECT](../t-sql/queries/select-transact-sql.md) statement.  
  
-   The destination server must be a non-appliance server. Data cannot be copied directly from one appliance to another using the instructions in this topic.  
  
-   The destination server must be accessible to all nodes on the appliance's Infiniband network.  
  
## <a name="ConfigureRemote"></a>Configure Remote Table Copy  
To use remote table copy, you need to purchase and configure a Windows server, configure SQL Server on the Windows server, and configure SQL Server PDW. Use the following links to perform these three configuration steps.  
  
1.  [Configure an External Windows System To Receive Remote Table Copies Using InfiniBand](configure-an-external-windows-system-to-receive-remote-table-copies-using-infiniband.md)  
  
2.  [Configure an External SMP SQL Server to Receive Remote Table Copies](configure-an-external-smp-sql-server-to-receive-remote-table-copies.md)  
  
3.  [Configure SQL Server PDW for Remote Table Copies](configure-sql-server-pdw-for-remote-table-copies.md)  
  
## <a name="PerformRemote"></a>Perform a Remote Table COpy  
To perform a remote table copy, use the [CREATE REMOTE TABLE AS SELECT](../t-sql/statements/create-remote-table-as-select-parallel-data-warehouse.md) SQL statement. Examples are included with the CREATE REMOTE TABLE topic.  
  
<!-- MISSING LINKS 
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
  
