---
title: "SQL Server Login Password Expiration | Microsoft Docs"
description: Check whether password expiration of each SQL Server login is enabled to help counter a possible attack in SQL Server. 
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 7e3bf9da-a436-433d-847a-47c30428cad3
author: VanMSFT
ms.author: vanto
---
# SQL Server Login Password Expiration
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This rule checks whether "Password expiration" of each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is enabled. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is enabled and if the operating system version is earlier than [!INCLUDE[winserver2003](../../includes/winserver2003-md.md)], an attacker could repeatedly exploit a known [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login password.  
  
## Best Practices Recommendations  
 We recommend that you upgrade the operating system to [!INCLUDE[winserver2003](../../includes/winserver2003-md.md)].  
  
 If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is not required in your environment, use Windows Authentication. For more information, see [Choose an Authentication Mode](../../relational-databases/security/choose-an-authentication-mode.md).  
  
 Enable "Password expiration" for all the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Use [ALTER LOGIN](../../t-sql/statements/alter-login-transact-sql.md) to configure the password policy for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
## For More Information  
 [Password Policy](../../relational-databases/security/password-policy.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
