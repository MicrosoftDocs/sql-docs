---
author: AttinderPalSingh
ms.author: atsingh
ms.reviewer: amitkh
ms.date: 11/18/2024
ms.service: sql
ms.topic: include
ms.custom:
  - linux-related-content
---
When you connect to your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance using the system administrator (`sa`) account for the first time after installation, it's important for you to follow these steps, and then immediately disable the `sa` account as a security best practice.

1. Create a new login, and make it a member of the **sysadmin** server role.

   - Depending on whether you have a container or non-container deployment, enable Windows authentication, and create a new Windows-based login and add it to the **sysadmin** server role.

     - [Tutorial: Use adutil to configure Active Directory authentication with SQL Server on Linux](../security/authentication/adutil-tutorial.md)

     - [Tutorial: Configure Active Directory authentication with SQL Server on Linux containers](../containers/tutorial-adutil.md)

   - Otherwise, create a login using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] authentication, and add it to the **sysadmin** server role.

1. Connect to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance using the new login you created.

1. Disable the `sa` account, as recommended for security best practice.
