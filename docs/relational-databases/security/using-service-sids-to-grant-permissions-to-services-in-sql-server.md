# Using Service SIDs to grant permissions to services in SQL Server

SQL Server uses [per-service Security Identifiers (SID)](https://support.microsoft.com/en-us/help/2620201/sql-server-uses-a-service-sid-to-provide-service-isolation) to allow permissions to be granted directly to a specific service. This is the method used by SQL Server to grant permissions to the engine and agent services (NT SERVICE\MSSQL$<InstanceName> and NT SERVICE\SQLAGENT$<InstanceName> respectively). Using this method, those services can access the database engine only when the services are running.

This same method can be utilized when granting permissions to other services. Using a Service SID eliminates the overhead of managing and maintaining service accounts and provide tighter, more granular control over permissions granted to system resources.

Examples of services where a Service SID can be used are:

- System Center Operations Manager (SCOM) Health Service (NT SERVICE\HealthService)
- Windows Server Failover Clustering (WSFC) service (NT SERVICE\ClusSvc)

Some services do not have a Service SID by default. The service SID must be created using [SC.exe](https://docs.microsoft.com/en-us/windows/desktop/services/configuring-a-service-using-sc). [This method](https://kevinholman.com/2016/08/25/sql-mp-run-as-accounts-no-longer-required/) has been adopted by Microsoft System Center Operations Manager (SCOM) administrators to grant permission to the HealthService within SQL server.

Once the service SID has been created and confirmed, it must be granted permission within SQL Server. This is accomplished by creating a Windows login using either [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) or a query. Once the login is created, it can be granted permissions, added to roles, and mapped to databases just like any other login.

> [!TIP]
> If the error `Login failed for user 'NT AUTHORITY\SYSTEM'` is received, verify the Service SID exists for the desired service, the Service SID login was created in SQL Server, and the appropriate permissions were granted to the Service SID in SQL Server.

## Security

### Eliminate service accounts

Traditionally service accounts have been used to allow services to log into SQL Server. This added an additional layer of management complexity due to having to maintain and periodically update the service account password. Additionally, the service account credentials could be used by an individual attempting to mask their activities when performing actions in the instance.

### Granular permissions to system accounts

System accounts have historically been granted permissions by creating a login for the [LocalSystem](https://msdn.microsoft.com/en-us/library/windows/desktop/ms684190) ([NT AUTHORITY\SYSTEM in en-us](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/configure-windows-service-accounts-and-permissions#Localized_service_names)) or [NetworkService](https://docs.microsoft.com/windows/desktop/Services/networkservice-account) ([NT AUTHORITY\NETWORK SERVICE in en-us](https://docs.microsoft.com/sql/database-engine/configure-windows/configure-windows-service-accounts-and-permissions?#Localized_service_names)) accounts and granting those logins permissions. This method grants any process or service permissions into SQL which is running as a system account.

Using a Service SID allows permissions to be granted to a specific service. The service only has access to the resources it was granted permissions to when it is running. For example, if the `HealthService` is running as `LocalSystem` and is granted `View Server State`, the `LocalSystem` account will only have permission to `View Server State` when it is running in the context of the `HealthService`. If any other process attempts to access the server state of SQL as `LocalSystem`, they will be denied access.

## Examples

### A. Create a Service SID

The following PowerShell command will create a service SID on the SCOM health service.

```PowerShell
sc.exe --% sidtype "HealthService" unrestricted
```

> [!IMPORTANT]
> `--%` tells PowerShell to stop parsing the rest of the command. This is useful when using legacy commands and applications.

### B. Query a Service SID

To check a service SID or to ensure a service SID exists, execute the following command in PowerShell.

```PowerShell
sc.exe --% qsidtype "HealthService"
```

> [!IMPORTANT]
> `--%` tells PowerShell to stop parsing the rest of the command. This is useful when using legacy commands and applications.

### C. Add a newly created Service SID as a Login

The following example creates a login for the SCOM health service using T-SQL.

```SQL
CREATE LOGIN [NT SERVICE\HealthService] FROM WINDOWS
GO
```

### D. Add an existing Service SID as a Login

The following example creates a login for the Cluster Service using T-SQL. Granting permissions to the cluster service directly eliminates the need to grant excessive permissions to the SYSTEM account.

```SQL
CREATE LOGIN [NT SERVICE\ClusSvc] FROM WINDOWS
GO
```

### E. Grant permissions to a Service SID

Grant the permissions required to manage Availability Groups to the Cluster Service.

```SQL
GRANT ALTER ANY AVAILABILITY GROUP TO 'NT SERVICE\ClusSvc'
GO

GRANT CONNECT SQL TO 'NT SERVICE\ClusSvc'
GO

GRANT VIEW SERVER STATE TO 'NT SERVICE\ClusSvc'
GO
```

## See Also

 [SERVICE_SID_INFO structure](https://docs.microsoft.com/windows/desktop/api/winsvc/ns-winsvc-_service_sid_info)

 [CREATE LOGIN (Transact-SQL)](https://docs.microsoft.com/sql/t-sql/statements/create-login-transact-sql)

 [ALTER ROLE (Transact-SQL)](https://docs.microsoft.com/sql/t-sql/statements/alter-role-transact-sql)

[GRANT (Transact-SQL)](https://docs.microsoft.com/en-us/sql/t-sql/statements/grant-transact-sql)
