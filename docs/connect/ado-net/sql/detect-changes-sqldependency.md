---
title: Detecting changes with SqlDependency
description: Demonstrates how to detect when query results will be different from the results originally received.
author: David-Engel
ms.author: v-davidengel
ms.reviewer: v-kaywon
ms.date: 04/20/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
dev_langs:
  - "csharp"
---

# Detecting changes with SqlDependency

[!INCLUDE[Driver_ADONET_Download](../../../includes/driver_adonet_download.md)]

A <xref:Microsoft.Data.SqlClient.SqlDependency> object can be associated with a <xref:Microsoft.Data.SqlClient.SqlCommand> to detect when query results differ from the results originally retrieved. You can also assign a delegate to the `OnChange` event, which will fire when the results change for an associated command. Associate the <xref:Microsoft.Data.SqlClient.SqlDependency> with the command before you execute the command. The `HasChanges` property of the <xref:Microsoft.Data.SqlClient.SqlDependency> can also be used to determine if the query results have changed since the data was first retrieved.

## Security considerations

The dependency infrastructure relies on a <xref:Microsoft.Data.SqlClient.SqlConnection> that is opened when <xref:Microsoft.Data.SqlClient.SqlDependency.Start%2A> is called to receive notifications that the underlying data has changed for a given command. The ability for a client to begin the call to `SqlDependency.Start` is controlled by using <xref:Microsoft.Data.SqlClient.SqlClientPermission> and code access security attributes. For more information, see [Enabling query notifications](enable-query-notifications.md).

### Example

The following steps illustrate how to declare a dependency, execute a command, and receive a notification when the result set changes:

1. Initiate a `SqlDependency` connection to the server.

2. Create <xref:Microsoft.Data.SqlClient.SqlConnection> and <xref:Microsoft.Data.SqlClient.SqlCommand> objects to connect to the server and define a Transact-SQL statement.

3. Create a new `SqlDependency` object, or use an existing one, and bind it to the `SqlCommand` object. Internally, this association creates a <xref:Microsoft.Data.Sql.SqlNotificationRequest> object and binds it to the command object as needed. This notification request contains an internal identifier that uniquely identifies this `SqlDependency` object. It also starts the client listener if it isn't already active.

4. Subscribe an event handler to the `OnChange` event of the `SqlDependency` object.

5. Execute the command using any of the `Execute` methods of the `SqlCommand` object. Because the command is bound to the notification object, the server recognizes that it must generate a notification, and the queue information will point to the dependencies queue.

6. Stop the `SqlDependency` connection to the server.

If any user then changes the underlying data, Microsoft SQL Server detects that there's a notification pending for such a change, and posts a notification that is processed and forwarded to the client through the underlying `SqlConnection` that was created by calling `SqlDependency.Start`. The client listener receives the invalidation message. The client listener then locates the associated `SqlDependency` object and fires the `OnChange` event.

The following code fragment shows the design pattern you would use to create a sample application.

```csharp
void Initialization()
{
    // Create a dependency connection.
    SqlDependency.Start(connectionString, queueName);
}

void SomeMethod()
{
    // Assume connection is an open SqlConnection.

    // Create a new SqlCommand object.
    using (SqlCommand command=new SqlCommand(
        "SELECT ShipperID, CompanyName, Phone FROM dbo.Shippers",
        connection))
    {

        // Create a dependency and associate it with the SqlCommand.
        SqlDependency dependency=new SqlDependency(command);
        // Maintain the reference in a class member.

        // Subscribe to the SqlDependency event.
        dependency.OnChange+=new
           OnChangeEventHandler(OnDependencyChange);

        // Execute the command.
        using (SqlDataReader reader = command.ExecuteReader())
        {
            // Process the DataReader.
        }
    }
}

// Handler method
void OnDependencyChange(object sender,
   SqlNotificationEventArgs e )
{
  // Handle the event (for example, invalidate this cache entry).
}

void Termination()
{
    // Release the dependency.
    SqlDependency.Stop(connectionString, queueName);
}
```

## Next steps

- [Query notifications in SQL Server](query-notifications-sql-server.md)
