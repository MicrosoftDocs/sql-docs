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
You can load or insert data into SQL Server PDW by using Integration Services, [bcp Utility](https://msdn.microsoft.com/library/ms162802.aspx), **dwloader** command-line tool, or the SQL INSERT statement.  
  
## Data Loading Basics  
  
### Loading Environment  
To load data, you need one or more loading servers. You can use your own existing ETL or other servers, or you can purchase new servers. For more information, see [Acquire and Configure a Loading Server &#40;SQL Server PDW&#41;](../sqlpdw/acquire-and-configure-a-loading-server-sql-server-pdw.md). These instructions include a [Loading Server Capacity Planning Worksheet &#40;SQL Server PDW&#41;](../sqlpdw/loading-server-capacity-planning-worksheet-sql-server-pdw.md) to help you plan the right solution for loading.  
  
### Loading with dwloader  
Using the [dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../sqlpdw/dwloader-command-line-loader-sql-server-pdw.md) is the fastest way to load data into SQL Server PDW.  
  
![Loading process](../sqlpdw/media/SQL_Server_PDW_V2_LoadingProcess.png "SQL_Server_PDW_V2_LoadingProcess")  
  
dwloader loads data directly to the Compute nodes without passing the data through the Control node. To load data, dwloader first communicates with the Control node to obtain contact information for the Compute nodes. dwloader sets up a communication channel with each Compute node and then sends 256KB chunks of data to the Compute nodes in a round-robin manner.  
  
On each Compute node, Data Movement Service (DMS) receives and processes the chunks of data. Processing the data includes converting each row into SQL Server native format, and computing the distribution hash to determine the Compute node to which each row belongs.  
  
After processing the rows, DMS uses a shuffle move to transfer each row to the correct Compute node and instance of SQL Server. As SQL Server receives the rows, it batches them according to the **–b** batch size parameter set in dwloader, and then bulk loads the batch.  
  
## Related Tasks  
  
|Task|Description|  
|--------|---------------|  
|Create a staging database.|[Create the Staging Database &#40;SQL Server PDW&#41;](../sqlpdw/create-the-staging-database-sql-server-pdw.md)|  
|Load data with Integration Services.|[Load Data With Integration Services &#40;SQL Server PDW&#41;](../sqlpdw/load-data-with-integration-services-sql-server-pdw.md)|  
|Understand type conversions for dwloader.|[Data Type Conversion Rules for dwloader &#40;SQL Server PDW&#41;](../sqlpdw/data-type-conversion-rules-for-dwloader-sql-server-pdw.md)|  
|Load data with dwloader.|[dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../sqlpdw/dwloader-command-line-loader-sql-server-pdw.md)|  
|Understand type conversions for INSERT.|[Load Data With INSERT &#40;SQL Server PDW&#41;](../sqlpdw/load-data-with-insert-sql-server-pdw.md)|  
|||  
  
## See Also  
[Grant Permissions to Load Data &#40;SQL Server PDW&#41;](../sqlpdw/grant-permissions-to-load-data-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
