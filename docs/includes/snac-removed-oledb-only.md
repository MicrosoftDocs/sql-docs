---
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 11/03/2022
ms.service: sql
ms.topic: include
---
The [SQL Server Native Client](../relational-databases/native-client/sql-server-native-client.md) (often abbreviated SNAC) has been removed from [!INCLUDE[sssql22-md](sssql22-md.md)] and [!INCLUDE[ssManStudioFull](ssmanstudiofull-md.md)] 19 (SSMS). Both the SQL Server Native Client OLE DB provider (SQLNCLI or SQLNCLI11) and the legacy Microsoft OLE DB Provider for SQL Server (SQLOLEDB) are not recommended for new development. Switch to the new [Microsoft OLE DB Driver (MSOLEDBSQL) for SQL Server](../connect/oledb/oledb-driver-for-sql-server.md) going forward. 