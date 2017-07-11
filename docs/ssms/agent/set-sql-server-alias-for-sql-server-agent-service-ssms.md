---
title: "Set a SQL Server Alias for the SQL Server Agent Service | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "aliases [SQL Server], creating"
  - "SQL Server Agent, aliases"
ms.assetid: 02d6295d-ab52-44f0-8f1b-f3910a507d8f
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Set a SQL Server Alias for the SQL Server Agent Service (SQL Server Management Studio)
This topic describes how to set a [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] alias for [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent to use to connect to the [!INCLUDE[ssDE](../../includes/ssde_md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull_md.md)]. By default, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent service connects to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] over named pipes by using dynamic server names that require no additional client configuration. You need to configure a server connection alias only if you are not using the default network transport or if you are connecting to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] that listens on an alternate named pipe.  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   [To set a SQL Server Alias for the SQL Server Agent Service using SQL Server Management Studio](#SSMSProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent will not work correctly unless you select an alias that refers to the local instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
-   Object Explorer only displays the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent node if you have permission to use it.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
To perform its functions, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent must be configured to use the credentials of an account that is a member of the **sysadmin** fixed server role in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)]. The account must have the following Windows permissions:  
  
-   Log on as a service (SeServiceLogonRight)  
  
-   Replace a process-level token (SeAssignPrimaryTokenPrivilege)  
  
-   Bypass traverse checking (SeChangeNotifyPrivilege)  
  
-   Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)  
  
For more information about the Windows permissions required for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent service account, see [Select an Account for the SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md) and [Setting Up Windows Service Accounts](http://msdn.microsoft.com/en-us/309b9dac-0b3a-4617-85ef-c4519ce9d014).  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To set a SQL Server Alias for the SQL Server Agent Service  
  
1.  In the **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion_md.md)] and then expand that instance.  
  
2.  Right-click **SQL Server Agent**, and then click **Properties**.  
  
3.  In the **SQL Server Agent Properties***server_name* dialog box, under **Select a page**, select **Connection**, and  
  
4.  In the **Alias local host server** box, type the alias of the server to which [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent should connect.  
  
5.  Click **OK**.  
  
