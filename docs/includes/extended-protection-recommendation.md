---
author: adbadram
ms.author: adbadram
ms.date: 05/07/2026
ms.service: sql
ms.subservice: configuration
ms.topic: include
---

**Applies to**: [!INCLUDE [ssnoversion-md](ssnoversion-md.md)]

To enhance security when you use Windows authentication, set **Extended Protection** to **Required** and **Force Encryption** to **Yes** in SQL Server Configuration Manager.

These settings provide the most secure configuration for [!INCLUDE [ssnoversion-md](ssnoversion-md.md)].

> [!NOTE]  
> In [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and later versions, use **Force Strict Encryption** instead of **Force Encryption** to enable stronger security via TDS 8.0.

Update your [connection strings](../relational-databases/security/networking/tds-8.md#additional-changes-to-connection-string-encryption-properties) to accommodate these changes.

For more information, see:

- [Connect to the database engine with Extended Protection](../database-engine/configure-windows/connect-to-the-database-engine-using-extended-protection.md)
- [Configure SQL Server Database Engine for encrypting connections](../database-engine/configure-windows/configure-sql-server-encryption.md)
- [TDS 8.0](../relational-databases/security/networking/tds-8.md)
- [Connect to SQL Server with strict encryption](../relational-databases/security/networking/connect-with-strict-encryption.md)
