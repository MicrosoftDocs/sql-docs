---
title: "Database Properties (Mirroring Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.databaseproperties.mirroring.f1"
ms.assetid: 5bdcd20f-532d-4ee6-b2c7-18dbb7584a87
author: stevestein
ms.author: sstein
manager: craigg
---
# Database Properties (Mirroring Page)
  Access this page from the principal database, and use it to configure and to modify the properties of database mirroring for a database. Also use it to launch the Configure Database Mirroring Security Wizard, to view the status of a mirroring session, and to pause or remove the database mirroring session.  
  
> [!IMPORTANT]  
>  Security must be configured before you can start mirroring. If mirroring has not been started, you must begin by using the wizard. The **Mirroring** page textboxes are disabled until the wizard has been finished.  
  
 **To configure database mirroring by using SQL Server Management Studio**  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
## Options  
 **Configure Security**  
 Click this button to launch the **Configure Database Mirroring Security Wizard.**  
  
 If the wizard completes successfully, the action taken depends on whether mirroring has already begun, as follows:  
  
|||  
|-|-|  
|If mirroring has not begun.|The property page caches that connection information and, also, caches a value that indicates whether the mirror database has the partner property set.<br /><br /> At the end of the wizard, you are prompted to start database mirroring using the default server network addresses and operating mode. If you need to change the addresses or operating mode, click **Do Not Start Mirroring**.|  
|If mirroring has begun.|If the witness server was changed in the wizard, it is set accordingly.|  
  
 **Server network addresses**  
 An equivalent option exists for each of the server instances: **Principal**, **Mirror**, and **Witness**.  
  
 The server network addresses of the server instances are specified automatically when you complete the Configure Database Mirroring Security Wizard. After completing the wizard, you can modify the network addresses manually, if necessary.  
  
 The server network address has the following basic syntax:  
  
 TCP**://**_fully_qualified_domain_name_**:**_port_  
  
 where  
  
-   *fully_qualified_domain_name* is the server on which the server instance exists.  
  
-   *port* is the port assigned to the database mirroring endpoint of the server instance.  
  
     To participate in database mirroring, a server requires a database mirroring endpoint. When you use the Configure Database Mirroring Security Wizard to establish the first mirroring session for a server instance, the wizard automatically creates the endpoint and configures it to use Windows Authentication. For information about how to use the wizard with certificate-based authentication, see [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md).  
  
    > [!IMPORTANT]  
    >  Each server instance requires one and only one database mirroring endpoint, regardless of the number of mirroring session to be supported.  
  
 For example, for a server instance on a computer system named `DBSERVER9` whose endpoint uses port `7022`, the network address might be:  
  
```  
TCP://DBSERVER9.COMPANYINFO.ADVENTURE-WORKS.COM:7022  
```  
  
 For more information, see [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md).  
  
> [!NOTE]  
>  During a database mirroring session the principal and mirror server instances cannot be changed; the witness server instance, however, can be changed during a session. For more information, see "Remarks," later in this topic.  
  
 **Start Mirroring**  
 Click to begin mirroring, when all of the following conditions exist:  
  
-   The mirror database must exist.  
  
     Before you can start mirroring, the mirror database must have been created by restoring WITH NORECOVERY a recent full backup and, perhaps, log backups of the principal database onto the mirror server. For more information, see [Prepare a Mirror Database for Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/prepare-a-mirror-database-for-mirroring-sql-server.md).  
  
-   The TCP addresses of the principal and mirror server instances are already specified (in the **Server network addresses** section).  
  
-   If the operating mode is set to high safety with automatic failover (synchronous), the TCP address of the mirror server instance is also specified.  
  
-   Security has been configured correctly.  
  
 Click **Start Mirroring** to initiate the session. The Database Engine attempts to automatically connect to the mirroring partner to verify that the mirror server is correctly configured and begin the mirroring session. If mirroring can be started, a job is created to monitor the database.  
  
 **Pause** or **Resume**  
 During a database mirroring session, click **Pause** to pause the session. A prompt asks for confirmation; if you click **Yes**, the session is paused, and the button changes to **Resume**. To resume the session, click **Resume**.  
  
 For information about the impact of pausing a session, see [Pausing and Resuming Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).  
  
> [!IMPORTANT]  
>  Following a forced service, when the original principal server reconnects, mirroring is suspended. Resuming mirroring in this situation could possibly cause data loss on the original principal server. For information about how to manage the potential data loss, see [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md).  
  
 **Remove Mirroring**  
 On the principal server instance, click to stop the session and remove the mirroring configuration from the databases. A prompt asks for confirmation; if you click **Yes**, the session is stopped and mirroring is removed. For information about the impact of removing database mirroring, see [Removing Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/removing-database-mirroring-sql-server.md).  
  
> [!NOTE]  
>  If this is the only mirrored database on the server instance, the monitor job is removed.  
  
 **Failover**  
 Click to fail over the principal database to the mirror database manually.  
  
> [!NOTE]  
>  If the mirroring session is running in high-performance mode, manual failover is not supported. To fail over manually, you must first change the operating mode to **High safety without automatic failover (synchronous)**. After failover completes, you can change the mode back to **High performance (asynchronous)** on the new principal server instance.  
  
 A prompt asks for confirmation. If you click **Yes**, failover is attempted. The principal server begins by trying to connect to the mirror server by using Windows Authentication. If Windows Authentication does not work, the principal server displays the **Connect to Server** dialog box. If the mirror server uses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, select **SQL Server Authentication** in the **Authentication** box. In the **Login** text box, specify the login account to connect with on the mirror server, and in the **Password** text box, specify the password for that account.  
  
 If failover succeeds, the **Database Properties** dialog box closes. The principal and mirror server roles are switched: the former mirror database becomes the principal database, and vice versa. Note that the **Database Properties** dialog box becomes unavailable on the old principal database immediately because it has become the mirror database; this dialog box will become available on the new principal database after failover.  
  
 If failover fails, an error message is displayed, and the dialog box remains open.  
  
> [!IMPORTANT]  
>  If you click **Failover** after modifying properties in the **Database Properties** dialog box, those changes are lost. To save your current changes, answer **No** to the confirmation prompt, and click **OK** to save your changes. Then, reopen the database properties dialog box and click **Failover**.  
  
 **Operating mode**  
 Optionally, change the operating mode. The availability of certain operating modes depends on whether you have specified a TCP address for a witness. The options are as follows:  
  
|Option|Witness?|Explanation|  
|------------|--------------|-----------------|  
|**High performance (asynchronous)**|Null (if exists, not used but the session requires a quorum)|To maximize performance, the mirror database always lags somewhat behind the principal database, never quite catching up. However, the gap between the databases is typically small. The loss of a partner has the following effect:<br /><br /> If the mirror server instance becomes unavailable, the principal continues.<br /><br /> If the principal server instance becomes unavailable, the mirror stops. But if the session has no witness (as recommended) or the witness is connected to the mirror server, the mirror server remains accessible as a warm standby; the database owner can force service to the mirror server instance (with possible data loss).|  
|**High safety without automatic failover (synchronous)**|No|All committed transactions are guaranteed to be written to disk on the mirror server. Manual failover is possible if the partners are connected to each other. The loss of a partner has the following effect:<br /><br /> If the mirror server instance becomes unavailable, the principal continues.<br /><br /> If the principal server instance becomes unavailable, the mirror stops but is available as a warm standby; the database owner can force service to the mirror server instance (with possible data loss).|  
|**High safety with automatic failover (synchronous)**|Yes (required)|Maximized availability by including a witness server instance to support automatic failover. Note that you can select the **High safety with automatic failover (synchronous)** option only if you have first specified a witness server address. Manual failover is possible whenever the partners are connected to each other. **&#42;&#42; Important &#42;&#42;** If the witness becomes disconnected, the partners must be connected to each other for the database to be available. For more information, see [Quorum: How a Witness Affects Database Availability &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/quorum-how-a-witness-affects-database-availability-database-mirroring.md).<br /><br /> In the synchronous operating modes, all committed transactions are guaranteed to be written to disk on the mirror server. In the presence of a witness, the loss of a partner has the following effect:<br /><br /> If the principal server instance becomes unavailable, automatic failover occurs. The mirror server instance switches to the role of principal, and it offers its database as the principal database.<br /><br /> If the mirror server instance becomes unavailable, the principal continues.<br /><br /> <br /><br /> For more information, see [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).|  
  
 After mirroring begins, you can change the operating mode and save the change by clicking **OK**.  
  
 For more information about operating modes, see [Database Mirroring Operating Modes](../../database-engine/database-mirroring/database-mirroring-operating-modes.md).  
  
 **Status**  
 After mirroring begins, the **Status** panel displays the status of the database mirroring session as of when you selected the **Mirroring** page. To update the **Status** panel, click the **Refresh** button. The possible states are as follows:  
  
|States|Explanation|  
|------------|-----------------|  
|**This database has not been configured for mirroring**|No database mirroring session exists and there is no activity to report on the **Mirroring** page.|  
|**Paused**|The principal database is available but is not sending any logs to the mirror server.|  
|**No connection**|The principal server instance cannot connect to its partner.|  
|**Synchronizing**|The contents of the mirror database are lagging behind the contents of the principal database. The principal server instance is sending log records to the mirror server instance, which is applying the changes to the mirror database to roll it forward.<br /><br /> At the start of a database mirroring session, the mirror and principal databases are in this state.|  
|**Failover**|On the principal server instance, a manual failover (role switching) has begun, and the server is currently transitioning into the mirror role. In this state, user connections to the principal database are terminated quickly, and the database takes over the mirror role soon thereafter.|  
|**Synchronized**|When the mirror server becomes sufficiently caught up to the principal server, the database state changes to **Synchronized**. The database remains in this state as long as the principal server continues to send changes to the mirror server and the mirror server continues to apply changes to the mirror database.<br /><br /> For high-safety mode, failover is possible, without any data loss.<br /><br /> For high-performance mode, some data loss is always possible, even in the **Synchronized** state.|  
  
 For more information, see [Mirroring States &#40;SQL Server&#41;](../../database-engine/database-mirroring/mirroring-states-sql-server.md).  
  
 **Refresh**  
 Click to update the **Status** box.  
  
## Remarks  
 If you are unfamiliar with database mirroring, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).  
  
### Adding a Witness to an Existing Session  
 You can add a witness to an existing session or replace an existing witness. If you know the server network address of the witness, you can enter it into the **Witness** field manually. If you do not know the server network address of the witness, use Configure Database Mirroring Security Wizard to configure the witness. After the address is in the field, make sure that the **High-safety with automatic failover (synchronous)** option is selected.  
  
 After you configure a new witness, you must click **Ok** to add it to the mirroring session.  
  
 **To add a witness when using Windows Authentication**  
  
 [Add or Replace a Database Mirroring Witness &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/add-or-replace-a-database-mirroring-witness-sql-server-management-studio.md)  
  
### Removing a Witness  
 To remove a witness, delete its server network address from the **Witness** field. If you switch from high-safety mode with automatic failover to high-performance mode, the **Witness** field is automatically cleared.  
  
 After deleting the witness, you must click **Ok** to remove it from the mirroring session.  
  
### Monitoring Database Mirroring  
 To monitor the mirrored databases on a server instance, you can use either the Database Mirroring Monitor or the sp_dbmmonitorresults system stored procedure.  
  
 **To monitor mirrored databases**  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
-   [sp_dbmmonitorresults &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql)  
  
 For more information, see [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Specify a Server Network Address &#40;Database Mirroring&#41;](../../database-engine/database-mirroring/specify-a-server-network-address-database-mirroring.md)  
  
-   [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md)  
  
-   [Start Database Mirroring Monitor &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/start-database-mirroring-monitor-sql-server-management-studio.md)  
  
## See Also  
 [Transport Security for Database Mirroring and AlwaysOn Availability Groups &#40;SQL Server&#41;](../../database-engine/database-mirroring/transport-security-database-mirroring-always-on-availability.md)   
 [Role Switching During a Database Mirroring Session &#40;SQL Server&#41;](../../database-engine/database-mirroring/role-switching-during-a-database-mirroring-session-sql-server.md)   
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)   
 [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md)   
 [Pausing and Resuming Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/pausing-and-resuming-database-mirroring-sql-server.md)   
 [Removing Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/removing-database-mirroring-sql-server.md)   
 [Database Mirroring Witness](../../database-engine/database-mirroring/database-mirroring-witness.md)  
  
  
