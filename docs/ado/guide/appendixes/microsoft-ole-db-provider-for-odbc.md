---
title: "Microsoft OLE DB Provider for ODBC"
description: "Microsoft OLE DB Provider for ODBC Overview"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "OLE DB provider for ODBC [ADO]"
  - "providers [ADO], OLE DB provider for ODBC"
---
# Microsoft OLE DB Provider for ODBC Overview
To an ADO or RDS programmer, an ideal world would be one in which every data source exposes an OLE DB interface, so that ADO could call directly into the data source. Although increasingly more database vendors are implementing OLE DB interfaces, some data sources are not yet exposed this way. However, most DBMS systems in use today can be accessed through ODBC.

 ODBC drivers are available for every major DBMS in use today, including Microsoft SQL Server, Microsoft Access (Microsoft Jet database engine), and Microsoft FoxPro, in addition to non-Microsoft database products such as Oracle.

 The Microsoft ODBC Provider, however, allows ADO to connect to any ODBC data source. The provider is free-threaded and Unicode enabled.

 The provider supports transactions, although different DBMS engines offer different types of transaction support. For example, Microsoft Access supports nested transactions up to five levels deep.

 This is the default provider for ADO, and all provider-dependent ADO properties and methods are supported.

## Connection String Parameters
 To connect to this provider, set the **Provider=** argument of the [ConnectionString](../../reference/ado-api/connectionstring-property-ado.md) property to:

```
MSDASQL
```

 Reading the [Provider](../../reference/ado-api/provider-property-ado.md) property will return this string as well.

## Typical Connection String
 A typical connection string for this provider is:

```
"Provider=MSDASQL;DSN=dsnName;UID=MyUserID;PWD=MyPassword;"
```

 The string consists of these keywords:

|Keyword|Description|
|-------------|-----------------|
|**Provider**|Specifies the OLE DB provider for ODBC.|
|**DSN**|Specifies the data source name.|
|**UID**|Specifies the user name.|
|**PWD**|Specifies the user password.|
|**URL**|Specifies the URL of a file or directory published in a Web folder.|

 Because this is the default provider for ADO, if you omit the **Provider=** parameter from the connection string, ADO will try to establish a connection to this provider.

> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.

 The provider does not support any specific connection parameters in addition to those defined by ADO. However, the provider will pass any non-ADO connection parameters to the ODBC driver manager.

 Because you can omit the **Provider** parameter, you can therefore compose an ADO connection string that is identical to an ODBC connection string for the same data source. Use the same parameter names (**DRIVER=**, **DATABASE=**, **DSN=**, and so on), values, and syntax as you would when composing an ODBC connection string. You can connect with or without a predefined data source name (DSN) or FileDSN.

## Syntax with a DSN or FileDSN:

```
"[Provider=MSDASQL;] { DSN=name | FileDSN=filename } ;
[DATABASE=database;] UID=user; PWD=password"
```

## Syntax without a DSN (DSN-less connection):

```
"[Provider=MSDASQL;] DRIVER=driver; SERVER=server;
DATABASE=database; UID=MyUserID; PWD=MyPassword"
```

## Remarks
 If you use a **DSN** or **FileDSN**, it must be defined through the ODBC Data Source Administrator in the Windows Control Panel. In Microsoft Windows 2000, the ODBC Administrator is located under Administrative Tools. In earlier versions of Windows, the ODBC Administrator icon is named **32-bit ODBC** or just **ODBC**.

 As an alternative to setting a **DSN**, you can specify the ODBC driver (**DRIVER=**), such as "SQL Server;" the server name (**SERVER=**); and the database name (**DATABASE=**).

 You can also specify a user account name (**UID=**), and the password for the user account (**PWD=**) in the ODBC-specific parameters or in the standard ADO-defined *user* and *password* parameters.

 Although a **DSN** definition already specifies a database, you can specify *a* *database* parameter in addition to a **DSN** to connect to a different database. It is a good idea to always include *the* *database* parameter when you use a **DSN**. This will ensure that you connect to the correct database if another user changed the default database parameter since you last checked the **DSN** definition.

## Provider-Specific Connection Properties
 The OLE DB provider for ODBC adds several properties to the [Properties](../../reference/ado-api/properties-collection-ado.md) collection of the **Connection** object. The following table lists these properties with the corresponding OLE DB property name in parentheses.

|Property Name|Description|
|-------------------|-----------------|
|Accessible Procedures (KAGPROP_ACCESSIBLEPROCEDURES)|Indicates whether the user has access to stored procedures.|
|Accessible Tables (KAGPROP_ACCESSIBLETABLES)|Indicates whether the user has permission to execute SELECT statements against the database tables.|
|Active Statements (KAGPROP_ACTIVESTATEMENTS)|Indicates the number of handles an ODBC driver can support on a connection.|
|Driver Name (KAGPROP_DRIVERNAME)|Indicates the file name of the ODBC driver.|
|Driver ODBC Version (KAGPROP_DRIVERODBCVER)|Indicates the version of ODBC that this driver supports.|
|File Usage (KAGPROP_FILEUSAGE)|Indicates how the driver treats a file in a data source; as a table or as a catalog.|
|Like Escape Clause (KAGPROP_LIKEESCAPECLAUSE)|Indicates whether the driver supports the definition and use of an escape character for the percent character (%) and underline character (_) in the LIKE predicate of a WHERE clause.|
|Max Columns in Group By (KAGPROP_MAXCOLUMNSINGROUPBY)|Indicates the maximum number of columns that can be listed in the GROUP BY clause of a SELECT statement.|
|Max Columns in Index (KAGPROP_MAXCOLUMNSININDEX)|Indicates the maximum number of columns that can be included in an index.|
|Max Columns in Order By (KAGPROP_MAXCOLUMNSINORDERBY)|Indicates the maximum number of columns that can be listed in the ORDER BY clause of a SELECT statement.|
|Max Columns in Select (KAGPROP_MAXCOLUMNSINSELECT)|Indicates the maximum number of columns that can be listed in the SELECT portion of a SELECT statement.|
|Max Columns in Table (KAGPROP_MAXCOLUMNSINTABLE)|Indicates the maximum number of columns allowed in a table.|
|Numeric Functions (KAGPROP_NUMERICFUNCTIONS)|Indicates which numeric functions are supported by the ODBC driver. For a listing of function names and the associated values used in this bitmask, see [Appendix E: Scalar Functions](../../../odbc/reference/appendixes/appendix-e-scalar-functions.md), in the ODBC documentation.|
|Outer Join Capabilities (KAGPROP_OJCAPABILITY)|Indicates the types of OUTER JOINs supported by the provider.|
|Outer Joins (KAGPROP_OUTERJOINS)|Indicates whether the provider supports OUTER JOINs.|
|Special Characters (KAGPROP_SPECIALCHARACTERS)|Indicates which characters have special meaning for the ODBC driver.|
|Stored Procedures (KAGPROP_PROCEDURES)|Indicates whether stored procedures are available for use with this ODBC driver.|
|String Functions (KAGPROP_STRINGFUNCTIONS)|Indicates which string functions are supported by the ODBC driver. For a listing of function names and the associated values used in this bitmask, see [Appendix E: Scalar Functions](../../../odbc/reference/appendixes/appendix-e-scalar-functions.md), in the ODBC documentation.|
|System Functions (KAGPROP_SYSTEMFUNCTIONS)|Indicates which system functions are supported by the ODBC driver. For a listing of function names and the associated values used in this bitmask, see [Appendix E: Scalar Functions](../../../odbc/reference/appendixes/appendix-e-scalar-functions.md), in the ODBC documentation.|
|Time/Date Functions (KAGPROP_TIMEDATEFUNCTIONS)|Indicates which time and date functions are supported by the ODBC driver. For a listing of function names and the associated values used in this bitmask, see [Appendix E: Scalar Functions](../../../odbc/reference/appendixes/appendix-e-scalar-functions.md), in the ODBC documentation.|
|SQL Grammar Support (KAGPROP_ODBCSQLCONFORMANCE)|Indicates the SQL grammar that the ODBC driver supports.|

## Provider-Specific Recordset and Command Properties
 The OLE DB provider for ODBC adds several properties to the **Properties** collection of the **Recordset** and **Command** objects. The following table lists these properties with the corresponding OLE DB property name in parentheses.

|Property Name|Description|
|-------------------|-----------------|
|Query Based Updates/Deletes/Inserts (KAGPROP_QUERYBASEDUPDATES)|Indicates whether updates, deletions, and insertions can be performed by using SQL queries.|
|ODBC Concurrency Type (KAGPROP_CONCURRENCY)|Indicates the method used to reduce potential problems caused by two users trying to access the same data from the data source simultaneously.|
|BLOB accessibility on Forward-Only cursor (KAGPROP_BLOBSONFOCURSOR)|Indicates whether BLOB **Fields** can be accessed when using a forward-only cursor.|
|Include SQL_FLOAT, SQL_DOUBLE, and SQL_REAL in QBU WHERE clauses (KAGPROP_INCLUDENONEXACT)|Indicates whether SQL_FLOAT, SQL_DOUBLE, and SQL_REAL values can be included in a QBU WHERE clause.|
|Position on the last row after insert (KAGPROP_POSITIONONNEWROW)|Indicates that after a new record has been inserted in a table, the last row in the table will be come the current row.|
|IRowsetChangeExtInfo (KAGPROP_IROWSETCHANGEEXTINFO)|Indicates whether the **IRowsetChange** interface provides extended information support.|
|ODBC Cursor Type (KAGPROP_CURSOR)|Indicates the type of cursor used by the **Recordset**.|
|Generate a Rowset that can be marshaled (KAGPROP_MARSHALLABLE)|Indicates that the ODBC driver generates a recordset that can be marshaled|

## Command Text
 How you use the [Command](../../reference/ado-api/command-object-ado.md) object largely depends on the data source, and what type of query or command statement it will accept.

 ODBC provides a specific syntax for calling stored procedures. For the [CommandText](../../reference/ado-api/commandtext-property-ado.md) property of a **Command** object, the *CommandText* argument to the **Execute** method on a [Connection](../../reference/ado-api/connection-object-ado.md) object, or the *Source* argument to the **Open** method on a [Recordset](../../reference/ado-api/recordset-object-ado.md) object, passes in a string with this syntax:

```
"{ [ ? = ] call procedure [ ( ? [, ? [ , ... ]] ) ] }"
```

 Each **?** references an object in the [Parameters](../../reference/ado-api/parameters-collection-ado.md) collection. The first **?** references **Parameters**(0), the next **?** references **Parameters**(1), and so on.

 The parameter references are optional and depend on the structure of the stored procedure. If you want to call a stored procedure that defines no parameters, your string would look like the following:

```
"{ call procedure }"
```

 If you have two query parameters, your string would resemble the following:

```
"{ call procedure ( ?, ? ) }"
```

 If the stored procedure will return a value, the return value is treated as another parameter. If you have no query parameters but you do have a return value, your string would resemble the following:

```
"{ ? = call procedure }"
```

 Finally, if you have a return value and two query parameters, your string would resemble the following:

```
"{ ? = call procedure ( ?, ? ) }"
```

## Recordset Behavior
 The following tables list the standard ADO methods and properties available on a **Recordset** object opened with this provider.

 For more detailed information about **Recordset** behavior for your provider configuration, run the [Supports](../../reference/ado-api/supports-method.md) method and enumerate the **Properties** collection of the **Recordset** to determine whether provider-specific dynamic properties are present.

 Availability of standard ADO **Recordset** properties:

|Property|ForwardOnly|Dynamic|Keyset|Static|
|--------------|-----------------|-------------|------------|------------|
|[AbsolutePage](../../reference/ado-api/absolutepage-property-ado.md)|not available|not available|read/write|read/write|
|[AbsolutePosition](../../reference/ado-api/absoluteposition-property-ado.md)|not available|not available|read/write|read/write|
|[ActiveConnection](../../reference/ado-api/activeconnection-property-ado.md)|read/write|read/write|read/write|read/write|
|[BOF](../../reference/ado-api/bof-eof-properties-ado.md)|read-only|read-only|read-only|read-only|
|[Bookmark](../../reference/ado-api/bookmark-property-ado.md)|not available|not available|read/write|read/write|
|[CacheSize](../../reference/ado-api/cachesize-property-ado.md)|read/write|read/write|read/write|read/write|
|[CursorLocation](../../reference/ado-api/cursorlocation-property-ado.md)|read/write|read/write|read/write|read/write|
|[CursorType](../../reference/ado-api/cursortype-property-ado.md)|read/write|read/write|read/write|read/write|
|[EditMode](../../reference/ado-api/editmode-property.md)|read-only|read-only|read-only|read-only|
|[Filter](../../reference/ado-api/filter-property.md)|read/write|read/write|read/write|read/write|
|[LockType](../../reference/ado-api/locktype-property-ado.md)|read/write|read/write|read/write|read/write|
|[MarshalOptions](../../reference/ado-api/marshaloptions-property-ado.md)|read/write|read/write|read/write|read/write|
|[MaxRecords](../../reference/ado-api/maxrecords-property-ado.md)|read/write|read/write|read/write|read/write|
|[PageCount](../../reference/ado-api/pagecount-property-ado.md)|read/write|not available|read-only|read-only|
|[PageSize](../../reference/ado-api/pagesize-property-ado.md)|read/write|read/write|read/write|read/write|
|[RecordCount](../../reference/ado-api/recordcount-property-ado.md)|read/write|not available|read-only|read-only|
|[Source](../../reference/ado-api/source-property-ado-recordset.md)|read/write|read/write|read/write|read/write|
|[State](../../reference/ado-api/state-property-ado.md)|read-only|read-only|read-only|read-only|
|[Status](../../reference/ado-api/status-property-ado-recordset.md)|read-only|read-only|read-only|read-only|

 The [AbsolutePosition](../../reference/ado-api/absoluteposition-property-ado.md) and [AbsolutePage](../../reference/ado-api/absolutepage-property-ado.md) properties are write-only when ADO is used with version 1.0 of the Microsoft OLE DB Provider for ODBC.

 Availability of standard ADO **Recordset** methods:

|Method|ForwardOnly|Dynamic|Keyset|Static|
|------------|-----------------|-------------|------------|------------|
|[AddNew](../../reference/ado-api/addnew-method-ado.md)|Yes|Yes|Yes|Yes|
|[Cancel](../../reference/ado-api/cancel-method-ado.md)|Yes|Yes|Yes|Yes|
|[CancelBatch](../../reference/ado-api/cancelbatch-method-ado.md)|Yes|Yes|Yes|Yes|
|[CancelUpdate](../../reference/ado-api/cancelupdate-method-ado.md)|Yes|Yes|Yes|Yes|
|[Clone](../../reference/ado-api/clone-method-ado.md)|No|No|Yes|Yes|
|[Close](../../reference/ado-api/close-method-ado.md)|Yes|Yes|Yes|Yes|
|[Delete](../../reference/ado-api/delete-method-ado-recordset.md)|Yes|Yes|Yes|Yes|
|[GetRows](../../reference/ado-api/getrows-method-ado.md)|Yes|Yes|Yes|Yes|
|[Move](../../reference/ado-api/move-method-ado.md)|Yes|Yes|Yes|Yes|
|[MoveFirst](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Yes|Yes|Yes|Yes|
|[MoveLast](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|No|Yes|Yes|Yes|
|[MoveNext](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Yes|Yes|Yes|Yes|
|[MovePrevious](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|No|Yes|Yes|Yes|
|[NextRecordset](../../reference/ado-api/nextrecordset-method-ado.md)*|Yes|Yes|Yes|Yes|
|[Open](../../reference/ado-api/open-method-ado-recordset.md)|Yes|Yes|Yes|Yes|
|[Requery](../../reference/ado-api/requery-method.md)|Yes|Yes|Yes|Yes|
|[Resync](../../reference/ado-api/resync-method.md)|No|No|Yes|Yes|
|[Supports](../../reference/ado-api/supports-method.md)|Yes|Yes|Yes|Yes|
|[Update](../../reference/ado-api/update-method.md)|Yes|Yes|Yes|Yes|
|[UpdateBatch](../../reference/ado-api/updatebatch-method.md)|Yes|Yes|Yes|Yes|

 *Not supported for Microsoft Access databases.

## Dynamic Properties
 The Microsoft OLE DB Provider for ODBC inserts several dynamic properties into the **Properties** collection of the unopened [Connection](../../reference/ado-api/connection-object-ado.md), [Recordset](../../reference/ado-api/recordset-object-ado.md), and [Command](../../reference/ado-api/command-object-ado.md) objects.

 The following tables are a cross-index of the ADO and OLE DB names for each dynamic property. The OLE DB Programmer's Reference refers to an ADO property name by the term, "Description." You can find more information about these properties in the OLE DB Programmer's Reference. Search for the OLE DB property name in the Index or see [Appendix C: OLE DB Properties](/previous-versions/windows/desktop/ms723130(v=vs.85)).

## Connection Dynamic Properties
 The following properties are added to the **Connection** object's **Properties** collection.

|ADO Property Name|OLE DB Property Name|
|-----------------------|--------------------------|
|Active Sessions|DBPROP_ACTIVESESSIONS|
|Asynchable Abort|DBPROP_ASYNCTXNABORT|
|Asynchable Commit|DBPROP_ASYNCTNXCOMMIT|
|Autocommit Isolation Levels|DBPROP_SESS_AUTOCOMMITISOLEVELS|
|Catalog Location|DBPROP_CATALOGLOCATION|
|Catalog Term|DBPROP_CATALOGTERM|
|Column Definition|DBPROP_COLUMNDEFINITION|
|Connect Timeout|DBPROP_INIT_TIMEOUT|
|Current Catalog|DBPROP_CURRENTCATALOG|
|Data Source|DBPROP_INIT_DATASOURCE|
|Data Source Name|DBPROP_DATASOURCENAME|
|Data Source Object Threading Model|DBPROP_DSOTHREADMODEL|
|DBMS Name|DBPROP_DBMSNAME|
|DBMS Version|DBPROP_DBMSVER|
|Extended Properties|DBPROP_INIT_PROVIDERSTRING|
|GROUP BY Support|DBPROP_GROUPBY|
|Heterogeneous Table Support|DBPROP_HETEROGENEOUSTABLES|
|Identifier Case Sensitivity|DBPROP_IDENTIFIERCASE|
|Initial Catalog|DBPROP_INIT_CATALOG|
|Isolation Levels|DBPROP_SUPPORTEDTXNISOLEVELS|
|Isolation Retention|DBPROP_SUPPORTEDTXNISORETAIN|
|Locale Identifier|DBPROP_INIT_LCID|
|Location|DBPROP_INIT_LOCATION|
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
|OLE DB Services|DBPROP_INIT_OLEDBSERVICES|
|OLE DB Version|DBPROP_PROVIDEROLEDBVER|
|OLE Object Support|DBPROP_OLEOBJECTS|
|Open Rowset Support|DBPROP_OPENROWSETSUPPORT|
|ORDER BY Columns in Select List|DBPROP_ORDERBYCOLUMNSINSELECT|
|Output Parameter Availability|DBPROP_OUTPUTPARAMETERAVAILABILITY|
|Password|DBPROP_AUTH_PASSWORD|
|Pass By Ref Accessors|DBPROP_BYREFACCESSORS|
|Persist Security Info|DBPROP_AUTH_PERSIST_SENSITIVE_AUTHINFO|
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
 The following properties are added to the **Recordset** object's **Properties** collection.

|ADO Property Name|OLE DB Property Name|
|-----------------------|--------------------------|
|Access Order|DBPROP_ACCESSORDER|
|Blocking Storage Objects|DBPROP_BLOCKINGSTORAGEOBJECTS|
|Bookmark Type|DBPROP_BOOKMARKTYPE|
|Bookmarkable|DBPROP_IROWSETLOCATE|
|Change Inserted Rows|DBPROP_CHANGEINSERTEDROWS|
|Column Privileges|DBPROP_COLUMNRESTRICT|
|Column Set Notification|DBPROP_NOTIFYCOLUMNSET|
|Delay Storage Object Updates|DBPROP_DELAYSTORAGEOBJECTS|
|Fetch Backwards|DBPROP_CANFETCHBACKWARDS|
|Hold Rows|DBPROP_CANHOLDROWS|
|IAccessor|DBPROP_IAccessor|
|IColumnsInfo|DBPROP_IColumnsInfo|
|IColumnsRowset|DBPROP_IColumnsRowset|
|IConnectionPointContainer|DBPROP_IConnectionPointContainer|
|IConvertType|DBPROP_IConvertType|
|Immobile Rows|DBPROP_IMMOBILEROWS|
|IRowset|DBPROP_IRowset|
|IRowsetChange|DBPROP_IRowsetChange|
|IRowsetIdentity|DBPROP_IRowsetIdentity|
|IRowsetInfo|DBPROP_IRowsetInfo|
|IRowsetLocate|DBPROP_IRowsetLocate|
|IRowsetResynch||
|IRowsetUpdate|DBPROP_IRowsetUpdate|
|ISequentialStream|DBPROP_ISequentialStream|
|ISupportErrorInfo|DBPROP_ISupportErrorInfo|
|Literal Bookmarks|DBPROP_LITERALBOOKMARKS|
|Literal Row Identity|DBPROP_LITERALIDENTITY|
|Maximum Open Rows|DBPROP_MAXOPENROWS|
|Maximum Pending Rows|DBPROP_MAXPENDINGROWS|
|Maximum Rows|DBPROP_MAXROWS|
|Notification Granularity|DBPROP_NOTIFICATIONGRANULARITY|
|Notification Phases|DBPROP_NOTIFICATIONPHASES|
|Objects Transacted|DBPROP_TRANSACTEDOBJECT|
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
|Unique Rows|DBPROP_UNIQUEROWS|
|Updatability|DBPROP_UPDATABILITY|
|Use Bookmarks|DBPROP_BOOKMARKS|

## Command Dynamic Properties
 The following properties are added to the **Command** object's **Properties** collection.

|ADO Property Name|OLE DB Property Name|
|-----------------------|--------------------------|
|Access Order|DBPROP_ACCESSORDER|
|Blocking Storage Objects|DBPROP_BLOCKINGSTORAGEOBJECTS|
|Bookmark Type|DBPROP_BOOKMARKTYPE|
|Bookmarkable|DBPROP_IROWSETLOCATE|
|Change Inserted Rows|DBPROP_CHANGEINSERTEDROWS|
|Column Privileges|DBPROP_COLUMNRESTRICT|
|Column Set Notification|DBPROP_NOTIFYCOLUMNSET|
|Delay Storage Object Updates|DBPROP_DELAYSTORAGEOBJECTS|
|Fetch Backwards|DBPROP_CANFETCHBACKWARDS|
|Hold Rows|DBPROP_CANHOLDROWS|
|IAccessor|DBPROP_IAccessor|
|IColumnsInfo|DBPROP_IColumnsInfo|
|IColumnsRowset|DBPROP_IColumnsRowset|
|IConnectionPointContainer|DBPROP_IConnectionPointContainer|
|IConvertType|DBPROP_IConvertType|
|Immobile Rows|DBPROP_IMMOBILEROWS|
|IRowset|DBPROP_IRowset|
|IRowsetChange|DBPROP_IRowsetChange|
|IRowsetIdentity|DBPROP_IRowsetIdentity|
|IRowsetInfo|DBPROP_IRowsetInfo|
|IRowsetLocate|DBPROP_IRowsetLocate|
|IRowsetResynch||
|IRowsetUpdate|DBPROP_IRowsetUpdate|
|ISequentialStream|DBPROP_ISequentialStream|
|ISupportErrorInfo|DBPROP_ISupportErrorInfo|
|Literal Bookmarks|DBPROP_LITERALBOOKMARKS|
|Literal Row Identity|DBPROP_LITERALIDENTITY|
|Maximum Open Rows|DBPROP_MAXOPENROWS|
|Maximum Pending Rows|DBPROP_MAXPENDINGROWS|
|Maximum Rows|DBPROP_MAXROWS|
|Notification Granularity|DBPROP_NOTIFICATIONGRANULARITY|
|Notification Phases|DBPROP_NOTIFICATIONPHASES|
|Objects Transacted|DBPROP_TRANSACTEDOBJECT|
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
|Skip Deleted Bookmarks|DBPROP_BOOKMARKSKIP|
|Strong Row Identity|DBPROP_STRONGIDENTITY|
|Updatability|DBPROP_UPDATABILITY|
|Use Bookmarks|DBPROP_BOOKMARKS|

 For details regarding specific implementation and functional information about the Microsoft OLE DB Provider for ODBC, see the [OLE DB Programmer's Reference](/previous-versions/windows/desktop/ms713643(v=vs.85)) or visit the Data Access and Storage Developer Center Web site on MSDN.

## See Also
 [Command Object (ADO)](../../reference/ado-api/command-object-ado.md)
 [CommandText Property (ADO)](../../reference/ado-api/commandtext-property-ado.md)
 [Connection Object (ADO)](../../reference/ado-api/connection-object-ado.md)
 [ConnectionString Property (ADO)](../../reference/ado-api/connectionstring-property-ado.md)
 [Execute Method (ADO Command)](../../reference/ado-api/execute-method-ado-command.md)
 [Open Method (ADO Recordset)](../../reference/ado-api/open-method-ado-recordset.md)
 [Parameters Collection (ADO)](../../reference/ado-api/parameters-collection-ado.md)
 [Properties Collection (ADO)](../../reference/ado-api/properties-collection-ado.md)
 [Provider Property (ADO)](../../reference/ado-api/provider-property-ado.md)
 [Recordset Object (ADO)](../../reference/ado-api/recordset-object-ado.md)
 [Supports Method](../../reference/ado-api/supports-method.md)