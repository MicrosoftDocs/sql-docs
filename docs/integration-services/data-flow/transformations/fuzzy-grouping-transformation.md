---
description: "Fuzzy Grouping Transformation"
title: "Fuzzy Grouping Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.fuzzygroupingtrans.f1"
  - "sql13.dts.designer.fuzzygroupingtransformation.connection.f1"
  - "sql13.dts.designer.fuzzygroupingtransformation.columns.f1"
  - "sql13.dts.designer.fuzzygroupingtransformation.advanced.f1"
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
author: chugugrace
ms.author: chugu
---
# Fuzzy Grouping Transformation

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  The Fuzzy Grouping transformation performs data cleaning tasks by identifying rows of data that are likely to be duplicates and selecting a canonical row of data to use in standardizing the data.  
  
> [!NOTE]  
>  For more detailed information about the Fuzzy Grouping transformation, including performance and memory limitations, see the white paper, [Fuzzy Lookup and Fuzzy Grouping in SQL Server Integration Services 2005](/previous-versions/sql/sql-server-2005/administrator/ms345128(v=sql.90)).  
  
 The Fuzzy Grouping transformation requires a connection to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to create the temporary [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] tables that the transformation algorithm requires to do its work. The connection must resolve to a user who has permission to create tables in the database.  
  
 To configure the transformation, you must select the input columns to use when identifying duplicates, and you must select the type of match-fuzzy or exact-for each column. An exact match guarantees that only rows that have identical values in that column will be grouped. Exact matching can be applied to columns of any [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] data type except DT_TEXT, DT_NTEXT, and DT_IMAGE. A fuzzy match groups rows that have approximately the same values. The method for approximate matching of data is based on a user-specified similarity score. Only columns with the DT_WSTR and DT_STR data types can be used in fuzzy matching. For more information, see [Integration Services Data Types](../../../integration-services/data-flow/integration-services-data-types.md).  
  
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
  
 The amount of memory that the Fuzzy Grouping transformation uses can be configured by setting the MaxMemoryUsage custom property. You can specify the number of megabytes (MB) or use the value 0 to allow the transformation to use a dynamic amount of memory based on its needs and the physical memory available. The MaxMemoryUsage custom property can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../../integration-services/expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
 This transformation has one input and one output. It does not support an error output.  
  
## Row Comparison  
 When you configure the Fuzzy Grouping transformation, you can specify the comparison algorithm that the transformation uses to compare rows in the transformation input. If you set the Exhaustive property to **true**, the transformation compares every row in the input to every other row in the input. This comparison algorithm may produce more accurate results, but it is likely to make the transformation perform more slowly unless the number of rows in the input is small. To avoid performance issues, it is advisable to set the Exhaustive property to **true** only during package development.  
  
## Temporary Tables and Indexes  
 At run time, the Fuzzy Grouping transformation creates temporary objects such as tables and indexes, potentially of significant size, in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database that the transformation connects to. The size of the tables and indexes are proportional to the number of rows in the transformation input and the number of tokens created by the Fuzzy Grouping transformation.  
  
 The transformation also queries the temporary tables. You should therefore consider connecting the Fuzzy Grouping transformation to a non-production instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], especially if the production server has limited disk space available.  
  
 The performance of this transformation may improve if the tables and indexes it uses are located on the local computer.  
  
## Configuration of the Fuzzy Grouping Transformation  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../set-the-properties-of-a-data-flow-component.md)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
## Related Tasks  
 For details about how to set properties of this task, click one of the following topics:  
  
-   [Identify Similar Data Rows by Using the Fuzzy Grouping Transformation](../../../integration-services/data-flow/transformations/identify-similar-data-rows-by-using-the-fuzzy-grouping-transformation.md)  
  
-   [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md)  
  
## Fuzzy Grouping Transformation Editor (Connection Manager Tab)
  Use the **Connection Manager** tab of the **Fuzzy Grouping Transformation Editor** dialog box to select an existing connection or create a new one.  
  
> [!NOTE]  
>  The server specified by the connection must be running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The Fuzzy Grouping transformation creates temporary data objects in tempdb that may be as large as the full input to the transformation. While the transformation executes, it issues server queries against these temporary objects. This can affect overall server performance.  
  
### Options  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager by using the list box, or create a new connection by using the **New** button.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
## Fuzzy Grouping Transformation Editor (Columns Tab)
  Use the **Columns** tab of the **Fuzzy Grouping Transformation Editor** dialog box to specify the columns used to group rows with duplicate values.  
  
### Options  
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
 For information about the string comparison options, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).  
  
## Fuzzy Grouping Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Fuzzy Grouping Transformation Editor** dialog box to specify input and output columns, set similarity thresholds, and define delimiters.  
  
> [!NOTE]  
>  The **Exhaustive** and the **MaxMemoryUsage** properties of the Fuzzy Grouping transformation are not available in the **Fuzzy Grouping Transformation Editor**, but can be set by using the **Advanced Editor**. For more information on these properties, see the Fuzzy Grouping Transformation section of [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
### Options  
 **Input key column name**  
 Specify the name of an output column that contains the unique identifier for each input row. The **_key_in** column has a value that uniquely identifies each row.  
  
 **Output key column name**  
 Specify the name of an output column that contains the unique identifier for the canonical row of a group of duplicate rows. The **_key_out** column corresponds to the **_key_in** value of the canonical data row.  
  
 **Similarity score column name**  
 Specify a name for the column that contains the similarity score. The similarity score is a value between 0 and 1 that indicates the similarity of the input row to the canonical row. The closer the score is to 1, the more closely the row matches the canonical row.  
  
 **Similarity threshold**  
 Set the similarity threshold by using the slider. The closer the threshold is to 1, the more the rows must resemble each other to qualify as duplicates. Increasing the threshold can improve the speed of matching because fewer candidate records have to be considered.  
  
 **Token delimiters**  
 The transformation provides a default set of delimiters for tokenizing data, but you can add or remove delimiters as needed by editing the list.  
  
## See Also  
 [Fuzzy Lookup Transformation](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
