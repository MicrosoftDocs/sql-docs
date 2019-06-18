---
title: "Start, Stop, or Pause the SQL Server Agent Service | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, starting"
  - "SQL Server Agent, pausing"
  - "SQL Server Agent, stopping"
ms.assetid: c95a9759-dd30-4ab6-9ab0-087bb3bfb97c
author: "markingmyname"
ms.author: "maghan"
manager: craigg
monikerRange: "= azuresqldb-mi-current || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Start, Stop, or Pause the SQL Server Agent Service
[!INCLUDE[appliesto-ss-asdbmi-xxxx-xxx-md](../../includes/appliesto-ss-asdbmi-xxxx-xxx-md.md)]

> [!IMPORTANT]  
> On [Azure SQL Database Managed Instance](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance), most, but not all SQL Server Agent features are currently supported. See [Azure SQL Database Managed Instance T-SQL differences from SQL Server](https://docs.microsoft.com/azure/sql-database/sql-database-managed-instance-transact-sql-information#sql-server-agent) for details.

This topic describes how to start, stop, or restart the SQL Server Agent Service in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
You can configure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to start automatically when the operating system starts, or you can start it manually when you need to complete jobs. You can stop or pause the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service to suspend jobs, operator notifications, and alerts.  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   [To start, stop, or restart the SQL Server Agent Service using SQL Server Management Studio](#SSMSProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent must be running as a service in order to automate administrative tasks. For more information, see [Configure SQL Server Agent](../../ssms/agent/configure-sql-server-agent.md).  
  
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
  
#### To start, stop, or restart the SQL Server Agent Service  
  
1.  In **Object Explorer**, click the plus sign to expand the server where you want to manage SQL Server Agent Service.  
  
2.  Right-click **SQL Server Agent**, and then select either **Start**, **Stop**, or **Restart**.  
  
3.  In the **User Account Control** dialog box, click **Yes**.  
  
4.  When prompted if you want to perform the action, click **Yes**.  
  
For more information, see:  
  
-   [Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](https://msdn.microsoft.com/32660a02-e5a1-411a-9e57-7066ca459df6)  
  
-   [Autostart SQL Server Agent &#40;SQL Server Management Studio&#41;](../../ssms/agent/autostart-sql-server-agent-sql-server-management-studio.md)  
  
