---
title: "BackupRemotePartitions Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "BackupRemotePartitions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "microsoft.xml.analysis.backupremotepartitions"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#BackupRemotePartitions"
  - "urn:schemas-microsoft-com:xml-analysis#BackupRemotePartitions"
helpviewer_keywords: 
  - "BackupRemotePartitions element"
ms.assetid: bd68bcf9-b324-4fa8-b6e5-1f5531f9992c
author: minewiskan
ms.author: owend
manager: craigg
---
# BackupRemotePartitions Element (XMLA)
  Determines whether the parent [Backup](../xml-elements-commands/backup-element-xmla.md) command backs up remote partitions associated with the object.  
  
## Syntax  
  
```xml  
  
<Backup>  
   ...  
   <BackupRemotePartitions>...</BackupRemotePartitions>  
   ...  
</Backup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|False|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Backup](../xml-elements-commands/backup-element-xmla.md)|  
|Child elements|None|  
  
## Remarks  
 If `BackupRemotePartitions` is set to `True`, a `Locations` element that contains one or more `Location` elements must be included in the `Backup` command, or an error occurs. For more information about backing up and restoring remote partitions, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See Also  
 [Locations Element &#40;XMLA&#41;](locations-element-xmla.md)   
 [Location Element &#40;XMLA&#41;](location-element-xmla.md)   
 [Properties &#40;XMLA&#41;](xml-elements-properties.md)  
  
  
