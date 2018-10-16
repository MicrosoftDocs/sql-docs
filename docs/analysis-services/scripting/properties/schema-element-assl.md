---
title: "Schema Element (ASSL) | Microsoft Docs"
ms.date: 5/8/2018
ms.prod: sql
ms.custom: assl
ms.reviewer: owend
ms.technology: analysis-services
ms.topic: reference
author: minewiskan
ms.author: owend
manager: kfile
---
# Schema Element (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
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
  
  
