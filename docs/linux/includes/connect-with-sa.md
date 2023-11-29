---
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/24/2023
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
---
When you connect to your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance using the `sa` account for the first time after installation, it's important for you to follow these steps, and then immediately disable the `sa` login as a security best practice.

1. Create a new login, and make it a member of the **sysadmin** server role.

   - Depending on whether you have a container or non-container deployment, enable Windows authentication, and create a new Windows-based login and add it to the **sysadmin** server role.

     - [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](../sql-server-linux-ad-auth-adutil-tutorial.md)

     - [Tutorial: Configure Active Directory authentication with SQL Server on Linux containers](../sql-server-linux-containers-ad-auth-adutil-tutorial.md)

   - Otherwise, create a login using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] authentication, and add it to the **sysadmin** server role.

1. Connect to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance using the new login you created.

1. Disable the `sa` account, as recommended for security best practice.
