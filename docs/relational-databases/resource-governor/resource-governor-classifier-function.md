---
title: Resource Governor Classifier Function
description: The resource governor classification process uses the classifier function to assign incoming sessions to a workload group based on session attributes.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
helpviewer_keywords:
  - "Resource Governor, classifier function"
  - "user-defined functions [SQL Server], classifier function"
  - "classifier function [SQL Server]"
  - "classifier function [SQL Server], overview"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Resource governor classifier function

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

The resource governor classification process can use a classifier function to assign incoming sessions to a workload group. The classifier function contains your custom logic for classifying sessions into workload groups.

For configuration and monitoring examples and to learn resource governor best practices, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

## Classification

With resource governor, each new session is classified into a workload group. The classifier is a scalar user-defined function that you create. It contains your desired logic to assign incoming sessions to a workload group. The scalar value returned by the classifier is the name of a workload group to assign to an incoming session.

If resource governor is enabled and a classifier function is specified in the resource governor configuration, then the function output determines the workload group used for new sessions. Otherwise, all user sessions are classified into the `default` workload group.

> [!NOTE]
> The `internal` workload group is used for internal system requests only. You can't change the criteria used for assigning requests into the `internal` workload group, and you can't explicitly classify requests into the `internal` workload group.

You must complete the following steps to start using a classifier function:

1. Create the function in the `master` database using [CREATE FUNCTION](../../t-sql/statements/create-function-transact-sql.md). The function must use schema binding.
1. Reference the function in the resource governor configuration using [ALTER RESOURCE GOVERNOR](../../t-sql/statements/alter-resource-governor-transact-sql.md) with the `CLASSIFIER_FUNCTION` parameter.
1. Make the new configuration effective using `ALTER RESOURCE GOVERNOR RECONFIGURE`.

> [!IMPORTANT]
> Client connection attempts might time out if the classifier function doesn't complete within the connection timeout period configured by the client. It is important that you create classifier functions that finish execution before connection timeout might occur.
>
> Keep the classifier function simple. Avoid using complex or time-consuming logic. If possible, avoid data access in the classifier.

The classifier function has the following characteristics and behaviors:

- The function is defined in the server scope (in the `master` database).
- The function is defined with schema binding. For more information, see [SCHEMABINDING](../../t-sql/statements/create-function-transact-sql.md#schemabinding-1).
- The function is evaluated for every new session, even when connection pooling is enabled.
- The function returns the workload group context for the session. The session is assigned to the workload group returned by the classifier for the lifetime of the session.
- If the function returns `NULL`, `default`, or the name of a nonexistent workload group, the session is given the `default` workload group context. The session is also given the `default` context if the function fails for any reason.
- After a classifier function is added or removed using `ALTER RESOURCE GOVERNOR (WITH CLASSIFIER_FUNCTION = ...)` statement, the change takes effect only after `ALTER RESOURCE GOVERNOR RECONFIGURE` statement is executed.
- Only one function can be designated as a classifier at a time.
- The classifier function can't be modified or deleted unless its classifier status is removed using `ALTER RESOURCE GOVERNOR (WITH CLASSIFIER_FUNCTION = ...)` statement that sets the function name to `NULL` or to the name of another function.
- In the absence of a classifier function, all sessions are classified into the `default` group.
- The workload groups specified in the classifier function output are outside the scope of the schema binding restriction. For example, you can't drop a table referenced in the classifier function, but you can drop a workload group even if the classifier returns the name of that group.

### Enable DAC

For troubleshooting and diagnostic purposes, we recommend proactively enabling and getting familiar with the Dedicated Administrator Connection (DAC). The DAC isn't subject to resource governor classification. You can use a DAC to monitor and troubleshoot a classifier function even if your resource governor configuration malfunctions and makes other connections not usable. For more information, see [Diagnostic connection for database administrators](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md).

If a DAC isn't available for troubleshooting, you can [start the server in single user mode](../../database-engine/configure-windows/start-sql-server-in-single-user-mode.md). Although the single user mode connection isn't subject to classification, it doesn't give you the ability to diagnose resource governor classification while it is running.

Once you connect using a DAC or connect in single user mode, you can [modify resource governor configuration](../../t-sql/statements/alter-resource-governor-transact-sql.md) to remove a malfunctioning classifier function or [disable resource governor](disable-resource-governor.md).

### Login process

In the context of resource governor, the login process for a session consists of the following steps:

1. Login authentication.
1. [Logon trigger](../triggers/logon-triggers.md) execution. Occurs only if logon triggers exist in the instance.
1. Classification.

When classification starts, resource governor executes the classifier function and uses the scalar value returned by the function to send requests to the matching workload group.

You can monitor the execution of logon triggers and the classifier function using [sys.dm_exec_sessions](../system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md) and [sys.dm_exec_requests](../system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) system views.

## Examples

The resource governor classifier function can use a wide variety of custom logic. For more examples and a walkthrough, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

#### A. Host name

This function classifies sessions from a specific hostname into a workload group named `Reports`, using the [HOST_NAME()](../../t-sql/functions/host-name-transact-sql.md) built-in system function. All other sessions continue to be classified into the `default` workload group.

```sql
CREATE FUNCTION dbo.rg_classifier()
RETURNS sysname
WITH SCHEMABINDING
AS
BEGIN
    DECLARE @grp_name sysname = 'default';

    IF (HOST_NAME() IN ('reportserver1','reportserver2'))
        SET @grp_name = 'Reports';
    
    RETURN @grp_name;
END;
GO
```

#### B. User name

This function classifies sessions from specific user names or service account names into a workload group named `Reports`, using the [SUSER_SNAME()](../../t-sql/functions/suser-sname-transact-sql.md) built-in system function. All other sessions continue to be classified into the `default` workload group.

```sql
CREATE FUNCTION dbo.rg_classifier()
RETURNS sysname
WITH SCHEMABINDING
AS
BEGIN
    DECLARE @grp_name sysname = 'default';

    IF (SUSER_SNAME() IN ('Reporting', 'domain/svc_reporting'))
        SET @grp_name = 'Reports';
    
    RETURN @grp_name;
END;
GO
```

#### C. Application name

This function classifies sessions from specific application names into a workload group named `Adhoc`, using the [APP_NAME()](../../t-sql/functions/app-name-transact-sql.md) built-in system function. All other sessions continue to be classified into the `default` workload group.

> [!IMPORTANT]
> An application or user can provide any application name as part of the connection string. Users can connect via a wide variety of applications.

```sql
CREATE FUNCTION dbo.rg_classifier()
RETURNS sysname
WITH SCHEMABINDING
AS
BEGIN
    DECLARE @grp_name sysname = 'default';

    IF (APP_NAME() IN ('Microsoft SQL Server Management Studio - Query','azdata'))
        SET @grp_name = 'Adhoc';
    
    RETURN @grp_name;
END;
GO
```

## Related content

- [Resource governor](resource-governor.md)
- [Enable resource governor](enable-resource-governor.md)
- [Resource governor resource pool](resource-governor-resource-pool.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Configure resource governor using a template](configure-resource-governor-using-a-template.md)
- [View and modify resource governor properties](view-resource-governor-properties.md)
