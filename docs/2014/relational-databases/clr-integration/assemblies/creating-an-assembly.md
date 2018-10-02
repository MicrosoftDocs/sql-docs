---
title: "Creating an Assembly | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "creating assemblies"
  - "UNSAFE assemblies"
  - "CREATE ASSEMBLY statement"
  - "SAFE assemblies"
  - "EXTERNAL_ACCESS assemblies"
  - "assemblies [CLR integration], creating"
ms.assetid: a2bc503d-b6b2-4963-8beb-c11c323f18e0
author: rothja
ms.author: jroth
manager: craigg
---
# Creating an Assembly
  Managed database objects, such as stored procedures or triggers, are compiled and then deployed in units called an assembly. Managed DLL assemblies must be registered in [!INCLUDE[msCoName](../../../includes/ssnoversion-md.md)] before the functionality the assembly provides can be used. To register an assembly in a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, use the CREATE ASSEMBLY statement. This topic discusses how to register an assembly in a database using the CREATE ASSEMBLY statement, and how to specify the security settings for the assembly.  
  
## The CREATE ASSEMBLY Statement  
 The CREATE ASSEMBLY statement is used to create an assembly in a database. Here is an example:  
  
```  
CREATE ASSEMBLY SQLCLRTest  
FROM 'C:\MyDBApp\SQLCLRTest.dll';  
```  
  
 The FROM clause specifies the pathname of the assembly to create. This path can either be a Universal Naming Convention (UNC) path or a physical file path that is local to the machine.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not allow registering different versions of an assembly with the same name, culture and public key.  
  
 It is possible to create assemblies that reference other assemblies. When an assembly is created in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] also creates the assemblies referenced by the root-level assembly, if the referenced assemblies are not already created into the database.  
  
 Database users or user roles are given permissions to create, and thereby own, assemblies in a database. In order to create assemblies, the database user or role should have the CREATE ASSEMBLY permission.  
  
 An assembly can only succeed in referencing other assemblies if:  
  
-   The assembly that is called or referenced is owned by the same user or role.  
  
-   The assembly that is called or referenced was created in the same database.  
  
## Specifying Security When Creating Assemblies  
 When creating an assembly into a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, you can specify one of three different levels of security in which your code can run: `SAFE`, `EXTERNAL_ACCESS`, or `UNSAFE`. When the `CREATE ASSEMBLY` statement is run, certain checks are performed on the code assembly which may cause the assembly to fail to register on the server. For more information, see the Impersonation sample on [CodePlex](http://msftengprodsamples.codeplex.com/).  
  
 `SAFE` is the default permission set and works for the majority of scenarios. To specify a given security level, you modify the syntax of the CREATE ASSEMBLY statement as follows:  
  
```  
CREATE ASSEMBLY SQLCLRTest  
FROM 'C:\MyDBApp\SQLCLRTest.dll'  
WITH PERMISSION_SET = SAFE;  
```  
  
 It is also possible to create an assembly with the `SAFE` permission set by simply omitting the third line of code above:  
  
```  
CREATE ASSEMBLY SQLCLRTest  
FROM 'C:\MyDBApp\SQLCLRTest.dll';  
```  
  
 When code in an assembly runs under the `SAFE` permission set, it can only do computation and data access within the server through the in-process managed provider.  
  
### Creating EXTERNAL_ACCESS and UNSAFE Assemblies  
 `EXTERNAL_ACCESS` addresses scenarios in which the code needs to access resources outside the server, such as files, network, registry, and environment variables. Whenever the server accesses an external resource, it impersonates the security context of the user calling the managed code.  
  
 `UNSAFE` code permission is for those situations in which an assembly is not verifiably safe or requires additional access to restricted resources, such as the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Win32 API.  
  
 To create an `EXTERNAL_ACCESS` or `UNSAFE` assembly in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], one of the following two conditions must be met:  
  
1.  The assembly is strong name signed or Authenticode signed with a certificate. This strong name (or certificate) is created inside [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] as an asymmetric key (or certificate), and has a corresponding login with `EXTERNAL ACCESS ASSEMBLY` permission (for external access assemblies) or `UNSAFE ASSEMBLY` permission (for unsafe assemblies).  
  
2.  The database owner (DBO) has `EXTERNAL ACCESS ASSEMBLY` (for `EXTERNAL ACCESS` assemblies) or `UNSAFE ASSEMBLY` (for `UNSAFE` assemblies) permission, and the database has the [TRUSTWORTHY Database Property](../../security/trustworthy-database-property.md) set to `ON`.  
  
 The two conditions listed above are also checked at assembly load time (which includes execution). At least one of the conditions must be met in order to load the assembly.  
  
 We recommend that the [TRUSTWORTHY Database Property](../../security/trustworthy-database-property.md) on a database not be set to `ON` only to run common language runtime (CLR) code in the server process. Instead, we recommend that an asymmetric key be created from the assembly file in the master database. A login mapped to this asymmetric key must then be created, and the login must be granted `EXTERNAL ACCESS ASSEMBLY` or `UNSAFE ASSEMBLY` permission.  
  
 The following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements before running the CREATE ASSEMBLY statement.  
  
```  
USE master;   
GO    
  
CREATE ASYMMETRIC KEY SQLCLRTestKey FROM EXECUTABLE FILE = 'C:\MyDBApp\SQLCLRTest.dll'     
CREATE LOGIN SQLCLRTestLogin FROM ASYMMETRIC KEY SQLCLRTestKey     
GRANT EXTERNAL ACCESS ASSEMBLY TO SQLCLRTestLogin;   
GO   
```  
  
> [!NOTE]  
>  You must create a new login to associate with the asymmetric key. This login is only used to grant permissions; it does not have to be associated with a user, or used within the application.  
  
 To create an `EXTERNAL ACCESS` assembly, the creator needs to have `EXTERNAL ACCESS` permission. This is specified when creating the assembly:  
  
```  
CREATE ASSEMBLY SQLCLRTest  
FROM 'C:\MyDBApp\SQLCLRTest.dll'  
WITH PERMISSION_SET = EXTERNAL_ACCESS;  
```  
  
 The following [!INCLUDE[tsql](../../../includes/tsql-md.md)] statements before running the CREATE ASSEMBLY statement.  
  
```  
USE master;   
GO    
  
CREATE ASYMMETRIC KEY SQLCLRTestKey FROM EXECUTABLE FILE = 'C:\MyDBApp\SQLCLRTest.dll';     
CREATE LOGIN SQLCLRTestLogin FROM ASYMMETRIC KEY SQLCLRTestKey ;    
GRANT UNSAFE ASSEMBLY TO SQLCLRTestLogin ;  
GO  
```  
  
 To specify that an assembly loads with `UNSAFE` permission, you specify the `UNSAFE` permission set when loading the assembly into the server:  
  
```  
CREATE ASSEMBLY SQLCLRTest  
FROM 'C:\MyDBApp\SQLCLRTest.dll'  
WITH PERMISSION_SET = UNSAFE;  
```  
  
 For more details about the permissions for each of the settings, see [CLR Integration Security](../security/clr-integration-security.md).  
  
## See Also  
 [Managing CLR Integration Assemblies](managing-clr-integration-assemblies.md)   
 [Altering an Assembly](altering-an-assembly.md)   
 [Dropping an Assembly](dropping-an-assembly.md)   
 [CLR Integration Code Access Security](../security/clr-integration-code-access-security.md)   
 [TRUSTWORTHY Database Property](../../security/trustworthy-database-property.md)   
 [Allowing Partially Trusted Callers](../../../database-engine/dev-guide/allowing-partially-trusted-callers.md)  
  
  
