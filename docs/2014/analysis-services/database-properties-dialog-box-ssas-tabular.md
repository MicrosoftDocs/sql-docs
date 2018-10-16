---
title: "Database Properties Dialog Box (SSAS - Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.ssmsimbi.DatabaseProperties.f1"
ms.assetid: 0f0ec02f-7b55-40ea-8a04-ed0deb1efd7a
author: minewiskan
ms.author: owend
manager: craigg
---
# Database Properties Dialog Box (SSAS - Tabular)
  This dialog box provides timestamps and other descriptive information, plus customizable properties that determine whether the database uses cached data. Other customizable properties include changing the database name and specifying the impersonation options.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Name**|**Name** is the database name that uniquely identifies the database on the server. When changing the database name, consider the impact on reports and client applications that use the current name in existing connection strings. You will need to update connection strings in existing reports to avoid access denied errors. In addition, the tabular model that is the source of this database most likely uses the original name. Consider updating the database deployment properties in the model to ensure that future updates to that model are published to the intended database.|  
|**ID**|Displays the identifier of the database.|  
|**Description**|Type to change the description of the database.|  
|**Create Timestamp**|Displays the date and time the database was created.|  
|**Last Schema Update**|Displays the date and time the metadata for the database was last updated.|  
|**Last Update**|Displays the date and time the data for the database was last updated.|  
|**Read-Write Mode**|This is a read-only property, but you can change it using a sequence of **Detach** and **Attach** commands, where the property is a parameter of the **Attach** command. For more information, see [Database ReadWriteModes](multidimensional-models/database-readwritemodes.md).|  
|**DirectQueryMode**|Specifies whether the database uses only in-memory storage (no disk storage), only disk-based storage, or some combination of the two. Valid values are InMemory, DirectQuery, InMemoryWithDirectQuery (mostly memory-based with some paging to disk), or DirectQueryWithInMemory (mostly disk-based with some in-memory storage). For more information, see [DirectQuery Deployment Scenarios &#40;SSAS Tabular&#41;](directquery-deployment-scenarios-ssas-tabular.md).|  
|**Data Source Impersonation Info**|Specifies the impersonation account used for database connections when processing or refreshing data on local or remote partitions, queries that run against a relational data store (via DirectQuery), out-of-line bindings, and database synchronization from target to source.<br /><br /> Valid values include the Analysis Services service account or a specific set of Windows credentials. Do not specify **Use the credentials of the current user**. That credential option is not supported for a tabular model database.|  
|**Last Processed**|Displays the date and time the database was last processed.|  
|**Estimated Size**|Displays the estimated size of the database.|  
|**Storage Location**|Specifies the location of the database. If the database is located in the default Data directory, this value will be empty.|  
  
  
