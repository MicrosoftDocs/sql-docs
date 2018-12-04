---
title: "Create New Policy or Open Policy Dialog Box, General Page | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.dmf.policy.f1"
  - "sql13.swb.dmf.policy.filter.f1"
  - "sql13.swb.dmf.newgroup.f1"
ms.assetid: c00bebd0-d04b-4c64-840e-8b7a2c603436
author: VanMSFT
ms.author: vanto
manager: craigg
---
# Create New Policy or Open Policy Dialog Box, General Page
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use this dialog box to create a new Policy-Based Management policy or modify an existing policy. Use the **Against targets** and **Server restriction** areas as a filter to limit policies to a subset of all possible targets. For conditions to be used as target filters, they must be defined on a physical facet, must not contain functions, and must not contain the LIKE operator. When the system computes the object set for a policy, by default the system objects are excluded.  For example, if the object set of the policy refers to all tables, the policy will not apply to system tables. If users want to evaluate a policy against system objects, they can explicitly add system objects to the object set. However, though all policies are supported for **check on schedule** evaluation mode, for performance reason, not all policies with arbitrary object sets are supported for **check on change** evaluation mode. For more information, see [https://blogs.msdn.com/b/sqlpbm/archive/2009/04/13/policy-evaluation-modes.aspx](https://blogs.msdn.com/b/sqlpbm/archive/2009/04/13/policy-evaluation-modes.aspx)  
  
## Options  
 **Name**  
 For a new policy, type the new policy name. For an existing policy, the name is displayed.  
  
 **Enabled**  
 Select the **Enabled** check box to enable the policy. Clear the **Enabled** check box to disable the policy. The **Enabled** box applies to policy automation. It creates or removes the automation system for the policy. Automation uses the following mechanisms:  
  
 **On change: prevent**  
 A database trigger enforces compliance.  
  
 **On change: log only**  
 A notification services event checks for compliance.  
  
 **On schedule**  
 A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job is created to check for compliance on a schedule.  
  
 Policies that are run using **On demand** evaluation mode do not use this check box.  
  
 **Check condition**  
 Select the Policy-Based Management condition that this policy uses. All conditions on the server for the associated Policy-Based Management facet are listed. Click **New condition** to create a new condition. Click the ellipsis (**...**) button to modify the condition.  
  
 **Against targets**  
 Select the target types that are available for this facet to complete a filter expression.  
  
 **Evaluation Mode**  
 Select the evaluation mode for the policy. Some policies can be checked but not enforced. The evaluation modes are as follows:  
  
 **On demand**  
 Policy will only be run when you run it from the **Evaluate** dialog box.  
  
 **On schedule**  
 Periodically evaluates the policy, records a log entry for policies that have out-of-compliance, and creates a report. Enables the **Schedule** box.  
  
 **On change: log only**  
 When changes are tried, this option does not prevent out-of-compliance changes, but logs policy violations.  
  
 **On change: prevent**  
 When changes are tried, this option prevents changes that would violate the policy.  
  
 **Schedule**  
 This option appears when **On schedule** evaluation mode is selected. Type the name of the schedule, click **Pick** to select a schedule from a list, or click **New** to create a new schedule. To enable the schedule area, **On schedule** must be selected.  
  
 **Server restriction**  
 Select the types of servers that are appropriate for this policy. Options are **None** or select a condition that filters the possible servers.  
  
## See Also  
 [Administer Servers by Using Policy-Based Management](../../relational-databases/policy-based-management/administer-servers-by-using-policy-based-management.md)  
  
  
