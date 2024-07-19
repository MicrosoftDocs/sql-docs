---
title: "MSSQLSERVER_7399"
description: "MSSQLSERVER_7399"
author: PiJoCoder
ms.author: jopilov
ms.reviewer: prmadhes, randolphwest
ms.date: 11/20/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "7399 (Database Engine error)"
---

# MSSQLSERVER_7399

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

| Attribute | Value |
| :--- | :--- |
| Product | SQL Server |
| Event ID | 7399 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | RMT_HRESULT_FAIL |
| Message Text | The OLE DB provider "%ls" for linked server "%ls" reported an error.%ls |

## Explanation

This error occurs when a linked server query fails because of an error that is generated on the remote server.

Error 7399 is a generic error message from the provider. In some cases, you can enable Trace Flag 7300 in order to get a more detailed error message from the provider. Whether you receive more information depends on the OLE DB (Object Linking and Embedding, Database) provider that you use. To enable the trace flag, run the following command before you run the query that causes the error:

```sql
DBCC TRACEON (7300, 3604);
```

## User action

Even though error 7399 is a generic error that includes a more specific message inside it, a commonly observed specific error is "Timeout Expired." There are two configurable timeout options that affect remote queries: **remote login timeout** option and **remote query timeout** option. Here are examples of how you might see the timeout error raised within 7399 and how to address it:

### IDBInitialize::Initialize

> Server: Msg 7399, Level 16, State 1, Line 1 OLE DB provider 'SQLOLEDB' reported an error. [OLE/DB provider returned message: Timeout expired] OLE DB error trace [OLE/DB Provider 'SQLOLEDB' IDBInitialize::Initialize returned 0x80004005: ].

This error message pertains to the `IDBInitialize::Initialize` method. It occurs if the time taken by the query to establish a connection to the remote server exceeds the **remote login timeout** option value.

To work around this error, set the **remote login timeout** value to 30 seconds by running the following code:

```sql
sp_configure 'remote login timeout', 30;
GO
RECONFIGURE WITH OVERRIDE;
GO
```

### ICommandText::Execute

> Server: Msg 7399, Level 16, State 1, Line 1 OLE DB provider 'SQLOLEDB' reported an error. Execution terminated by the provider because a resource limit was reached. [OLE/DB provider returned message: Timeout expired] OLE DB error trace [OLE/DB Provider 'SQLOLEDB' ICommandText::Execute returned 0x80040e31: Execution terminated by the provider because a resource limit was reached.].

This error message pertains to the `ICommandText::Execute` method. It indicates that the query took longer to process than the time specified in the **remote query timeout** configuration setting.

The default [**remote query timeout** value](../../database-engine/configure-windows/configure-the-remote-query-timeout-server-configuration-option.md) is `600` (10 minutes).

To work around this error, disable the timeout by setting the **remote query timeout** value to `0` (infinite wait) by running the following code:

```sql
sp_configure 'remote query timeout', 0;
GO
RECONFIGURE WITH OVERRIDE;
GO
```
