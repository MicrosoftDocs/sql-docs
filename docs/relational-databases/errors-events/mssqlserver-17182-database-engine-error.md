---
title: "MSSQLSERVER_17182"
description: "MSSQLSERVER_17182"
author: pijocoder
ms.author: jopilov
ms.reviewer: mathoma
ms.date: 02/02/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17182 (Database Engine error)"
---
# MSSQLSERVER_17182

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] |
| Event ID | 17182 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | INIT_TDSSNICLIENT |
| Message Text | TDSSNIClient initialization failed with error 0x%lx, status code 0x%lx. Reason: %S_MSG %.*ls |

## Explanation

When SQL Server starts, one of the steps it takes is to initialize a [Tabular Data Stream (TDS)](/openspecs/sql_server_protocols/ms-sstds/aaa7eab3-1d72-4c2e-9008-39260e45ed73) listener and network libraries to accept incoming connections. If this initialization fails, error 17182 is raised. Initialization activities include starting the SNI layer/TDS listener, configuring or initializing ports, protocols, [SSPI](/windows/win32/rpc/security-support-provider-interface-sspi-) authentication context, encryption (TLS/SSL) and so on.

Typically this error is raised together with other errors [MSSQLSERVER_17826](mssqlserver-17826-database-engine-error.md) and [MSSQLSERVER_17120](mssqlserver-17120-database-engine-error.md)


The 17182 error message contains three placeholders that are dynamically filled based on what issue that occurred. The "failed with error 0x%lx" hexadecimal value is the underlying OS error that occurred. This is the most important part of the error. The text after "Reason: " is the text message associated with this OS error. To illustrate, here's an example of this error:

```output
   Error: 17182, Severity: 16, State: 1.
   TDSSNIClient initialization failed with error 0x139f, status code 0x80. Reason: Unable to initialize SSL support. The group or resource is not in the correct state to perform the requested operation.
```

In this case OS error = 0x139f, which is 5023 in decimal. If you go to a Command Prompt and type `net helpmsg 5023` to look up this OS error, you'll get: "The group or resource isn't in the correct state to perform the requested operation." This text is what you see after "Reason:" in the example.
 
The third placeholder is status code. It's an internal value that indicates what component in the initialization failed. This may help Microsoft with troubleshooting the issue in more detail if needed. Here are some common status codes that have been observed:


|Status code  | Meaning                |
|---------    |---------               |
|0x01         | SNI Client             |
|0x04         | No listener (empty)    |
|0x0a         | TCP/IP provider        |
|0x40         | Shared Memory provider |
|0x50         | Named Pipe provider    |
|0x80         | SSL provider           |

## Cause

There could be multiple reasons the can lead to this error, but they all relate to initializing network libraries or encryption at SQL Server Network Interface (SNI) layer. Here are some examples:

  - Misconfigured network protocols
    - no protocol is selected 
    - invalid TCP ports are specified
- Misconfigured TLS/SSL for network encryption 
    - invalid certificate, 
    - invalid TLS version
    - invalid or missing registry key configuration
- Operating system issue with protocols or TLS/SSL

## User Action


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

   For more information, see [SQL Server service can't start after you configure an instance to use a Secure Sockets Layer certificate](/troubleshoot/sql/database-engine/startup-shutdown/service-cannot-start)

1. Use SQL Server Configuration Manager to validate network protocols have been correctly configured. For more information, see [Enable or Disable a Server Network Protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
1. Use SQL Server Configuration Manager 2019 or later to manage certificates and validate them. For more information, see [Certificate Management (SQL Server Configuration Manager)](../../database-engine/configure-windows/manage-certificates.md)