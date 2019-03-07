---
title: "Subscribe to and Check the Finance Name Policy | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 126b4c4c-2a1c-4701-a0ad-8de23fbd7306
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Subscribe to and Check the Finance Name Policy
  In this task, you will configure the Finance database to subscribe to the Finance policy category. Then, you will test the Finance Name policy.  
  
### To subscribe to the Finance policy category  
  
1.  In Object Explorer, expand **Databases**, right-click `Finance`, point to **Policies**, and then click **Categories**.  
  
2.  Select the **Subscribed** checkbox for the `Finance` category.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To test the enforcement of the Finance Name policy  
  
1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a query window. Execute the following statements that try to create a table that violates the **Finance Name** policy. The table violates the policy because the table name does not begin with the letters **fintbl**.  
  
    ```  
    USE Finance ;  
    GO  
    CREATE TABLE NewTable  
    (Col1 int) ;  
    GO  
  
    ```  
  
     Notice that the policy prevents the table from being created and returns an informational message that provides the policy name.  
  
2.  To provide a valid name, modify the code as follows and run the statement again.  
  
    ```  
    USE Finance ;  
    GO  
    CREATE TABLE fintblNewTable  
    (Col1 int) ;  
    GO  
  
    ```  
  
     This time, the table is created.  
  
### To apply the policy to the whole server  
  
1.  Currently, only the Finance database subscribes to the Finance policy category. In many cases, it is easier to apply the policy category to the whole server. In Object Explorer, expand **Management**, right-click **Policy Management**, and then click **Manage Categories**.  
  
2.  In the **Manage Policy Categories** dialog box, locate the Finance category, and select the **Mandate Database Subscriptions** checkbox for the Finance category.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)] Now the Finance category applies to all databases, but the condition that you have created restricts the Finance Name policy to the Finance database. This shows how you can use complex combinations of conditions to target policies in ways that will apply correctly on many servers.  
  
## Summary  
 This tutorial has shown you how to create Policy-Based Management conditions, policies and policy groups, and how to apply filters and check the compliance of Policy-Based Management targets.  
  
## Next  
 This tutorial is finished. To return to the start, click [Tutorial: Administering Servers by Using Policy-Based Management](tutorial-administering-servers-by-using-policy-based-management.md).  
  
 For a list of tutorials, see [Tutorials for SQL Server 2014](../../tutorials/tutorials-for-sql-server-2014.md).  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md)  
  
  
