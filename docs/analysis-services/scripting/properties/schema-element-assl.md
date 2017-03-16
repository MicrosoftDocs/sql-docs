---
title: "Schema Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Schema Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Schema"
helpviewer_keywords: 
  - "Schema element"
ms.assetid: 4b6375fb-7ad8-4d5f-88b1-abd3da2654db
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Schema Element (ASSL)
  Contains the schema of the data source view.  
  
## Syntax  
  
```xml  
  
<DataSourceView>  
   ...  
   <Schema>...</Schema>  
   ...  
</DataSourceView>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Schema|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataSourceView](../../../analysis-services/scripting/objects/datasourceview-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The **Schema** is represented using the XML Schema Definition (XSD) language format of DataSets in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] .NET Framework, with some extensions for DataSets and others specific to this usage within the data definition language (DDL). DataSets define a flexible mapping from XSD to a relational schema, but then return XSD in a more canonical form. Only this canonical form is valid within data sources.  
  
 The element that corresponds to the parent of **Schema** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataSourceView>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  