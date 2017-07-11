---
title: "Defect a Target Server from a Master Server | Microsoft Docs"
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
  - "SQL Server Agent jobs, target servers"
  - "target servers [SQL Server], defecting"
  - "SQL Server Agent jobs, master servers"
  - "master servers [SQL Server], defecting target servers"
  - "defecting target servers"
ms.assetid: a6da262b-7b38-4ce4-bfd6-6a557c6e8a84
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Defect a Target Server from a Master Server
This topic describes how to defect a target server from a master server in [!INCLUDE[ssCurrent](../../includes/sscurrent_md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull_md.md)], [!INCLUDE[tsql](../../includes/tsql_md.md)], or SQL Server Management Objects (SMO). Run this procedure from the target server.  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Security](#Security)  
  
-   **To defect a target server, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure)  
  
    [Transact-SQL](#TsqlProcedure)  
  
    [SMO](#PowerShellProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
To execute this stored procedure, a user must be a member of the **sysadmin** fixed server role.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To defect a target server from a master server  
  
1.  In **Object Explorer**, expand a server that is configured as a target server.  
  
2.  Right-click **SQL Server Agent**, point to **Multi Server Administration**, and then click **Defect**.  
  
3.  Click **Yes** to confirm that you want to defect this target server from a master server.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To defect a target server from a master server  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
```  
sp_msx_defect ;  
```  
  
For more information, see [sp_msx_defect (Transact-SQL)](http://msdn.microsoft.com/en-us/0dfd963a-3bc5-4b58-94f7-aec976da2883).  
  
## <a name="PowerShellProcedure"></a>Using SQL Server Management Objects (SMO)  
Use the **MsxDefect Method**.  
  
## See Also  
[Create a Multiserver Environment](../../ssms/agent/create-a-multiserver-environment.md)  
[Automated Administration Across an Enterprise](../../ssms/agent/automated-administration-across-an-enterprise.md)  
[Defect Multiple Target Servers from a Master Server](../../ssms/agent/defect-multiple-target-servers-from-a-master-server.md)  
  
