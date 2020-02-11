1. **On all SQL Servers, create a Server login for Pacemaker**. The following Transact-SQL creates a login:

   ```Transact-SQL
   USE [master]
   GO
   CREATE LOGIN [pacemakerLogin] with PASSWORD= N'ComplexP@$$w0rd!'
   	
   ALTER SERVER ROLE [sysadmin] ADD MEMBER [pacemakerLogin]
   ```

  At the time of availability group creation, the pacemaker user will require ALTER, CONTROL and VIEW DEFINITION permissions on the availability group, after it's created but before any nodes are added to it.

1. **On all SQL Servers, save the credentials for the SQL Server login**.

   ```bash
   echo 'pacemakerLogin' >> ~/pacemaker-passwd
   echo 'ComplexP@$$w0rd!' >> ~/pacemaker-passwd
   sudo mv ~/pacemaker-passwd /var/opt/mssql/secrets/passwd
   sudo chown root:root /var/opt/mssql/secrets/passwd
   sudo chmod 400 /var/opt/mssql/secrets/passwd # Only readable by root
   ```
