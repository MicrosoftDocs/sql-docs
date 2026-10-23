---
author: AttinderPalSingh
ms.author: atsingh
ms.reviewer: amitkh
ms.date: 11/18/2024
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---

> [!CAUTION]  
> [!INCLUDE [password-complexity](password-complexity.md)]

1. **On all SQL Server instances, create a server login for Pacemaker**.

   The following Transact-SQL creates a login. Replace `<password>` with your own complex password.

   ```sql
   USE [master];
   GO

   CREATE LOGIN [pacemakerLogin]
       WITH PASSWORD = N'<password>';

   ALTER SERVER ROLE [sysadmin] ADD MEMBER [pacemakerLogin];
   ```

   At the time of availability group creation, the Pacemaker user requires `ALTER`, `CONTROL`, and `VIEW DEFINITION` permissions on the availability group, after it's created but before any nodes are added to it.

1. **On all SQL Server instances, save the credentials for the SQL Server login**.

   Replace `<password>` with your own complex password.

   ```bash
   echo 'pacemakerLogin' >> ~/pacemaker-passwd
   echo '<password>' >> ~/pacemaker-passwd
   sudo mv ~/pacemaker-passwd /var/opt/mssql/secrets/passwd
   sudo chown root:root /var/opt/mssql/secrets/passwd
   sudo chmod 400 /var/opt/mssql/secrets/passwd # Only readable by root
   ```
