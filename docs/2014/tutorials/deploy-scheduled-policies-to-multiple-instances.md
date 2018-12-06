---
title: "Deploy Scheduled Policies to Multiple Instances | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: f551b8e8-3668-4ed4-852f-bae826254f4f
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Deploy Scheduled Policies to Multiple Instances
  By using Registered Servers, you can deploy scheduled policies to managed servers from a central location. You can deploy scheduled policies from either a local server group, or from a Central Management Server.  
  
 In this task, you will do the following:  
  
1.  Export the policies that you scheduled in the previous task to a folder.  
  
2.  Deploy the scheduled policies to target instances that are managed through Registered Servers.  
  
 You will perform these tasks on the computer where you completed the previous tasks in this lesson.  
  
## Prerequisites  
 This task has the following prerequisites:  
  
-   You must have completed the previous tasks in this lesson.  
  
-   The instances where you want to deploy the scheduled policies must be running [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] or a later version. Automation requires the policies to be stored locally, which is not supported on versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)].  
  
-   The servers where you want to deploy the scheduled policies must be registered in Registered Servers in either the **Local Server Groups** or the **Central Management Servers** node. For more information, see the following topics:  
  
    -   [Create or Edit a Server Group &#40;SQL Server Management Studio&#41;](../ssms/register-servers/create-or-edit-a-server-group-sql-server-management-studio.md)  
  
    -   [Register a Connected Server &#40;SQL Server Management Studio&#41;](../ssms/register-servers/register-a-connected-server-sql-server-management-studio.md).  
  
    -   [Create a Central Management Server and Server Group &#40;SQL Server Management Studio&#41;](../ssms/register-servers/create-a-central-management-server-and-server-group.md)  
  
### To export the scheduled policies as .xml files  
  
1.  On the server where you configured scheduled policies in the previous task, expand **Management**, expand **Policy Management**, and then click **Policies**.  
  
2.  On the **View** menu, click **Object Explorer Details**.  
  
3.  In the **Object Explorer Details** pane, select all the scheduled best practices policies that you want to deploy to other servers through Registered Servers.  
  
    > [!NOTE]  
    >  You can click the **Category** heading to sort the policies by category.  
  
4.  Right-click the selection, and then click **Export Policy**.  
  
5.  If you selected multiple policies to export, in the **Browse For Folder** dialog box, select a destination folder, or create a new folder. For this lesson, create a new folder with the path **C:\Scheduled_BP_Policies**, and then click **OK**.  
  
     If you only selected one policy to export, in the **Export Policy** dialog box, create a new folder with the path **C:\Scheduled_BP_Policies**, click **Open**, and then click **Save**.  
  
     The .xml policy files are created in the folder location.  
  
### To deploy the scheduled policies to servers that are managed through Registered Servers  
  
1.  On the **View** menu, click **Registered Servers**.  
  
2.  Expand **Database Engine**, expand either **Local Server Groups** or **Central Management Servers**, right-click the node that you want to deploy the policies to, and then click **Import Policies**.  
  
    > [!NOTE]  
    >  If you right-click **Local Server Groups** or the Central Management Server itself, the policies will be deployed to all managed servers. If you right-click a specific server group, the policies will only be deployed to servers in that group. If you right-click a specific registered server, the policies will only be deployed to that server.  
  
3.  Next to **Files to import**, click the ellipsis button (**...**).  
  
4.  In the **Select Policy** dialog box, browse to the folder location where you saved the scheduled policies. For this example, browse to the location **C:\Scheduled_BP_Policies**.  
  
5.  Select the policies that you want to import to the target instances, and then click **Open**.  
  
6.  In the **Import** dialog box, in the **Policy state** list, select the desired policy state. You can choose to preserve the policy state, enable, or disable the policies on import. Be aware that the policies must be enabled to run on a schedule.  
  
7.  Click **OK** to import the policies to all the target instances.  
  
    > [!NOTE]  
    >  If there are any errors, the **Import** dialog box does not disappear. Click the **Log** page to review the messages. Click **Cancel** to close the dialog box.  
  
8.  To view the policies from a target instance, connect to the instance, open Object Explorer, expand **Management**, and then expand **Policies**. You should see the imported policies in the **Policies** node. If you double-click each policy, you can view the schedule, or change the settings.  
  
    > [!NOTE]  
    >  To view the evaluation results after a scheduled policy runs, open the Policy History log on the target instance. To open the log, right-click **Policy Management**, and then click **View History**.  
  
## Summary  
 This tutorial has shown you how to perform both on-demand and scheduled evaluations of the best practices policies against one or more instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
## Next  
 This tutorial is finished. To return to the start, see [Tutorial: Evaluating Best Practices by Using Policy-Based Management](../../2014/tutorials/tutorial-evaluating-best-practices-by-using-policy-based-management.md).  
  
 To see a list of [!INCLUDE[ssDE](../includes/ssde-md.md)] tutorials, click [Database Engine Tutorials](../relational-databases/database-engine-tutorials.md).  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)  
  
  
