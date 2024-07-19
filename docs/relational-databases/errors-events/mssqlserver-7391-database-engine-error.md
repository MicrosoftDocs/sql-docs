---
title: "MSSQLSERVER_7391"
description: "MSSQLSERVER_7391"
author: suresh-kandoth
ms.author: sureshka
ms.reviewer: randolphwest
ms.date: 08/04/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "7391 (Database Engine error)"
---
# MSSQLSERVER_7391

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

| Attribute | Value |
| :--- | :--- |
| Product | SQL Server |
| Event ID | 7391 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | RMT_TRANS_JOIN_FAIL |
| Message Text | The operation could not be performed because OLE DB provider "%ls" for linked server "%ls" was unable to begin a distributed transaction. |

## Explanation

This error occurs because the Microsoft Distributed Transaction Coordinator (MSDTC) service isn't running or has disabled network access.

In some instances, you might also receive error 8522:

```output
Microsoft Distributed Transaction Coordinator (MS DTC) has stopped this transaction.
```

Error numbers that are in the range of 7300 to 7399 indicate a problem that affects the provider. Because each provider might have different capabilities and show different details, you might not receive a complete error message. To retrieve the complete error message from providers, run the following command before you run the query that generates the error:

   ```sql
   DBCC TRACEON (3604, 7300)
   ```

If you receive error 7391 from a process such as SQL Server replication or SQL Server Integration Services (SSIS), you might also receive the error message if the code contains a `BEGIN DISTRIBUTED TRAN` statement.

## User action

> [!NOTE]  
> It's a best practice to limit your code in a transaction that involves a distributed query that is made to only the remote server.

### Supportability

- To check whether the driver supports distributed transactions, contact the vendor of the driver that you use in your linked server query.

- Check whether the object on the destination server points back to the first server. This is known as a loopback situation. [Loopback linked servers](../linked-servers/linked-servers-database-engine.md) are intended for testing and aren't supported for many operations, such as distributed transactions.

### Server communication

To make sure that communication between servers is successful, follow these steps:

1. Check whether your network name resolution works. Make sure that the servers can communicate with one another by name and not only by IP address. Check in both directions (for example, from server A to server B and from server B to server A). Resolve all name resolution problems on the network before you run your distributed query. This might involve updating the WINS, DNS, or LMHost files.

1. If you have a firewall, make sure that your Remote Procedure Call (RPC) ports are opened correctly. For more information, see the following articles:

   - [How to configure RPC dynamic port allocation to work with firewalls - Windows Server](/troubleshoot/windows-server/networking/configure-rpc-dynamic-port-allocation-with-firewalls)

   - [Configure firewall for AD domain and trusts - Windows Server](/troubleshoot/windows-server/identity/config-firewall-for-ad-domains-and-trusts)

   - [Configure the Windows Firewall to allow SQL Server access - SQL Server](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)

1. Check the object that you point to on the destination server. If the object is a view or a stored procedure, or if it causes a trigger to run, check whether it implicitly points to another server. If so, the third server is the source of the problem. Run the query directly on the third server. If you can't do this, the linked server query isn't the problem. Resolve the underlying problem first.

1. Check whether you're using Remote Access Server (RAS) to access remote servers. If so, make sure that you have implemented Routing RAS (RRAS). Linked servers don't work on RAS because RAS allows only one-way communication.

### Server configuration

Follow these steps for configuring the servers:

1. Start the Distributed Transaction Coordinator (DTC or MS DTC) on all servers that are involved in the distributed transaction. For information about enabling network DTC access, see [Error message of OLE DB provider - SQL Server](/troubleshoot/sql/database-engine/linked-servers/error-message-ole-db-provider).

1. Set the **XACT_ABORT** option to ON for data modification statements in an implicit or explicit transaction against most OLE DB providers, including SQL Server. You can do this by running the following command before you run your query.

   ```sql
   SET XACT_ABORT ON
   ```

   > [!NOTE]  
   > This option isn't required if the provider supports nested transactions.

1. Check whether any of the servers are on a Windows Server Failover Cluster. The MSDTC service on the cluster must have its own IP address. Make sure that correct name resolution of the DTC service occurs on each server. The IP address of the DTC must be defined in your name resolution system (such as WINS, DNS, or LMHosts). Verify that each server can communicate with MSDTC on the other servers by name and not only by IP address. Check in both directions. For example, check from server A to server B's MSDTC service, and then check from server B to server A's MSDTC. You must resolve all name resolution problems on the network before you run your distributed query. To configure MSDTC on a cluster, see [MSDTC Recommendations on SQL Failover Cluster - Microsoft Community Hub](/troubleshoot/sql/database-engine/linked-servers/error-message-ole-db-provider).

1. If you're using the older remote servers technology instead of the recommended linked servers, set the **remote proc trans** configuration option to `OFF` for the server, or run a `SET REMOTE_PROC_TRANSACTIONS OFF` statement before you run any distributed query. If this setting is set to `ON`, the remote procedure calls are made in a local transaction. For more information, see [Configure the remote proc trans (server configuration option) - SQL Server](../../database-engine/configure-windows/configure-the-remote-proc-trans-server-configuration-option.md).

1. Check the return value of the system function `@@SERVERNAME` on both servers. Verify whether the return value matches the computer name of each server. If it doesn't match, rename the server.

1. Verify that the SQL Server startup account has full control permissions on the following registry key:

   `HKEY_LOCAL_MACHINE\Software\Microsoft\MSSQLServer`

## Next steps

- [Set up and troubleshoot a linked server to an Oracle database](/troubleshoot/sql/database-engine/linked-servers/set-up-troubleshoot-linked-server)
- [Linked Servers (Database Engine)](../linked-servers/linked-servers-database-engine.md)
