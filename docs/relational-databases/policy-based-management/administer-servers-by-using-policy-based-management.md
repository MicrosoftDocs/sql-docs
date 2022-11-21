---
title: "Administer servers using Policy-Based Management"
description: Learn how to use Policy-Based Management to manage one or more instances of SQL Server. 
ms.custom: seo-lt-2019
ms.date: "08/12/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords: 
  - "facet See facets"
  - "Declarative Management Framework See Policy-Based Management"
  - "surface area configuration [SQL Server], Policy-Based Management"
  - "Policy-Based Management"
  - "facets [Policy-Based Management]"
  - "Policy-Based Management, administering"
  - "conditions [Policy-Based Management]"
  - "facets [Policy-Based Management], about facets"
  - "PolicyAdministratorRole role"
ms.assetid: ef2a7b3b-614b-405d-a04a-2464a019df40
author: VanMSFT
ms.author: vanto
---
# Administer Servers by Using Policy-Based Management
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
   Policy-Based Management is a policy based system for managing one or more instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use it to create conditions that contain condition expressions. Then, create policies that apply the conditions to database target objects.  

For example, as the database administrator, you may want to ensure that certain servers do not have Database Mail enabled, so you  create a condition and a policy that sets that server option. 
   
 > [!IMPORTANT]  
 > Policies can affect how some features work. For example, change data capture and transactional replication both use the systranschemas table, which does not have an index. If you enable a policy that all tables must have an index, enforcing compliance of the policy will cause these features to fail.  
  
 Use SQL Server management Studio to create and manage policies, to:
  
1.  Select a Policy-Based Management facet that contains the properties to be configured.  
  
2.  Define a condition that specifies the state of a management facet.  
  
3.  Define a policy that contains the condition, additional conditions that filter the target sets, and the evaluation mode.  
  
4.  Check whether an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in compliance with the policy.  
  
 For failed policies, Object Explorer indicates a critical health warning as a red icon next to the target and the nodes that are higher in the Object Explorer tree.  
  
> [!NOTE]  
> When the system computes the object set for a policy, by default the system objects are excluded.  For example, if the object set of the policy refers to all tables, the policy will not apply to system tables. If users want to evaluate a policy against system objects, they can explicitly add system objects to the object set. However, though all policies are supported for **check on schedule** evaluation mode, for performance reason, not all policies with arbitrary object sets are supported for **check on change** evaluation mode. For more information, see [Policy Evaluation Modes](/archive/blogs/sqlpbm/policy-evaluation-modes)  
  
## Three Policy-Based Management components  
 Policy-Based Management has three components:  
  
-   Policy management. Policy administrators create policies.  
  
-   Explicit administration. Administrators select one or more managed targets and explicitly check that the targets comply with a specific policy, or explicitly make the targets comply with a policy.  
  
-   Evaluation modes. There are four evaluation modes; three can be automated:  
  
    -   **On demand**. This mode evaluates the policy when directly specified by the user.  
  
    -   **On change: prevent**. This automated mode uses DDL triggers to prevent policy violations.  
  
        > [!IMPORTANT]  
        > If the nested triggers server configuration option is disabled, **On change: prevent** will not work correctly. Policy-Based Management relies on DDL triggers to detect and roll back DDL operations that do not comply with policies that use this evaluation mode. Removing the Policy-Based Management DDL triggers or disabling nested triggers, will cause this evaluation mode to fail or perform unexpectedly.  
  
    -   **On change: log only**. This automated mode uses event notification to evaluate a policy when a relevant change is made.  
  
    -   **On schedule**. This automated mode uses a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job to periodically evaluate a policy.  
  
     When automated policies are not enabled, Policy-Based Management will not affect system performance.  
  
## Terms  
 **Policy-Based Management managed target** 
 Entities that are managed by Policy-Based Management, such as an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], a database, a table, or an index. All targets in a server instance form a target hierarchy. A target set is the set of targets that results from applying a set of target filters to the target hierarchy, for example, all the tables in the database owned by the HumanResources schema.  
  
 **Policy-Based Management facet**
 A set of logical properties that model the behavior or characteristics for certain types of managed targets. The number and characteristics of the properties are built into the facet and can be added or removed by only the maker of the facet. A target type can implement one or more management facets, and a management facet can be implemented by one or more target types. Some properties of a facet can only apply to a specific version..  
  
 **Policy-Based Management condition**  
 A Boolean expression that specifies a set of allowed states of a Policy-Based Management managed target with regard to a management facet. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to observe collations when evaluating a condition. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations do not exactly match Windows collations, test your condition to determine how the algorithm resolves conflicts.  
  
 **Policy-Based Management policy**  
 A Policy-Based Management condition and the expected behavior, for example, evaluation mode, target filters, and schedule. A policy can contain only one condition. Policies can be enabled or disabled. Policies are stored in the msdb database.  
  
 **Policy-Based Management policy category**  
 A user-defined category to help manage policies. Users can classify policies into different policy categories. A policy belongs to one and only one policy category. Policy categories apply to databases and servers. At the database level, the following conditions apply:  
  
-   Database owners can subscribe a database to a set of policy categories.  
  
-   Only policies from its subscribed categories can govern a database.  
  
-   All databases implicitly subscribe to the default policy category.  
  
 At the server level, policy categories can be applied to all databases.  
  
 **Effective policy**  
 The effective policies of a target are those policies that govern this target. A policy is effective with regard to a target only if all the following conditions are satisfied:  
  
-   The policy is enabled.  
  
-   The target belongs to the target set of the policy.  
  
-   The target or one of the targets ancestors subscribes to the policy group that contains this policy.  
  
## Links to specific tasks 

 - [Store Policy-Based Management policies.](policy-based-management-storage.md)
 - [Configure Alerts to Notify Policy Administrators of Policy Failures](../../relational-databases/policy-based-management/configure-alerts-to-notify-policy-administrators-of-policy-failures.md)
 - [Create a New Policy-Based Management Condition](../../relational-databases/policy-based-management/create-a-new-policy-based-management-condition.md)
 - [Delete a Policy-Based Management Condition](../../relational-databases/policy-based-management/delete-a-policy-based-management-condition.md)
 - [View or Modify the Properties of a Policy-Based Management Condition](../../relational-databases/policy-based-management/view-or-modify-the-properties-of-a-policy-based-management-condition.md)
 - [Export a Policy-Based Management Policy](../../relational-databases/policy-based-management/export-a-policy-based-management-policy.md)
 - [Import a Policy-Based Management Policy](../../relational-databases/policy-based-management/import-a-policy-based-management-policy.md)
 - [Evaluate a Policy-Based Management Policy from an Object](../../relational-databases/policy-based-management/evaluate-a-policy-based-management-policy-from-an-object.md)
 - [Work with Policy-Based Management Facets](../../relational-databases/policy-based-management/working-with-policy-based-management-facets.md)
 - [Monitor and Enforce Best Practices Using Policy-Based Management](../../relational-databases/policy-based-management/monitor-and-enforce-best-practices-by-using-policy-based-management.md)

## See also  
 
 - [Tutorial: Create and apply an off-by-default policy](lesson-1-create-and-apply-an-off-by-default-policy.md)
 - [Tutorial: Create and apply naming standards policy](lesson-2-create-and-apply-a-naming-standards-policy.md)
 - [Policy-Based Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/policy-based-management-views-transact-sql.md)
