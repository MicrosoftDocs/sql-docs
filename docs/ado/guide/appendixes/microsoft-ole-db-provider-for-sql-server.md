---
title: "Microsoft OLE DB Provider for SQL Server | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "providers [ADO], OLE DB provider for SQL Server"
  - "OLE DB provider for SQL Server [ADO]"
  - "SQLOLEDB [ADO]"
ms.assetid: 99bc40c4-9181-4ca1-a06f-9a1a914a0b7b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft OLE DB Provider for SQL Server Overview
The Microsoft OLE DB Provider for SQL Server, SQLOLEDB, allows ADO to access Microsoft SQL Server.

**NOTE:**  It is not recommended to use this driver for new development. The new OLE DB provider is called the [Microsoft OLE DB Driver for SQL Server](../../../connect/oledb/oledb-driver-for-sql-server.md) (MSOLEDBSQL) which will be updated with the most recent server features going forward.

## Connection String Parameters
 To connect to this provider, set the *Provider* argument to the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property to:

```vb
SQLOLEDB
```

 This value can also be set or read using the [Provider](../../../ado/reference/ado-api/provider-property-ado.md) property.

## Typical Connection String
 A typical connection string for this provider is:

```vb
"Provider=SQLOLEDB;Data Source=serverName;"
Initial Catalog=databaseName;
User ID=MyUserID;Password=MyPassword;"
```

 The string consists of these keywords:

|Keyword|Description|
|-------------|-----------------|
|**Provider**|Specifies the OLE DB Provider for SQL Server.|
|**Data Source** or **Server**|Specifies the name of a server.|
|**Initial Catalog** or **Database**|Specifies the name of a database on the server.|
|**User ID** or **uid**|Specifies the user name (for SQL Server Authentication).|
|**Password** or **pwd**|Specifies the user password (for SQL Server Authentication).|

> [!NOTE]
>  If you are connecting to a data source provider that supports Windows authentication, you should specify **Trusted_Connection=yes** or **Integrated Security = SSPI** instead of user ID and password information in the connection string.

## Provider-Specific Connection Parameters
 The provider supports several provider-specific connection parameters in addition to those defined by ADO. As with the ADO connection properties, these provider-specific properties can be set via the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection of a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) or can be set as part of the **ConnectionString**.

|Parameter|Description|
|---------------|-----------------|
|Trusted_Connection|Indicates the user authentication mode. This can be set to **Yes** or **No**. The default value is **No**. If this property is set to **Yes**, SQLOLEDB uses Microsoft Windows NT Authentication Mode to authorize user access to the SQL Server database specified by the **Location** and [Datasource](../../../ado/reference/ado-api/datasource-property-ado.md) property values. If this property is set to **No**, SQLOLEDB uses Mixed Mode to authorize user access to the SQL Server database. The SQL Server login and password are specified in the **User Id** and **Password** properties.|
|Current Language|Indicates a SQL Server language name. Identifies the language used for system message selection and formatting. The language must be installed on the SQL Server, otherwise opening the connection will fail.|
|Network Address|Indicates the network address of the SQL Server specified by the **Location** property.|
|Network Library|Indicates the name of the network library (DLL) used to communicate with the SQL Server. The name should not include the path or the .dll file name extension. The default is provided by the SQL Server client configuration.|
|Use Procedure for Prepare|Determines whether SQL Server creates temporary stored procedures when Commands are prepared (by the **Prepared** property).|
|Auto Translate|Indicates whether OEM/ANSI characters are converted. This property can be set to **True** or **False**. The default value is **True**. If this property is set to **True**, SQLOLEDB performs OEM/ANSI character conversion when multi-byte character strings are retrieved from, or sent to, the SQL Server. If this property is set to **False**, SQLOLEDB does not perform OEM/ANSI character conversion on multi-byte character string data.|
|Packet Size|Indicates a network packet size in bytes. The packet size property value must be between 512 and 32767. The default SQLOLEDB network packet size is 4096.|
|Application Name|Indicates the client application name.|
|Workstation ID|A string identifying the workstation.|

## Command Object Usage
 SQLOLEDB accepts an amalgam of ODBC, ANSI, and SQL Server-specific Transact-SQL as valid syntax. For example, the following SQL statement uses an ODBC SQL escape sequence to specify the LCASE string function:

```sql
SELECT customerid={fn LCASE(CustomerID)} FROM Customers

```

 LCASE returns a character string, converting all uppercase characters to their lowercase equivalents. The ANSI SQL string function LOWER performs the same operation, so the following SQL statement is an ANSI equivalent to the ODBC statement presented earlier:

```sql
SELECT customerid=LOWER(CustomerID) FROM Customers

```

 SQLOLEDB successfully processes either form of the statement when specified as text for a command.

## Stored Procedures
 When executing a SQL Server stored procedure using a SQLOLEDB command, use the ODBC procedure call escape sequence in the command text. SQLOLEDB then uses the remote procedure call mechanism of SQL Server to optimize command processing. For example, the following ODBC SQL statement is the preferred command text over the Transact-SQL form:

## ODBC SQL

```vb
{call SalesByCategory('Produce', '1995')}

```

## Transact-SQL

```sql
EXECUTE SalesByCategory 'Produce', '1995'

```

## SQL Server Features
 With SQL Server, ADO can use XML for **Command** input and retrieve results in XML stream format instead of in **Recordset** objects. For more information, see [Using Streams for Command Input](../../../ado/guide/data/command-streams.md) and [Retrieving Resultsets Into Streams](../../../ado/guide/data/retrieving-resultsets-into-streams.md).

### Accessing sql_variant data using MDAC 2.7, MDAC 2.8, or Windows DAC 6.0
 Microsoft SQL Server has a data type called **sql_variant**. Similar to OLE DB's **DBTYPE_VARIANT**, the **sql_variant** data type can store data of several different types. However, there are a few key differences between **DBTYPE_VARIANT** and **sql_variant**. ADO also handles data stored as a **sql_variant** value differently than how it handles other data types. The following list describes issues to consider when you access SQL Server data stored in columns of type **sql_variant**.

-   In MDAC 2.7, MDAC 2.8, and Windows Data Access Components (Windows DAC) 6.0, the OLE DB Provider for SQL Server supports the **sql_variant** type. The OLE DB Provider for ODBC does not.

-   The **sql_variant** type does not exactly match the **DBTYPE_VARIANT** data type.  The **sql_variant** type supports a few new subtypes not supported by **DBTYPE_VARIANT,** including **GUID**, **ANSI** (non-UNICODE) strings, and **BIGINT**. Using subtypes other than those listed earlier will work correctly.

-   The **sql_variant** subtype **NUMERIC** does not match the **DBTYPE_DECIMAL** in size.

-   Multiple data type coercions will result in types that do not match. For example, coercing a **sql_variant** with a subtype of **GUID** to a **DBTYPE_VARIANT** will result in a subtype of **safearray**(bytes). Converting this type back to a **sql_variant** will result in a new subtype of **array**(bytes).

-   **Recordset** fields that contain **sql_variant** data can be remoted (marshaled) or persisted only if the **sql_variant** contains specific subtypes. Attempting to remote or persist data with the following unsupported subtypes will cause a run-time error (unsupported conversion) from the Microsoft Persistence Provider (MSPersist): **VT_VARIANT**, **VT_RECORD**, **VT_ILLEGAL**, **VT_UNKNOWN**, **VT_BSTR**, and **VT_DISPATCH.**

-   The OLE DB Provider for SQL Server in MDAC 2.7, MDAC 2.8, and Windows DAC 6.0 has a dynamic property called **Allow Native Variants** which, as the name implies, allows developers to access the **sql_variant** in its native form as opposed to a **DBTYPE_VARIANT**. If this property is set, and a **Recordset** is opened with the Client Cursor Engine (**adUseClient**), the **Recordset.Open** call will fail. If this property is set and a **Recordset** is opened with server cursors (**adUseServer**), the **Recordset.Open** call will succeed, but accessing columns of type **sql_variant** will produce an error.

-   In client applications that use MDAC 2.5, **sql_variant** data can be used with queries against Microsoft SQL Server. However, the values of the **sql_variant** data are treated as strings. Such client applications should be upgraded to MDAC 2.7, MDAC 2.8, or Windows DAC 6.0.

## Recordset Behavior
 SQLOLEDB cannot use SQL Server cursors to support the multiple-result generated by many commands. If a consumer requests a recordset requiring SQL Server cursor support, an error occurs if the command text used generates more than a single recordset as its result.

 Scrollable SQLOLEDB recordsets are supported by SQL Server cursors. SQL Server imposes limitations on cursors that are sensitive to changes made by other users of the database. Specifically, the rows in some cursors cannot be ordered, and attempting to create a recordset using a command containing an SQL ORDER BY clause can fail.

## Dynamic Properties
 The Microsoft OLE DB Provider for SQL Server inserts several dynamic properties into the **Properties** collection of the unopened [Connection](../../../ado/reference/ado-api/connection-object-ado.md), [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md), and [Command](../../../ado/reference/ado-api/command-object-ado.md) objects.

 The following tables are a cross-index of the ADO and OLE DB names for each dynamic property. The OLE DB Programmer's Reference refers to an ADO property name by the term "Description." You can find more information about these properties in the OLE DB Programmer's Reference. Search for the OLE DB property name in the Index or see [Appendix C: OLE DB Properties](https://msdn.microsoft.com/deded3ff-f508-4e1b-b2b1-fd9afd3bd292).

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
|Maximum Index Size|DBPROP_MAXINDEXSIZE|
|Maximum Row Size|DBPROP_MAXROWSIZE|
|Maximum Row Size Includes BLOB|DBPROP_MAXROWSIZEINCLUDESBLOB|
|Maximum Tables in SELECT|DBPROP_MAXTABLESINSELECT|
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
 The following properties are added to the **Properties** collection of the **Recordset** object.

|ADO Property Name|OLE DB Property Name|
|-----------------------|--------------------------|
|Access Order|DBPROP_ACCESSORDER|
|Blocking Storage Objects|DBPROP_BLOCKINGSTORAGEOBJECTS|
|Bookmark Type|DBPROP_BOOKMARKTYPE|
|Bookmarkable|DBPROP_IROWSETLOCATE|
|Change Inserted Rows|DBPROP_CHANGEINSERTEDROWS|
|Column Privileges|DBPROP_COLUMNRESTRICT|
|Column Set Notification|DBPROP_NOTIFYCOLUMNSET|
|Command Time Out|DBPROP_COMMANDTIMEOUT|
|Defer Column|DBPROP_DEFERRED|
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
|IRowsetLocate|DBPROP_IRowsestLocate|
|IRowsetResynch||
|IRowsetScroll|DBPROP_IRowsetScroll|
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
|Server Cursor|DBPROP_SERVERCURSOR|
|Skip Deleted Bookmarks|DBPROP_BOOKMARKSKIPPED|
|Strong Row Identity|DBPROP_STRONGITDENTITY|
|Unique Rows|DBPROP_UNIQUEROWS|
|Updatability|DBPROP_UPDATABILITY|
|Use Bookmarks|DBPROP_BOOKMARKS|

## Command Dynamic Properties
 The following properties are added to the **Properties** collection of the **Command** object.

|ADO Property Name|OLE DB Property Name|
|-----------------------|--------------------------|
|Access Order|DBPROP_ACCESSORDER|
|Base Path|SSPROP_STREAM_BASEPATH|
|Blocking Storage Objects|DBPROP_BLOCKINGSTORAGEOBJECTS|
|Bookmark Type|DBPROP_BOOKMARKTYPE|
|Bookmarkable|DBPROP_IROWSETLOCATE|
|Change Inserted Rows|DBPROP_CHANGEINSERTEDROWS|
|Column Privileges|DBPROP_COLUMNRESTRICT|
|Column Set Notification|DBPROP_NOTIFYCOLUMNSET|
|Content Type|SSPROP_STREAM_CONTENTTYPE|
|Cursor Auto Fetch|SSPROP_CURSORAUTOFETCH|
|Defer Column|DBPROP_DEFERRED|
|Defer Prepare|SSPROP_DEFERPREPARE|
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
|IRowsetResynch|DBPROP_IRowsetResynch|
|IRowsetScroll|DBPROP_IRowsetScroll|
|IRowsetUpdate|DBPROP_IRowsetUpdate|
|ISequentialStream|DBPROP_ISequentialStream|
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
|Output Encoding Property|DBPROP_OUTPUTENCODING|
|Output Stream Property|DBPROP_OUTPUTSTREAM|
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
|Server Cursor|DBPROP_SERVERCURSOR|
|Server Data on Insert|DBPROP_SERVERDATAONINSERT|
|Skip Deleted Bookmarks|DBPROP_BOOKMARKSKIP|
|Strong Row Identity|DBPROP_STRONGIDENTITY|
|Updatability|DBPROP_UPDATABILITY|
|Use Bookmarks|DBPROP_BOOKMARKS|
|XML Root|SSPROP_STREAM_XMLROOT|
|XSL|SSPROP_STREAM_XSL|

 For specific implementation details and functional information about the Microsoft SQL Server OLE DB Provider, see the [SQL Server Provider](https://msdn.microsoft.com/adf1d6c4-5930-444a-9248-ff1979729635).

## See Also
 [ConnectionString Property (ADO)](../../../ado/reference/ado-api/connectionstring-property-ado.md)
 [Provider Property (ADO)](../../../ado/reference/ado-api/provider-property-ado.md)
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
