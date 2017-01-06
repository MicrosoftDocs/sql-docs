---
title: "OBJECTPROPERTYEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 50c42ecb-d36a-4bdf-b8c0-5a90c677e3c2
caps.latest.revision: 13
author: BarbKess
---
# OBJECTPROPERTYEX (SQL Server PDW)
Returns information about schema-scoped objects in the current database. For a list of these objects, see [sys.objects &#40;SQL Server PDW&#41;](../sqlpdw/sys-objects-sql-server-pdw.md). For more information, see the [OBJECTPROPERTYEX (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms188390.aspx) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
OBJECTPROPERTYEX ( id , property )  
```  
  
## Arguments  
*id*  
The ID of the object in the current database. *id* is **int** and is assumed to be a schema-scoped object in the current database context.  
  
*property*  
The information to be returned for the object specified by id. The return type is **sql_variant**. The following table shows the base data type for each property value.  
  
> [!NOTE]  
> Unless noted otherwise, NULL is returned when *property* is not a valid property name, *id* is not a valid object ID, *id* is an unsupported object type for the specified *property*, or the caller does not have permission to view the object's metadata.  
  
|Property name|Object type|Description and values returned|  
|-----------------|---------------|-----------------------------------|  
|BaseType|Any schema-scoped object|Identifies the base type of the object. When the specified object is a SYNONYM, the base type of the underlying object is returned.<br /><br />Nonnull = Object type<br /><br />Base data type: **char(2)**|  
|CnstIsClustKey|Constraint|PRIMARY KEY constraint with a clustered index.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|CnstIsColumn|Constraint|CHECK, DEFAULT, or FOREIGN KEY constraint on a single column.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|CnstIsDeleteCascade|Constraint|FOREIGN KEY constraint with the ON DELETE CASCADE option.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|CnstIsDisabled|Constraint|Disabled constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|CnstIsNonclustKey|Constraint|PRIMARY KEY constraint with a nonclustered index.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|CnstIsNotRepl|Constraint|Constraint is defined by using the NOT FOR REPLICATION keywords.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|CnstIsNotTrusted|Constraint|Constraint was enabled without checking existing rows. Therefore, the constraint may not hold for all rows.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|CnstIsUpdateCascade|Constraint|FOREIGN KEY constraint with the ON UPDATE CASCADE option.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsAfterTrigger|Trigger|AFTER trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsAnsiNullsOn|Transact\-SQL function, Transact\-SQL procedure, Transact\-SQL trigger, view|The setting of ANSI_NULLS at creation time.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsDeleteTrigger|Trigger|DELETE trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsFirstDeleteTrigger|Trigger|The first trigger fired when a DELETE is executed against the table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsFirstInsertTrigger|Trigger|The first trigger fired when an INSERT is executed against the table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsFirstUpdateTrigger|Trigger|The first trigger fired when an UPDATE is executed against the table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsInsertTrigger|Trigger|INSERT trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsInsteadOfTrigger|Trigger|INSTEAD OF trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsLastDeleteTrigger|Trigger|Last trigger fired when a DELETE is executed against the table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsLastInsertTrigger|Trigger|Last trigger fired when an INSERT is executed against the table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsLastUpdateTrigger|Trigger|Last trigger fired when an UPDATE is executed against the table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsQuotedIdentOn|Transact\-SQL function, Transact\-SQL procedure, Transact\-SQL trigger, view|Setting of QUOTED_IDENTIFIER at creation time.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsStartup|Procedure|Startup procedure.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsTriggerDisabled|Trigger|Disabled trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsTriggerNotForRepl|Trigger|Trigger defined as NOT FOR REPLICATION.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|ExecIsUpdateTrigger|Trigger|UPDATE trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|HasAfterTrigger|Table, view|Table or view has an AFTER trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|HasDeleteTrigger|Table, view|Table or view has a DELETE trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|HasInsertTrigger|Table, view|Table or view has an INSERT trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|HasInsteadOfTrigger|Table, view|Table or view has an INSTEAD OF trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|HasUpdateTrigger|Table, view|Table or view has an UPDATE trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsAnsiNullsOn|Transact\-SQL function, Transact\-SQL procedure, table, Transact\-SQL trigger, view|Specifies that the ANSI NULLS option setting for the table is ON, meaning all comparisons against a null value evaluate to UNKNOWN. This setting applies to all expressions in the table definition, including computed columns and constraints, for as long as the table exists.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsCheckCnst|Any schema-scoped object|CHECK constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsConstraint|Any schema-scoped object|Constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsDefault|Any schema-scoped object|Bound default.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsDefaultCnst|Any schema-scoped object|DEFAULT constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsDeterministic|Scalar and table-valued functions, view|The determinism property of the function or view.<br /><br />1 = Deterministic<br /><br />0 = Not Deterministic<br /><br />Base data type: **int**|  
|IsEncrypted|Transact\-SQL function, Transact\-SQL procedure, table, Transact\-SQL trigger, view|Indicates that the original text of the module statement was converted to an obfuscated format. The output of the obfuscation is not directly visible in any of the catalog views in SQL Server 2005. Users without access to system tables or database files cannot retrieve the obfuscated text. However, the text is available to users that can either access system tables over the [DAC port](assetId:///993e0820-17f2-4c43-880c-d38290bf7abc) or directly access database files. Also, users that can attach a debugger to the server process can retrieve the original procedure from memory at run time.<br /><br />1 = Encrypted<br /><br />0 = Not encrypted<br /><br />Base data type: **int**|  
|IsExecuted|Any schema-scoped object|Specifies the object can be executed (view, procedure, function, or trigger).<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsExtendedProc|Any schema-scoped object|Extended procedure.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsForeignKey|Any schema-scoped object|FOREIGN KEY constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsIndexed|Table, view|A table or view with an index.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsIndexable|Table, view|A table or view on which an index may be created.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsInlineFunction|Function|Inline function.<br /><br />1 = Inline function<br /><br />0 = Not inline function<br /><br />Base data type: **int**|  
|IsMSShipped|Any schema-scoped object|An object created during installation of SQL Server.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsPrecise|Computed column, function, user-defined type, view|Indicates whether the object contains an imprecise computation, such as floating point operations.<br /><br />1 = Precise<br /><br />0 = Imprecise<br /><br />Base data type: **int**|  
|IsPrimaryKey|Any schema-scoped object|PRIMARY KEY constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsProcedure|Any schema-scoped object|Procedure.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsQuotedIdentOn|CHECK constraint, DEFAULT definition, Transact\-SQL function, Transact\-SQL procedure, table, Transact\-SQL trigger, view|Specifies that the quoted identifier setting for the object is ON, meaning double quotation marks delimit identifiers in all expressions involved in the object definition.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsQueue|Any schema-scoped object|Service Broker Queue<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsReplProc|Any schema-scoped object|Replication procedure.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsRule|Any schema-scoped object|Bound rule.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsScalarFunction|Function|Scalar-valued function.<br /><br />1 = Scalar-valued function<br /><br />0 = Not scalar-valued function<br /><br />Base data type: **int**|  
|IsSchemaBound|Function, view|A schema bound function or view created by using SCHEMABINDING.<br /><br />1 = Schema-bound<br /><br />0 = Not schema-bound<br /><br />Base data type: **int**|  
|IsSystemTable|Table|System table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsSystemVerified|Computed column, function, user-defined type, view|The precision and determinism properties of the object can be verified by SQL Server.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsTable|Table|Table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsTableFunction|Function|Table-valued function.<br /><br />1 = Table-valued function<br /><br />0 = Not table-valued function<br /><br />Base data type: **int**|  
|IsTrigger|Any schema-scoped object|Trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsUniqueCnst|Any schema-scoped object|UNIQUE constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsUserTable|Table|User-defined table.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|IsView|View|View.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|OwnerId|Any schema-scoped object|Owner of the object.<br /><br />**Note:** The schema owner is not necessarily the object owner. For example, child objects (those where *parent_object_id* is nonnull) will always return the same owner ID as the parent.<br /><br />Nonnull = Database user ID of the object owner.<br /><br />NULL = Unsupported object type, or object ID is not valid.<br /><br />Base data type: **int**|  
|SchemaId|Any schema-scoped object|The ID of the schema associated with the object.<br /><br />Nonnull = Schema ID of the object.<br /><br />Base data type: **int**|  
|SystemDataAccess|Function, view|Object accesses system data, system catalogs or virtual system tables, in the local instance of SQL Server.<br /><br />0 = None<br /><br />1 = Read<br /><br />Base data type: **int**|  
|TableDeleteTrigger|Table|Table has a DELETE trigger.<br /><br />>1 = ID of first trigger with the specified type.<br /><br />Base data type: **int**|  
|TableDeleteTriggerCount|Table|The table has the specified number of DELETE triggers.<br /><br />Nonnull = Number of DELETE triggers<br /><br />Base data type: **int**|  
|TableFullTextMergeStatus|Table|Whether a table that has a full-text index that is currently in merging.<br /><br />0 = Table does not have a full-text index, or the full-text index is not in merging.<br /><br />1 = The full-text index is in merging.|  
|TableFullTextBackgroundUpdateIndexOn|Table|The table has full-text background update index (autochange tracking) enabled.<br /><br />1 = TRUE<br /><br />0 = FALSE<br /><br />Base data type: **int**|  
|TableFulltextCatalogId|Table|ID of the full-text catalog in which the full-text index data for the table resides.<br /><br />Nonzero = Full-text catalog ID, associated with the unique index that identifies the rows in a full-text indexed table.<br /><br />0 = Table does not have a full-text index.<br /><br />Base data type: **int**|  
|TableFullTextChangeTrackingOn|Table|Table has full-text change-tracking enabled.<br /><br />1 = TRUE<br /><br />0 = FALSE<br /><br />Base data type: **int**|  
|TableFulltextDocsProcessed|Table|Number of rows processed since the start of full-text indexing. In a table that is being indexed for full-text search, all the columns of one row are considered as part of one document to be indexed.<br /><br />0 = No active crawl or full-text indexing is completed.<br /><br />> 0 = One of the following: Either the number of documents processed by insert or update operations since the start of full, incremental, or manual change tracking population., or the number of rows processed by insert or update operations since change tracking with background update index population was enabled, the full-text index schema changed, the full-text catalog rebuilt, or the instance of SQL Server restarted, and so on.<br /><br />NULL = Table does not have a full-text index.<br /><br />Base data type: **int**<br /><br />**Note** This property does not monitor or count deleted rows.|  
|TableFulltextFailCount|Table|The number of rows that full-text search did not index.<br /><br />0 = The population has completed.<br /><br />>0 = One of the following: Either the number of documents that were not indexed since the start of Full, Incremental, and Manual Update change tracking population, or for change tracking with background update index, the number of rows that were not indexed since the start of the population, or the restart of the population. This could be caused by a schema change, rebuild of the catalog, server restart, and so on.<br /><br />NULL = Table does not have a Full-Text index.<br /><br />Base data type: **int**|  
|TableFulltextItemCount|Table|Nonnull = Number of rows that were full-text indexed successfully.<br /><br />NULL = Table does not have a full-text index.<br /><br />Base data type: **int**|  
|TableFulltextKeyColumn|Table|ID of the column associated with the single-column unique index that is part of the definition of a full-text index and semantic index.<br /><br />0 = Table does not have a full-text index.<br /><br />Base data type: **int**|  
|TableFulltextPendingChanges|Table|Number of pending change tracking entries to process.<br /><br />0 = change tracking is not enabled.<br /><br />NULL = Table does not have a full-text index.<br /><br />Base data type: **int**|  
|TableFulltextPopulateStatus|Table|0 = Idle.<br /><br />1 = Full population is in progress.<br /><br />2 = Incremental population is in progress.<br /><br />3 = Propagation of tracked changes is in progress.<br /><br />4 = Background update index is in progress, such as autochange tracking.<br /><br />5 = Full-text indexing is throttled or paused.<br /><br />Base data type: **int**|  
|TableFullTextSemanticExtraction|Table|Table is enabled for semantic indexing.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasActiveFulltextIndex|Table|Table has an active full-text index.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasCheckCnst|Table|Table has a CHECK constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasClustIndex|Table|Table has a clustered index.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasDefaultCnst|Table|Table has a DEFAULT constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasDeleteTrigger|Table|Table has a DELETE trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasForeignKey|Table|Table has a FOREIGN KEY constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasForeignRef|Table|Table is referenced by a FOREIGN KEY constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasIdentity|Table|Table has an identity column.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasIndex|Table|Table has an index of any type.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasInsertTrigger|Table|Object has an INSERT trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasNonclustIndex|Table|The table has a nonclustered index.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasPrimaryKey|Table|Table has a primary key.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasRowGuidCol|Table|Table has a ROWGUIDCOL for a **uniqueidentifier** column.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasTextImage|Table|Table has a **text**, **ntext**, or **image** column.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasTimestamp|Table|Table has a **timestamp** column.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasUniqueCnst|Table|Table has a UNIQUE constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasUpdateTrigger|Table|The object has an UPDATE trigger.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableHasVarDecimalStorageFormat|Table|Table is enabled for **vardecimal** storage format.<br /><br />1 = True<br /><br />0 = False|  
|TableInsertTrigger|Table|Table has an INSERT trigger.<br /><br />>1 = ID of first trigger with the specified type.<br /><br />Base data type: **int**|  
|TableInsertTriggerCount|Table|The table has the specified number of INSERT triggers.<br /><br />>0 = The number of INSERT triggers.<br /><br />Base data type: **int**|  
|TableIsFake|Table|Table is not real. It is materialized internally on demand by the Database Engine.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**|  
|TableIsLockedOnBulkLoad|Table|Table is locked because a **bcp** or BULK INSERT job.<br /><br />1 = True<br /><br />0 = False<br /><br />Base data type: **int**<br /><br />Not supported in SQL Server PDW.|  
|TableIsPinned|Table|Table is pinned to be held in the data cache.<br /><br />0 = False<br /><br />This feature is not supported in SQL Server 2005 and later versions.|  
|TableTextInRowLimit|Table|Table has text in row option set.<br /><br />> 0 = Maximum bytes allowed for text in row.<br /><br />0 = text in row option is not set.<br /><br />Base data type: **int**|  
|TableUpdateTrigger|Table|Table has an UPDATE trigger.<br /><br />> 1 = ID of first trigger with the specified type.<br /><br />Base data type: **int**|  
|TableUpdateTriggerCount|Table|Table has the specified number of UPDATE triggers.<br /><br />> 0 = The number of UPDATE triggers.<br /><br />Base data type: **int**|  
|UserDataAccess|Function, View|Indicates the object accesses user data, user tables, in the local instance of SQL Server.<br /><br />1 = Read<br /><br />0 = None<br /><br />Base data type: **int**|  
|TableHasColumnSet|Table|Table has a column set.<br /><br />0 = False<br /><br />1 = True<br /><br />For more information, see [Using Column sets](http://msdn.microsoft.com/en-us/library/cc280521.aspx).|  
|Cardinality|Table (system or user-defined), view, or index|The number of rows in the specified object. Not supported in SQL Server PDW.|  
  
## Return Types  
**sql_variant**  
  
## Error Handling  
Returns NULL on error or if a caller does not have permission to view the object.  
  
Returns an error if the property is valid in SQL Server but not valid for SQL Server PDW.  
  
A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECTPROPERTYEX may return NULL if the user does not have any permission on the object.  
  
Properties that are valid but not applicable to SQL Server PDW such as properties related to triggers, always return 0.  
  
## General Remarks  
The SQL Server PDW assumes that *object_id* is in the current database context. A query that references an *object_id* in another database will return NULL or incorrect results.  
  
OBJECTPROPERTYEX(*view_id*, 'IsIndexable') may consume significant computer resources because evaluation of IsIndexable property requires the parsing of view definition, normalization, and partial optimization  
  
OBJECTPROPERTYEX (*table_id*, 'TableHasActiveFulltextIndex') will return a value of 1 (true) when at least one column of a table is added for indexing. Full-text indexing becomes active for population as soon as the first column is added for indexing.  
  
## Examples  
The following example creates returns the base type of `dbo.DimReseller` object.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT OBJECTPROPERTYEX ( object_id(N'dbo.DimReseller'), N'BaseType')AS BaseType;  
```  
  
The result set shows that the base type of the underlying object, the `dbo.DimReseller` table, is a user table.  
  
```  
BaseType   
--------   
U  
```  
  
