---
description: "Transformation Custom Properties"
title: "Transformation Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
dev_langs: 
  - "VB"
  - "CSharp"
helpviewer_keywords: 
  - "Aggregate transformation [Integration Services]"
  - "Slowly Changing Dimension transformation"
  - "Import Column transformation [Integration Services]"
  - "Sort transformation"
  - "Unpivot transformation"
  - "Merge Join transformation"
  - "Data Mining Query transformation"
  - "Fuzzy Grouping transformation"
  - "Data Conversion transformation"
  - "Fuzzy Lookup transformation"
  - "Term Extraction transformation"
  - "Row Count transformation custom properties [Integration Services]"
  - "transformations [Integration Services], properties"
  - "Pivot transformation"
  - "Lookup transformation"
  - "Percentage Sampling transformation"
  - "Export Column transformation [Integration Services]"
  - "Row Sampling transformation"
  - "Conditional Split transformation custom properties [Integration Services]"
  - "custom properties [Integration Services]"
  - "Audit transformation"
  - "Term Lookup transformation"
  - "Script Component transformation custom properties [Integration Services]"
  - "Derived Column transformation"
  - "OLE DB Command transformation"
  - "Copy Column transformation custom properties [Integration Services]"
  - "Character Map transformation custom properties [Integration Services]"
ms.assetid: 56f5df6a-56f6-43df-bca9-08476a3bd931
author: chugugrace
ms.author: chugu
---
# Transformation Custom Properties

[!INCLUDE[sqlserver-ssis](../../../includes/applies-to-version/sqlserver-ssis.md)]


  In addition to the properties that are common to most data flow objects in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] object model, many data flow objects have custom properties that are specific to the object. These custom properties are available only at run time, and are not documented in the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] Managed Programming Reference Documentation.  
  
 This topic lists and describes the custom properties of the various data flow transformations. For information about the properties common to most data flow objects, see [Common Properties](../set-the-properties-of-a-data-flow-component.md).  
  
 Some properties of transformations can be set by using property expressions. For more information, see [Data Flow Properties that Can Be Set by Using Expressions](/previous-versions/sql/sql-server-2016/ms136104(v=sql.130)).  
  
## Transformations with Custom Properties  

:::row:::
    :::column:::
        [Aggregate](#aggregate)  
        [Audit](#audit)  
        [Cache Transform](#cachetransform)  
        [Character Map](#charmap)  
        [Conditional Split](#condsplit)  
        [Copy Column](#copymap)  
        [Data Conversion](#dataconv)  
        [Data Mining Query](#dmquery)  
        [Derived Column](#derived)  
    :::column-end:::
    :::column:::
        [Export Column](#extract)  
        [Fuzzy Grouping](#fgroup)  
        [Fuzzy Lookup](#flookup)  
        [Import Column](#insert)  
        [Lookup](#lookup)  
        [Merge Join](#mjoin)  
        [OLE DB Command](#oledbcmd)  
        [Percentage Sampling](#percent)  
        [Pivot](#pivot)  
    :::column-end:::
    :::column:::
        [Row Count](#rowcount)  
        [Row Sampling](#rowsamp)  
        [Script Component](#script)  
        [Slowly Changing Dimension](#scd)  
        [Sort](#sort)  
        [Term Extraction](#textract)  
        [Term Lookup](#tlookup)  
        [Unpivot](#unpivot)  
    :::column-end:::
:::row-end:::

### Transformations Without Custom Properties  
 The following transformations have no custom properties at the component, input, or output levels: [Merge Transformation](../../../integration-services/data-flow/transformations/merge-transformation.md), [Multicast Transformation](../../../integration-services/data-flow/transformations/multicast-transformation.md), and [Union All Transformation](../../../integration-services/data-flow/transformations/union-all-transformation.md). They use only the properties common to all data flow components.  
  
##  <a name="aggregate"></a> Aggregate Transformation Custom Properties  
 The Aggregate transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Aggregate transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|AutoExtendFactor|Integer|A value between 1 and 100 that specifies the percentage by which memory can be extended during the aggregation. The default value of this property is **25**.|  
|CountDistinctKeys|Integer|A value that specifies the exact number of distinct counts that the aggregation can write. If a CountDistinctScale value is specified, the value in CountDistinctKeys takes precedence.|  
|CountDistinctScale|Integer (enumeration)|A value that describes the approximate number of distinct values in a column that the aggregation can count. This property can have one of the following values:<br /><br /> **Low** (1)-indicates up to 500,000 key values<br /><br /> **Medium** (2)-indicates up to 5 million key values<br /><br /> **High** (3)-indicates more than 25 million key values.<br /><br /> **Unspecified** (0)-indicates no CountDistinctScale value is used. Using the **Unspecified** (0) option may affect performance in large data sets.|  
|Keys|Integer|A value that specifies the exact number of Group By keys that the aggregation writes. If a KeyScalevalue is specified, the value in Keys takes precedence.|  
|KeyScale|Integer (enumeration)|A value that describes approximately how many Group By key values the aggregation can write. This property can have one of the following values:<br /><br /> **Low** (1)-indicates up to 500,000 key values.<br /><br /> **Medium** (2)-indicates up to 5 million key values.<br /><br /> **High** (3)-indicates more than 25 million key values.<br /><br /> **Unspecified** (0)-indicates that no KeyScale value is used.|  
  
 The following table describes the custom properties of the output of the Aggregate transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|Keys|Integer|A value that specifies the exact number of Group By keys that the aggregation can write. If a KeyScale value is specified, the value in Keys takes precedence.|  
|KeyScale|Integer (enumeration)|A value that describes approximately how many Group By key values the aggregation can write. This property can have one of the following values:<br /><br /> **Low** (1)-indicates up to 500,000 key values,<br /><br /> **Medium** (2)-indicates up to 5 million key values,<br /><br /> **High** (3)-indicates more than 25 million key values.<br /><br /> **Unspecified** (0)-indicates no KeyScale value is used.|  
  
 The following table describes the custom properties of the output columns of the Aggregate transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|AggregationColumnId|Integer|The **LineageID** of a column that participates in GROUP BY or aggregate functions.|  
|AggregationComparisonFlags|Integer|A value that specifies how the Aggregate transformation compares string data in a column. For more information, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).|  
|AggregationType|Integer (enumeration)|A value that specifies the aggregation operation to be performed on the column. This property can have one of the following values:<br /><br /> **Group by** (0)<br /><br /> **Count** (1)<br /><br /> **Count all** (2)<br /><br /> **Countdistinct** (3)<br /><br /> **Sum** (4)<br /><br /> **Average** (5)<br /><br /> **Maximum** (7)<br /><br /> **Minimum** (6)|  
|CountDistinctKeys|Integer|When the aggregation type is **Count distinct**, a value that specifies the exact number of keys that the aggregation can write. If a CountDistinctScale value is specified, the value in CountDistinctKeys takes precedence.|  
|CountDistinctScale|Integer (enumeration)|When the aggregation type is **Count distinct**, a value that describes approximately how many key values the aggregation can write. This property can have one of the following values:<br /><br /> **Low** (1)-indicates up to 500,000 key values,<br /><br /> **Medium** (2)-indicates up to 5 million key values,<br /><br /> **High** (3)-indicates more than 25 million key values.<br /><br /> **Unspecified** (0)-indicates no CountDistinctScale value is used.|  
|IsBig|Boolean|A value that indicates whether the column contains a value larger than 4 billion or a value with more precision than a double-precision floating-point value. The value can be 0 or 1. 0 indicates that IsBig is **False** and the column does not contain a large value or precise value. The default value of this property is 1.|  
  
 The input and the input columns of the Aggregate transformation have no custom properties.  
  
 For more information, see [Aggregate Transformation](../../../integration-services/data-flow/transformations/aggregate-transformation.md).  
  
##  <a name="audit"></a> Audit Transformation Custom Properties  
 The Audit transformation has only the properties common to all data flow components at the component level.  
  
 The following table describes the custom properties of the output columns of the Audit transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|LineageItemSelected|Integer (enumeration)|The audit item selected for output. This property can have one of the following values:<br /><br /> **Execution instance GUID** (0)<br /><br /> **Execution start time** (4)<br /><br /> **Machine name** (5)<br /><br /> **Package ID** (1)<br /><br /> **Package name** (2)<br /><br /> **Task ID** (8)<br /><br /> **Task name** (7)<br /><br /> **User name** (6)<br /><br /> **Version ID** (3)|  
  
 The input, the input columns, and the output of the Audit transformation have no custom properties.  
  
 For more information, see [Audit Transformation](../../../integration-services/data-flow/transformations/audit-transformation.md).  
  
##  <a name="cachetransform"></a> Cache Transform Transformation Custom Properties  
 The Cache Transform transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the properties of the Cache Transform transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|Connectionmanager|String|Specifies the name of the connection manager.|  
|ValidateExternalMetadata|Boolean|Indicates whether the Cache Transform is validated by using external data sources at design time. If the property is set to **False**, validation against external data sources occurs at run time.<br /><br /> The default value it **True**.|  
|AvailableInputColumns|String|A list of the available input columns.|  
|InputColumns|String|A list of the selected input columns.|  
|CacheColumnName|String|Specifies the name of the column that is mapped to a selected input column.<br /><br /> The name of the column in the CacheColumnName property must match the name of the corresponding column listed on the **Columns** page of the **Cache Connection Manager Editor**.<br /><br /> For more information, see [Cache Connection Manager Editor](../../connection-manager/cache-connection-manager.md)|  
  
##  <a name="charmap"></a> Character Map Transformation Custom Properties  
 The Character Map transformation has only the properties common to all data flow components at the component level.  
  
 The following table describes the custom properties of the output columns of the Character Map transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|InputColumnLineageId|Integer|A value that specifies the **LineageID** of the input column that is the source of the output column.|  
|MapFlags|Integer (enumeration)|A value that specifies the string operations that the Character Map transformation performs on the column. This property can have one of the following values:<br /><br /> **Byte reversal** (2)<br /><br /> **Full width** (6)<br /><br /> **Half width** (5)<br /><br /> **Hiragana** (3)<br /><br /> **Katakana** (4)<br /><br /> **Linguistic casing** (7)<br /><br /> **Lowercase** (0)<br /><br /> **Simplified Chinese** (8)<br /><br /> **Traditional Chinese**(9)<br /><br /> **Uppercase** (1)|  
  
 The input, the input columns, and the output of the Character Map transformation have no custom properties.  
  
 For more information, see [Character Map Transformation](../../../integration-services/data-flow/transformations/character-map-transformation.md).  
  
##  <a name="condsplit"></a> Conditional Split Transformation Custom Properties  
 The Conditional Split transformation has only the properties common to all data flow components at the component level.  
  
 The following table describes the custom properties of the output of the Conditional Split transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|EvaluationOrder|Integer|A value that specifies the position of a condition, associated with an output, in the list of conditions that the Conditional Split transformation evaluates. The conditions are evaluated in order from the lowest to the highest value.|  
|Expression|String|An expression that represents the condition that the Conditional Split transformation evaluates. Columns are represented by lineage identifiers.|  
|FriendlyExpression|String|An expression that represents the condition that the Conditional Split transformation evaluates. Columns are represented by their names.<br /><br /> The value of this property can be specified by using a property expression.|  
|IsDefaultOut|Boolean|A value that indicates whether the output is the default output.|  
  
 The input, the input columns, and the output columns of the Conditional Split transformation have no custom properties.  
  
 For more information, see [Conditional Split Transformation](../../../integration-services/data-flow/transformations/conditional-split-transformation.md).  
  
##  <a name="copymap"></a> Copy Column Transformation Custom Properties  
 The Copy Column transformation has only the properties common to all data flow components at the component level.  
  
 The following table describes the custom properties of the output columns of the Copy Column transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|copyColumnId|Integer|The **LineageID** of the input column from which the output column is copied.|  
  
 The input, the input columns, and the output of the Copy Column transformation have no custom properties.  
  
 For more information, see [Copy Column Transformation](../../../integration-services/data-flow/transformations/copy-column-transformation.md).  
  
##  <a name="dataconv"></a> Data Conversion Transformation Custom Properties  
 The Data Conversion transformation has only the properties common to all data flow components at the component level.  
  
 The following table describes the custom properties of the output columns of the Data Conversion transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|FastParse|Boolean|A value that indicates whether the column uses the quicker, but locale-insensitive, fast parsing routines that [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] provides, or the locale-sensitive standard parsing routines. The default value of this property is **False**. For more information, see [Fast Parse](../parsing-data.md) and [Standard Parse](../parsing-data.md). .<br /><br /> Note: This property is not available in the **Data Conversion Transformation Editor**, but can be set by using the **Advanced Editor**.|  
|SourceInputColumnLineageId|Integer|The **LineageID** of the input column that is the source of the output column.|  
  
 The input, the input columns, and the output of the Data Conversion transformation have no custom properties.  
  
 For more information, see [Data Conversion Transformation](../../../integration-services/data-flow/transformations/data-conversion-transformation.md).  
  
##  <a name="dmquery"></a> Data Mining Query Transformation Custom Properties  
 The Data Mining Query transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Data Mining Query transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|ASConnectionId|String|The unique identifier of the connection object.|  
|ASConnectionString|String|The connection string to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] project or an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database.|  
|CatalogName|String|The name of an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] database.|  
|ModelName|String|The name of the data mining model.|  
|ModelStructureName|String|The name of the mining structure.|  
|ObjectRef|String|An XML tag that identifies the data mining structure that the transformation uses.|  
|QueryText|String|The prediction query statement that the transformation uses.|  
  
 The input, the input columns, the output, and the output columns of the Data Mining Query transformation have no custom properties.  
  
 For more information, see [Data Mining Query Transformation](../../../integration-services/data-flow/transformations/data-mining-query-transformation.md).  
  
##  <a name="derived"></a> Derived Column Transformation Custom Properties  
 The Derived Column transformation has only the properties common to all data flow components at the component level.  
  
 The following table describes the custom properties of the input columns and output columns of the Derived Column transformation. When you choose to add the derived column as a new column, these custom properties apply to the new output column; when you choose to replace the contents of an existing input column with the derived results, these custom properties apply to the existing input column. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|Expression|String|An expression that represents the condition that the Conditional Split transformation evaluates. Columns are represented by the **LineageID** property for the column.|  
|FriendlyExpression|String|An expression that represents the condition that the Conditional Split transformation evaluates. Columns are represented by their names.<br /><br /> The value of this property can be specified by using a property expression.|  
  
 The input and output of the Derived Column transformation have no custom properties.  
  
 For more information, see [Derived Column Transformation](../../../integration-services/data-flow/transformations/derived-column-transformation.md).  
  
##  <a name="extract"></a> Export Column Transformation Custom Properties  
 The Export Column transformation has only the properties common to all data flow components at the component level.  
  
 The following table describes the custom properties of the input columns of the Export Column transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|AllowAppend|Boolean|A value that specifies whether the transformation appends data to an existing file. The default value of this property is **False**.|  
|ForceTruncate|Boolean|A value that specifies whether the transformation truncates an existing files before writing data. The default value of this property is **False**.|  
|FileDataColumnID|Integer|A value that identifies the column that contains the data that the transformation inserts into a file. On the Extract Column, this property has a value of **0**; on the File Path Column, this property contains the **LineageID** of the Extract Column.|  
|WriteBOM|Boolean|A value that specifies whether a byte-order mark (BOM) is written to the file.|  
  
 The input, the output, and the output columns of the Export Column transformation have no custom properties.  
  
 For more information, see [Export Column Transformation](../../../integration-services/data-flow/transformations/export-column-transformation.md).  
  
##  <a name="insert"></a> Import Column Transformation Custom Properties  
 The Import Column transformation has only the properties common to all data flow components at the component level.  
  
 The following table describes the custom properties of the input columns of the Import Column transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|ExpectBOM|Boolean|A value that specifies whether the Import Column transformation expects a byte-order mark (BOM). A BOM is only expected if the data has the DT_NTEXT data type.|  
|FileDataColumnID|Integer|A value that identifies the column that contains the data that the transformation inserts into the data flow. On the column of data to be inserted, this property has a value of 0; on the column that contains the source file paths, this property contains the **LineageID** of the column of data to be inserted.|  
  
 The input, the output, and the output columns of the Import Column transformation have no custom properties.  
  
 For more information, see [Import Column Transformation](../../../integration-services/data-flow/transformations/import-column-transformation.md).  
  
##  <a name="fgroup"></a> Fuzzy Grouping Transformation Custom Properties  
 The Fuzzy Grouping transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Fuzzy Grouping transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|Delimiters|String|The token delimiters that the transformation uses. The default delimiters include the following characters: space ( ), comma(,), period (.), semicolon (;), colon (:), hyphen (-), double straight quotation mark ("), single straight quotation mark ('), ampersand (&), slash mark (/), backslash (\\), at sign (@), exclamation point (!), question mark (?), opening parenthesis ((), closing parenthesis ()), less than (\<), greater than (>), opening bracket ([), closing bracket (]), opening brace ({), closing brace (}), pipe (&#124;), number sign (#), asterisk (*), caret (^), and percent (%).|  
|Exhaustive|Boolean|A value that specifies whether each input record is compared to every other input record. The value of **True** is intended mostly for debugging purposes. The default value of this property is **False**.<br /><br /> Note: This property is not available in the **Fuzzy Grouping Transformation Editor**, but can be set by using the **Advanced Editor**.|  
|MaxMemoryUsage|Integer|The maximum amount of memory for use by the transformation. The default value of this property is **0**, which enables dynamic memory usage.<br /><br /> The value of this property can be specified by using a property expression.<br /><br /> Note: This property is not available in the **Fuzzy Grouping Transformation Editor**, but can be set by using the **Advanced Editor**.|  
|MinSimilarity|Double|The similarity threshold that the transformation uses to identify duplicates, expressed as a value between 0 and 1.  The default value of this property is 0.8.|  
  
 The following table describes the custom properties of the input columns of the Fuzzy Grouping transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|ExactFuzzy|Integer (enumeration)|A value that specifies whether the transformation performs a fuzzy match or an exact match. The valid values are **Exact** and **Fuzzy**. The default value for this property is **Fuzzy**.|  
|FuzzyComparisonFlags|Integer (enumeration)|A value that specifies how the transformation compares the string data in a column. This property can have one of the following values:<br /><br /> **FullySensitive**<br /><br /> **IgnoreCase**<br /><br /> **IgnoreKanaType**<br /><br /> **IgnoreNonSpace**<br /><br /> **IgnoreSymbols**<br /><br /> **IgnoreWidth**<br /><br /> <br /><br /> For more information, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).|  
|LeadingTrailingNumeralsSignificant|Integer (enumeration)|A value that specifies the significance of numerals. This property can have one of the following values:<br /><br /> **NumeralsNotSpecial** (0)-use if numerals are not significant.<br /><br /> **LeadingNumeralsSignificant** (1)-use if leading numerals are significant.<br /><br /> **TrailingNumeralsSignificant** (2)-use if trailing numerals are significant.<br /><br /> **LeadingAndTrailingNumeralsSignificant** (3)-use if both leading and trailing numerals are significant.|  
|MinSimilarity|Double|The similarity threshold used for the join on the column, specified as a value between 0 and 1. Only rows greater than the threshold qualify as matches.|  
|ToBeCleaned|Boolean|A value that specifies whether the column is used to identify duplicates; that is, whether this is a column on which you are grouping. The default value of this property is **False**.|  
  
 The following table describes the custom properties of the output columns of the Fuzzy Grouping transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|ColumnType|Integer (enumeration)|A value that identifies the type of output column. This property can have one of the following values:<br /><br /> **Undefined** (0)<br /><br /> **KeyIn** (1)<br /><br /> **KeyOut** (2)<br /><br /> **Similarity** (3)<br /><br /> **ColumnSimilarity** (4)<br /><br /> **PassThru** (5)<br /><br /> **Canonica**l (6)|  
|InputID|Integer|The **LineageID** of the corresponding input column.|  
  
 The input and output of the Fuzzy Grouping transformation have no custom properties.  
  
 For more information, see [Fuzzy Grouping Transformation](../../../integration-services/data-flow/transformations/fuzzy-grouping-transformation.md).  
  
##  <a name="flookup"></a> Fuzzy Lookup Transformation Custom Properties  
 The Fuzzy Lookup transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Fuzzy Lookup transformation. All properties except for **ReferenceMetadataXML** are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|CopyReferenceTable|Boolean|Specifies whether a copy of the reference table should be made for fuzzy lookup index construction and subsequent lookups. The default value of this property is **True**.|  
|Delimiters|String|The delimiters that the transformation uses to tokenize column values. The default delimiters include the following characters: space ( ), comma (,), period (.) semicolon(;), colon (:) hyphen (-), double straight quotation mark ("), single straight quotation mark ('), ampersand (&), slash mark (/), backslash (\\), at sign (@), exclamation point (!), question mark (?), opening parenthesis ((), closing parenthesis ()), less than (\<), greater than (>), opening bracket ([), closing bracket (]), opening brace ({), closing brace (}), pipe (&#124;). number sign (#), asterisk (*), caret (^), and percent (%).|  
|DropExistingMatchIndex|Boolean|A value that specifies whether the match index specified in MatchIndexName is deleted when MatchIndexOptions is not set to ReuseExistingIndex. The default value for this property is **True**.|  
|Exhaustive|Boolean|A value that specifies whether each input record is compared to every other input record. The value of **True** is intended mostly for debugging purposes. The default value of this property is **False**.<br /><br /> Note: This property is not available in the **Fuzzy Lookup Transformation Editor**, but can be set by using the **Advanced Editor**.|  
|MatchIndexName|String|The name of the match index. The match index is the table in which the transformation creates and saves the index that it uses. If the match index is reused, MatchIndexName specifies the index to reuse. MatchIndexName must be a valid [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] identifier name. For example, if the name contains spaces, the name must be enclosed in brackets.|  
|MatchIndexOptions|Integer (enumeration)|A value that specifies how the transformation manages the match index. This property can have one of the following values:<br /><br /> **ReuseExistingIndex** (0)<br /><br /> **GenerateNewIndex** (1)<br /><br /> **GenerateAndPersistNewIndex** (2)<br /><br /> **GenerateAndMaintainNewIndex** (3)|  
|MaxMemoryUsage|Integer|The maximum cache size for the lookup table. The default value of this property is **0**, which means the cache size has no limit.<br /><br /> The value of this property can be specified by using a property expression.<br /><br /> Note: This property is not available in the **Fuzzy Lookup Transformation Editor**, but can be set by using the **Advanced Editor**.|  
|MaxOutputMatchesPerInput|Integer|The maximum number of matches the transformation can return for each input row. The default value of this property is **1**.<br /><br /> Note: Values greater than 100 can only be specified by using the **Advanced Editor**.|  
|MinSimilarity|Integer|The similarity threshold that the transformation uses at the component level, specified as a value between 0 and 1. Only rows greater than the threshold qualify as matches.|  
|ReferenceMetadataXML|String|[!INCLUDE[ssInternalOnly](../../../includes/ssinternalonly-md.md)]|  
|ReferenceTableName|String|The name of the lookup table. The name must be a valid [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] identifier name. For example, if the name contains spaces, the name must be enclosed in brackets.|  
|WarmCaches|Boolean|When true, the lookup partially loads the index and reference table into memory before execution begins. This can enhance performance.|  
  
 The following table describes the custom properties of the input columns of the Fuzzy Lookup transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|FuzzyComparisonFlags|Integer|A value that specifies how the transformation compares the string data in a column. For more information, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).|  
|FuzzyComparisonFlagsEx|Integer (enumeration)|A value that specifies which extended comparison flags the transformation uses. The values can include **MapExpandLigatures, MapFoldCZone**, **MapFoldDigits**, **MapPrecomposed**, and **NoMapping**. **NoMapping** cannot be used with other flags.|  
|JoinToReferenceColumn|String|A value that specifies the name of the column in the reference table to which the column is joined.|  
|JoinType|Integer|A value that specifies whether the transformation performs a fuzzy or an exact match. The default value for this property is **Fuzzy**. The integer value for the exact join type is **1** and the value for the fuzzy join type is **2**.|  
|MinSimilarity|Double|The similarity threshold that the transformation uses at the column level, specified as a value between 0 and 1. Only rows greater than the threshold qualify as matches.|  
  
 The following table describes the custom properties of the output columns of the Fuzzy Lookup transformation. All properties are read/write.  
  
> [!NOTE]  
>  For output columns that contain passthrough values from the corresponding input columns, CopyFromReferenceColumn is empty and SourceInputColumnLineageID contains the **LineageID** of the corresponding input column. For output columns that contain lookup results, CopyFromReferenceColumn contains the name of the lookup column, and SourceInputColumnLineageID is empty.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|ColumnType|Integer  (enumeration)|A value that identifies the type of output column for columns that the transformation adds to the output. This property can have one of the following values:<br /><br /> **Undefined** (0)<br /><br /> **Similarity** (1)<br /><br /> **Confidence** (2)<br /><br /> **ColumnSimilarity** (3)|  
|CopyFromReferenceColumn|String|A value that specifies the name of the column in the reference table that provides the value in an output column.|  
|SourceInputColumnLineageId|Integer|A value that identifies the input column that provides values to this output column.|  
  
 The input and output of the Fuzzy Lookup transformation have no custom properties.  
  
 For more information, see [Fuzzy Lookup Transformation](../../../integration-services/data-flow/transformations/fuzzy-lookup-transformation.md).  
  
##  <a name="lookup"></a> Lookup Transformation Custom Properties  
 The Lookup transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Lookup transformation. All properties except for **ReferenceMetadataXML** are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|CacheType|Integer (enumeration)|The cache type for the lookup table. The values are **Full** (0), **Partial** (1), and **None** (2). The default value of this property is **Full**.|  
|DefaultCodePage|Integer|The default code page to use when code page information is not available from the data source.|  
|MaxMemoryUsage|Integer|The maximum cache size for the lookup table. The default value of this property is **25**, which means that the cache size has no limit.|  
|MaxMemoryUsage64|Integer|The maximum cache size for the lookup table on a 64-bit computer.|  
|NoMatchBehavior|Integer (enumeration)|A value that specifies whether rows without matching entries in the reference dataset are treated as errors.<br /><br /> When the property is set to **Treat rows with no matching entries as errors** (0), the rows without matching entries are treated as errors. You can specify what should happen when this type of error occurs by using the **Error Output** page of the **Lookup Transformation Editor** dialog box. For more information, see [Lookup Transformation Editor &#40;Error Output Page&#41;](./lookup-transformation.md).<br /><br /> When the property is set to **Send rows with no matching entries to the no match output** (1), the rows are not treaded as errors.<br /><br /> The default value is **Treat rows with no matching entries as errors** (0).|  
|ParameterMap|String|A semicolon-delimited list of lineage IDs that map to the parameters used in the **SqlCommand** statement.|  
|ReferenceMetaDataXML|String|Metadata for the columns in the lookup table that the transformation copies to its output.|  
|SqlCommand|String|The SELECT statement that populates the lookup table.|  
|SqlCommandParam|String|The parameterized SQL statement that populates the lookup table.|  
  
 The following table describes the custom properties of the input columns of the Lookup transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|CopyFromReferenceColumn|String|The name of the column in the reference table from which a column is copied.|  
|JoinToReferenceColumns|String|The name of the column in the reference table to which a source column joins.|  
  
 The following table describes the custom properties of the output columns of the Lookup transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|CopyFromReferenceColumn|String|The name of the column in the reference table from which a column is copied.|  
  
 The input and output of the Lookup transformation have no custom properties.  
  
 For more information, see [Lookup Transformation](../../../integration-services/data-flow/transformations/lookup-transformation.md).  
  
##  <a name="mjoin"></a> Merge Join Transformation Custom Properties  
 The Merge Join transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Merge Join transformation.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|JoinType|Integer (enumeration)|Specifies whether the join is an inner (2), left outer (1), or full join (0).|  
|MaxBuffersPerInput|Integer|You no longer have to configure the value of the **MaxBuffersPerInput** property because Microsoft has made changes that reduce the risk that the Merge Join transformation will consume excessive memory. This problem sometimes occurred when the multiple inputs of the Merge Join produced data at uneven rates.|  
|NumKeyColumns|Integer|The number of columns that are used in the join.|  
|TreatNullsAsEqual|Boolean|A value that specifies whether the transformation handles null values as equal values. The default value of this property is **True**. If the property value is **False**, the transformation handles null values like [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does.|  
  
 The following table describes the custom properties of the output columns of the Merge Join transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|InputColumnID|Integer|The **LineageID** of the input column from which data is copied to this output column.|  
  
 The input, the input columns, and the output of the Merge Join transformation have no custom properties.  
  
 For more information, see [Merge Join Transformation](../../../integration-services/data-flow/transformations/merge-join-transformation.md).  
  
##  <a name="oledbcmd"></a> OLE DB Command Transformation Custom Properties  
 The OLE DB Command transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the OLE DB Command transformation.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|CommandTimeout|Integer|The maximum number of seconds that the SQL command can run before timing out. A value of **0** indicates an infinite time. The default value of this property is **0**.|  
|DefaultCodePage|Integer|The code page to use when code page information is unavailable from the data source.|  
|SQLCommand|String|The Transact-SQL statement that the transformation runs for each row in the data flow.<br /><br /> The value of this property can be specified by using a property expression.|  
  
 The following table describes the custom properties of the external columns of the OLE DB Command transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|DBParamInfoFlag|Integer (bitmask)|A set of flags that describe parameter characteristics. For more information, see the DBPARAMFLAGSENUM in the OLE DB documentation in the MSDN Library.|  
  
 The input, the input columns, the output, and the output columns of the OLE DB Command transformation have no custom properties.  
  
 For more information, see [OLE DB Command Transformation](../../../integration-services/data-flow/transformations/ole-db-command-transformation.md).  
  
##  <a name="percent"></a> Percentage Sampling Transformation Custom Properties  
 The Percentage Sampling transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Percentage Sampling transformation.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|SamplingSeed|Integer|The seed the random number generator uses. The default value of this property is **0**, indicating that the transformation uses a tick count.|  
|SamplingValue|Integer|The size of the sample as a percentage of the source.<br /><br /> The value of this property can be specified by using a property expression.|  
  
 The following table describes the custom properties of the outputs of the Percentage Sampling transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|Selected|Boolean|Designates the output to which the sampled rows are directed. On the selected output, Selected is set to **True**, and on the unselected output, Selected is set to **False**.|  
  
 The input, the input columns, and the output columns of the Percentage Sampling transformation have no custom properties.  
  
 For more information, see [Percentage Sampling Transformation](../../../integration-services/data-flow/transformations/percentage-sampling-transformation.md).  
  
##  <a name="pivot"></a> Pivot Transformation Custom Properties  
 The following table describes the custom component properties of the Pivot transformation.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|**PassThroughUnmatchedPivotKeyts**|Boolean|Set to **True** to configure the Pivot transformation to ignore rows containing unrecognized values in the Pivot Key column and to output all of the pivot key values to a log message, when the package is run.|  
  
 The following table describes the custom properties of the input columns of the Pivot transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|PivotUsage|Integer (enumeration)|A value that specifies the role of a column when the data set is pivoted.<br /><br /> **0**:<br />                      The column is not pivoted, and the column values are passed through to the transformation output.<br /><br /> **1**:<br />                      The column is part of the set key that identifies one or more rows as part of one set. All input rows with the same set key are combined into one output row.<br /><br /> **2**:<br />                      The column is a pivot column. At least one column is created from each column value.<br /><br /> **3**:<br />                      The values from this column are put in columns that are created as a result of the pivot.|  
  
 The following table describes the custom properties of the output columns of the Pivot transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|PivotKeyValue|String|One of the possible values from the column that is marked as the pivot key by the value of its PivotUsage property.<br /><br /> The value of this property can be specified by using a property expression.|  
|SourceColumn|Integer|The **LineageID** of an input column that contains a pivoted value, or -1. The value of -1 indicates that the column is not used in a pivot operation.|  
  
 For more information, see [Pivot Transformation](../../../integration-services/data-flow/transformations/pivot-transformation.md).  
  
##  <a name="rowcount"></a> Row Count Transformation Custom Properties  
 The Row Count transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Row Count transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|VariableName|String|The name of the variable that holds the row count.|  
  
 The input, the input columns, the output, and the output columns of the Row Count transformation have no custom properties.  
  
 For more information, see [Row Count Transformation](../../../integration-services/data-flow/transformations/row-count-transformation.md).  
  
##  <a name="rowsamp"></a> Row Sampling Transformation Custom Properties  
 The Row Sampling transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Row Sampling transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|SamplingSeed|Integer|The seed that the random number generator uses. The default value of this property is **0**, indicating that the transformation uses a tick count.|  
|SamplingValue|Integer|The row count of the sample.<br /><br /> The value of this property can be specified by using a property expression.|  
  
 The following table describes the custom properties of the outputs of the Row Sampling transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|Selected|Boolean|Designates the output to which the sampled rows are directed. On the selected output, Selected is set to **True**, and on the unselected output, Selected is set to **False**.|  
  
 The following table describes the custom properties of the output columns of the Row Sampling transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|InputColumnLineageId|Integer|A value that specifies the **LineageID** of the input column that is the source of the output column.|  
  
 The input and input columns of the Row Sampling transformation have no custom properties.  
  
 For more information, see [Row Sampling Transformation](../../../integration-services/data-flow/transformations/row-sampling-transformation.md).  
  
##  <a name="script"></a> Script Component Custom Properties  
 The Script Component has both custom properties and the properties common to all data flow components. The same custom properties are available whether the Script Component functions as a source, transformation, or destination.  
  
 The following table describes the custom properties of the Script Component. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|ReadOnlyVariables|String|A comma-separated list of variables available to the Script Component for read-only access.|  
|ReadWriteVariables|String|A comma-separated list of variables available to the Script Component for read/write access.|  
  
 The input, the input columns, the output, and the output columns of the Script Component have no custom properties, unless the script developer creates custom properties for them.  
  
 For more information, see [Script Component](../../../integration-services/data-flow/transformations/script-component.md).  
  
##  <a name="scd"></a> Slowly Changing Dimension Transformation Custom Properties  
 The Slowly Changing Dimension transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Slowly Changing Dimension transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|CurrentRowWhere|String|The WHERE clause in the SELECT statement that selects the current row among rows with the same business key.|  
|EnableInferredMember|Boolean|A value that specifies whether inferred member updates are detected. The default value of this property is **True**.|  
|FailOnFixedAttributeChange|Boolean|A value that specifies whether the transformation fails when row columns with fixed attributes contain changes or the lookup in the dimension table fails. If you expect incoming rows to contain new records, set this value to **True** to make the transformation continue after the lookup fails, because the transformation uses the failure to identify new records. The default value of this property is **False**.|  
|FailOnLookupFailure|Boolean|A value that specifies whether the transformation fails when a lookup of an existing record fails. The default value of this property is **False**.|  
|IncomingRowChangeType|Integer|A value that specifies whether all incoming rows are new rows, or whether the transformation should detect the type of change.|  
|InferredMemberIndicator|String|The column name for the inferred member.|  
|SQLCommand|String|The SQL statement used to create a schema rowset.|  
|UpdateChangingAttributeHistory|Boolean|A value that indicates whether historical attribute updates are directed to the transformation output for changing attribute updates.|  
  
 The following table describes the custom properties of the input columns of the Slowly Changing Dimension transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|ColumnType|Integer (enumeration)|The update type of the column. The values are: **Changing Attribute** (2), **Fixed Attribute** (4), **Historical Attribute** (3), **Key** (1), and **Other** (0).|  
  
 The input, the outputs, and the output columns of the Slowly Changing Dimension transformation have no custom properties.  
  
 For more information, see [Slowly Changing Dimension Transformation](../../../integration-services/data-flow/transformations/slowly-changing-dimension-transformation.md).  
  
##  <a name="sort"></a> Sort Transformation Custom Properties  
 The Sort transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Sort transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|EliminateDuplicates|Boolean|Specifies whether the transformation removes duplicate rows from the transformation output. The default value of this property is **False**.|  
|MaximumThreads|Integer|Contains the maximum number of threads the transformation can use for sorting. A value of **0** indicates an infinite number of threads. The default value of this property is **0**.<br /><br /> The value of this property can be specified by using a property expression.|  
  
 The following table describes the custom properties of the input columns of the Sort transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|NewComparisonFlags|Integer (bitmask)|A value that specifies how the transformation compares the string data in a column. For more information, see [Comparing String Data](../../../integration-services/data-flow/comparing-string-data.md).|  
|NewSortKeyPosition|Integer|A value that specifies the sort order of the column. A value of 0 indicates that the data is not sorted on this column.|  
  
 The following table describes the custom properties of the output columns of the Sort transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|SortColumnID|Integer|The **LineageID** of a sort column.|  
  
 The input and output of the Sort transformation have no custom properties.  
  
 For more information, see [Sort Transformation](../../../integration-services/data-flow/transformations/sort-transformation.md).  
  
##  <a name="textract"></a> Term Extraction Transformation Custom Properties  
 The Term Extraction transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Term Extraction transformation. All properties are read/write.  
  
|Property|Data ype|Description|  
|--------------|--------------|-----------------|  
|FrequencyThreshold|Integer|A numeric value that indicates the number of times a term must occur before it is extracted. The default value of this property is **2**.|  
|IsCaseSensitive|Boolean|A value that specifies whether case sensitivity is used when extracting nouns and noun phrases. The default value of this property is **False**.|  
|MaxLengthOfTerm|Integer|A numeric value that indicates the maximum length of a term. This property applies only to phrases. The default value of this property is **12**.|  
|NeedRefenceData|Boolean|A value that specifies whether the transformation uses a list of exclusion terms stored in a reference table. The default value of this property is **False**.|  
|OutTermColumn|String|The name of the column that contains the exclusion terms.|  
|OutTermTable|String|The name of the table that contains the column with exclusion terms.|  
|ScoreType|Integer|A value that specifies the type of score to associate with the term. Valid values are 0 indicating frequency, and 1 indicating a TFIDF score. The TFIDF score is the product of Term Frequency and Inverse Document Frequency, defined as: TFIDF of a Term T = (frequency of T) \* log( (#rows in Input) / (#rows having T) ). The default value of this property is **0**.|  
|WordOrPhrase|Integer|A value that specifies term type. The valid values are 0 indicating words only, 1 indicating noun phrases only, and 2 indicating both words and noun phrases. The default value of this property is **0**.|  
  
 The input, the input columns, the output, and the output columns of the Term Extraction transformation have no custom properties.  
  
 For more information, see [Term Extraction Transformation](../../../integration-services/data-flow/transformations/term-extraction-transformation.md).  
  
##  <a name="tlookup"></a> Term Lookup Transformation Custom Properties  
 The Term Lookup transformation has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the Term Lookup transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|IsCaseSensitive|Boolean|A value that specifies whether a case sensitive comparison applies to the match of the input column text and the lookup term. The default value of this property is **False**.|  
|RefTermColumn|String|The name of the column that contains the lookup terms.|  
|RefTermTable|String|The name of the table that contains the column with lookup terms.|  
  
 The following table describes the custom properties of the input columns of the Term Lookup transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|InputColumnType|Integer|A value that specifies the use of the column. Valid values are 0 for a passthrough column, 1 for a lookup column, and 2 for a column that is both a passthrough and a lookup column.|  
  
 The following table describes the custom properties of the output columns of the Term Lookup transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|CustomLineageID|Integer|The **LineageID** of the corresponding input column if the **InputColumnType** of that column is 0 or 2.|  
  
 The input and output of the Term Lookup transformation have no custom properties.  
  
 For more information, see [Term Lookup Transformation](../../../integration-services/data-flow/transformations/term-lookup-transformation.md).  
  
##  <a name="unpivot"></a> Unpivot Transformation Custom Properties  
 The Unpivot transformation has only the properties common to all data flow components at the component level.  
  
> [!NOTE]  
>  This section relies on the Unpivot scenario described in [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md) to illustrate the use of the options described here.  
  
 The following table describes the custom properties of the input columns of the Unpivot transformation. All properties are read/write.  
  
|Property|Data type|Description|  
|--------------|---------------|-----------------|  
|DestinationColumn|Integer|The **LineageID** of the output column to which the input column maps. A value of -1 indicates that the input column is not mapped to an output column.|  
|PivotKeyValue|String|A value that is copied to a transformation output column.<br /><br /> The value of this property can be specified by using a property expression.<br /><br /> In the Unpivot scenario described in [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md), the Pivot Values are the text values Ham, Coke, Milk, Beer, and Chips. These will appear as text values in the new Product column designated by the **Pivot Key Value Column Name** option.|  
  
 The following table describes the custom properties of the output columns of the Unpivot transformation. All properties are read/write.  
  
|Property name|Data type|Description|  
|-------------------|---------------|-----------------|  
|PivotKey|Boolean|Indicates whether the values in the **PivotKeyValue** property of input columns are written to this output column.<br /><br /> In the Unpivot scenario described in [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md), the Pivot Value column name is **Product** and designates the new **Product** column into which the Ham, Coke, Milk, Beer, and Chips columns are unpivoted.|  
  
 The input and output of the Unpivot transformation have no custom properties.  
  
 For more information, see [Unpivot Transformation](../../../integration-services/data-flow/transformations/unpivot-transformation.md).  
  
## See Also  
 [Integration Services Transformations](../../../integration-services/data-flow/transformations/integration-services-transformations.md)   
 [Common Properties](../set-the-properties-of-a-data-flow-component.md)   
 [Path Properties](../integration-services-paths.md)   
 [Data Flow Properties that Can Be Set by Using Expressions](/previous-versions/sql/sql-server-2016/ms136104(v=sql.130))  
  
