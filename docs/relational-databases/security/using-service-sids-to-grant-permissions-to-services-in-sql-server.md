---
description: "Using Service SIDs to grant permissions to services in SQL Server"
title: Using Service SIDs to grant permissions to services
ms.custom: seo-dt-2019
author: randomnote1
ms.author: dareist
ms.date: "05/02/2019"
ms.topic: conceptual
ms.service: sql
---

# Using Service SIDs to grant permissions to services in SQL Server

SQL Server uses [per-service Security Identifiers (SID)](https://support.microsoft.com/help/2620201/sql-server-uses-a-service-sid-to-provide-service-isolation) (also referred to as service security principal (SID)) to allow permissions to be granted directly to a specific service. This method is used by SQL Server to grant permissions to the engine and agent services (NT SERVICE\MSSQL$\<InstanceName\> and NT SERVICE\SQLAGENT$\<InstanceName\> respectively). Using this method, those services can access the database engine only when the services are running.

This same method can be used when granting permissions to other services. Using a Service SID eliminates the overhead of managing and maintaining service accounts and provide tighter, more granular control over permissions granted to system resources.

Examples of services where a Service SID can be used are:

- System Center Operations Manager Health Service (NT SERVICE\HealthService)
- Windows Server Failover Clustering (WSFC) service (NT SERVICE\ClusSvc)

Some services don't have a Service SID by default. The service SID must be created using [SC.exe](/windows/desktop/services/configuring-a-service-using-sc). [This method](https://kevinholman.com/2016/08/25/sql-mp-run-as-accounts-no-longer-required/) has been adopted by Microsoft System Center Operations Manager administrators to grant permission to the HealthService within SQL server.

Once the service SID has been created and confirmed, it must be granted permission within SQL Server. Granting permissions is accomplished by creating a Windows login using either [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) or a query. Once the login is created, it can be granted permissions, added to roles, and mapped to databases just like any other login.

> [!TIP]
> If the error `Login failed for user 'NT AUTHORITY\SYSTEM'` is received, verify the Service SID exists for the desired service, the Service SID login was created in SQL Server, and the appropriate permissions were granted to the Service SID in SQL Server.

## Security

### Eliminate service accounts

Traditionally service accounts have been used to allow services to log into SQL Server. Service accounts add an additional layer of management complexity because of having to maintain and periodically update the service account password. Additionally, the service account credentials could be used by an individual attempting to mask their activities when performing actions in the instance.

### Granular permissions to system accounts

System accounts have historically been granted permissions by creating a login for the [LocalSystem](/windows/win32/services/localsystem-account) ([NT AUTHORITY\SYSTEM in en-us](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md#Localized_service_names)) or [NetworkService](/windows/desktop/Services/networkservice-account) ([NT AUTHORITY\NETWORK SERVICE in en-us](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md#Localized_service_names)) accounts and granting those logins permissions. This method grants any process or service permissions into SQL, which is running as a system account.

Using a Service SID allows permissions to be granted to a specific service. The service only has access to the resources it was granted permissions to when it is running. For example, if the `HealthService` is running as `LocalSystem` and is granted `View Server State`, the `LocalSystem` account will only have permission to `View Server State` when it is running in the context of the `HealthService`. If any other process attempts to access the server state of SQL as `LocalSystem`, they will be denied access.

## Examples

### A. Create a Service SID

The following PowerShell command will create a service SID on the System Center Operations Manager health service.

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

The following example creates a login for the System Center Operations Manager health service using T-SQL.

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
GRANT ALTER ANY AVAILABILITY GROUP TO [NT SERVICE\ClusSvc]
GO

GRANT CONNECT SQL TO [NT SERVICE\ClusSvc]
GO

GRANT VIEW SERVER STATE TO [NT SERVICE\ClusSvc]
GO
```

  > [!NOTE]
  > Removing the service SID logins or removing them from the sysadmin server role can cause problems for various components of SQL Server that connect to the SQL Server Database Engine. Some problems include the following:
  > - SQL Server Agent cannot start or connect to a SQL Server service
  > - SQL Server Setup programs encounter the problem that is mentioned in the following Microsoft Knowledge Base article:
  > https://mskb.pkisolutions.com/kb/955813
  >
  > For a default instance of SQL Server, you can correct this situation by adding the service SID using the following Transact-SQL commands:
  >
  > ```sql
  > CREATE LOGIN [NT SERVICE\MSSQLSERVER] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
  > 
  > ALTER ROLE sysadmin ADD MEMBER [NT SERVICE\MSSQLSERVER]
  > 
  > CREATE LOGIN [NT SERVICE\SQLSERVERAGENT] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
  > 
  > ALTER ROLE sysadmin ADD MEMBER [NT SERVICE\SQLSERVERAGENT]
  > ```
  > For a named instance of SQL Server, use the following Transact-SQL commands:
  > ```sql
  > CREATE LOGIN [NT SERVICE\MSSQL$SQL2019] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
  > 
  > ALTER ROLE sysadmin ADD MEMBER [NT SERVICE\MSSQL$SQL2019]
  > 
  > CREATE LOGIN [NT SERVICE\SQLAgent$SQL2019] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
  > 
  > ALTER ROLE sysadmin ADD MEMBER [NT SERVICE\SQLAgent$SQL2019]
  > 
  > ```
  > In this example, `SQL2019` is the instance name of the SQL Server.

## Next steps

For more information about the service sid structure, read [SERVICE_SID_INFO structure](/windows/win32/api/winsvc/ns-winsvc-service_sid_info).

Read about additional options that are available when [creating a login](../../t-sql/statements/create-login-transact-sql.md).

To use Role-Based Security with Service SIDs, read about [creating roles](../../t-sql/statements/create-role-transact-sql.md) in SQL Server.

Read about different ways to [grant permissions](../../t-sql/statements/grant-transact-sql.md) to Service SIDs in SQL Server.

For more information on configuring service accounts for SQL server, read [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).
