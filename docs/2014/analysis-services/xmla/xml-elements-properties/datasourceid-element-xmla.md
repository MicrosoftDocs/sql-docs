---
title: "DataSourceID Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
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
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSourceID Element (XMLA)
  Identifies a data source used by a [Location](location-element-xmla.md) element during a [Backup](../xml-elements-commands/backup-element-xmla.md), [Restore](../xml-elements-commands/restore-element-xmla.md), or [Synchronize](../xml-elements-commands/synchronize-element-xmla.md) command.  
  
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
|Parent elements|[Location](location-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 The `DataSourceID` element contains the name of the data source on the source instance that identifies the remote instance on which remote partition information is to be backed up, restored, or synchronized.  
  
 For more information about backing up and restoring remote partitions, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [ConnectionString Element &#40;XMLA&#41;](connectionstring-element-xmla.md)   
 [DataSourceType Element &#40;XMLA&#41;](type-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
