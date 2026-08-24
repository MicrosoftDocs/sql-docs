---
title: "Policy Management Node (Object Explorer)"
description: "Reference for Policy Management node in Object Explorer, providing F1 help for dialog boxes used to create, manage, and evaluate Policy-Based Management policies in SSMS."
author: VanMSFT
ms.author: vanto
ms.date: 02/05/2026
ms.service: sql
ms.subservice: security
ms.topic: reference
ai-usage: ai-assisted
---
# Policy management node (Object Explorer)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The **Policy Management** node in Object Explorer provides access to Policy-Based Management functionality in SQL Server Management Studio. Policy-Based Management is a policy-based system for managing one or more instances of SQL Server. Use it to create conditions that contain condition expressions, then create policies that apply those conditions to database target objects.

This reference provides F1 help for the dialog boxes available under the **Policy Management** node.

## Condition management

These dialog boxes help you create and manage conditions. A condition is a Boolean expression that specifies a set of allowed states of a Policy-Based Management managed target with regard to facets.

| Dialog box | Description |
| --- | --- |
| [Create New Condition or Open Condition dialog box, General page](create-new-condition-or-open-condition-dialog-box-general-page.md) | Creates or modifies a Policy-Based Management condition. Use to define the Boolean expression that specifies allowed states for a management facet. |
| [Create New Condition or Open Condition dialog box, Description page](create-new-condition-or-open-condition-dialog-box-description-page.md) | Adds or modifies a description for a condition, helping document the purpose and usage of the condition. |
| [Open Condition dialog box, Dependent Policies page](open-condition-dialog-box-dependent-policies-page.md) | Displays the policies that currently reference the selected condition. Useful for understanding policy dependencies before modifying a condition. |
| [Advanced Edit (Condition) dialog box](advanced-edit-condition-dialog-box.md) | Creates or edits conditions using advanced expressions. Use when the simple expression builder doesn't provide the complexity needed. |

## Policy management

These dialog boxes help you create, view, and manage policies. A policy combines a condition with the expected behavior, including evaluation mode, target filters, and schedule.

| Dialog box | Description |
| --- | --- |
| [Create New Policy or Open Policy dialog box, General page](create-new-policy-or-open-policy-dialog-box-general-page.md) | Creates or modifies a policy. Set the condition, target types, evaluation mode, and schedule for automated policy checking. |
| [Create New Policy or Open Policy dialog box, Description page](create-new-policy-or-open-policy-dialog-box-description-page.md) | Adds or modifies a description for a policy. Use to document the policy purpose, compliance requirements, and implementation details. |
| [View Policies dialog box](view-policies-dialog-box.md) | Displays policies that apply to a selected target. Use to see which policies govern a specific database or server object. |

## Policy evaluation

These dialog boxes help you evaluate policies and review compliance results. Use them to check whether targets comply with policies and to optionally enforce compliance.

| Dialog box | Description |
| --- | --- |
| [Evaluate Policies dialog box, Policy Selection page](evaluate-policies-dialog-box-policy-selection-page.md) | Selects policies to evaluate against targets. Specify the policy source (server or file) and choose which policies to run. |
| [Evaluate Policies dialog box, Evaluation Results page](evaluate-policies-dialog-box-evaluation-results-page.md) | Displays the results of policy evaluation. Shows which targets comply or fail, and optionally apply policy changes to non-compliant targets. |
| [Results Detailed View dialog box](results-detailed-view-dialog-box.md) | Shows detailed results for policy evaluation, including the specific properties checked and their compliance status. |

## Facet management

These dialog boxes help you work with facets. A facet is a set of logical properties that model the behavior or characteristics for certain types of managed targets.

| Dialog box | Description |
| --- | --- |
| [Facet Properties dialog box, General page](facet-properties-dialog-box-general-page.md) | Displays the properties and their current values for a management facet on a specific target. Use to inspect facet state. |
| [Facet Properties dialog box, Dependent Policies page](facet-properties-dialog-box-dependent-policies-page.md) | Lists the policies that reference the selected facet. Useful for understanding which policies might be affected by target changes. |
| [Facet Properties dialog box, Dependent Conditions page](facet-properties-dialog-box-dependent-conditions-page.md) | Lists the conditions that reference the selected facet. Use to see what conditions are based on a particular facet. |
| [View Facets dialog box](view-facets-dialog-box.md) | Displays all available facets for a selected target type. Use to explore which facets can be used for policy conditions. |

## Import and export

These dialog boxes help you import policies from files and export policies for use on other servers or for backup purposes.

| Dialog box | Description |
| --- | --- |
| [Import Policies dialog box](import-policies-dialog-box.md) | Imports Policy-Based Management policies from XML files. Use to deploy policies from files or apply policies created on other servers. |
| [Export As Policy dialog box](export-as-policy-dialog-box.md) | Exports the current state of a facet as a Policy-Based Management policy. Use to create policies based on a known-good configuration. |
| [Select Source dialog box](select-source-dialog-box.md) | Specifies the source location for policies to evaluate or import. Can be a file path or a connection to another SQL Server instance. |

## Related content

- [Administer Servers by Using Policy-Based Management](administer-servers-by-using-policy-based-management.md)
- [Working with Policy-Based Management Facets](working-with-policy-based-management-facets.md)
