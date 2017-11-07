---
title: "Set the SQL Server Connection for the SQL Server Agent Service | Microsoft Docs"
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
  - "SQL Server Agent, connections"
  - "connections [SQL Server], SQL Server Agent service"
ms.assetid: 28b6178b-0a9e-4f2c-8562-7a62d2d2a285
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Set the SQL Server Connection for the SQL Server Agent Service (SQL Server Management Studio)
This topic describes how to set the connection between [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent and the [!INCLUDE[ssDE](../../includes/ssde_md.md)] in [!INCLUDE[ssCurrent](../../includes/sscurrent_md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull_md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent service can connect to a local instance of SQL Server by using Windows Authentication.  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   **To set the SQL Server Connection for the SQL Server Agent, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   Object Explorer only displays the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent node if you have permission to use it.  
  
-   Beginning with [!INCLUDE[ssVersion2005](../../includes/ssversion2005_md.md)], [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent does not support [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Authentication. This option is available only when you administer an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)].  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
To perform its functions, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent must be configured to use the credentials of an account that is a member of the **sysadmin** fixed server role in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)]. The account must have the following Windows permissions:  
  
-   Log on as a service (SeServiceLogonRight)  
  
-   Replace a process-level token (SeAssignPrimaryTokenPrivilege)  
  
-   Bypass traverse checking (SeChangeNotifyPrivilege)  
  
-   Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)  
  
For more information about the Windows permissions required for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent service account, see [Select an Account for the SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md) and [Setting Up Windows Service Accounts](http://msdn.microsoft.com/en-us/309b9dac-0b3a-4617-85ef-c4519ce9d014).  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To set the SQL Server connection  
  
1.  In **Object Explorer**, click the plus sign to expand the server that you want to set up with a connection to its SQL Server Agent Service.  
  
2.  Right-click **SQL Server Agent** and select **Properties**.  
  
3.  In the **SQL Server Agent Properties***sever_name* dialog box, under **Select a page**, click **Connection**.  
  
4.  Under **SQL Server connection**, select **Use Windows Authentication** to enable [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent to connect to an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] [!INCLUDE[ssDE](../../includes/ssde_md.md)] with [!INCLUDE[msCoName](../../includes/msconame_md.md)] Windows Authentication. Connections to [!INCLUDE[ssVersion2005](../../includes/ssversion2005_md.md)] and later databases require Windows Authentication.  
  
