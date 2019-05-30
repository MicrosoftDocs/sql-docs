---
title: "Open Method (ADO Recordset) | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
apitype: "COM"
f1_keywords: 
  - "Recordset15::raw_Open"
  - "Recordset15::Open"
helpviewer_keywords: 
  - "Open method [ADO]"
ms.assetid: 3236749c-4b71-4235-89e2-ccdfaaa9319d
author: MightyPen
ms.author: genemi
manager: craigg
---
# Open Method (ADO Recordset)
Opens a cursor on a [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md) object.  
  
## Syntax  
  
```  
  
recordset.Open Source, ActiveConnection, CursorType, LockType, Options  
```  
  
#### Parameters  
 *Source*  
 Optional. A **Variant** that evaluates to a valid [Command](../../../ado/reference/ado-api/command-object-ado.md) object, an SQL statement, a table name, a stored procedure call, a URL, or the name of a file or [Stream](../../../ado/reference/ado-api/stream-object-ado.md) object containing a persistently stored [Recordset](../../../ado/reference/ado-api/recordset-object-ado.md).  
  
 *ActiveConnection*  
 Optional. Either a **Variant** that evaluates to a valid [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object variable name, or a **String** that contains [ConnectionString](../../../ado/reference/ado-api/connectionstring-property-ado.md) parameters.  
  
 *CursorType*  
 Optional. A [CursorTypeEnum](../../../ado/reference/ado-api/cursortypeenum.md) value that determines the type of cursor that the provider should use when opening the **Recordset**. The default value is **adOpenForwardOnly**.  
  
 *LockType*  
 Optional. A [LockTypeEnum](../../../ado/reference/ado-api/locktypeenum.md) value that determines what type of locking (concurrency) the provider should use when opening the **Recordset**. The default value is **adLockReadOnly**.  
  
 *Options*  
 Optional. A **Long** value that indicates how the provider should evaluate the *Source* argument if it represents something other than a **Command** object, or that the **Recordset** should be restored from a file where it was previously saved. Can be one or more [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) or [ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md) values, which can be combined with a bitwise OR operator.  
  
> [!NOTE]
>  If you open a **Recordset** from a **Stream** containing a persisted **Recordset**, using an [ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md) value of **adAsyncFetchNonBlocking** will have no effect; the fetch will be synchronous and blocking.  
  
> [!NOTE]
>  The **ExecuteOpenEnum** values of **adExecuteNoRecords** or **adExecuteStream** should not be used with **Open**.  
  
## Remarks  
 The default cursor for an ADO **Recordset** is a forward-only, read-only cursor located on the server.  
  
 Using the **Open** method on a **Recordset** object opens a cursor that represents records from a base table, the results of a query, or a previously saved **Recordset**.  
  
 Use the optional *Source* argument to specify a data source using one of the following: a **Command** object variable, an SQL statement, a stored procedure, a table name, a URL, or a complete file path name. If *Source* is a file path name, it can be a full path ("c:\dir\file.rst"), a relative path ("..\file.rst"), or a URL ("<https://files/file.rst>").  
  
 It is not a good idea to use the *Source* argument of the **Open** method to perform an action query that does not return records because there is no easy way to determine whether the call succeeded. The **Recordset** returned by such a query will be closed. To perform a query that does not return records, such as a SQL INSERT statement, call the [Execute](../../../ado/reference/ado-api/execute-method-ado-command.md) method of a **Command** object or the [Execute](../../../ado/reference/ado-api/execute-method-ado-connection.md) method of a [Connection](../../../ado/reference/ado-api/connection-object-ado.md) object instead.  
  
 The *ActiveConnection* argument corresponds to the [ActiveConnection](../../../ado/reference/ado-api/activeconnection-property-ado.md) property and specifies in which connection to open the **Recordset** object. If you pass a connection definition for this argument, ADO opens a new connection using the specified parameters. After you open the **Recordset** with a client-side cursor by setting the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to **adUseClient**, you can change the value of this property to send updates to another provider. Or you can set this property to **Nothing** (in Microsoft Visual Basic) or NULL to disconnect the **Recordset** from any provider. Changing *ActiveConnection* for a server-side cursor generates an error, however.  
  
 For the other arguments that correspond directly to properties of a **Recordset** object (*Source*, *CursorType*, and *LockType*), the relationship of the arguments to the properties is as follows:  
  
-   The property is read/write before the **Recordset** object is opened.  
  
-   The property settings are used unless you pass the corresponding arguments when executing the **Open** method. If you pass an argument, it overrides the corresponding property setting, and the property setting is updated with the argument value.  
  
-   After you open the **Recordset** object, these properties become read-only.  
  
> [!NOTE]
>  The **ActiveConnection** property is read-only for **Recordset** objects whose [Source](../../../ado/reference/ado-api/source-property-ado-recordset.md) property is set to a valid **Command** object, even if the **Recordset** object is not open.  
  
 If you pass a **Command** object in the *Source* argument and also pass an *ActiveConnection* argument, an error occurs. The **ActiveConnection** property of the **Command** object must already be set to a valid **Connection** object or connection string.  
  
 If you pass something other than a **Command** object in the *Source* argument, you can use the *Options* argument to optimize evaluation of the *Source* argument. If the *Options* argument is not defined, you may experience diminished performance because ADO must make calls to the provider to determine if the argument is an SQL statement, a stored procedure, a URL, or a table name. If you know what *Source* type you are using, setting the *Options* argument instructs ADO to jump directly to the relevant code. If the *Options* argument does not match the *Source* type, an error occurs.  
  
 If you pass a **Stream** object in the *Source* argument, you should not pass information into the other arguments. Doing so will generate an error. The **ActiveConnection** information is not retained when a **Recordset** is opened from a **Stream**.  
  
 The default for the *Options* argument is **adCmdFile** if no connection is associated with the **Recordset**. This will typically be the case for persistently stored **Recordset** objects.  
  
 If the data source returns no records, the provider sets both the [BOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) and [EOF](../../../ado/reference/ado-api/bof-eof-properties-ado.md) properties to **True**, and the current record position is undefined. You can still add new data to this empty **Recordset** object if the cursor type allows it.  
  
 When you have concluded your operations over an open **Recordset** object, use the [Close](../../../ado/reference/ado-api/close-method-ado.md) method to free any associated system resources. Closing an object does not remove it from memory; you can change its property settings and use the **Open** method to open it again later. To completely eliminate an object from memory, set the object variable to *Nothing*.  
  
 Before the **ActiveConnection** property is set, call **Open** with no operands to create an instance of a **Recordset** created by appending fields to the **Recordset** [Fields](../../../ado/reference/ado-api/fields-collection-ado.md) collection.  
  
 If you have set the [CursorLocation](../../../ado/reference/ado-api/cursorlocation-property-ado.md) property to **adUseClient**, you can retrieve rows asynchronously in one of two ways. The recommended method is to set *Options* to **adAsyncFetch**. Alternatively, you can use the "Asynchronous Rowset Processing" dynamic property in the [Properties](../../../ado/reference/ado-api/properties-collection-ado.md) collection, but related retrieved events can be lost if you do not set the *Options* parameter to **adAsyncFetch**.  
  
> [!NOTE]
>  Background fetching in the MS Remote provider is supported only through the **Open** method's *Options* parameter.  
  
> [!NOTE]
>  URLs using the http scheme will automatically invoke the [Microsoft OLE DB Provider for Internet Publishing](../../../ado/guide/appendixes/microsoft-ole-db-provider-for-internet-publishing.md). For more information, see [Absolute and Relative URLs](../../../ado/guide/data/absolute-and-relative-urls.md).  
  
 Certain combinations of [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md) and [ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md) values are not valid. For information about which options cannot be combined, see the topics for the [ExecuteOptionEnum](../../../ado/reference/ado-api/executeoptionenum.md), and [CommandTypeEnum](../../../ado/reference/ado-api/commandtypeenum.md).  
  
## Applies To  
 [Recordset Object (ADO)](../../../ado/reference/ado-api/recordset-object-ado.md)  
  
## See Also  
 [Open and Close Methods Example (VB)](../../../ado/reference/ado-api/open-and-close-methods-example-vb.md)   
 [Open and Close Methods Example (VBScript)](../../../ado/reference/ado-api/open-and-close-methods-example-vbscript.md)   
 [Open and Close Methods Example (VC++)](../../../ado/reference/ado-api/open-and-close-methods-example-vc.md)   
 [Save and Open Methods Example (VB)](../../../ado/reference/ado-api/save-and-open-methods-example-vb.md)   
 [Open Method (ADO Connection)](../../../ado/reference/ado-api/open-method-ado-connection.md)   
 [Open Method (ADO Record)](../../../ado/reference/ado-api/open-method-ado-record.md)   
 [Open Method (ADO Stream)](../../../ado/reference/ado-api/open-method-ado-stream.md)   
 [OpenSchema Method](../../../ado/reference/ado-api/openschema-method.md)   
 [Save Method](../../../ado/reference/ado-api/save-method.md)
