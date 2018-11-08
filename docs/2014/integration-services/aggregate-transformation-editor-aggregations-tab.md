---
title: "Aggregate Transformation Editor (Aggregations Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.aggregationtransformation.aggregations.f1"
helpviewer_keywords: 
  - "Aggregate Transformation Editor"
ms.assetid: a01cb124-ec79-4673-b1a1-bf4d60ee1b45
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Aggregate Transformation Editor (Aggregations Tab)
  Use the **Aggregations** tab of the **Aggregate Transformation Editor** dialog box to specify columns for aggregation and aggregation properties. You can apply multiple aggregations. This transformation does not generate an error output.  
  
> [!NOTE]  
>  The options for key count, key scale, distinct key count, and distinct key scale apply at the component level when specified on the **Advanced** tab, at the output level when specified in the advanced display of the **Aggregations** tab, and at the column level when specified in the column list at the bottom of the **Aggregations** tab.  
>   
>  In the Aggregate transformation, **Keys** and **Keys scale** refer to the number of groups that are expected to result from a **Group by** operation. **Count distinct keys** and **Count distinct scale** refer to the number of distinct values that are expected to result from a **Distinct count** operation.  
  
 To learn more about the Aggregate transformation, see [Aggregate Transformation](data-flow/transformations/aggregate-transformation.md).  
  
## Options  
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
 If you choose **Group By**, use the check boxes to control how the transformation performs the comparison. For information on the string comparison options, see [Comparing String Data](data-flow/comparing-string-data.md).  
  
 **Count Distinct Scale**  
 Optionally specify the approximate number of distinct values that the aggregation can write. By default, the value of this option is **Unspecified**. If both `CountDistinctScale` and **CountDistinctKeys** are specified, **CountDistinctKeys** takes precedence.  
  
|Value|Description|  
|-----------|-----------------|  
|Unspecified|The `CountDistinctScale` property is not used.|  
|Low|Aggregation can write approximately 500,000 distinct values.|  
|Medium|Aggregation can write approximately 5,000,000 distinct values.|  
|High|Aggregation can write more than 25,000,000 distinct values.|  
  
 **Count Distinct Keys**  
 Optionally specify the exact number of distinct values that the aggregation can write. If both `CountDistinctScale` and **CountDistinctKeys** are specified, **CountDistinctKeys** takes precedence.  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Aggregate Transformation Editor &#40;Advanced Tab&#41;](../../2014/integration-services/aggregate-transformation-editor-advanced-tab.md)   
 [Aggregate Values in a Dataset by Using the Aggregate Transformation](data-flow/transformations/aggregate-values-in-a-dataset-by-using-the-aggregate-transformation.md)  
  
  
