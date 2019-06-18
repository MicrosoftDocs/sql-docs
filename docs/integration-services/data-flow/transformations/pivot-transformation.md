---
title: "Pivot Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.pivottrans.f1"
helpviewer_keywords: 
  - "Pivot transformation"
  - "normalized data [Integration Services]"
  - "PivotUsage property"
  - "datasets [Integration Services], normalized data"
  - "less normalized data set [Integration Services]"
ms.assetid: 55f5db6e-6777-435f-8a06-b68c129f8437
author: janinezhang
ms.author: janinez
manager: craigg
---
# Pivot Transformation

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Pivot transformation makes a normalized data set into a less normalized but more compact version by pivoting the input data on a column value. For example, a normalized **Orders** data set that lists customer name, product, and quantity purchased typically has multiple rows for any customer who purchased multiple products, with each row for that customer showing order details for a different product. By pivoting the data set on the product column, the Pivot transformation can output a data set with a single row per customer. That single row lists all the purchases by the customer, with the product names shown as column names, and the quantity shown as a value in the product column. Because not every customer purchases every product, many columns may contain null values.  
  
 When a dataset is pivoted, input columns perform different roles in the pivoting process. A column can participate in the following ways:  
  
-   The column is passed through unchanged to the output. Because many input rows can result only in one output row, the transformation copies only the first input value for the column.  
  
-   The column acts as the key or part of the key that identifies a set of records.  
  
-   The column defines the pivot. The values in this column are associated with columns in the pivoted dataset.  
  
-   The column contains values that are placed in the columns that the pivot creates.  
  
 This transformation has one input, one regular output, and one error output.  
  
## Sort and Duplicate Rows  
 To pivot data efficiently, which means creating as few records in the output dataset as possible, the input data must be sorted on the pivot column. If the data is not sorted, the Pivot transformation might generate multiple records for each value in the set key, which is the column that defines set membership. For example, if a dataset is pivoted on a **Name** column but the names are not sorted, the output dataset could have more than one row for each customer, because a pivot occurs every time that the value in **Name** changes.  
  
 The input data might contain duplicate rows, which will cause the Pivot transformation to fail. "Duplicate rows" means rows that have the same values in the set key columns and the pivot columns. To avoid failure, you can either configure the transformation to redirect error rows to an error output or you can pre-aggregate values to ensure there are no duplicate rows.  
  
##  <a name="options"></a> Options in the Pivot Dialog Box  
 You configure the pivot operation by setting the options in the **Pivot** dialog box. To open the **Pivot** dialog box, add the Pivot transformation to the package in [!INCLUDE[ssBIDevStudioFull](../../../includes/ssbidevstudiofull-md.md)], and then right-click the component and click **Edit**.  
  
 The following list describes the options in the **Pivot** dialog box.  
  
 **Pivot Key**  
 Specifies the column to use for values across the top row (header row) of the table.  
  
 **Set Key**  
 Specifies the column to use for values in the left column of the table. The input date must be sorted on this column.  
  
 **Pivot Value**  
 Specifies the column to use for the table values, other than the values in the header row and the left column.  
  
 **Ignore un-matched Pivot Key values and report them after DataFlow execution**  
 Select this option to configure the Pivot transformation to ignore rows containing unrecognized values in the **Pivot Key** column and to output all of the pivot key values to a log message, when the package is run.  
  
 You can also configure the transformation to output the values by setting the **PassThroughUnmatchedPivotKeys** custom property to **True**.  
  
 **Generate pivot output columns from values**  
 Enter the pivot key values in this box to enable the Pivot transformation to create output columns for each value. You can either enter the values prior to running the package, or do the following.  
  
1.  Select the **Ignore un-matched Pivot Key values and report them after DataFlow execution** option, and then click **OK** in the **Pivot** dialog box to save the changes to the Pivot transformation.  
  
2.  Run the package.  
  
3.  When the package succeeds, click the **Progress** tab and look for the information log message from the Pivot transformation that contains the pivot key values.  
  
4.  Right-click the message and click **Copy Message Text**.  
  
5.  Click **Stop Debugging** on the **Debug** menu to switch to the design mode.  
  
6.  Right-click the Pivot transformation, and then click **Edit**.  
  
7.  Uncheck the **Ignore un-matched Pivot Key values and report them after DataFlow execution** option, and then paste the pivot key values in the **Generate pivot output columns from values** box using the following format.  
  
     [value1],[value2],[value3]  
  
 **Generate Columns Now**  
 Click to create an output column for each pivot key value that is listed in the **Generate pivot output columns from values** box.  
  
 The output columns appear in the **Existing pivoted output columns** box.  
  
 **Existing pivoted output columns**  
 Lists the output columns for the pivot key values  
  
 The following table shows a data set before the data is pivoted on the **Year** column.  
  
|Year|Product Name|Total|  
|----------|------------------|-----------|  
|2004|HL Mountain Tire|1504884.15|  
|2003|Road Tire Tube|35920.50|  
|2004|Water Bottle - 30 oz.|2805.00|  
|2002|Touring Tire|62364.225|  
  
 The following table shows a data set after the data has been pivoted on the **Year** column.  
  
||2002|2003|2004|  
|-|----------|----------|----------|  
|HL Mountain Tire|141164.10|446297.775|1504884.15|  
|Road Tire Tube|3592.05|35920.50|89801.25|  
|Water Bottle - 30 oz.|*NULL*|*NULL*|2805.00|  
|Touring Tire|62364.225|375051.60|1041810.00|  
  
 To pivot the data on the **Year** column, as shown above, the following options are set in the **Pivot** dialog box.  
  
-   Year is selected in the **Pivot Key** list box.  
  
-   Product Name is selected in the **Set Key** list box.  
  
-   Total is selected in the **Pivot Value** list box.  
  
-   The following values are entered in the **Generate pivot output columns from values** box.  
  
     [2002],[2003],[2004]  
  
## Configuration of the Pivot Transformation  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
## Related Content  
 For information about how to set the properties of this component, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## See Also  
 [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md)   
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  
