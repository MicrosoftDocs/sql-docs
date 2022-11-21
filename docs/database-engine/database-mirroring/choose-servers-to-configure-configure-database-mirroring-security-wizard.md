---
title: "Configure Security Wizard: Choose Servers"
description: "Describes the properties found on the 'Choose Servers' page of the 'Configure Database Mirroring Security Wizard'."
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.configdbmsecurwiz.choosesrvrs.f1"
---
# Configure Database Mirroring Wizard: Choose Servers to Configure 
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
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
  
  
