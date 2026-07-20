---
title: "Grant a Permission to a Principal"
description: Learn how to grant permission to a principal in SQL Server by using SQL Server Management Studio or Transact-SQL, including best practices.
author: VanMSFT
ms.author: vanto
ms.date: 11/05/2024
ms.service: sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "Grant permission to a principal"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Grant a Permission to a Principal

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

  This article describes how to grant permission to a principal in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)].  
    
<a id="Security"></a>

## Security
  
The grantor (or the principal specified with the AS option) must have either the permission itself with GRANT OPTION or a higher permission that implies the permission being granted. 

<a id="SSMSProcedure"></a>

<a id="using-sql-server-management-studio"></a>

## Use SQL Server Management Studio
  
<a id="to-grant-permission-to-a-principal"></a>

#### Grant permission to a principal
  
1. In Object Explorer, expand the database that contains the object to which you want to grant permissions.  
  
    > [!NOTE]  
    >  These steps deal specifically with granting permissions to a stored procedure, but you can use similar steps to add permissions to tables, views, functions, and assemblies, as well as other securables. For more information, see [GRANT (Transact-SQL)](../../../t-sql/statements/grant-transact-sql.md)  
  
1. Expand the **Programmability** folder.  
  
1. Expand the **Stored Procedures** folder.  
  
1. Right-click a stored procedure and select **Properties**.  
  
1. In the **Stored Procedure Properties** dialog box, select the **Permissions** page. Use this page to add users or roles to the stored procedure and specify the permissions those users or roles have.  
  
1. When finished, select **OK**.  
  
<a id="TsqlProcedure"></a>

<a id="using-transact-sql"></a>

## Use Transact-SQL
  
<a id="to-grant-permission-to-a-principal"></a>

#### Grant permission to a principal
  
1. In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../../includes/ssde-md.md)].  
  
1. On the Standard bar, select **New Query**.  
  
1. Copy and paste the following example into the query window and select **Execute**. The following sample grants EXECUTE permission on the stored procedure `HumanResources.uspUpdateEmployeeHireInfo` to an application role called `Recruiting11`.
  
    ```sql
    -- Grants EXECUTE permission on stored procedure HumanResources.uspUpdateEmployeeHireInfo to an application role called Recruiting11.   
    USE AdventureWorks2022;  
    GO  
    GRANT EXECUTE ON OBJECT::HumanResources.uspUpdateEmployeeHireInfo  
        TO Recruiting11;  
    GO  
    ```  
  
 For more information, see [GRANT (Transact-SQL)](../../../t-sql/statements/grant-transact-sql.md) and [GRANT object permissions (Transact-SQL)](../../../t-sql/statements/grant-object-permissions-transact-sql.md).  
  
<a name="Restrictions"></a> 

### Limitations

 Consider the following best practices that can make managing permissions easier.  
  
-   Grant permission to roles, instead of individual logins or users. When one individual is replaced by another, remove the departing individual from the role and add the new individual to the role. The many permissions that might be associated with the role will automatically be available to the new individual. If several people in an organization require the same permissions, adding each of them to the role will grant them the same permissions.  
  
-   Configure similar securables (tables, views, and procedures) to be owned by a schema, then grant permissions to the schema. For example, the payroll schema might own several tables, views, and stored procedures. By granting access to the schema, all the necessary permissions to perform the payroll function can be granted at the same time. For more information about what securables can be granted permissions, see [Securables](../securables.md).  

- In [!INCLUDE [fabric-sqldb](../../../includes/fabric-sqldb.md)], Microsoft Entra ID for database users is the only supported authentication method. Server-level roles and permissions are not available, only database-level. For more information, see [Authorization in SQL database in Microsoft Fabric](/fabric/database/sql/authorization).

## Related content

- [Principals (Database Engine)](principals-database-engine.md)
