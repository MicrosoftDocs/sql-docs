---
title: "SQL Server Login Password Strength | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: b0862c3a-926b-490c-a37f-382e50146a3e
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server Login Password Strength
  This rule checks whether "Enforce password policy" of each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login is enabled. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is enabled and if the operating system version is earlier than [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)], an attacker could repeatedly exploit a known [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login password.  
  
## Best Practices Recommendations  
 We recommend that you upgrade the operating system to [!INCLUDE[winxpsvr](../../includes/winxpsvr-md.md)].  
  
 If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is not required in your environment, use Windows Authentication.  
  
 Enable "Enforce password policy" for all the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins. Use [ALTER LOGIN](/sql/t-sql/statements/alter-login-transact-sql) to configure the password policy for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login.  
  
## For More Information  
 [Password Policy](../security/password-policy.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
