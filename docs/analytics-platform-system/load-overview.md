---
title: Loading data into Parallel Data Warehouse | Microsoft Docs
description: You can load or insert data into SQL Server Parallel Data Warehouse (PDW) by using Integration Services, bcp Utility, dwloader, or the SQL INSERT statement.
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Loading data into Parallel Data Warehouse
You can load or insert data into SQL Server Parallel Data Warehouse (PDW) by using Integration Services, [bcp Utility](../tools/bcp-utility.md), **dwloader** Command-line Loader, or the SQL INSERT statement.  

## Loading Environment  
To load data, you need one or more loading servers. You can use your own existing ETL or other servers, or you can purchase new servers. For more information, see [Acquire and Configure a Loading Server](acquire-and-configure-loading-server.md). These instructions include a [Loading Server Capacity Planning Worksheet](loading-server-capacity-planning-worksheet.md) to help you plan the right solution for loading.  
  
## Load with dwloader  
Using the [dwloader Command-Line Loader](dwloader.md) is the fastest way to load data into PDW.  
  
![Loading process](media/loading-process.png "Loading process")  
  
dwloader loads data directly to the Compute nodes without passing the data through the Control node. To load data, dwloader first communicates with the Control node to obtain contact information for the Compute nodes. dwloader sets up a communication channel with each Compute node and then sends 256KB chunks of data to the Compute nodes in a round-robin manner.  
  
On each Compute node, Data Movement Service (DMS) receives and processes the chunks of data. Processing the data includes converting each row into SQL Server native format, and computing the distribution hash to determine the Compute node to which each row belongs.  
  
After processing the rows, DMS uses a shuffle move to transfer each row to the correct Compute node and instance of SQL Server. As SQL Server receives the rows, it batches them according to the **-b** batch size parameter set in dwloader, and then bulk loads the batch.  

## Load with prepared statements

You can use prepared statements to load data into distributed and replicated tables. When the input data does not match the target data type, an implicit conversion is performed. The implicit conversions supported by PDW prepared statements are a subset of conversions supported by SQL Server. That is, only a subset of conversions are supported, but the supported conversions match SQL Server implicit conversions. Regardless of whether the target table to be loaded is defined as a distributed or replicated table, implicit conversions are applied (if needed) to all columns that exist in target table. 

<!-- MISSING LINK
For more information, see [Prepared statements](prepared-statements.md).
-->
  
## Related Tasks  
  
|Task|Description|  
|--------|---------------|  
|Create the staging database.|[Create the staging database](staging-database.md)|  
|Load with Integration Services.|[Load with Integration Services](load-with-ssis.md)|  
|Understand type conversions for dwloader.|[Data type conversion rules for dwloader](dwloader-data-type-conversion-rules.md)|  
|Load data with dwloader.|[dwloader Command-Line Loader](dwloader.md)|  
|Understand type conversions for INSERT.|[Load data with INSERT](load-with-insert.md)|  
 
<!-- MISSING LINKS
## See Also  
[Grant permissions to load data](grant-permissions-to-load-data.md)  
[Common metadata query examles](metadata-query-examples.md)  
  
-->
