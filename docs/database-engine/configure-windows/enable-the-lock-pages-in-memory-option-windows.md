---
title: "Enable the Lock Pages in Memory Option (Windows)"
description: "Learn how to turn on the Lock Pages in Memory option. See how it can boost performance by keeping data in physical memory instead of paging it to disk."
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "Lock Pages in Memory option"
---
# Enable the Lock Pages in Memory Option (Windows)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  This Windows policy determines which accounts can use a process to keep data in physical memory, preventing the system from paging the data to virtual memory on disk.  
  
> [!NOTE]  
>  Locking pages in memory may boost performance when paging memory to disk is expected. For more information, see [Lock Pages in Memory (LPIM)](server-memory-server-configuration-options.md#lock-pages-in-memory-lpim).
  
 Use the Windows Group Policy tool (`gpedit.msc`) to enable this policy for the account used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You must be a system administrator to change this policy.  
  
### To enable the lock pages in memory option  
  
1. On the **Start** menu, select **Run**. In the **Open** box, type **gpedit.msc**. The **Group Policy** dialog box opens.  
1. On the **Local Group Group Policy** console, expand **Computer Configuration**. 
1. Expand **Windows Settings**. 
1. Expand **Security Settings**.
1. Expand **Local Policies**.  
1. Select the **User Rights Assignment** folder. The policies will be displayed in the details pane.  
1. In the pane, scroll to and double-click the **Lock pages in memory** policy.  
1. In the **Local Security Policy Setting** dialog box, select **Add User or Group...*. Add the SQL Server Service account. To determine the service account for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], refer to the SQL Server Configuration Manager or query the `service_account` from `sys.dm_server_services`. For more information, see [sys.dm_server_services (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-server-services-transact-sql.md).
1. Select **OK**.
1. Restart the instance for this setting to take effect.
  
## Next steps

- [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)  
- [Memory Management Architecture Guide](../../relational-databases/memory-management-architecture-guide.md)
  
  
