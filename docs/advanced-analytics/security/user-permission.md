---
title: Grant database permissions for R and Python script execution - SQL Server Machine Learning Services
description: How to grant database user permissions for R and Python script execution on SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/17/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Give users permission to SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how you can give users permission to run external scripts in SQL Server Machine Learning Services and give read, write, or data definition language (DDL) permissions to databases.

For more information, see the permissions section in [Security overview for the extensibility framework](../../advanced-analytics/concepts/security.md#permissions).

<a name="permissions-external-script"></a>

## Permission to run scripts

If you installed [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] yourself, and you are running R or Python scripts in your own instance, you typically execute scripts as an administrator. Thus, you have implicit permission over various operations and all data in the database.

Most users, however, do not have such elevated permissions. For example, users in an organization who use SQL logins to access the database generally do not have elevated permissions. Therefore, for each user who is using R or Python, you must grant users of Machine Learning Services the permission to run external scripts in each database where the language is used. Here's how:

```sql
USE <database_name>
GO
GRANT EXECUTE ANY EXTERNAL SCRIPT TO [UserName]
```

> [!NOTE]
> Permissions are not specific to the supported script language. In other words, there are not separate permission levels for R script versus Python script. If you need to maintain separate permissions for these languages, install R and Python on separate instances.

<a name="permissions-db"></a> 

## Grant databases permissions

While a user is running scripts, the user might need to read data from other databases. The user might also need to create new tables to store results, and write data into tables.

For each Windows user account or SQL login that is running R or Python scripts, ensure that it has the appropriate permissions on the specific database:  `db_datareader` to read data, `db_datawriter` to save objects to the database, or `db_ddladmin` to create objects such as stored procedures or tables containing trained and serialized data.

For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement gives the SQL login *MySQLLogin* the rights to run T-SQL queries in the *ML_Samples* database. To run this statement, the SQL login must already exist in the security context of the server.

```sql
USE ML_Samples
GO
EXEC sp_addrolemember 'db_datareader', 'MySQLLogin'
```

## Next steps

For more information about the permissions included in each role, see [Database-level roles](../../relational-databases/security/authentication-access/database-level-roles.md).