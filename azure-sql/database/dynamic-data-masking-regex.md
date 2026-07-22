---
title: "Regex-based dynamic data masking (preview)"
titleSuffix: Azure SQL Database
description: Use REGEXP_REPLACE in Azure SQL Database dynamic data masking to define pattern-driven masks for emails, phone numbers, and other structured string data.
author: madhumitatripathy
ms.author: matripathy
ms.reviewer: vanto, wiassaf, mathoma, randolphwest
ms.date: 05/28/2026
ms.service: azure-sql-database
ms.subservice: security
ms.topic: how-to
ms.custom:
  - sqldbrb=1
monikerRange: "=azuresql || =azuresql-db"
ai-usage: ai-assisted
---
# Regex-based dynamic data masking in Azure SQL Database (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article shows you how to use the `REGEXP_REPLACE` masking function to define pattern-driven [dynamic data masking](dynamic-data-masking-overview.md) rules in Azure SQL Database. Regex-based masking lets you mask variable-length string data and preserve specific segments that built-in masks can't express.

> [!IMPORTANT]
> Regex-based dynamic data masking is currently in public preview for Azure SQL Database. Preview features are provided "as is" and are excluded from the service-level agreements and limited warranty. Use this feature only in nonproduction environments. For more information, see [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

## Overview

Dynamic data masking (DDM) in Azure SQL Database dynamically hides sensitive data in query results before the data is sent to the application. This feature reduces accidental exposure of sensitive information and removes the need to add masking logic to your application code.

DDM lets database administrators and developers define masking rules for sensitive columns centrally in the database. Nonprivileged users see obfuscated values, while the actual data remains intact in storage.

## Limitations of built-in masking functions

Azure SQL Database provides several built-in masking functions: `default()`, `email()`, `random()`, `partial()`, and `datetime()`. Most of these functions apply rigid patterns that you can't customize and aren't flexible for variable-length data.

For example, the built-in `email()` mask always produces a format like `aXXX@xxxx.com` for an email address such as `alice.johnson@example.com`. This mask exposes only the first letter of the username and obscures the domain name. The fixed output isn't useful when you need to retain the domain name for business workflows.

## What is regex-based masking?

Regex-based DDM introduces [`REGEXP_REPLACE`](/sql/t-sql/functions/regexp-replace-transact-sql) as a new masking function. By using this function, you can create custom, pattern-driven masks for string data such as phone numbers, email addresses, and identification numbers.


Regex lets you precisely define which substrings to reveal or obscure. Built-in functions can't express this kind of variable-length pattern. This capability is useful when you work with structured data patterns such as emails, phone numbers, or identifiers. In those cases, you often need to preserve specific visible segments while masking sensitive parts, and built-in masks are too rigid for your business workflows.

> [!NOTE]
> - The permission model is unchanged. This enhancement introduces no new permissions.
> - You configure regex masks in T-SQL only. The Azure portal doesn't support regex mask configuration in this release.

## Syntax

Apply a regex mask at table creation:

```sql
column_name <data_type> MASKED WITH (FUNCTION = 'REGEXP_REPLACE("<pattern_expression>", "<string_replacement>")')
```

Add a regex mask to an existing column:

```sql
ALTER TABLE <schema_name>.<table_name>
ALTER COLUMN <column_name>
ADD MASKED WITH (FUNCTION = 'REGEXP_REPLACE("<pattern_expression>", "<string_replacement>")');
```

### Arguments

*pattern_expression*

The regular expression to evaluate against the column value. The maximum length is 8,000 bytes.

*string_replacement*

The replacement string applied to every match of *pattern_expression*.

> [!NOTE]
> When you use `REGEXP_REPLACE` as a masking function, only the *pattern_expression* and *string_replacement* arguments work. The native T-SQL [`REGEXP_REPLACE`](/sql/t-sql/functions/regexp-replace-transact-sql) function also supports *start*, *occurrence*, and *flags* arguments, but you can't override them when you use it as a masking function. The fixed values are *start* = 1, *occurrence* = 0 (replace all matches), and *flags* = `'c'` (case-sensitive).

The `REGEXP_REPLACE` masking function supports columns of data type `char`, `nchar`, `varchar`, or `nvarchar`, and LOB types (`varchar(max)` and `nvarchar(max)`) up to 2 MB.

## Tutorial: Configure regex-based dynamic data masking

The following tutorial shows you how to create regex masks, insert sample data, and verify masked versus unmasked output for different users.

### Step 1: Create a table with regex masks

The following statement creates a `CustomerDetails` table with two regex-masked columns:

- **Phone mask**: Captures the country code (for example, `+1`, `+44`, `+91`) and replaces the remaining digits with `xxxx`. This preserves the country code for context.
- **Email mask**: Replaces the username portion (before `@`) with `*****` while preserving the domain name. For example, `alice.johnson@example.com` becomes `*****@example.com`.

```sql
-- Drop the CustomerDetails table if it exists
DROP TABLE IF EXISTS Data.CustomerDetails;

CREATE TABLE Data.CustomerDetails (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name varchar(30),
    Phone_Number varchar(30)
        MASKED WITH (FUNCTION = 'REGEXP_REPLACE(
            "(\+\d{1,3})(?:[ -.]?\d){7,14}",
            "(\1)-xxxx")'),
    Email varchar(255)
        MASKED WITH (FUNCTION = 'REGEXP_REPLACE(
            "([a-zA-Z0-9._%+-]+)(@+)([a-zA-Z0-9.-]+)(\.)(\w)",
            "*****\2\3\4\5")')
);
```

### Step 2: Insert sample data

```sql
INSERT INTO Data.CustomerDetails (Name, Phone_Number, Email) VALUES
('Alice Johnson', '+1 202-555-0123', 'alice.johnson@example.com'),
('Bob Smith', '+1 415-555-0198', 'bob.smith@contoso.com'),
('Oliver Bennett', '+44 7700 900123', 'oliver.bennett@example.co.uk'),
('Amelia Thompson', '+44 7700 900456', 'amelia.thompson@example.co.uk'),
('Rajeev Mehra', '+91 90000 12345', 'rajeev.mehra@adventure-works.net'),
('John Miller', '+91 98888 45678', 'jon.miller@contoso.net'),
('Lukas Mueller', '+49 151 23456789', 'lukas.mueller@example.de'),
('Daniel Tan', '+65 8123 4567', 'daniel.tan@example.sg'),
('Hiroshi Tanaka', '+81 90 1234 5678', 'hiroshi.tanaka@example.jp');
```

### Step 3: Query as a privileged user

Users with `sysadmin`, `db_owner`, or explicit `UNMASK` permission always see the actual data.

```sql
-- As sysadmin or db_owner, you see the real data
SELECT * FROM Data.CustomerDetails;
```

Expected result: All phone numbers and emails appear in full (for example, `+1 202-555-0123`, `alice.johnson@example.com`).

### Step 4: Query as a nonprivileged user

Create a test user without `UNMASK` permission and query the table.

```sql
-- Create a test user without UNMASK permission
CREATE USER SupportEngineer WITHOUT LOGIN;

-- Grant SELECT permission on CustomerDetails to SupportEngineer
GRANT SELECT ON Data.CustomerDetails TO SupportEngineer;

-- Query the table as the nonprivileged user
EXECUTE AS USER = 'SupportEngineer';
SELECT * FROM Data.CustomerDetails;
REVERT;
```

Expected result: Phone numbers show only the country code (for example, `(+1)-xxxx`), and emails hide the username (for example, `*****@example.com`).

### Step 5: Grant UNMASK and requery

After you grant `UNMASK`, the same user now sees full unmasked values. This shows how DDM's permission-based access control works.

```sql
-- Grant UNMASK permission to the test user
GRANT UNMASK ON Data.CustomerDetails TO SupportEngineer;

-- Re-run the query as the same user - now sees unmasked data
EXECUTE AS USER = 'SupportEngineer';
SELECT * FROM Data.CustomerDetails;
REVERT;
```

## Add a regex mask to an existing column

To add a regex mask to an existing column, use [`ALTER TABLE`](/sql/t-sql/statements/alter-table-transact-sql):

```sql
ALTER TABLE Data.CustomerDetails
ALTER COLUMN Phone_Number
ADD MASKED WITH (FUNCTION = 'REGEXP_REPLACE(
    "(\+\d{1,3})(?:[ -.]?\d){7,14}",
    "(\1)-xxxx")');
```

## Verify the masking policy via metadata

Use the [`sys.masked_columns`](/sql/relational-databases/system-catalog-views/sys-masked-columns-transact-sql) system view to inspect configured masks. This view includes `REGEXP_REPLACE` mask definitions.

```sql
SELECT c.name,
       tbl.name AS table_name,
       c.is_masked,
       c.masking_function
FROM sys.masked_columns AS c
JOIN sys.tables AS tbl
    ON c.[object_id] = tbl.[object_id]
WHERE is_masked = 1;
```

## Considerations

- **Regex masks are pattern-dependent.** Validate your regex patterns on representative data before you apply them to production columns.
- **Unmatched values stay unmasked.** An unmatched pattern returns raw data to nonprivileged users, which can expose sensitive information.
- **Test thoroughly with representative data.** Cover edge cases such as null values, empty strings, and atypical formats your regex might not catch.
- **Use in nonproduction environments during preview.** Validate thoroughly before you consider this feature for production workloads.

## Related content

- [Dynamic data masking](dynamic-data-masking-overview.md)
- [Get started with SQL Database dynamic data masking with the Azure portal](dynamic-data-masking-configure-portal.md)
- [GRANT database permissions (Transact-SQL)](/sql/t-sql/statements/grant-database-permissions-transact-sql)
- [sys.masked_columns (Transact-SQL)](/sql/relational-databases/system-catalog-views/sys-masked-columns-transact-sql)
- [ALTER TABLE (Transact-SQL)](/sql/t-sql/statements/alter-table-transact-sql)
- [REGEXP_REPLACE (Transact-SQL)](/sql/t-sql/functions/regexp-replace-transact-sql)
- [Regular expressions (SQL Server)](/sql/relational-databases/regular-expressions/overview)
- [Regular expressions functions (Transact-SQL)](/sql/t-sql/functions/regular-expressions-functions-transact-sql)
