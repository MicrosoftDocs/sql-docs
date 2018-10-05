---
title: "DataSourceViews Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataSourceViews Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataSourceViews"
helpviewer_keywords: 
  - "DataSourceViews element"
ms.assetid: f708ceac-8eeb-45ee-a2bb-919126898c80
author: minewiskan
ms.author: owend
manager: craigg
---
# DataSourceViews Element (ASSL)
  Contains the collection of [DataSourceView](../objects/datasourceview-element-assl.md) elements associated with a [Database](../objects/database-element-assl.md) element.  
  
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
|Parent elements|[Database](../objects/database-element-assl.md)|  
|Child elements|[DataSourceView](../objects/datasourceview-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSourceViewCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
