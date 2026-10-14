---
author: AttinderPalSingh
ms.author: atsingh
ms.reviewer: amitkh
ms.date: 05/07/2026
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
  - sfi-ropc-blocked
---
The `sa` account is a system administrator on the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance that's created during setup. After you create your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container, the `MSSQL_SA_PASSWORD` environment variable you specified is discoverable by running `echo $MSSQL_SA_PASSWORD` in the container. For security purposes, change your `sa` password:

1. Choose a strong password to use for the `sa` account. [!INCLUDE [password-complexity](password-complexity.md)]

1. Use `docker exec` to run the **`sqlcmd`** utility to change the password through a Transact-SQL statement. Replace `<old-password>` and `<new-password>` with your own password values:

   > [!IMPORTANT]  
   > The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

   ```bash
   sudo docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd \
      -S localhost -U sa -P '<old-password>' \
      -Q 'ALTER LOGIN sa WITH PASSWORD="<new-password>"'
   ```

   ```powershell
   docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd `
      -S localhost -U sa -P "<old-password>" `
      -Q "ALTER LOGIN sa WITH PASSWORD='<new-password>'"
   ```
