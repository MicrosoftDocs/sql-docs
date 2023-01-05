---
title: "Handle DataAdapter events"
description: Describes DataAdapter events and how to use them.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-chmalh
ms.date: "11/30/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Handle DataAdapter events

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The Microsoft SqlClient data provider for SQL Server <xref:Microsoft.Data.SqlClient.SqlDataAdapter> exposes three events that you can use to respond to changes made to data at the data source. The following table shows the `DataAdapter` events.

|Event|Description|  
|-----------|-----------------|  
|`RowUpdating`|An UPDATE, INSERT, or DELETE operation on a row (by a call to one of the `Update` methods) is about to begin.|  
|`RowUpdated`|An UPDATE, INSERT, or DELETE operation on a row (by a call to one of the `Update` methods) is complete.|  
|`FillError`|An error has occurred during a `Fill` operation.|  

## RowUpdating and RowUpdated events

`RowUpdating` is raised before any update to a row from the <xref:System.Data.DataSet> has been processed at the data source. `RowUpdated` is raised after any update to a row from the `DataSet` has been processed at the data source. As a result, you can use `RowUpdating` to modify update behavior before it happens, to provide additional handling when an update will occur, to retain a reference to an updated row, to cancel the current update and schedule it for a batch process to be processed later, and so on. `RowUpdated` is useful for responding to errors and exceptions that occur during the update. You can add **error information** to the `DataSet`, as well as **retry logic**, and so on.

The <xref:System.Data.Common.RowUpdatingEventArgs> and <xref:System.Data.Common.RowUpdatedEventArgs> arguments passed to the `RowUpdating` and `RowUpdated` events include the following: a `Command` property that references the `Command` object being used to perform the update; a `Row` property that references the `DataRow` object containing the updated information; a `StatementType` property for what type of update is being performed; the `TableMapping`, if applicable; and the `Status` of the operation.

You can use the `Status` property to determine if an error has occurred during the operation and, if desired, to control the actions against the current and resulting rows. When the event occurs, the `Status` property equals either `Continue` or `ErrorsOccurred`. The following table shows the values to which you can set the `Status` property in order to control later actions during the update.

|Status|Description|  
|------------|-----------------|  
|`Continue`|Continue the update operation.|  
|`ErrorsOccurred`|Abort the update operation and throw an exception.|  
|`SkipCurrentRow`|Ignore the current row and continue the update operation.|  
|`SkipAllRemainingRows`|Abort the update operation but do not throw an exception.|  

Setting the `Status` property to `ErrorsOccurred` causes an exception to be thrown. You can control which exception is thrown by setting the `Errors` property to the desired exception. Using one of the other values for `Status` prevents an exception from being thrown.

You can also use the `ContinueUpdateOnError` property to handle errors for updated rows. If `DataAdapter.ContinueUpdateOnError` is `true`, when an update to a row results in an exception being thrown, the text of the exception is placed into the `RowError` information of the particular row, and processing continues without throwing an exception. This enables you to respond to errors when the `Update` is complete, in contrast to the `RowUpdated` event, which enables you to respond to errors when the error is encountered.

The following code sample shows how to both add and remove event handlers. The `RowUpdating` event handler writes a log of all deleted records with a time stamp. The `RowUpdated` event handler adds error information to the `RowError` property of the row in the `DataSet`, suppresses the exception, and continues processing (mirroring the behavior of `ContinueUpdateOnError` = `true`).

[!code-csharp[SqlDataAdapter_Events#1](~/../sqlclient/doc/samples/SqlDataAdapter_Events.cs#1)]

## FillError event

The `DataAdapter` issues the `FillError` event when an error occurs during a `Fill` operation. This type of error commonly occurs when the data in the row being added could not be converted to a .NET type without some loss of precision.

If an error occurs during a `Fill` operation, the current row is not added to the `DataTable`. The `FillError` event enables you to resolve the error and add the row, or to ignore the excluded row and continue the `Fill` operation.

The <xref:System.Data.FillErrorEventArgs> passed to the `FillError` event can contain several properties that enable you to respond to and resolve errors. The following table shows the properties of the `FillErrorEventArgs` object.

|Property|Description|  
|--------------|-----------------|  
|`Errors`|The `Exception` that occurred.|  
|`DataTable`|The `DataTable` object being filled when the error occurred.|  
|`Values`|An array of objects that contains the values of the row being added when the error occurred. The ordinal references of the `Values` array correspond to the ordinal references of the columns of the row being added. For example, `Values[0]` is the value that was being added as the first column of the row.|  
|`Continue`|Allows you to choose whether or not to throw an exception. Setting the `Continue` property to `false` will halt the current `Fill` operation, and an exception will be thrown. Setting `Continue` to `true` continues the `Fill` operation despite the error.|  

The following code example adds an event handler for the `FillError` event of the `DataAdapter`. In the `FillError` event code, the example determines if there is the potential for precision loss, providing the opportunity to respond to the exception.

[!code-csharp[SqlDataAdapter_Events#2](~/../sqlclient/doc/samples/SqlDataAdapter_Events.cs#2)]

## See also

- [DataAdapters and DataReaders](dataadapters-datareaders.md)
- [Events](/dotnet/standard/events/index)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
