---
title: "Set Custom Member Formulas for Attributes in a Dimension | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Business Intelligence enhancements [Analysis Services], custom member formulas"
  - "member formulas [Analysis Services]"
  - "dimensions [Analysis Services], Business Intelligence enhancements"
  - "custom member formulas [Analysis Services]"
  - "CustomRollupColumn property"
ms.assetid: c4467b08-ce59-4de7-a2d9-c22e246bdd52
author: minewiskan
ms.author: owend
manager: craigg
---
# Set Custom Member Formulas for Attributes in a Dimension
  Add a custom member formula enhancement to a cube or dimension to replace the default aggregation that is associated with a dimension member with the results of a Multidimensional Expressions (MDX) expression. (This enhancement sets the `CustomRollupColumn` property on a specified attribute in a dimension.)  
  
> [!NOTE]  
>  A custom member formula is available only for dimensions that are based on existing data sources. For dimensions that were created without using a data source, you must run the Schema Generation Wizard to create a data source view before adding a custom member formula.  
  
 To add a custom member formula, you use the Business Intelligence Wizard, and select the **Create a custom member formula** option on the **Choose Enhancement** page. This wizard then guides you through the steps of selecting a dimension to which you want to apply a custom member formula and enabling the custom member formula.  
  
## Selecting a Dimension  
 On the first **Create a Custom Member Formula** page of the wizard, you specify the dimension to which you want to apply a custom member formula. The custom member formula enhancement added to this selected dimension will result in changes to the dimension. These changes will be inherited by all cubes that include the selected dimension.  
  
## Enabling a Custom Member Formula  
 On the second **Create a Custom Member Formula** page, you associate the source column that contains the custom member formula with one or more attributes in the dimension. In the **Attribute** column, select the check box next to the attribute that you want to associate with the custom member formula column. After you select each attribute, the wizard displays the **Select a Column** dialog box. In this dialog box, click the column in the dimension table that contains the formula. If you want to change a selection after you close the **Select a Column** dialog box, click the **Source Column** cell that you want to change, and then click the ellipsis (**...**) to open the **Select a Column** dialog box again.  
  
## See Also  
 [Use the Business Intelligence Wizard to Enhance Dimensions](../use-the-business-intelligence-wizard-to-enhance-dimensions.md)  
  
  
