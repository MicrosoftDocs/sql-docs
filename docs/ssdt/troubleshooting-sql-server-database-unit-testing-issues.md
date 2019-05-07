---
title: "Troubleshooting SQL Server Database Unit Testing Issues | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: cf4c9cd1-7e73-4c3b-922a-68b9247e7b33
author: "markingmyname"
ms.author: "maghan"
manager: "craigg"
---
# Troubleshooting SQL Server Database Unit Testing Issues
You might encounter the issues in this topic when you work with SQL Server unit tests on a database:  
  
-   [Unit Testing and App.Config Changes Ignored When You Run Unit Tests](#UnitTestingAndAppConfigChanges)  
  
-   [Database Deployment to Unexpected Target When You Run Unit Tests](#DatabaseDeploymentInUnitTests)  
  
-   [Timeouts When You Run Database Unit Tests](#TimeoutsDuringUnitTests)  
  
## <a name="UnitTestingAndAppConfigChanges"></a>Unit Testing and App.Config Changes Ignored When You Run Unit Tests  
If you modify the App.Config file in the test project, you must rebuild the test project before those changes will take effect. These changes include those that you make to App.Config by using the **SQL Server Test Configuration** dialog box. If you do not rebuild the test project, the changes are not applied when you run the unit tests.  
  
## <a name="DatabaseDeploymentInUnitTests"></a>Database Deployment to Unexpected Target When You Run Unit Tests  
If you deploy a database from a database project when you run unit tests, the database is deployed by using the connection string information that is specified in your unit test configuration. The connection information that is specified in the database project Debug properties is not used for this task, which allows you to run SQL Server unit tests against different instances of the same database.  
  
## <a name="TimeoutsDuringUnitTests"></a>Timeouts when You Run Database Unit Tests  
If your database unit tests are failing because of a timeout, you can increase the time-out period by updating the app.config file in your test project. The connect time-out, defined on the connection string, specifies how long to wait when the unit test connects to the server. The command timeout, which must be defined directly in the app.config file specifies how long to wait when the unit test executes the Transact\-SQL script. If you have problems with long running unit tests, try increasing the command timeout value in the appropriate context element. For example, to specify a command timeout of 120 seconds for the **PrivilegedContext** element, update the app.config as follows:  
  
```  
<SqlUnitTesting_VS2010>  
    <DatabaseDeployment DatabaseProjectFileName="..\..\..\..\..\..\Visual Studio 2010\Projects\Database10\Database10\AdventureWorks.sqlproj"  
        Configuration="Debug" />  
    <DataGeneration ClearDatabase="true" />  
    <ExecutionContext Provider="System.Data.SqlClient" ConnectionString="Data Source=(LocalDB)\Projects;Initial Catalog=AdventureWorks_Test;Integrated Security=True;Pooling=False"  
        CommandTimeout="30" />  
    <PrivilegedContext Provider="System.Data.SqlClient" ConnectionString="Data Source=(LocalDB)\Projects;Initial Catalog=AdventureWorks_Test;Integrated Security=True;Pooling=False"  
        CommandTimeout="120" />  
</SqlUnitTesting_VS2010>  
```  
  
## See Also  
[How to: Create SQL Server Unit Tests for Functions, Triggers, and Stored Procedures](../ssdt/how-to-create-unit-tests-for-functions-triggers-stored-procedures.md)  
[How to: Configure SQL Server Unit Test Execution](../ssdt/how-to-configure-sql-server-unit-test-execution.md)  
  
