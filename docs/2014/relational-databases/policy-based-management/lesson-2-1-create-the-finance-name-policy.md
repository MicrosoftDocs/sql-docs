---
title: "Create the Finance Name Policy | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 56b2c852-fd69-4cd2-9b5d-977467b94fd9
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create the Finance Name Policy
  In this task, you will create a database named Finance, and then create a condition that requires all tables to start with the letters **fintbl**. Then, you will create a policy and policy category to enforce a naming standard for tables in the Finance database.  
  
### To create the Finance database  
  
1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], open a query window and execute the following statement:  
  
    ```  
    CREATE DATABASE Finance ;  
    GO  
    ```  
  
2.  In Object Explorer, click **Databases**, and then press F5 to refresh the list of databases.  
  
### To create the Finance Tables condition  
  
1.  In Object Explorer, expand **Management**, expand **Policy Management**, right-click **Conditions**, and then click **New Condition**.  
  
2.  In the **Create New Condition** dialog box, in the **Name** box, type **Finance Tables**.  
  
3.  In the **Facet** list, select **Multipart Name**.  
  
4.  In the **Expression** area, in the **Field** box, select **@Name**; in the **Operator** box, select **Like**; and in the **Value** box, type **'fintbl%'** to force all table names to start with the letters **fintbl**.  
  
5.  On the **Description** page, type **Finance table names must begin with fintbl**, and then click **OK** to create the condition.  
  
### To create the Finance Name policy  
  
1.  In Object Explorer, right-click **Policies**, and then click **New Policy**.  
  
2.  In the **Create New Policy** dialog box, in the **Name** box, type **Finance Name**.  
  
3.  In the **Check condition** list, select **Finance Tables**. This is in the **Multipart Name** area.  
  
4.  In the **Against** area you will see a list of the database objects that could apply this policy. Select the check box for **Every Table**.  
  
5.  In the **Every Database** area, expand **Every**, and then click **New condition**.  
  
6.  In the **Create New Condition** dialog box, in the **Name** box, type **Finance Database**.  
  
7.  In the **Expression** box, complete the expression to include **@Name = 'Finance'**, and then click **OK** to close the condition page.  
  
    > [!NOTE]  
    >  You might have to tab out of the **Value** box to enable the **OK** button.  
  
8.  In the **Evaluation Mode** list, select **On change: prevent**. This will enforce the policy by creating a database trigger on the Finance database.  
  
9. Select the **Enabled** list. (The **Enabled** box does not apply to **On demand** policies.)  
  
10. In the **Server restriction** list, select **None**.  
  
11. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To create the Finance policy category  
  
1.  In Object Explorer, expand **Management**, right-click **Policy Management**, and then click **Manage Categories**.  
  
2.  In the **Manage Policy Categories** dialog box, under **Name**, type `Finance` in the blank box, and then clear **Mandate Database Subscriptions**. **Mandate Database Subscriptions** will force every database in the instance to subscribe to the policies that belong to this policy category. For this lesson, only the Finance database should subscribe to the Finance Name policy.  
  
3.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## Next Task in Lesson  
 [Subscribe to and Check the Finance Name Policy](lesson-2-2-subscribe-to-and-check-the-finance-name-policy.md)  
  
  
