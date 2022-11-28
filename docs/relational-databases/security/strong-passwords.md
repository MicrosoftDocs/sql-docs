---
title: "Strong Passwords | Microsoft Docs"
description: Learn about passwords in SQL Server and find out what constitutes a strong password to enhance security for your deployment.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "logins [SQL Server], passwords"
  - "passwords [SQL Server], strong"
  - "symbols [SQL Server]"
  - "security [SQL Server], passwords"
  - "passwords [SQL Server], symbols"
  - "characters [SQL Server], password policies"
  - "strong passwords [SQL Server]"
ms.assetid: 338548f4-c4d8-47ca-b597-5c9c0f2fa205
author: VanMSFT
ms.author: vanto
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Strong Passwords
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  Passwords can be the weakest link in a server security deployment. Take great care when you select a password. A strong password has the following characteristics:  
  
-   Is at least eight characters long.  
  
-   Combines letters, numbers, and symbol characters within the password.  
  
-   Is not found in a dictionary.  
  
-   Is not the name of a command.  
  
-   Is not the name of a person.  
  
-   Is not the name of a user.  
  
-   Is not the name of a computer.  
  
-   Is changed regularly.  
  
-   Is different from previous passwords.  
  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] passwords can contain up to 128 characters, including letters, symbols, and digits. Because logins, user names, roles, and passwords are frequently used in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, certain symbols must be enclosed by double quotation marks (") or square brackets ([ ]). Use these delimiters in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, user, role, or password has the following characteristics:  
  
-   Contains or starts with a space character.  
  
-   Starts with the $ or \@ character.  
  
 If used in an OLE DB or ODBC connection string, a login or password containing special characters must be enclosed in braces and right braces must be escaped. For example, the password `my}Pass;word` must be specified in the connection string like `PWD={my}}Pass;word}`.
  
## Related Content  
 [Password Policy](../../relational-databases/security/password-policy.md)  
  
  
