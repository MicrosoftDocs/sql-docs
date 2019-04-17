---
title: "ALTER PROCEDURE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/01/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_PROCEDURE_TSQL"
  - "ALTER_PROC_TSQL"
  - "ALTER PROC"
  - "ALTER PROCEDURE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER PROCEDURE statement"
  - "stored procedure modifications [SQL Server]"
  - "modifying stored procedures"
  - "stored procedures [SQL Server], modifying"
ms.assetid: ed9b2f76-11ec-498d-a95e-75b490a75733
author: CarlRabeler
ms.author: carlrab
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# ALTER PROCEDURE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Modifies a previously created procedure that was created by executing the CREATE PROCEDURE statement in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions (Transact-SQL)](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
-- Syntax for SQL Server and Azure SQL Database
  
ALTER { PROC | PROCEDURE } [schema_name.] procedure_name [ ; number ]   
    [ { @parameter [ type_schema_name. ] data_type }   
        [ VARYING ] [ = default ] [ OUT | OUTPUT ] [READONLY]  
    ] [ ,...n ]   
[ WITH <procedure_option> [ ,...n ] ]  
[ FOR REPLICATION ]   
AS { [ BEGIN ] sql_statement [;] [ ...n ] [ END ] }  
[;]  
  
<procedure_option> ::=   
    [ ENCRYPTION ]  
    [ RECOMPILE ]  
    [ EXECUTE AS Clause ]  
```  
  
```  
-- Syntax for SQL Server CLR Stored Procedure  
  
ALTER { PROC | PROCEDURE } [schema_name.] procedure_name [ ; number ]   
    [ { @parameter [ type_schema_name. ] data_type }   
        [ = default ] [ OUT | OUTPUT ] [READONLY]  
    ] [ ,...n ]   
[ WITH EXECUTE AS Clause ]  
AS { EXTERNAL NAME assembly_name.class_name.method_name }  
[;]  
```  
  
```sql  
-- Syntax for Azure SQL Data Warehouse and Parallel Data Warehouse  
  
ALTER { PROC | PROCEDURE } [schema_name.] procedure_name  
    [ { @parameterdata_type } [= ] ] [ ,...n ]  
AS { [ BEGIN ] sql_statement [ ; ] [ ,...n ] [ END ] }  
[;]  
```  
  
## Arguments  
 *schema_name*  
 The name of the schema to which the procedure belongs.  
  
 *procedure_name*  
 The name of the procedure to change. Procedure names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
 **;** *number*  
 An existing optional integer that is used to group procedures of the same name so that they can be dropped together by using one DROP PROCEDURE statement.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)]  
  
 **@** *parameter*  
 A parameter in the procedure. Up to 2,100 parameters can be specified.  
  
 [ _type\_schema\_name_**.** ] _data\_type_  
 Is the data type of the parameter and the schema it belongs to.  
  
 For information about data type restrictions, see [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md).  
  
 VARYING  
 Specifies the result set supported as an output parameter. This parameter is constructed dynamically by the stored procedure and its contents can vary. Applies only to cursor parameters. This option is not valid for CLR procedures.  
  
 *default*  
 Is a default value for the parameter.  
  
 OUT | OUTPUT  
 Indicates that the parameter is a return parameter.  
  
 READONLY  
 Indicates that the parameter cannot be updated or modified within the body of the procedure. If the parameter type is a table-value type, READONLY must be specified.  
  
 RECOMPILE  
 Indicates that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] does not cache a plan for this procedure and the procedure is recompiled at run time.  
  
 ENCRYPTION  
 **Applies to**: SQL Server ( [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]) and [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)].  
  
 Indicates that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] will convert the original text of the ALTER PROCEDURE statement to an obfuscated format. The output of the obfuscation is not directly visible in any of the catalog views in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Users that have no access to system tables or database files cannot retrieve the obfuscated text. However, the text will be available to privileged users that can either access system tables over the [DAC port](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md) or directly access database files. Also, users that can attach a debugger to the server process can retrieve the original procedure from memory at runtime. For more information about accessing system metadata, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
 Procedures created with this option cannot be published as part of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication.  
  
 This option cannot be specified for common language runtime (CLR) stored procedures.  
  
> [!NOTE]  
>  During an upgrade, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses the obfuscated comments stored in **sys.sql_modules** to re-create procedures.  
  
 EXECUTE AS  
 Specifies the security context under which to execute the stored procedure after it is accessed.  
  
 For more information, see [EXECUTE AS Clause &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-clause-transact-sql.md).  
  
 FOR REPLICATION  

  
 Specifies that stored procedures that are created for replication cannot be executed on the Subscriber. A stored procedure created with the FOR REPLICATION option is used as a stored procedure filter and only executed during replication. Parameters cannot be declared if FOR REPLICATION is specified. This option is not valid for CLR procedures. The RECOMPILE option is ignored for procedures created with FOR REPLICATION.  
  
> [!NOTE]  
>  This option is not available in a contained database.  
  
 { [ BEGIN ] *sql_statement* [;] [ ...*n* ] [ END ] }  
 One or more [!INCLUDE[tsql](../../includes/tsql-md.md)] statements comprising the body of the procedure. You can use the optional BEGIN and END keywords to enclose the statements. For more information, see the Best Practices, General Remarks, and Limitations and Restrictions sections in [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md).  
  
 EXTERNAL NAME _assembly\_name_**.**_class\_name_**.**_method\_name_  
 **Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 Specifies the method of a [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] assembly for a CLR stored procedure to reference. *class_name* must be a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier and must exist as a class in the assembly. If the class has a namespace-qualified name uses a period (**.**) to separate namespace parts, the class name must be delimited by using brackets (**[]**) or quotation marks (**""**). The specified method must be a static method of the class.  
  
 By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot execute CLR code. You can create, modify, and drop database objects that reference common language runtime modules; however, you cannot execute these references in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] until you enable the [clr enabled option](../../database-engine/configure-windows/clr-enabled-server-configuration-option.md). To enable the option, use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md).  
  
> [!NOTE]  
>  CLR procedures are not supported in a contained database.  
  
## General Remarks  
 [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures cannot be modified to be CLR stored procedures and vice versa.  
  
 ALTER PROCEDURE does not change permissions and does not affect any dependent stored procedures or triggers. However, the current session settings for QUOTED_IDENTIFIER and ANSI_NULLS are included in the stored procedure when it is modified. If the settings are different from those in effect when stored procedure was originally created, the behavior of the stored procedure may change.  
  
 If a previous procedure definition was created using WITH ENCRYPTION or WITH RECOMPILE, these options are enabled only if they are included in ALTER PROCEDURE.  
  
 For more information about stored procedures, see [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md).  
  
## Security  
  
### Permissions  
 Requires **ALTER** permission on the procedure or requires membership in the **db_ddladmin** fixed database role.  
  
## Examples  
 The following example creates the `uspVendorAllInfo` stored procedure. This procedure returns the names of all the vendors that supply [!INCLUDE[ssSampleDBCoFull](../../includes/sssampledbcofull-md.md)], the products they supply, their credit ratings, and their availability. After this procedure is created, it is then modified to return a different result set.  
  
```  
  
IF OBJECT_ID ( 'Purchasing.uspVendorAllInfo', 'P' ) IS NOT NULL   
    DROP PROCEDURE Purchasing.uspVendorAllInfo;  
GO  
CREATE PROCEDURE Purchasing.uspVendorAllInfo  
WITH EXECUTE AS CALLER  
AS  
    SET NOCOUNT ON;  
    SELECT v.Name AS Vendor, p.Name AS 'Product name',   
      v.CreditRating AS 'Rating',   
      v.ActiveFlag AS Availability  
    FROM Purchasing.Vendor v   
    INNER JOIN Purchasing.ProductVendor pv  
      ON v.BusinessEntityID = pv.BusinessEntityID   
    INNER JOIN Production.Product p  
      ON pv.ProductID = p.ProductID   
    ORDER BY v.Name ASC;  
GO  
  
```  
  
 The following example alters the `uspVendorAllInfo` stored procedure. It removes the EXECUTE AS CALLER clause and modifies the body of the procedure to return only those vendors that supply the specified product. The `LEFT` and `CASE` functions customize the appearance of the result set.  
  
```  
USE AdventureWorks2012;  
GO  
ALTER PROCEDURE Purchasing.uspVendorAllInfo  
    @Product varchar(25)   
AS  
    SET NOCOUNT ON;  
    SELECT LEFT(v.Name, 25) AS Vendor, LEFT(p.Name, 25) AS 'Product name',   
    'Rating' = CASE v.CreditRating   
        WHEN 1 THEN 'Superior'  
        WHEN 2 THEN 'Excellent'  
        WHEN 3 THEN 'Above average'  
        WHEN 4 THEN 'Average'  
        WHEN 5 THEN 'Below average'  
        ELSE 'No rating'  
        END  
    , Availability = CASE v.ActiveFlag  
        WHEN 1 THEN 'Yes'  
        ELSE 'No'  
        END  
    FROM Purchasing.Vendor AS v   
    INNER JOIN Purchasing.ProductVendor AS pv  
      ON v.BusinessEntityID = pv.BusinessEntityID   
    INNER JOIN Production.Product AS p   
      ON pv.ProductID = p.ProductID   
    WHERE p.Name LIKE @Product  
    ORDER BY v.Name ASC;  
GO  
  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
 ```   
 Vendor               Product name  Rating    Availability  
-------------------- ------------- -------   ------------  
Proseware, Inc.      LL Crankarm   Average   No  
Vision Cycles, Inc.  LL Crankarm   Superior  Yes  
(2 row(s) affected)`  
```  

## See Also  
 [CREATE PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/create-procedure-transact-sql.md)   
 [DROP PROCEDURE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-procedure-transact-sql.md)   
 [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md)   
 [EXECUTE AS &#40;Transact-SQL&#41;](../../t-sql/statements/execute-as-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Stored Procedures &#40;Database Engine&#41;](../../relational-databases/stored-procedures/stored-procedures-database-engine.md)   
 [sys.procedures &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-procedures-transact-sql.md)  
  
  
