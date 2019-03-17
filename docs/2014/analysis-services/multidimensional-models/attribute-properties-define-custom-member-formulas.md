---
title: "Define Custom Member Formulas | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "members [Analysis Services], custom"
  - "custom rollup formulas [Analysis Services]"
  - "MDX [Analysis Services], custom rollup formulas"
  - "custom member formulas [Analysis Services]"
ms.assetid: 258304e2-d900-4013-97e3-871f51dfdce2
author: minewiskan
ms.author: owend
manager: craigg
---
# Define Custom Member Formulas
  You can define a Multidimensional Expressions (MDX) expression, called a custom member formula, to supply the values for the members of a specified attribute. A column in a table from a data source view provides, for each member in an attribute, the expression used to supply the value for that member.  
  
 Custom member formulas determine the cell values that are associated with members and override the aggregate functions of measures. Custom member formulas are written in MDX. Each custom member formula applies to a single member. Custom member formulas are stored in the dimension table or in another table that has a foreign key relationship with the dimension table.  
  
 The `CustomRollupColumn` property on an attribute specifies the column that contains custom member formulas for members of the attribute. If a row in the column is empty, the cell value for the member is returned normally. If the formula in the column is not valid, a run-time error occurs whenever a cell value that uses the member is retrieved.  
  
 Before you can specify custom member formulas for an attribute, make sure that the dimension table that contains the attribute, or a directly related table, has a string column to store the custom member formulas. If this is the case, you can either set the `CustomRollupColumn` property on an attribute manually or use the Set Custom Member Formula enhancement of the Business Intelligence Wizard to enable a custom member formula on an attribute. For more information about how to use this enhancement, see [Set Custom Member Formulas for Attributes in a Dimension](bi-wizard-custom-member-formulas-for-attributes-in-a-dimension.md).  
  
## Evaluating Custom Member Formulas  
 Custom member formulas differ from calculated members. Custom member formulas apply to members that exist in dimension tables, and only provide the value of the member. In contrast, calculated members are not stored in dimension tables, and calculated member expressions define both data and metadata for additional members included in a dimension or hierarchy.  
  
 Custom member formulas override the aggregate functions associated with measures. For example, before a custom member formula is specified, a measure using the `Sum` aggregate function has the following values for the following members of the Time dimension:  
  
-   2003: 2100  
  
    -   Quarter 1: 700  
  
    -   Quarter 2: 500  
  
    -   Quarter 3: 100  
  
    -   Quarter 4: 800  
  
-   2004: 1500  
  
    -   Quarter 1: 600  
  
    -   Quarter 2: 200  
  
    -   Quarter 3: 300  
  
    -   Quarter 4: 400  
  
 With a custom member formula, the value of the member is instead supplied by the custom rollup formula. For example, the following custom member formula can be used to supply the value for the Quarter 4 child member of the 2004 member in the Time dimension as 450.  
  
```  
Time.[Quarter 3] * 1.5  
```  
  
 Custom member formulas are stored in a column of the dimension table. You enable custom rollup formulas by setting the `CustomRollupColumn` property on an attribute.  
  
 To apply a single MDX expression to all members of an attribute, create a named calculation on the dimension table that returns an MDX expression as a literal string. Then, specify the named calculation with the `CustomRollupColumn` property setting on the attribute that you want to configure. A named calculation is a column in a data source view table that returns row values defined by a SQL expression. For more information about constructing named calculations, see [Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](define-named-calculations-in-a-data-source-view-analysis-services.md)  
  
> [!NOTE]  
>  To apply an MDX expression to members of a particular level instead of to members of all levels based on a particular attribute, you can define the expression as an MDX script on the level. For more information, see [MDX Scripting Fundamentals &#40;Analysis Services&#41;](mdx/mdx-scripting-fundamentals-analysis-services.md).  
  
 If you use both calculated members and custom rollup formulas for members of an attribute, you should be aware of the order of evaluation. Calculated members are resolved before custom rollup formulas are resolved.  
  
## See Also  
 [Attributes and Attribute Hierarchies](../multidimensional-models-olap-logical-dimension-objects/attributes-and-attribute-hierarchies.md)   
 [Set Custom Member Formulas for Attributes in a Dimension](bi-wizard-custom-member-formulas-for-attributes-in-a-dimension.md)  
  
  
