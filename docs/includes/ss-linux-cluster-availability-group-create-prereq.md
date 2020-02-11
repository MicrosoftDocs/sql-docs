## Prerequisites

Before you create the availability group, you need to:

- Set your environment so that all the servers that will host availability replicas can communicate.
- Install SQL Server.

>[!NOTE]
>On Linux, you must create an availability group before you add it as a cluster resource to be managed by the cluster. This document provides an example that creates the availability group. For distribution-specific instructions to create the cluster and add the availability group as a cluster resource, see the links under "Next steps."

1. Update the computer name for each host.

   Each SQL Server name must be:
   
   - 15 characters or less.
   - Unique within the network.
   
   To set the computer name, edit `/etc/hostname`. The following script lets you edit `/etc/hostname` with `vi`:

   ```bash
   sudo vi /etc/hostname
   ```

2. Configure the hosts file.

    >[!NOTE]
    >If hostnames are registered with their IP in the DNS server, you don't need to do the following steps. Validate that all the nodes intended to be part of the availability group configuration can communicate with each other. (A ping to the hostname should reply with the corresponding IP address.) Also, make sure that the /etc/hosts file doesn't contain a record that maps the localhost IP address 127.0.0.1 with the hostname of the node.
    >

   The hosts file on every server contains the IP addresses and names of all servers that will participate in the availability group. 

   The following command returns the IP address of the current server:

   ```bash
   sudo ip addr show
   ```

   Update `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`:

   ```bash
   sudo vi /etc/hosts
   ```

   The following example shows `/etc/hosts` on **node1** with additions for **node1**, **node2**, and **node3**. In this document, **node1** refers to the server that hosts the primary replica. And **node2** and **node3** refer to servers that host the secondary replicas.

    ```
    127.0.0.1   localhost localhost4 localhost4.localdomain4
    ::1       localhost localhost6 localhost6.localdomain6
    10.128.18.12 node1
    10.128.16.77 node2
    10.128.15.33 node3
    ```

### Install SQL Server

Install SQL Server. The following links point to SQL Server installation instructions for various distributions: 

- [Red Hat Enterprise Linux](../linux/quickstart-install-connect-red-hat.md)
- [SUSE Linux Enterprise Server](../linux/quickstart-install-connect-suse.md)
- [Ubuntu](../linux/quickstart-install-connect-ubuntu.md)

## Enable AlwaysOn availability groups and restart mssql-server

Enable AlwaysOn availability groups on each node that hosts a SQL Server instance. Then restart `mssql-server`. Run the following script:

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled  1
sudo systemctl restart mssql-server
```

##	Enable an AlwaysOn_health event session 

You can optionally enable AlwaysOn availability groups extended events to help with root-cause diagnosis when you troubleshoot an availability group. Run the following command on each instance of SQL Server: 

```SQL
ALTER EVENT SESSION  AlwaysOn_health ON SERVER WITH (STARTUP_STATE=ON);
GO
```

For more information about this XE session, see [AlwaysOn extended events](https://msdn.microsoft.com/library/dn135324.aspx).

## Create a certificate

The SQL Server service on Linux uses certificates to authenticate communication between the mirroring endpoints. 

The following Transact-SQL script creates a master key and a certificate. It then backs up the certificate and secures the file with a private key. Update the script with strong passwords. Connect to the primary SQL Server instance. To create the certificate, run the following Transact-SQL script:

```SQL
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '**<Master_Key_Password>**';
CREATE CERTIFICATE dbm_certificate WITH SUBJECT = 'dbm';
BACKUP CERTIFICATE dbm_certificate
   TO FILE = '/var/opt/mssql/data/dbm_certificate.cer'
   WITH PRIVATE KEY (
           FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
           ENCRYPTION BY PASSWORD = '**<Private_Key_Password>**'
       );
```

At this point, your primary SQL Server replica has a certificate at `/var/opt/mssql/data/dbm_certificate.cer` and a private key at `var/opt/mssql/data/dbm_certificate.pvk`. Copy these two files to the same location on all servers that will host availability replicas. Use the mssql user, or give permission to the mssql user to access these files. 

For example, on the source server, the following command copies the files to the target machine. Replace the `**<node2>**` values with the names of the SQL Server instances that will host the replicas. 

```bash
cd /var/opt/mssql/data
scp dbm_certificate.* root@**<node2>**:/var/opt/mssql/data/
```

On each target server, give permission to the mssql user to access the certificate.

```bash
cd /var/opt/mssql/data
chown mssql:mssql dbm_certificate.*
```

## Create the certificate on secondary servers

The following Transact-SQL script creates a master key and a certificate from the backup that you created on the primary SQL Server replica. Update the script with strong passwords. The decryption password is the same password that you used to create the .pvk file in a previous step. To create the certificate, run the following script on all secondary servers:

```SQL
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '**<Master_Key_Password>**';
CREATE CERTIFICATE dbm_certificate
    FROM FILE = '/var/opt/mssql/data/dbm_certificate.cer'
    WITH PRIVATE KEY (
    FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
    DECRYPTION BY PASSWORD = '**<Private_Key_Password>**'
            );
```

## Create the database mirroring endpoints on all replicas

Database mirroring endpoints use the Transmission Control Protocol (TCP) to send and receive messages between the server instances that participate in database mirroring sessions or host availability replicas. The database mirroring endpoint listens on a unique TCP port number. 

The following Transact-SQL script creates a listening endpoint named `Hadr_endpoint` for the availability group. It starts the endpoint and gives connection permission to the certificate that you created. Before you run the script, replace the values between `**< ... >**`. Optionally you can include an IP address `LISTENER_IP = (0.0.0.0)`. The listener IP address must be an IPv4 address. You can also use `0.0.0.0`. 

Update the following Transact-SQL script for your environment on all SQL Server instances: 

```SQL
CREATE ENDPOINT [Hadr_endpoint]
    AS TCP (LISTENER_PORT = **<5022>**)
    FOR DATABASE_MIRRORING (
	    ROLE = ALL,
	    AUTHENTICATION = CERTIFICATE dbm_certificate,
		ENCRYPTION = REQUIRED ALGORITHM AES
		);
ALTER ENDPOINT [Hadr_endpoint] STATE = STARTED;
```

>[!NOTE]
>If you use SQL Server Express Edition on one node to host a configuration-only replica, the only valid value for `ROLE` is `WITNESS`. Run the following script on SQL Server Express Edition:

```SQL
CREATE ENDPOINT [Hadr_endpoint]
    AS TCP (LISTENER_PORT = **<5022>**)
    FOR DATABASE_MIRRORING (
	    ROLE = WITNESS,
	    AUTHENTICATION = CERTIFICATE dbm_certificate,
		ENCRYPTION = REQUIRED ALGORITHM AES
		);
ALTER ENDPOINT [Hadr_endpoint] STATE = STARTED;
```

The TCP port on the firewall must be open for the listener port.



>[!IMPORTANT]
>For the SQL Server 2017 release, the only authentication method supported for the database mirroring endpoint is `CERTIFICATE`. The `WINDOWS` option will be enabled in a future release.

For more information, see [The database mirroring endpoint (SQL Server)](https://msdn.microsoft.com/library/ms179511.aspx).


