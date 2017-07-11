---
title: "TableBinding Data Type (ASSL) | Microsoft Docs"
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
  - "TableBinding Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "TableBinding"
helpviewer_keywords: 
  - "TableBinding data type"
ms.assetid: 3195dca4-82bf-46b7-a31f-5383586e3573
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# TableBinding Data Type (ASSL)
  Defines a derived data type that represents a binding to a table.  
  
## Syntax  
  
```xml  
  
<TableBinding>  
   <!-- The following elements extend TabularBinding -->  
   <DataSourceID>...</DataSourceID>  
   <DbTableName>...</DbTableName>  
   <DbSchemaName>...</DbSchemaName>  
</TableBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[TabularBinding](../../../analysis-services/scripting/data-type/tabularbinding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[DataSourceID](../../../analysis-services/scripting/properties/datasourceid-element-assl.md), [DbSchemaName](../../../analysis-services/scripting/properties/dbschemaname-element-assl.md), [DbTableName](../../../analysis-services/scripting/properties/dbtablename-element-assl.md)|  
|Derived elements|See [Binding](../../../analysis-services/scripting/data-type/binding-data-type-assl.md)|  
  
## Remarks  
 Note that referencing other tables in the filter expression by use of a subselect could have performance implications in some data sources. However, the designer can totally control the SQL expression by defining a named query in the data source view, and then referencing that.  
  
 The method of defining the bindings for a partition are independent of the use of partitioned tables in the data source view.  
  
 As an example, consider a measure group whose default table is "Sales," with columns Date, Product ID, Qty, Price, and Amount (calculated in the data source view). Then the partition "Sales97" could use the table "Sales97" with filter "Year(Sales.Date) = 97."  
  
 The effective query is:  
  
```  
SELECT Date, Product ID, Qty, Price, Qty * Price AS Amount   
   FROM Sales97 As Sales  
   WHERE Year(Sales.Date) = 97  
```  
  
 The calculated expression still applies, even if the expression used qualified table names (for example, Sales.Qty). The same applies if instead the table were replaced by some query "SELECT…" The FROM clause above would become "FROM SELECT ... As Sales."  
  
 For more information about the **Binding** type, including tables of Analysis Services Scripting Language (ASSL) objects of type **Binding** and the inheritance hierarchy of **Binding** types, see [Binding Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../analysis-services/multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.TableBinding>.  
  
## See Also  
 [Binding Data Type &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/binding-data-type-assl.md)   
 [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../analysis-services/multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  