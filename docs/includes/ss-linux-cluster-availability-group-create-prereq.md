## Prerequisites

Before you create the availability group, you need to:

- Set your environment so all servers that will host availability replicas can communicate
- Install SQL Server

>[!NOTE]
>On Linux, you must create an availability group before adding it as a cluster resource to be managed by the cluster. This document provides an example that creates the availability group. For distribution specific instructions to create the cluster and add the availability group as a cluster resource, see the links under [Next steps](#next-steps).

1. **Update the computer name for each host**

   Each SQL Server name must be:
   
   - 15 characters or less
   - Unique within the network
   
   To set the computer name, edit `/etc/hostname`. The following script lets you edit `/etc/hostname` with `vi`.

   ```bash
   sudo vi /etc/hostname
   ```

1. **Configure the hosts file**

>[!NOTE]
>If hostnames are registered with their IP in the DNS server, there is no need to do the steps below. Validate that all nodes that are going to be part of the availability group configuration can communicate with each other (pinging the hostname should reply with the corresponding IP address). Also, make sure that /etc/hosts file does not contain a record that maps localhost IP address 127.0.0.1 with the hostname of the node.


   The hosts file on every server contains the IP addresses and names of all servers that will participate in the availability group. 

   The following command returns the IP address of the current server:

   ```bash
   sudo ip addr show
   ```

   Update `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`.

   ```bash
   sudo vi /etc/hosts
   ```

   The following example shows `/etc/hosts` on **node1** with additions for **node1**, **node2**, and **node3**. In this document **node1** refers to the server hosting the primary replica. **node2**, and **node3** refer to servers hosting secondary replicas.


   ```
   127.0.0.1   localhost localhost4 localhost4.localdomain4
   ::1       localhost localhost6 localhost6.localdomain6
   10.128.18.12 node1
   10.128.16.77 node2
   10.128.15.33 node3
   ```

### Install SQL Server

Install SQL Server. The following links point to SQL Server installation instructions for various distributions. 

- [Red Hat Enterprise Linux](../linux/quickstart-install-connect-red-hat.md)

- [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md)

- [Ubuntu](../linux/quickstart-install-connect-ubuntu.md)

## Enable Always On availability groups and restart sqlserver

Enable Always On availability groups on each node hosting a SQL Server instance, then restart `mssql-server`.  Run the following script:

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled  1
sudo systemctl restart mssql-server
```

##	Enable AlwaysOn_health event session 

You can optionally enable Always On availability groups extended events to help with root-cause diagnosis when you troubleshoot an availability group. Run the following command on each each instance of SQL Server. 

```Transact-SQL
ALTER EVENT SESSION  AlwaysOn_health ON SERVER WITH (STARTUP_STATE=ON);
GO
```

For more information about this XE session, see [Always On Extended Events](http://msdn.microsoft.com/library/dn135324.aspx).

## Create db mirroring endpoint user

The following Transact-SQL script creates a login named `dbm_login`, and a user named `dbm_user`. Update the script with a strong password. Run the following command on all SQL Server instances to create the database mirroring endpoint user.

```Transact-SQL
CREATE LOGIN dbm_login WITH PASSWORD = '**<1Sample_Strong_Password!@#>**';
CREATE USER dbm_user FOR LOGIN dbm_login;
```

## Create a certificate

The SQL Server service on Linux uses certificates to authenticate communication between the mirroring endpoints. 

The following Transact-SQL script creates a master key and certificate. It then backs the certificate up and secures the file with a private key. Update the script with strong passwords. Connect to the primary SQL Server instance and run the following Transact-SQL to create the certificate:

```Transact-SQL
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '**<Master_Key_Password>**';
CREATE CERTIFICATE dbm_certificate WITH SUBJECT = 'dbm';
BACKUP CERTIFICATE dbm_certificate
   TO FILE = '/var/opt/mssql/data/dbm_certificate.cer'
   WITH PRIVATE KEY (
           FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
           ENCRYPTION BY PASSWORD = '**<Private_Key_Password>**'
       );
```

At this point your primary SQL Server replica has a certificate at `/var/opt/mssql/data/dbm_certificate.cer` and a private key at `var/opt/mssql/data/dbm_certificate.pvk`. Copy these two files to the same location on all servers that will host availability replicas. Use the mssql user or give permission to mssql user to access these files. 

For example on the source server, the following command copies the  files to the target machine. Replace the **<node2>** values with the names of the SQL Server instances that will host the replicas. 

```bash
cd /var/opt/mssql/data
scp dbm_certificate.* root@**<node2>**:/var/opt/mssql/data/
```

On each target server, give permission to mssql user to access the certificate.

```bash
cd /var/opt/mssql/data
chown mssql:mssql dbm_certificate.*
```

## Create the certificate on secondary servers

The following Transact-SQL script creates a master key and certificate from the backup that you created on the primary SQL Server replica. The command also authorizes the user to access the certificate. Update the script with strong passwords. The decryption password is the same password that you used to create the .pvk file in a previous step. Run the following script on all secondary servers to create the certificate.

```Transact-SQL
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

Database mirroring endpoints use Transmission Control Protocol (TCP) to send and receive messages between the server instances participating database mirroring sessions or hosting availability replicas. The database mirroring endpoint listens on a unique TCP port number. 

The following Transact-SQL creates a listening endpoint named `Hadr_endpoint` for the availability group. It starts the endpoint, and gives connect permission to the user that you created. Before you run the script, replace the values between `**< ... >**`.

>[!NOTE]
>For this release, do not use a different IP address for the listener IP. We are working on a fix for this issue, but the only acceptable value for now is '0.0.0.0'.

Update the following Transact-SQL for your environment  on all SQL Server instances: 

```Transact-SQL
CREATE ENDPOINT [Hadr_endpoint]
    AS TCP (LISTENER_IP = (0.0.0.0), LISTENER_PORT = **<5022>**)
    FOR DATA_MIRRORING (
	    ROLE = ALL,
	    AUTHENTICATION = CERTIFICATE dbm_certificate,
		ENCRYPTION = REQUIRED ALGORITHM AES
		);
ALTER ENDPOINT [Hadr_endpoint] STATE = STARTED;
GRANT CONNECT ON ENDPOINT::[Hadr_endpoint] TO [dbm_login];
```

>[!IMPORTANT]
>The TCP port on the firewall needs to be open for the listener port.

>[!IMPORTANT]
>For SQL Server 2017 release, the only authentication method supported for database mirroring endpoint is `CERTIFICATE`. `WINDOWS` option will be enabled in a future release.

For complete information, see [The Database Mirroring Endpoint (SQL Server)](http://msdn.microsoft.com/library/ms179511.aspx).
