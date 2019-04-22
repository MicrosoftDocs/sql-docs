---
title: "Links in CLR Integration Security | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: "database-engine"
ms.topic: "reference"
helpviewer_keywords: 
  - "table-access links [CLR integration]"
  - "security [CLR integration]"
  - "invocation links [CLR integration]"
  - "gated links [CLR integration]"
ms.assetid: 168efd01-d12e-4bdf-a1b3-0b5c76474eaf
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Links in CLR Integration Security
  This section describes how pieces of user code can call each other in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], either in [!INCLUDE[tsql](../../includes/tsql-md.md)] or in one of the managed languages. These relationships between objects are referred to as links.  
  
## Invocation Links  
 Invocation links correspond to a code invocation, either from a user calling an object (such as a [!INCLUDE[tsql](../../includes/tsql-md.md)] batch calling a stored procedure), or a common language runtime (CLR) stored procedure or function. Invocation links cause an `EXECUTE` permission on the callee to be checked.  
  
## Table-Access Links  
 Table access links correspond to retrieving or modifying values in a table, view, or a table-valued function. They are similar to invocation links, except that they have a finer-grained access control in terms of SELECT, INSERT, UPDATE, and DELETE permissions.  
  
## Gated Links  
 Gated links mean that during execution, permissions are not checked across the object relationship once it has been established. When there is a gated link between two objects (for example, object **x** and object **y**), permissions on object **y** and other objects accessed from object **y** are checked only at the creation time of object **x**. At the creation time of object **x**, `REFERENCE` permission is checked on **y** against the owner of **x**. At execution time, (for example, when someone calls object **x**), there are no permissions checked against **y** or other objects it references statically. At execution time, an appropriate permission will be checked against object **x** itself.  
  
 Gated links are always used in conjunction with a metadata dependency between two objects. This metadata dependency is a relationship established in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] catalogs that prevents an object from being dropped as long as another object depends on it.  
  
 Gated links are useful when it is not appropriate or manageable to give permissions to many dependent objects. Gated links are introduced between objects that define [!INCLUDE[tsql](../../includes/tsql-md.md)] entry points into CLR assemblies (for example, CLR procedures, triggers, functions, types, and aggregates) and the assemblies from which they are defined. Gated security against these objects implies that in order to invoke a [!INCLUDE[tsql](../../includes/tsql-md.md)] entry point defined in a CLR assembly, the caller only needs an appropriate permission on that [!INCLUDE[tsql](../../includes/tsql-md.md)] entry point. The caller is not required to have permissions on that assembly or any other assemblies it statically references. The permissions on the assembly are checked at creation time of the [!INCLUDE[tsql](../../includes/tsql-md.md)] entry point.  
  
## SQL Server Authorization-Based Security  
 The following are the basic rules behind the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security checks for invocations of and between CLR-based database objects; the first three rules define which permissions are checked and against which object; the fourth rule defines which execution context the permission is checked against.  
  
1.  All invocations require `EXECUTE` permission unless the invocations occur within the same object; this means that calls within the same assembly do not require any permission checks. The permission is checked at execution time.  
  
2.  Gated links require `REFERENCE` permission against the callee when the calling object is created. The permission is checked for the owner of the calling object when the object is created.  
  
3.  Table-access links require the corresponding `SELECT`, `INSERT`, `UPDATE`, or `DELETE` permission against the table or view being accessed.  
  
4.  The permission is checked against the current execution context. Procedures and functions can be created with an execution context that is different from the caller. Assemblies are always created with the execution context of the procedure, function, or trigger that is defined against it.  
  
## See Also  
 [CLR Integration Security](../../relational-databases/clr-integration/security/clr-integration-security.md)  
  
  
