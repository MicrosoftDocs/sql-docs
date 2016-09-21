---
title: "OBJECTPROPERTY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 721b3610-871a-4ba3-a3c1-6dcb8e56bd8f
caps.latest.revision: 13
author: BarbKess
---
# OBJECTPROPERTY (SQL Server PDW)
Returns information about schema-scoped objects in the current database. For a list of schema-scoped objects, see [sys.objects &#40;SQL Server PDW&#41;](../sqlpdw/sys-objects-sql-server-pdw.md). For more information, see the [OBJECTPROPERTY (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms176105.aspx) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
OBJECTPROPERTY ( id , property )  
```  
  
## Arguments  
*id*  
An expression that represents the ID of the object in the current database. *id* is **int** and is assumed to be a schema-scoped object in the current database context.  
  
*property*  
An expression that represents the information to be returned for the object specified by *id*. *property* can be one of the following values.  
  
> [!NOTE]  
> Unless noted otherwise, NULL is returned when *property* is not a valid property name, *id* is not a valid object ID, *id* is an unsupported object type for the specified *property*, or the caller does not have permission to view the object's metadata.  
  
|Property name|Object type|Description and values returned|  
|-----------------|---------------|-----------------------------------|  
|CnstIsClustKey|Constraint|PRIMARY KEY constraint with a clustered index.<br /><br />1 = True<br /><br />0 = False|  
|CnstIsColumn|Constraint|CHECK, DEFAULT, or FOREIGN KEY constraint on a single column.<br /><br />1 = True<br /><br />0 = False|  
|CnstIsDeleteCascade|Constraint|FOREIGN KEY constraint with the ON DELETE CASCADE option.<br /><br />1 = True<br /><br />0 = False|  
|CnstIsDisabled|Constraint|Disabled constraint.<br /><br />1 = True<br /><br />0 = False|  
|CnstIsNonclustKey|Constraint|PRIMARY KEY or UNIQUE constraint with a nonclustered index.<br /><br />1 = True<br /><br />0 = False|  
|CnstIsNotRepl|Constraint|Constraint is defined by using the NOT FOR REPLICATION keywords.<br /><br />1 = True<br /><br />0 = False|  
|CnstIsNotTrusted|Constraint|Constraint was enabled without checking existing rows; therefore, the constraint may not hold for all rows.<br /><br />1 = True<br /><br />0 = False|  
|CnstIsUpdateCascade|Constraint|FOREIGN KEY constraint with the ON UPDATE CASCADE option.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsAfterTrigger|Trigger|AFTER trigger.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsAnsiNullsOn|Transact\-SQL function, Transact\-SQL procedure, Transact\-SQL trigger, view|Setting of ANSI_NULLS at creation time.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsDeleteTrigger|Trigger|DELETE trigger.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsFirstDeleteTrigger|Trigger|First trigger fired when a DELETE is executed against the table.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsFirstInsertTrigger|Trigger|First trigger fired when an INSERT is executed against the table.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsFirstUpdateTrigger|Trigger|First trigger fired when an UPDATE is executed against the table.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsInsertTrigger|Trigger|INSERT trigger.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsInsteadOfTrigger|Trigger|INSTEAD OF trigger.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsLastDeleteTrigger|Trigger|Last trigger fired when a DELETE is executed against the table.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsLastInsertTrigger|Trigger|Last trigger fired when an INSERT is executed against the table.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsLastUpdateTrigger|Trigger|Last trigger fired when an UPDATE is executed against the table.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsQuotedIdentOn|Transact\-SQL function, Transact\-SQL procedure, Transact\-SQL trigger, view|Setting of QUOTED_IDENTIFIER at creation time.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsStartup|Procedure|Startup procedure.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsTriggerDisabled|Trigger|Disabled trigger.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsTriggerNotForRepl|Trigger|Trigger defined as NOT FOR REPLICATION.<br /><br />1 = True<br /><br />0 = False|  
|ExecIsUpdateTrigger|Trigger|UPDATE trigger.<br /><br />1 = True<br /><br />0 = False|  
|HasAfterTrigger|Table, view|Table or view has an AFTER trigger.<br /><br />1 = True<br /><br />0 = False|  
|HasDeleteTrigger|Table, view|Table or view has a DELETE trigger.<br /><br />1 = True<br /><br />0 = False|  
|HasInsertTrigger|Table, view|Table or view has an INSERT trigger.<br /><br />1 = True<br /><br />0 = False|  
|HasInsteadOfTrigger|Table, view|Table or view has an INSTEAD OF trigger.<br /><br />1 = True<br /><br />0 = False|  
|HasUpdateTrigger|Table, view|Table or view has an UPDATE trigger.<br /><br />1 = True<br /><br />0 = False|  
|IsAnsiNullsOn|Transact\-SQL function, Transact\-SQL procedure, table, Transact\-SQL trigger, view|Specifies that the ANSI NULLS option setting for the table is ON. This means all comparisons against a null value evaluate to UNKNOWN. This setting applies to all expressions in the table definition, including computed columns and constraints, for as long as the table exists.<br /><br />1 = True<br /><br />0 = False|  
|IsCheckCnst|Any schema-scoped object|CHECK constraint.<br /><br />1 = True<br /><br />0 = False|  
|IsConstraint|Any schema-scoped object|Is a single column CHECK, DEFAULT, or FOREIGN KEY constraint on a column or table.<br /><br />1 = True<br /><br />0 = False|  
|IsDefault|Any schema-scoped object|Bound default.<br /><br />1 = True<br /><br />0 = False|  
|IsDefaultCnst|Any schema-scoped object|DEFAULT constraint.<br /><br />1 = True<br /><br />0 = False|  
|IsDeterministic|Function, view|The determinism property of the function or view.<br /><br />1 = Deterministic<br /><br />0 = Not Deterministic|  
|IsEncrypted|Transact\-SQL function, Transact\-SQL procedure, table, Transact\-SQL trigger, view|Indicates that the original text of the module statement was converted to an obfuscated format. The output of the obfuscation is not directly visible in any of the catalog views in SQL Server 2005. Users without access to system tables or database files cannot retrieve the obfuscated text. However, the text is available to users that can either access system tables over the [DAC port](assetId:///993e0820-17f2-4c43-880c-d38290bf7abc) or directly access database files. Also, users that can attach a debugger to the server process can retrieve the original procedure from memory at run time.<br /><br />1 = Encrypted<br /><br />0 = Not encrypted<br /><br />Base data type: **int**|  
|IsExecuted|Any schema-scoped object|Object can be executed (view, procedure, function, or trigger).<br /><br />1 = True<br /><br />0 = False|  
|IsExtendedProc|Any schema-scoped object|Extended procedure.<br /><br />1 = True<br /><br />0 = False|  
|IsForeignKey|Any schema-scoped object|FOREIGN KEY constraint.<br /><br />1 = True<br /><br />0 = False|  
|IsIndexed|Table, view|Table or view that has an index.<br /><br />1 = True<br /><br />0 = False|  
|IsIndexable|Table, view|Table or view on which an index can be created.<br /><br />1 = True<br /><br />0 = False|  
|IsInlineFunction|Function|Inline function.<br /><br />1 = Inline function<br /><br />0 = Not inline function|  
|IsMSShipped|Any schema-scoped object|Object created during installation of SQL Server.<br /><br />1 = True<br /><br />0 = False|  
|IsPrimaryKey|Any schema-scoped object|PRIMARY KEY constraint.<br /><br />1 = True<br /><br />0 = False<br /><br />NULL = Not a function, or object ID is not valid.|  
|IsProcedure|Any schema-scoped object|Procedure.<br /><br />1 = True<br /><br />0 = False|  
|IsQuotedIdentOn|Transact\-SQL function, Transact\-SQL procedure, table, Transact\-SQL trigger, view, CHECK constraint, DEFAULT definition|Specifies that the quoted identifier setting for the object is ON. This means double quotation marks delimit identifiers in all expressions involved in the object definition.<br /><br />1 = ON<br /><br />0 = OFF|  
|IsQueue|Any schema-scoped object|Service Broker Queue<br /><br />1 = True<br /><br />0 = False|  
|IsReplProc|Any schema-scoped object|Replication procedure.<br /><br />1 = True<br /><br />0 = False|  
|IsRule|Any schema-scoped object|Bound rule.<br /><br />1 = True<br /><br />0 = False|  
|IsScalarFunction|Function|Scalar-valued function.<br /><br />1 = Scalar-valued function<br /><br />0 = Not scalar-valued function|  
|IsSchemaBound|Function, view|A schema bound function or view created by using SCHEMABINDING.<br /><br />1 = Schema-bound<br /><br />0 = Not schema-bound.|  
|IsSystemTable|Table|System table.<br /><br />1 = True<br /><br />0 = False|  
|IsTable|Table|Table.<br /><br />1 = True<br /><br />0 = False|  
|IsTableFunction|Function|Table-valued function.<br /><br />1 = Table-valued function<br /><br />0 = Not table-valued function|  
|IsTrigger|Any schema-scoped object|Trigger.<br /><br />1 = True<br /><br />0 = False|  
|IsUniqueCnst|Any schema-scoped object|UNIQUE constraint.<br /><br />1 = True<br /><br />0 = False|  
|IsUserTable|Table|User-defined table.<br /><br />1 = True<br /><br />0 = False|  
|IsView|View|View.<br /><br />1 = True<br /><br />0 = False|  
|OwnerId|Any schema-scoped object|Owner of the object.<br /><br />**Note:** The schema owner is not necessarily the object owner. For example, child objects (those where *parent_object_id* is nonnull) will always return the same owner ID as the parent.<br /><br />Nonnull = The database user ID of the object owner.|  
|TableDeleteTrigger|Table|Table has a DELETE trigger.<br /><br />>1 = ID of first trigger with the specified type.|  
|TableDeleteTriggerCount|Table|Table has the specified number of DELETE triggers.<br /><br />>0 = The number of DELETE triggers.|  
|TableFullTextMergeStatus|Table|Whether a table that has a full-text index that is currently in merging.<br /><br />0 = Table does not have a full-text index, or the full-text index is not in merging.<br /><br />1 = The full-text index is in merging.|  
|TableFullTextBackgroundUpdateIndexOn|Table|Table has full-text background update index (autochange tracking) enabled.<br /><br />1 = TRUE<br /><br />0 = FALSE|  
|TableFulltextCatalogId|Table|ID of the full-text catalog in which the full-text index data for the table resides.<br /><br />Nonzero = Full-text catalog ID, associated with the unique index that identifies the rows in a full-text indexed table.<br /><br />0 = Table does not have a full-text index.|  
|TableFulltextChangeTrackingOn|Table|Table has full-text change-tracking enabled.<br /><br />1 = TRUE<br /><br />0 = FALSE|  
|TableFulltextDocsProcessed|Table|Number of rows processed since the start of full-text indexing. In a table that is being indexed for full-text search, all the columns of one row are considered as part of one document to be indexed.<br /><br />0 = No active crawl or full-text indexing is completed.<br /><br />> 0 = One of the following: Either the number of documents processed by insert or update operations since the start of Full, Incremental, or Manual change tracking population, or the number of rows processed by insert or update operations since change tracking with background update index population was enabled, the full-text index schema changed, the full-text catalog rebuilt, or the instance of SQL Server restarted, and so on.<br /><br />NULL = Table does not have a full-text index.<br /><br />**Note:** This property does not monitor or count deleted rows.|  
|TableFulltextFailCount|Table|Number of rows Full-Text Search did not index.<br /><br />0 = The population has completed.<br /><br />> 0 = One of the following: Either the number of documents that were not indexed since the start of Full, Incremental, and Manual Update change tracking population, or for change tracking with background update index, the number of rows that were not indexed since the start of the population, or the restart of the population. This could be caused by a schema change, rebuild of the catalog, server restart, and so on.<br /><br />NULL = Table does not have a full-text index.|  
|TableFulltextItemCount|Table|Number of rows that were successfully full-text indexed.|  
|TableFulltextKeyColumn|Table|ID of the column associated with the single-column unique index that is participating in the full-text index definition.<br /><br />0 = Table does not have a full-text index.|  
|TableFulltextPendingChanges|Table|Number of pending change tracking entries to process.<br /><br />0 = change tracking is not enabled.<br /><br />NULL = Table does not have a full-text index.|  
|TableFulltextPopulateStatus|Table|0 = Idle.<br /><br />1 = Full population is in progress.<br /><br />2 = Incremental population is in progress.<br /><br />3 = Propagation of tracked changes is in progress.<br /><br />4 = Background update index is in progress, such as autochange tracking.<br /><br />5 = Full-text indexing is throttled or paused.|  
|TableHasActiveFulltextIndex|Table|Table has an active full-text index.<br /><br />1 = True<br /><br />0 = False|  
|TableHasCheckCnst|Table|Table has a CHECK constraint.<br /><br />1 = True<br /><br />0 = False|  
|TableHasClustIndex|Table|Table has a clustered index.<br /><br />1 = True<br /><br />0 = False|  
|TableHasDefaultCnst|Table|Table has a DEFAULT constraint.<br /><br />1 = True<br /><br />0 = False|  
|TableHasDeleteTrigger|Table|Table has a DELETE trigger.<br /><br />1 = True<br /><br />0 = False|  
|TableHasForeignKey|Table|Table has a FOREIGN KEY constraint.<br /><br />1 = True<br /><br />0 = False|  
|TableHasForeignRef|Table|Table is referenced by a FOREIGN KEY constraint.<br /><br />1 = True<br /><br />0 = False|  
|TableHasIdentity|Table|Table has an identity column.<br /><br />1 = True<br /><br />0 = False|  
|TableHasIndex|Table|Table has an index of any type.<br /><br />1 = True<br /><br />0 = False|  
|TableHasInsertTrigger|Table|Object has an INSERT trigger.<br /><br />1 = True<br /><br />0 = False|  
|TableHasNonclustIndex|Table|Table has a nonclustered index.<br /><br />1 = True<br /><br />0 = False|  
|TableHasPrimaryKey|Table|Table has a primary key.<br /><br />1 = True<br /><br />0 = False|  
|TableHasRowGuidCol|Table|Table has a ROWGUIDCOL for a **uniqueidentifier** column.<br /><br />1 = True<br /><br />0 = False|  
|TableHasTextImage|Table|Table has a **text**, **ntext**, or **image** column.<br /><br />1 = True<br /><br />0 = False|  
|TableHasTimestamp|Table|Table has a **timestamp** column.<br /><br />1 = True<br /><br />0 = False|  
|TableHasUniqueCnst|Table|Table has a UNIQUE constraint.<br /><br />1 = True<br /><br />0 = False|  
|TableHasUpdateTrigger|Table|Object has an UPDATE trigger.<br /><br />1 = True<br /><br />0 = False|  
|TableHasVarDecimalStorageFormat|Table|Table is enabled for **vardecimal** storage format.<br /><br />1 = True<br /><br />0 = False|  
|TableInsertTrigger|Table|Table has an INSERT trigger.<br /><br />>1 = ID of first trigger with the specified type.|  
|TableInsertTriggerCount|Table|Table has the specified number of INSERT triggers.<br /><br />>0 = The number of INSERT triggers.|  
|TableIsFake|Table|Table is not real. It is materialized internally on demand by the SQL Server Database Engine.<br /><br />1 = True<br /><br />0 = False|  
|TableIsLockedOnBulkLoad|Table|Table is locked due to a **bcp** or BULK INSERT job.<br /><br />1 = True<br /><br />0 = False<br /><br />Not available in SQL Server PDW.|  
|TableIsPinned|Table|Table is pinned to be held in the data cache.<br /><br />0 = False<br /><br />This feature is not supported in SQL Server 2005 and later.|  
|TableTextInRowLimit|Table|Maximum bytes allowed for text in row.<br /><br />0 if text in row option is not set.|  
|TableUpdateTrigger|Table|Table has an UPDATE trigger.<br /><br />> 1 = ID of first trigger with the specified type.|  
|TableUpdateTriggerCount|Table|The table has the specified number of UPDATE triggers.<br /><br />> 0 = The number of UPDATE triggers.|  
|TableHasColumnSet|Table|Table has a column set.<br /><br />0 = False<br /><br />1 = True<br /><br />For more information, see [Use Column Sets](http://msdn.microsoft.com/en-us/library/cc280521.aspx).|  
  
## Return Types  
**int**  
  
## Exceptions  
Returns NULL on error or if a caller does not have permission to view the object.  
  
A user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECTPROPERTY may return NULL if the user does not have any permission on the object.  
  
## Remarks  
Returns an error if the property is valid in SQL Server but not valid for SQL Server PDW.  
  
The SQL Server PDW assumes that *object_id* is in the current database context. A query that references an *object_id* in another database will return NULL or incorrect results.  
  
OBJECTPROPERTY(view_id, 'IsIndexable') may consume significant computer resources because evaluation of **IsIndexable** property requires the parsing of view definition, normalization, and partial optimization. Although the **IsIndexable** property identifies tables or views that can be indexed, the actual creation of the index still might fail if certain index key requirements are not met.  
  
OBJECTPROPERTY(table_id, 'TableHasActiveFulltextIndex') will return a value of 1 (true) when at least one column of a table is added for indexing. Full-text indexing becomes active for population as soon as the first column is added for indexing.  
  
Properties that are valid but not applicable to SQL Server PDW such as properties related to triggers, always return 0.  
  
## Examples  
The following example tests whether `dbo.DimReseller` is a table in the **AdventureWorksPDW2012** database.  
  
```  
USE AdventureWorksPDW2012;  
GO  
IF OBJECTPROPERTY (OBJECT_ID(N'dbo.DimReseller'),'ISTABLE') = 1  
   SELECT 'DimReseller is a table.'  
ELSE   
   SELECT 'DimReseller is not a table.';  
GO  
```  
  
The following example returns all the tables in the dbo schema.  
  
```  
USE AdventureWorksPDW2012;  
GO  
SELECT name, object_id, type_desc  
FROM sys.objects   
WHERE OBJECTPROPERTY(object_id, N'SchemaId') = SCHEMA_ID(N'dbo')  
ORDER BY type_desc, name;  
GO  
```  
  
