---
title: "Administer Servers by Using Policy-Based Management | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: security
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
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Administer Servers by Using Policy-Based Management
  Policy-Based Management is a system for managing one or more instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] policy administrators use Policy-Based Management, they use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to create policies to manage entities on the server, such as the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], databases, or other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects.  
  
## Benefits of Policy-Based Management  
 Policy-Based Management is helpful in resolving the issues presented in the following scenarios:  
  
-   A company policy prohibits enabling Database Mail or SQL Mail. A policy is created to check the server state of those two features. An administrator compares the server state to the policy. If the server state is out of compliance, the administrator chooses the Configure mode and the policy brings the server state into compliance.  
  
-   The [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database has a naming convention that requires all stored procedures to start with the letters AW_. A policy is created to enforce this policy. An administrator tests this policy and receives a list of stored procedures that are out of compliance. If future stored procedures do not comply with this naming convention, the creation statements for the stored procedures fail.  
  
> [!NOTE]  
>  Be aware that policies can affect how some [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features work. For example, change data capture and transactional replication both use the systranschemas table, which does not have an index. If you enable a policy that all tables must have an index, enforcing compliance of the policy will cause these features to fail.  
  
 Policies are created and managed by using [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)]. The process includes the following steps:  
  
1.  Select a Policy-Based Management facet that contains the properties to be configured.  
  
2.  Define a condition that specifies the state of a management facet.  
  
3.  Define a policy that contains the condition, additional conditions that filter the target sets, and the evaluation mode.  
  
4.  Check whether an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in compliance with the policy.  
  
 For failed policies, Object Explorer indicates a critical health warning as a red icon next to the target and the nodes that are higher in the Object Explorer tree.  
  
> [!NOTE]  
>  When the system computes the object set for a policy, by default the system objects are excluded.  For example, if the object set of the policy refers to all tables, the policy will not apply to system tables. If users want to evaluate a policy against system objects, they can explicitly add system objects to the object set. However, though all policies are supported for **check on schedule** evaluation mode, for performance reason, not all policies with arbitrary object sets are supported for **check on change** evaluation mode. For more information, see [https://blogs.msdn.com/b/sqlpbm/archive/2009/04/13/policy-evaluation-modes.aspx](https://blogs.msdn.com/b/sqlpbm/archive/2009/04/13/policy-evaluation-modes.aspx)  
  
## Policy-Based Management Concepts  
 Policy-Based Management has three components:  
  
-   Policy management  
  
     Policy administrators create policies.  
  
-   Explicit administration  
  
     Administrators select one or more managed targets and explicitly check that the targets comply with a specific policy, or explicitly make the targets comply with a policy.  
  
-   Evaluation modes  
  
     There are four evaluation modes, three of which can be automated:  
  
    -   **On demand**. This mode evaluates the policy when directly specified by the user.  
  
    -   **On change: prevent**. This automated mode uses DDL triggers to prevent policy violations.  
  
        > [!IMPORTANT]  
        >  If the nested triggers server configuration option is disabled, **On change: prevent** will not work correctly. Policy-Based Management relies on DDL triggers to detect and roll back DDL operations that do not comply with policies that use this evaluation mode. Removing the Policy-Based Management DDL triggers or disabling nest triggers, will cause this evaluation mode to fail or perform unexpectedly.  
  
    -   **On change: log only**. This automated mode uses event notification to evaluate a policy when a relevant change is made.  
  
    -   **On schedule**. This automated mode uses a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job to periodically evaluate a policy.  
  
     When automated policies are not enabled, Policy-Based Management will not affect system performance.  
  
## Policy-Based Management Terms  
 Policy-Based Management managed target  
 Entities that are managed by Policy-Based Management, such as an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], a database, a table, or an index. All targets in a server instance form a target hierarchy. A target set is the set of targets that results from applying a set of target filters to the target hierarchy, for example, all the tables in the database owned by the HumanResources schema.  
  
 Policy-Based Management facet  
 A set of logical properties that model the behavior or characteristics for certain types of managed targets. The number and characteristics of the properties are built into the facet and can be added or removed by only the maker of the facet. A target type can implement one or more management facets, and a management facet can be implemented by one or more target types. Some properties of a facet can only apply to a specific version..  
  
 Policy-Based Management condition  
 A Boolean expression that specifies a set of allowed states of a Policy-Based Management managed target with regard to a management facet. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to observe collations when evaluating a condition. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations do not exactly match Windows collations, test your condition to determine how the algorithm resolves conflicts.  
  
 Policy-Based Management policy  
 A Policy-Based Management condition and the expected behavior, for example, evaluation mode, target filters, and schedule. A policy can contain only one condition. Policies can be enabled or disabled. Policies are stored in the msdb database.  
  
 Policy-Based Management policy category  
 A user-defined category to help manage policies. Users can classify policies into different policy categories. A policy belongs to one and only one policy category. Policy categories apply to databases and servers. At the database level, the following conditions apply:  
  
-   Database owners can subscribe a database to a set of policy categories.  
  
-   Only policies from its subscribed categories can govern a database.  
  
-   All databases implicitly subscribe to the default policy category.  
  
 At the server level, policy categories can be applied to all databases.  
  
 Effective policy  
 The effective policies of a target are those policies that govern this target. A policy is effective with regard to a target only if all the following conditions are satisfied:  
  
-   The policy is enabled.  
  
-   The target belongs to the target set of the policy.  
  
-   The target or one of the targets ancestors subscribes to the policy group that contains this policy.  
  
## Policy-Based Management Tasks  
 Policy-Based Management is a policy based system for managing one or more instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use Policy-Based Management to create conditions that contain condition expressions. Then, create policies that apply the conditions to database target objects.  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Describes how Policy-Based Management policies are stored.|Policy-Based Management Storage|  
|Describes how to configure alerts to notify policy administrators of policy failures.|[Configure Alerts to Notify Policy Administrators of Policy Failures](configure-alerts-to-notify-policy-administrators-of-policy-failures.md)|  
|Describes how to create, view, modify, and delete a Policy-based Management condition.|[Create a New Policy-Based Management Condition](create-a-new-policy-based-management-condition.md)<br /><br /> [Delete a Policy-Based Management Condition](delete-a-policy-based-management-condition.md)<br /><br /> [View or Modify the Properties of a Policy-Based Management Condition](view-or-modify-the-properties-of-a-policy-based-management-condition.md)|  
|Describes how to create, view, modify, and delete a Policy-Based Management policy.|[Create a Policy-Based Management Policy](create-a-policy-based-management-policy.md)<br /><br /> [Delete a Policy-Based Management Policy](delete-a-policy-based-management-policy.md)<br /><br /> [View or Modify the Properties of a Policy-Based Management Policy](view-or-modify-the-properties-of-a-policy-based-management-policy.md)|  
|Describes how to export and import a Policy-based Management policy.|[Export a Policy-Based Management Policy](export-a-policy-based-management-policy.md)<br /><br /> [Import a Policy-Based Management Policy](import-a-policy-based-management-policy.md)|  
|Describes how to verify that a server instance, database, server object, or database object complies with a policy.|[Evaluate a Policy-Based Management Policy from an Object](evaluate-a-policy-based-management-policy-from-an-object.md)<br /><br /> [Evaluate a Policy-Based Management Policy from That Policy](evaluate-a-policy-based-management-policy-from-that-policy.md)<br /><br /> [Evaluate a Policy-Based Management Policy on a Schedule](evaluate-a-policy-based-management-policy-on-a-schedule.md)|  
|Describes how to view and copy a Policy-based Management facet state to a file.|[Working with Policy-Based Management Facets](working-with-policy-based-management-facets.md)|  
|Provides a set of policy files that you can import as best practice policies, and describes how to evaluate the policies against a target set that includes instances, instance objects, databases, or database objects.|[Monitor and Enforce Best Practices by Using Policy-Based Management](monitor-and-enforce-best-practices-by-using-policy-based-management.md)|  
|Provides the F1 Help topics for the **PolicyManagement** node of Object Explorer in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].|[Policy Management Node &#40;Object Explorer&#41;](../../ssms/object/object-explorer.md)|  
  
## See Also  
 [Policy-Based Management Views &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/policy-based-management-views-transact-sql)  
  
  
