---
title: Security limitations for SQL Server on Linux
description: Learn about SQL Server on Linux restrictions, including how using keys stored in Azure Key Vault and extensible Key Management are not supported.
author: VanMSFT 
ms.author: vanto
ms.date: 09/12/2019
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.assetid: 64da74cc-14bf-4636-a55e-8cc1fce2aaff
---
# Security limitations for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

SQL Server on Linux currently has the following limitations:

* A standard password policy is provided. MUST_CHANGE is the only option you may configure. CHECK_POLICY option is not supported.
* Extensible Key Management is not supported.
* SQL Server Authentication mode cannot be disabled. 
* Using keys stored in the Azure Key Vault is not supported.
* SQL Server generates its own self-signed certificate for encrypting connections. SQL Server can be configured to use a user provided certificate for TLS. 

For more information about security features available in SQL Server, see the [Security Center for SQL Server Database Engine and Azure SQL Database](../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md).

## Next steps

For common security tasks, see [Get started with security features of SQL Server on Linux](sql-server-linux-security-get-started.md). For a script to change the TCP port number, the SQL Server directories, and configure traceflags or collation, see [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md).
