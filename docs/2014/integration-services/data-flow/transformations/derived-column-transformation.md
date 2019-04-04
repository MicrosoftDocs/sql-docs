---
title: "Derived Column Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.derivedcolumntrans.f1"
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
  
-   Provide an expression for each input column or new column that will be changed. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../expressions/integration-services-ssis-expressions.md).  
  
    > [!NOTE]  
    >  If an expression references an input column that is overwritten by the Derived Column transformation, the expression uses the original value of the column, not the derived value.  
  
-   If adding results to new columns and the data type is `string`, specify a code page. For more information, see [Comparing String Data](../comparing-string-data.md).  
  
 The Derived Column transformation includes the FriendlyExpression custom property. This property can be updated by a property expression when the package is loaded. For more information, see [Use Property Expressions in Packages](../../expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](transformation-custom-properties.md).  
  
 This transformation has one input, one regular output, and one error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Derived Column Transformation Editor** dialog box, see [Derived Column Transformation Editor](../../derived-column-transformation-editor.md).  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md)  
  
## Related Tasks  
  
-   [Derive Column Values by Using the Derived Column Transformation](derived-column-transformation.md)  
  
## Related Content  
 Technical article, [SSIS Expression Examples](https://go.microsoft.com/fwlink/?LinkId=220761), on social.technet.microsoft.com  
  
 Blog, [How to Split Column Data using SSIS](https://microsoft-ssis.blogspot.com/2012/10/split-multi-value-column-into-multiple.html).  
  
  
