---
title: "Managing CLR Integration Assemblies | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: clr
ms.topic: "reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database objects [CLR integration], assemblies"
  - "common language runtime [SQL Server], assemblies"
  - "assemblies [CLR integration], managing"
ms.assetid: bdbbf325-14f6-460e-a35a-d3861d3c961e
author: rothja
ms.author: jroth
manager: craigg
---
# Managing CLR Integration Assemblies
  Managed code is compiled and then deployed in units called an assembly. An assembly is packaged as a DLL or executable (.exe) file. While an executable file can run on its own, a DLL must be hosted in an existing application. Managed DLL assemblies can be loaded into and hosted by [!INCLUDE[msCoName](../../../includes/ssnoversion-md.md)]. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database using the CREATE ASSEMBLY statement, before it can be loaded in the process and used. Assemblies can also be updated from a more recent version using the ALTER ASSEMBLY statement, or removed from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] using the DROP ASSEMBLY statement.  
  
 Assembly information is stored in the `sys.assembly_files` table in the database where the assembly has been installed. The `sys.assembly_files` table contains the following columns.  
  
|Column|Description|  
|------------|-----------------|  
|assembly_id|The identifier defined for the assembly. This number is assigned to all objects relating to the same assembly.|  
|name|The name of the object.|  
|file_id|A number identifying each object, with the first object associated with a given `assembly_id` being given the value of 1. If multiple objects are associated with the same `assembly_id`, then each subsequent `file_id` value is incremented by 1.|  
|content|The hexadecimal representation of the assembly or file.|  
  
## In This Section  
 [Creating an Assembly](creating-an-assembly.md)  
 Discusses creating SAFE, EXTERNAL_ACCESS, and UNSAFE CLR assemblies in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 [Altering an Assembly](altering-an-assembly.md)  
 Describes updating CLR assemblies in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
 [Dropping an Assembly](dropping-an-assembly.md)  
 Discusses dropping CLR assemblies from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## See Also  
 [CLR Integration Security](../security/clr-integration-security.md)   
 [CLR Integration Code Access Security](../security/clr-integration-code-access-security.md)  
  
  
