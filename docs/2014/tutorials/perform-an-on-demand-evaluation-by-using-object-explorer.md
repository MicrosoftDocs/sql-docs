---
title: "Perform an On-Demand Evaluation by Using Object Explorer | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: ee6d3b79-18bc-49d3-8a1d-0c0905b990f0
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Perform an On-Demand Evaluation by Using Object Explorer
  In this task, you will use Object Explorer to perform an on-demand evaluation of best practices policies for the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] on a single instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  You can also evaluate policies on a single instance through Registered Servers. For more information, see [Perform an On-Demand Evaluation by Using Registered Servers](../../2014/tutorials/perform-an-on-demand-evaluation-by-using-registered-servers.md).  
  
## Prerequisites  
 This lesson is based on the version of [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] from [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
> [!NOTE]  
>  To perform an on-demand evaluation of best practices policies against instances that are running [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], you must use the procedure in the topic [Perform an On-Demand Evaluation by Using Registered Servers](../../2014/tutorials/perform-an-on-demand-evaluation-by-using-registered-servers.md).  
  
### To perform an on-demand evaluation by using Object Explorer  
  
1.  Start [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], and then connect to the [!INCLUDE[ssDE](../includes/ssde-md.md)].  
  
2.  In Object Explorer, expand **Management**, expand **Policy Management**, right-click **Policies**, and then click **Evaluate**.  
  
    > [!NOTE]  
    >  By default, the local instance is used as the source of the policies. If you have previously imported best practices policies, they will be listed, together with any other policies that you have created. You can select any of the imported best practices policies, and then click **Evaluate**. If you have not imported the best practices policies, continue with this procedure.  
  
3.  In the **Evaluate Policies** dialog box, next to the **Source** box, click the ellipsis (**...**) button.  
  
4.  In the **Select Source** dialog box, you can select either **Files** or **Server** as the source of the policy files to evaluate. If you click **Server**, you can perform an on-demand evaluation of any best practices policies that were previously imported into Policy-Based Management on a local or remote server. In this tutorial, you will click **Files**, and then select the individual policy files that you want to evaluate. To do this, follow these steps:  
  
    1.  Click **Files**.  
  
    2.  Next to **Files**, click the ellipsis (**...**) button.  
  
    3.  In the **Select Policy** dialog box, browse to the following folder, which contains the best practices policies:  
  
         **C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Policies\DatabaseEngine\1033**  
  
        > [!NOTE]  
        >  The file path may vary, depending on where you installed the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] program files, whether you are running a 32-bit or 64-bit operating system, and the language identifier.  
  
    4.  Select one or more .xml policy files to evaluate, and then click **Open**.  
  
         The list of selected files appears in the **Files** box.  
  
    5.  In the **Select Source** dialog box, click **OK**.  
  
    6.  If the **Loading Policies** dialog box appears, click **Close**.  
  
     The policies that you selected are listed on the **Policy Selection** page. Be aware that a warning icon next to a policy indicates that the policy contains scripts.  
  
5.  Click **Evaluate** to evaluate the policies.  
  
     In the **Results** table, the results for each policy are listed. A red "x" icon indicates that policy compliance failed.  
  
6.  For some policy failures, Policy-Based Management enables you to immediately enforce policy compliance on the target. For such failures, a check box will appear next to the failed policy. If you select the check box, the **Apply** button becomes available. When you click **Apply**, the noncompliant setting will be automatically updated on the target instance.  
  
    > [!CAUTION]  
    >  Make sure that you fully understand the policy setting before automatically updating a target instance. We recommend that after you select one or more check boxes, you click **Script**, and choose an output location so that you can review the underlying [!INCLUDE[tsql](../includes/tsql-md.md)] code before you apply the changes.  
  
7.  To view detailed results for a policy, click the policy in the **Results** table.  
  
## Next Task in Lesson  
 [Perform an On-Demand Evaluation by Using Registered Servers](../../2014/tutorials/perform-an-on-demand-evaluation-by-using-registered-servers.md)  
  
  
