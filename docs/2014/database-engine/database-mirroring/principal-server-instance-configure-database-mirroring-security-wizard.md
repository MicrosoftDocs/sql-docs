---
title: "Principal Server Instance (Configure Database Mirroring Security Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.configdbmsecurwiz.principalsrvr.f1"
ms.assetid: 58af27d7-c5dd-4669-be6b-b472bc2c8ef4
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Principal Server Instance (Configure Database Mirroring Security Wizard)
  Use this page to specify information about the server instance of the principal database. The principal database is the copy of the database that begins the mirroring session. After the session has begun, the principal database is the copy of the database that accepts user changes. (When a failover occurs, the principal and mirroring roles are swapped; therefore, the initial principal database might not remain the principal database.)  
  
 **To configure database mirroring by using SQL Server Management Studio**  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](establish-database-mirroring-session-windows-authentication.md)  
  
-   [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](start-the-configuring-database-mirroring-security-wizard.md)  
  
## Options  
 **Principal server instance**  
 Because database mirroring in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] is always configured from the principal server, the current server instance is always the principal server instance.  
  
 **Listener Port**  
 The behavior of this option depends on whether the mirroring endpoint exists for this server instance, as follows:  
  
-   If the listener port does not exist for this server instance, port number 5022 is displayed in the **Port** text box. You can use any available port number, such as, 7022.  
  
-   When the mirroring endpoint already exists, the port number from the endpoint is displayed. If you need to change the port, use an ALTER ENDPOINT command. For more information, see [ALTER ENDPOINT &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-endpoint-transact-sql).  
  
> [!NOTE]  
>  A port number is required.  
  
 **Endpoint name**  
 If the mirroring endpoint exists for this server instance, the endpoint name is displayed here. If the endpoint does not exist, you can specify the name of the endpoint.  
  
 **Encrypt data sent through this endpoint**  
 By default, encryption is enabled. When enabled, encryption is required (not merely supported) and uses the default values for all of the encryption options. For more information, see [CREATE ENDPOINT &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-endpoint-transact-sql).  
  
 To disable encryption, clear the check box. To re-enable encryption, select the check box.  
  
## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](the-database-mirroring-endpoint-sql-server.md)   
 [Database Properties &#40;Mirroring Page&#41;](../../relational-databases/databases/database-properties-mirroring-page.md)   
 [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)   
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)  
  
  
