---
title: "Synchronize Element (XMLA) | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: xmla
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Synchronize Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Synchronizes a Analysis Services database with another existing database.  
  
## Syntax  
  
```xml  
  
<Command>  
   <Synchronize>  
      <Source>...</Source>  
      <SynchronizeSecurity>...</SynchronizeSecurity>  
      <ApplyCompression>...</ApplyCompression>  
      <Locations>...</Locations>  
   </Synchronize>  
</Command>  
```  
  
## Element characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md)|  
|Child elements|[ApplyCompression](../../../analysis-services/xmla/xml-elements-properties/applycompression-element-xmla.md), [Locations](../../../analysis-services/xmla/xml-elements-properties/locations-element-xmla.md), [Source](../../../analysis-services/xmla/xml-elements-properties/source-element-synchronize-xmla.md), [SynchronizeSecurity](../../../analysis-services/xmla/xml-elements-properties/synchronizesecurity-element-xmla.md)|  
  
## Remarks  
 The **Synchronize** command synchronizes the target database with a source instance and database specified in the **Source** element. Optionally, the **Synchronize** command synchronizes remote partitions defined on the source database.  
  
 Depending on the storage mode used by objects stored in the backup file, the **Synchronize** command synchronizes information as listed in the following table.  
  
|Storage mode|Information|  
|------------------|-----------------|  
|Multidimensional OLAP (MOLAP)|Source data, aggregations, and metadata|  
|Hybrid OLAP (HOLAP)|Aggregations and metadata|  
|Relational OLAP (ROLAP)|Metadata|  
  
 During a **Synchronize** command, a read lock is placed on the source database and a write lock is placed on the target database. Both locks are released after the **Synchronize** command has completed.  
  
 For more information about synchronizing databases, see [Backing Up, Restoring, and Synchronizing Databases &#40;XMLA&#41;](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/backing-up-restoring-and-synchronizing-databases-xmla.md).  
  
## See also
 [Backup Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/backup-element-xmla.md)   
 [Batch Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md)   
 [Parallel Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-properties/parallel-element-xmla.md)   
 [Restore Element &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/restore-element-xmla.md)   
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
