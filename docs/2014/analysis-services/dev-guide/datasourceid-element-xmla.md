---
title: "DataSourceID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "DataSourceID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.datasourceid"
  - "urn:schemas-microsoft-com:xml-analysis#DataSourceID"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#DataSourceID"
helpviewer_keywords: 
  - "DataSourceID element"
ms.assetid: 695522c7-acca-420a-a5fb-f01f3fd9a96b
caps.latest.revision: 12
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# DataSourceID Element (XMLA)
  Identifies a data source used by a [Location](../../../2014/analysis-services/dev-guide/location-element-xmla.md) element during a [Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [Restore](../../../2014/analysis-services/dev-guide/restore-element-xmla.md), or [Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md) command.  
  
## Syntax  
  
```xml  
  
<Location>  
   ...  
   <DataSourceID>...</DataSourceID>  
   ...  
</Location>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Location](../../../2014/analysis-services/dev-guide/location-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `DataSourceID` element contains the name of the data source on the source instance that identifies the remote instance on which remote partition information is to be backed up, restored, or synchronized.  
  
 For more information about backing up and restoring remote partitions, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [ConnectionString Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/connectionstring-element-xmla.md)   
 [DataSourceType Element &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/datasourcetype-element-xmla.md)   
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  