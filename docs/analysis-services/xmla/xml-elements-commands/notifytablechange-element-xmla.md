---
title: "NotifyTableChange Element (XMLA) | Microsoft Docs"
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
# NotifyTableChange Element (XMLA)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../../includes/ssas-appliesto-sqlas-aas.md)]
  Notifies an instance of Analysis Services that a change has occurred to tables in a specified data source.  
  
## Syntax  
  
```xml  
  
<Command>  
   <NotifyTableChange>  
      <Object>...</Object>  
      <TableNotifications>...</TableNotifications>  
   </NotifyTableChange>  
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
|Child elements|[Object](../../../analysis-services/xmla/xml-elements-properties/object-element-xmla.md), [TableNotifications](../../../analysis-services/xmla/xml-elements-properties/tablenotifications-element-xmla.md)|  
  
## Remarks  
 The **NotifyTableChange** command allows a client application to explicitly notify an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance that one or more tables contained in a data source have been changed. For proactive caching, this notification indicates that relational OLAP (ROLAP) objects based on those tables should be reviewed and updated.  
  
 This method of notification is best used for ROLAP objects based on views or named queries defined in a data source view for which changes can be hard to detect and track.  
  
 The **Object** element must refer to a data source in the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database. If the **Object** element refers to an object other than a data source, an error occurs.  
  
 For more information about proactive caching, see [Proactive Caching &#40;Partitions&#41;](../../../analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-proactive-caching.md).  
  
## See also
 [Commands &#40;XMLA&#41;](../../../analysis-services/xmla/xml-elements-commands/xml-elements-commands.md)  
  
  
