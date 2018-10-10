---
title: "Strong Passwords | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
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
manager: craigg
---
# Strong Passwords
  Passwords can be the weakest link in a server security deployment. You should always take great care when you select a password. A strong password has the following characteristics:  
  
-   Is at least 8 characters long.  
  
-   Combines letters, numbers, and symbol characters within the password.  
  
-   Is not found in a dictionary.  
  
-   Is not the name of a command.  
  
-   Is not the name of a person.  
  
-   Is not the name of a user.  
  
-   Is not the name of a computer.  
  
-   Is changed regularly.  
  
-   Is significantly different from previous passwords.  
  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] passwords can contain up to 128 characters, including letters, symbols, and digits. Because logins, user names, roles, and passwords are frequently used in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, certain symbols must be enclosed by double quotation marks (") or square brackets ([ ]). Use these delimiters in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login, user, role, or password has the following characteristics:  
  
-   Contains or starts with a space character.  
  
-   Starts with the $ or \@ character.  
  
 If used in an OLE DB or ODBC connection string, a login or password must not contain the following characters: [] {}() , ; ? * ! \@. These characters are used to either initialize a connection or separate connection values.  
  
## Related Content  
 [Password Policy](password-policy.md)  
  
  
