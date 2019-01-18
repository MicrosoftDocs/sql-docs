---
title: "ADO Methods | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "ADO, methods"
  - "methods [ADO]"
ms.assetid: a38c5670-ba28-44f3-bd5b-fcb46880e904
author: MightyPen
ms.author: genemi
manager: craigg
---
# ADO Methods
|||  
|-|-|  
|[AddNew](../../../ado/reference/ado-api/addnew-method-ado.md)|Creates a new record for an updatable **Recordset** object.|  
|[Append](../../../ado/reference/ado-api/append-method-ado.md)|Appends an object to a collection. If the collection is **Fields**, a new **Field** object may be created before it is appended to the collection.|  
|[AppendChunk](../../../ado/reference/ado-api/appendchunk-method-ado.md)|Appends data to a large text or binary data **Field**, or to a **Parameter** object.|  
|[BeginTrans, CommitTrans, and RollbackTrans](../../../ado/reference/ado-api/begintrans-committrans-and-rollbacktrans-methods-ado.md)|Manages transaction processing within a **Connection** object as follows:<br /><br /> **BeginTrans** - Begins a new transaction.<br /><br /> **CommitTrans** - Saves any changes and ends the current transaction. It may also start a new transaction.<br /><br /> **RollbackTrans** - Cancels any changes and ends the current transaction. It may also start a new transaction.|  
|[Cancel](../../../ado/reference/ado-api/cancel-method-ado.md)|Cancels execution of a pending, asynchronous method call.|  
|[CancelBatch](../../../ado/reference/ado-api/cancelbatch-method-ado.md)|Cancels a pending batch update.|  
|[CancelUpdate](../../../ado/reference/ado-api/cancelupdate-method-ado.md)|Cancels any changes that were made to the current or new row of a **Recordset** object, or the **Fields** collection of a **Record** object, before calling the **Update** method.|  
|[Clear](../../../ado/reference/ado-api/clear-method-ado.md)|Removes all the **Error** objects from the **Errors** collection.|  
|[Clone](../../../ado/reference/ado-api/clone-method-ado.md)|Creates a duplicate **Recordset** object from an existing **Recordset** object. Optionally, specifies that the clone be read-only.|  
|[Close](../../../ado/reference/ado-api/close-method-ado.md)|Closes an open object and any dependent objects.|  
|[CompareBookmarks](../../../ado/reference/ado-api/comparebookmarks-method-ado.md)|Compares two bookmarks and returns an indication of their relative values.|  
|[CopyRecord](../../../ado/reference/ado-api/copyrecord-method-ado.md)|Copies a file or directory, and its contents, to another location.|  
|[CopyTo](../../../ado/reference/ado-api/copyto-method-ado.md)|Copies the specified number of characters or bytes (depending on **Type**) in the **Stream** to another **Stream** object.|  
|[CreateParameter](../../../ado/reference/ado-api/createparameter-method-ado.md)|Creates a new **Parameter** object that has the specified properties.|  
|[Delete (ADO Parameters Collection)](../../../ado/reference/ado-api/delete-method-ado-parameters-collection.md)|Deletes an object from the **Parameters** collection.|  
|[Delete (ADO Fields Collection)](../../../ado/reference/ado-api/delete-method-ado-fields-collection.md)|Deletes an object from the **Fields** collection.|  
|[Delete (ADO Recordset)](../../../ado/reference/ado-api/delete-method-ado-recordset.md)|Deletes the current record or a group of records.|  
|[DeleteRecord](../../../ado/reference/ado-api/deleterecord-method-ado.md)|Deletes a file or directory, and all its subdirectories.|  
|[Execute (ADO Command)](../../../ado/reference/ado-api/execute-method-ado-command.md)|Executes the query, SQL statement, or stored procedure specified in the **CommandText** property.|  
|[Execute (ADO Connection)](../../../ado/reference/ado-api/execute-method-ado-connection.md)|Executes the specified query, SQL statement, stored procedure, or provider-specific text.|  
|[Find](../../../ado/reference/ado-api/find-method-ado.md)|Searches a **Recordset** for the row that satisfies the specified criteria.|  
|[Flush](../../../ado/reference/ado-api/flush-method-ado.md)|Forces the contents of the **Stream** remaining in the ADO buffer to the underlying object with which the **Stream** is associated.|  
|[get_OLEDBCommand Method](../../../ado/reference/ado-api/get-oledbcommand-method.md)|Returns the underlying OLEDB Command, first propagating any parameter information set on the ADO Command to the OLEDB command.|  
|[GetChildren](../../../ado/reference/ado-api/getchildren-method-ado.md)|Returns a **Recordset** whose rows represent the files and subdirectories in the directory represented by this **Record**.|  
|[GetChunk](../../../ado/reference/ado-api/getchunk-method-ado.md)|Returns all, or a portion of, the contents of a large text or binary data **Field** object.|  
|[GetDataProviderDSO Method](../../../ado/reference/ado-api/getdataproviderdso-method.md)|Retrieves the underlying OLEDB Data Source object from the Shape provider.|  
|[GetRows](../../../ado/reference/ado-api/getrows-method-ado.md)|Retrieves multiple records of a **Recordset** object into an array.|  
|[GetString](../../../ado/reference/ado-api/getstring-method-ado.md)|Returns the **Recordset** as a string.|  
|[LoadFromFile](../../../ado/reference/ado-api/loadfromfile-method-ado.md)|Loads the contents of an existing file into a **Stream**.|  
|[Move](../../../ado/reference/ado-api/move-method-ado.md)|Moves the position of the current record in a **Recordset** object.|  
|[MoveFirst, MoveLast, MoveNext, and MovePrevious](../../../ado/reference/ado-api/movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Moves to the first, last, next, or previous record in a specified **Recordset** object and makes that record the current record.|  
|[MoveRecord](../../../ado/reference/ado-api/moverecord-method-ado.md)|Moves a file, or a directory and its contents, to another location.|  
|[NextRecordset](../../../ado/reference/ado-api/nextrecordset-method-ado.md)|Clears the current **Recordset** object and returns the next **Recordset** by advancing through a series of commands.|  
|[Open (ADO Connection)](../../../ado/reference/ado-api/open-method-ado-connection.md)|Opens a connection to a data source.|  
|[Open (ADO Record)](../../../ado/reference/ado-api/open-method-ado-record.md)|Opens an existing **Record** object, or creates a new file or directory.|  
|[Open (ADO Recordset)](../../../ado/reference/ado-api/open-method-ado-recordset.md)|Opens a cursor.|  
|[Open (ADO Stream)](../../../ado/reference/ado-api/open-method-ado-stream.md)|Opens a **Stream** object to manipulate streams of binary or text data.|  
|[OpenSchema](../../../ado/reference/ado-api/openschema-method.md)|Obtains database schema information from the provider.|  
|[put_OLEDBCommand Method](../../../ado/reference/ado-api/put-oledbcommand-method.md)|This method performs no operation - it always returns S_OK.|  
|[Read](../../../ado/reference/ado-api/read-method.md)|Reads a specified number of bytes from a **Stream** object.|  
|[ReadText](../../../ado/reference/ado-api/readtext-method.md)|Reads a specified number of characters from a text **Stream** object.|  
|[Refresh](../../../ado/reference/ado-api/refresh-method-ado.md)|Updates the objects in a collection to reflect objects available from, and specific to, the provider.|  
|[Requery](../../../ado/reference/ado-api/requery-method.md)|Updates the data in a **Recordset** object by re-executing the query on which the object is based.|  
|[Resync](../../../ado/reference/ado-api/resync-method.md)|Refreshes the data in the current **Recordset** object, or **Fields** collection of a **Record** object, from the underlying database.|  
|[Save](../../../ado/reference/ado-api/save-method.md)|Saves the **Recordset** in a file or **Stream** object.|  
|[SaveToFile](../../../ado/reference/ado-api/savetofile-method.md)|Saves the binary contents of a **Stream** to a file.|  
|[Seek](../../../ado/reference/ado-api/seek-method.md)|Searches the index of a **Recordset** to quickly locate the row that matches the specified values, and changes the current row position to that row.|  
|[SetEOS](../../../ado/reference/ado-api/seteos-method.md)|Sets the position that is the end of the stream.|  
|[SkipLine](../../../ado/reference/ado-api/skipline-method.md)|Skips one entire line when reading a text stream.|  
|[Stat](../../../ado/reference/ado-api/stat-method.md)|Gets statistical information about an open stream.|  
|[Supports](../../../ado/reference/ado-api/supports-method.md)|Determines whether a specified **Recordset** object supports a particular type of functionality.|  
|[Update](../../../ado/reference/ado-api/update-method.md)|Saves any changes you make to the current row of a **Recordset** object, or the **Fields** collection of a **Record** object.|  
|[UpdateBatch](../../../ado/reference/ado-api/updatebatch-method.md)|Writes all pending batch updates to disk.|  
|[Write](../../../ado/reference/ado-api/write-method.md)|Writes binary data to a **Stream** object.|  
|[WriteText](../../../ado/reference/ado-api/writetext-method.md)|Writes a specified text string to a **Stream** object.|  
  
## See Also  
 [ADO API Reference](../../../ado/reference/ado-api/ado-api-reference.md)   
 [ADO Collections](../../../ado/reference/ado-api/ado-collections.md)   
 [ADO Dynamic Properties](../../../ado/reference/ado-api/ado-dynamic-properties.md)   
 [ADO Enumerated Constants](../../../ado/reference/ado-api/ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../../ado/guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](../../../ado/reference/ado-api/ado-events.md)   
 [ADO Object Model](../../../ado/reference/ado-api/ado-object-model.md)   
 [ADO Objects and Interfaces](../../../ado/reference/ado-api/ado-objects-and-interfaces.md)   
 [ADO Properties](../../../ado/reference/ado-api/ado-properties.md)
