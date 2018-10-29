---
title: "Connection Properties Dialog Box (SSAS - Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.asvs.ssmsimbi.ConnectionProperties.F1"
ms.assetid: 17bae8ae-2ba0-4978-be70-61c687f59d54
author: minewiskan
ms.author: owend
manager: craigg
---
# Connection Properties Dialog Box (SSAS - Tabular)
  Use this page to view or modify in SQL Server Management Studio the connection properties of a data source used by a tabular model database.  
  
 This dialog box provides timestamps and other descriptive information, plus customizable properties that determine characteristics of the connection.  
  
## Options  
  
|Term|Definition|  
|----------|----------------|  
|**Name**|Specifies the name of the data source.|  
|**ID**|Displays the identifier of the data source object.|  
|**Description**|Displays the description of the data source object.|  
|**Create Timestamp**|Displays the date and time the database was created.|  
|**Last Schema Update**|Displays the date and time the metadata for the database was last updated.|  
|**Connection String**|Displays the connection string used to connect to the data source that provides data to the model.|  
|**Maximum Number of Connections**|Specifies the maximum number of client connections to this database.|  
|**Isolation**|Valid values are ReadCommitted or Snapshot. For more information, see [Isolation Element &#40;ASSL&#41;](https://docs.microsoft.com/bi-reference/assl/properties/isolation-element-assl).|  
|**Query Timeout**|Specifies the time, in seconds, after which an attempt to retrieve data is timed out.|  
|**Managed Provider**|Specifies the name of the managed provider. If the data source connection uses a native OLE DB provider, this value is empty.|  
|**Impersonation Info**|Specifies the impersonation account used for database connections when processing or refreshing data, queries that run against a relational data store (via DirectQuery), out-of-line bindings, remote partitions, and database synchronization from target to source.<br /><br /> Valid values include the Analysis Services service account or a specific set of Windows credentials. Do not specify **Use the credentials of the current user** or **Inherit**. Those credential options are not supported for a tabular model database.|  
  
  
