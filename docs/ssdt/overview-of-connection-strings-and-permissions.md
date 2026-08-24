---
title: Connection Strings and Permissions
description: Learn about the connection strings, accounts, and permissions that you need to run SQL Server unit tests. See how to configure the connection strings.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: concept-article
---

# Overview of connection strings and permissions

To run SQL Server unit tests, you must connect to a database server by using one or two specific connection strings. Each connection string represents an account that has the specific permissions that you must have to perform the task or set of tasks in a particular script as part of the test. You can specify these strings in the **SQL Server Test Configuration** dialog box or by manually editing the `app.config` file for your test project.

## Connection strings

In the **SQL Server Test Configuration** dialog box, you can specify connection strings for each of the following accounts.

> [!NOTE]  
> The execution context and privileged context only differ if you use SQL Server authentication. If you use Windows authentication, the same credentials are used for both connection strings.

- **Execution Context** (Required): a user account for running the test script. This connection string should have the same credentials that you expect your users to have. This is important because it ensures that the appropriate permissions have been applied to your database. For more information, see [How to: Configure SQL Server unit test execution](how-to-configure-sql-server-unit-test-execution.md).

  In the `app.config` file for your test project, this is the `ExecutionContext` element.

- **Privileged Context** (Optional): an account on the same database that has higher permissions for running the pre-test action, post-test action, TestInitialize, and TestCleanup scripts. These scripts set the database state and for the post-test action, can be used to validate objects in the database. This connection string is also used to deploy database changes and generate data.

  In the `app.config` file for your test project, this element is the `PrivilegedContext` element. If your SQL Server unit tests run the test script only, you don't have to specify a privileged context.

The strings that you specify in the project configuration dialog box are stored in your test project's `app.config` file. You could also edit that file directly and rebuild the project, after which the new values appear in the dialog box.

## Windows authentication versus SQL Server authentication

When you specify connection strings, you must choose between the use of Windows authentication and SQL authentication. One reason to choose Windows authentication is that it supports the use of tests by a team better than SQL Server authentication does. If you choose SQL Server authentication, the connection strings are encrypted, using Data Protection API (DPAPI), based on your user credentials. This means that tests in this test project run only for you, not for team members who obtain the tests through the source-control system after you check them in. To run tests in this test project, others on your team would have to reconfigure the test project by using their own credentials. To do this, they would edit their copy of the `app.config` file or use the project configuration dialog box.

## Permissions

The test script runs at the execution context permission level, which is the same permission level that would be in effect for user commands that are run on the database when it's in typical use. The pre-test action, post-test, TestInitialize, and TestCleanup scripts run at the privileged context permission level.

Because of the higher-permission connection used for the post-test action script, you can perform validation in it. In this script, you can also run script commands that test permissions. For more information about permissions, see the SQL Server unit testing section of [Required permissions for SQL Server Data Tools](required-permissions-for-sql-server-data-tools.md).

## Related content

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [Scripts in SQL Server unit tests](scripts-in-sql-server-unit-tests.md)
- [SQL Server unit test files](sql-server-unit-test-files.md)
