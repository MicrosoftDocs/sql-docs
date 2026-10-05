---
title: Retirement of Microsoft Purview access policies for SQL
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & SQL Server enabled by Azure Arc
description: Microsoft Purview DevOps, data owner, and self-service policies for SQL retire on October 30, 2027. Learn what changes, who is affected, and how to migrate to SQL native roles and permissions.
author: VanMSFT
ms.author: vanto
ms.reviewer: shdasgupta, randolphwest
ms.date: 10/02/2026
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ai-usage: ai-assisted
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16 || =azuresqldb-current || =azuresqldb-mi-current"
---
# Retirement of Microsoft Purview access policies for SQL

[!INCLUDE [appliesto-sqldb-sqlmi-arc](../../includes/applies-to-version/appliesto-sqldb-sqlmi-arc.md)]

Microsoft Purview access policies for SQL, which include DevOps policies, data owner policies, and self-service policies, retire on October 30, 2027. This article explains what's changing, how to tell whether you're affected, and how to move the access those policies grant to SQL native roles and permissions.

The retirement applies to Azure SQL Database, Azure SQL Managed Instance, and Azure Arc-enabled SQL Server 2022.

## What's changing

Microsoft Purview access policies for SQL retire on October 30, 2027.

Instead of Microsoft Purview access policies, use SQL's native authorization model, which assigns SQL roles and permissions to Microsoft Entra users and groups.

Until October 30, 2027, your existing DevOps, data owner, and self-service policies keep working as they do today. After that date, SQL no longer enforces them and removes any access granted through them.

## Who is affected

If your organization uses Microsoft Purview DevOps, data owner, or self-service policies to grant access to SQL resources, your users lose access after October 30, 2027 unless you configure equivalent permissions by using SQL native authorization.

## How to identify affected resources

Use either method. The Purview portal shows you the policies themselves. Azure Resource Graph shows you which SQL resources have external governance turned on, which is useful when you manage many subscriptions.

### Review policies in the Microsoft Purview portal

To list policies, you need the Policy Author, Data Source Admin, Data Curator, or Data Reader role at the root collection level.

1. Sign in to the [Microsoft Purview governance portal](https://web.purview.azure.com/resource/).
1. On the left pane, select **Data policy**, and then select **DevOps policies** to review your DevOps policies.
1. Select **Data policies** to review your data owner policies.
1. Select **Self-service access policies** to review the self-service policies that Purview created from approved access requests.
1. Identify the policies scoped to Azure SQL Database, Azure SQL Managed Instance, or Azure Arc-enabled SQL Server 2022.

These Microsoft Purview articles describe the policy configuration on each platform: [Azure SQL Database](/purview/legacy/how-to-policies-devops-azure-sql-db), [Azure SQL Managed Instance](/purview/legacy/how-to-policies-devops-azure-sql-mi), and [Azure Arc-enabled SQL Server 2022](/purview/legacy/how-to-policies-devops-arc-sql-server). For background on what the policies do, see [Microsoft Purview DevOps policies concepts](/purview/legacy/concept-policies-devops).

### Use Azure Resource Graph

The following queries identify SQL resources that currently have Purview access policies enabled.

For Azure SQL Database:

```kusto
resources
| where type == "microsoft.sql/servers"
| where properties.externalGovernanceStatus == 'Enabled'
| project SubscriptionId=subscriptionId, ResourceId = id
```

For Azure SQL Managed Instance:

```kusto
resources
| where type == "microsoft.sql/managedinstances"
| where properties.externalGovernanceStatus == 'Enabled'
| project SubscriptionId=subscriptionId, ResourceId = id
```

For Azure Arc-enabled SQL Server 2022:

```kusto
resources
| where type == 'microsoft.hybridcompute/machines/extensions'
| where name contains "SqlServer"
| where properties.instanceView.status contains "PurviewPlugin"
| where properties.instanceView.status contains "Purview Plugin:"
| where properties.instanceView.status !contains "Purview Plugin: []"
| where properties.instanceView.status contains "Azure Purview"
| project SubscriptionId=subscriptionId, ResourceId = id
```

## Migrate to SQL native roles and permissions

Replace each Purview policy action with a fixed server-level role, assigned to the same Microsoft Entra users and groups. The following roles are available on Azure SQL Database, Azure SQL Managed Instance, and Azure Arc-enabled SQL Server 2022.

| Purview policy action | Equivalent SQL server role |
| --- | --- |
| SQL performance monitoring | `##MS_ServerPerformanceStateReader##`, `##MS_PerformanceDefinitionReader##` |
| SQL security auditing | `##MS_ServerSecurityStateReader##`, `##MS_SecurityDefinitionReader##` |
| Connect to a database without a database user | `##MS_DatabaseConnector##` |

Assign only the roles that the job function requires. The four reader roles are each narrower than `##MS_ServerStateReader##` and `##MS_DefinitionReader##`, which helps you comply with the principle of least privilege.

For the full list of roles and the permissions each one grants, see [Server-level roles](authentication-access/server-level-roles.md) and [Azure SQL Database server roles for permission management](/azure/azure-sql/database/security-server-roles).

> [!NOTE]
> In Azure SQL Database, run `CREATE LOGIN` and `ALTER SERVER ROLE` while connected to the virtual `master` database.

### Replace SQL performance monitoring access

Create a login for a Microsoft Entra security group, then add it to the performance-focused fixed server roles that your scenario needs.

```sql
-- Create a login for a Microsoft Entra security group
CREATE LOGIN [SQL-Performance-Monitors]
FROM EXTERNAL PROVIDER;

-- Grant access to server performance state information
ALTER SERVER ROLE [##MS_ServerPerformanceStateReader##]
ADD MEMBER [SQL-Performance-Monitors];

-- Grant access to performance-related definitions
ALTER SERVER ROLE [##MS_PerformanceDefinitionReader##]
ADD MEMBER [SQL-Performance-Monitors];
```

### Replace SQL security auditing access

Create a login for a Microsoft Entra security group, then assign the security state and security definition reader roles that match the auditing responsibilities you're preserving.

```sql
-- Create a login for a Microsoft Entra security group
CREATE LOGIN [SQL-Security-Auditors]
FROM EXTERNAL PROVIDER;

-- Grant access to server security state information
ALTER SERVER ROLE [##MS_ServerSecurityStateReader##]
ADD MEMBER [SQL-Security-Auditors];

-- Grant access to security-related definitions
ALTER SERVER ROLE [##MS_SecurityDefinitionReader##]
ADD MEMBER [SQL-Security-Auditors];
```

### Allow connections without a database user

If a Purview policy lets a principal connect to a database without a user provisioned in that database, use the `##MS_DatabaseConnector##` fixed server role.

```sql
-- Create a login for a Microsoft Entra security group
CREATE LOGIN [SQL-Operations-Team]
FROM EXTERNAL PROVIDER;

-- Allow the login to connect to databases without a mapped database user
ALTER SERVER ROLE [##MS_DatabaseConnector##]
ADD MEMBER [SQL-Operations-Team];
```

### Recommended migration approach

- Create Microsoft Entra security groups that represent job functions, such as SQL performance monitors, SQL security auditors, SQL operations, and SQL data owners.
- Create the corresponding SQL login or database user from each Microsoft Entra group.
- Assign only the fixed server roles, database roles, or explicit permissions that the job function requires.
- Test the scripts in a nonproduction environment first, and compare the effective permissions against the access the Purview policy granted.
- Validate access with representative users before you remove the dependency on the Purview policy.
- Keep a rollback plan, and remove the legacy assignments only after validation finishes.

## Required action

To avoid losing access:

1. Identify the SQL resources governed by Microsoft Purview DevOps, data owner, or self-service policies.
1. Review the users and groups that receive access through those policies.
1. Recreate the access by using [SQL native roles and permissions](#migrate-to-sql-native-roles-and-permissions) assigned to Microsoft Entra users and groups.
1. Validate the new access before October 30, 2027.
1. Remove your remaining dependencies on Purview access policies for SQL resources.

## Frequently asked questions

### What's being retired?

Microsoft Purview access policies for SQL, which include DevOps policies, data owner policies, and self-service policies, for Azure SQL Database, Azure SQL Managed Instance, and Azure Arc-enabled SQL Server 2022.

### Are my existing policies affected today?

No. Your existing policies keep working until October 30, 2027.

### What happens after October 30, 2027?

SQL stops enforcing Purview access policies. Access that those policies granted is no longer available.

### What should I use instead?

SQL's built-in authorization, which assigns fixed server-level roles and permissions to Microsoft Entra users and groups. See [Migrate to SQL native roles and permissions](#migrate-to-sql-native-roles-and-permissions).

### How do I know whether I'm affected?

Review your DevOps, data owner, and self-service policies in the Microsoft Purview portal, and run the Azure Resource Graph queries in [How to identify affected resources](#how-to-identify-affected-resources).

## Related content

- [Server-level roles](authentication-access/server-level-roles.md)
- [Azure SQL Database server roles for permission management](/azure/azure-sql/database/security-server-roles)
- [Microsoft Entra authentication for Azure SQL](/azure/azure-sql/database/authentication-aad-overview)
- [Microsoft Purview DevOps policies concepts](/purview/legacy/concept-policies-devops)
- [Provision access to system metadata in Azure SQL Database using Microsoft Purview DevOps policies](/purview/legacy/how-to-policies-devops-azure-sql-db)
- [Provision read access to Azure SQL Database using Microsoft Purview data owner policies](/purview/legacy/how-to-policies-data-owner-azure-sql-db)
- [Self-service policies for Azure SQL Database (preview)](/purview/legacy/how-to-policies-self-service-azure-sql-db)
- [Discontinued SQL Server database engine functionality](../../database-engine/discontinued-database-engine-functionality-in-sql-server.md)
