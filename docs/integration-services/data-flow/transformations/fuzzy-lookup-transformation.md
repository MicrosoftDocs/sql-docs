---
description: "Fuzzy Lookup Transformation"
title: "Fuzzy Lookup Transformation | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.fuzzylookuptrans.f1"
  - "sql13.dts.designer.fuzzylookuptransformation.referencetable.f1"
  - "sql13.dts.designer.fuzzylookuptransformation.columns.f1"
  - "sql13.dts.designer.fuzzylookuptransformation.advanced.f1"
helpviewer_keywords: 
  - "cleaning data"
  - "comparing data"
  - "token delimiters [Integration Services]"
  - "temporary indexes [Integration Services]"
  - "temporary tables [Integration Services]"
  - "Fuzzy Lookup transformation"
  - "reference tables [Integration Services]"
  - "match similar data [Integration Services]"
  - "replacing missing values"
  - "correcting data [Integration Services]"
  - "cache [Integration Services]"
  - "standardizing data [Integration Services]"
  - "lookups [Integration Services]"
  - "confidence scores [Integration Services]"
  - "fuzzy matches"
  - "missing values replaced [Integration Services]"
  - "similarity thresholds [Integration Services]"
ms.assetid: 019db426-3de2-4ca9-8667-79fd9a47a068
author: chugugrace
ms.author: chugu
---
# Fuzzy Lookup Transformation

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  The Fuzzy Lookup transformation performs data cleaning tasks such as standardizing data, correcting data, and providing missing values.  
  
> [!NOTE]  
>  For more detailed information about the Fuzzy Lookup transformation, including performance and memory limitations, see the white paper, [Fuzzy Lookup and Fuzzy Grouping in SQL Server Integration Services 2005](/previous-versions/sql/sql-server-2005/administrator/ms345128(v=sql.90)).  
  
 The Fuzzy Lookup transformation differs from the Lookup transformation in its use of fuzzy matching. The Lookup transformation uses an equi-join to locate matching records in the reference table. It returns records with at least one matching record, and returns records with no matching records. In contrast, the Fuzzy Lookup transformation uses fuzzy matching to return one or more close matches in the reference table.  
  
 A Fuzzy Lookup transformation frequently follows a Lookup transformation in a package data flow. First, the Lookup transformation tries to find an exact match. If it fails, the Fuzzy Lookup transformation provides close matches from the reference table.  
  
 The transformation needs access to a reference data source that contains the values that are used to clean and extend the input data. The reference data source must be a table in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database. The match between the value in an input column and the value in the reference table can be an exact match or a fuzzy match. However, the transformation requires at least one column match to be configured for fuzzy matching. If you want to use only exact matching, use the Lookup transformation instead.  
  
 This transformation has one input and one output.  
  
 Only input columns with the **DT_WSTR** and **DT_STR** data types can be used in fuzzy matching. Exact matching can use any DTS data type except **DT_TEXT**, **DT_NTEXT**, and **DT_IMAGE**. For more information, see [Integration Services Data Types](../../../integration-services/data-flow/integration-services-data-types.md). Columns that participate in the join between the input and the reference table must have compatible data types. For example, it is valid to join a column with the DTS **DT_WSTR** data type to a column with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **nvarchar** data type, but invalid to join a column with the **DT_WSTR** data type to a column with the **int** data type.  
  
 You can customize this transformation by specifying the maximum amount of memory, the row comparison algorithm, and the caching of indexes and reference tables that the transformation uses.  
  
 The amount of memory that the Fuzzy Lookup transformation uses can be configured by setting the MaxMemoryUsage custom property. You can specify the number of megabytes (MB), or use the value 0, which lets the transformation use a dynamic amount of memory based on its needs and the physical memory available. The MaxMemoryUsage custom property can be updated by a property expression when the package is loaded. For more information, see [Integration Services &#40;SSIS&#41; Expressions](../../../integration-services/expressions/integration-services-ssis-expressions.md), [Use Property Expressions in Packages](../../../integration-services/expressions/use-property-expressions-in-packages.md), and [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
## Controlling Fuzzy Matching Behavior  
 The Fuzzy Lookup transformation includes three features for customizing the lookup it performs: maximum number of matches to return per input row, token delimiters, and similarity thresholds.  
  
 The transformation returns zero or more matches up to the number of matches specified. Specifying a maximum number of matches does not guarantee that the transformation returns the maximum number of matches; it only guarantees that the transformation returns at most that number of matches. If you set the maximum number of matches to a value greater than 1, the output of the transformation may include more than one row per lookup and some of the rows may be duplicates.  
  
 The transformation provides a default set of delimiters used to tokenize the data, but you can add token delimiters to suit the needs of your data. The Delimiters property contains the default delimiters. Tokenization is important because it defines the units within the data that are compared to each other.  
  
 The similarity thresholds can be set at the component and join levels. The join-level similarity threshold is only available when the transformation performs a fuzzy match between columns in the input and the reference table. The similarity range is 0 to 1. The closer to 1 the threshold is, the more similar the rows and columns must be to qualify as duplicates. You specify the similarity threshold by setting the MinSimilarity property at the component and join levels. To satisfy the similarity that is specified at the component level, all rows must have a similarity across all matches that is greater than or equal to the similarity threshold that is specified at the component level. That is, you cannot specify a very close match at the component level unless the matches at the row or join level are equally close.  
  
 Each match includes a similarity score and a confidence score. The similarity score is a mathematical measure of the textural similarity between the input record and the record that Fuzzy Lookup transformation returns from the reference table. The confidence score is a measure of how likely it is that a particular value is the best match among the matches found in the reference table. The confidence score assigned to a record depends on the other matching records that are returned. For example, matching *St.* and *Saint* returns a low similarity score regardless of other matches. If *Saint* is the only match returned, the confidence score is high. If both *Saint* and *St.* appear in the reference table, the confidence in *St.* is high and the confidence in *Saint* is low. However, high similarity may not mean high confidence. For example, if you are looking up the value *Chapter 4*, the returned results *Chapter 1*, *Chapter 2*, and *Chapter 3* have a high similarity score but a low confidence score because it is unclear which of the results is the best match.  
  
 The similarity score is represented by a decimal value between 0 and 1, where a similarity score of 1 means an exact match between the value in the input column and the value in the reference table. The confidence score, also a decimal value between 0 and 1, indicates the confidence in the match. If no usable match is found, similarity and confidence scores of 0 are assigned to the row, and the output columns copied from the reference table will contain null values.  
  
 Sometimes, Fuzzy Lookup may not locate appropriate matches in the reference table. This can occur if the input value that is used in a lookup is a single, short word. For example, *helo* is not matched with the value *hello* in a reference table when no other tokens are present in that column or any other column in the row.  
  
 The transformation output columns include the input columns that are marked as pass-through columns, the selected columns in the lookup table, and the following additional columns:  
  
-   **_Similarity**, a column that describes the similarity between values in the input and reference columns.  
  
-   **_Confidence**, a column that describes the quality of the match.  
  
 The transformation uses the connection to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database to create the temporary tables that the fuzzy matching algorithm uses.  
  
## Running the Fuzzy Lookup Transformation  
 When the package first runs the transformation, the transformation copies the reference table, adds a key with an integer data type to the new table, and builds an index on the key column. Next, the transformation builds an index, called a match index, on the copy of the reference table. The match index stores the results of tokenizing the values in the transformation input columns, and the transformation then uses the tokens in the lookup operation. The match index is a table in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
 When the package runs again, the transformation can either use an existing match index or create a new index. If the reference table is static, the package can avoid the potentially expensive process of rebuilding the index for repeat sessions of data cleaning. If you choose to use an existing index, the index is created the first time that the package runs. If multiple Fuzzy Lookup transformations use the same reference table, they can all use the same index. To reuse the index, the lookup operations must be identical; the lookup must use the same columns. You can name the index and select the connection to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database that saves the index.  
  
 If the transformation saves the match index, the match index can be maintained automatically. This means that every time a record in the reference table is updated, the match index is also updated. Maintaining the match index can save processing time, because the index does not have to be rebuilt when the package runs. You can specify how the transformation manages the match index.  
  
 The following table describes the match index options.  
  
|Option|Description|  
|------------|-----------------|  
|**GenerateAndMaintainNewIndex**|Create a new index, save it, and maintain it. The transformation installs triggers on the reference table to keep the reference table and index table synchronized.|  
|**GenerateAndPersistNewIndex**|Create a new index and save it, but do not maintain it.|  
|**GenerateNewIndex**|Create a new index, but do not save it.|  
|**ReuseExistingIndex**|Reuse an existing index.|  
  
### Maintenance of the Match Index Table  
 The **GenerateAndMaintainNewIndex** option installs triggers on the reference table to keep the match index table and the reference table synchronized. If you have to remove the installed trigger, you must run the **sp_FuzzyLookupTableMaintenanceUnInstall** stored procedure, and provide the name specified in the MatchIndexName property as the input parameter value.  
  
 You should not delete the maintained match index table before running the **sp_FuzzyLookupTableMaintenanceUnInstall** stored procedure. If the match index table is deleted, the triggers on the reference table will not execute correctly. All subsequent updates to the reference table will fail until you manually drop the triggers on the reference table.  
  
 The SQL TRUNCATE TABLE command does not invoke DELETE triggers. If the TRUNCATE TABLE command is used on the reference table, the reference table and the match index will no longer be synchronized and the Fuzzy Lookup transformation fails. While the triggers that maintain the match index table are installed on the reference table, you should use the SQL DELETE command instead of the TRUNCATE TABLE command.  
  
> [!NOTE]  
>  When you select **Maintain stored index** on the **Reference Table** tab of the **Fuzzy Lookup Transformation Editor**, the transformation uses managed stored procedures to maintain the index. These managed stored procedures use the common language runtime (CLR) integration feature in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. By default, CLR integration in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is not enabled. To use the **Maintain stored index** functionality, you must enable CLR integration. For more information, see [Enabling CLR Integration](../../../relational-databases/clr-integration/clr-integration-enabling.md).  
>   
>  Because the **Maintain stored index** option requires CLR integration, this feature works only when you select a reference table on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] where CLR integration is enabled.  
  
## Row Comparison  
 When you configure the Fuzzy Lookup transformation, you can specify the comparison algorithm that the transformation uses to locate matching records in the reference table. If you set the Exhaustive property to **True**, the transformation compares every row in the input to every row in the reference table. This comparison algorithm may produce more accurate results, but it is likely to make the transformation perform more slowly unless the number of rows is the reference table is small. If the Exhaustive property is set to **True**, the entire reference table is loaded into memory. To avoid performance issues, it is advisable to set the Exhaustive property to **True** during package development only.  
  
 If the Exhaustive property is set to **False**, the Fuzzy Lookup transformation returns only matches that have at least one indexed token or substring (the substring is called a *q-gram*) in common with the input record. To maximize the efficiency of lookups, only a subset of the tokens in each row in the table is indexed in the inverted index structure that the Fuzzy Lookup transformation uses to locate matches. When the input dataset is small, you can set Exhaustive to **True** to avoid missing matches for which no common tokens exist in the index table.  
  
## Caching of Indexes and Reference Tables  
 When you configure the Fuzzy Lookup transformation, you can specify whether the transformation partially caches the index and reference table in memory before the transformation does its work. If you set the WarmCaches property to **True**, the index and reference table are loaded into memory. When the input has many rows, setting the WarmCaches property to **True** can improve the performance of the transformation. When the number of input rows is small, setting the WarmCaches property to **False** can make the reuse of a large index faster.  
  
## Temporary Tables and Indexes  
 At run time, the Fuzzy Lookup transformation creates temporary objects, such as tables and indexes, in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database that the transformation connects to. The size of these temporary tables and indexes is proportionate to the number of rows and tokens in the reference table and the number of tokens that the Fuzzy Lookup transformation creates; therefore, they could potentially consume a significant amount of disk space. The transformation also queries these temporary tables. You should therefore consider connecting the Fuzzy Lookup transformation to a non-production instance of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, especially if the production server has limited disk space available.  
  
 The performance of this transformation may improve if the tables and indexes it uses are located on the local computer. If the reference table that the Fuzzy Lookup transformation uses is on the production server, you should consider copying the table to a non-production server and configuring the Fuzzy Lookup transformation to access the copy. By doing this, you can prevent the lookup queries from consuming resources on the production server. In addition, if the Fuzzy Lookup transformation maintains the match index-that is, if MatchIndexOptionsis set to **GenerateAndMaintainNewIndex**-the transformation may lock the reference table for the duration of the data cleaning operation and prevent other users and applications from accessing the table.  
  
## Configuring the Fuzzy Lookup Transformation  
 You can set properties through [!INCLUDE[ssIS](../../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in the **Advanced Editor** dialog box or programmatically, click one of the following topics:  
  
-   [Common Properties](../set-the-properties-of-a-data-flow-component.md)  
  
-   [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md)  
  
## Related Tasks  
 For details about how to set properties of a data flow component, see [Set the Properties of a Data Flow Component](../../../integration-services/data-flow/set-the-properties-of-a-data-flow-component.md).  
  
## Fuzzy Lookup Transformation Editor (Reference Table Tab)
  Use the **Reference Table** tab of the **Fuzzy Lookup Transformation Editor** dialog box to specify the source table and the index to use for the lookup. The reference data source must be a table in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database.  
  
> [!NOTE]  
>  The Fuzzy Lookup transformation creates a working copy of the reference table. The indexes described below are created on this working table by using a special table, not an ordinary [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] index. The transformation does not modify the existing source tables unless you select **Maintain stored index**. In this case, it creates a trigger on the reference table that updates the working table and the lookup index table based on changes to the reference table.  
  
> [!NOTE]  
>  The **Exhaustive** and the **MaxMemoryUsage** properties of the Fuzzy Lookup transformation are not available in the **Fuzzy Lookup Transformation Editor**, but can be set by using the **Advanced Editor**. In addition, a value greater than 100 for **MaxOutputMatchesPerInput** can be specified only in the **Advanced Editor**. For more information on these properties, see the Fuzzy Lookup Transformation section of [Transformation Custom Properties](../../../integration-services/data-flow/transformations/transformation-custom-properties.md).  
  
### Options  
 **OLE DB connection manager**  
 Select an existing OLE DB connection manager from the list, or create a new connection by clicking **New**.  
  
 **New**  
 Create a new connection by using the **Configure OLE DB Connection Manager** dialog box.  
  
 **Generate new index**  
 Specify that the transformation should create a new index to use for the lookup.  
  
 **Reference table name**  
 Select the existing table to use as the reference (lookup) table.  
  
 **Store new index**  
 Select this option if you want to save the new lookup index.  
  
 **New index name**  
 If you have chosen to save the new lookup index, type a descriptive name for the index.  
  
 **Maintain stored index**  
 If you have chosen to save the new lookup index, specify whether you also want [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to maintain the index.  
  
> [!NOTE]  
>  When you select **Maintain stored index** on the **Reference Table** tab of the **Fuzzy Lookup Transformation Editor**, the transformation uses managed stored procedures to maintain the index. These managed stored procedures use the common language runtime (CLR) integration feature in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. By default, CLR integration in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is not enabled. To use the **Maintain stored index** functionality, you must enable CLR integration. For more information, see [Enabling CLR Integration](../../../relational-databases/clr-integration/clr-integration-enabling.md).  
>   
>  Because the **Maintain stored index** option requires CLR integration, this feature works only when you select a reference table on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] where CLR integration is enabled.  
  
 **Use existing index**  
 Specify that the transformation should use an existing index for the lookup.  
  
 **Name of an existing index**  
 Select a previously created lookup index from the list.  
  
## Fuzzy Lookup Transformation Editor (Columns Tab)
  Use the **Columns** tab of the **Fuzzy Lookup Transformation Editor** dialog box to set properties for input and output columns.  
  
### Options  
 **Available Input Columns**  
 Drag input columns to connect them to available lookup columns. These columns must have matching, supported data types. Select a mapping line and right-click to edit the mappings in the [Create Relationships](../../../integration-services/data-flow/transformations/create-relationships.md) dialog box.  
  
 **Name**  
 View the names of the available input columns.  
  
 **Pass Through**  
 Specify whether to include the input columns in the output of the transformation.  
  
 **Available Lookup Columns**  
 Use the check boxes to select columns on which to perform fuzzy lookup operations.  
  
 **Lookup Column**  
 Select lookup columns from the list of available reference table columns. Your selections are reflected in the check box selections in the **Available Lookup Columns** table. Selecting a column in the **Available Lookup Columns** table creates an output column that contains the reference table column value for each matching row returned.  
  
 **Output Alias**  
 Type an alias for the output for each lookup column. The default is the name of the lookup column with a numeric index value appended; however, you can choose any unique, descriptive name.  
  
## Fuzzy Lookup Transformation Editor (Advanced Tab)
  Use the **Advanced** tab of the **Fuzzy Lookup Transformation Editor** dialog box to set parameters for the fuzzy lookup.  
  
### Options  
 **Maximum number of matches to output per lookup**  
 Specify the maximum number of matches the transformation can return for each input row. The default is **1**.  
  
 **Similarity threshold**  
 Set the similarity threshold at the component level by using the slider. The closer the value is to 1, the closer the resemblance of the lookup value to the source value must be to qualify as a match. Increasing the threshold can improve the speed of matching since fewer candidate records need to be considered.  
  
 **Token delimiters**  
 Specify the delimiters that the transformation uses to tokenize column values.  
  
## See Also  
 [Lookup Transformation](../../../integration-services/data-flow/transformations/lookup-transformation.md)   
 [Fuzzy Grouping Transformation](../../../integration-services/data-flow/transformations/fuzzy-grouping-transformation.md)   
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)  
  
