---
title: "Delete an Operator | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent jobs, operators"
  - "canceling operators"
  - "removing operators"
  - "deleting operators"
  - "dropping operators"
  - "jobs [SQL Server Agent], operators"
  - "operators (users) [Database Engine], deleting with Management Studio"
ms.assetid: 2b7b8627-082d-4189-8584-abd3a9b604cf
author: stevestein
ms.author: sstein
manager: craigg
---
# Delete an Operator
  This topic describes how to remove an operator so they no longer receive [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alert notifications in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To delete an operator, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 When an operator is removed, all the notifications associated with the operator are also removed.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Members of the **sysadmin** fixed server role can delete operators.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete an operator  
  
1.  In **Object Explorer**, click the plus sign to expand the server that contains the operator you want to delete.  
  
2.  Click the plus sign to expand **SQL Server Agent**.  
  
3.  Click the plus sign to expand the **Operators** folder.  
  
4.  Right-click the operator that you want to delete and select **Delete**.  
  
5.  In the **Delete Object** dialog box, make sure that the correct operator is selected, and then click **OK**. If you want another operator to receive the alerts and jobs sent to the deleted operator, check **Reassign to** and select an operator in the list.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To delete an operator  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- deletes operator 'Test Operator' and reassigns all alerts and jobs sent to that operator to 'Fran??ois Ajenstat'  
    USE msdb ;  
    GO  
  
    EXEC sp_delete_operator @name = 'Test Operator',  
        @reassign_to_operator = 'Fran??ois Ajenstat';  
    GO  
    ```  
  
 For more information, see [sp_delete_operator &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-delete-operator-transact-sql).  
  
  
