---
title: Impersonation and Credentials for Connections
description: In SQL Server CLR integration, you can impersonate the caller in Windows Authentication by using the SqlContext.WindowsIdentity property.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/27/2024
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
---
# Impersonation and credentials for connections

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

In the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] common language runtime (CLR) integration, using Windows Authentication is complex, but is more secure than using SQL Server Authentication. When using Windows Authentication, keep in mind the following considerations.

By default, a SQL Server process that connects out to Windows acquires the security context of the SQL Server Windows service account. But it's possible to map a CLR function to a proxy identity, so that its outbound connections have a different security context than the Windows service account.

In some cases, you might want to impersonate the caller by using the `SqlContext.WindowsIdentity` property instead of running as the service account. The `WindowsIdentity` instance represents the identity of the client that invoked the calling code, and is only available when the client used Windows Authentication. After you obtain the `WindowsIdentity` instance, you can call `Impersonate` to change the security token of the thread, and then open ADO.NET connections on behalf of the client.

After you call `SQLContext.WindowsIdentity.Impersonate`, you can't access local data and you can't access system data. To access data again, you have to call `WindowsImpersonationContext.Undo`.

The following [!INCLUDE [c-sharp-md](../../../includes/c-sharp-md.md)] example shows how to impersonate the caller by using the `SqlContext.WindowsIdentity` property.

```csharp
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
> For information about behavior changes in impersonation, see [Breaking changes to Database Engine features in SQL Server 2016](/previous-versions/sql/database-engine/breaking-changes-to-database-engine-features-in-sql-server-2016).

Furthermore, if you obtained the Windows identity instance, by default you can't propagate that instance to another computer; Windows security infrastructure restricts that by default. However, there's a mechanism called *delegation* that enables propagation of Windows identities across multiple trusted computers. For more information about delegation, see [Kerberos Protocol Transition and Constrained Delegation](/previous-versions/windows/it-pro/windows-server-2003/cc739587(v=ws.10)).

## Related content

- [SqlContext object](../../clr-integration-data-access-in-process-ado-net/sqlcontext-object.md)
