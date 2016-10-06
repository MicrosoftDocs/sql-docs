---
title: "Load (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c7292108-4a48-409e-b0f4-e4ba84dce26f
caps.latest.revision: 22
author: BarbKess
---
# Load (SQL Server PDW)
You can load or insert data into SQL Server Parallel Data Warehouse (PDW) by using Integration Services, [bcp Utility](https://msdn.microsoft.com/library/ms162802.aspx), **dwloader** command-line tool, or the SQL INSERT statement.  
  
## Data Loading Basics  
  
### Loading Environment  
To load data, you need one or more loading servers. You can use your own existing ETL or other servers, or you can purchase new servers. For more information, see [Acquire and Configure a Loading Server](acquire-and-configure-a-loading-server-sql-server-pdw.md). These instructions include a [Loading Server Capacity Planning Worksheet](loading-server-capacity-planning-worksheet.md) to help you plan the right solution for loading.  
  
### Loading with dwloader  
Using the [dwloader Command-Line Loading Tool](dwloader.md) is the fastest way to load data into SQL Server PDW.  
  
![Loading process](media/loading-process.png "Loading process")  
  
dwloader loads data directly to the Compute nodes without passing the data through the Control node. To load data, dwloader first communicates with the Control node to obtain contact information for the Compute nodes. dwloader sets up a communication channel with each Compute node and then sends 256KB chunks of data to the Compute nodes in a round-robin manner.  
  
On each Compute node, Data Movement Service (DMS) receives and processes the chunks of data. Processing the data includes converting each row into SQL Server native format, and computing the distribution hash to determine the Compute node to which each row belongs.  
  
After processing the rows, DMS uses a shuffle move to transfer each row to the correct Compute node and instance of SQL Server. As SQL Server receives the rows, it batches them according to the **–b** batch size parameter set in dwloader, and then bulk loads the batch.  
  
## Related Tasks  
  
|Task|Description|  
|--------|---------------|  
|Create the staging database.|[Create the staging database](create-the-staging-database.md)|  
|Load with Integration Services.|[Load with Integration Services](load-with-integration-services.md)|  
|Understand type conversions for dwloader.|[Data type conversion rules for dwloader](data-type-conversion-rules-dwloader.md)|  
|Load data with dwloader.|[dwloader Command-Line Loading Tool](dwloader.md)|  
|Understand type conversions for INSERT.|[Load data with INSERT](load-data-with-insert.md)|  
|||  
  
## See Also  
[Grant permissions to load data](grant-permissions-to-load-data.md)  
[Common metadata query examles](common-metadata-query-examples.md)  
  
