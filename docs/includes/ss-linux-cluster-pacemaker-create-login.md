1. **On all SQL Servers, create a Server login for Pacemaker**. The following Transact-SQL creates a login:

   ```Transact-SQL
   USE [master]
   GO
   CREATE LOGIN [pacemakerLogin] with PASSWORD= N'ComplexP@$$w0rd!'
   	
   ALTER SERVER ROLE [sysadmin] ADD MEMBER [pacemakerLogin]
   ```

   Alternatively, you can set the permissions at a more granular level. The Pacemaker login requires ALTER, CONTROL, and VIEW DEFINITION PERMISSION for managing the availability group as well as VIEW SERVER STATE for the login to be able to run sp_server_diagnostics. For more information, see [GRANT Availability Group Permissions (Transact-SQL)](http://msdn.microsoft.com/library/hh968934.aspx) and [sp_server_diagnostic permissions](https://docs.microsoft.com/en-us/sql/relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql#permissions).

   The following Transact-SQL grants only the required permission to the Pacemaker login. In the statement below 'ag1' is the name of the availability group that will be added as a cluster resource.

   ```Transact-SQL
   GRANT ALTER, CONTROL, VIEW DEFINITION ON AVAILABILITY GROUP::ag1 TO pacemakerLogin
   GRANT VIEW SERVER STATE TO pacemakerLogin
   ```

1. **On all SQL Servers, save the credentials for the SQL Server login**.

   ```bash
   echo 'pacemakerLogin' >> ~/pacemaker-passwd
   echo 'ComplexP@$$w0rd!' >> ~/pacemaker-passwd
   sudo mv ~/pacemaker-passwd /var/opt/mssql/secrets/passwd
   sudo chown root:root /var/opt/mssql/secrets/passwd
   sudo chmod 400 /var/opt/mssql/secrets/passwd # Only readable by root
   ```
