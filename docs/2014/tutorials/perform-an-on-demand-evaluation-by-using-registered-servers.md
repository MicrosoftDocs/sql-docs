---
title: "Perform an On-Demand Evaluation by Using Registered Servers | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: c14034ef-6e0b-4df5-8072-bfb8d90b3172
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Perform an On-Demand Evaluation by Using Registered Servers
  You can perform an on-demand evaluation of best practices policies against one or more instances of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using Registered Servers. You can use either local server groups or a central management server.  
  
> [!NOTE]  
>  You can perform an on-demand evaluation of best practices policies against server group members that are running [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] or a later version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. However, you may receive an exception error if there are some properties that are referred to by a policy that are not supported in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] or [!INCLUDE[ssVersion2000](../includes/ssversion2000-md.md)].  
  
## Prerequisites  
 To perform this task, you must have configured one or more server registrations in Registered Servers. For more information, see the following topics:  
  
-   [Create or Edit a Server Group &#40;SQL Server Management Studio&#41;](../ssms/register-servers/create-or-edit-a-server-group-sql-server-management-studio.md)  
  
-   [Register a Connected Server &#40;SQL Server Management Studio&#41;](../ssms/register-servers/register-a-connected-server-sql-server-management-studio.md).  
  
-   [Create a Central Management Server and Server Group &#40;SQL Server Management Studio&#41;](../ssms/register-servers/create-a-central-management-server-and-server-group.md)  
  
### To evaluate best practices policies against a server group  
  
1.  In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], on the **View** menu, click **Registered Servers**.  
  
2.  Expand **Database Engine**, and then expand either **Local Server Groups**, or **Central Management Servers**, depending on your configuration.  
  
3.  Do either of the following:  
  
    -   To evaluate the policies against all servers that are managed by the local server group or the central management server, right-click the local server group name or the central management server name, and then click **Evaluate Policies**.  
  
        > [!NOTE]  
        >  When you evaluate policies through a central management server, the policies are not evaluated against the central management server itself.  
  
    -   To evaluate the policies against a specific server or server group, expand **Local Server Groups** or the central management server name, right-click the server or server group that you want to evaluate policies against, and then click **Evaluate Policies**.  
  
4.  In the **Evaluate Policies** dialog box, next to the **Source** box, click the ellipsis (**...**) button.  
  
5.  In the **Select Source** dialog box, you can select either **Files** or **Server** as the source of the policy files to evaluate. If you click **Server**, you can perform an on-demand evaluation of any best practices policies that were previously imported into Policy-Based Management on a local or remote server. In this tutorial, you will click **Files**, and then select the individual policy files that you want to evaluate. To do this, follow these steps:  
  
    1.  Click **Files**.  
  
    2.  Next to **Files**, click the ellipsis (**...**) button.  
  
    3.  Select one or more .xml policy files to evaluate, and then click **Open**.  
  
         The list of selected files appears in the **Files** box.  
  
    4.  In the **Select Source** dialog box, click **OK**.  
  
    5.  If the **Loading Policies** dialog box appears, click **Close**.  
  
     The policies that you selected are listed on the **Policy Selection** page. Be aware that a warning icon next to a policy indicates that the policy contains scripts.  
  
6.  Click **Evaluate** to evaluate the policies.  
  
7.  For some policy failures, Policy-Based Management enables you to immediately enforce policy compliance on the target. For such failures, a check box will appear next to the failed policy. If you select the check box, or click the row with the failed policy, check boxes appear in the **Target details** pane next to the target instances that failed the evaluation. If any of the check boxes are selected, the **Apply** button becomes available. When you click **Apply**, the noncompliant setting will be automatically updated on the target instances that you selected.  
  
    > [!CAUTION]  
    >  Make sure that you fully understand the policy setting before automatically updating a target instance. We recommend that after you select one or more check boxes, you click **Script**, and choose an output location so that you can review the underlying [!INCLUDE[tsql](../includes/tsql-md.md)] code before you apply the changes.  
  
8.  To view detailed results for a policy, click the policy in the **Results** table. The **Target details** table shows the details for each instance.  
  
## Next Lesson  
 [Lesson 2: Evaluate Best Practices Policies on a Scheduled Basis](../../2014/tutorials/lesson-2-evaluate-best-practices-policies-on-a-scheduled-basis.md)  
  
## See Also  
 [Monitor and Enforce Best Practices by Using Policy-Based Management](../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)   
 [Administer Multiple Servers Using Central Management Servers](../relational-databases/administer-multiple-servers-using-central-management-servers.md)  
  
  
