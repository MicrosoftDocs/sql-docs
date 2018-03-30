---
title: "DbSchemaName Element (ASSL) | Microsoft Docs"
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
  - "DbSchemaName Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DbSchemaName"
helpviewer_keywords: 
  - "DbSchemaName element"
ms.assetid: ae0f0edd-7b76-400d-a288-39a36d2a746b
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DbSchemaName Element (ASSL)
  Contains the name of the schema used by the parent element in the table identified by the [DbTableName](../../../2014/analysis-services/dev-guide/dbtablename-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<TableBinding> <!-- or TableNotification -->  
   ...  
   <DbSchemaName>...</DbSchemaName>  
   ...  
</TableBinding>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[TableBinding](../../../2014/analysis-services/dev-guide/tablebinding-data-type-assl.md), [TableNotification](../../../2014/analysis-services/dev-guide/tablenotification-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `DbSchemaName` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.TableBinding> and <xref:Microsoft.AnalysisServices.TableNotification>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/properties-assl.md)  
  
  