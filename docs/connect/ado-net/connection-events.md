---
title: Connection events
description: The connection events to retrieve informational messages from a data source and determine if its state is changed.
author: David-Engel
ms.author: v-davidengel
ms.date: 11/13/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---
# Connection events

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The Microsoft SqlClient Data Provider for SQL Server has **Connection** objects with two events that you can use to retrieve informational messages from a data source or to determine if the state of a **Connection** has changed. The following table describes the events of the **Connection** object.

|Event|Description|  
|-----------|-----------------|  
|**InfoMessage**|Occurs when an informational message is returned from a data source. Informational messages are messages from a data source that don't result in an exception being thrown.|  
|**StateChange**|Occurs when the state of the **Connection** changes.|  

## Work with the InfoMessage event

You can retrieve warnings and informational messages from a SQL Server data source using the <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event of the <xref:Microsoft.Data.SqlClient.SqlConnection> object. Errors returned from the data source with a severity level of 11 through 16 cause an exception to be thrown. However, the <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event can be used to obtain messages from the data source that aren't associated with an error. With Microsoft SQL Server, any error with a severity of 10 or less is considered to be an informational message, and can be captured by using the <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event. For more information, see the [Database Engine Error Severities](../../relational-databases/errors-events/database-engine-error-severities.md) article.

The <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event receives an <xref:Microsoft.Data.SqlClient.SqlInfoMessageEventArgs> object containing, in its **Errors** property, a collection of the messages from the data source. You can query the **Error** objects in this collection for the error number, message text, and the source of the error. The Microsoft SqlClient Data Provider for SQL Server also includes detail about the database, stored procedure, and line number that the message came from.

### Example

The following code example shows how to add an event handler for the <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event.

[!code-csharp[SqlConnection_._InfoMessage#1](~/../sqlclient/doc/samples/SqlConnection_InfoMessage_StateChange.cs#1)]

## Handle errors as InfoMessages

The <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event will normally fire only for informational and warning messages that are sent from the server. However, when an actual error occurs, the execution of the **ExecuteNonQuery** or **ExecuteReader** method that began the server operation is halted and an exception is thrown.

If you want to continue processing the rest of the statements in a command regardless of any errors produced by the server, set the <xref:Microsoft.Data.SqlClient.SqlConnection.FireInfoMessageEventOnUserErrors%2A> property of the <xref:Microsoft.Data.SqlClient.SqlConnection> to `true`. Setting this property causes the connection to fire the <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event for errors instead of throwing an exception and interrupting processing. The client application can then handle this event and respond to error conditions.

> [!NOTE]
> An error with a severity level of 17 or above that causes the server to stop processing the command must be handled as an exception. In this case, an exception is thrown regardless of how the error is handled in the <xref:Microsoft.Data.SqlClient.SqlConnection.InfoMessage> event.

## Work with the StateChange event

The **StateChange** event occurs when the state of a **Connection** changes. The **StateChange** event receives <xref:System.Data.StateChangeEventArgs> that enable you to determine the change in state of the **Connection** by using the **OriginalState** and **CurrentState** properties. The **OriginalState** property is a <xref:System.Data.ConnectionState> enumeration that indicates the state of the **Connection** before it changed. **CurrentState** is a <xref:System.Data.ConnectionState> enumeration that indicates the state of the **Connection** after it changed.

The following code example uses the **StateChange** event to write a message to the console when the state of the **Connection** changes.

[!code-csharp[SqlConnection_._StateChange#2](~/../sqlclient/doc/samples/SqlConnection_InfoMessage_StateChange.cs#2)]

## See also

- [Connecting to a data source](connecting-to-data-source.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
