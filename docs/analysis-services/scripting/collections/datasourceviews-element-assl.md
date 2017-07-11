---
title: "DataSourceViews Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DataSourceViews Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DataSourceViews"
helpviewer_keywords: 
  - "DataSourceViews element"
ms.assetid: f708ceac-8eeb-45ee-a2bb-919126898c80
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DataSourceViews Element (ASSL)
  Contains the collection of [DataSourceView](../../../analysis-services/scripting/objects/datasourceview-element-assl.md) elements associated with a [Database](../../../analysis-services/scripting/objects/database-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Database>  
   ...  
   <DataSourceViews>  
      <DataSourceView>...</DataSourceView>  
   </DataSourceViews>  
   ...  
</Database>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Database](../../../analysis-services/scripting/objects/database-element-assl.md)|  
|Child elements|[DataSourceView](../../../analysis-services/scripting/objects/datasourceview-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSourceViewCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  