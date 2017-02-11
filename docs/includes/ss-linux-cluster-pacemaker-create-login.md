1. **On all SQL Servers, create a Server login for Pacemaker**. The following Transact-SQL creates a login:

   ```Transact-SQL
   USE [master]
   GO
   CREATE LOGIN [<pacemakerLogin>] with PASSWORD= N'<ComplexP@$$w0rd!>'
   	
   ALTER SERVER ROLE [sysadmin] ADD MEMBER [pacemakerLogin]
   ```

   Alternatively, you can set the permissions at a more granular level. The Pacemaker login requires ALTER, CONNECT, and VIEW DEFINITION PERMISSION. For more information, see [GRANT Availability Group Permissions (Transact-SQL)](http://msdn.microsoft.com/library/hh968934.aspx).

   The following Transact-SQL grants only the required permission to the Pacemaker login.

   ```Transact-SQL
   GRANT ALTER, CONNECT, VIEW DEFINITION ON AVAILABILITY GROUP::ag1 TO pacemakerLogin
   ```

1. **On all SQL Servers, save the credentials for the SQL Server login**.

   ```bash
   echo 'pacemakerLogin' > ~/pacemaker-passwd
   echo 'Yukon900' >> ~/pacemaker-passwd
   sudo mv ~/pacemaker-passwd /var/opt/mssql/secrets/passwd
   sudo chown root:root /var/opt/mssql/secrets/passwd
   sudo chmod 400 /var/opt/mssql/secrets/passwd # Only readable by root
   ```