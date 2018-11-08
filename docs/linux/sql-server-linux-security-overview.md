---
title: Security limitations for SQL Server on Linux | Microsoft Docs
description: This article describes SQL Server on Linux restrictions.
author: "rothja"
ms.author: "jroth"
manager: craigg
ms.date: 01/30/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 64da74cc-14bf-4636-a55e-8cc1fce2aaff
---
# Security limitations for SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

SQL Server on Linux currently has the following limitations:

* A standard password policy is provided. MUST_CHANGE is the only option you may configure.  
* Extensible Key Management is not supported. 
* Using keys stored in the Azure Key Vault is not supported.
* SQL Server generates its own self-signed certificate for encrypting connections. SQL Server can be configured to use a user provided certificate for TLS. 

For more information about security features available in SQL Server, see the [Security Center for SQL Server Database Engine and Azure SQL Database](../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md).

## Next steps

For common security tasks, see [Get started with security features of SQL Server on Linux](sql-server-linux-security-get-started.md). For a script to change the TCP port number, the SQL Server directories, and configure traceflags or collation, see [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md).
