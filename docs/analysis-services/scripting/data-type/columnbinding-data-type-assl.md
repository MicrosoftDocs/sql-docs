---
title: "ColumnBinding Data Type (ASSL) | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: assl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# ColumnBinding Data Type (ASSL)
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Defines a derived data type that represents the binding of a column in a data source view to a [DataItem](../../../analysis-services/scripting/data-type/dataitem-data-type-assl.md) element.  
  
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
|Base data types|[Binding](../../../analysis-services/scripting/data-type/binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[ColumnID](../../../analysis-services/scripting/properties/columnid-element-eventcolumn-assl.md), [TableID](../../../analysis-services/scripting/properties/tableid-element-assl.md)|  
|Derived elements|See [Binding](../../../analysis-services/scripting/data-type/binding-data-type-assl.md)|  
  
## Remarks  
 To create valid XML element names, [!INCLUDE[vstecado](../../../includes/vstecado-md.md)] **DataSet** objects encode table names as they serialize to XML Schema Definition (XSD); for example, the name "Order Details" becomes "Order_x0020_Details". Likewise, the **ColumnID** and **TableID** elements contained by the **ColumnBinding** element and which reference objects in the data source view (DSV) must also encode names during serialization, to ensure that the names directly match the text in the DSV. The Analysis Services instance will decode these names, just as the **DataSet** object model does.  
  
 A **TableDefinitions** element contained by an element using the **TableBinding** data type and which refers to tables in the DSV must also encode names as they serialize to XSD. However, the table names in the **Partition** bindings should not be encoded because these names are simply names of tables that exist in the database and do not have to be in the DSV. Not encoding the table names in the **Partition** bindings also achieves the following:  
  
-   It keeps the data definition library (DDL) for partitions simpler.  
  
-   It provides more consistency because partitions can either have a table name or a SELECT statement, and the SELECT statement should not be encoded.  
  
 Table and column names do not include delimiters (for example, "[" for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]).  
  
 For additional information about the **Binding** type, including tables of Analysis Services Scripting Language (ASSL) objects of the **Binding** type and the inheritance hierarchy of **Binding** types, see [Binding Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../analysis-services/multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the AMO object model is <xref:Microsoft.AnalysisServices.ColumnBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
