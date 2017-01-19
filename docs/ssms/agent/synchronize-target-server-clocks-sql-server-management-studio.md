---
title: "Synchronize Target Server Clocks (SQL Server Management Studio) | Microsoft Docs"
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
  - "clocks [SQL Server]"
  - "master servers [SQL Server], clock synchronization"
  - "synchronization [SQL Server], target server clocks"
  - "target servers [SQL Server], clock synchronization"
ms.assetid: 4fb80502-d271-4d06-bcbc-bfbbceb5f2a2
caps.latest.revision: 5
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Synchronize Target Server Clocks (SQL Server Management Studio)
This topic describes how to synchronize target server clocks in [!INCLUDE[ssCurrent](../../includes/sscurrent_md.md)] with the master server clock by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull_md.md)] or [!INCLUDE[tsql](../../includes/tsql_md.md)]. Synchronizing these system clocks supports your job schedules.  
  
**In This Topic**  
  
-   **Before you begin:**  
  
    [Security](#Security)  
  
-   **To synchronize target server clocks, using:**  
  
    [SQL Server Management Studio](#SSMSProcedure)  
  
    [Transact-SQL](#TsqlProcedure)  
  
## <a name="BeforeYouBegin"></a>Before You Begin  
  
### <a name="Security"></a>Security  
  
#### <a name="Permissions"></a>Permissions  
Requires membership in the **sysadmin** fixed server role.  
  
## <a name="SSMSProcedure"></a>Using SQL Server Management Studio  
  
#### To synchronize target server clocks  
  
1.  In **Object Explorer,** click the plus sign to expand the server where you want to synchronize the target server clocks with the master server clock.  
  
2.  Right-click **SQL Server Agent**, point to **Multi Server Administration**, and select **Manage Target Servers**.  
  
3.  In the **Manage Target Servers** dialog box, click **Post Instructions**.  
  
4.  In the **Instruction type** list, select **Synchronize clocks**.  
  
5.  Under **Recipients**, do one of the following:  
  
    -   Click **All target servers** to synchronize all target server clocks with the master server clock.  
  
    -   Click **These target servers** to synchronize certain server clocks, and then select each target server whose clock you want to synchronize with the master server clock.  
  
6.  When finished, click **OK**.  
  
## <a name="TsqlProcedure"></a>Using Transact-SQL  
  
#### To synchronize target server clocks  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde_md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE msdb ;  
    GO  
    -- resynchronizes the SEATTLE1 target server  
    EXEC dbo.sp_resync_targetserver  
        N'SEATTLE1' ;  
    GO  
    ```  
  
For more information, see [sp_resync_targetserver (Transact-SQL)](http://msdn.microsoft.com/en-us/40e44df7-d3e3-44ee-b149-08aba629a21f).  
  
