---
title: "Schedule the Policies | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 59417a54-55f1-4468-ba41-368aa852c2d4
author: craigg-msft
ms.author: craigg
manager: craigg
---
# Schedule the Policies
  In this task, you will schedule the best practices policies that you imported in the previous task.  
  
### To schedule the best practices policies  
  
1.  In Object Explorer, expand **Management**, expand **Policy Management**, expand **Policies**, right-click a best practices policy, and then click **Properties**.  
  
    > [!NOTE]  
    >  As an alternative, to easily see which policies are associated with best practices and to sort the best practices categories, expand **Management**, expand **Policy Management**, and then click **Policies**. On the **View** menu, click **Object Explorer Details**. In the **Object Explorer Details** pane, click the **Category** heading to sort the policies by category. The best practices policies have the prefix **Microsoft Best Practices**. Right-click the policy that you want to configure, and then click **Properties**.  
  
2.  On the **General** page of the **Open Policy** dialog box, in the **Evaluation Mode** list, click **On schedule**.  
  
3.  Next to the **Schedule** box, click **Pick** to specify an existing schedule, or click **New** to create a new schedule.  
  
4.  After you have configured a schedule, you can select the **Enabled** check box near the top of the page to enable the scheduled policy.  
  
5.  Repeats steps 1 through 4 for each policy that you want to schedule.  
  
    > [!NOTE]  
    >  To view the evaluation results after a scheduled policy runs, open the Policy History log on the target instance. To open the log, right-click **Policy Management**, and then click **View History**.  
  
## Summary  
 You configured scheduled policies to run on a single instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. If you want to deploy scheduled policies to multiple instances, continue to the next task in this tutorial.  
  
## Next Steps  
 [Deploy Scheduled Policies to Multiple Instances](../../2014/tutorials/deploy-scheduled-policies-to-multiple-instances.md)  
  
  
