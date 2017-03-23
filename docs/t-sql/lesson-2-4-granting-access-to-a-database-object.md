---
title: "Granting Access to a Database Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
helpviewer_keywords: 
  - "granting access to database objects"
ms.assetid: a44d9bbf-f58e-4734-b7f4-eb3b492b777b
caps.latest.revision: 14
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Lesson 2-4 - Granting Access to a Database Object
As an administrator, you can execute the SELECT from the **Products** table and the **vw_Names** view, and execute the **pr_Names** procedure; however, Mary cannot. To grant Mary the necessary permissions, use the GRANT statement.  
  
### Procedure Title  
  
1.  Execute the following statement to give `Mary` the `EXECUTE` permission for the `pr_Names` stored procedure.  
  
    ```  
    GRANT EXECUTE ON pr_Names TO Mary;  
    GO  
    ```  
  
In this scenario, Mary can only access the **Products** table by using the stored procedure. If you want Mary to be able to execute a SELECT statement against the view, then you must also execute `GRANT SELECT ON vw_Names TO Mary`. To remove access to database objects, use the REVOKE statement.  
  
> [!NOTE]  
> If the table, the view, and the stored procedure are not owned by the same schema, granting permissions becomes more complex.  
  
## About GRANT  
You must have EXECUTE permission to execute a stored procedure. You must have SELECT, INSERT, UPDATE, and DELETE permissions to access and change data. The GRANT statement is also used for other permissions, such as permission to create tables.  
  
## Next Task in Lesson  
[Summary: Configuring Permissions on Database Objects](../t-sql/lesson-2-5-summary-configuring-permissions-on-database-objects.md)  
  
## See Also  
[GRANT &#40;Transact-SQL&#41;](../t-sql/statements/grant-transact-sql.md)  
[REVOKE &#40;Transact-SQL&#41;](../t-sql/statements/revoke-transact-sql.md)  
  
  
  
