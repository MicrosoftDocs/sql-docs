---
title: "Witness Server Instance (Configure Database Mirroring Security Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.configdbmsecurwiz.witnsrvr.f1"
ms.assetid: b5763663-984a-473b-93a3-6cd3322ad41c
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Witness Server Instance (Configure Database Mirroring Security Wizard)
  Use this page to specify information about the server instance that is to serve as the witness for the session.  
  
> [!NOTE]  
>  A witness server instance is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md).  
  
 **To configure database mirroring by using SQL Server Management Studio**  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](establish-database-mirroring-session-windows-authentication.md)  
  
-   [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](start-the-configuring-database-mirroring-security-wizard.md)  
  
## Options  
 **Witness server instance**  
 If a witness server instance is already specified (on the **Mirroring** page of the **Database Properties** dialog box), that instance is displayed (for more information, see [Database Properties &#40;Mirroring Page&#41;](../../relational-databases/databases/database-properties-mirroring-page.md)).  
  
 Otherwise, this list box displays the name of the current server. Be aware that the witness server instance cannot be the same as the principal or mirror server instances.  
  
 **Connect**  
 If a witness server instance has not been specified, click **Connect**. This displays the **Connect to Server** dialog box in which you can specify the server instance and establish a connection.  
  
 If the instance has been specified, but the wizard lacks a connection with sufficient permission to check for the existence of the endpoint, click **Connect**. This displays the **Connect to Server** dialog box with the server instance pre-selected and unchangeable. Specify a domain account with sufficient permission, and connect to the server instance.  
  
> [!NOTE]  
>  When connecting to the server instance, the Configure Database Mirroring Security Wizard uses the credentials provided in the **Connect to Server** dialog box. These are different from the credentials of a mirroring session, which uses the credentials of the startup account where the server instance is running as a service.  
  
 **Listener Port**  
 The behavior of this option depends on whether the mirroring endpoint exists for this server instance, as follows:  
  
-   If the listener port does not exist for the server instance, port number 5022 is displayed in the **Port** text box. You can enter any available port number, such as, 7022.  
  
-   When the mirroring endpoint already exists, the port number from the endpoint is displayed. If you need to change that port, use an ALTER ENDPOINT statement. For more information, see [ALTER ENDPOINT &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-endpoint-transact-sql).  
  
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
 [Database Mirroring Witness](database-mirroring-witness.md)  
  
  
