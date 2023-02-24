---
title: "Impersonation and Credentials for Connections"
description: In SQL Server CLR integration, you may want to impersonate the caller in Windows Authentication by using the SqlContext.WindowsIdentity property.
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: clr
ms.topic: "reference"
helpviewer_keywords:
  - "impersonation [CLR integration]"
  - "security [CLR integration]"
  - "database objects [CLR integration], connections"
  - "connections [CLR integration]"
  - "authentication [CLR integration]"
  - "user impersonation [CLR integration]"
  - "credentials [CLR integration]"
  - "database objects [CLR integration], security"
ms.assetid: 293dce7d-1db2-4657-992f-8c583d6e9ebb
---
# Impersonation and Credentials for Connections
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  In the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] common language runtime (CLR) integration, using Windows Authentication is complex, but is more secure than using SQL Server Authentication. When using Windows Authentication, keep in mind the following considerations.  
  
 By default, a SQL Server process that connects out to Windows acquires the security context of the SQL Server Windows service account. But it is possible to map a CLR function to a proxy identity, so that its outbound connections have a different security context than that of the Windows service account.  
  
 In some cases, you may want to impersonate the caller by using the **SqlContext.WindowsIdentity** property instead of running as the service account. The **WindowsIdentity** instance represents the identity of the client that invoked the calling code, and is only available when the client used Windows Authentication. After you have obtained the **WindowsIdentity** instance, you can call **Impersonate** to change the security token of the thread, and then open ADO.NET connections on behalf of the client.  
  
 After you call SQLContext.WindowsIdentity.Impersonate, you cannot access local data and you cannot access system data. To access data again, you have to call WindowsImpersonationContext.Undo.  
  
 The following example shows how to impersonate the caller by using the **SqlContext.WindowsIdentity** property.  
  
 Visual C#  
  
```  
WindowsIdentity clientId = null;  
WindowsImpersonationContext impersonatedUser = null;  
  
clientId = SqlContext.WindowsIdentity;  
  
// This outer try block is used to protect from   
// exception filter attacks which would prevent  
// the inner finally block from executing and   
// resetting the impersonation.  
try  
{  
   try  
   {  
      impersonatedUser = clientId.Impersonate();  
      if (impersonatedUser != null)  
         return GetFileDetails(directoryPath);  
         else return null;  
   }  
   finally  
   {  
      if (impersonatedUser != null)  
         impersonatedUser.Undo();  
   }  
}  
catch  
{  
   throw;  
}  
```  
  
> [!NOTE]  
>  For information about behavior changes in impersonation, see [Breaking Changes to Database Engine Features in SQL Server 2016](../../../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016.md).  
  
 Furthermore, if you obtained the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows identity instance, by default you cannot propagate that instance to another computer; Windows security infrastructure restricts that by default. There is, however, a mechanism called "delegation" that enables propagation of Windows identities across multiple trusted computers. You can learn more about delegation in the TechNet article, "[Kerberos Protocol Transition and Constrained Delegation](/previous-versions/windows/it-pro/windows-server-2003/cc739587(v=ws.10))".  
  
## See Also  
 [SqlContext Object](../../../relational-databases/clr-integration-data-access-in-process-ado-net/sqlcontext-object.md)  
  
