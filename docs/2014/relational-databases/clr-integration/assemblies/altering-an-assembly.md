---
title: "Altering an Assembly | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
helpviewer_keywords: 
  - "assemblies [CLR integration], modifying"
  - "permissions [CLR integration]"
  - "altering assemblies"
  - "ALTER ASSEMBLY statement"
ms.assetid: 9e765fbd-f339-473c-8537-22f478e79696
author: rothja
ms.author: jroth
manager: craigg
---
# Altering an Assembly
  Assemblies that have been registered in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can be updated from a more recent version using the ALTER ASSEMBLY statement. To update an assembly, use the ALTER ASSEMBLY statement with the following syntax:  
  
```  
ALTER ASSEMBLY SQLCLRTest  
FROM 'C:\MyDBApp\SQLCLRTest.dll'  
```  
  
 ALTER ASSEMBLY does not disrupt currently running processes that are using the assembly; the processes continue executing with the unaltered assembly. ALTER ASSEMBLY cannot be used to change the signatures of common language runtime (CLR) functions, aggregate functions, stored procedures, and triggers. New public methods can be added to the assembly, private methods can be modified in any way, and public methods can be modified as long as signatures or attributes are not changed. Fields that are contained within a native-serialized user-defined type, including data members or base classes, cannot be changed by using ALTER ASSEMBLY. All other changes are unsupported. For more information, see [ALTER ASSEMBLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-assembly-transact-sql).  
  
## Changing the Permission Set of an Assembly  
 The permission set of an assembly can also be changed using the ALTER ASSEMBLY statement. The following statement changes the permission set of the SQLCLRTest assembly to `EXTERNAL_ACCESS`.  
  
```  
ALTER ASSEMBLY SQLCLRTest  
WITH PERMISSION_SET = EXTERNAL_ACCESS   
```  
  
 If the permission set of an assembly is being changed from `SAFE` to `EXTERNAL_ACCESS` or `UNSAFE`, an asymmetric key and corresponding login with `EXTERNAL ACCESS ASSEMBLY` permission or `UNSAFE ASSEMBLY` permission for the assembly must first be created. For more information, see [Creating an Assembly](creating-an-assembly.md).  
  
## Adding the Source Code of an Assembly  
 The ADD FILE clause in the ALTER ASSEMBLY syntax is not present in CREATE ASSEMBLY. You can use it to add source code or any other files associated with an assembly. The files are copied from their original locations and stored in system tables in the database. This ensures that you always have source code or other files on hand should you ever need to re-create or document the current version of the UDT.  
  
 The following statement adds the Point.cs class source code for the Point UDT. This copies the text contained in the Point.cs file and stores it in the database under the name "PointSource".  
  
 `ALTER ASSEMBLY Point`  
  
 `ADD FILE FROM 'C:\Projects\Point\Point.cs' AS PointSource`  
  
## See Also  
 [Managing CLR Integration Assemblies](managing-clr-integration-assemblies.md)   
 [Creating an Assembly](creating-an-assembly.md)   
 [Dropping an Assembly](dropping-an-assembly.md)   
 [ALTER ASSEMBLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-assembly-transact-sql)  
  
  
