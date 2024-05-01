---
title: Dynamic data masking
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: Dynamic data masking limits sensitive data exposure by masking it to nonprivileged users for Azure SQL Database, Azure SQL Managed Instance and Azure Synapse Analytics
author: madhumitatripathy
ms.author: matripathy
ms.reviewer: wiassaf, vanto, mathoma, randolphwest
ms.date: 04/03/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
ms.custom: sqldbrb=1
tags: azure-synapse
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Dynamic data masking

[!INCLUDE[appliesto-sqldb-sqlmi-asa-dedicated-only](../includes/appliesto-sqldb-sqlmi-asa-dedicated-only.md)]

Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics support dynamic data masking. Dynamic data masking limits sensitive data exposure by masking it to nonprivileged users.

Dynamic data masking helps prevent unauthorized access to sensitive data by enabling customers to designate how much of the sensitive data to reveal with minimal effect on the application layer. It's a policy-based security feature that hides the sensitive data in the result set of a query over designated database fields, while the data in the database isn't changed.

For example, a service representative at a call center might identify a caller by confirming several characters of their email address, but the complete email address shouldn't be revealed to the service representative. A masking rule can be defined that masks all the email address in the result set of any query. As another example, an appropriate data mask can be defined to protect personal data, so that a developer can query production environments for troubleshooting purposes without violating compliance regulations.

## Dynamic data masking basics

You set up a dynamic data masking policy in the Azure portal by selecting the **Dynamic Data Masking** pane under **Security** in your SQL Database configuration pane. This feature can't be set using portal for SQL Managed Instance. For more information, see [Dynamic Data Masking](/sql/relational-databases/security/dynamic-data-masking).

### Dynamic data masking policy

- **SQL users excluded from masking:** A set of SQL users, which can include identities from Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), that get unmasked data in the SQL query results. Users with administrative rights like server admin, Microsoft Entra admin and db_owner role can view the original data without any mask. (Note: It also applies to sysadmin role in SQL Server)
- **Masking rules:** A set of rules that define the designated fields to be masked and the masking function that is used. The designated fields can be defined using a database schema name, table name, and column name.
- **Masking functions:** A set of methods that control the exposure of data for different scenarios.

| Masking function | Masking logic |
| --- | --- |
| **Default** | **Full masking according to the data types of the designated fields**<br /><br />* Use `XXXX` (or fewer) if the size of the field is fewer than 4 characters for string data types (**nchar**, **ntext**, **nvarchar**).<br />* Use a zero value for numeric data types (**bigint**, **bit**, **decimal**, **int**, **money**, **numeric**, **smallint**, **smallmoney**, **tinyint**, **float**, **real**).<br />* Use `1900-01-01` for date/time data types (**date**, **datetime2**, **datetime**, **datetimeoffset**, **smalldatetime**, **time**).<br />* For **sql_variant**, the default value of the current type is used.<br />* For XML, the document `<masked />` is used.<br />* Use an empty value for special data types (**timestamp**, **table**, **HierarchyID**, **uniqueidentifier**, **binary**, **image**, **varbinary**, and spatial types). |
| **Credit card** | **Masking method, which exposes the last four digits of the designated fields** and adds a constant string as a prefix in the form of a credit card.<br /><br />`XXXX-XXXX-XXXX-1234` |
| **Email** | **Masking method, which exposes the first letter and replaces the domain with XXX.com** using a constant string prefix in the form of an email address.<br /><br />`aXX@XXXX.com` |
| **Random number** | **Masking method, which generates a random number** according to the selected boundaries and actual data types. If the designated boundaries are equal, then the masking function is a constant number.<br /><br />:::image type="content" source="./media/dynamic-data-masking-overview/random-number.png" alt-text="Screenshot that shows the masking method for generating a random number."::: |
| **Custom text** | **Masking method, which exposes the first and last characters** and adds a custom padding string in the middle. If the original string is shorter than the exposed prefix and suffix, only the padding string is used.<br /><br />`prefix[padding]suffix`<br /><br />:::image type="content" source="./media/dynamic-data-masking-overview/custom-text.png" alt-text="Screenshot of the navigation pane."::: |

### Recommended fields to mask

The DDM recommendations engine, flags certain fields from your database as potentially sensitive fields, which may be good candidates for masking. In the **Dynamic Data Masking** pane in the portal, you see the recommended columns for your database. Select **Add Mask** for one or more columns, then select the appropriate masking function and select **Save**, to apply mask for these fields.

## Manage dynamic data masking using T-SQL

- To create a dynamic data mask, see [Creating a Dynamic Data Mask](/sql/relational-databases/security/dynamic-data-masking#creating-a-dynamic-data-mask).
- To add or edit a mask on an existing column, see [Adding or Editing a Mask on an Existing Column](/sql/relational-databases/security/dynamic-data-masking#adding-or-editing-a-mask-on-an-existing-column).
- To grant permissions to view unmasked data, see [Granting Permissions to View Unmasked Data](/sql/relational-databases/security/dynamic-data-masking#granting-permissions-to-view-unmasked-data).
- To drop a dynamic data mask, see [Dropping a Dynamic Data Mask](/sql/relational-databases/security/dynamic-data-masking#dropping-a-dynamic-data-mask).

## Set up dynamic data masking for your database using PowerShell cmdlets

### Data masking policies

- [Get-AzSqlDatabaseDataMaskingPolicy](/powershell/module/az.sql/Get-AzSqlDatabaseDataMaskingPolicy)
- [Set-AzSqlDatabaseDataMaskingPolicy](/powershell/module/az.sql/Set-AzSqlDatabaseDataMaskingPolicy)

### Data masking rules

- [Get-AzSqlDatabaseDataMaskingRule](/powershell/module/az.sql/Get-AzSqlDatabaseDataMaskingRule)
- [New-AzSqlDatabaseDataMaskingRule](/powershell/module/az.sql/New-AzSqlDatabaseDataMaskingRule)
- [Remove-AzSqlDatabaseDataMaskingRule](/powershell/module/az.sql/Remove-AzSqlDatabaseDataMaskingRule)
- [Set-AzSqlDatabaseDataMaskingRule](/powershell/module/az.sql/Set-AzSqlDatabaseDataMaskingRule)

## Set up dynamic data masking for your database using the REST API

You can use the REST API to programmatically manage data masking policy and rules. The published REST API supports the following operations:

### Data masking policies

- [Create Or Update](/rest/api/sql/data-masking-policies/create-or-update): Creates or updates a database data masking policy.
- [Get](/rest/api/sql/data-masking-policies/get): Gets a database data masking policy.

### Data masking rules

- [Create Or Update](/rest/api/sql/data-masking-rules/create-or-update): Creates or updates a database data masking rule.
- [List By Database](/rest/api/sql/data-masking-rules/list-by-database): Gets a list of database data masking rules.

## Permissions

These are the built-in roles to configure dynamic data masking is:

- [SQL Security Manager](/azure/role-based-access-control/built-in-roles#sql-security-manager)
- [SQL DB Contributor](/azure/role-based-access-control/built-in-roles#sql-db-contributor)
- [SQL Server Contributor](/azure/role-based-access-control/built-in-roles#sql-server-contributor)

These are the required actions to use dynamic data masking:

Read/Write:

- `Microsoft.Sql/servers/databases/dataMaskingPolicies/*`

Read:

- `Microsoft.Sql/servers/databases/dataMaskingPolicies/read`

Write:

- `Microsoft.Sql/servers/databases/dataMaskingPolicies/write`

To learn more about permissions when using dynamic data masking with T-SQL command, see [Permissions](/sql/relational-databases/security/dynamic-data-masking#permissions)

## Granular permission example

Prevent unauthorized access to sensitive data and gain control by masking it to an unauthorized user at different levels of the database. You can grant or revoke UNMASK permissions at the database-level, schema-level, table-level or at the column-level to any database user or role. Combined with Microsoft Entra authentication, UNMASK permissions can be managed for users, groups, and applications maintained within your Azure environment. The UNMASK permission provides a granular way to control and limit unauthorized access to data stored in the database and improve data security management.

1. Create schema to contain user tables:

   ```sql
   CREATE SCHEMA Data;
   GO
   ```

1. Create table with masked columns:

   ```sql
   CREATE TABLE Data.Membership (
       MemberID INT IDENTITY(1, 1) NOT NULL,
       FirstName VARCHAR(100) MASKED WITH (FUNCTION = 'partial(1, "xxxxx", 1)') NULL,
       LastName VARCHAR(100) NOT NULL,
       Phone VARCHAR(12) MASKED WITH (FUNCTION = 'default()') NULL,
       Email VARCHAR(100) MASKED WITH (FUNCTION = 'email()') NOT NULL,
       DiscountCode SMALLINT MASKED WITH (FUNCTION = 'random(1, 100)') NULL,
       BirthDay DATETIME MASKED WITH (FUNCTION = 'default()') NULL
   );
   ```

1. Insert sample data:

   ```sql
   INSERT INTO Data.Membership (FirstName, LastName, Phone, Email, DiscountCode, BirthDay)
   VALUES
   ('Roberto', 'Tamburello', '555.123.4567', 'RTamburello@contoso.com', 10, '1985-01-25 03:25:05'),
   ('Janice', 'Galvin', '555.123.4568', 'JGalvin@contoso.com.co', 5, '1990-05-14 11:30:00'),
   ('Shakti', 'Menon', '555.123.4570', 'SMenon@contoso.net', 50, '2004-02-29 14:20:10'),
   ('Zheng', 'Mu', '555.123.4569', 'ZMu@contoso.net', 40, '1990-03-01 06:00:00');
   ```

1. Create schema to contain service tables:

   ```sql
   CREATE SCHEMA Service;
   GO
   ```

1. Create service table with masked columns:

   ```sql
   CREATE TABLE Service.Feedback (
       MemberID INT IDENTITY(1, 1) NOT NULL,
       Feedback VARCHAR(100) MASKED WITH (FUNCTION = 'default()') NULL,
       Rating INT MASKED WITH (FUNCTION = 'default()'),
       Received_On DATETIME
   );
   ```

1. Insert sample data:

   ```sql
   INSERT INTO Service.Feedback (Feedback, Rating, Received_On)
   VALUES
       ('Good', 4, '2022-01-25 11:25:05'),
       ('Excellent', 5, '2021-12-22 08:10:07'),
       ('Average', 3, '2021-09-15 09:00:00');
   ```

1. Create different users in the database:

   ```sql
   CREATE USER ServiceAttendant WITHOUT LOGIN;
   GO
   
   CREATE USER ServiceLead WITHOUT LOGIN;
   GO
   
   CREATE USER ServiceManager WITHOUT LOGIN;
   GO
   
   CREATE USER ServiceHead WITHOUT LOGIN;
   GO
   ```

1. Grant read permissions to the users in the database:

   ```sql
   ALTER ROLE db_datareader ADD MEMBER ServiceAttendant;
   
   ALTER ROLE db_datareader ADD MEMBER ServiceLead;
   
   ALTER ROLE db_datareader ADD MEMBER ServiceManager;
   
   ALTER ROLE db_datareader ADD MEMBER ServiceHead;
   ```

1. Grant different UNMASK permissions to users:

   ```sql
   --Grant column level UNMASK permission to ServiceAttendant
   GRANT UNMASK ON Data.Membership(FirstName) TO ServiceAttendant;
   
   -- Grant table level UNMASK permission to ServiceLead
   GRANT UNMASK ON Data.Membership TO ServiceLead;
   
   -- Grant schema level UNMASK permission to ServiceManager
   GRANT UNMASK ON SCHEMA::Data TO ServiceManager;
   GRANT UNMASK ON SCHEMA::Service TO ServiceManager;
   
   --Grant database level UNMASK permission to ServiceHead;
   GRANT UNMASK TO ServiceHead;
   ```

1. Query the data under the context of user `ServiceAttendant`:

   ```sql
   EXECUTE AS USER = 'ServiceAttendant';

   SELECT MemberID, FirstName, LastName, Phone, Email, BirthDay
   FROM Data.Membership;

   SELECT MemberID, Feedback, Rating
   FROM Service.Feedback;

   REVERT;
   ```

1. Query the data under the context of user `ServiceLead`:

   ```sql
   EXECUTE AS USER = 'ServiceLead';

   SELECT MemberID, FirstName, LastName, Phone, Email, BirthDay
   FROM Data.Membership;

   SELECT MemberID, Feedback, Rating
   FROM Service.Feedback;

   REVERT;
   ```

1. Query the data under the context of user `ServiceManager`:

   ```sql
   EXECUTE AS USER = 'ServiceManager';

   SELECT MemberID, FirstName, LastName, Phone, Email, BirthDay
   FROM Data.Membership;

   SELECT MemberID, Feedback, Rating
   FROM Service.Feedback;

   REVERT;
   ```

1. Query the data under the context of user `ServiceHead`

   ```sql
   EXECUTE AS USER = 'ServiceHead';

   SELECT MemberID, FirstName, LastName, Phone, Email, BirthDay
   FROM Data.Membership;

   SELECT MemberID, Feedback, Rating
   FROM Service.Feedback;

   REVERT;
   ```

1. To revoke UNMASK permissions, use the following T-SQL statements:

   ```sql
   REVOKE UNMASK ON Data.Membership(FirstName) FROM ServiceAttendant;
   
   REVOKE UNMASK ON Data.Membership FROM ServiceLead;
   
   REVOKE UNMASK ON SCHEMA::Data FROM ServiceManager;
   
   REVOKE UNMASK ON SCHEMA::Service FROM ServiceManager;
   
   REVOKE UNMASK FROM ServiceHead;
   ```

## Related content

- [Dynamic Data Masking](/sql/relational-databases/security/dynamic-data-masking) for SQL Server.
- Data Exposed episode about [Granular Permissions for Azure SQL Dynamic Data Masking](/Shows/Data-Exposed/Granular-Permissions-for-Azure-SQL-Dynamic-Data-Masking) on Channel 9.
