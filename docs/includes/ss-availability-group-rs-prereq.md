## Prerequisites

Before you create the availability group, you need to:

- Set your environment so that all the servers that will host availability replicas can communicate.
- Install SQL Server. See [Install SQL Server](../database-engine/install-windows/install-sql-server.md) for details.

## Enable Always On availability groups and restart mssql-server

>[!NOTE]
>The following command utilizes cmdlets from the sqlserver module that's published in the PowerShell Gallery. You can install this module by using the Install-Module command.

Enable Always On availability groups on each replica that hosts a SQL Server instance. Then restart the SQL Server service. Run the following command to enable and then restart the SQL Server services:

```powershell
Enable-SqlAlwaysOn -ServerInstance <server\instance> -Force
```

## Enable an AlwaysOn_health event session

 To help with root-cause diagnosis when you troubleshoot an availability group, you can optionally enable an Always On availability groups extended events (XEvents) session. To do so, run the following command on each instance of SQL Server:

```sql
ALTER EVENT SESSION  AlwaysOn_health ON SERVER WITH (STARTUP_STATE=ON);
GO
```

For more information about this XEvents session, see [Always On availability groups extended events](../database-engine/availability-groups/windows/always-on-extended-events.md).

## Database-mirroring endpoint authentication

For synchronization to function properly, the replicas that are involved in the read-scale availability group need to authenticate over the endpoint. The two main scenarios that you can use for such authentication are covered in the next sections.

### Service account

In an Active Directory environment where all secondary replicas are joined to the same domain, SQL Server can authenticate by utilizing the service account. You must explicitly create a login for the service account on each SQL Server instance:

```sql
CREATE LOGIN [<domain>\service account] FROM WINDOWS;
```

### SQL login authentication

In environments where the secondary replicas might not be joined to an Active Directory domain, you must utilize SQL authentication. The following Transact-SQL script creates a login named `dbm_login` and a user named `dbm_user`. Update the script with a strong password. To create the database-mirroring endpoint user, run the following command on all SQL Server instances:

```sql
CREATE LOGIN dbm_login WITH PASSWORD = '**<1Sample_Strong_Password!@#>**';
CREATE USER dbm_user FOR LOGIN dbm_login;
```

#### Certificate authentication

If you utilize a secondary replica that requires authentication with SQL authentication, use a certificate for authenticating between the mirroring endpoints.

The following Transact-SQL script creates a master key and a certificate. It then backs up the certificate and secures the file with a private key. Update the script with strong passwords. Run the script on the primary SQL Server instance to create the certificate:

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '**<Master_Key_Password>**';
CREATE CERTIFICATE dbm_certificate WITH SUBJECT = 'dbm';
BACKUP CERTIFICATE dbm_certificate
   TO FILE = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbm_certificate.cer'
   WITH PRIVATE KEY (
           FILE = 'c:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbm_certificate.pvk',
           ENCRYPTION BY PASSWORD = '**<Private_Key_Password>**'
       );
```

At this point, your primary SQL Server replica has a certificate at `c:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbm_certificate.cer` and a private key at `c:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbm_certificate.pvk`. Copy these two files to the same location on all servers that will host availability replicas.

On each secondary replica, ensure that the service account for the SQL Server instance has permissions to access the certificate.

#### Create the certificate on secondary servers

The following Transact-SQL script creates a master key and a certificate from the backup that you created on the primary SQL Server replica. The command also authorizes users to access the certificate. Update the script with strong passwords. The decryption password is the same password that you used to create the *.pvk* file in a previous step. To create the certificate, run the following script on all secondary replicas:

```sql
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '**<Master_Key_Password>**';
CREATE CERTIFICATE dbm_certificate
    AUTHORIZATION dbm_user
    FROM FILE = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbm_certificate.cer'
    WITH PRIVATE KEY (
    FILE = 'c:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbm_certificate.pvk',
    DECRYPTION BY PASSWORD = '**<Private_Key_Password>**'
            );
```

## Create database-mirroring endpoints on all replicas

Database-mirroring endpoints use the Transmission Control Protocol (TCP) to send and receive messages between the server instances that participate in database-mirroring sessions or host availability replicas. The database-mirroring endpoint listens on a unique TCP port number.

The following Transact-SQL script creates a listening endpoint named `Hadr_endpoint` for the availability group. It starts the endpoint and gives connection permission to the service account or SQL login that you created in a previous step. Before you run the script, replace the values between `**< ... >**`. Optionally you can include an IP address, `LISTENER_IP = (0.0.0.0)`. The listener IP address must be an IPv4 address. You can also use `0.0.0.0`.

Update the following Transact-SQL script for your environment on all SQL Server instances:

```SQL
CREATE ENDPOINT [Hadr_endpoint]
    AS TCP (LISTENER_PORT = **<5022>**)
    FOR DATA_MIRRORING (
	    ROLE = ALL,
	    AUTHENTICATION = CERTIFICATE dbm_certificate,
		ENCRYPTION = REQUIRED ALGORITHM AES
		);
ALTER ENDPOINT [Hadr_endpoint] STATE = STARTED;
GRANT CONNECT ON ENDPOINT::[Hadr_endpoint] TO [<service account or user>];
```

The TCP port on the firewall must be open for the listener port.

For more information, see [The database-mirroring endpoint (SQL Server)](../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md).
