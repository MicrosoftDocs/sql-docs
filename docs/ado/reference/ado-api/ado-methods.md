---
title: "ADO Methods"
description: "ADO Methods"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: reference
helpviewer_keywords:
  - "ADO, methods"
  - "methods [ADO]"
---
# ADO Methods

|Method|Description|  
|-|-|  
|[AddNew](./addnew-method-ado.md)|Creates a new record for an updatable **Recordset** object.|  
|[Append](./append-method-ado.md)|Appends an object to a collection. If the collection is **Fields**, a new **Field** object may be created before it is appended to the collection.|  
|[AppendChunk](./appendchunk-method-ado.md)|Appends data to a large text or binary data **Field**, or to a **Parameter** object.|  
|[BeginTrans, CommitTrans, and RollbackTrans](./begintrans-committrans-and-rollbacktrans-methods-ado.md)|Manages transaction processing within a **Connection** object as follows:<br /><br /> **BeginTrans** - Begins a new transaction.<br /><br /> **CommitTrans** - Saves any changes and ends the current transaction. It may also start a new transaction.<br /><br /> **RollbackTrans** - Cancels any changes and ends the current transaction. It may also start a new transaction.|  
|[Cancel](./cancel-method-ado.md)|Cancels execution of a pending, asynchronous method call.|  
|[CancelBatch](./cancelbatch-method-ado.md)|Cancels a pending batch update.|  
|[CancelUpdate](./cancelupdate-method-ado.md)|Cancels any changes that were made to the current or new row of a **Recordset** object, or the **Fields** collection of a **Record** object, before calling the **Update** method.|  
|[Clear](./clear-method-ado.md)|Removes all the **Error** objects from the **Errors** collection.|  
|[Clone](./clone-method-ado.md)|Creates a duplicate **Recordset** object from an existing **Recordset** object. Optionally, specifies that the clone be read-only.|  
|[Close](./close-method-ado.md)|Closes an open object and any dependent objects.|  
|[CompareBookmarks](./comparebookmarks-method-ado.md)|Compares two bookmarks and returns an indication of their relative values.|  
|[CopyRecord](./copyrecord-method-ado.md)|Copies a file or directory, and its contents, to another location.|  
|[CopyTo](./copyto-method-ado.md)|Copies the specified number of characters or bytes (depending on **Type**) in the **Stream** to another **Stream** object.|  
|[CreateParameter](./createparameter-method-ado.md)|Creates a new **Parameter** object that has the specified properties.|  
|[Delete (ADO Parameters Collection)](./delete-method-ado-parameters-collection.md)|Deletes an object from the **Parameters** collection.|  
|[Delete (ADO Fields Collection)](./delete-method-ado-fields-collection.md)|Deletes an object from the **Fields** collection.|  
|[Delete (ADO Recordset)](./delete-method-ado-recordset.md)|Deletes the current record or a group of records.|  
|[DeleteRecord](./deleterecord-method-ado.md)|Deletes a file or directory, and all its subdirectories.|  
|[Execute (ADO Command)](./execute-method-ado-command.md)|Executes the query, SQL statement, or stored procedure specified in the **CommandText** property.|  
|[Execute (ADO Connection)](./execute-method-ado-connection.md)|Executes the specified query, SQL statement, stored procedure, or provider-specific text.|  
|[Find](./find-method-ado.md)|Searches a **Recordset** for the row that satisfies the specified criteria.|  
|[Flush](./flush-method-ado.md)|Forces the contents of the **Stream** remaining in the ADO buffer to the underlying object with which the **Stream** is associated.|  
|[get_OLEDBCommand Method](./get-oledbcommand-method.md)|Returns the underlying OLEDB Command, first propagating any parameter information set on the ADO Command to the OLEDB command.|  
|[GetChildren](./getchildren-method-ado.md)|Returns a **Recordset** whose rows represent the files and subdirectories in the directory represented by this **Record**.|  
|[GetChunk](./getchunk-method-ado.md)|Returns all, or a portion of, the contents of a large text or binary data **Field** object.|  
|[GetDataProviderDSO Method](./getdataproviderdso-method.md)|Retrieves the underlying OLEDB Data Source object from the Shape provider.|  
|[GetRows](./getrows-method-ado.md)|Retrieves multiple records of a **Recordset** object into an array.|  
|[GetString](./getstring-method-ado.md)|Returns the **Recordset** as a string.|  
|[LoadFromFile](./loadfromfile-method-ado.md)|Loads the contents of an existing file into a **Stream**.|  
|[Move](./move-method-ado.md)|Moves the position of the current record in a **Recordset** object.|  
|[MoveFirst, MoveLast, MoveNext, and MovePrevious](./movefirst-movelast-movenext-and-moveprevious-methods-ado.md)|Moves to the first, last, next, or previous record in a specified **Recordset** object and makes that record the current record.|  
|[MoveRecord](./moverecord-method-ado.md)|Moves a file, or a directory and its contents, to another location.|  
|[NextRecordset](./nextrecordset-method-ado.md)|Clears the current **Recordset** object and returns the next **Recordset** by advancing through a series of commands.|  
|[Open (ADO Connection)](./open-method-ado-connection.md)|Opens a connection to a data source.|  
|[Open (ADO Record)](./open-method-ado-record.md)|Opens an existing **Record** object, or creates a new file or directory.|  
|[Open (ADO Recordset)](./open-method-ado-recordset.md)|Opens a cursor.|  
|[Open (ADO Stream)](./open-method-ado-stream.md)|Opens a **Stream** object to manipulate streams of binary or text data.|  
|[OpenSchema](./openschema-method.md)|Obtains database schema information from the provider.|  
|[put_OLEDBCommand Method](./put-oledbcommand-method.md)|This method performs no operation - it always returns S_OK.|  
|[Read](./read-method.md)|Reads a specified number of bytes from a **Stream** object.|  
|[ReadText](./readtext-method.md)|Reads a specified number of characters from a text **Stream** object.|  
|[Refresh](./refresh-method-ado.md)|Updates the objects in a collection to reflect objects available from, and specific to, the provider.|  
|[Requery](./requery-method.md)|Updates the data in a **Recordset** object by re-executing the query on which the object is based.|  
|[Resync](./resync-method.md)|Refreshes the data in the current **Recordset** object, or **Fields** collection of a **Record** object, from the underlying database.|  
|[Save](./save-method.md)|Saves the **Recordset** in a file or **Stream** object.|  
|[SaveToFile](./savetofile-method.md)|Saves the binary contents of a **Stream** to a file.|  
|[Seek](./seek-method.md)|Searches the index of a **Recordset** to quickly locate the row that matches the specified values, and changes the current row position to that row.|  
|[SetEOS](./seteos-method.md)|Sets the position that is the end of the stream.|  
|[SkipLine](./skipline-method.md)|Skips one entire line when reading a text stream.|  
|[Stat](./stat-method.md)|Gets statistical information about an open stream.|  
|[Supports](./supports-method.md)|Determines whether a specified **Recordset** object supports a particular type of functionality.|  
|[Update](./update-method.md)|Saves any changes you make to the current row of a **Recordset** object, or the **Fields** collection of a **Record** object.|  
|[UpdateBatch](./updatebatch-method.md)|Writes all pending batch updates to disk.|  
|[Write](./write-method.md)|Writes binary data to a **Stream** object.|  
|[WriteText](./writetext-method.md)|Writes a specified text string to a **Stream** object.|  
  
## See Also  
 [ADO API Reference](./ado-api-reference.md)   
 [ADO Collections](./ado-collections.md)   
 [ADO Dynamic Properties](./ado-dynamic-properties.md)   
 [ADO Enumerated Constants](./ado-enumerated-constants.md)   
 [Appendix B: ADO Errors](../../guide/appendixes/appendix-b-ado-errors.md)   
 [ADO Events](./ado-events.md)   
 [ADO Object Model](./ado-object-model.md)   
 [ADO Objects and Interfaces](./ado-objects-and-interfaces.md)   
 [ADO Properties](./ado-properties.md)