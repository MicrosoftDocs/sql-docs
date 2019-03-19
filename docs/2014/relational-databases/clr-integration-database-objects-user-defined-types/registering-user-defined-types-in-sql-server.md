---
title: "Registering User-Defined Types in SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "UDTs [CLR integration], maintaining"
  - "user-defined types [CLR integration], maintaining"
  - "dependencies [CLR integration]"
  - "deploying user-defined types [CLR integration]"
  - "CurrencyConversion function"
  - "user-defined types [CLR integration], deploying"
  - "Transact-SQL deploying UDTs"
  - "assemblies [CLR integration], user-defined types"
  - "cross-database UDT support"
  - "CREATE ASSEMBLY statement"
  - "DROP TYPE statement"
  - "Currency UDT"
  - "CREATE TYPE statement"
  - "registering user-defined types"
  - "UDTs [CLR integration], deploying"
  - "removing user-defined types"
  - "user-defined types [CLR integration], registering"
  - "ALTER ASSEMBLY statement"
  - "UDTs [CLR integration], registering"
  - "ADD FILE clause"
ms.assetid: f7da3e92-e407-4f0b-b3a3-f214e442b37d
author: rothja
ms.author: jroth
manager: craigg
---
# Registering User-Defined Types in SQL Server
  In order to use a user-defined type (UDT) in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must register it. Registering a UDT involves registering the assembly and creating the type in the database in which you wish to use it. UDTs are scoped to a single database, and cannot be used in multiple databases unless the identical assembly and UDT are registered with each database. Once the UDT assembly is registered and the type created, you can use the UDT in [!INCLUDE[tsql](../../includes/tsql-md.md)] and in client code. For more information, see [CLR User-Defined Types](clr-user-defined-types.md).  
  
## Using Visual Studio to Deploy UDTs  
 The easiest way to deploy your UDT is by using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Visual Studio. For more complex deployment scenarios and the greatest flexibility, however, use [!INCLUDE[tsql](../../includes/tsql-md.md)] as discussed later in this topic.  
  
 Follow these steps to create and deploy a UDT using Visual Studio:  
  
1.  Create a new **Database** project in the **Visual Basic** or **Visual C#** language nodes.  
  
2.  Add a reference to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that will contain the UDT.  
  
3.  Add a **User-Defined Type** class.  
  
4.  Write code to implement the UDT.  
  
5.  From the **Build** menu, select **Deploy**. This registers the assembly and creates the type in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.  
  
## Using Transact-SQL to Deploy UDTs  
 The [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE ASSEMBLY syntax is used to register the assembly in the database in which you wish to use the UDT. It is stored internally in database system tables, not externally in the file system. If the UDT is dependent on external assemblies, they too must be loaded into the database. The CREATE TYPE statement is used to create the UDT in the database in which it is to be used. For more information, see [CREATE ASSEMBLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-assembly-transact-sql) and [CREATE TYPE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-type-transact-sql).  
  
### Using CREATE ASSEMBLY  
 The CREATE ASSEMBLY syntax registers the assembly in the database in which you wish to use the UDT. Once the assembly is registered, it has no dependencies.  
  
 Creating multiple versions of the same assembly in a given database is not allowed. However, it is possible to create multiple versions of the same assembly based on culture in a given database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] distinguishes multiple culture versions of an assembly by different names as registered in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see "Creating and Using Strong-Named Assemblies" in the .NET Framework SDK.  
  
 When CREATE ASSEMBLY is executed with the SAFE or EXTERNAL_ACCESS permission sets, the assembly is checked to make sure that it is verifiable and type safe. If you omit specifying a permission set, SAFE is assumed. Code with the UNSAFE permission set is not checked. For more information about assembly permission sets, see [Designing Assemblies](../../relational-databases/clr-integration/assemblies-designing.md).  
  
#### Example  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement registers the Point assembly in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in the **AdventureWorks** database, with the SAFE permission set. If the WITH PERMISSION_SET clause is omitted, the assembly is registered with the SAFE permission set.  
  
```  
USE AdventureWorks;  
CREATE ASSEMBLY Point  
FROM '\\ShareName\Projects\Point\bin\Point.dll'   
WITH PERMISSION_SET = SAFE;  
```  
  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement registers the assembly using *<assembly_bits>* argument in the FROM clause. This `varbinary` value represents the file as a stream of bytes.  
  
```  
USE AdventureWorks;  
CREATE ASSEMBLY Point  
FROM 0xfeac4 ... 21ac78  
```  
  
### Using CREATE TYPE  
 Once the assembly is loaded into the database, you can then create the type using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CREATE TYPE statement. This adds the type to the list of available types for that database. The type has database scope and the type can only be used in the database in which it was created. If the UDT already exists in the database, the CREATE TYPE statement fails with an error.  
  
> [!NOTE]  
>  The CREATE TYPE syntax is also used for creating native [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] alias data types, and is intended to replace `sp_addtype` as a means of creating alias data types. Some of the optional arguments in the CREATE TYPE syntax refer to creating UDTs, and are not applicable to creating alias data types (such as base type).  
  
 For more information, see [CREATE TYPE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-type-transact-sql).  
  
#### Example  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] statement creates the `Point` type. The EXTERNAL NAME is specified using the two-part naming syntax of *AssemblyName*.*UDTName*.  
  
```  
CREATE TYPE dbo.Point   
EXTERNAL NAME Point.[Point];  
```  
  
## Removing a UDT from the Database  
 The DROP TYPE statement removes a UDT from the current database. Once a UDT is dropped, you can use the DROP ASSEMBLY statement to drop the assembly from the database.  
  
 The DROP TYPE statement does not execute in the following situations:  
  
-   Tables in the database that contain columns defined using the UDT.  
  
-   Functions, stored procedures, or triggers that use variables or parameters of the UDT, created in the database with the WITH SCHEMABINDING clause.  
  
### Example  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] must execute in the following order. First the table which references the `Point` UDT must be dropped, then the type, and finally the assembly.  
  
```  
DROP TABLE dbo.Points;  
DROP TYPE dbo.Point;  
DROP ASSEMBLY Point;  
```  
  
### Finding UDT Dependencies  
 If there are dependent objects, such as tables with UDT column definitions, the DROP TYPE statement fails. It also fails if there are functions, stored procedures, or triggers created in the database using the WITH SCHEMABINDING clause, if these routines use variables or parameters of the user-defined type. You must first drop all dependent objects, and then execute the DROP TYPE statement.  
  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] query locates all of the columns and parameters that use a UDT in the **AdventureWorks** database.  
  
```  
USE Adventureworks;  
SELECT o.name AS major_name, o.type_desc AS major_type_desc  
     , c.name AS minor_name, c.type_desc AS minor_type_desc  
     , at.assembly_class  
  FROM (  
        SELECT object_id, name, user_type_id, 'SQL_COLUMN' AS type_desc  
          FROM sys.columns  
     UNION ALL  
        SELECT object_id, name, user_type_id, 'SQL_PROCEDURE_PARAMETER'  
          FROM sys.parameters  
     ) AS c  
  JOIN sys.objects AS o  
    ON o.object_id = c.object_id  
  JOIN sys.assembly_types AS at  
    ON at.user_type_id = c.user_type_id;  
```  
  
## Maintaining UDTs  
 You cannot modify a UDT once it is created in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, although you can alter the assembly on which the type is based. In most cases, you must remove the UDT from the database with the [!INCLUDE[tsql](../../includes/tsql-md.md)] DROP TYPE statement, make changes to the underlying assembly, and reload it using the ALTER ASSEMBLY statement. You then need to re-create the UDT and any dependent objects.  
  
### Example  
 The ALTER ASSEMBLY statement is used after you have made changes to the source code in your UDT assembly and recompiled it. It copies the .dll file to the server and rebinds to the new assembly. For the complete syntax, see [ALTER ASSEMBLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-assembly-transact-sql).  
  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] ALTER ASSEMBLY statement reloads the Point.dll assembly from the specified location on disk.  
  
```  
ALTER ASSEMBLY Point  
FROM '\\Projects\Point\bin\Point.dll'  
```  
  
### Using ALTER ASSEMBLY to Add Source Code  
 The ADD FILE clause in the ALTER ASSEMBLY syntax is not present in CREATE ASSEMBLY. You can use it to add source code or any other files associated with an assembly. The files are copied from their original locations and stored in system tables in the database. This ensures that you always have source code or other files on hand should you ever need to re-create or document the current version of the UDT.  
  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] ALTER ASSEMBLY statement adds the Point.cs class source code for the `Point` UDT. This copies the text contained in the Point.cs file and stores it in the database under the name "PointSource".  
  
```  
ALTER ASSEMBLY Point  
ADD FILE FROM '\\Projects\Point\Point.cs' AS PointSource;  
```  
  
 Assembly information is stored in the **sys.assembly_files** table in the database where the assembly has been installed. The **sys.assembly_files** table contains the following columns.  
  
 **assembly_id**  
 The identifier defined for the assembly. This number is assigned to all objects relating to the same assembly.  
  
 **name**  
 The name of the object.  
  
 **file_id**  
 A number identifying each object, with the first object associated with a given **assembly_id** being given the value of 1. If there are multiple objects associated with the same **assembly_id**, then each subsequent **file_id** value is incremented by 1.  
  
 **content**  
 The hexadecimal representation of the assembly or file.  
  
 You can use the CAST or CONVERT function to convert the contents of the **content** column to readable text. The following query converts the contents of the Point.cs file to readable text, using the name in the WHERE clause to restrict the result set to a single row.  
  
```  
SELECT CAST(content AS varchar(8000))   
  FROM sys.assembly_files   
  WHERE name='PointSource';  
```  
  
 If you copy and paste the results in to a text editor, you will see that the line breaks and spaces that existed in the original have been preserved.  
  
## Managing UDTs and Assemblies  
 When planning your implementation of UDTs, consider which methods are needed in the UDT assembly itself, and which methods should be created in separate assemblies and implemented as user-defined functions or stored procedures. Separating methods into separate assemblies allows you to update code without affecting data that may be stored in a UDT column in a table. You can modify UDT assemblies without dropping UDT columns and other dependent objects only when the new definition can read the former values and the signature of the type does not change.  
  
 Separating procedural code that may change from the code required to implement the UDT greatly simplifies maintenance. Including only code that is necessary for the UDT to function, and keeping your UDT definitions as simple as possible, reduces the risk that the UDT itself may need to be dropped from the database for code revisions or bug fixes.  
  
### The Currency UDT and Currency Conversion Function  
 The **Currency** UDT in the **AdventureWorks** sample database provides a useful example of the recommended way to structure a UDT and its associated functions. The **Currency** UDT is used for handling money based on the monetary system of a particular culture, and allows for storage of different currency types, such as dollars, euros, and so forth. The UDT class exposes a culture name as a string, and an amount of money as a `decimal` data type. All of the necessary serialization methods are contained within the assembly defining the class. The function that implements currency conversion from one culture to another is implemented as an external function named **ConvertCurrency**, and this function is located in a separate assembly. The **ConvertCurrency** function does its work by retrieving the conversion rate from a table in the **AdventureWorks** database. If the source of the conversion rates should ever change, or if there should be any other changes to the existing code, the assembly can be easily modified without affecting the **Currency** UDT.  
  
 The code listing for the **Currency** UDT and **ConvertCurrency** functions can be found by installing the common language runtime (CLR) samples.  
  
### Using UDTs Across Databases  
 UDTs are by definition scoped to a single database. Therefore, a UDT defined in one database cannot be used in a column definition in another database. In order to use UDTs in multiple databases, you must execute the CREATE ASSEMBLY and CREATE TYPE statements in each database on identical assemblies. Assemblies are considered identical if they have the same name, strong name, culture, version, permission set, and binary contents.  
  
 Once the UDT is registered and accessible in both databases, you can convert a UDT value from one database for use in another. Identical UDTs can be used across databases in the following scenarios:  
  
-   Calling stored procedure defined in different databases.  
  
-   Querying tables defined in different databases.  
  
-   Selecting UDT data from one database table UDT column and inserting it into a second database with an identical UDT column.  
  
 In these situations, any conversion required by the server occurs automatically. You are not able to perform the conversions explicitly using the [!INCLUDE[tsql](../../includes/tsql-md.md)] CAST or CONVERT functions.  
  
 Note that you do not need to take any action for using UDTs when [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] creates work tables in the **tempdb** system database. This includes the handling of cursors, table variables, and user-defined table-valued functions that include UDTs and that transparently make use of **tempdb**. However, if you explicitly create a temporary table in **tempdb** that defines a UDT column, then the UDT must be registered in **tempdb** the same way as for a user database.  
  
## See Also  
 [CLR User-Defined Types](clr-user-defined-types.md)  
  
  
