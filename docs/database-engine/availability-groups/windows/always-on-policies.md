---
title: "Always On Availability Groups policies (SQL Server) | Microsoft Docs"
ms.custom: "ag-guide"
ms.date: "06/13/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 26bf8f71-c2b8-45ef-b3a3-372b96c9e6e3
caps.latest.revision: 7
author: "rothja"
ms.author: "jroth"
manager: "jhubbard"
---
# Always On Availability Groups policies
  The Always On Availability Groups system policies are used by the Always On Dashboard to provide information on the availability group health to the user. They are very useful for initial troubleshooting of an availability group’s operational issues. These policies can be extended and used to customize the Always On Dashboard, or run instantly to report the desired health information.  
  
 There are 14 system policies for availability groups. For detailed information on each policy, see [Always On policies for operational issues with Always On Availability Groups (SQL Server)](always-on-policies-for-operational-issues-always-on-availability.md).  
  
## View or evaluate availability groups system policies  
 To view or run the availability groups system policies in SQL Server Management Studio (SSMS), do the following:  
  
1.  In the **Object Explorer**, expand **Management**, **Policy Management**, **Policies**, and then **System Policies**.  
  
2.  Right-click one of the policies and click **Evaluate**. If you want to evaluate the policy you selected, you are done. You can click **View** in the **Target details** box to see the details of the evaluation results.  
  
3.  To view all the availability groups system policies, in the **Select a page** pane, click **Policy Selection**.  
  
## Next steps  
 [The Always On health model, part 2: Extending the health model](http://blogs.msdn.com/b/sqlalwayson/archive/2012/02/13/extending-the-alwayson-health-model.aspx).  
  
  