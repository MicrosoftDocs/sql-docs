---
title: "Always Encrypted (client development) | Microsoft Docs"
ms.custom: ""
ms.date: "07/29/2016"
ms.prod: sql
ms.reviewer: ""
ms.suite: "sql"
ms.technology: security
ms.tgt_pltfrm: ""
ms.topic: conceptual
dev_langs: 
  - "CSharp"
ms.assetid: 9595eb66-284c-4474-828f-8961a05ce989
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "= azuresqldb-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Always Encrypted (client development)
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

[Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md) is a client-side encryption technology that ensures sensitive data (and related encryption keys) are never revealed to the SQL Server or Azure SQL Database. With Always Encrypted, a client driver transparently encrypts sensitive data before passing the data to the Database Engine, and it transparently decrypts data retrieved from encrypted database columns.

For details about developing applications that use Always Encrypted protected databases, and which client drivers and which driver versions support Always Encrypted, see:

- [Using Always Encrypted with the .NET Framework Data Provider for SQL Server](../../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md)
- [Using Always Encrypted with the JDBC Driver](../../../connect/jdbc/using-always-encrypted-with-the-jdbc-driver.md)
- [Using Always Encrypted with the Windows ODBC Driver](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md)



## See Also

[Always Encrypted (Database Engine)](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)

