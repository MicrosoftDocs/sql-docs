---
title: "Microsoft OLE DB Provider for Microsoft Jet"
description: "Microsoft OLE DB Provider for Microsoft Jet Overview"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Jet provider for OLE DB [ADO]"
  - "providers [ADO], OLE DB provider for Microsoft Jet"
  - "OLE DB provider for Microsoft Jet [ADO]"
---
# Microsoft OLE DB Provider for Microsoft Jet Overview
The OLE DB Provider for Microsoft Jet allows ADO to access Microsoft Jet databases.

## Connection String Parameters
 To connect to this provider, set the *Provider* argument of the [ConnectionString](../../reference/ado-api/connectionstring-property-ado.md) property to the following property:

```vb
Microsoft.Jet.OLEDB.4.0
```

 Reading the [Provider](../../reference/ado-api/provider-property-ado.md) property will also return this string.

## Typical Connection String
 A typical connection string for this provider is:

```vb
"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=databaseName;User ID=MyUserID;Password=MyPassword;"
```

 The string consists of these keywords:

|Keyword|Description|
|-------------|-----------------|
|**Provider**|Specifies the OLE DB Provider for Microsoft Jet.|
|**Data Source**|Specifies the database path and file name (for example, `c:\Northwind.mdb`).|
|**User ID**|Specifies the user name. If this keyword is not specified, the string, "`admin`", is used by default.|
|**Password**|Specifies the user password. If this keyword is not specified, the empty string (""), is used by default.|

> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.

## Provider-Specific Connection Parameters
 The OLE DB Provider for Microsoft Jet supports several provider-specific dynamic properties in addition to those defined by ADO. As with all other **Connection** parameters, they can be set by using the **Properties** collection of the **Connection** object or as part of the connection string.

 The following table lists these properties together with the corresponding OLE DB property name in parentheses.

|Parameter|Description|
|---------------|-----------------|
|Jet OLEDB:Compact Reclaimed Space Amount (DBPROP_JETOLEDB_COMPACTFREESPACESIZE)|Indicates an estimate of the amount of space, in bytes, that can be reclaimed by compacting the database. This value is only valid after a database connection has been established.|
|Jet OLEDB:Connection Control (DBPROP_JETOLEDB_CONNECTIONCONTROL)|Indicates whether users can connect to the database.|
|Jet OLEDB:Create System Database (DBPROP_JETOLEDB_CREATESYSTEMDATABASE)|Indicates whether a system database should be created when creating a new data source.|
|Jet OLEDB:Database Locking Mode (DBPROP_JETOLEDB_DATABASELOCKMODE)|Indicates the locking mode for this database. The first user to open the database determines the mode used while the database is open.|
|Jet OLEDB:Database Password (DBPROP_JETOLEDB_DATABASEPASSWORD)|Indicates the database password.|
|Jet OLEDB:Don't Copy Locale on Compact (DBPROP_JETOLEDB_COMPACT_DONTCOPYLOCALE)|Indicates whether Jet should copy locale information when compacting a database.|
|Jet OLEDB:Encrypt Database (DBPROP_JETOLEDB_ENCRYPTDATABASE)|Indicates whether a compacted database should be encrypted. If this property is not set, the compacted database will be encrypted if the original database was also encrypted.|
|Jet OLEDB:Engine Type (DBPROP_JETOLEDB_ENGINE)|Indicates the storage engine used to access the current data store.|
|Jet OLEDB:Exclusive Async Delay (DBPROP_JETOLEDB_EXCLUSIVEASYNCDELAY)|Indicates the maximum length of time, in milliseconds, that Jet can delay asynchronous writes to disk when the database is opened exclusively.<br /><br /> This property is ignored unless **Jet OLEDB:Flush Transaction Timeout** is set to 0.|
|Jet OLEDB:Flush Transaction Timeout (DBPROP_JETOLEDB_FLUSHTRANSACTIONTIMEOUT)|Indicates the amount of time to wait before data stored in a cache for asynchronous writing is written to disk. This setting overrides the values for **Jet OLEDB:Shared Async Delay** and **Jet OLEDB:Exclusive Async Delay**.|
|Jet OLEDB:Global Bulk Transactions (DBPROP_JETOLEDB_GLOBALBULKNOTRANSACTIONS)|Indicates whether SQL bulk transactions are transacted.|
|Jet OLEDB:Global Partial Bulk Ops (DBPROP_JETOLEDB_GLOBALBULKPARTIAL)|Indicates the password used to open the database.|
|Jet OLEDB:Implicit Commit Sync (DBPROP_JETOLEDB_IMPLICITCOMMITSYNC)|Indicates whether changes that were made in internal implicit transactions are written in synchronous or asynchronous mode.|
|Jet OLEDB:Lock Delay (DBPROP_JETOLEDB_LOCKDELAY)|Indicates the number of milliseconds to wait before trying to acquire a lock after a previous attempt has failed.|
|Jet OLEDB:Lock Retry (DBPROP_JETOLEDB_LOCKRETRY)|Indicates how many times an attempt to access a locked page is repeated.|
|Jet OLEDB:Max Buffer Size (DBPROP_JETOLEDB_MAXBUFFERSIZE)|Indicates the maximum amount of memory, in kilobytes, Jet can use before it starts flushing changes to disk.|
|Jet OLEDB:Max Locks Per File (DBPROP_JETOLEDB_MAXLOCKSPERFILE)|Indicates the maximum number of locks Jet can place on a database. The default value is 9500.|
|Jet OLEDB:New Database Password (DBPROP_JETOLEDB_NEWDATABASEPASSWORD)|Indicates the new password to be set for this database. The old password is stored in **Jet OLEDB:Database Password**.|
|Jet OLEDB:ODBC Command Time Out (DBPROP_JETOLEDB_ODBCCOMMANDTIMEOUT)|Indicates the number of milliseconds before a remote ODBC query from Jet will time out.|
|Jet OLEDB:Page Locks to Table Lock (DBPROP_JETOLEDB_PAGELOCKSTOTABLELOCK)|Indicates how many pages must be locked within a transaction before Jet tries to promote the lock to a table lock. If this value is 0, the lock is never promoted.|
|Jet OLEDB:Page Timeout (DBPROP_JETOLEDB_PAGETIMEOUT)|Indicates the number of milliseconds Jet will wait before checking to see whether its cache is out of date with the database file.|
|Jet OLEDB:Recycle Long-Valued Pages (DBPROP_JETOLEDB_RECYCLELONGVALUEPAGES)|Indicates whether Jet should aggressively try to reclaim BLOB pages when they are freed.|
|Jet OLEDB:Registry Path (DBPROP_JETOLEDB_REGPATH)|Indicates the Windows registry key that contains values for the Jet database engine.|
|Jet OLEDB:Reset ISAM Stats (DBPROP_JETOLEDB_RESETISAMSTATS)|Indicates whether the schema **Recordset** DBSCHEMA_JETOLEDB_ISAMSTATS should reset its performance counters after it returns performance information.|
|Jet OLEDB:Shared Async Delay (DBPROP_JETOLEDB_SHAREDASYNCDELAY)|Indicates the maximum amount of time, in milliseconds, Jet can delay asynchronous writes to disk when the database is opened in multiuser mode.|
|Jet OLEDB:System Database (DBPROP_JETOLEDB_SYSDBPATH)|Indicates the path and file name for the workgroup information file (system database).|
|Jet OLEDB:Transaction Commit Mode (DBPROP_JETOLEDB_TXNCOMMITMODE)|Indicates whether Jet writes data to disk synchronously or asynchronously when a transaction is committed.|
|Jet OLEDB:User Commit Sync (DBPROP_JETOLEDB_USERCOMMITSYNC)|Indicates whether changes that were made in transactions are written in synchronous or asynchronous mode.|

## Provider-Specific Recordset and Command Properties
 The Jet provider also supports several provider-specific **Recordset** and **Command** properties. These properties are accessed and set through the **Properties** collection of the **Recordset** or **Command** object. The table lists the ADO property name and its corresponding OLE DB property name in parentheses.

|Property Name|Description|
|-------------------|-----------------|
|Jet OLEDB:Bulk Transactions (DBPROP_JETOLEDB_BULKNOTRANSACTIONS)|Indicates whether SQL bulk operations are transacted. Large bulk operations might fail when transacted, because of resource delays.|
|Jet OLEDB:Enable Fat Cursors (DBPROP_JETOLEDB_ENABLEFATCURSOR)|Indicates whether Jet should cache multiple rows when populating a recordset for remote row sources.|
|Jet OLEDB:Fat Cursor Cache Size (DBPROP_JETOLEDB_FATCURSORMAXROWS)|Indicates the number of rows to cache when using remote data store row caching. This value is ignored unless **Jet OLEDB:Enable Fat Cursors** is True.|
|Jet OLEDB:Inconsistent (DBPROP_JETOLEDB_INCONSISTENT)|Indicates whether query results allow inconsistent updates.|
|Jet OLEDB:Locking Granularity (DBPROP_JETOLEDB_LOCKGRANULARITY)|Indicates whether a table is opened using row-level locking.|
|Jet OLEDB:ODBC Pass-Through Statement (DBPROP_JETOLEDB_ODBCPASSTHROUGH)|Indicates that Jet should pass the SQL text in a **Command** object to the back end unaltered.|
|Jet OLEDB:Partial Bulk Ops (DBPROP_JETOLEDB_BULKPARTIAL)|Indicates Jet's behavior when SQL DML operations fail.|
|Jet OLEDB:Pass Through Query Bulk-Op (DBPROP_JETOLEDB_PASSTHROUGHBULKOP)|Indicates whether queries that do not return a **Recordset** are passed unaltered to the data source.|
|Jet OLEDB:Pass Through Query Connect String (DBPROP_JETOLEDB_ODBCPASSTHROUGHCONNECTSTRING)|Indicates the Jet connect string used to connect to a remote data store. This value is ignored unless **Jet OLEDB:ODBC Pass-Through Statement** is True.|
|Jet OLEDB:Stored Query (DBPROP_JETOLEDB_STOREDQUERY)|Indicates whether the command text should be interpreted as a stored query instead of an SQL command.|
|Jet OLEDB:Validate Rules On Set (DBPROP_JETOLEDB_VALIDATEONSET)|Indicates whether the Jet validation rules are evaluated when column data is set or when the changes are committed to the database.|

 By default, the OLE DB Provider for Microsoft Jet opens Microsoft Jet databases in read/write mode. To open a database in read-only mode, set the [Mode](../../reference/ado-api/mode-property-ado.md) property on the ADO **Connection** object to **adModeRead**.

## Command Object Usage
 Command text in the [Command](../../reference/ado-api/command-object-ado.md) object uses the Microsoft Jet SQL dialect. You can specify row-returning queries, action queries, and table names in the command text; however, stored procedures are not supported and should not be specified.

## Recordset Behavior
 The Microsoft Jet database engine does not support dynamic cursors. Therefore, the OLE DB Provider for Microsoft Jet does not support the **adLockDynamic** cursor type. When a dynamic cursor is requested, the provider will return a keyset cursor and reset the [CursorType](../../reference/ado-api/cursortype-property-ado.md) property to indicate the type of [Recordset](../../reference/ado-api/recordset-object-ado.md) returned. Further, if an updatable **Recordset** is requested (**LockType** is **adLockOptimistic**, **adLockBatchOptimistic**, or **adLockPessimistic**) the provider will also return a keyset cursor and reset the **CursorType** property.

## Dynamic Properties
 The OLE DB Provider for Microsoft Jet inserts several dynamic properties into the **Properties** collection of the unopened [Connection](../../reference/ado-api/connection-object-ado.md), [Recordset](../../reference/ado-api/recordset-object-ado.md), and [Command](../../reference/ado-api/command-object-ado.md) objects.

 The following tables are a cross-index of the ADO and OLE DB names for each dynamic property. The OLE DB Programmer's Reference refers to an ADO property name by the term, "Description." You can find more information about these properties in the OLE DB Programmer's Reference.

## Connection Dynamic Properties
 The following properties are added to the **Properties** collection of the **Connection** object.

|ADO Property Name|OLE DB Property Name|
|-----------------------|--------------------------|
|Active Sessions|DBPROP_ACTIVESESSIONS|
|Asynchable Abort|DBPROP_ASYNCTXNABORT|
|Asynchable Commit|DBPROP_ASYNCTNXCOMMIT|
|Autocommit Isolation Levels|DBPROP_SESS_AUTOCOMMITISOLEVELS|
|Catalog Location|DBPROP_CATALOGLOCATION|
|Catalog Term|DBPROP_CATALOGTERM|
|Column Definition|DBPROP_COLUMNDEFINITION|
|Current Catalog|DBPROP_CURRENTCATALOG|
|Data Source|DBPROP_INIT_DATASOURCE|
|Data Source Name|DBPROP_DATASOURCENAME|
|Data Source Object Threading Model|DBPROP_DSOTHREADMODEL|
|DBMS Name|DBPROP_DBMSNAME|
|DBMS Version|DBPROP_DBMSVER|
|GROUP BY Support|DBPROP_GROUPBY|
|Heterogeneous Table Support|DBPROP_HETEROGENEOUSTABLES|
|Identifier Case Sensitivity|DBPROP_IDENTIFIERCASE|
|Isolation Levels|DBPROP_SUPPORTEDTXNISOLEVELS|
|Isolation Retention|DBPROP_SUPPORTEDTXNISORETAIN|
|Locale Identifier|DBPROP_INIT_LCID|
|Maximum Index Size|DBPROP_MAXINDEXSIZE|
|Maximum Row Size|DBPROP_MAXROWSIZE|
|Maximum Row Size Includes BLOB|DBPROP_MAXROWSIZEINCLUDESBLOB|
|Maximum Tables in SELECT|DBPROP_MAXTABLESINSELECT|
|Mode|DBPROP_INIT_MODE|
|Multiple Parameter Sets|DBPROP_MULTIPLEPARAMSETS|
|Multiple Results|DBPROP_MULTIPLERESULTS|
|Multiple Storage Objects|DBPROP_MULTIPLESTORAGEOBJECTS|
|Multi-Table Update|DBPROP_MULTITABLEUPDATE|
|NULL Collation Order|DBPROP_NULLCOLLATION|
|NULL Concatenation Behavior|DBPROP_CONCATNULLBEHAVIOR|
|OLE DB Version|DBPROP_PROVIDEROLEDBVER|
|OLE Object Support|DBPROP_OLEOBJECTS|
|Open Rowset Support|DBPROP_OPENROWSETSUPPORT|
|ORDER BY Columns in Select List|DBPROP_ORDERBYCOLUMNSINSELECT|
|Output Parameter Availability|DBPROP_OUTPUTPARAMETERAVAILABILITY|
|Pass By Ref Accessors|DBPROP_BYREFACCESSORS|
|Password|DBPROP_AUTH_PASSWORD|
|Persistent ID Type|DBPROP_PERSISTENTIDTYPE|
|Prepare Abort Behavior|DBPROP_PREPAREABORTBEHAVIOR|
|Prepare Commit Behavior|DBPROP_PREPARECOMMITBEHAVIOR|
|Procedure Term|DBPROP_PROCEDURETERM|
|Prompt|DBPROP_INIT_PROMPT|
|Provider Friendly Name|DBPROP_PROVIDERFRIENDLYNAME|
|Provider Name|DBPROP_PROVIDERFILENAME|
|Provider Version|DBPROP_PROVIDERVER|
|Read-Only Data Source|DBPROP_DATASOURCEREADONLY|
|Rowset Conversions on Command|DBPROP_ROWSETCONVERSIONSONCOMMAND|
|Schema Term|DBPROP_SCHEMATERM|
|Schema Usage|DBPROP_SCHEMAUSAGE|
|SQL Support|DBPROP_SQLSUPPORT|
|Structured Storage|DBPROP_STRUCTUREDSTORAGE|
|Subquery Support|DBPROP_SUBQUERIES|
|Table Term|DBPROP_TABLETERM|
|Transaction DDL|DBPROP_SUPPORTEDTXNDDL|
|User ID|DBPROP_AUTH_USERID|
|User Name|DBPROP_USERNAME|
|Window Handle|DBPROP_INIT_HWND|

## Recordset Dynamic Properties
 The following properties are added to the **Properties** collection of the **Recordset** object.

|ADO Property Name|OLE DB Property Name|
|-----------------------|--------------------------|
|Access Order|DBPROP_ACCESSORDER|
|Append-Only Rowset|DBPROP_APPENDONLY|
|Blocking Storage Objects|DBPROP_BLOCKINGSTORAGEOBJECTS|
|Bookmark Type|DBPROP_BOOKMARKTYPE|
|Bookmarkable|DBPROP_IROWSETLOCATE|
|Bookmarks Ordered|DBPROP_ORDEREDBOOKMARKS|
|Cache Deferred Columns|DBPROP_CACHEDEFERRED|
|Change Inserted Rows|DBPROP_CHANGEINSERTEDROWS|
|Column Privileges|DBPROP_COLUMNRESTRICT|
|Column Set Notification|DBPROP_NOTIFYCOLUMNSET|
|Column Writable|DBPROP_MAYWRITECOLUMN|
|Defer Column|DBPROP_DEFERRED|
|Delay Storage Object Updates|DBPROP_DELAYSTORAGEOBJECTS|
|Fetch Backwards|DBPROP_CANFETCHBACKWARDS|
|Hold Rows|DBPROP_CANHOLDROWS|
|IAccessor|DBPROP_IAccessor|
|IColumnsInfo|DBPROP_IColumnsInfo|
|IColumnsRowset|DBPROP_IColumnsRowset|
|IConnectionPointContainer|DBPROP_IConnectionPointContainer|
|IConvertType|DBPROP_IConvertType|
|ILockBytes|DBPROP_ILockBytes|
|Immobile Rows|DBPROP_IMMOBILEROWS|
|IRowset|DBPROP_IRowset|
|IRowsetChange|DBPROP_IRowsetChange|
|IRowsetIdentity|DBPROP_IRowsetIdentity|
|IRowsetIndex|DBPROP_IRowsetIndex|
|IRowsetInfo|DBPROP_IRowsetInfo|
|IRowsetLocate|DBPROP_IRowsestLocate|
|IRowsetResynch||
|IRowsetScroll|DBPROP_IRowsetScroll|
|IRowsetUpdate|DBPROP_IRowsetUpdate|
|ISequentialStream|DBPROP_ISequentialStream|
|IStorage|DBPROP_IStorage|
|IStream|DBPROP_IStream|
|ISupportErrorInfo|DBPROP_ISupportErrorInfo|
|Literal Bookmarks|DBPROP_LITERALBOOKMARKS|
|Literal Row Identity|DBPROP_LITERALIDENTITY|
|Maximum Open Rows|DBPROP_MAXOPENROWS|
|Maximum Pending Rows|DBPROP_MAXPENDINGROWS|
|Maximum Rows|DBPROP_MAXROWS|
|Memory Usage|DBPROP_MEMORYUSAGE|
|Notification Granularity|DBPROP_NOTIFICATIONGRANULARITY|
|Notification Phases|DBPROP_NOTIFICATIONPHASES|
|Objects Transacted|DBPROP_TRANSACTEDOBJECT|
|Others' Changes Visible|DBPROP_OTHERUPDATEDELETE|
|Others' Inserts Visible|DBPROP_OTHERINSERT|
|Own Changes Visible|DBPROP_OWNUPDATEDELETE|
|Own Inserts Visible|DBPROP_OWNINSERT|
|Preserve on Abort|DBPROP_ABORTPRESERVE|
|Preserve on Commit|DBPROP_COMMITPRESERVE|
|Quick Restart|DBPROP_QUICKRESTART|
|Reentrant Events|DBPROP_REENTRANTEVENTS|
|Remove Deleted Rows|DBPROP_REMOVEDELETED|
|Report Multiple Changes|DBPROP_REPORTMULTIPLECHANGES|
|Return Pending Inserts|DBPROP_RETURNPENDINGINSERTS|
|Row Delete Notification|DBPROP_NOTIFYROWDELETE|
|Row First Change Notification|DBPROP_NOTIFYROWFIRSTCHANGE|
|Row Insert Notification|DBPROP_NOTIFYROWINSERT|
|Row Privileges|DBPROP_ROWRESTRICT|
|Row Resynchronization Notification|DBPROP_NOTIFYROWRESYNCH|
|Row Threading Model|DBPROP_ROWTHREADMODEL|
|Row Undo Change Notification|DBPROP_NOTIFYROWUNDOCHANGE|
|Row Undo Delete Notification|DBPROP_NOTIFYROWUNDODELETE|
|Row Undo Insert Notification|DBPROP_NOTIFYROWUNDOINSERT|
|Row Update Notification|DBPROP_NOTIFYROWUPDATE|
|Rowset Fetch Position Change Notification|DBPROP_NOTIFYROWSETFETCHPOSISIONCHANGE|
|Rowset Release Notification|DBPROP_NOTIFYROWSETRELEASE|
|Scroll Backwards|DBPROP_CANSCROLLBACKWARDS|
|Skip Deleted Bookmarks|DBPROP_BOOKMARKSKIPPED|
|Strong Row Identity|DBPROP_STRONGITDENTITY|
|Updatability|DBPROP_UPDATABILITY|
|Use Bookmarks|DBPROP_BOOKMARKS|

## Command Dynamic Properties
 The following properties are added to the **Properties** collection of the **Command** object.

|ADO Property Name|OLE DB Property Name|
|-----------------------|--------------------------|
|Access Order|DBPROP_ACCESSORDER|
|Append-Only Rowset|DBPROP_APPENDONLY|
|Blocking Storage Objects|DBPROP_BLOCKINGSTORAGEOBJECTS|
|Bookmark Type|DBPROP_BOOKMARKTYPE|
|Bookmarkable|DBPROP_IROWSETLOCATE|
|Change Inserted Rows|DBPROP_CHANGEINSERTEDROWS|
|Column Privileges|DBPROP_COLUMNRESTRICT|
|Column Set Notification|DBPROP_NOTIFYCOLUMNSET|
|Defer Column|DBPROP_DEFERRED|
|Delay Storage Object Updates|DBPROP_DELAYSTORAGEOBJECTS|
|Fetch Backwards|DBPROP_CANFETCHBACKWARDS|
|Hold Rows|DBPROP_CANHOLDROWS|
|IAccessor|DBPROP_IAccessor|
|IColumnsInfo|DBPROP_IColumnsInfo|
|IColumnsRowset|DBPROP_IColumnsRowset|
|IConnectionPointContainer|DBPROP_IConnectionPointContainer|
|IConvertType|DBPROP_IConvertType|
|ILockBytes|DBPROP_ILockBytes|
|Immobile Rows|DBPROP_IMMOBILEROWS|
|IRowset|DBPROP_IRowset|
|IRowsetChange|DBPROP_IRowsetChange|
|IRowsetIdentity|DBPROP_IRowsetIdentity|
|IRowsetIndex|DBPROP_IRowsetIndex|
|IRowsetInfo|DBPROP_IRowsetInfo|
|IRowsetLocate|DBPROP_IRowsetLocate|
|IRowsetResynch||
|IRowsetScroll|DBPROP_IRowsetScroll|
|IRowsetUpdate|DBPROP_IRowsetUpdate|
|ISequentialStream|DBPROP_ISequentialStream|
|IStorage|DBPROP_IStorage|
|IStream|DBPROP_IStream|
|ISupportErrorInfo|DBPROP_ISupportErrorInfo|
|Literal Bookmarks|DBPROP_LITERALBOOKMARKS|
|Literal Row Identity|DBPROP_LITERALIDENTITY|
|Lock Mode|DBPROP_LOCKMODE|
|Maximum Open Rows|DBPROP_MAXOPENROWS|
|Maximum Pending Rows|DBPROP_MAXPENDINGROWS|
|Maximum Rows|DBPROP_MAXROWS|
|Notification Granularity|DBPROP_NOTIFICATIONGRANULARITY|
|Notification Phases|DBPROP_NOTIFICATIONPHASES|
|Objects Transacted|DBPROP_TRANSACTEDOBJECT|
|Others' Changes Visible|DBPROP_OTHERUPDATEDELETE|
|Others' Inserts Visible|DBPROP_OTHERINSERT|
|Own Changes Visible|DBPROP_OWNUPDATEDELETE|
|Own Inserts Visible|DBPROP_OWNINSERT|
|Preserve on Abort|DBPROP_ABORTPRESERVE|
|Preserve on Commit|DBPROP_COMMITPRESERVE|
|Quick Restart|DBPROP_QUICKRESTART|
|Reentrant Events|DBPROP_REENTRANTEVENTS|
|Remove Deleted Rows|DBPROP_REMOVEDELETED|
|Report Multiple Changes|DBPROP_REPORTMULTIPLECHANGES|
|Return Pending Inserts|DBPROP_RETURNPENDINGINSERTS|
|Row Delete Notification|DBPROP_NOTIFYROWDELETE|
|Row First Change Notification|DBPROP_NOTIFYROWFIRSTCHANGE|
|Row Insert Notification|DBPROP_NOTIFYROWINSERT|
|Row Privileges|DBPROP_ROWRESTRICT|
|Row Resynchronization Notification|DBPROP_NOTIFYROWRESYNCH|
|Row Threading Model|DBPROP_ROWTHREADMODEL|
|Row Undo Change Notification|DBPROP_NOTIFYROWUNDOCHANGE|
|Row Undo Delete Notification|DBPROP_NOTIFYROWUNDODELETE|
|Row Undo Insert Notification|DBPROP_NOTIFYROWUNDOINSERT|
|Row Update Notification|DBPROP_NOTIFYROWUPDATE|
|Rowset Fetch Position Change Notification|DBPROP_NOTIFYROWSETFETCHPOSITIONCHANGE|
|Rowset Release Notification|DBPROP_NOTIFYROWSETRELEASE|
|Scroll Backwards|DBPROP_CANSCROLLBACKWARDS|
|Server Data on Insert|DBPROP_SERVERDATAONINSERT|
|Skip Deleted Bookmarks|DBPROP_BOOKMARKSKIP|
|Strong Row Identity|DBPROP_STRONGIDENTITY|
|Updatability|DBPROP_UPDATABILITY|
|Use Bookmarks|DBPROP_BOOKMARKS|

 For specific implementation details and functional information about the OLE DB Provider for Microsoft Jet, see [Jet Provider](/previous-versions/windows/desktop/ms722791(v=vs.85)) in the OLE DB documentation.
