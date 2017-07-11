---
title: "Write Execution Trace Messages to SQL Server Agent Error Log | Microsoft Docs"
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
  - "logs [SQL Server], SQL Server Agent"
  - "writing trace messages"
  - "traces [SQL Server], SQL Server Agent logs"
  - "SQL Server Agent, errors"
  - "errors [SQL Server Agent]"
ms.assetid: 90e3731e-6fae-43db-833e-9accecdd1c03
caps.latest.revision: 6
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Write Execution Trace Messages to the SQL Server Agent Error Log (SQL Server Management Studio)
This topic describes how to configure [!INCLUDE[msCoName](../../includes/msconame_md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent to include execution trace messages in its error log in [!INCLUDE[ssCurrent](../../includes/sscurrent_md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull_md.md)].  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Limitations and Restrictions](#Restrictions)  
  
    [Security](#Security)  
  
-   To write execution trace messages to the SQL Server Agent Error Log using SQL Server Management Studio  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Restrictions"></a>Limitations and Restrictions  
  
-   Object Explorer only displays the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent node if you have permission to use it.  
  
-   Because this option can cause the error log to become large, only include execution trace messages in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent error logs when investigating a specific [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent problem.  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
To perform its functions, [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent must be configured to use the credentials of an account that is a member of the **sysadmin** fixed server role in [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)]. The account must have the following Windows permissions:  
  
-   Log on as a service (SeServiceLogonRight)  
  
-   Replace a process-level token (SeAssignPrimaryTokenPrivilege)  
  
-   Bypass traverse checking (SeChangeNotifyPrivilege)  
  
-   Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)  
  
For more information about the Windows permissions required for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent service account, see [Select an Account for the SQL Server Agent Service](../../ssms/agent/select-an-account-for-the-sql-server-agent-service.md) and [Setting Up Windows Service Accounts](http://msdn.microsoft.com/en-us/309b9dac-0b3a-4617-85ef-c4519ce9d014).  
  
## <a name="SSMSProcedure"></a>  
#### To write execution trace messages to the SQL Server Agent error log  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the [!INCLUDE[ssNoVersion](../../includes/ssnoversion_md.md)] Agent error log to which you want to write execution trace messages.  
  
2.  Right-click **SQL Server Agent** and select **Properties**.  
  
3.  In the **SQL Server Agent Properties –***server_name* dialog box, under **Error log** on the **General** page, select the **Include execution trace messages** check box.  
  
4.  Click **OK**.  
  
