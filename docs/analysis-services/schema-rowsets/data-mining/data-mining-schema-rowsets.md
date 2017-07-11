---
title: "Data Mining Schema Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "schema rowsets [Analysis Services], data mining"
  - "schema rowsets [Analysis Services]"
  - "rowsets [Analysis Services], data mining"
  - "data mining [Analysis Services], schema rowsets"
ms.assetid: bd7d5df5-500b-4159-8467-880e141bc043
caps.latest.revision: 44
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Data Mining Schema Rowsets
  A server that is running [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports the following data mining schema rowsets. To check whether a particular XML/A provider supports a specific rowset, use the [DISCOVER_ENUMERATORS](../../../analysis-services/schema-rowsets/xml/discover-enumerators-rowset.md) rowset with the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method.  
  
 In [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)], the data mining schema rowsets are exposed as tables in the Transact-SQL language, in the $SYSTEM schema. For example, the following query on an Analysis Services instance returns a list of the schemas that are available on the current instance.  
  
```  
SELECT * FROM [$system].[DBSCHEMA_TABLES]  
```  
  
## In This Section  
  
|Schema Rowset|Description|  
|-------------------|-----------------|  
|[DMSCHEMA_MINING_COLUMNS Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-columns-rowset.md)|Describes the individual columns of all defined data mining models that are deployed on the server.|  
|[DMSCHEMA_MINING_FUNCTIONS Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-functions-rowset.md)|Describes the prediction functions and mining functions that can be used with each data mining algorithm that is installed on the server.|  
|[DMSCHEMA_MINING_MODEL_CONTENT Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-model-content-rowset.md)|Allows the client application to browse the content of a trained data mining model.|  
|[DMSCHEMA_MINING_MODEL_CONTENT_PMML Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-model-content-pmml-rowset.md)|Returns the XML (PMML 2.1) representation of the mining model content.|  
|[DMSCHEMA_MINING_MODEL_XML Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-model-xml-rowset.md)|Returns the XML (PMML 2.1) structure of the mining model. This is the same schema as DMSCHEMA_MINING_MODEL_PMML, which is preserved for backward compatibility.|  
|[DMSCHEMA_MINING_MODELS Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-models-rowset.md)|Enumerates the data mining models that are deployed on the server.|  
|[DMSCHEMA_MINING_SERVICE_PARAMETERS Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-service-parameters-rowset.md)|Provides a list of parameters that can be used to configure the behavior of each data mining algorithm that is installed on the server.|  
|[DMSCHEMA_MINING_SERVICES Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-services-rowset.md)|Provides a description of each data mining algorithm that is available on the server.|  
|[DMSCHEMA_MINING_STRUCTURE_COLUMNS Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-structure-columns-rowset.md)|Describes the individual columns of all mining structures that are deployed on the server.|  
|[DMSCHEMA_MINING_STRUCTURES Rowset](../../../analysis-services/schema-rowsets/data-mining/dmschema-mining-structures-rowset.md)|Enumerates information on mining structures.|  
  
 All the schema rowsets listed here are supported by the server that is running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## See Also  
 [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)   
 [Data Mining Schema Rowsets &#40;SSAs&#41;](../../../analysis-services/data-mining/data-mining-schema-rowsets-ssas.md)  
  
  