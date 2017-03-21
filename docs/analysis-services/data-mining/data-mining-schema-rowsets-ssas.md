---
title: "Data Mining Schema Rowsets (SSAs) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "schema rowsets [Analysis Services], data mining"
  - "data mining [Analysis Services], queries"
  - "mining model content"
  - "data mining [Analysis Services], schema rowsets"
  - "schema rowsets [Analysis Services], retrieving"
  - "data mining [Analysis Services], troubleshooting"
ms.assetid: 442d8c29-07c7-45de-9a15-d556059f68d7
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "jhubbard"
---
# Data Mining Schema Rowsets (SSAs)
  In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], many of the existing OLE DB data mining schema rowsets are exposed as a set of system tables that you can query by using Data Mining Extensions (DMX) statements. By creating queries against the data mining schema rowset, you can identify the services that are available, get updates on the status of your models and structures, and find out details about the model content or parameters. For a description of the data mining schema rowsets, see [Data Mining Schema Rowsets](../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md).  
  
> [!NOTE]  
>  You can also query the data mining schema rowsets by using XMLA. For more information about how to do this in SQL Server Management Studio, see [Create a Data Mining Query by Using XMLA](../../analysis-services/data-mining/create-a-data-mining-query-by-using-xmla.md).  
  
## List of Data Mining Schema Rowsets  
 The following table lists the data mining schema rowsets that may be useful for querying and monitoring.  
  
|Rowset name|Description|  
|-----------------|-----------------|  
|DMSCHEMA_MINING_MODELS|Lists all mining models in the current context.<br /><br /> Includes such information as the date created, parameters used to create the model, and the size of the training set.|  
|DMSCHEMA_MINING_COLUMNS|Lists all columns used in mining models in the current context.<br /><br /> Information includes mapping to mining structure source column, data type, precision, and prediction functions that can be used with the column.|  
|DMSCHEMA_MINING_STRUCTURES|Lists all mining structure in the current context.<br /><br /> Information includes whether the structure is populated, the date the structure was last processed, and the definition of the holdout data set for the structure, if any.|  
|DMSCHEMA_MINING_STRUCTURE_COLUMNS|Lists all columns used in mining structures in the current context.<br /><br /> Information includes content type and data type, nullability, and whether the column contains nested table data.|  
|DMSCHEMA_MINING_SERVICES|Lists all mining services, or algorithms, that are available on the specified server.<br /><br /> Information includes supported modeling flags, input types, and supported data source types.|  
|DMSCHEMA_MINING_SERVICE_PARAMETERS|Lists all parameters for the mining services that are available on the current instance.<br /><br /> Information includes the data type for each parameter, the default values, and the upper and lower limits.|  
|DMSCHEMA_MODEL_CONTENT|Returns the content of the model if the model has been processed.<br /><br /> For more information, see [Mining Model Content &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/mining-model-content-analysis-services-data-mining.md).|  
|DBSCHEMA_CATALOGS|Lists all databases (catalogs) in the current instance of Analysis Services.|  
|MDSCHEMA_INPUT_DATASOURCES|Lists all data sources in the current instance of Analysis Services.|  
  
> [!NOTE]  
>  The list in the table is not comprehensive; it shows only those rowsets that may be of most interest for troubleshooting.  
  
## Examples  
 The following section provides some examples of queries against the data mining schema rowsets.  
  
### Example 1: List Data Mining Services  
 The following query returns a list of the mining services that are available on the current server, meaning the algorithms that are enabled. The columns provided for each mining service include the modeling flags and content types that can be used by each algorithm, the GUID for each service, and any prediction limits that may have been added for each service.  
  
```  
SELECT *  
FROM $system.DMSCHEMA_MINING_SERVICES  
```  
  
### Example 2: List Mining Model Parameters  
 The following example returns the parameters that were used to create a specific mining model:  
  
```  
SELECT MINING_PARAMETERS   
FROM $system.DMSCHEMA_MINING_MODELS  
WHERE MODEL_NAME = 'TM Clustering'  
```  
  
### Example 3: List All Rowsets  
 The following example returns a comprehensive list of the rowsets that are available on the current server:  
  
```  
SELECT *   
FROM $system.DBSCHEMA_TABLES  
```  
  
  
