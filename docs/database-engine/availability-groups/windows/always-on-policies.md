---
title: "Evaluate health of the availability group using group policies"
description: "Learn how to view the group system policies that the Always On dashboard uses to provide information about the availability group health."
ms.custom: "ag-guide, seodec18"
ms.date: "06/13/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
ms.assetid: 26bf8f71-c2b8-45ef-b3a3-372b96c9e6e3
author: rothja
ms.author: jroth
manager: craigg
---
# Evaluate health of the Always On availability group using group policies
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The Always On availability groups system policies are used by the Always On Dashboard to provide information on the availability group health to the user. They are very useful for initial troubleshooting of an availability group's operational issues. These policies can be extended and used to customize the Always On Dashboard, or run instantly to report the desired health information.  
  
 There are 14 system policies for availability groups. For detailed information on each policy, see [Always On policies for operational issues with Always On Availability Groups (SQL Server)](always-on-policies-for-operational-issues-always-on-availability.md).  
  
## View or evaluate availability groups system policies  
 To view or run the availability groups system policies in SQL Server Management Studio (SSMS), do the following:  
  
1.  In the **Object Explorer**, expand **Management**, **Policy Management**, **Policies**, and then **System Policies**.  
  
2.  Right-click one of the policies and click **Evaluate**. If you want to evaluate the policy you selected, you are done. You can click **View** in the **Target details** box to see the details of the evaluation results.  
  
3.  To view all the availability groups system policies, in the **Select a page** pane, click **Policy Selection**.  
  
## Next steps  
 [The Always On health model, part 2: Extending the health model](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/13/extending-the-alwayson-health-model.aspx).   
  
  
