---
title: "Aggregate Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.aggregatetrans.f1"
  - "sql13.dts.designer.aggregationtransformation.aggregations.f1"
  - "sql13.dts.designer.aggregationtransformation.advanced.f1"
helpviewer_keywords: 
  - "IsBig property"
  - "aggregate functions [Integration Services]"
  - "Aggregate transformation [Integration Services]"
  - "large data, SSIS transformations"
ms.assetid: 2871cf2a-fbd3-41ba-807d-26ffff960e81
author: janinezhang
ms.author: janinez
manager: craigg
---
# Aggregate Transformation
  The Aggregate transformation applies aggregate functions, such as Average, to column values and copies the results to the transformation output. Besides aggregate functions, the transformation provides the GROUP BY clause, which you can use to specify groups to aggregate across.  
  
## Operations  
 The Aggregate transformation supports the following operations.  
  
|Operation|Description|  
|---------------|-----------------|  
|Group by|Divides datasets into groups. Columns of any data type can be used for grouping. For more information, see [GROUP BY &#40;Transact-SQL&#41;](../../../t-sql/queries/select-group-by-transact-sql.md).|  
|Sum|Sums the values in a column. Only columns with numeric data types can be summed. For more information, see [SUM &#40;Transact-SQL&#41;](../../../t-sql/functions/sum-transact-sql.md).|  
|Average|Returns the average of the column values in a column. Only columns with numeric data types can be averaged. For more information, see [AVG &#40;Transact-SQL&#41;](../../../t-sql/functions/avg-transact-sql.md).|  
|Count|Returns the number of items in a group. For more information, see [COUNT &#40;Transact-SQL&#41;](../../../t-sql/functions/count-transact-sql.md).|  
|Count distinct|Returns the number of unique nonnull values in a group.|  
|Minimum|Returns the minimum value in a group. For more information, see [MIN &#40;Transact-SQL&#41;](../../../t-sql/functions/min-transact-sql.md). In contrast to the Transact-SQL MIN function, this operation can be used only with numeric, date, and time data types.|  
|Maximum|Returns the maximum value in a group. For more information, see [MAX &#40;Transact-SQL&#41;](../../../t-sql/functions/max-transact-sql.md). In contrast to the Transact-SQL MAX function, this operation can be used only with numeric, date, and time data types.|  
  
 The Aggregate transformation handles null values in the same way as the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] relational database engine. The behavior is defined in the SQL-92 standard. The following rules apply:  
  
-   In a GROUP BY clause, nulls are treated like other column values. If the grouping column contains more than one null value, the null values are put into a single group.  
  
-   In the COUNT (column name) and COUNT (DISTINCT column name) functions, nulls are ignored and the result excludes rows that contain null values in the named column.  
  
-   In the COUNT (*) function, all rows are counted, including rows with null values.  
  
## Big Numbers in Aggregates  
 A column may contain numeric values that require special consideration because of their large value or precision requirements. The Aggregation transformation includes the IsBig property, which you can set on output columns to invoke special handling of big or high-precision numbers. If a column value may exceed 4 billion or a precision beyond a float data type is required, IsBig should be set to 1.  
  
 Setting the IsBig property to 1 affects the output of the aggregation transformation in the following ways:  
  
-   The DT_R8 data type is used instead of the DT_R4 data type.  
  
-   Count results are stored as the DT_UI8 data type.  
  
-   Distinct count results are stored as the DT_UI4 data type.  
  
> [!NOTE]  
>  You cannot set IsBig to 1 on columns that are used in the GROUP BY, Maximum, or Minimum operations.  
  
## Performance Considerations  
 The Aggregate transformation includes a set of properties that you can set to enhance the performance of the transformation.  
  
-   When performing a **Group by** operation, set the Keys or KeysScale properties of the component and the component outputs. Using Keys, you can specify the exact number of keys the transformation is expected to handle. (In this context, Keys refers to the number of groups that are expected to result from a **Group by** operation.) Using KeysScale, you can specify an approximate number of keys. When you specify an appropriate value for Keys or KeyScale, you improve performance because the tranformation is able to allocate adequate memory for the data that the transformation caches.  
  
-   When performing a **Distinct count** operation, set the CountDistinctKeys or CountDistinctScale properties of the component. Using CountDistinctKeys, you can specify the exact number of keys the transformation is expected to handle for a count distinct operation. (In this context, CountDistinctKeys refers to the number of distinct values that are expected to result from a **Distinct count** operation.) Using CountDistinctScale, you can specify an approximate number of keys for a count distinct operation. When you specify an appropriate value for CountDistinctKeys or CountDistinctScale, you improve performance because the transformation is able to allocate adequate memory for the data that the transformation caches.  
  
## Aggregate Transformation Configuration  
 You configure the Aggregate transformation at the transformation, output, and column levels.  
  
-   At the transformation level, you configure the Aggregate transformation for performance by specifying the following values:  
  
    -   The number of groups that are expected to result from a **Group by** operation.  
  
    -   The number of distinct values that are expected to result from a **Count distinct** operation.  
  
    -   The percentage by which memory can be extended during the aggregation.  
  
     The Aggregate transformation can also be configured to generate a warning instead of failing when the value of a divisor is zero.  
  
-   At the output level, you configure the Aggregate transformation for performance by specifying the number of groups that are expected to result from a **Group by** operation. The Aggregate transformation supports multiple outputs, and each can be configured differently.  
  
-   At the column level, you specify the following values:  
  
    -   The aggregation that the column performs.  
  
    -   The comparison options of the aggregation.  
  
 You can also configure the Aggregate transformation for performance by specifying these values:  
  
-   The number of groups that are expected to result from a **Group by** operation on the column.  
  
-   The number of distinct values that are expected to result from a **Count distinct** operation on the column.  
  
 You can also identify columns as IsBig if a column contains large numeric values or numeric values with high precision.  
  
 The Aggregate transformation is asynchronous, which means that it does not consume and publish data row by row. Instead it consumes the whole rowset, performs its groupings and aggregations, and then publishes the results.  
  
 This transformation does not pass through any columns, but creates new columns in the data flow for the data it publishes. Only the input columns to which aggregate functions apply or the input columns the transformation uses for grouping are copied to the transformation output. For example, an Aggregate transformation input might have three columns: **CountryRegion**, **City**, and **Population**. The transformation groups by the **CountryRegion** column and applies the Sum function to the **Population** column. Therefore the output does not include the **City** column.  
  
 You can also add multiple outputs to the Aggregate transformation and direct each aggregation to a different output. For example, if the Aggregate transformation applies the Sum and the Average functions, each aggregation can be directed to a different output.  
  
 You can apply multiple aggregations to a single input column. For example, if you want the sum and average values for an input column named **Sales**, you can configure the transformation to apply both the Sum and Average functions to the **Sales** column.  
  
 The Aggregate transformation has one input and one or more outputs. It does not support an error output.  
  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 The **Advanced Editor** dialog box reflects the properties that can be set programmatically. For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](https://msdn.microsoft.com/library/51973502-5cc6-4125-9fce-e60fa1b7b796)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
 For more information about how to set properties, click one of the following topics:  
  
-   [Aggregate Values in a Dataset by Using the Aggregate Transformation](../../../integration-services/data-flow/transformations/aggregate-values-in-a-dataset-by-using-the-aggregate-transformation.md)  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
-   [Sort Data for the Merge and Merge Join Transformations](../../../integration-services/data-flow/transformations/sort-data-for-the-merge-and-merge-join-transformations.md)  
  
## Related Tasks  
 [Aggregate Values in a Dataset by Using the Aggregate Transformation](../../../integration-services/data-flow/transformations/aggregate-values-in-a-dataset-by-using-the-aggregate-transformation.md)  
  
## Aggregate Transformation Editor (Aggregations Tab)
  Use the **Aggregations** tab of the **Aggregate Transformation Editor** dialog box to specify columns for aggregation and aggregation properties. You can apply multiple aggregations. This transformation does not generate an error output.  
  
> [!NOTE]  
>  The options for key count, key scale, distinct key count, and distinct key scale apply at the component level when specified on the **Advanced** tab, at the output level when specified in the advanced display of the **Aggregations** tab, and at the column level when specified in the column list at the bottom of the **Aggregations** tab.  
>   
>  In the Aggregate transformation, **Keys** and **Keys scale** refer to the number of groups that are expected to result from a **Group by** operation. **Count distinct keys** and **Count distinct scale** refer to the number of distinct values that are expected to result from a **Distinct count** operation.  
  
### Options  
 **Advanced / Basic**  
 Display or hide options to configure multiple aggregations for multiple outputs. By default, the Advanced options are hidden.  
  
 **Aggregation Name**  
 In the Advanced display, type a friendly name for the aggregation.  
  
 **Group By Columns**  
 In the Advanced display, select columns for grouping by using the **Available Input Columns** list as described below.  
  
 **Key Scale**  
 In the Advanced display, optionally specify the approximate number of keys that the aggregation can write. By default, the value of this option is **Unspecified**. If both the **Key Scale** and **Keys** properties are set, the value of **Keys** takes precedence.  
  
|Value|Description|  
|-----------|-----------------|  
|Unspecified|The Key Scale property is not used.|  
|Low|Aggregation can write approximately 500,000 keys.|  
|Medium|Aggregation can write approximately 5,000,000 keys.|  
|High|Aggregation can write more than 25,000,000 keys.|  
  
 **Keys**  
 In the Advanced display, optionally specify the exact number of keys that the aggregation can write. If both **Key Scale** and **Keys** are specified, **Keys** takes precedence.  
  
 **Available Input Columns**  
 Select from the list of available input columns by using the check boxes in this table.  
  
 **Input Column**  
 Select from the list of available input columns.  
  
 **Output Alias**  
 Type an alias for each column. The default is the name of the input column; however, you can choose any unique, descriptive name.  
  
 **Operation**  
 Choose from the list of available operations, using the following table as a guide.  
  
|Operation|Description|  
|---------------|-----------------|  
|**GroupBy**|Divides datasets into groups. Columns with any data type can be used for grouping. For more information, see GROUP BY.|  
|**Sum**|Sums the values in a column. Only columns with numeric data types can be summed. For more information, see SUM.|  
|**Average**|Returns the average of the column values in a column. Only columns with numeric data types can be averaged. For more information, see AVG.|  
|**Count**|Returns the number of items in a group. For more information, see COUNT.|  
|**CountDistinct**|Returns the number of unique nonnull values in a group. For more information, see COUNT and Distinct.|  
|**Minimum**|Returns the minimum value in a group. Restricted to numeric data types.|  
|**Maximum**|Returns the maximum value in a group. Restricted to numeric data types.|  
  
 **Comparison Flags**  
 If you choose **Group By**, use the check boxes to control how the transformation performs the comparison. For information on the string comparison options, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).  
  
 **Count Distinct Scale**  
 Optionally specify the approximate number of distinct values that the aggregation can write. By default, the value of this option is **Unspecified**. If both **CountDistinctScale** and **CountDistinctKeys** are specified, **CountDistinctKeys** takes precedence.  
  
|Value|Description|  
|-----------|-----------------|  
|Unspecified|The **CountDistinctScale** property is not used.|  
|Low|Aggregation can write approximately 500,000 distinct values.|  
|Medium|Aggregation can write approximately 5,000,000 distinct values.|  
|High|Aggregation can write more than 25,000,000 distinct values.|  
  
 **Count Distinct Keys**  
 Optionally specify the exact number of distinct values that the aggregation can write. If both **CountDistinctScale** and **CountDistinctKeys** are specified, **CountDistinctKeys** takes precedence.  
  
## Aggregate Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Aggregate Transformation Editor** dialog box to set component properties, specify aggregations, and set properties of input and output columns.  
  
> [!NOTE]  
>  The options for key count, key scale, distinct key count, and distinct key scale apply at the component level when specified on the **Advanced** tab, at the output level when specified in the advanced display of the **Aggregations** tab, and at the column level when specified in the column list at the bottom of the **Aggregations** tab.  
>   
>  In the Aggregate transformation, **Keys** and **Keys scale** refer to the number of groups that are expected to result from a **Group by** operation. **Count distinct keys** and **Count distinct scale** refer to the number of distinct values that are expected to result from a **Distinct count** operation.  
  
### Options  
 **Keys scale**  
 Optionally specify the approximate number of keys that the aggregation expects. The transformation uses this information to optimize its initial cache size. By default, the value of this option is **Unspecified**. If both **Keys scale** and **Number of keys** are specified, **Number of keys** takes precedence.  
  
|Value|Description|  
|-----------|-----------------|  
|Unspecified|The **Keys scale** property is not used.|  
|Low|Aggregation can write approximately 500,000 keys.|  
|Medium|Aggregation can write approximately 5,000,000 keys.|  
|High|Aggregation can write more than 25,000,000 keys.|  
  
 **Number of keys**  
 Optionally specify the exact number of keys that the aggregation expects. The transformation uses this information to optimize its initial cache size. If both **Keys scale** and **Number of keys** are specified, **Number of keys** takes precedence.  
  
 **Count distinct scale**  
 Optionally specify the approximate number of distinct values that the aggregation can write. By default, the value of this option is **Unspecified**. If both **Count distinct scale** and **Count distinct keys** are specified, **Count distinct keys** takes precedence.  
  
|Value|Description|  
|-----------|-----------------|  
|Unspecified|The CountDistinctScale property is not used.|  
|Low|Aggregation can write approximately 500,000 distinct values.|  
|Medium|Aggregation can write approximately 5,000,000 distinct values.|  
|High|Aggregation can write more than 25,000,000 distinct values.|  
  
 **Count distinct keys**  
 Optionally specify the exact number of distinct values that the aggregation can write. If both **Count distinct scale** and **Count distinct keys** are specified, **Count distinct keys** takes precedence.  
  
 **Auto extend factor**  
 Use a value between 1 and 100 to specify the percentage by which memory can be extended during the aggregation. By default, the value of this option is **25%**.  
  
## See Also  
 [Data Flow](../../../integration-services/data-flow/data-flow.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
  
