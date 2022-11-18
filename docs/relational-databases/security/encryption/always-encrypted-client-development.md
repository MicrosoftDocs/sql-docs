---
title: "Develop applications using Always Encrypted | Microsoft Docs"
description: Learn about Always Encrypted client-side technology that ensures sensitive data are never revealed to the SQL Server or Azure SQL Database.
ms.custom: ""
ms.date: "10/30/2019"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
dev_langs: 
  - "CSharp"
ms.assetid: 9595eb66-284c-4474-828f-8961a05ce989
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Develop applications using Always Encrypted
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

[Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md) is a client-side encryption technology that ensures sensitive data (and related encryption keys) are never revealed to the SQL Server or Azure SQL Database. With Always Encrypted, a client driver transparently encrypts sensitive data before passing the data to the Database Engine, and it transparently decrypts data retrieved from encrypted database columns.

For details about developing applications that use Always Encrypted protected databases, and which client drivers and which driver versions support Always Encrypted, see:

- [Using Always Encrypted with the .NET Framework Data Provider for SQL Server](../../../relational-databases/security/encryption/develop-using-always-encrypted-with-net-framework-data-provider.md)
- [Using Always Encrypted with the JDBC Driver](../../../connect/jdbc/using-always-encrypted-with-the-jdbc-driver.md)
- [Using Always Encrypted with the ODBC Driver](../../../connect/odbc/using-always-encrypted-with-the-odbc-driver.md)
- [Using Always Encrypted with the PHP Drivers](../../../connect/php/using-always-encrypted-php-drivers.md)
- [Using Always Encrypted the Microsoft .NET Data Provider for SQL Server in .NET Core and .NET Framework Applications](../../../connect/ado-net/sql/sqlclient-support-always-encrypted.md)
- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
