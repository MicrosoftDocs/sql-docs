---
title: "Add a Custom Aggregation to a Dimension | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "dimensions [Analysis Services], Business Intelligence enhancements"
  - "Business Intelligence enhancements [Analysis Services], custom aggregations"
  - "aggregations [Analysis Services], custom"
  - "unary operators"
  - "custom aggregations [Analysis Services]"
ms.assetid: 3199a6c2-a06d-47b9-bd1c-604dbb085318
author: minewiskan
ms.author: owend
manager: craigg
---
# Add a Custom Aggregation to a Dimension
  Add a custom aggregation enhancement to a cube or dimension to replace the default aggregations that are associated with a dimension member with a different unary operator. This enhancement specifies a unary operator column in the dimension table that defines rollup for members in a parent-child hierarchy. The unary operator acts on the parent attribute in a parent-child hierarchy.  
  
> [!NOTE]  
>  A custom aggregation is available only for dimensions that are based on existing data sources. For dimensions that were created without using a data source, you must run the Schema Generation Wizard to create a data source view before adding the custom aggregation.  
  
 To add a custom aggregation, you use the Business Intelligence Wizard, and select the **Specify a unary operator** option on the **Choose Enhancement** page. This wizard then guides you through the steps of selecting a dimension to which you want to apply a custom aggregation and identifying the custom aggregation.  
  
> [!NOTE]  
>  Before you run the Business Intelligence Wizard to add a custom aggregation, make sure that the dimension that you want to enhance contains a parent-child attribute hierarchy. For more information, see [Parent-Child Hierarchy](parent-child-dimension.md).  
  
## Selecting a Dimension  
 On the first **Specify a Unary Operator** page of the wizard, you specify the dimension to which you want to apply a custom aggregation. The custom aggregation added to this selected dimension will result in changes to the dimension. These changes will be inherited by all cubes that include the selected dimension.  
  
## Adding Custom Aggregation (Unary Operator)  
 On the second **Specify a Unary Operator** page, you specify the parent attribute that you want for the custom aggregation and the source column in the dimension table for the unary operator. **Parent attribute** lists attributes that have their `Usage` property set to `Parent`. If there is more than one parent attribute, choose the parent attribute that corresponds to the parent-child relationship that you want to use. If there is no parent attribute listed, then the dimension does not have a valid parent-child hierarchy.  
  
 In **Source column**, you select the string column that contains the unary operators. (This selection sets the `UnaryOperatorColumn` property on the parent attribute.) The dimension table should also have a string column that specifies the unary rollup operator. The string values in this column should contain valid aggregation operators. If a row is empty, the corresponding member is calculated normally. If the formula in a column is not valid, a run-time error occurs when a cell value that uses the member is retrieved. For more information, see [Unary Operators in Parent-Child Dimensions](parent-child-dimension-attributes-unary-operators.md).  
  
  
