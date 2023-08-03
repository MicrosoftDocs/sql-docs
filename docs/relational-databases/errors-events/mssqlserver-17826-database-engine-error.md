---
title: "MSSQLSERVER_17826"
description: "MSSQLSERVER_17826"
author: pijocoder
ms.author: jopilov
ms.reviewer: mathoma
ms.date: 02/02/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17826 (Database Engine error)"
---
# MSSQLSERVER_17826

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] |
| Event ID | 17826 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | SRV_BAD_SS_EP |
| Message Text | Could not start the network library because of an internal error in the network library. To determine the cause, review the errors immediately preceding this one in the error log. |

## Explanation

When SQL Server starts, one of the steps it takes is to initialize a [Tabular Data Stream (TDS)](/openspecs/sql_server_protocols/ms-sstds/aaa7eab3-1d72-4c2e-9008-39260e45ed73) listener and network libraries to accept incoming connections. If this network library initialization fails, error 17826 is raised. Typically this error is raised together with more specific errors. Those specific errors may provide more insight into the failure. 
Examples of such errors include [MSSQLSERVER_17182](mssqlserver-17182-database-engine-error.md) and [MSSQLSERVER_17120](mssqlserver-17120-database-engine-error.md)


## User Action

Review SQL Server error log located in the \Log folder for errors raised together with this error. Take action according to those more specific errors. The following articles describe commonly encountered problems with this error and the actions to take to resolve them:

- [SQL Server can't start if all the protocols are disabled](/troubleshoot/sql/database-engine/startup-shutdown/error-17182-protocols-disabled-start-failure).

- [SQL Server service cannot start after you configure an instance to use a Secure Sockets Layer certificate](/troubleshoot/sql/database-engine/startup-shutdown/service-cannot-start)

- Ensure you configure TLS correctly for SQL Server. For information on updates needed, see [TLS 1.2 support for Microsoft SQL Server](https://support.microsoft.com/en-us/topic/kb3135244-tls-1-2-support-for-microsoft-sql-server-e4472ef8-90a9-13c1-e4d8-44aad198cdbe)
