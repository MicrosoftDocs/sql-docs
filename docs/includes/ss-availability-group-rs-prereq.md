## Prerequisites

Before you create the availability group, you need to:

- Set your environment so that all the servers that will host availability replicas can communicate.
- Install SQL Server. See [Install SQL Server](install-sql-server.md) for details.

## Enable AlwaysOn availability groups and restart mssql-server

>[!NOTE]
>The command used below is utilizing cmdlets from the sqlserver module that is published on the PowerShell Gallery. You can install this module using the Install-Module command.

Enable AlwaysOn availability groups on each replica that hosts a SQL Server instance. Then restart the SQL Server service. Run the following command to enable and the restart the SQL Server services:

```powershell
Enable-SqlAlwaysOn -ServerInstance <server\instance> -Force
```

## Enable an AlwaysOn_health event session

You can optionally enable AlwaysOn availability groups Extended Event (XE) session to help with root-cause diagnosis when you troubleshoot an availability group. Run the following command on each instance of SQL Server:

```SQL
ALTER EVENT SESSION  AlwaysOn_health ON SERVER WITH (STARTUP_STATE=ON);
GO
```

For more information about this XE session, see [Always On Availability Groups extended events](always-on-extended-events.md).

## Database Mirroring Endpoint Authentication

The replicas involved in the read-scale Availability Group will need to authenticate over the endpoint in order for synchronization to function properly. There are two main scenarios covered below that can be used for such authentication.

### Service Account

In an Active Directory environment where all secondary replicas on joined to the same domain SQL Server can authenticate utilizing the service account. You will need to explicitly create a login for the service account on each all SQL Server instances:

```SQL
CREATE LOGIN [<domain>\service account] FROM WINDOWS;
```

### SQL Login Authentication

In environments where the secondary replicas may not be joined to an Active Directory Domain you will need to utilize SQL Authentication. The following Transact-SQL script creates a login named `dbm_login` and a user named `dbm_user`. Update the script with a strong password. To create the database mirroring endpoint user, run the following command on all SQL Server instances:

```SQL
CREATE LOGIN dbm_login WITH PASSWORD = '**<1Sample_Strong_Password!@#>**';
CREATE USER dbm_user FOR LOGIN dbm_login;
```

#### Certificate Authentication

If you utilizing a secondary replica that requires authentication with SQL Authentication you will need to utilize a certificate for authenticating between the mirroring endpoints.

The following Transact-SQL script creates a master key and a certificate. It then backs up the certificate and secures the file with a private key. Update the script with strong passwords. Run the following on the primary SQL Server instance to create the certificate:

```SQL
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

Ensure on each secondary replica that the service account for SQL Server has permissions to access the certificate.

#### Create the certificate on secondary servers

The following Transact-SQL script creates a master key and a certificate from the backup that you created on the primary SQL Server replica. The command also authorizes the user to access the certificate. Update the script with strong passwords. The decryption password is the same password that you used to create the `.pvk` file in a previous step. To create the certificate, run the following script on all secondary replicas:

```SQL
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '**<Master_Key_Password>**';
CREATE CERTIFICATE dbm_certificate
    AUTHORIZATION dbm_user
    FROM FILE = '/var/opt/mssql/data/dbm_certificate.cer'
    WITH PRIVATE KEY (
    FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
    DECRYPTION BY PASSWORD = '**<Private_Key_Password>**'
            );
```

## Create the database mirroring endpoints on all replicas

Database mirroring endpoints use the Transmission Control Protocol (TCP) to send and receive messages between the server instances that participate in database mirroring sessions or host availability replicas. The database mirroring endpoint listens on a unique TCP port number.

The following Transact-SQL script creates a listening endpoint named `Hadr_endpoint` for the availability group. It starts the endpoint and gives connection permission to the service account or SQL Login that you created in a previous step. Before you run the script, replace the values between `**< ... >**`. Optionally you can include an IP address `LISTENER_IP = (0.0.0.0)`. The listener IP address must be an IPv4 address. You can also use `0.0.0.0`.

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

For more information, see [The database mirroring endpoint (SQL Server)](http://msdn.microsoft.com/library/ms179511.aspx).
