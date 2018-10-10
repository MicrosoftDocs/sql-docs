---
 title: include file
 description: include file
 services: sql-database
 author: MightyPen
 ms.service: sql-database
 ms.topic: include
 ms.date: 04/05/2018
 ms.author: genemi
 ms.custom: include file
---
## Specifying Application Intent

The keyword **ApplicationIntent** can be specified in your connection string. The assignable values are **ReadWrite** or **ReadOnly**. The default is **ReadWrite**.

When **ApplicationIntent=ReadOnly**, the client requests a read workload when connecting. The server enforces the intent at connection time, and during a **USE** database statement.

The **ApplicationIntent** keyword does not work with legacy read-only databases.  


#### Targets of ReadOnly

When a connection chooses **ReadOnly**, the connection is assigned to any of the following special configurations that might exist for the database:

- [Always On](~/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
    - A database can allow or disallow read workloads on the targeted Always On database. This choice is controlled by using the **ALLOW_CONNECTIONS** clause of the **PRIMARY_ROLE** and **SECONDARY_ROLE** Transact-SQL statements.

- [Geo-Replication](https://docs.microsoft.com/azure/sql-database/sql-database-geo-replication-overview)

- [Read Scale-Out](https://docs.microsoft.com/azure/sql-database/sql-database-read-scale-out)

If none of those special targets are available, the regular database is read from.

&nbsp;

The **ApplicationIntent** keyword enables *read-only routing*.


## Read-Only Routing

Read-only routing is a feature that can ensure the availability of a read-only replica of a database. To enable read-only routing, all of the following apply:

- You must connect to an Always On Availability Group availability group listener.

- The **ApplicationIntent** connection string keyword must be set to **ReadOnly**.

- The Availability Group must be configured by the database administrator to enable read-only routing.

Multiple connections each using read-only routing might not all connect to the same read-only replica. Changes in database synchronization or changes in the server's routing configuration can result in client connections to different read-only replicas. You can ensure that all read-only requests connect to the same read-only replica. Ensure this sameness by *not* passing an availability group listener to the **Server** connection string keyword. Instead, specify the name of the read-only instance.

Read-only routing may take longer than connecting to the primary. The longer wait is because read-only routing first connects to the primary, and then looks for the best available readable secondary. Due to these multiple staps, you should increase your login timeout to at least 30 seconds.

