---
title: "Microsoft OLE DB Provider for Microsoft Indexing Service | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/08/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords:
  - "Indexing Service provider [ADO]"
  - "providers [ADO], OLE DB provider for Microsoft Indexing service"
  - "OLE DB provider for Microsoft Indexing service [ADO]"
ms.assetid: f86a0598-5097-471b-8318-d2c859d085f2
author: MightyPen
ms.author: genemi
manager: craigg
---
# Microsoft OLE DB Provider for Microsoft Indexing Service Overview
The Microsoft OLE DB Provider for Microsoft Indexing Service provides programmatic read-only access to file system and Web data indexed by Microsoft Indexing Service. ADO applications can issue SQL queries to retrieve content and file property information.

 The provider is free-threaded and UNICODE enabled.

## Connection String Parameters
 To connect to this provider, set the **Provider=** argument to the [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) property to:

```vb
MSIDXS
```

 Reading the [Provider](../../../ado/reference/ado-api/provider-property-ado.md) property will return this string as well.

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
 The Indexing Service SQL query syntax consists of extensions to the SQL-92 **SELECT** statement and its **FROM** and **WHERE** clauses. The results of the query are returned via OLE DB rowsets, which can be consumed by ADO and manipulated as [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) objects.

 You can search for exact words or phrases, or use wildcards to search for patterns or stems of words. The search logic can be based on Boolean decisions, weighted terms, or proximity to other words. You can also search by "free text," which finds matches based on meaning, rather than exact words.

 The specific command dialect is fully documented in the Query Languages for Indexing Service documentation.

 The provider does not accept stored procedure calls or simple table names (for example, the [CommandType](../../../ado/reference/ado-api/commandtype-property-ado.md) property will always be **adCmdText**).

## Recordset Behavior
 The following tables list the features available with a **Recordset** object opened with this provider. Only the static cursor type (**adOpenStatic**) is available.

 For more detailed information about **Recordset** behavior for your provider configuration, run the [Supports](../../../ado/reference/ado-api/supports-method.md) method and enumerate the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection of the **Recordset** to determine whether provider-specific dynamic properties are present.

 **Availability of standard ADO Recordset properties:**

|Property|Availability|
|--------------|------------------|
|[AbsolutePage](../../../ado/reference/ado-api/absolutepage-property-ado.md)|read/write|
|[AbsolutePosition](../../../ado/reference/ado-api/absoluteposition-property-ado.md)|read/write|
|[ActiveConnection](../../../ado/reference/ado-api/activeconnection-property-ado.md)|read-only|
|[BOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md)|read-only|
|[Bookmark](../../../ado/reference/ado-api/bookmark-property-ado.md)*|read/write|
|[CacheSize](../../../ado/reference/ado-api/cachesize-property-ado.md)|read/write|
|[CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md)|always **adUseServer**|
|[CursorType](../../../ado/reference/ado-api/cursortype-property-ado.md)|always **adOpenStatic**|
|[EditMode](../../../ado/reference/ado-api/editmode-property.md)|always **adEditNone**|
|[EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md)|read-only|
|[Filter](../../../ado/reference/ado-api/filter-property.md)|read/write|
|[LockType](../../../ado/reference/ado-api/locktype-property-ado.md)|read/write|
|[MarshalOptions](../../../ado/reference/ado-api/marshaloptions-property-ado.md)|not available|
|[MaxRecords](../../../ado/reference/ado-api/maxrecords-property-ado.md)|read/write|
|[PageCount](../../../ado/reference/ado-api/pagecount-property-ado.md)|read-only|
|[PageSize](../../../ado/reference/ado-api/pagesize-property-ado.md)|read/write|
|[RecordCount](../../../ado/reference/ado-api/recordcount-property-ado.md)|read-only|
|[Source](../../../ado/reference/ado-api/source-property-ado-recordset.md)|read/write|
|[State](../../../ado/reference/ado-api/state-property-ado.md)|read-only|
|[Status](../../../ado/reference/ado-api/status-property-ado-recordset.md)|read-only|

 \*Bookmarks must be enabled on the provider in order for this feature to exist on the **Recordset**.

 **Availability of standard ADO Recordset methods:**

|Method|Available?|
|------------|----------------|
|[AddNew](../../../ado/reference/ado-api/addnew-method-ado.md)|No|
|[Cancel](../../../ado/reference/ado-api/cancel-method-ado.md)|Yes|
|[CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md)|No|
|[CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md)|No|
|[Clone](../../../ado/reference/ado-api/clone-method-ado.md)|Yes|
|[Close](../../../ado/reference/ado-api/close-method-ado.md)|Yes|
|[Delete](../../../ado/reference/ado-api/delete-method-ado-recordset.md)|No|
|[GetRows](../../../ado/reference/ado-api/getrows-method-ado.md)|Yes|
|[Move](../../../ado/reference/ado-api/move-method-ado.md)|Yes|
|[MoveFirst](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Yes|
|[NextRecordset](../../../ado/reference/ado-api/nextrecordset-method-ado.md)|Yes|
|[Open](../../../ado/reference/ado-api/open-method-ado-recordset.md)|Yes|
|[Requery](../../../ado/reference/ado-api/requery-method.md)|Yes|
|[Resync](../../../ado/reference/ado-api/resync-method.md)|Yes|
|[Supports](../../../ado/reference/ado-api/supports-method.md)|Yes|
|[Update](../../../ado/reference/ado-api/update-method.md)|No|
|[UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md)|No|

 For specific implementation details and functional information about the Microsoft OLE DB Provider for Microsoft Indexing Service, consult the [OLE DB Programmer's Guide](https://msdn.microsoft.com/library/windows/desktop/ms713643.aspx), or visit the Web Services page of the Windows NT Server Web site.

## See Also
 [CommandType Property (ADO)](../../../ado/reference/ado-api/commandtype-property-ado.md)
 [ConnectionString Property (ADO)](../../../ado/reference/ado-api/connectionstring-property-ado.md)
 [Properties Collection (ADO)](../../../ado/reference/ado-api/properties-collection-ado.md)
 [Provider Property (ADO)](../../../ado/reference/ado-api/provider-property-ado.md)
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)
 [Supports Method](../../../ado/reference/ado-api/supports-method.md)
