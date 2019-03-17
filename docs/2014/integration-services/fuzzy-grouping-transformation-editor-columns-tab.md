---
title: "Fuzzy Grouping Transformation Editor (Columns Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.fuzzygroupingtransformation.columns.f1"
helpviewer_keywords: 
  - "Fuzzy Grouping Transformation Editor"
ms.assetid: 24f4539f-2a9f-4acd-acc7-06228a07f7df
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Fuzzy Grouping Transformation Editor (Columns Tab)
  Use the **Columns** tab of the **Fuzzy Grouping Transformation Editor** dialog box to specify the columns used to group rows with duplicate values.  
  
 To learn more about the Fuzzy Grouping transformation, see [Fuzzy Grouping Transformation](data-flow/transformations/fuzzy-grouping-transformation.md).  
  
## Options  
 **Available Input Columns**  
 Select from this list the input columns used to group rows with duplicate values.  
  
 **Name**  
 View the names of available input columns.  
  
 **Pass Through**  
 Select whether to include the input column in the output of the transformation. All columns used for grouping are automatically copied to the output. You can include additional columns by checking this column.  
  
 **Input Column**  
 Select one of the input columns selected earlier in the **Available Input Columns** list.  
  
 **Output Alias**  
 Enter a descriptive name for the corresponding output column. By default, the output column name is the same as the input column name.  
  
 **Group Output Alias**  
 Enter a descriptive name for the column that will contain the canonical value for the grouped duplicates. The default name of this output column is the input column name with _clean appended.  
  
 **Match Type**  
 Select fuzzy or exact matching. Rows are considered duplicates if they are sufficiently similar across all columns with a fuzzy match type. If you also specify exact matching on certain columns, only rows that contain identical values in the exact matching columns are considered as possible duplicates. Therefore, if you know that a certain column contains no errors or inconsistencies, you can specify exact matching on that column to increase the accuracy of the fuzzy matching on other columns.  
  
 **Minimum Similarity**  
 Set the similarity threshold at the join level by using the slider. The closer the value is to 1, the closer the resemblance of the lookup value to the source value must be to qualify as a match. Increasing the threshold can improve the speed of matching since fewer candidate records need to be considered.  
  
 **Similarity Output Alias**  
 Specify the name for a new output column that contains the similarity scores for the selected join. If you leave this value empty, the output column is not created.  
  
 **Numerals**  
 Specify the significance of leading and trailing numerals in comparing the column data. For example, if leading numerals are significant, "123 Main Street" will not be grouped with "456 Main Street."  
  
|Value|Description|  
|-----------|-----------------|  
|**Neither**|Leading and trailing numerals are not significant.|  
|**Leading**|Only leading numerals are significant.|  
|**Trailing**|Only trailing numerals are significant.|  
|**LeadingAndTrailing**|Both leading and trailing numerals are significant.|  
  
 **Comparison Flags**  
 For information about the string comparison options, see [Comparing String Data](data-flow/comparing-string-data.md).  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Identify Similar Data Rows by Using the Fuzzy Grouping Transformation](data-flow/transformations/identify-similar-data-rows-by-using-the-fuzzy-grouping-transformation.md)  
  
  
