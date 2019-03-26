---
title: "Fuzzy Grouping Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.fuzzygroupingtrans.f1"
helpviewer_keywords: 
  - "cleaning data"
  - "comparing data"
  - "token delimiters [Integration Services]"
  - "temporary indexes [Integration Services]"
  - "Fuzzy Grouping transformation"
  - "temporary tables [Integration Services]"
  - "grouping data"
  - "standardizing data [Integration Services]"
  - "columns [Integration Services], standardizing"
  - "similarity thresholds [Integration Services]"
  - "data cleaning [Integration Services]"
  - "duplicate data [Integration Services]"
ms.assetid: e43f17bd-9d13-4a8f-9f29-cce44cac1025
author: janinezhang
ms.author: janinez
manager: craigg
---
# Fuzzy Grouping Transformation
  The Fuzzy Grouping transformation performs data cleaning tasks by identifying rows of data that are likely to be duplicates and selecting a canonical row of data to use in standardizing the data.  
  
> [!NOTE]  
>  For more detailed information about the Fuzzy Grouping transformation, including performance and memory limitations, see the white paper, [Fuzzy Lookup and Fuzzy Grouping in SQL Server Integration Services 2005](https://go.microsoft.com/fwlink/?LinkId=96604).  
  
 The Fuzzy Grouping transformation requires a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to create the temporary [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tables that the transformation algorithm requires to do its work. The connection must resolve to a user who has permission to create tables in the database.  
  
 To configure the transformation, you must select the input columns to use when identifying duplicates, and you must select the type of match-fuzzy or exact-for each column. An exact match guarantees that only rows that have identical values in that column will be grouped. Exact matching can be applied to columns of any [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] data type except DT_TEXT, DT_NTEXT, and DT_IMAGE. A fuzzy match groups rows that have approximately the same values. The method for approximate matching of data is based on a user-specified similarity score. Only columns with the DT_WSTR and DT_STR data types can be used in fuzzy matching. For more information, see [Integration Services Data Types](../integration-services-data-types.md).  
  
 The transformation output includes all input columns, one or more columns with standardized data, and a column that contains the similarity score. The score is a decimal value between 0 and 1. The canonical row has a score of 1. Other rows in the fuzzy group have scores that indicate how well the row matches the canonical row. The closer the score is to 1, the more closely the row matches the canonical row. If the fuzzy group includes rows that are exact duplicates of the canonical row, these rows also have a score of 1. The transformation does not remove duplicate rows; it groups them by creating a key that relates the canonical row to similar rows.  
  
 The transformation produces one output row for each input row, with the following additional columns:  
  
-   **_key_in**, a column that uniquely identifies each row.  
  
-   **_key_out**, a column that identifies a group of duplicate rows. The **_key_out** column has the value of the **_key_in** column in the canonical data row. Rows with the same value in **_key_out** are part of the same group. The **_key_out**value for a group corresponds to the value of **_key_in** in the canonical data row.  
  
-   **_score**, a value between 0 and 1 that indicates the similarity of the input row to the canonical row.  
  
 These are the default column names and you can configure the Fuzzy Grouping transformation to use other names. The output also provides a similarity score for each column that participates in a fuzzy grouping.  
  
 The Fuzzy Grouping transformation includes two features for customizing the grouping it performs: token delimiters and similarity threshold. The transformation provides a default set of delimiters used to tokenize the data, but you can add new delimiters that improve the tokenization of your data.  
  
 The similarity threshold indicates how strictly the transformation identifies duplicates. The similarity thresholds can be set at the component and the column levels. The column-level similarity threshold is only available to columns that perform a fuzzy match. The similarity range is 0 to 1. The closer to 1 the threshold is, the more similar the rows and columns must be to qualify as duplicates. You specify the similarity threshold among rows and columns by setting the MinSimilarity property at the component and column levels. To satisfy the similarity that is specified at the component level, all rows must have a similarity across all columns that is greater than or equal to the similarity threshold that is specified at the component level.  
  
 The Fuzzy Grouping transformation calculates internal measures of similarity, and rows that are less similar than the value specified in MinSimilarity are not grouped.  
  
 To identify a similarity threshold that works for your data, you may have to apply the Fuzzy Grouping transformation several times using different minimum similarity thresholds. At run time, the score columns in transformation output contain the similarity scores for each row in a group. You can use these values to identify the similarity threshold that is appropriate for your data. If you want to increase similarity, you should set MinSimilarity to a value larger than the value in the score columns.  
  
 You can customize the grouping that the transformation performs by setting the properties of the columns in the Fuzzy Grouping transformation input. For example, the FuzzyComparisonFlags property specifies how the transformation compares the string data in a column, and the ExactFuzzy property specifies whether the transformation performs a fuzzy match or an exact match.  
  
 The amount of memory that the Fuzzy Grouping transformation uses can be configured by setting the MaxMemoryUsage custom property. You can specify the number of megabytes (MB) or use the value 0 to allow the transformation to use a dynamic amount of memory based on its needs and the physical memory available. The MaxMemoryUsage custom property can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](transformation-custom-properties.md).  
  
 This transformation has one input and one output. It does not support an error output.  
  
## Row Comparison  
 When you configure the Fuzzy Grouping transformation, you can specify the comparison algorithm that the transformation uses to compare rows in the transformation input. If you set the Exhaustive property to `true`, the transformation compares every row in the input to every other row in the input. This comparison algorithm may produce more accurate results, but it is likely to make the transformation perform more slowly unless the number of rows in the input is small. To avoid performance issues, it is advisable to set the Exhaustive property to `true` only during package development.  
  
## Temporary Tables and Indexes  
 At run time, the Fuzzy Grouping transformation creates temporary objects such as tables and indexes, potentially of significant size, in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database that the transformation connects to. The size of the tables and indexes are proportional to the number of rows in the transformation input and the number of tokens created by the Fuzzy Grouping transformation.  
  
 The transformation also queries the temporary tables. You should therefore consider connecting the Fuzzy Grouping transformation to a non-production instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], especially if the production server has limited disk space available.  
  
 The performance of this transformation may improve if the tables and indexes it uses are located on the local computer.  
  
## Configuration of the Fuzzy Grouping Transformation  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Fuzzy Grouping Transformation Editor** dialog box, click one of the following topics:  
  
-   [Fuzzy Grouping Transformation Editor &#40;Connection Manager Tab&#41;](../../fuzzy-grouping-transformation-editor-connection-manager-tab.md)  
  
-   [Fuzzy Grouping Transformation Editor &#40;Columns Tab&#41;](../../fuzzy-grouping-transformation-editor-columns-tab.md)  
  
-   [Fuzzy Grouping Transformation Editor &#40;Advanced Tab&#41;](../../fuzzy-grouping-transformation-editor-advanced-tab.md)  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../../common-properties.md)  
  
-   [Transformation Custom Properties](transformation-custom-properties.md)  
  
## Related Tasks  
 For details about how to set properties of this task, click one of the following topics:  
  
-   [Identify Similar Data Rows by Using the Fuzzy Grouping Transformation](fuzzy-grouping-transformation.md)  
  
-   [Set the Properties of a Data Flow Component](../set-the-properties-of-a-data-flow-component.md)  
  
## See Also  
 [Fuzzy Lookup Transformation](lookup-transformation.md)   
 [Integration Services Transformations](integration-services-transformations.md)  
  
  
