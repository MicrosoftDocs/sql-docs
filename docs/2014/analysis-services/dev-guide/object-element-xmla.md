---
title: "Object Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Object Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine#Object"
  - "urn:schemas-microsoft-com:xml-analysis#Object"
  - "microsoft.xml.analysis.object"
helpviewer_keywords: 
  - "Object element"
ms.assetid: 99470537-2c4a-4072-9613-940c41c12487
caps.latest.revision: 16
author: "mgblythe"
ms.author: "mblythe"
manager: "mblythe"
---
# Object Element (XMLA)
  Contains an object reference used by the parent element.  
  
## Syntax  
  
```xml  
  
<Alter> <!-- or any of the parent elements in the Element Relationships table -->  
...  
   <Object>  
      <!-- One or more object identifiers, depending on the parent element -->  
   </Object>  
...  
</Alter>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|Ancestor or Parent: [Alter](../../../2014/analysis-services/dev-guide/create-element-xmla.md) &#124;  0-1: Optional element that can occur once and only once.<br /><br /> Ancestor or Parent: All others &#124; 1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Alter](../../../2014/analysis-services/dev-guide/alter-element-xmla.md), [Backup](../../../2014/analysis-services/dev-guide/backup-element-xmla.md), [ClearCache](../../../2014/analysis-services/dev-guide/clearcache-element-xmla.md), [Delete](../../../2014/analysis-services/dev-guide/delete-element-xmla.md), [DesignAggregations](../../../2014/analysis-services/dev-guide/designaggregations-element-xmla.md), [Lock](../../../2014/analysis-services/dev-guide/lock-element-xmla.md), [NotifyTableChange](../../../2014/analysis-services/dev-guide/notifytablechange-element-xmla.md), [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md), [Subscribe](../../../2014/analysis-services/dev-guide/subscribe-element-xmla.md), [Synchronize](../../../2014/analysis-services/dev-guide/synchronize-element-xmla.md)|  
|Child elements|Required Analysis Services Scripting Language (ASSL) elements. Specified by listing the ID elements of the object and its ancestors (excluding the `Server` object.) For example, the following `Object` element identifies a partition:<br /><br /> `<Object>`<br /><br /> `<DatabaseID>Adventure Works DW Multidimensional 2012</DatabaseID>`<br /><br /> `<CubeID>Adventure Works</CubeID>`<br /><br /> `<MeasureGroupID>Internet Sales</MeasureGroupID>`<br /><br /> `<PartitionID>Inernet_Sales_2001</PartitionID>`<br /><br /> `</Object>`|  
  
## Remarks  
 The order in which identifiers appear is not important.  
  
 For `Alter` elements, the instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is used as the default object if the `Object` element is not specified.  
  
## See Also  
 [Properties &#40;XMLA&#41;](../../../2014/analysis-services/dev-guide/properties-xmla.md)  
  
  