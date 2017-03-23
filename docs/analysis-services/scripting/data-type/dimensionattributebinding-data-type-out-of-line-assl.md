---
title: "DimensionAttributeBinding Data Type (out-of-line) (ASSL) | Microsoft Docs"
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
  - "DimensionAttributeBinding Data Type (out-of-line)"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DimensionAttributeBinding data type"
ms.assetid: d8ec77a9-749f-4b08-8d56-8b6514a70248
caps.latest.revision: 9
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Base data types|[Binding](../../../analysis-services/scripting/data-type/binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[AttributeID](../../../analysis-services/scripting/properties/attributeid-element-assl.md), [DatabaseID](../../../analysis-services/xmla/xml-elements-properties/databaseid-element-xmla.md), [DimensionID](../../../analysis-services/scripting/properties/dimensionid-element-assl.md), [KeyColumns](../../../analysis-services/scripting/collections/keycolumns-element-assl.md), [NameColumn](../../../analysis-services/scripting/objects/namecolumn-element-assl.md), [Translations](../../../analysis-services/scripting/collections/translations-element-assl.md)|  
|Derived elements|[Binding](../../../analysis-services/xmla/xml-elements-properties/binding-element-xmla.md) ([Bindings](../../../analysis-services/scripting/collections/attributes-element-assl.md) collection of XML for Analysis (XMLA) [Batch](../../../analysis-services/xmla/xml-elements-commands/batch-element-xmla.md) and [Process](../../../analysis-services/xmla/xml-elements-commands/process-element-xmla.md) commands)|  
  
## Remarks  
 For more information about out-of-line bindings, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../analysis-services/multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  