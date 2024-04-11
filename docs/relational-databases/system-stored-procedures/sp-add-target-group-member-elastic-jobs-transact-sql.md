---
title: "jobs.sp_add_target_group_member (Azure Elastic Jobs) (Transact-SQL)"
description: "jobs.sp_add_target_group_member adds a new member to a target group for the Azure Elastic Jobs service for Azure SQL Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 10/30/2023
ms.service: sql-database
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_target_group_member_TSQL"
  - "sp_add_target_group_member"
  - "jobs.sp_add_target_group_member_TSQL"
  - "jobs.sp_add_target_group_member"
helpviewer_keywords:
  - "sp_add_target_group_member"
  - "jobs.sp_add_target_group_member"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current"
---
# jobs.sp_add_target_group_member (Azure Elastic Jobs) (Transact-SQL)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Adds a database or group of databases to a target group in the [Azure Elastic Jobs service for Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
[jobs].sp_add_target_group_member [ @target_group_name = ] 'target_group_name'
         [ @membership_type = ] 'membership_type' ]
        [ , [ @target_type = ] 'target_type' ]
        [ , [ @refresh_credential_name = ] 'refresh_credential_name' ]
        [ , [ @server_name = ] 'server_name' ]
        [ , [ @database_name = ] 'database_name' ]
        [ , [ @elastic_pool_name = ] 'elastic_pool_name' ]
        [ , [ @target_id = ] 'target_id' OUTPUT ]
```

## Arguments

#### @target_group_name

The name of the target group to which the member will be added. *target_group_name* is nvarchar(128), with no default.

#### @membership_type

Specifies if the target group member will be included or excluded. *membership_type* is nvarchar(128), with default of 'Include'. Valid values for *membership_type* are 'Include' or 'Exclude'.

#### @target_type

The type of target database or collection of databases including all databases in an Azure SQL Database logical server, all databases in an elastic pool, or an individual database. *target_type* is nvarchar(128), with no default.

Valid values for *target_type* are `SqlServer`, `SqlElasticPool`, `SqlDatabase`.

#### @refresh_credential_name

The name of the database-scoped credential. *refresh_credential_name* is nvarchar(128), with no default.

When using Microsoft Entra authentication (formerly Azure Active Directory), omit the *@refresh_credential_name* parameter. Only for use with credential-based authentication.

#### @server_name

The name of the Azure SQL Database logical server that should be added to the specified target group. *server_name* should be specified when target_type is `SqlServer`. *server_name* is nvarchar(128), with no default.

Include the `.database.windows.net` as part of *@server_name*.

#### @database_name

The name of the database that should be added to the specified target group. *database_name* should be specified when target_type is `SqlDatabase`. *database_name* is nvarchar(128), with no default.

#### @elastic_pool_name

The name of the Azure SQL Database elastic pool that should be added to the specified target group. *elastic_pool_name* should be specified when target_type is `SqlElasticPool`. *elastic_pool_name* is nvarchar(128), with no default.

#### @target_id  OUTPUT

The target identification number assigned to the target group member if created added to the target group. *target_id* is an output variable of type uniqueidentifier, with a default of `NULL`.

## Return Code Values

0 (success) or 1 (failure)

## Remarks

A job executes on all single databases within a server or in an elastic pool at time of execution, when a server or elastic pool is included in the target group.

Choose one method for all targets for an elastic job. For example, for a single elastic job, you cannot configure one target server to use database-scoped credentials and another to use Microsoft Entra ID authentication. For more information, see [Authentication](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true#authentication).

## Permissions

By default, members of the sysadmin fixed server role can execute this stored procedure. Only members of sysadmin can use this stored procedure to edit the attributes of jobs that are owned by other users.

## Examples

### Add one database to target group

The following example shows how to add one database in a server to a target group named `ElasticJobGroup`, using Microsoft Entra (formerly Azure Active Directory) authentication.

Connect to the `job_database` and run the following command to add the `master` database to the target group named `ElasticJobGroup`:

```sql
-- Connect to the job database specified when creating the job agent

-- Create a target group containing elastic job database
EXEC jobs.sp_add_target_group 'ElasticJobGroup';

-- Add a server target member
EXEC jobs.sp_add_target_group_member
@target_group_name = 'ElasticJobGroup',
@target_type = 'SqlDatabase',
@server_name = 'server1.database.windows.net',
@database_name = 'master';

--View the recently created target group and target group members
SELECT * FROM jobs.target_groups 
WHERE target_group_name='ServerGroup1';
GO
SELECT * FROM jobs.target_group_members 
WHERE target_group_name='ServerGroup1';
GO
```

### Add multiple databases to a target group

The following example adds all the databases in the `London` and `NewYork` servers to the group `Servers Maintaining Customer Information`. You must connect to the jobs database specified when creating the job agent, in this case `ElasticJobs`.

When using Microsoft Entra authentication (formerly Azure Active Directory), omit the *@refresh_credential_name* parameter, which should only be provided when using database-scoped credentials. In the following examples, the `@refresh_credential_name` parameter is commented out.

```sql
--Connect to the jobs database specified when creating the job agent
USE ElasticJobs;
GO

-- Create a target group containing server(s)
EXEC jobs.sp_add_target_group @target_group_name =  N'Servers Maintaining Customer Information';
GO

-- Add a server target member
EXEC jobs.sp_add_target_group_member
@target_group_name = N'Servers Maintaining Customer Information',
@target_type = N'SqlServer',
--@refresh_credential_name=N'refresh_credential', --database-scoped credential
@server_name=N'London.database.windows.net';
GO

-- Add a server target member
EXEC jobs.sp_add_target_group_member
@target_group_name = N'Servers Maintaining Customer Information',
@target_type = N'SqlServer',
--@refresh_credential_name=N'refresh_credential', --database-scoped credential
@server_name=N'NewYork.database.windows.net';
GO

--View the recently added members to the target group
SELECT * FROM [jobs].target_group_members 
WHERE target_group_name= N'Servers Maintaining Customer Information';
GO
```

### Exclude databases on a logical server

The following example shows how to execute a job against all databases in a logical server `server1.database.windows.net`, except for the database named `MappingDB` or the database named `MappingDB2`.

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- Create a target group containing server(s)
EXEC [jobs].sp_add_target_group N'ServerGroup';
GO

-- Add a server target member
EXEC jobs.sp_add_target_group_member
@target_group_name = N'ServerGroup',
@target_type = N'SqlServer',
@server_name=N'server1.database.windows.net';
GO

--Exclude one database from a server target group
EXEC [jobs].sp_add_target_group_member
@target_group_name = N'ServerGroup',
@membership_type = N'Exclude',
@target_type = N'SqlDatabase',
@server_name = N'server1.database.windows.net',
@database_name = N'MappingDB';
GO

--Exclude another database from a server target group
EXEC [jobs].sp_add_target_group_member
@target_group_name = N'ServerGroup',
@membership_type = N'Exclude',
@target_type = N'SqlDatabase',
@server_name = N'server1.database.windows.net',
@database_name = N'MappingDB2';
GO

--View the recently created target group and target group members
SELECT * FROM [jobs].target_groups 
WHERE target_group_name = N'ServerGroup';
GO
SELECT * FROM [jobs].target_group_members
WHERE target_group_name = N'ServerGroup';
GO
```

### Create a target group (pools)

The following example shows how to target all the databases in one or more elastic pools.

Connect to the `job_database` and run the following command:

```sql
--Connect to the job database specified when creating the job agent

-- Create a target group containing pool(s)
EXEC jobs.sp_add_target_group 'PoolGroup';

-- Add an elastic pool(s) target member
EXEC jobs.sp_add_target_group_member
@target_group_name = 'PoolGroup',
@target_type = 'SqlElasticPool',
@server_name = 'server1.database.windows.net',
@elastic_pool_name = 'ElasticPool1';

-- View the recently created target group and target group members
SELECT * FROM jobs.target_groups 
WHERE target_group_name = N'PoolGroup';
GO
SELECT * FROM jobs.target_group_members 
WHERE target_group_name = N'PoolGroup';
GO
```

## Related content

- [Elastic jobs in Azure SQL Database](/azure/azure-sql/database/elastic-jobs-overview?view=azuresql-db&preserve-view=true)
- [Create, configure, and manage elastic jobs](/azure/azure-sql/database/elastic-jobs-tutorial?view=azuresql-db&preserve-view=true)
- [Create and manage elastic jobs by using T-SQL](/azure/azure-sql/database/elastic-jobs-tsql-create-manage?view=azuresql-db&preserve-view=true)
