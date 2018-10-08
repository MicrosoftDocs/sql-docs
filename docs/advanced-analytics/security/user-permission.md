---
title: Give users permission to SQL Server Machine Learning Services | Microsoft Docs
description: How to give users permission to SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/05/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Give users permission to SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how you can give users permission to run external scripts in SQL Server Machine Learning Services and give read, write, or data definition language (DDL) permissions to databases.

A SQL Server login or Windows user account is required to run external scripts that use SQL Server data or that run with SQL Server as the compute context.

The login or user account identifies the *security principal*, who might need multiple levels of access, depending on the external script requirements:

+ Permission to access the database where external scripts are enabled.
+ Permissions to read data from secured objects such as tables.
+ The ability to write new data to a table, such as a model, or scoring results.
+ The ability to create new objects, such as tables, stored procedures that use the external script, or custom functions that use R or Python job.
+ The right to install new packages on the SQL Server computer, or use packages provided to a group of users.

Therefore, each person who runs an external script using SQL Server as the execution context must be mapped to a user in the database. Under SQL Server security, it is easiest to create roles to manage sets of permissions, and assign users to those roles, rather than individually set user permissions.

Even users who are using R or Python in an external tool must be mapped to a login or account in the database if the user needs to run an external script in-database, or access database objects and data. The same permissions are required whether the external script is sent from a remote data science client or started using a T-SQL stored procedure.

For example, assume that you created an external script that runs on your local computer, and you want to run that code on SQL Server. You must ensure that the following conditions are met:

+ The database allows remote connections.
+ The SQL login or Windows account that you used for database access has been added to the SQL Server at the instance level.
+ The SQL login or Windows user must have the permission to execute external scripts. Generally, this permission can only be added by a database administrator.
+ The SQL login or Window user must be added as a user, with appropriate permissions, in each database where the external script performs any of these operations:
  + Retrieving data.
  + Writing or updating data.
  + Creating new objects, such as tables or stored procedures.

After the login or Windows user account has been provisioned and given the necessary permissions, you can run an external script on SQL Server by using a data source object in R or the **revoscalepy** library in Python, or by calling a stored procedure that contains the external script.

Whenever an external script is launched from SQL Server, the database engine security gets the security context of the user who started the job, and manages the mappings of the user or login to securable objects.

Therefore, all external scripts that are initiated from a remote client must specify the login or user information as part of the connection string.

<a name="permissions-external-script"></a> 

## Grant permission to run external scripts

If you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] yourself, and you are running R or Python scripts in your own instance, you typically execute scripts as an administrator. Thus, you have implicit permission over various operations and all data in the database.

Most users, however, do not have such elevated permissions. For example, users in an organization who use SQL logins to access the database generally do not have elevated permissions. Therefore, for each user who is using R or Python, you must grant users of Machine Learning Services the permission to run external scripts in each database where the language is used. Here's how:

```SQL
USE <database_name>
GO
GRANT EXECUTE ANY EXTERNAL SCRIPT  TO [UserName]
```

> [!NOTE]
> Permissions are not specific to the supported script language. In other words, there are not separate permission levels for R script versus Python script. If you need to maintain separate permissions for these languages, install R and Python on separate instances.

<a name="permissions-db"></a> 

## Grant databases permissions

While a user is running scripts, the user might need to read data from other databases. The user might also need to create new tables to store results, and write data into tables.

For each Windows user account or SQL login that is running R or Python scripts, ensure that it has the appropriate permissions on the specific database:  `db_datareader`, `db_datawriter`, or `db_ddladmin`.

For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement gives the SQL login *MySQLLogin* the rights to run T-SQL queries in the *ML_Samples* database. To run this statement, the SQL login must already exist in the security context of the server.

```SQL
USE ML_Samples
GO
EXEC sp_addrolemember 'db_datareader', 'MySQLLogin'
```



## See also

For more information about the permissions included in each role, see [Database-level roles](../../relational-databases/security/authentication-access/database-level-roles.md).