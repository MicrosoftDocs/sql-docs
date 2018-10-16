---
title: "Supported XMLA Properties (XMLA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "properties [XML for Analysis]"
  - "XML for Analysis, properties"
  - "XMLA, properties"
ms.assetid: 5745f7b4-6b96-44d5-b77c-f2831a898e5e
author: minewiskan
ms.author: owend
manager: craigg
---
# Supported XMLA Properties (XMLA)
  [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] supports the properties listed in the following table. You use these listed properties in the [Properties](properties-element-xmla.md) element of the [Discover](../xml-elements-methods-discover.md) and [Execute](../xml-elements-methods-execute.md) methods.  
  
|Name|Element|  
|----------|-------------|  
|AxisFormat|*Usage*<br /> Optional, write-only `String` property<br /><br /> *Description*<br /> Determines the format used within an [MDDataSet](../xml-data-types/mddataset-data-type-xmla.md) result set to describe the axes of the multidimensional dataset. This property can have the values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*ClusterFormat*|The `MDDataSet` axis is made up of one or more [CrossProduct](crossproduct-element-xmla.md) elements.|  
|*CustomFormat*|[!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] uses the *TupleFormat* format for this setting.|  
|*TupleFormat*|(Default) The `MDDataSet` axis contains one or more [Tuple](tuple-element-xmla.md) elements.|  
  
 This property can be used with the `Execute` method.  
  
|Name|Element|  
|----------|-------------|  
|BeginRange|*Usage*<br /> Optional, write-only `Integer` property<br /><br /> *Description*<br /> Contains a zero-based integer value corresponding to a `CellOrdinal` attribute value. (The `CellOrdinal` attribute is part of the [Cell](cell-element-mddataset-xmla.md) element in the [CellData](celldata-element-xmla.md) section of `MDDataSet`.)<br /><br /> Used together with the `EndRange` property, the client application can use this property to restrict an OLAP dataset returned by a command to a specific range of cells. If -1 is specified, all cells up to the cell specified in the `EndRange` property are returned.<br /><br /> The default value for this property is -1.<br /><br /> This property can be used with the `Execute` method.|  
|Catalog|*Usage*<br /> Optional, read/write `String` property<br /><br /> *Description*<br /> When establishing a session with an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance to send an XMLA command, this property is equivalent to the OLE DB property, DBPROP_INIT_CATALOG.<br /><br /> When you set this property during a session to change the current database for the session, this property is equivalent to the OLE DB property, DBPROP_CURRENTCATALOG.<br /><br /> The default value for this property is an empty string.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|CatalogLocation|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_CATALOGLOCATION.<br /><br /> The default value for this property is zero (0), equivalent to DBPROPVAL_CL_START.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|ClientProcessID|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Contains the identifier (ID) of the process thread for the current session.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|CommitTimeout|*Usage*<br /> Optional, write-only `Integer` property<br /><br /> *Description*<br /> Determines how long, in seconds, the commit phase of a currently running XMLA command waits before rolling back. The commit phase corresponds to XMLA commands such as [Statement](../xml-elements-commands/statement-element-xmla.md) or [Process](../xml-elements-commands/process-element-xmla.md).<br /><br /> A value of zero (0) indicates that the instance waits indefinitely.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|Content|*Usage*<br /> Optional, write-only `String` property<br /><br /> *Description*<br /> Determines the type of data that is returned from the `Discover` and `Execute` methods. This property can have the values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*None*|Allows the structure of the command to be verified, but not run.|  
|*Schema*|Returns the XML schema that relates to the requested command. The XML schema indicates columns and other information.|  
|*Data*|Returns only the data that was requested.|  
|*SchemaData*|(Default) Returns the schema information and the data.|  
  
 This property can be used with the `Discover` and `Execute` methods.  
  
|Name|Element|  
|----------|-------------|  
|Cube|*Usage*<br /> Optional, write-only `String` property<br /><br /> *Description*<br /> Contains the name of the cube that sets the context for the command. If the command itself contains a cube name, such as within the FROM clause of a Multidimensional Expressions (MDX) [SELECT](/sql/mdx/mdx-data-manipulation-select) statement, the setting of this property is ignored.<br /><br /> The default value for this property is an empty string.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DataSourceInfo|*Usage*<br /> Required, read/write `String` property<br /><br /> *Description*<br /> Contains the information, such as the instance name, required to connect to the data source.<br /><br /> Client applications should not construct the contents of the `DataSourceInfo` property to send to an instance. Instead, the client application should find the data sources supported by the provider by using the `Discover` method to retrieve the [DISCOVER_DATASOURCES](../../schema-rowsets/xml/discover-datasources-rowset.md) rowset. The client application then sends back the same value for the `DataSourceInfo` property that the client retrieved from the DISCOVER_DATASOURCES rowset.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropCatalogTerm|*Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_CATALOGTERM.<br /><br /> The default value for this property is "Database".<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropCatalogUsage|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_CATALOGUSAGE.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropColumnDefinition|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_COLUMNDEFINITION.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropConcatNullBehavior|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_CONCATNULLBEHAVIOR.<br /><br /> The default value for this property is 1, equivalent to DBPROPVAL_CB_NULL.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropDataSourceReadOnly|*Usage*<br /> Optional, read-only `Boolean` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_DATASOURCEREADONLY.<br /><br /> The default value for this property is FALSE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropGroupBy|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_GROUPBY.<br /><br /> The default value for this property is 2, equivalent to DBPROPVAL_GB_EQUALS_SELECT.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropHeterogeneousTables|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_HETEROGENEOUSTABLES.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropIdentifierCase|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_IDENTIFIERCASE.<br /><br /> The default value for this property is 8, equivalent to DBPROPVAL_IC_MIXED.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropInitMode|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_INIT_MODE.<br /><br /> The only supported values for this property are DB_MODE_READWRITE and DB_MODE_READ.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMaxIndexSize|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_MAXINDEXSIZE.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMaxOpenChapters|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_MAXOPENCHAPTERS.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMaxRowSize|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_MAXROWSIZE.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMaxRowSizeIncludeBlob|*Usage*<br /> Optional read-only `Boolean` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_MAXROWSIZEINCLUDESBLOB.<br /><br /> The default value for this property is TRUE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMaxTablesInSelect|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property DBPROP_MAXTABLESINSELECT.<br /><br /> The default value for this property is 1.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdAutoexists|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Determines the behavior of autoexists. This property can have the values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*0*|Default value, same as 1.|  
|*1*|Apply deep autoexists for query axes and named sets. Includes WHERE clauses and subselects.|  
|*2*|Apply deep autoexists for query axes and exclude named sets from autoexists. Includes WHERE clauses and subselects.|  
|*3*|Apply no autoexists for named sets with WHERE clause. Apply shallow autoexists for query axes with WHERE clause. Apply deep autoexists for query axes with subselects and named sets with subselects.|  
  
 Zero or empty are the default values for this property. This is a session property that can only be set when the session is created.  
  
|Name|Element|  
|----------|-------------|  
|DbpropMsmdCacheMode|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdCachePolicy|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdCacheRatio|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdCacheRatio2|*Usage*<br /> Optional, read/write `Double` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdCompareCaseNotSensitiveStringFlags|*Usage*<br /> Optional read/write `Integer` property<br /><br /> *Description*<br /> Determines case-sensitive string comparison and sort order functionality. This property controls how comparisons are made in character sets that do not support uppercase and lowercase characters, such as katakana for Japanese and Hindi. The value of this property is set in the first connection of the process thread, and affects all subsequent connections in that process thread.<br /><br /> Use the following table to determine which flags to use.|  
  
|Name|Value|Description|  
|----------|-----------|-----------------|  
|NORM_IGNORECASE|*0x00000001*|Case is ignored.|  
|Not applicable|*0x00000002*|Binary comparison. Characters are compared based on their underlying value in the character set, not on their order in their particular alphabet.|  
|NORM_IGNORENONSPACE|*0x00000010*|Nonspacing characters are ignored.|  
|NORM_IGNORESYMBOLS|*0x00000100*|Symbols are ignored.|  
|NORM_IGNOREKANATYPE|*0x00001000*|No differentiation is made between hiragana and katakana characters. When compared, corresponding hiragana and katakana characters are considered to be equal.|  
|NORM_IGNOREWIDTH|*0x00010000*|No differentiation is made between single-byte and double-byte versions of the same character.|  
|SORT_STRINGSORT|*0x00100000*|Punctuation is treated the same as symbols.|  
  
 For more information about comparing strings in OLE DB, search on "CompareString" in the Platform SDK section of the MSDN Library.  
  
 There is no default value for this property.  
  
 This property can be used with the `Discover` and `Execute` methods.  
  
|Name|Element|  
|----------|-------------|  
|DbpropMsmdCompareCaseSensitiveStringFlags|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Determines case-insensitive string comparison and sort order functionality. This property controls how comparisons are made in character sets that do not support uppercase and lowercase characters, such as katakana for Japanese and Hindi. The value of this property is set in the first connection of the process thread, and affects all subsequent connections in that process thread.<br /><br /> Use the following table to determine which flags to use.|  
  
|Name|Value|Description|  
|----------|-----------|-----------------|  
|NORM_IGNORECASE|*0x00000001*|Case is ignored.|  
|Not applicable|*0x00000002*|Binary comparison. Characters are compared based on their underlying value in the character set, not on their order in their particular alphabet.|  
|NORM_IGNORENONSPACE|*0x00000010*|Nonspacing characters are ignored.|  
|NORM_IGNORESYMBOLS|*0x00000100*|Symbols are ignored.|  
|NORM_IGNOREKANATYPE|*0x00001000*|No differentiation is made between hiragana and katakana characters. When compared, corresponding hiragana and katakana characters are considered to be equal.|  
|NORM_IGNOREWIDTH|*0x00010000*|No differentiation is made between single-byte and double-byte versions of the same character.|  
|SORT_STRINGSORT|*0x00100000*|Punctuation is treated the same as symbols.|  
  
 For more information about comparing strings in OLE DB, search on "CompareString" in the Platform SDK section of the MSDN Library.  
  
 There is no default value for this property.  
  
 This property can be used with the `Discover` and `Execute` methods.  
  
|Name|Element|  
|----------|-------------|  
|DbpropMsmdDebugMode|*Usage*<br /> Optional, read/write `String` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdDynamicDebugLimit|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdFlattened2|*Usage*<br /> Optional, read/write `Boolean` property<br /><br /> *Description*<br /> Outputs all members of a parent-child hierarchy in a single table column in the flattened result, unless the parent-child hierarchy is requested on Axis 0. The Level template for output columns is not used.<br /><br /> The default value for this property is FALSE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdMDXCompatibility|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Determines how placeholder members in a ragged or unbalanced hierarchy are treated. This property can have the values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*0*|For compatibility with earlier versions of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], this value is equivalent to 1|  
|*1*|Hierarchies in role-playing dimensions receive a caption that includes the dimension name and the hierarchy name. The caption has the following format:<br /><br /> [Dimension].[Hierarchy]<br /><br /> Placeholder members are exposed.|  
|*2*|Hierarchies in role-playing dimensions receive a caption that includes the dimension name and the hierarchy name The caption has the following format:<br /><br /> [Dimension].[Hierarchy]<br /><br /> Placeholder members are not exposed.|  
|*3*|(Default) Placeholder members are not exposed.|  
  
 This property can be used with the `Discover` and `Execute` methods.  
  
|Name|Element|  
|----------|-------------|  
|DbpropMsmdMDXUniqueNameStyle|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Determines the algorithm for generating the unique names of members in a dimension. This property can have the values listed in the following table.<br /><br /> The default value for this property is 6.|  
  
|Value|Description|  
|-----------|-----------------|  
|*0*|For compatibility with earlier versions of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], this value is equivalent to 2.|  
|*1*|Uses a key path algorithm:<br /><br /> [dim].&[key1].&[key2]|  
|*2*|Uses a name path algorithm:<br /><br /> [dim].[name1].&[name2]|  
|*3*|Uses guaranteed unique names that are stable over time.|  
  
 This property can be used with the `Discover` and `Execute` methods.  
  
|Name|Element|  
|----------|-------------|  
|DbpropMsmdSQLCompatibility|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMsmdSubQueries|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> A bitmask that determines the behavior of subqueries. This property can have the values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*0*|Default value,  Compatible with earlier versions of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].<br /><br /> Calculated members or calculated sets are not allowed in subselects or subcubes.|  
|*1*|Calculated members or calculated sets are allowed in subselects or subcubes. Ascendants of the calculated member are not included in the space of the subselect or subcube.|  
|*2*|Calculated members or calculated sets are allowed in subselects or subcubes. Ascendants of the calculated member are included in the space of the subselect or subcube.|  
  
 Zero or empty are the default values for this property.  
  
 This is a session property that can only be set when the session is created.  
  
 See [Calculated Members in Subselects and Subcubes](../../multidimensional-models/mdx/calculated-members-in-subselects-and-subcubes.md) for a detailed explanation of the behavior of calculated members or calculated sets in subselects and subcubes.  
  
|Name|Element|  
|----------|-------------|  
|DbpropMsmdUseFormulaCache|*Usage*<br /> Optional, read/write `String` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropMultiTableUpdate|*Usage*<br /> Optional, read-only `Boolean` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_MULTITABLEUPDATE.<br /><br /> The default value for this property is FALSE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropNullCollation|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_NULLCOLLATION.<br /><br /> The default value for this property is 4, equivalent to DBPROPVAL_NC_LOW.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropOrderByColumnsInSelect|*Usage*<br /> Optional, read-only `Boolean` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_ORDERBYCOLUMNSINSELECT.<br /><br /> The default value for this property is FALSE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropOutputParameterAvailable|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_OUTPUTPARAMETERAVAILABILITY.<br /><br /> The default value for this property is 1, equivalent to DBPROPVAL_OA_NOTSUPPORTED.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropPersistentIdType|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_PERSISTENTIDTYPE.<br /><br /> The default value for this property is 4, equivalent to DBPROPVAL_PT_NAME.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropPrepareAbortBehavior|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_PREPAREABORTBEHAVIOR.<br /><br /> The default value for this property is 1, equivalent to DBPROPVAL_CB_DELETE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropPrepareCommitBehavior|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property DBPROP_PREPARECOMMITBEHAVIOR.<br /><br /> The default value for this property is 1, equivalent to DBPROPVAL_CB_DELETE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropProcedureTerm|*Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_PROCEDURETERM.<br /><br /> The default value for this property is "Calculated member".<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropQuotedIdentifierCase|*Usage*<br /> Optional read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_QUOTEDIDENTIFIERCASE.<br /><br /> The default value for this property is 8, equivalent to DBPROPVAL_IC_MIXED.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropSchemaUsage|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_SCHEMAUSAGE.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropSqlSupport|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_SQLSUPPORT.<br /><br /> The default value for this property is 512, equivalent to DBPROPVAL_SQL_SUBMINIMUM.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropSubqueries|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_SUBQUERIES.<br /><br /> **Note!** While Data Mining Extensions (DMX) supports subqueries, this property refers to subquery support in SQL.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropSupportedTxnDdl|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_SUPPORTEDTXNDDL.<br /><br /> The default value for this property is zero (0), equivalent to DBPROPVAL_TC_NONE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropSupportedTxnIsoLevels|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_SUPPORTEDTXNISOLEVELS.<br /><br /> The default value for this property is 4096, equivalent to DBPROPVAL_TI_READCOMMITTED.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropSupportedTxnIsoRetain|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_SUPPORTEDTXNISORETAIN.<br /><br /> The default value for this property is 292, equivalent to a combination of DBPROPVAL_TR_ABORT_NO, DBPROPVAL_TR_COMMIT_NO, and DBPROPVAL_TR_NONE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|DbpropTableTerm|*Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_TABLETERM.<br /><br /> The default value for this property is "Cube".<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|Dialect|*Usage*<br /> Optional, read/write `String` property<br /><br /> *Description*<br /> Establishes the dialect used in the following situations:<br /><br /> -   The dialect that the provider will use the first time that the provider tries to run a query.<br />-   The dialect used for the execution errors returned as the result of query failures.<br /><br /> You can use the `Dialect` property when you expect that most of queries will use one particular dialect over any other.<br /><br /> Query syntax can be similar for language dialects, such as DMX and SQL. Because the syntax can be similar, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] may not be able to infer the dialect from the query syntax. If a query does not run in one dialect, the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance may try to run the query again in a different dialect.<br /><br /> If the `Dialect` property is set, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] returns query execution errors in the dialect that has precedence, even if the provider tries to run the query again in another dialect. For example, the `Dialect` property is set to MDGUID_DM. The provider first tries to run the query as a data mining query, but this query fails. The provider then resubmits the query as an SQL query. However, this SQL query also fails. Because the value of the `Dialect` property is MDGUID_DM, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] returns a data mining error message, not an SQL error message.<br /><br /> If the `Dialect` property is not set, [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] returns query execution errors in the dialect last used. For example, the `Dialect` property is not set, and a data mining query fails. The provider then resubmits the query as SQL. The SQL query also fails. Because the `Dialect` property is not set, the provider returns an SQL error message instead of a data mining error message.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.<br /><br /> The dialects available to this property are listed in the following table.|  
  
|Name|Value|Description|  
|----------|-----------|-----------------|  
|DBGUID_SQL|*C8B522D7-5CF3-11CE-ADE5-00AA0044773D*|The SQL parser has precedence.|  
|MDGUID_DM|*62C58FED-CCA5-44F1-83B6-7B45682B3904*|The DMX parser has precedence.|  
|MDGUID_MDX|*A07CCCD0-8148-11D0-87BB-00C04FC33942*|The MDX parser has precedence.|  
  
|Name|Element|  
|----------|-------------|  
|Disable Prefetch Facts|*Usage*<br /> Optional, read/write `Boolean` property,<br /><br /> *Description*<br /> When set to True, the engine stops trying to pre-fetch values for the length of the session.<br /><br /> The default value for this property is `False`.|  
|EffectiveRoles|*Usage*<br /> Optional, write-only `String` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|EffectiveUserName|*Usage*<br /> Optional, write-only `String` property<br /><br /> *Description*<br /> Specifies the name of an account to use to override the user name when connecting to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. The value of the property is not normalized, in that the MDX [UserName](/sql/mdx/username-mdx) function returns the literal value if this property is used. This property can only be used by server administrators.<br /><br /> This property supports the following SID types: User, Group, Alias, WellKnownGroup, Computer.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|EndRange|*Usage*<br /> Optional, write-only `Integer` property<br /><br /> *Description*<br /> Specifies a zero-based integer value corresponding to a `CellOrdinal` attribute value. (The `CellOrdinal` attribute is part of the `Cell` element in the `CellData` section of `MDDataSet`).<br /><br /> Used together with the `BeginRange` property, the client application can use this property to restrict an OLAP dataset returned by a command to a specific range of cells. If -1 is specified, all cells from the cell specified in the `BeginRange` property are returned.<br /><br /> The default value for this property is -1.<br /><br /> This property can be used with the `Execute` method.|  
|ExecutionMode|*Usage*<br /> Optional, write-only `String` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> The default value for this property is *Execute*.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|ForceCommitTimeout|*Usage*<br /> Optional, write-only `Integer` property<br /><br /> *Description*<br /> Determines for how long, in seconds, the commit phase of a currently running XMLA command waits before forcing previously issued commands to roll back. The commit phase corresponds to XMLA commands such as `Statement` or `Process`.<br /><br /> A value of zero (0) indicates that the instance waits indefinitely.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|Format|*Usage*<br /> Optional, write-only `String` property<br /><br /> *Description*<br /> Determines the type of result set that is returned from the `Discover` and `Execute` methods. This property can have the values listed in the following table.|  
  
 The default value for this property is *Native*.  
  
 This property can be used with the `Discover` and `Execute` methods.  
  
|Value|Description|  
|-----------|-----------------|  
|*Tabular*|Returns a result set using the [Rowset](../xml-data-types/rowset-data-type-xmla.md) data type.|  
|*Multidimensional*|Returns a rowset using the [MDDataSet](../xml-data-types/mddataset-data-type-xmla.md) data type.|  
|*Native*|No format is explicitly specified. [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] returns the appropriate format for the command. The actual result type is identified by the namespace of the result.|  
  
|Name|Element|  
|----------|-------------|  
|ImpactAnalysis|*Usage*<br /> Optional, write-only `Boolean` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|LocaleIdentifier|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Reads or sets the locale identifier (LCID) used by the `Discover` or `Execute` method. For the complete hexadecimal list of language identifiers, look for "Language Identifiers" in the MSDN Library.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MaximumRows|*Usage*<br /> Optional, write-only `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropAggregateCellUpdate|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_AGGREGATECELL_UPDATE.<br /><br /> The default value for this property is 4, equivalent to MDPROPVAL_AU_SUPPORTED.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropAxes|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_AXES.<br /><br /> The default value for this property is 2147483647.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropDrillFunctions|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> Determines the level of support for drill functions on the server. The following values are used to build a valid bitmask:<br /><br /> MDPROPVAL_MDF_BASIC (0x01)<br /><br /> MDPROPVAL_MDF_ASYMMETRIC (0x02)<br /><br /> MDPROPVAL_MDF_CALC_MEMBERS (0x04)<br /><br /> The default values are:<br /><br /> 3 for SQL Server 2008<br /><br /> 7 for SQL Server 2008 R2 and [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropFlatteningSupport|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_FLATTENING_SUPPORT.<br /><br /> The default value for this property is 1, equivalent to MDPROPVAL_FS_FULL_SUPPORT.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxCaseSupport|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_CASESUPPORT.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxDescFlags|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_DESCFLAGS.<br /><br /> The default value for this property is 7, equivalent to MDPROPVAL_MD_BEFORE, MDPROPVAL_MD_AFTER, and MDPROPVAL_MD_SELF.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxFormulas|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property MDPROP_MDX_FORMULAS.<br /><br /> The default value for this property is 63, equivalent to a combination of MDPROPVAL_MF_WITH_CALCMEMBERS, MDPROPVAL_MF_WITH_NAMEDSETS, MDPROPVAL_MF_CREATE_CALCMEMBERS, MDPROPVAL_MF_CREATE_NAMEDSETS, MDPROPVAL_MF_SCOPE_SESSION, and MDPROPVAL_MF_SCOPE_GLOBAL.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxJoinCubes|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_JOINCUBES.<br /><br /> The default value for this property is 1, equivalent to MDPROPVAL_MJC_SINGLECUBE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxMemberFunctions|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_MEMBER_FUNCTIONS.<br /><br /> The default value for this property is 15, equivalent to a combination of all available OLE DB values.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxNamedSets|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> A bitmask from values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*0x01*|MDPROPVAL_MNS_BASIC.|  
|*0x02*|MDPROPVAL_MNS_DYNAMIC.|  
|*0x04*|MDPROPVAL_MNS_DISPLAYFOLDER.|  
|*0x08*|MDPROPVAL_MNS_CAPTION.|  
  
 A value of 15 is the default value for this property.  
  
|Name|Element|  
|----------|-------------|  
|MdpropMdxNonMeasureExpressions|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_NONMEASURE_EXPRESSIONS.<br /><br /> The default value for this property is zero (0), equivalent to MDPROPVAL_NME_ALLDIMENSIONS.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxNumericFunctions|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_NUMERIC_FUNCTIONS.<br /><br /> The default value for this property is 2047, equivalent to a combination of MDPROPVAL_MNF_MEDIAN, MDPROPVAL_MNF_VAR, MDPROPVAL_MNF_STDDEV, MDPROPVAL_MNF_RANK, MDPROPVAL_MNF_AGGREGATE, MDPROPVAL_MNF_COVARIANCE, MDPROPVAL_MNF_CORRELATION, MDPROPVAL_MNF_LINREGSLOPE, MDPROPVAL_MNF_LINREGVARIANCE, MDPROPVAL_MNF_LINREG2, and MDPROPVAL_MNF_LINREGPOINT.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxObjQualification|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_OBJQUALIFICATION.<br /><br /> The default value for this property is 496, equivalent to a combination of MDPROPVAL_MOQ_DIM_HIER, MDPROPVAL_MOQ_DIMHIER_LEVEL, MDPROPVAL_MOQ_DIMHIER_MEMBER, MDPROPVAL_MOQ_LEVEL_MEMBER, and MDPROPVAL_MOQ_MEMBER_MEMBER.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxOuterReference|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property MDPROP_MDX_OUTERREFERENCE.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxQueryByProperty|*Usage*<br /> Optional, read-only `Boolean` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_QUERYBYPROPERTY.<br /><br /> The default value for this property is TRUE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxRangeRowset|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_RANGEROWSET.<br /><br /> The default value for this property is 4, equivalent to MDPROPVAL_RR_UPDATE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxSetFunctions|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_SET_FUNCTIONS.<br /><br /> The default value for this property is 524287, equivalent to a combination of MDPROPVAL_MSF_TOPPERCENT, MDPROPVAL_MSF_BOTTOMPERCENT, MDPROPVAL_MSF_TOPSUM, MDPROPVAL_MSF_BOTTOMSUM, MDPROPVAL_MSF_PERIODSTODATE, MDPROPVAL_MSF_LASTPERIODS, MDPROPVAL_MSF_YTD, MDPROPVAL_MSF_QTD, MDPROPVAL_MSF_MTD, MDPROPVAL_MSF_WTD, MDPROPVAL_MSF_DRILLDOWNMEMBER, MDPROPVAL_MSF_DRILLDOWNLEVEL, MDPROPVAL_MSF_DRILLDOWNMEMBERTOP, MDPROPVAL_MSF_DRILLDOWNMEMBERBOTTOM, MDPROPVAL_MSF_DRILLDOWNLEVEL, MDPROPVAL_MSF_DRILLDOWNLEVELTOP, MDPROPVAL_MSF_DRILLDOWNLEVELBOTTOM, MDPROPVAL_MSF_DRILLUPMEMBER, MDPROPVAL_MSF_DRILLUPLEVEL, and MDPROPVAL_MSF_TOGGLEDRILLSTATE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxSlicer|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_SLICER.<br /><br /> The default value for this property is 2, equivalent to MDPROPVAL_MS_SINGLETUPLE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxStringCompop|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_MDX_STRING_COMPOP.<br /><br /> The default value for this property is 15, equivalent to a combination of MDPROPVAL_MSC_LESSTHAN, MDPROPVAL_MSC_GREATERTHAN, MDPROPVAL_MSC_LESSTHANEQUAL, and MDPROPVAL_MSC_GREATERTHANEQUAL.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdpropMdxSubQueries|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> A value of 63 is the default value for this property in SQL Server 2014.<br /><br /> A value of 31 is the default value for this property in SQL Server 2008 R2 and [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]<br /><br /> A value of 15 is the default value for this property in SQL Server 2008<br /><br /> Indicates the level of support for subqueries in MDX. A bitmask from values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*0x01*|MDPROPVAL_MSQ_BASIC.|  
|*0x02*|MDPROPVAL_MSQ_ARBITRARYSHAPE.|  
|*0x04*|MDPROPVAL_MSQ_NONVISUAL.|  
|*0x08*|MDPROPVAL_MSQ_CALCMEMBERS.|  
  
|||  
|-|-|  
|*0x10*|MDPROPVAL_MSQ_CALCMEMBERS2|  
  
|Name|Element|  
|----------|-------------|  
|MdpropNamedLevels|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_NAMED_LEVELS.<br /><br /> The default value for this property is 3, equivalent to a combination of MDPROPVAL_NL_NAMEDLEVELS and MDPROPVAL_NL_NUMBEREDLEVELS.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|MdxMissingMemberMode|*Usage*<br /> Optional, write-only `String` property<br /><br /> *Description*<br /> Indicates whether missing members are ignored in MDX statements.<br /><br /> This property is equivalent to the OLE DB property, DBPROP_MDX_MISSING_MEMBER_MODE.<br /><br /> The default value for this property is *Default*.<br /><br /> This property can be used with the `Discover` and `Execute` methods.<br /><br /> This property can have the values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*Default*|Use value generated by the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.|  
|*Error*|Generate an error.|  
|*Ignore*|Always ignore missing members.|  
  
|Name|Element|  
|----------|-------------|  
|MDXSupport|*Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> Specifies an enumeration that describes the degree of MDX support.<br /><br /> `Value` = *Core* All MDX options are supported.<br /><br /> Currently, the only value that the enumeration contains is *Core*. In future releases, other values will be defined for this enumeration.<br /><br /> The default value for this property is *Core*.<br /><br /> This property can be used with the `Discover` method.|  
|NonEmptyThreshold|*Usage*<br /> Optional read/write `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|Password|**This property is no longer supported.**<br /><br /> *Usage*<br /> Optional write-only `String` property<br /><br /> *Description*<br /> For backward compatibility, this property is ignored without generating an error when used with the `Execute` or `Discover` method.|  
|ProviderName|*Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_DBMSNAME.<br /><br /> The default value for this property is "OLAP Server".<br /><br /> This property can be used with the `Discover` method.|  
|ProviderType|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_DATASOURCE_TYPE.<br /><br /> The default value for this property is 6.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|ProviderVersion|*Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_DBMSVER.<br /><br /> The default value for this property is the version of the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.<br /><br /> This property can be used with the `Discover` method.|  
|ReadOnlySession|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|RealTimeOlap|*Usage*<br /> Optional, read/write `Boolean` property<br /><br /> *Description*<br /> When set to TRUE, indicates that all the partitions listening for table notifications are to be queried in real time, bypassing caching. This property is equivalent to the OLE DB property, DBPROP_MSMD_REAL_TIME_OLAP.<br /><br /> The default value for this property is FALSE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|ReturnCellProperties|*Usage*<br /> Optional, read/write `Boolean` property<br /><br /> *Description*<br /> The default value for this property is FALSE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|Roles|*Usage*<br /> Optional, read/write `String` property<br /><br /> *Description*<br /> Specifies a comma-delimited string of the role names under which a client application connects to an [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance. This property lets the user connect using a role other than the one he or she is currently using. For example, a server administrator may want to connect to a cube as a member of a role to test permissions granted to that role. This user must be a member of the role specified in order to connect using this property.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.<br /><br /> **Note!** Role names are case-sensitive. **Do not use** spaces between the comma-delimited role names. Otherwise errors and unexpected results may be returned by queries to secured cell sets.|  
|SafetyOptions|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Determines whether unsafe libraries can be registered and loaded by client applications.<br /><br /> The value of this property also determines whether the PASSTHROUGH keyword is allowed in local cubes. An error occurs in the following situations:<br /><br /> -   If a client application tries to create a local cube with an INSERT INTO statement that contains the PASSTHROUGH keyword.<br />-   If a client application tries to update a local cube that contains an INSERT INTO statement which uses the PASSTHROUGH keyword.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.<br /><br /> This property can have the values listed in the following table.|  
  
|Name|Value|Description|  
|----------|-----------|-----------------|  
|DBPROPVAL_MSMD_SAFETY_OPTIONS_DEFAULT|*0*|This value is treated as DBPROPVAL_MSMD_SAFETY_OPTIONS_ALLOW_SAFE.<br /><br /> For connections to a local cube, this value depends on whether the CREATECUBE connection string property is used. If the CREATECUBE connection string property is used, this value is the same as DBPROPVAL_MSMD_SAFETY_OPTIONS_ALLOW_ALL. Otherwise, this value is the same as DBPROPVAL_MSMD_SAFETY_OPTIONS_ALLOW_SAFE.|  
|DBPROPVAL_MSMD_SAFETY_OPTIONS_ALLOW_ALL|*1*|This value enables all user-defined function libraries without verifying that they are safe for initialization and scripting. For connections to local cubes, this value enables usage of stored procedures and of the PASSTHROUGH keyword in INSERT INTO statements.<br /><br /> **We do not recommend this option**.|  
|DBPROPVAL_MSMD_SAFETY_OPTIONS_ALLOW_SAFE|*2*|This value makes sure that all classes for a particular user-defined function library are checked to make sure that they are safe for initialization and scripting. For connections to local cubes, this value prevents usage of the PASSTHROUGH keyword in INSERT INTO statements and of stored procedures where the PermissionSet property is not set to Safe.<br /><br /> This value also removes actions in the [MDSCHEMA_ACTIONS](../../schema-rowsets/ole-db-olap/mdschema-actions-rowset.md) schema rowset that have either a value of HTML or COMMAND in the ACTION_TYPE column, or have a value of URL in the ACTION_TYPE column and a value in the CONTENT column that does not start with "http://" or "https://".|  
|DBPROPVAL_MSMD_SAFETY_OPTIONS_ALLOW_NONE|*3*|This value prevents user-defined functions from being used during the session. For connections to local cubes, this value prevents usage of all stored procedures and of the PASSTHROUGH keyword in INSERT INTO statements.<br /><br /> This value also removes all actions in the MDSCHEMA_ACTIONS schema rowset.|  
  
|Name|Element|  
|----------|-------------|  
|SecuredCellValue|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Specifies the error code and the values for the `Value` and `Formatted Value` cell properties to be returned when it tries to access a secured cell.<br /><br /> This property can be used with the `Discover` and `Execute` methods.<br /><br /> This property can have the values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*0*|(Default) For compatibility with earlier versions, this value is the same as *1*. The meaning of this default value is subject to change in future versions.|  
|*1*|Returns: HRESULT = NO_ERROR<br /><br /> The `Value` property of the cell contains the result as a variant data type. The string "#N/A" is returned in the `Formatted Value` property.|  
|*2*|Returns an error as the value of HRESULT.|  
|*3*|Returns NULL in both the `Value` and `Formatted Value` properties.|  
|*4*|Returns a numeric zero (0) in the `Value` property, and returns a formatted zero in the `Formatted Value` property. For example, 0.00 is returned in the `Formatted Value` property for a cell whose `Format` property is "#.##".|  
|*5*|Returns the string "#SEC" in both the `Value` and `Formatted Value` properties.|  
  
|Name|Element|  
|----------|-------------|  
|ServerName|*Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, DBPROP_SERVERNAME.<br /><br /> The default value for this property is the name of the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|ShowHiddenCubes|*Usage*<br /> Optional, read/write `Boolean` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> The default value for this property is FALSE.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|SQLQueryMode|*Usage*<br /> Optional, read/write `String` property<br /><br /> *Description*<br /> Determines whether calculations are included in SQL queries.<br /><br /> The default value for this property is *Calculated*.<br /><br /> This property can be used with the `Discover` and `Execute` methods.<br /><br /> This property can have the values listed in the following table.|  
  
|Value|Description|  
|-----------|-----------------|  
|*Data*|No calculations are included.|  
|*Calculated*|Calculations are returned.|  
|*IncludeEmpty*|Calculations and empty rows are returned.|  
  
|Name|Element|  
|----------|-------------|  
|SQLSupport|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> The default value for this property is 512.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|SspropInitAppName|*Usage*<br /> Optional, read/write `String` property<br /><br /> *Description*<br /> Contains the name of the client application.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|SspropInitPacketsize|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Contains the ID of the client application.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|SspropInitWsid|*Usage*<br /> Optional, read/write `String` property<br /><br /> *Description*<br /> Contains the ID of the client workstation.<br /><br /> There is no default value for this property.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|StateSupport|*Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> Specifies the degree of support for statefulness.<br /><br /> *None* = <br />                        Statefulness is not supported.<br /><br /> *Sessions* = Statefulness is provided through session support.<br /><br /> For more information about statefulness and session support, see [Managing Connections and Sessions &#40;XMLA&#41;](../../multidimensional-models-scripting-language-assl-xmla/managing-connections-and-sessions-xmla.md).<br /><br /> The default value for this property is *Sessions*.<br /><br /> This property can be used with the `Discover` method.|  
|Timeout|*Usage*<br /> Optional, read/write `Integer` property<br /><br /> *Description*<br /> Specifies, in seconds, the maximum time that the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance should wait for a request to be successful before returning an error. This property also determines the maximum time that the instance should wait for an update to a writeback table to be successful before returning an error, equivalent to the connection string property, Writeback Timeout.<br /><br /> The default value for this property is zero (0).<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|TransactionDDL|*Usage*<br /> Optional, read-only `Integer` property<br /><br /> *Description*<br /> Reserved for future use.<br /><br /> The default value for this property is 0.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
|UserName|This property is no longer supported.<br /><br /> *Usage*<br /> Optional, read-only `String` property<br /><br /> *Description*<br /> Specifies a string that returns the user name that the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] instance associates with the command. For backward compatibility, this property is ignored without generating an error when used with the `Execute` or `Discover` method. This property is equivalent to the OLE DB property, DBPROP_USERNAME.<br /><br /> The default value for this property is the user name that opened the current session or connection.<br /><br /> This property can be used with the `Execute` method.|  
|VisualMode|*Usage*<br /> Optional, write-only `Integer` property<br /><br /> *Description*<br /> This property is equivalent to the OLE DB property, MDPROP_VISUALMODE.<br /><br /> The default value for this property is zero (0), equivalent to DBPROPVAL_VISUAL_MODE_DEFAULT.<br /><br /> This property can be used with the `Discover` and `Execute` methods.|  
  
## See Also  
 [PropertyList Element &#40;XMLA&#41;](propertylist-element-xmla.md)  
  
  
