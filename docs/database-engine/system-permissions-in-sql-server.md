# SYSTEM Permissions in SQL Server

[SYSTEM](https://msdn.microsoft.com/en-us/library/windows/desktop/ms684190) ([NT AUTHORITY\SYSTEM in en-us](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/configure-windows-service-accounts-and-permissions#Localized_service_names)) should not be granted additional permissions in SQL Server. [SYSTEM](https://msdn.microsoft.com/en-us/library/windows/desktop/ms684190) is the account the operating system runs as. Any process that is executed as SYSTEM is running in the highest level permissions that can be granted.

A large portion of the running processes on a server are executing with SYSTEM as the owner. When a process is executing as SYSTEM, the process can do anything on the server.

Members of the Local Administrators group can easily configure workloads to execute as SYSTEM. Some examples include [Services](https://msdn.microsoft.com/en-us/library/windows/desktop/ms685141), [Scheduled Tasks](https://msdn.microsoft.com/en-us/library/windows/desktop/aa383614), and utilities like [PsExec](https://docs.microsoft.com/en-us/sysinternals/downloads/psexec). An Administrator could unintentionally or intentionally configure a rogue workload to execute as SYSTEM.

When a workload is running as SYSTEM, it is difficult (if not impossible) to trace the actions taken by SYSTEM back to the workload because all the actions show they were run as SYSTEM in the audit logs.

## Default Permissions

[SYSTEM](https://msdn.microsoft.com/en-us/library/windows/desktop/ms684190) is granted the CONNECT SQL and VIEW ANY DATABASE permissions by default.

## A Better Way to Grant Permissions

SQL Server 2008 introduced the use of [per-service Security Identifiers (SID)](https://support.microsoft.com/en-us/help/2620201/sql-server-uses-a-service-sid-to-provide-service-isolation) to allow permissions to be granted directly to a specific service. This is the method used by SQL Server to grant permissions to the engine and agent services (e.g. NT SERVICE\MSSQL$<InstanceName> and NT SERVICE\SQLAGENT$<InstanceName> respectively). Using this method, those services can access the database engine only when the services are running.

This same method can be utilized when granting permissions to other services. Some examples I have used include:

- NetBackup services (NT SERVICE\NetBackup Client Service and NT Service\NetBackup Legacy Client Service)
- System Center Operations Manager (SCOM) Health Service (NT SERVICE\HealthService)
- Windows Server Failover Clustering (WSFC) service (NT SERVICE\ClusSvc)

Some services will require the creation of the service SID using [SC.exe](https://docs.microsoft.com/en-us/windows/desktop/services/configuring-a-service-using-sc). [This method](https://blogs.technet.microsoft.com/kevinholman/2016/08/25/sql-mp-run-as-accounts-no-longer-required-2/) has been adopted by Microsoft System Center Operations Manager (SCOM) administrators to grant permission to the HealthService within SQL server.

### Create a Service SID

The following PowerShell command will create a service SID on the NetBackup Client Service.

```PowerShell
sc.exe --% sidtype "NetBackup Client Service" unrestricted
```

_Note: `--%` tells PowerShell to stop parsing the rest of the command. This is useful when using legacy commands and applications._

### Query a Service SID

To check a service SID or to ensure a service SID exists, execute the following command in PowerShell.

```PowerShell
sc.exe --% qsidtype "NetBackup Client Service"
```

_Note: `--%` tells PowerShell to stop parsing the rest of the command. This is useful when using legacy commands and applications._

### Add a Service SID as a Login

Once the service SID has been created and confirmed, it must be granted permission within SQL Server. This is accomplished by creating a Windows login using either SSMS or a query. The following example creates a login for the NetBackup Client Service using T-SQL:

```SQL
CREATE LOGIN [NT SERVICE\NetBackup Client Service] FROM WINDOWS
GO
```

Once the login is created, it can be granted permissions, added to roles, and mapped to databases just like any other login.

_Note: If the service SID has been created on the service and login failures are still occurring for SYSTEM, ensure the login for the service SID was created and has been permissioned correctly._