---
title: "Derived Column Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.derivedcolumntrans.f1"
  - "sql13.dts.designer.derivedcolumntransformation.f1"
helpviewer_keywords: 
  - "multiple derived columns"
  - "expressions [Integration Services], derived columns"
  - "derived columns"
  - "columns [Integration Services], derivations"
  - "Derived Column transformation"
ms.assetid: 8eba755e-8e48-4233-bd1e-09a46bf2692f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Derived Column Transformation
  The Derived Column transformation creates new column values by applying expressions to transformation input columns. An expression can contain any combination of variables, functions, operators, and columns from the transformation input. The result can be added as a new column or inserted into an existing column as a replacement value. The Derived Column transformation can define multiple derived columns, and any variable or input columns can appear in multiple expressions.  
  
 You can use this transformation to perform the following tasks:  
  
-   Concatenate data from different columns into a derived column. For example, you can combine values from the **FirstName** and **LastName** columns into a single derived column named **FullName**, by using the expression `FirstName + " " + LastName`.  
  
-   Extract characters from string data by using functions such as SUBSTRING, and then store the result in a derived column. For example, you can extract a person's initial from the **FirstName** column, by using the expression `SUBSTRING(FirstName,1,1)`.  
  
-   Apply mathematical functions to numeric data and store the result in a derived column. For example, you can change the length and precision of a numeric column, **SalesTax**, to a number with two decimal places, by using the expression `ROUND(SalesTax, 2)`.  
  
-   Create expressions that compare input columns and variables. For example, you can compare the variable **Version** against the data in the column **ProductVersion**, and depending on the comparison result, use the value of either **Version** or **ProductVersion**, by using the expression `ProductVersion == @Version? ProductVersion : @Version`.  
  
-   Extract parts of a datetime value. For example, you can use the GETDATE and DATEPART functions to extract the current year, by using the expression `DATEPART("year",GETDATE())`.  
  
-   Convert date strings to a specific format using an expression.  
  
## Configuration of the Derived Column Transformation  
 You can configure the Derived column transformation in the following ways:  
  
-   Provide an expression for each input column or new column that will be changed. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md).  
  
    > [!NOTE]  
    >  If an expression references an input column that is overwritten by the Derived Column transformation, the expression uses the original value of the column, not the derived value.  
  
-   If adding results to new columns and the data type is **string**, specify a code page. For more information, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).  
  
 The Derived Column transformation includes the FriendlyExpression custom property. This property can be updated by a property expression when the package is loaded. For more information, see [Use Property Expressions in Packages](../../../integration-services/expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
 This transformation has one input, one regular output, and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## Related Tasks  
  
-   [Derive Column Values by Using the Derived Column Transformation](../../../integration-services/data-flow/transformations/derive-column-values-by-using-the-derived-column-transformation.md)  
  
## Derived Column Transformation Editor
  Use the **Derived Column Transformation Editor** dialog box to create expressions that populate new or replacement columns.  
  
### Options  
 **Variables and Columns**  
 Build an expression that uses a variable or an input column by dragging the variable or column from the list of available variables and columns to an existing table row in the pane below, or to a new row at the bottom of the list.  
  
 **Functions and Operators**  
 Build an expression that uses a function or an operator to evaluate input data and direct output data by dragging functions and operators from the list to the pane below.  
  
 **Derived Column Name**  
 Provide a derived column name. The default is a numbered list of derived columns; however, you can choose any unique, descriptive name.  
  
 **Derived Column**  
 Select a derived column from the list. Choose whether to add the derived column as a new output column, or to replace the data in an existing column.  
  
 **Expression**  
 Type an expression or build one by dragging from the previous list of available columns, variables, functions, and operators.  
  
 The value of this property can be specified by using a property expression.  
  
 **Related topics**: [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md), [Operators &#40;SSIS Expression&#41;](../../../integration-services/expressions/operators-ssis-expression.md), and [Functions &#40;SSIS Expression&#41;](../../../integration-services/expressions/functions-ssis-expression.md)  
  
 **Data Type**  
 If adding data to a new column, the **Derived Column TransformationEditor** dialog box automatically evaluates the expression and sets the data type appropriately. The value of this column is read-only. For more information, see [Integration Services Data Types](../../../integration-services/data-flow/integration-services-data-types.md).  
  
 **Length**  
 If adding data to a new column, the **Derived Column TransformationEditor** dialog box automatically evaluates the expression and sets the column length for string data. The value of this column is read-only.  
  
 **Precision**  
 If adding data to a new column, the **Derived Column TransformationEditor** dialog box automatically sets the precision for numeric data based on the data type. The value of this column is read-only.  
  
 **Scale**  
 If adding data to a new column, the **Derived Column TransformationEditor** dialog box automatically sets the scale for numeric data based on the data type. The value of this column is read-only.  
  
 **Code Page**  
 If adding data to a new column, the **Derived Column TransformationEditor** dialog box automatically sets code page for the DT_STR data type. You can update **Code Page**.  
  
 **Configure error output**  
 Specify how to handle errors by using the [Configure Error Output](https://msdn.microsoft.com/library/5f8da390-fab5-44f8-b268-d8fa313ce4b9) dialog box.  
  
## Related Content  
 Technical article, [SSIS Expression Examples](https://go.microsoft.com/fwlink/?LinkId=220761), on social.technet.microsoft.com  
