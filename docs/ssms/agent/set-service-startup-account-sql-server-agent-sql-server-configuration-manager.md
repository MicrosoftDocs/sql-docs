---
title: "Set the Service Startup Account for SQL Server Agent | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, service accounts"
  - "startup accounts [SQL Server]"
  - "service startup accounts [SQL Server Agent]"
ms.assetid: 46ffe818-ebb5-43a0-840b-923f219a2472
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Set the Service Startup Account for SQL Server Agent (SQL Server Configuration Manager)
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service startup account defines the Windows account that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs as, as well as its network permissions. This topic describes how to set the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   [To set the Service Startup Account for SQL Server Agent using SQL Server Management Studio](#SSMSProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent no longer requires that the service startup account be a member of the [!INCLUDE[msCoName](../../includes/msconame_md.md)] Administrators group. However, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service startup account must be a member of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]sysadmin fixed server role. The account must also be a member of the msdb database role TargetServersRole on the master server if multiserver job processing is used.  
  
-   Object Explorer only displays the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent node if you have permission to use it.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
To perform its functions, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be configured to use the credentials of an account that is a member of the **sysadmin** fixed server role in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The account must have the following Windows permissions:  
  
-   Log on as a service (SeServiceLogonRight)  
  
-   Replace a process-level token (SeAssignPrimaryTokenPrivilege)  
  
-   Bypass traverse checking (SeChangeNotifyPrivilege)  
  
-   Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)  
  
For more information about the Windows permissions required for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account, see [Select an Account for the SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md) and [Setting Up Windows Service Accounts](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To set the Service Startup Account for SQL Server Agent  
  
1.  In **Registered Servers**, click the plus sign to expand **Database Engine**.  
  
2.  Click the plus sign to expand the **Local Server Groups** folder.  
  
3.  Right-click the server instance where you want set up the Service Startup Account, and select **SQL Server Configuration Manager...**.  
  
4.  In the **User Account Control** dialog box, click **Yes**.  
  
5.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, in the console pane, select **SQL Server Services**.  
  
6.  In the details pane, right-click **SQL Server Agent**_(server\_name)_, where *server_name* is the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent instance for which you want to change the service startup account, and select **Properties**.  
  
7.  In the **SQL Server Agent**_(server\_name)_ **Properties** dialog box, in the **Log On** tab, select one of the following options under **Log on as**:  
  
    -   **Built-in account**: select this option if your jobs require resources from the local server only. For information about how to choose a Windows built-in account type, see [Selecting an Account for SQL Server Agent Service.](https://msdn.microsoft.com/library/ms191543.aspx)  
  
        > [!IMPORTANT]  
        > The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service does not support the **Local Service** account in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
    -   **This account**: select this option if your jobs require resources across the network, including application resources; if you want to forward events to other Windows application logs; or if you want to notify operators through e-mail or pagers.  
  
        If you select this option:  
  
        1.  In the **Account Name** box, enter the account that will be used to run SQL Server Agent. Alternately, click **Browse** to open the **Select User or Group** dialog box and select the account to use.  
  
        2.  In the **Password** box, enter the password for the account. Re-enter the password in the **Confirm password** box.  
  
8.  Click **OK**.  
  
9. In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, click the **Close** button.  
  
