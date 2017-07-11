---
title: "DataSourceViewID Element (ASSL) | Microsoft Docs"
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
  - "DataSourceViewID Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "DataSourceViewID"
helpviewer_keywords: 
  - "DataSourceViewID element"
ms.assetid: dcf617fe-0bf6-4767-af35-07c0c7fd96e5
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DataSourceViewID Element (ASSL)
  Identifies the [DataSourceView](../../../analysis-services/scripting/objects/datasourceview-element-assl.md) element associated with the [Binding](../../../analysis-services/scripting/data-type/binding-data-type-assl.md) parent element.  
  
## Syntax  
  
```xml  
  
<DataSourceViewBinding> <!-- or DSVTableBinding -->  
   ...  
   <DataSourceViewID>...</DataSourceViewID>  
   ...  
</DataSourceViewBinding>  
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
|Parent element|[DataSourceViewBinding](../../../analysis-services/scripting/data-type/datasourceviewbinding-data-type-assl.md), [DSVTableBinding](../../../analysis-services/scripting/data-type/dsvtablebinding-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of **DataSourceViewID** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.DataSourceViewBinding> and <xref:Microsoft.AnalysisServices.DSVTableBinding>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  