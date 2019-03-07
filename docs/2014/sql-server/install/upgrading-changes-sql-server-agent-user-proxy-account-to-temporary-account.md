---
title: "Upgrading will change the SQL Server Agent User Proxy Account to the temporary UpgradedProxyAccount | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "log shipping [SQL Server Agent]"
ms.assetid: cd2d08c3-4e56-4034-8b68-0c78df8b5471
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Upgrading will change the SQL Server Agent User Proxy Account to the temporary UpgradedProxyAccount
  Database maintenance plans that have log shipping enabled will not be enabled after upgrade.  
  
## Component  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent  
  
## Description  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides a new set of log shipping functions that are not directly compatible with database maintenance plans.  
  
## Corrective Action  
 [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)] users that have database maintenance plans that contain log shipping functions should configure log shipping by using the new functions. For more information, search on "log shipping" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [SQL Server Agent log shipping job category causes upgrade to fail](../../../2014/sql-server/install/sql-server-agent-log-shipping-job-category-causes-upgrade-to-fail.md)   
 [Log shipping will not run after upgrading](../../../2014/sql-server/install/log-shipping-will-not-run-after-upgrading.md)  
  
  
