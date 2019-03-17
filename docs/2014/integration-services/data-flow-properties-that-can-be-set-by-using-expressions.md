---
title: "Data Flow Properties that Can Be Set by Using Expressions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "SQL Server Integration Services packages, property expressions"
  - "Integration Services packages, property expressions"
  - "packages [Integration Services], properties"
  - "SSIS packages, property expressions"
  - "property expressions [Integration Services]"
ms.assetid: cd0e171a-08be-45d6-81dc-ed94f37698b8
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Data Flow Properties that Can Be Set by Using Expressions
  The values of certain properties of data flow objects can be specified by using property expressions available on the Data Flow task container.  
  
 For information about using property expressions, see [Use Property Expressions in Packages](expressions/use-property-expressions-in-packages.md).  
  
 You can use property expressions to customize configurations for each deployed instance of a package. You can also use property expressions to specify run-time constraints for a package by using the **/set** option with the **dtexec** command prompt utility. For example, you can constrain the `MaximumThreads` used by the Sort transformation, or the `MaxMemoryUsage` of the Fuzzy Grouping and Fuzzy Lookup transformations. If unconstrained, these transformations may cache large amounts of data in memory.  
  
 To specify a property expression for one of the properties of data flow objects listed in this topic, display the **Properties** window for the Data Flow task by selecting the Data Flow task on the **Control Flow** surface of the designer, or by selecting the **Data Flow** tab of the designer without selecting any individual component or path. Select the **Expressions** property and click the ellipsis (...) to display the **Property Expressions Editor** dialog box. Drop down the **Property** list to select a property, then type an expression in the **Expression** text box, or click the ellipsis (...) to display the **Expression Builder** dialog box.  
  
 The **Property** list displays available properties for only those data flow objects that you have already placed on the **Data Flow** surface of the designer. Therefore, you cannot use the **Property** list to view all the possible properties of data flow objects that support property expressions. For example, if you have placed an ADO NET source on the designer surface, the **Property** list contains an entry for the `[ADO NET Source].[SqlCommand]` property. The list also displays many properties of the Data Flow task itself.  
  
## Properties of Data Flow Objects That Support Property Expressions  
 The values of the properties in the following list can be specified by using property expressions.  
  
### Data Flow Sources  
  
|Data Flow object|Property|  
|----------------------|--------------|  
|ADO NET source|TableOrViewName property<br /><br /> SqlCommand property|  
|XML source|XMLData property<br /><br /> XMLSchemaDefinition property|  
  
### Data Flow Transformations  
 For more information about these custom properties, see [Transformation Custom Properties](data-flow/transformations/transformation-custom-properties.md).  
  
|Data Flow object|Property|  
|----------------------|--------------|  
|Conditional Split transformation|FriendlyExpression property|  
|Derived Column transformation|FriendlyExpression property|  
|Fuzzy Grouping transformation|MaxMemoryUsage property|  
|Fuzzy Lookup transformation|MaxMemoryUsage property|  
|Lookup transformation|SqlCommand property<br /><br /> SqlCommandParam property|  
|OLE DB Command transformation|SqlCommand property|  
|Percentage Sampling transformation|SamplingValue property|  
|Pivot transformation|PivotKeyValue property|  
|Row Sampling transformation|SamplingValue property|  
|Sort transformation|MaximumThreads property|  
|Unpivot transformation|PivotKeyValue property|  
  
### Data Flow Destinations  
  
|Data Flow object|Property|  
|----------------------|--------------|  
|ADO NET Destination|TableOrViewName property<br /><br /> BatchSize property<br /><br /> CommandTimeout property|  
|Flat File destination|Header property|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Compact destination|TableName property|  
|[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] destination|BulkInsertTableName property<br /><br /> BulkInsertFirstRow property<br /><br /> BulkInsertLastRow property<br /><br /> BulkInsertOrder property<br /><br /> Timeout property|  
  
## Related Tasks  
  
-   [Add or Change a Property Expression](expressions/add-or-change-a-property-expression.md)  
  
## Related Content  
 Technical article, [SSIS Expression Cheat Sheet](http://pragmaticworks.com/cheatsheet/), on pragmaticworks.com  
  
## See Also  
 [Use Property Expressions in Packages](expressions/use-property-expressions-in-packages.md)   
 [Common Properties](../../2014/integration-services/common-properties.md)   
 [Transformation Custom Properties](data-flow/transformations/transformation-custom-properties.md)   
 [Path Properties](../../2014/integration-services/path-properties.md)  
  
  
