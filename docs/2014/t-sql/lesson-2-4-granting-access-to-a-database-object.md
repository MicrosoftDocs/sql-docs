---
title: "Granting Access to a Database Object | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "granting access to database objects"
ms.assetid: a44d9bbf-f58e-4734-b7f4-eb3b492b777b
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Granting Access to a Database Object
  As an administrator, you can execute the SELECT from the **Products** table and the **vw_Names** view, and execute the **pr_Names** procedure; however, Mary cannot. To grant Mary the necessary permissions, use the GRANT statement.  
  
### Procedure Title  
  
1.  Execute the following statement to give `Mary` the `EXECUTE` permission for the `pr_Names` stored procedure.  
  
    ```  
    GRANT EXECUTE ON pr_Names TO Mary;  
    GO  
    ```  
  
 In this scenario, Mary can only access the **Products** table by using the stored procedure. If you want Mary to be able to execute a SELECT statement against the view, then you must also execute `GRANT SELECT ON vw_Names TO Mary`. To remove access to database objects, use the REVOKE statement.  
  
> [!NOTE]  
>  If the table, the view, and the stored procedure are not owned by the same schema, granting permissions becomes more complex.  
  
## About GRANT  
 You must have EXECUTE permission to execute a stored procedure. You must have SELECT, INSERT, UPDATE, and DELETE permissions to access and change data. The GRANT statement is also used for other permissions, such as permission to create tables.  
  
## Next Task in Lesson  
 [Summary: Configuring Permissions on Database Objects](lesson-2-5-summary-configuring-permissions-on-database-objects.md)  
  
## See Also  
 [GRANT &#40;Transact-SQL&#41;](/sql/t-sql/statements/grant-transact-sql)   
 [REVOKE &#40;Transact-SQL&#41;](/sql/t-sql/statements/revoke-transact-sql)  
  
  
