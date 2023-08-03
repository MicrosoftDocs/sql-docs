---
title: "Microsoft OLE DB Provider for Microsoft Indexing Service"
description: "Microsoft OLE DB Provider for Microsoft Indexing Service Overview"
author: rothja
ms.author: jroth
ms.date: 11/08/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "Indexing Service provider [ADO]"
  - "providers [ADO], OLE DB provider for Microsoft Indexing service"
  - "OLE DB provider for Microsoft Indexing service [ADO]"
---
# Microsoft OLE DB Provider for Microsoft Indexing Service Overview
The Microsoft OLE DB Provider for Microsoft Indexing Service provides programmatic read-only access to file system and Web data indexed by Microsoft Indexing Service. ADO applications can issue SQL queries to retrieve content and file property information.

 The provider is free-threaded and UNICODE enabled.

## Connection String Parameters
 To connect to this provider, set the **Provider=** argument to the [ConnectionString](../../reference/ado-api/connectionstring-property-ado.md) property to:

```vb
MSIDXS
```

 Reading the [Provider](../../reference/ado-api/provider-property-ado.md) property will return this string as well.

## Typical Connection String
 A typical connection string for this provider is:

```vb
"Provider=MSIDXS;Data Source=myCatalog;Locale Identifier=nnnn;"
```

 The string consists of these keywords:

|Keyword|Description|
|-------------|-----------------|
|**Provider**|Specifies the OLE DB Provider for Microsoft Indexing Service. Typically this is the only keyword specified in the connection string.|
|**Data Source**|Specifies the Indexing Service catalog name. If this keyword is not specified, the default system catalog is used.|
|**Locale Identifier**|Specifies a unique 32-bit number (for example, 1033) that specifies preferences related to the user's language. If this keyword is not specified, the default system locale identifier is used.|

## Command Text
 The Indexing Service SQL query syntax consists of extensions to the SQL-92 **SELECT** statement and its **FROM** and **WHERE** clauses. The results of the query are returned via OLE DB rowsets, which can be consumed by ADO and manipulated as [Recordset](../../reference/ado-api/recordset-object-ado.md) objects.

 You can search for exact words or phrases, or use wildcards to search for patterns or stems of words. The search logic can be based on Boolean decisions, weighted terms, or proximity to other words. You can also search by "free text," which finds matches based on meaning, rather than exact words.

 The specific command dialect is fully documented in the Query Languages for Indexing Service documentation.

 The provider does not accept stored procedure calls or simple table names (for example, the [CommandType](../../reference/ado-api/commandtype-property-ado.md) property will always be **adCmdText**).

## Recordset Behavior
 The following tables list the features available with a **Recordset** object opened with this provider. Only the static cursor type (**adOpenStatic**) is available.

 For more detailed information about **Recordset** behavior for your provider configuration, run the [Supports](../../reference/ado-api/supports-method.md) method and enumerate the [Properties](../../reference/ado-api/properties-collection-ado.md) collection of the **Recordset** to determine whether provider-specific dynamic properties are present.

 **Availability of standard ADO Recordset properties:**

|Property|Availability|
|--------------|------------------|
|[AbsolutePage](../../reference/ado-api/absolutepage-property-ado.md)|read/write|
|[AbsolutePosition](../../reference/ado-api/absoluteposition-property-ado.md)|read/write|
|[ActiveConnection](../../reference/ado-api/activeconnection-property-ado.md)|read-only|
|[BOF](../../reference/ado-api/bof-eof-properties-ado.md)|read-only|
|[Bookmark](../../reference/ado-api/bookmark-property-ado.md)*|read/write|
|[CacheSize](../../reference/ado-api/cachesize-property-ado.md)|read/write|
|[CursorLocation](../../reference/ado-api/cursorlocation-property-ado.md)|always **adUseServer**|
|[CursorType](../../reference/ado-api/cursortype-property-ado.md)|always **adOpenStatic**|
|[EditMode](../../reference/ado-api/editmode-property.md)|always **adEditNone**|
|[EOF](../../reference/ado-api/bof-eof-properties-ado.md)|read-only|
|[Filter](../../reference/ado-api/filter-property.md)|read/write|
|[LockType](../../reference/ado-api/locktype-property-ado.md)|read/write|
|[MarshalOptions](../../reference/ado-api/marshaloptions-property-ado.md)|not available|
|[MaxRecords](../../reference/ado-api/maxrecords-property-ado.md)|read/write|
|[PageCount](../../reference/ado-api/pagecount-property-ado.md)|read-only|
|[PageSize](../../reference/ado-api/pagesize-property-ado.md)|read/write|
|[RecordCount](../../reference/ado-api/recordcount-property-ado.md)|read-only|
|[Source](../../reference/ado-api/source-property-ado-recordset.md)|read/write|
|[State](../../reference/ado-api/state-property-ado.md)|read-only|
|[Status](../../reference/ado-api/status-property-ado-recordset.md)|read-only|

 \*Bookmarks must be enabled on the provider in order for this feature to exist on the **Recordset**.

 **Availability of standard ADO Recordset methods:**

|Method|Available?|
|------------|----------------|
|[AddNew](../../reference/ado-api/addnew-method-ado.md)|No|
|[Cancel](../../reference/ado-api/cancel-method-ado.md)|Yes|
|[CancelBatch](../../reference/ado-api/cancelbatch-method-ado.md)|No|
|[CancelUpdate](../../reference/ado-api/cancelupdate-method-ado.md)|No|
|[Clone](../../reference/ado-api/clone-method-ado.md)|Yes|
|[Close](../../reference/ado-api/close-method-ado.md)|Yes|
|[Delete](../../reference/ado-api/delete-method-ado-recordset.md)|No|
|[GetRows](../../reference/ado-api/getrows-method-ado.md)|Yes|
|[Move](../../reference/ado-api/move-method-ado.md)|Yes|
|[MoveFirst](../../reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Yes|
|[NextRecordset](../../reference/ado-api/nextrecordset-method-ado.md)|Yes|
|[Open](../../reference/ado-api/open-method-ado-recordset.md)|Yes|
|[Requery](../../reference/ado-api/requery-method.md)|Yes|
|[Resync](../../reference/ado-api/resync-method.md)|Yes|
|[Supports](../../reference/ado-api/supports-method.md)|Yes|
|[Update](../../reference/ado-api/update-method.md)|No|
|[UpdateBatch](../../reference/ado-api/updatebatch-method.md)|No|

 For specific implementation details and functional information about the Microsoft OLE DB Provider for Microsoft Indexing Service, consult the [OLE DB Programmer's Guide](/previous-versions/windows/desktop/ms713643(v=vs.85)), or visit the Web Services page of the Windows NT Server Web site.

## See Also
 [CommandType Property (ADO)](../../reference/ado-api/commandtype-property-ado.md)
 [ConnectionString Property (ADO)](../../reference/ado-api/connectionstring-property-ado.md)
 [Properties Collection (ADO)](../../reference/ado-api/properties-collection-ado.md)
 [Provider Property (ADO)](../../reference/ado-api/provider-property-ado.md)
 [Recordset Object (ADO)](../../reference/ado-api/recordset-object-ado.md)
 [Supports Method](../../reference/ado-api/supports-method.md)