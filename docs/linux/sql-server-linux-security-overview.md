---
title: Security limitations for SQL Server on Linux
description: Learn about SQL Server on Linux restrictions, including how using keys stored in Azure Key Vault and extensible Key Management aren't supported.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/11/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
---
# Security limitations for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux currently has the following limitations:

- A standard password policy is provided. `MUST_CHANGE` is the only option you may configure. The `CHECK_POLICY` option isn't supported.
- Extensible Key Management isn't supported.
- [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] authentication mode can't be disabled.
- Password expiration is hard-coded to 90 days if you use [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] authentication.
- Using keys stored in the Azure Key Vault isn't supported.
- [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] generates its own self-signed certificate for encrypting connections. [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] can be configured to use a user provided certificate for TLS.

> [!NOTE]  
> If you don't plan to connect your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] containers to Windows Active Directory, the password expiration is hard-coded to 90 days, if you use [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] authentication only. To work around this issue, consider changing the [CHECK_EXPIRATION policy](../t-sql/statements/alter-login-transact-sql.md).

For more information about security features available in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], see the [Security Center for SQL Server Database Engine and Azure SQL Database](../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md).

## Next steps

- [Get started with security features of SQL Server on Linux](sql-server-linux-security-get-started.md)
- [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md)
- [Editions and supported features of [!INCLUDE[sssql22](../includes/sssql22-md.md)] on Linux](sql-server-linux-editions-and-components-2022.md)
