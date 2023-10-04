---
title: "ADO Dynamic Property Index"
description: "ADO Dynamic Property Index"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "dynamic properties [ADO], index"
---
# ADO Dynamic Property Index
Data providers, service providers, and service components can add dynamic properties to the **Properties** collections of the unopened [Connection](./connection-object-ado.md) and [Recordset](./recordset-object-ado.md) objects. A given provider may also insert additional properties when these objects are opened. Some of these properties are listed in the [ADO Dynamic Properties](./ado-dynamic-properties.md) section. More are listed under the specific providers in the [Appendix A: Providers](../../guide/appendixes/appendix-a-providers.md) section.  
  
 The following tables are cross-indexes of the ADO and OLE DB names for each standard OLE DB provider dynamic property. Your providers may add more properties than listed here. For the specific information about provider-specific dynamic properties, see your provider documentation.  
  
 The OLE DB Programmer's Reference refers to an ADO property name by the term "Description." For more information about these standard properties, search or browse the index in the [OLE DB documentation](/previous-versions/windows/desktop/ms722784(v=vs.85))for the OLE DB property by its name.  
  
## Connection Dynamic Properties  
  
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
 Note that the **Dynamic Properties** of the **Recordset** object go out of scope (become unavailable) when the **Recordset** is closed.  
  
|ADO Property Name|OLE DB Property Name|  
|-----------------------|--------------------------|  
|IAccessor|DBPROP_IACCESSOR|  
|IChapteredRowset||  
|IColumnsInfo|DBPROP_ICOLUMNSINFO|  
|IColumnsRowset|DBPROP_ICOLUMNSROWSET|  
|IConnectionPointContainer|DBPROP_ICONNECTIONPOINTCONTAINER|  
|IConvertType||  
|ILockBytes|DBPROP_ILOCKBYTES|  
|IRowset|DBPROP_IROWSET|  
|IDBAsynchStatus|DBPROP_IDBASYNCHSTATUS|  
|IParentRowset||  
|IRowsetChange|DBPROP_IROWSETCHANGE|  
|IRowsetExactScroll||  
|IRowsetFind|DBPROP_IROWSETFIND|  
|IRowsetIdentity|DBPROP_IROWSETIDENTITY|  
|IRowsetInfo|DBPROP_IROWSETINFO|  
|IRowsetLocate|DBPROP_IROWSETLOCATE|  
|IRowsetRefresh|DBPROP_IROWSETREFRESH|  
|IRowsetResynch||  
|IRowsetScroll|DBPROP_IROWSETSCROLL|  
|IRowsetUpdate|DBPROP_IROWSETUPDATE|  
|IRowsetView|DBPROP_IROWSETVIEW|  
|IRowsetIndex|DBPROP_IROWSETINDEX|  
|ISequentialStream|DBPROP_ISEQUENTIALSTREAM|  
|IStorage|DBPROP_ISTORAGE|  
|IStream|DBPROP_ISTREAM|  
|ISupportErrorInfo|DBPROP_ISUPPORTERRORINFO|  
|Access Order|DBPROP_ACCESSORDER|  
|Append-Only Rowset|DBPROP_APPENDONLY|  
|Asynchronous Rowset Processing|DBPROP_ROWSET_ASYNCH|  
|Auto Recalc|DBPROP_ADC_AUTORECALC|  
|Background Fetch Size|DBPROP_ASYNCHFETCHSIZE|  
|Background Thread Priority|DBPROP_ASYNCHTHREADPRIORITY|  
|Batch Size|DBPROP_ADC_BATCHSIZE|  
|Blocking Storage Objects|DBPROP_BLOCKINGSTORAGEOBJECTS|  
|Bookmark Type|DBPROP_BOOKMARKTYPE|  
|Bookmarkable|DBPROP_IROWSETLOCATE|  
|Bookmarks Ordered|DBPROP_ORDEREDBOOKMARKS|  
|Cache Child Rows|DBPROP_ADC_CACHECHILDROWS|  
|Cache Deferred Columns|DBPROP_CACHEDEFERRED|  
|Change Inserted Rows|DBPROP_CHANGEINSERTEDROWS|  
|Column Privileges|DBPROP_COLUMNRESTRICT|  
|Column Set Notification|DBPROP_NOTIFYCOLUMNSET|  
|Column Writable|DBPROP_MAYWRITECOLUMN|  
|Command Time Out|DBPROP_COMMANDTIMEOUT|  
|Cursor Engine Version|DBPROP_ADC_CEVER|  
|Defer Column|DBPROP_DEFERRED|  
|Delay Storage Object Updates|DBPROP_DELAYSTORAGEOBJECTS|  
|Fetch Backwards|DBPROP_CANFETCHBACKWARDS|  
|Filter Operations|DBPROP_FILTERCOMPAREOPS|  
|Find Operations|DBPROP_FINDCOMPAREOPS|  
|Hidden Columns (Count)|DBPROP_HIDDENCOLUMNS|  
|Hold Rows|DBPROP_CANHOLDROWS|  
|Immobile Rows|DBPROP_IMMOBILEROWS|  
|Initial Fetch Size|DBPROP_ASYNCHPREFETCHSIZE|  
|Literal Bookmarks|DBPROP_LITERALBOOKMARKS|  
|Literal Row Identity|DBPROP_LITERALIDENTITY|  
|Maintain Change Status|DBPROP_ADC_MAINTAINCHANGESTATUS|  
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
|Private1||  
|Quick Restart|DBPROP_QUICKRESTART|  
|Reentrant Events|DBPROP_REENTRANTEVENTS|  
|Remove Deleted Rows|DBPROP_REMOVEDELETED|  
|Report Multiple Changes|DBPROP_REPORTMULTIPLECHANGES|  
|Reshape Name|DBPROP_ADC_RESHAPENAME|  
|Resync Command|DBPROP_ADC_CUSTOMRESYNCH|  
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
|Skip Deleted Bookmarks|DBPROP_BOOKMARKSKIPPED|  
|Strong Row Identity|DBPROP_STRONGIDENTITY|  
|Unique Catalog|DBPROP_ADC_UNIQUECATALOG|  
|Unique Rows|DBPROP_UNIQUEROWS|  
|Unique Schema|DBPROP_ADC_UNIQUESCHEMA|  
|Unique Table|DBPROP_ADC_UNIQUETABLE|  
|Updatability|DBPROP_UPDATABILITY|  
|Update Criteria|DBPROP_ADC_UPDATECRITERIA|  
|Update Resync|DBPROP_ADC_UPDATERESYNC|  
|Use Bookmarks|DBPROP_BOOKMARKS|