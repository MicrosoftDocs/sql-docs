---
title: "Choose Servers to Configure (Configure Database Mirroring Security Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.configdbmsecurwiz.choosesrvrs.f1"
ms.assetid: 59e23ff3-d7ee-4e32-9629-0b54d3a258f7
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Choose Servers to Configure (Configure Database Mirroring Security Wizard)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use this page to specify which server instances you want to configure now. You must select at least one server instance before continuing the wizard.  
  
 If you clear the check box for a server instance, the wizard will not make any changes to it. The wizard, however, will ask you to enter information about that instance and save this information as part of the configuration of the other server instances. For example, if you clear the check box for the witness server instance, the wizard will ask you to enter the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account of the witness because a login for that account must be created as part of the security configuration saved at the principal and mirror server instances.  
  
 **To configure database mirroring by using SQL Server Management Studio**  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
-   [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-the-configuring-database-mirroring-security-wizard.md)  
  
## Options  
 **Principal server instance**  
 Select to configure security for the principal server.  
  
 **Mirror server instance**  
 Select to configure security for the mirror server.  
  
 **Witness server instance**  
 Select to configure security for the witness server (if present).  
  
## See Also  
 [Database Properties &#40;Mirroring Page&#41;](../../relational-databases/databases/database-properties-mirroring-page.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)  
  
  
