---
title: "Modify User-defined Functions"
description: "Modify User-defined Functions"
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/28/2022"
ms.service: sql
ms.topic: conceptual
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Modify user-defined functions

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

You can modify user-defined functions in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. Modifying user-defined functions as described below won't change the functions' permissions, nor will it affect any dependent functions, stored procedures, or triggers.

## <a id="Restrictions"></a> Limitations and restrictions

ALTER FUNCTION can't be used to perform any of the following actions:

- Change a scalar-valued function to a table-valued function, or vice versa.

- Change an inline function to a multistatement function, or vice versa.

- Change a Transact-SQL function to a CLR function, or vice-versa.

## Permissions

Requires ALTER permission on the function or on the schema. If the function specifies a user-defined type, requires EXECUTE permission on the type.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. Select the plus sign next to the database that contains the function you wish to modify.

1. Select the plus sign next to the **Programmability** folder.

1. Select the plus sign next to the folder that contains the function you wish to modify:

   - Table-valued Function

   - Scalar-valued Function

   - Aggregate Function

1. Right-click the function you want to modify and select **Modify**.

1. In the Query Window, make the necessary changes to the ALTER FUNCTION statement.

1. On the **File** menu, select **Save**_function_name_.

## <a id="TsqlProcedure"></a> Use Transact-SQL

1. In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following examples into the query window and select **Execute**.

   The following code sample alters a scalar-valued function.

   ```sql
   -- Scalar-Valued Function
   USE [AdventureWorks2012]
   GO
   ALTER FUNCTION [dbo].[ufnGetAccountingEndDate]()
   RETURNS [datetime]
   AS
   BEGIN
       RETURN DATEADD(millisecond, -2, CONVERT(datetime, '20040701', 112));
   END;
   ```

   The following code sample alters a table-valued function.

   ```sql
   -- Table-Valued Function
   USE [AdventureWorks2012]
   GO
   ALTER FUNCTION [dbo].[ufnGetContactInformation](@PersonID int)
   RETURNS @retContactInformation TABLE
   (
       -- Columns returned by the function
       [PersonID] int NOT NULL,
       [FirstName] [nvarchar](50) NULL,
       [LastName] [nvarchar](50) NULL,
       [JobTitle] [nvarchar](50) NULL,
       [BusinessEntityType] [nvarchar](50) NULL
   )
   AS
   -- Returns the first name, last name, job title and business entity type for the specified contact.
   -- Since a contact can serve multiple roles, more than one row may be returned.
   BEGIN
   IF @PersonID IS NOT NULL
   BEGIN
        IF EXISTS(SELECT * FROM [HumanResources].[Employee] e
        WHERE e.[BusinessEntityID] = @PersonID)
        INSERT INTO @retContactInformation
             SELECT @PersonID, p.FirstName, p.LastName, e.[JobTitle], 'Employee'
             FROM [HumanResources].[Employee] AS e
             INNER JOIN [Person].[Person] p ON p.[BusinessEntityID] = e.[BusinessEntityID]
             WHERE e.[BusinessEntityID] = @PersonID;

        IF EXISTS(SELECT * FROM [Purchasing].[Vendor] AS v
        INNER JOIN [Person].[BusinessEntityContact] bec ON bec.[BusinessEntityID] = v.[BusinessEntityID]
        WHERE bec.[PersonID] = @PersonID)
        INSERT INTO @retContactInformation
             SELECT @PersonID, p.FirstName, p.LastName, ct.[Name], 'Vendor Contact'
             FROM [Purchasing].[Vendor] AS v
             INNER JOIN [Person].[BusinessEntityContact] bec ON bec.[BusinessEntityID] = v.[BusinessEntityID]
             INNER JOIN [Person].ContactType ct ON ct.[ContactTypeID] = bec.[ContactTypeID]
             INNER JOIN [Person].[Person] p ON p.[BusinessEntityID] = bec.[PersonID]
             WHERE bec.[PersonID] = @PersonID;

        IF EXISTS(SELECT * FROM [Sales].[Store] AS s
        INNER JOIN [Person].[BusinessEntityContact] bec ON bec.[BusinessEntityID] = s.[BusinessEntityID]
        WHERE bec.[PersonID] = @PersonID)
        INSERT INTO @retContactInformation
             SELECT @PersonID, p.FirstName, p.LastName, ct.[Name], 'Store Contact'
             FROM [Sales].[Store] AS s
             INNER JOIN [Person].[BusinessEntityContact] bec ON bec.[BusinessEntityID] = s.[BusinessEntityID]
             INNER JOIN [Person].ContactType ct ON ct.[ContactTypeID] = bec.[ContactTypeID]
             INNER JOIN [Person].[Person] p ON p.[BusinessEntityID] = bec.[PersonID]
             WHERE bec.[PersonID] = @PersonID;

        IF EXISTS(SELECT * FROM [Person].[Person] AS p
        INNER JOIN [Sales].[Customer] AS c ON c.[PersonID] = p.[BusinessEntityID]
        WHERE p.[BusinessEntityID] = @PersonID AND c.[StoreID] IS NULL)
        INSERT INTO @retContactInformation
             SELECT @PersonID, p.FirstName, p.LastName, NULL, 'Consumer'
             FROM [Person].[Person] AS p
             INNER JOIN [Sales].[Customer] AS c ON c.[PersonID] = p.[BusinessEntityID]
             WHERE p.[BusinessEntityID] = @PersonID AND c.[StoreID] IS NULL;
        END
   RETURN;
   END;
   ```

## See also

- [ALTER FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-function-transact-sql.md)
