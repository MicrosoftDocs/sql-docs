---
title: Troubleshooting SQL Server Database Unit Testing Issues
description: View troubleshooting tips for issues that you might see with SQL Server unit tests, such as timeout failures and database deployment to unexpected targets.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: troubleshooting-general
ms.custom:
  - sfi-ropc-nochange
---

# Troubleshoot SQL Server database unit testing issues

You might encounter the following issues when you work with SQL Server unit tests on a database.

<a id="UnitTestingAndAppConfigChanges"></a>

## Unit testing and app.config changes are ignored when you run unit tests

If you modify the `app.config` file in the test project, you must rebuild the test project before the changes take effect. These changes include any changes you make to `app.config` with the **SQL Server Test Configuration** dialog box. If you don't rebuild the test project, the changes aren't applied when you run the unit tests.

<a id="DatabaseDeploymentInUnitTests"></a>

## Database deployment to unexpected target when you run unit tests

If you deploy a database from a database project when you run unit tests, the database is deployed by using the connection string information specified in your unit test configuration. The connection information that is specified in the database project Debug properties isn't used for this task, which allows you to run SQL Server unit tests against different instances of the same database.

<a id="TimeoutsDuringUnitTests"></a>

## Timeouts when you run database unit tests

If your database unit tests are failing because of a timeout, you can increase the timeout period by updating the `app.config` file in your test project. The connect timeout, defined on the connection string, specifies how long to wait when the unit test connects to the server. The command timeout, which must be defined directly in the `app.config` file specifies how long to wait when the unit test executes the Transact-SQL script. If you have problems with long running unit tests, try increasing the command timeout value in the appropriate context element. For example, to specify a command timeout of 120 seconds for the **PrivilegedContext** element, update the `app.config` as follows:

```xml
<SqlUnitTesting_VS2010>
    <DatabaseDeployment
        DatabaseProjectFileName="..\..\..\..\..\..\Visual Studio 2010\Projects\Database10\Database10\AdventureWorks.sqlproj"
        Configuration="Debug" />
    <DataGeneration ClearDatabase="true" />
    <ExecutionContext Provider="System.Data.SqlClient"
        ConnectionString="Data Source=(LocalDB)\Projects;Initial Catalog=AdventureWorks_Test;Integrated Security=True;Pooling=False"
        CommandTimeout="30" />
    <PrivilegedContext Provider="System.Data.SqlClient"
        ConnectionString="Data Source=(LocalDB)\Projects;Initial Catalog=AdventureWorks_Test;Integrated Security=True;Pooling=False"
        CommandTimeout="120" />
</SqlUnitTesting_VS2010>
```

## Related content

- [How to: Create SQL Server unit tests for functions, triggers, and stored procedures](how-to-create-unit-tests-for-functions-triggers-stored-procedures.md)
- [How to: Configure SQL Server unit test execution](how-to-configure-sql-server-unit-test-execution.md)
