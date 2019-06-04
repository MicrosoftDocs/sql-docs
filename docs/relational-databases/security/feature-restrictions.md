---
title: "Feature Restrictions | Microsoft Docs"
ms.custom: ""
ms.date: "01/09/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
author: vainolo
ms.author: arib
manager: tomerw
monikerRange: ">= sql-server-ver15 || = sqlallproducts-allversions"
---
# Feature Restrictions

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

One common source of SQL Server attacks is through web applications that access the database where various forms of SQL injection attacks are used to glean information about the database.  Ideally, application code is developed so it does not allow for SQL injection.  However, in large code-bases that include legacy and external code, one can never be sure that all cases have been addressed, so SQL injections are a fact of life that we have to protect against.  The goal of feature restrictions is to prevent some forms of SQL injection from leaking information about the database, even when the SQL injection is successful.

## Enabling Feature Restrictions

Enabling feature restrictions is done using the `sp_add_feature_restriction` stored procedure as follows:

```sql
EXEC sp_add_feature_restriction <feature>, <object_class>, <object_name>
```

The following features can be restricted:

| Feature          | Description |
|------------------|-------------|
| N'ErrorMessages' | When restricted, any user data within the error message will be masked. See [Error Messages Feature Restriction](#error-messages-feature-restriction) |
| N'Waitfor'       | When restricted, the command will return immediately without any delay. See [WAITFOR Feature Restriction](#waitfor-feature-restriction) |

The value of `object_class` can be either `N'User'` or `N'Role'` to denote whether `object_name` is a User name or a Role name in the database.

The following example will cause all error messages for user `MyUser` to be masked:

```sql
EXEC sp_add_feature_restriction N'ErrorMessages', N'User', N'MyUser'
```

## Disabling Feature Restrictions

Disabling feature restrictions is done using the `sp_drop_feature_restriction` stored procedure as follows:

```sql
EXEC sp_drop_feature_restriction <feature>, <object_class>, <object_name>
```

The following example disables error message masking for user `MyUser`:

```sql
EXEC sp_drop_feature_restriction N'ErrorMessages', N'User', N'MyUser'
```

## Viewing Feature Restrictions

The `sys.sql_feature_restrictions` view presents all the currently defined feature restrictions on the database. See [sys.sql_feature_restrictions](../system-catalog-views/sys-sql-feature-restrictions.md) for mode information.

## Feature Restrictions

### Error Messages Feature Restriction

One common SQL injection attack method is to inject code that causes an error.  By examining the error message, an attacker can learn information about the system, enabling additional more targeted attacks.  This attack can be especially useful where the application does not display the results of a query, but does display error messages.

Consider a web application that has a request in the form of:

```html
http://www.contoso.com/employee.php?id=1
```

Which executes the following database query:

```sql
SELECT Name FROM EMPLOYEES WHERE Id=$EmpId
```

If the value passed as the `Id` parameter to the web application request is copied to replace $EmpId in the database query, an attacker could make the following request:

```html
http://www.contoso.com/employee.php?id=1 AND CAST(DB_NAME() AS INT)=0
```

And the following error would be returned, allowing the attacker to learn the name of the database:

```sql
Conversion failed when converting the nvarchar value 'HR_Data' to data type int.
```

After enabling error messages feature restriction for the application user in the database, the error message returned is masked so that no internal information about the database is leaked:

```sql
Conversion failed when converting the ****** value '******' to data type ******.
```

Similarly, the attacker could make the following request:

```html
http://www.contoso.com/employee.php?id=1 AND CAST(Salary AS TINYINT)=0
```

And the following error would be returned, allowing the attacker to learn the employee’s salary:

```sql
Arithmetic overflow error for data type tinyint, value = 140000.
```

Using error messages feature restriction, the database would return:

```sql
Arithmetic overflow error for data type ******, value = ******.
```

### WAITFOR Feature Restriction

A Blind SQL Injection is when an application does not provides an attacker with the results of the injected SQL or with an error message, but the attacker can infer information from the database by constructing a conditional query in which the two conditional branches take a different amount of time to execute. By comparing the response time, the attacker can know which branch was executed, and thereby learn information about the system. The simplest variant of this attack is using the `WAITFOR` statement to introduce the delay.

Consider a web application that has a request in the form of:

```html
http://www.contoso.com/employee.php?id=1
```

which executes the following database query:

```sql
SELECT Name FROM EMPLOYEES WHERE Id=$EmpId
```

If the value passed as the `Id` parameter to the web application requests is copied to replace $EmpId in the database query, an attacker can make the following request:

```html
http://www.contoso.com/employee.php?id=1; IF SYSTEM_USER='sa' WAITFOR DELAY '00:00:05'
```

And the query would take an additional 5 seconds if the `sa` account was being used. If `WAITFOR` feature restriction is disabled in the database, the `WAITFOR` statement will be ignored and not information is leaked using this attack.
