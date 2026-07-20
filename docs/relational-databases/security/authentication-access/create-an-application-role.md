---
title: Create an Application Role
description: Create an application role in SQL Server by using SQL Server Management Studio or Transact-SQL to restrict access to a database except through an application.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.swb.approle.general.f1"
helpviewer_keywords:
  - "application roles [SQL Server], creating"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Create an application role

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to create an application role in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../../includes/tsql-md.md)]. Application roles restrict user access to a database except through specific applications. Application roles have no users, so the **Role Members** list isn't displayed when **Application role** is selected.

> [!IMPORTANT]  
> Password complexity is checked when application role passwords are set. Applications that invoke application roles must store their passwords. Application role passwords should always be stored encrypted.

<a id="Background"></a>

## Background

[!INCLUDE [encryption-algorithm-history-md](../../../includes/encryption-algorithm-history.md)]

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

1. In Object Explorer, expand the database where you want to create an application role.

1. Expand the **Security** folder.

1. Expand the **Roles** folder.

1. Right-click the **Application Roles** folder and select **New Application Role...**.

1. In the **Application Role - New** dialog box, on the **General Page**, enter the new name of the new application role in the **Role name** box.

1. In the **Default Schema** box, specify the schema that will own objects created by this role by entering the object names. Alternately, select the ellipsis **(...)** to open the **Locate Schema** dialog box.

1. In the **Password** box, enter a password for the new role. Enter that password again into the **Confirm Password** box.

1. Under **Schemas owned by this role**, select or view schemas that will be owned by this role. A schema can be owned by only one schema or role.

1. Select **OK**.

### Additional options

The **Application Role - New** dialog box also offers options on two additional pages: **Securables** and **Extended Properties**.

- The **Securables** page lists all possible securables and the permissions on those securables that can be granted to the login.

- The **Extended properties** page allows you to add custom properties to database users.

<a id="TsqlProcedure"></a>

## Use transact-SQL

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. This code creates an application role called `weekly_receipts` that has a password, and `Sales` as its default schema. Replace `<password>` with a strong password.

   ```sql
   -- 

   CREATE APPLICATION ROLE weekly_receipts
       WITH PASSWORD = '<password>'
       , DEFAULT_SCHEMA = Sales;
   GO
   ```

## Permissions

Requires `ALTER ANY APPLICATION ROLE` permission on the database.

## Related content

- [CREATE APPLICATION ROLE (Transact-SQL)](../../../t-sql/statements/create-application-role-transact-sql.md)
