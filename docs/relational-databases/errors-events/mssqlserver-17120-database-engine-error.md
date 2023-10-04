---
title: "MSSQLSERVER_17120"
description: "MSSQLSERVER_17120"
author: pijocoder
ms.author: jopilov
ms.reviewer: mathoma
ms.date: 01/18/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17120 (Database Engine error)"
---
# MSSQLSERVER_17120

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 17120 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | INIT_SPAWN |
| Message Text | SQL Server could not spawn %s thread. Check the SQL Server error log and the operating system error log for information about possible related problems. |

## Explanation

Error 17120 is raised when a thread that performs system or background tasks fails to be created. Examples of background tasks include Checkpoint, Lazy Writer, Database Recovery task, Log Reader, Log Writer, Auto shrink task, Communication manager, Parallel Redo worker task, and many more. The `%s` in the error contains the name of the task. Here are examples of how this error may appear in the error logs:

```output
Error: 17120, Severity: 16, State: 1.
SQL Server could not spawn checkpoint thread. Check the SQL Server error log and the operating system error log for information about possible related problems.
```

```output
Error: 17120, Severity: 16, State: 1.
SQL Server could not spawn FRunCommunicationsManager thread. Check the SQL Server error log and the operating system error log for information about possible related problems.
```

```output
Error: 17120, Severity: 16, State: 1.
SQL Server could not spawn log writer thread. Check the SQL Server error log and the operating system error log for information about possible related problems.
```

## Cause

SQL Server is unable to create a thread for a background task to start running. Common causes include:

- Low or no memory available on the system or inside SQL Server
- SQL Server is in the middle of a shutdown
- SQL Server is service is unable to start due to a misconfiguration or resource issue

## User Action

Diagnose other errors and on the system and retry the operation.

1. **Out of memory issues:** The first step to start the investigation is to check for low memory or out-of-memory conditions. Examine the System event log and SQL error logs. For information on how to troubleshoot, see [Troubleshoot out of memory or low memory issues in SQL Server](/troubleshoot/sql/database-engine/performance/troubleshoot-memory-issues)

1. **Resolve misconfigured protocols** A common issue that has been reported includes misconfigured SQL Server protocols. For more information, see [SQL Server can't start if all the protocols are disabled](/troubleshoot/sql/database-engine/startup-shutdown/error-17182-protocols-disabled-start-failure). You may observe the following sequence of errors in the error log:

   ```output
   Error: 17182, Severity: 16, State: 1.
   TDSSNIClient initialization failed with error 0xd, status code 0x4. Reason: **All protocols are disabled. The data is invalid**.
   Error: 17182, Severity: 16, State: 1.
   TDSSNIClient initialization failed with error 0xd, status code 0x1. Reason: Initialization failed with an infrastructure error. Check for previous errors. The data is    invalid.
   Error: 17826, Severity: 18, State: 3.
   Could not start the network library because of an internal error in the network library. To determine the cause, review the errors immediately preceding this one in the    error log.
   Error: 17120, Severity: 16, State: 1.
   SQL Server could not spawn FRunCommunicationsManager thread. Check the SQL Server error log and the operating system error log for information about possible related    problems.
   ```

1. **Resolve TLS configuration and update issues** Another common issue that has been reported includes TLS configuration on the server preventing SQL Server from creating a background communication task.

   ```output
   Error: 26011, Severity: 16, State: 1.
   The server was unable to initialize encryption because of a problem with a security library. The security library may be missing. Verify that security.dll exists on  the    system.
   Error: 17182, Severity: 16, State: 1.
   TDSSNIClient initialization failed with error 0x139f, status code 0x80. Reason: Unable to initialize SSL support. The group or resource is not in the correct state  to    perform the requested operation.
   Error: 17182, Severity: 16, State: 1.
   TDSSNIClient initialization failed with error 0x139f, status code 0x1. Reason: Initialization failed with an infrastructure error. Check for previous errors. The group  or    resource is not in the correct state to perform the requested operation.
   Error: 17826, Severity: 18, State: 3.
   Could not start the network library because of an internal error in the network library. To determine the cause, review the errors immediately preceding this one in  the    error log.
   Error: 17120, Severity: 16, State: 1.
   SQL Server could not spawn FRunCommunicationsManager thread. Check the SQL Server error log and the Windows event logs for information about possible related problems.
   ```

   Ensure you configure TLS correctly for SQL Server. For information on updates needed, see [TLS 1.2 support for Microsoft SQL Server](https://support.microsoft.com/en-us/topic/kb3135244-tls-1-2-support-for-microsoft-sql-server-e4472ef8-90a9-13c1-e4d8-44aad198cdbe)

1. **Resolve encryption certificates issues** Another common issue is the misconfiguration of TLS/SSL certificates leading to SQL Server not being able to start and start a thread.

   ```output
   Error: 26014, Severity: 16, State: 1.
   Unable to load user-specified certificate [Cert Hash(sha1) "%hs"]. The server will not accept a connection. You should verify that the certificate is correctly installed.    See "Configuring Certificate for Use by SSL" in Books Online.
   
   Error: 17182, Severity: 16, State: 1.
   TDSSNIClient initialization failed with error 0x80092004, status code 0x80. Reason: Unable to initialize SSL support. Cannot find object or property.
   
   Error: 17826, Severity: 18, State: 3.
   Could not start the network library because of an internal error in the network library. To determine the cause, review the errors immediately preceding this one in the    error log.
   
   Error: 17120, Severity: 16, State: 1.
   SQL Server could not spawn FRunCommunicationsManager thread. Check the SQL Server error log and the Windows event logs for information about possible related problems.
   ```

   For more information, see [SQL Server service cannot start after you configure an instance to use a Secure Sockets Layer certificate](/troubleshoot/sql/database-engine/startup-shutdown/service-cannot-start)
