---
title: Grant permissions to execute Python and R scripts
description: Learn how you can give users permission to run external Python and R scripts in SQL Server Machine Learning Services and give read, write, or data definition language (DDL) permissions to databases.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 06/03/2020
ms.topic: how-to
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019, contperfq4
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Grant users permission to execute Python and R scripts with SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Learn how you can give users permission to run external Python and R scripts in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and give read, write, or data definition language (DDL) permissions to databases.

For more information, see the permissions section in [Security overview for the extensibility framework](../../machine-learning/concepts/security.md#permissions).

<a name="permissions-external-script"></a>

## Permission to run scripts

For each user who runs Python or R scripts with SQL Server Machine Learning Services, and who are not an administrator, you must grant them the permission to run external scripts in each database where the language is used.

To grant permission to execute external script, run the following script:

```sql
USE <database_name>
GO
GRANT EXECUTE ANY EXTERNAL SCRIPT TO [UserName]
```

> [!NOTE]
> Permissions are not specific to the supported script language. In other words, there are not separate permission levels for R script versus Python script.

<a name="permissions-db"></a>

## Grant databases permissions

While a user is running scripts, the user might need to read data from other databases. The user might also need to create new tables to store results, and write data into tables.

For each Windows user account or SQL login that is running R or Python scripts, ensure that it has the appropriate permissions on the specific database: 

+ `db_datareader` to read data.
+ `db_datawriter` to save objects to the database.
+ `db_ddladmin` to create objects such as stored procedures or tables containing trained and serialized data.

For example, the following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement gives the SQL login *MySQLLogin* the rights to run T-SQL queries in the *ML_Samples* database. To run this statement, the SQL login must already exist in the security context of the server.

```sql
USE ML_Samples
GO
EXEC sp_addrolemember 'db_datareader', 'MySQLLogin'
```

## Next steps

For more information about the permissions included in each role, see [Database-level roles](../../relational-databases/security/authentication-access/database-level-roles.md).
