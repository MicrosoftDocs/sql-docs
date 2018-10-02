---
title: "NotifyTableChange Element (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "NotifyTableChange Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "urn:schemas-microsoft-com:xml-analysis#NotifyTableChange"
  - "http://schemas.microsoft.com/analysisservices/2003/engine#NotifyTableChange"
  - "microsoft.xml.analysis.notifytablechange"
helpviewer_keywords: 
  - "NotifyTableChange command"
ms.assetid: b76898eb-20d3-48c8-9eb8-1fd5df638bcc
author: minewiskan
ms.author: owend
manager: craigg
---
# NotifyTableChange Element (XMLA)
  Notifies an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] that a change has occurred to tables in a specified data source.  
  
## Syntax  
  
```xml  
  
<Command>  
   <NotifyTableChange>  
      <Object>...</Object>  
      <TableNotifications>...</TableNotifications>  
   </NotifyTableChange>  
</Command>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-n: Optional element that can occur more than once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Command](../xml-elements-properties/command-element-xmla.md)|  
|Child elements|[Object](../xml-elements-properties/object-element-xmla.md), [TableNotifications](../xml-elements-properties/tablenotifications-element-xmla.md)|  
  
## Remarks  
 The `NotifyTableChange` command allows a client application to explicitly notify an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance that one or more tables contained in a data source have been changed. For proactive caching, this notification indicates that relational OLAP (ROLAP) objects based on those tables should be reviewed and updated.  
  
 This method of notification is best used for ROLAP objects based on views or named queries defined in a data source view for which changes can be hard to detect and track.  
  
 The `Object` element must refer to a data source in the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database. If the `Object` element refers to an object other than a data source, an error occurs.  
  
 For more information about proactive caching, see [Proactive Caching &#40;Partitions&#41;](../../multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).  
  
## See Also  
 [Commands &#40;XMLA&#41;](xml-elements-commands.md)  
  
  
