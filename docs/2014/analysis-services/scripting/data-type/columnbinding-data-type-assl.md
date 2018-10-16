---
title: "ColumnBinding Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ColumnBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ColumnBinding"
helpviewer_keywords: 
  - "ColumnBinding data type"
ms.assetid: 3ab1bac1-6716-4b17-a107-d5f9c744c5e6
author: minewiskan
ms.author: owend
manager: craigg
---
# ColumnBinding Data Type (ASSL)
  Defines a derived data type that represents the binding of a column in a data source view to a [DataItem](dataitem-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<ColumnBinding>  
   <!-- The following elements extend Binding -->  
   <TableID>...</TableID>  
      <ColumnID>...</ColumnID>  
</ColumnBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Binding](binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[ColumnID](../properties/columnid-element-eventcolumn-assl.md), [TableID](../properties/id-element-assl.md)|  
|Derived elements|See [Binding](binding-data-type-assl.md)|  
  
## Remarks  
 To create valid XML element names, [!INCLUDE[vstecado](../../../includes/vstecado-md.md)] `DataSet` objects encode table names as they serialize to XML Schema Definition (XSD); for example, the name "Order Details" becomes "Order_x0020_Details". Likewise, the `ColumnID` and `TableID` elements contained by the `ColumnBinding` element and which reference objects in the data source view (DSV) must also encode names during serialization, to ensure that the names directly match the text in the DSV. The Analysis Services instance will decode these names, just as the `DataSet` object model does.  
  
 A `TableDefinitions` element contained by an element using the `TableBinding` data type and which refers to tables in the DSV must also encode names as they serialize to XSD. However, the table names in the `Partition` bindings should not be encoded because these names are simply names of tables that exist in the database and do not have to be in the DSV. Not encoding the table names in the `Partition` bindings also achieves the following:  
  
-   It keeps the data definition library (DDL) for partitions simpler.  
  
-   It provides more consistency because partitions can either have a table name or a SELECT statement, and the SELECT statement should not be encoded.  
  
 Table and column names do not include delimiters (for example, "[" for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]).  
  
 For additional information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the AMO object model is <xref:Microsoft.AnalysisServices.ColumnBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
