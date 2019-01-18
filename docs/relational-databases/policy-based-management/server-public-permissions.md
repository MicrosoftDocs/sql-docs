---
title: "Server public Permissions | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "Best Practices [Database Engine]"
ms.assetid: 9a276caa-ea38-473d-92bc-26302bfcf660
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Server public Permissions
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This rule determines whether the public server role has server permissions. Every login that is created on the server is a member of the public server role. If this condition is met, every login on the server will have server permissions.  
  
## Best Practices Recommendations  
 Do not grant server permissions to the server public role.  
  
> [!IMPORTANT]  
>  After setup completes the **PUBLIC** role has **CONNECT** permission on all the endpoints except the **Dedicated Admin Connection**. This is normal and should not be normally changed. (Access is controlled by using the **CONNECT SQL** permission which is automatically granted when new logins are created.)  
  
### For more information  
 [Securing SQL Server](../../relational-databases/security/securing-sql-server.md)  
  
  
