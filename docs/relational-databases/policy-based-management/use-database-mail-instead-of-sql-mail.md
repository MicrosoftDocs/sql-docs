---
title: "Use Database Mail Instead of SQL Mail | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: b08df7be-d8be-4184-a661-38ec0ac85cd1
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Use Database Mail Instead of SQL Mail
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This rule checks the sys.configurations catalog view to determine whether the SQL Mail XPs server-wide configuration option is set to ON.  
  
## Best Practices Recommendations  
 SQL Mail will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. To send mail, use Database Mail.  
  
 SQL Mail runs in-process to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. If SQL Mail goes down, so does the server. Database Mail runs outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a separate process, is scalable, and does not require Extended MAPI client components to be installed on the production server.  
  
## For More Information  
 [Database Mail](../../relational-databases/database-mail/database-mail.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)  
  
  
