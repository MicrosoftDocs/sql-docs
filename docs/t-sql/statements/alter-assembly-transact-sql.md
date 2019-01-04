---
title: "ALTER ASSEMBLY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_ASSEMBLY_TSQL"
  - "ALTER ASSEMBLY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "assemblies [CLR integration], modifying"
  - "refreshing assemblies"
  - "assemblies [CLR integration], versioning"
  - "assemblies [CLR integration], adding files"
  - "modifying assemblies"
  - "adding files"
  - "ALTER ASSEMBLY statement"
ms.assetid: 87bca678-4e79-40e1-bb8b-bd5ed8f34853
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER ASSEMBLY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdbmi-xxxx-xxx-md.md)]

  Alters an assembly by modifying the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] catalog properties of an assembly. ALTER ASSEMBLY refreshes it to the latest copy of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] modules that hold its implementation and adds or removes files associated with it. Assemblies are created by using [CREATE ASSEMBLY](../../t-sql/statements/create-assembly-transact-sql.md).  

> [!WARNING]
>  CLR uses Code Access Security (CAS) in the .NET Framework, which is no longer supported as a security boundary. A CLR assembly created with `PERMISSION_SET = SAFE` may be able to access external system resources, call unmanaged code, and acquire sysadmin privileges. Beginning with [!INCLUDE[sssqlv14-md](../../includes/sssqlv14-md.md)], an `sp_configure` option called `clr strict security` is introduced to enhance the security of CLR assemblies. `clr strict security` is enabled by default, and treats `SAFE` and `EXTERNAL_ACCESS` assemblies as if they were marked `UNSAFE`. The `clr strict security` option can be disabled for backward compatibility, but this is not recommended. Microsoft recommends that all assemblies be signed by a certificate or asymmetric key with a corresponding login that has been granted `UNSAFE ASSEMBLY` permission in the master database. For more information, see [CLR strict security](../../database-engine/configure-windows/clr-strict-security.md).  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
ALTER ASSEMBLY assembly_name  
    [ FROM <client_assembly_specifier> | <assembly_bits> ]  
    [ WITH <assembly_option> [ ,...n ] ]  
    [ DROP FILE { file_name [ ,...n ] | ALL } ]  
    [ ADD FILE FROM   
    {   
        client_file_specifier [ AS file_name ]   
      | file_bits AS file_name   
    } [,...n ]   
    ] [ ; ]  
<client_assembly_specifier> :: =  
    '\\computer_name\share-name\[path\]manifest_file_name'  
  | '[local_path\]manifest_file_name'  
  
<assembly_bits> :: =  
    { varbinary_literal | varbinary_expression }  
  
<assembly_option> :: =  
    PERMISSION_SET = { SAFE | EXTERNAL_ACCESS | UNSAFE }   
  | VISIBILITY = { ON | OFF }  
  | UNCHECKED DATA  
  
```  
  
## Arguments  
 *assembly_name*  
 Is the name of the assembly you want to modify. *assembly_name* must already exist in the database.  
  
 FROM \<client_assembly_specifier> | \<assembly_bits>  
 Updates an assembly to the latest copy of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] modules that hold its implementation. This option can only be used if there are no associated files with the specified assembly.  
  
 \<client_assembly_specifier> specifies the network or local location where the assembly being refreshed is located. The network location includes the computer name, the share name and a path within that share. *manifest_file_name* specifies the name of the file that contains the manifest of the assembly.  

> [!IMPORTANT]
> Azure SQL Database does not support refereencing a file.
  
 \<assembly_bits> is the binary value for the assembly.  
  
 Separate ALTER ASSEMBLY statements must be issued for any dependent assemblies that also require updating.  
  
 PERMISSION_SET = { SAFE | EXTERNAL_ACCESS | UNSAFE }   
> [!IMPORTANT]
>  The `PERMISSION_SET` option is affected by the `clr strict security` option, described in the opening warning. When `clr strict security` is enabled, all assemblies are treated as `UNSAFE`.  
>  Specifies the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] code access permission set property of the assembly. For more information about this property, see [CREATE ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/create-assembly-transact-sql.md).  
> 
> [!NOTE]
>  The EXTERNAL_ACCESS and UNSAFE options are not available in a contained database.  
  
 VISIBILITY = { ON | OFF }  
 Indicates whether the assembly is visible for creating common language runtime (CLR) functions, stored procedures, triggers, user-defined types, and user-defined aggregate functions against it. If set to OFF, the assembly is intended to be called only by other assemblies. If there are existing CLR database objects already created against the assembly, the visibility of the assembly cannot be changed. Any assemblies referenced by *assembly_name* are uploaded as not visible by default.  
  
 UNCHECKED DATA  
 By default, ALTER ASSEMBLY fails if it must verify the consistency of individual table rows. This option allows postponing the checks until a later time by using DBCC CHECKTABLE. If specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] executes the ALTER ASSEMBLY statement even if there are tables in the database that contain the following:  
  
-   Persisted computed columns that either directly or indirectly reference methods in the assembly, through [!INCLUDE[tsql](../../includes/tsql-md.md)] functions or methods.  
  
-   CHECK constraints that directly or indirectly reference methods in the assembly.  
  
-   Columns of a CLR user-defined type that depend on the assembly, and the type implements a **UserDefined** (non-**Native**) serialization format.  
  
-   Columns of a CLR user-defined type that reference views created by using WITH SCHEMABINDING.  
  
 If any CHECK constraints are present, they are disabled and marked untrusted. Any tables containing columns depending on the assembly are marked as containing unchecked data until those tables are explicitly checked.  
  
 Only members of the **db_owner** and **db_ddlowner** fixed database roles can specify this option.  
  
 Requires the **ALTER ANY SCHEMA** permission to specify this option.  
  
 For more information, see [Implementing Assemblies](../../relational-databases/clr-integration/assemblies-implementing.md).  
  
 [ DROP FILE { *file_name*[ **,**_...n_] | ALL } ]  
 Removes the file name associated with the assembly, or all files associated with the assembly, from the database. If used with ADD FILE that follows, DROP FILE executes first. This lets you to replace a file with the same file name.  
  
> [!NOTE]  
>  This option is not available in a contained database or Azure SQL Database.  
  
 [ ADD FILE FROM { *client_file_specifier* [ AS *file_name*] | *file_bits*AS *file_name*}  
 Uploads a file to be associated with the assembly, such as source code, debug files or other related information, into the server and made visible in the **sys.assembly_files** catalog view. *client_file_specifier* specifies the location from which to upload the file. *file_bits* can be used instead to specify the list of binary values that make up the file. *file_name* specifies the name under which the file should be stored in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. *file_name* must be specified if *file_bits* is specified, and is optional if *client_file_specifier* is specified. If *file_name* is not specified, the file_name part of *client_file_specifier* is used as *file_name*.  
  
> [!NOTE]  
>  This option is not available in a contained database or Azure SQL Database.  
  
## Remarks  
 ALTER ASSEMBLY does not disrupt currently running sessions that are running code in the assembly being modified. Current sessions complete execution by using the unaltered bits of the assembly.  
  
 If the FROM clause is specified, ALTER ASSEMBLY updates the assembly with respect to the latest copies of the modules provided. Because there might be CLR functions, stored procedures, triggers, data types, and user-defined aggregate functions in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are already defined against the assembly, the ALTER ASSEMBLY statement rebinds them to the latest implementation of the assembly. To accomplish this rebinding, the methods that map to CLR functions, stored procedures, and triggers must still exist in the modified assembly with the same signatures. The classes that implement CLR user-defined types and user-defined aggregate functions must still satisfy the requirements for being a user-defined type or aggregate.  
  
> [!CAUTION]  
>  If WITH UNCHECKED DATA is not specified, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tries to prevent ALTER ASSEMBLY from executing if the new assembly version affects existing data in tables, indexes, or other persistent sites. However, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not guarantee that computed columns, indexes, indexed views or expressions will be consistent with the underlying routines and types when the CLR assembly is updated. Use caution when you execute ALTER ASSEMBLY to make sure that there is not a mismatch between the result of an expression and a value based on that expression stored in the assembly.  
  
 ALTER ASSEMBLY changes the assembly version. The culture and public key token of the assembly remain the same.  
  
 ALTER ASSEMBLY statement cannot be used to change the following:  
  
-   The signatures of CLR functions, aggregate functions, stored procedures, and triggers in an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that reference the assembly. ALTER ASSEMBLY fails when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot rebind [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] database objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the new version of the assembly.  
  
-   The signatures of methods in the assembly that are called from other assemblies.  
  
-   The list of assemblies that depend on the assembly, as referenced in the **DependentList** property of the assembly.  
  
-   The indexability of a method, unless there are no indexes or persisted computed columns depending on that method, either directly or indirectly.  
  
-   The **FillRow** method name attribute for CLR table-valued functions.  
  
-   The **Accumulate** and **Terminate** method signature for user-defined aggregates.  
  
-   System assemblies.  
  
-   Assembly ownership. Use [ALTER AUTHORIZATION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-authorization-transact-sql.md) instead.  
  
 Additionally, for assemblies that implement user-defined types, ALTER ASSEMBLY can be used for making only the following changes:  
  
-   Modifying public methods of the user-defined type class, as long as signatures or attributes are not changed.  
  
-   Adding new public methods.  
  
-   Modifying private methods in any way.  
  
 Fields contained within a native-serialized user-defined type, including data members or base classes, cannot be changed by using ALTER ASSEMBLY. All other changes are unsupported.  
  
 If ADD FILE FROM is not specified, ALTER ASSEMBLY drops any files associated with the assembly.  
  
 If ALTER ASSEMBLY is executed without the UNCHECKED data clause, checks are performed to verify that the new assembly version does not affect existing data in tables. Depending on the amount of data that needs to be checked, this may affect performance.  
  
## Permissions  
 Requires ALTER permission on the assembly. Additional requirements are as follows:  
  
-   To alter an assembly whose existing permission set is EXTERNAL_ACCESS, requires**EXTERNAL ACCESS ASSEMBLY**permission on the server.  
  
-   To alter an assembly whose existing permission set is UNSAFE requires **UNSAFE ASSEMBLY** permission on the server.  
  
-   To change the permission set of an assembly to EXTERNAL_ACCESS, requires**EXTERNAL ACCESS ASSEMBLY** permission on the server.  
  
-   To change the permission set of an assembly to UNSAFE, requires **UNSAFE ASSEMBLY** permission on the server.  
  
-   Specifying WITH UNCHECKED DATA, requires **ALTER ANY SCHEMA** permission.  


### Permissions with CLR strict security    
The following permissions required to alter a CLR assembly when `CLR strict security` is enabled:

- The user must have the `ALTER ASSEMBLY` permission  
- And one of the following conditions must also be true:  
  - The assembly is signed with a certificate or asymmetric key that has a corresponding login with the `UNSAFE ASSEMBLY` permission on the server. Signing the assembly is recommended.  
  - The database has the `TRUSTWORTHY` property set to `ON`, and the database is owned by a login that has the `UNSAFE ASSEMBLY` permission on the server. This option is not recommended.  
  
  
 For more information about assembly permission sets, see [Designing Assemblies](../../relational-databases/clr-integration/assemblies-designing.md).  
  
## Examples  
  
### A. Refreshing an assembly  
 The following example updates assembly `ComplexNumber` to the latest copy of the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] modules that hold its implementation.  
  
> [!NOTE]  
>  Assembly `ComplexNumber` can be created by running the UserDefinedDataType sample scripts. For more information, see [User Defined Type](https://msdn.microsoft.com/library/a9b75f36-d7f5-47f7-94d6-b4448c6a2191).  
  
 ```
 ALTER ASSEMBLY ComplexNumber 
 FROM 'C:\Program Files\Microsoft SQL Server\130\Tools\Samples\1033\Engine\Programmability\CLR\UserDefinedDataType\CS\ComplexNumber\obj\Debug\ComplexNumber.dll' 
  ```

> [!IMPORTANT]
> Azure SQL Database does not support refereencing a file.

### B. Adding a file to associate with an assembly  
 The following example uploads the source code file `Class1.cs` to be associated with assembly `MyClass`. This example assumes assembly `MyClass` is already created in the database.  
  
```  
ALTER ASSEMBLY MyClass   
ADD FILE FROM 'C:\MyClassProject\Class1.cs';  
```  

> [!IMPORTANT]
> Azure SQL Database does not support refereencing a file.

### C. Changing the permissions of an assembly  
 The following example changes the permission set of assembly `ComplexNumber` from SAFE to `EXTERNAL ACCESS`.  
  
```  
ALTER ASSEMBLY ComplexNumber WITH PERMISSION_SET = EXTERNAL_ACCESS;  
```  
  
## See Also  
 [CREATE ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/create-assembly-transact-sql.md)   
 [DROP ASSEMBLY &#40;Transact-SQL&#41;](../../t-sql/statements/drop-assembly-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)  
  
  
