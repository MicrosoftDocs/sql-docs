---
title: "Include Witness Server (Configure Database Mirroring Security Wizard)"
description: Describes the 'Include Witness Server' page of the 'Configure Database Mirroring Security' Wizard within the SQL Server Management Studio (SSMS) GUI.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: database-mirroring
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.configdbmsecurwiz.inclwitness.f1"
---
# Include Witness Server (Configure Database Mirroring Security Wizard)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use this page to specify whether you want to include a witness server in this security configuration for database mirroring.  
  
 **To configure database mirroring by using SQL Server Management Studio**  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
-   [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-the-configuring-database-mirroring-security-wizard.md)  
  
## Options  
 **Yes**  
 Click to include a witness server instance in the security configuration. The witness is necessary for high-safety mode with automatic failover, which supports automatic failover to the mirror server instance if the principal server instance fails.  
  
 **No**  
 Click to configure security without a witness.  
  
## See Also  
 [Database Properties &#40;Mirroring Page&#41;](../../relational-databases/databases/database-properties-mirroring-page.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md)  
  
  
