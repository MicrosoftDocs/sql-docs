---
title: "Register Mirrored Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.dbmmonitor.registermirroreddb.f1"
ms.assetid: 6acd02b9-2311-49b0-a5f8-3852beecb4b0
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Register Mirrored Database
  Use this dialog box to register one or more mirrored databases on a given server instance by adding the database or databases to the Database Mirroring Monitor. When a database is added, Database Mirroring Monitor locally caches information about the database, its partners, and how to connect to the partners.  
  
> [!IMPORTANT]  
>  If you are a member of the **sysadmin** fixed server role on the principal server instance but not on the mirror server instance, you can only see status on the principal server instance.  
  
 **To use SQL Server Management Studio to monitor database mirroring**  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
## Options  
 **Server instance**  
 Select a server instance from the list, which contains server instances to which Database Mirroring Monitor already has a connection stored, or click **Connect**. To specify new credentials for a listed server instance, click **Connect** and connect using the new credentials.  
  
> [!NOTE]  
>  To register databases on multiple server instances, after you finish checking the desired databases for one server instance, click **Apply**, and then select another server instance.  
  
 **Connect**  
 To specify new credentials for the server instance, click **Connect** and connect using the new credentials. While connecting to a server instance, Database Mirroring Monitor displays **Waiting for data**.  
  
 **Mirrored databases**  
 The **Mirrored databases** grid lists the mirrored databases on the server instance.  
  
 The grid contains the following columns:  
  
|Column name|Description|  
|-----------------|-----------------|  
|**Register**|Check each of the databases that you want to register. If a database is currently monitored, its check box is checked and is disabled.<br /><br /> Note: To unregister a database, close the **Registered Mirrored Database** dialog box, select the database in the navigation tree, and select **Unregister** from the **Action** menu.|  
|**Database**|The name of a mirrored database on the selected server instance.|  
|**Current Role**|The current mirroring role of the database, either Principal or Mirror, on the selected server instance.|  
|**Partner (Connect as)**|The name of the failover partner for the database. Either **Windows Authentication of console user** or **SQL Server Authentication of login '***\<login name>***'** is displayed within the parentheses. This is the authentication information currently used, if the instance has been added before, or that will be used, if this instance has not been added to the monitor.|  
  
 **Show the Manage Server Connections dialog box when I click OK.**  
 By default, Database Mirroring Monitor uses Windows Authentication credentials for partner server instances for which credentials have not been previously given. Enable this option to change the credentials for one or more server instances when you finish registering databases.  
  
 If this option is enabled, when you click **OK**, the **Manage Server Connections** dialog box opens. There, you can choose a server instance for which you want to specify credentials for the monitor to use when connecting to a given failover partner.  
  
 To edit the credentials for a partner, locate its entry in the **Server instances** grid, and click **Edit** on that row. This opens the **Connect to Server** dialog box for that server instance name, with the credential controls initialized to the current cached value. Change the credentials as necessary and click **Connect**. If the credentials have sufficient privileges, the **Connect Using** column is updated with the new credentials.  
  
 **Apply**  
 Click this button to register the selected databases (and save credentials for the partner server instances) while keeping the dialog box open.  
  
## See Also  
 [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)   
 [Monitoring Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)   
 [Start the Configuring Database Mirroring Security Wizard &#40;SQL Server Management Studio&#41;](start-the-configuring-database-mirroring-security-wizard.md)  
  
  
