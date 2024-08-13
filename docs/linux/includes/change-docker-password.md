---
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/14/2023
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
The `SA` account is a system administrator on the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance that's created during setup. After you create your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container, the `MSSQL_SA_PASSWORD` environment variable you specified is discoverable by running `echo $MSSQL_SA_PASSWORD` in the container. For security purposes, change your SA password:

1. Choose a strong password to use for the SA user.

1. Use `docker exec` to run the **sqlcmd** utility to change the password through a Transact-SQL statement. Replace `<YourStrong!Passw0rd>` and `<YourNewStrong!Passw0rd>` with your own password values:

   > [!IMPORTANT]  
   > The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

   ```bash
   sudo docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd \
      -S localhost -U SA -P '<YourStrong!Passw0rd>' \
      -Q 'ALTER LOGIN SA WITH PASSWORD="<YourNewStrong!Passw0rd>"'
   ```

   ```PowerShell
   docker exec -it sql1 /opt/mssql-tools/bin/sqlcmd `
      -S localhost -U SA -P "<YourStrong!Passw0rd>" `
      -Q "ALTER LOGIN SA WITH PASSWORD='<YourNewStrong!Passw0rd>'"
   ```
