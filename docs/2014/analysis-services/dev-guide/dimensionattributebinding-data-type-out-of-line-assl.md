---
title: "DimensionAttributeBinding Data Type (out-of-line) (ASSL) | Microsoft Docs"
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
  - "DimensionAttributeBinding Data Type (out-of-line)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DimensionAttributeBinding data type"
ms.assetid: d8ec77a9-749f-4b08-8d56-8b6514a70248
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# DimensionAttributeBinding Data Type (out-of-line) (ASSL)
  Defines a derived data type that represents an out-of-line binding for an attribute in a dimension.  
  
## Syntax  
  
```xml  
  
<DimensionAttributeBinding>  
   <!-- The following elements extend Binding -->  
   <DatabaseID>...</DatabaseID>  
   <DimensionID>...</DimensionID>  
   <AttributeID>...</AttributeID>  
   <KeyColumns>...</KeyColumns>  
   <NameColumn>...</NameColumn>  
   <Translations>...</Translations>  
</DimensionAttributeBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[AttributeID](../../../2014/analysis-services/dev-guide/attributeid-element-assl.md), [DatabaseID](../../../2014/analysis-services/dev-guide/databaseid-element-xmla.md), [DimensionID](../../../2014/analysis-services/dev-guide/dimensionid-element-assl.md), [KeyColumns](../../../2014/analysis-services/dev-guide/keycolumns-element-assl.md), [NameColumn](../../../2014/analysis-services/dev-guide/namecolumn-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md)|  
|Derived elements|[Binding](../../../2014/analysis-services/dev-guide/binding-element-xmla.md) ([Bindings](../../../2014/analysis-services/dev-guide/attributes-element-assl.md) collection of XML for Analysis (XMLA) [Batch](../../../2014/analysis-services/dev-guide/batch-element-xmla.md) and [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md) commands)|  
  
## Remarks  
 For more information about out-of-line bindings, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  