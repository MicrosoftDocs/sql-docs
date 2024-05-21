---
title: "Configure link with scripts"
titleSuffix: Azure SQL Managed Instance
description: Learn how to configure a link between SQL Server and Azure SQL Managed Instance with Transact-SQL (T-SQL) and Azure PowerShell or Azure CLI scripts.
author: MariDjo
ms.author: dmarinkovic
ms.reviewer: mathoma, danil
ms.date: 11/14/2023
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.custom: devx-track-azurepowershell, devx-track-azurecli, ignite-2023, build-2024
ms.topic: how-to
---

# Configure link with scripts - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to configure a [link](managed-instance-link-feature-overview.md) between SQL Server and Azure SQL Managed Instance with Transact-SQL and PowerShell or Azure CLI scripts. With the link, databases from your initial primary are replicated to your secondary replica in near-real time.

After the link is created, you can then fail over to your secondary replica for the purpose of migration, or disaster recovery. 

> [!NOTE]
> - It's also possible to configure the link with [SQL Server Management Studio (SSMS)](managed-instance-link-configure-how-to-ssms.md). 
> - Configuring Azure SQL Managed Instance as your initial primary is currently in preview and only supported starting with [SQL Server 2022 CU10](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate10). 


## Overview

Use the link feature to replicate databases from your initial primary to your secondary replica. For SQL Server 2022, the initial primary can be either SQL Server or Azure SQL Managed Instance. For SQL Server 2019 and earlier versions, the initial primary must be SQL Server. After the link is configured, databases from the initial primary are replicated to the secondary replica. 

You can choose to leave the link in place for continuous data replication in a hybrid environment between the primary and secondary replica, or you can fail over the database to the secondary replica, to migrate to Azure, or for disaster recovery. For SQL Server 2019 and earlier versions, failing over to Azure SQL Managed Instance breaks the link and fail back is unsupported. With SQL Server 2022, you have the option to maintain the link and fail back and forth between the two replicas - this feature is currently in preview.

If you plan to use your secondary managed instance for only disaster recovery, you can save on licensing costs by activating the [hybrid failover benefit](managed-instance-link-disaster-recovery.md#license-free-passive-dr-replica). 

Use the instructions in this article to manually set up the link between SQL Server and Azure SQL Managed Instance. After the link is created, your source database gets a read-only copy on your target secondary replica. 

> [!TIP]
> - To simplify using T-SQL scripts with the correct parameters for your environment, we strongly recommend using the Managed Instance link wizard in [SQL Server Management Studio (SSMS)](managed-instance-link-configure-how-to-ssms.md#create-link-to-replicate-database) to generate a script to create the link. On the **Summary** page of the **New Managed Instance link** window, select **Script** instead of **Finish**. 

## Prerequisites 

> [!NOTE]
> Some functionality of the link is generally available, while some is currently in preview. Review [version supportability](managed-instance-link-feature-overview.md#prerequisites) to learn more. 

To replicate your databases, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#prerequisites) with the required service update installed.
- Azure SQL Managed Instance. [Get started](instance-create-quickstart.md) if you don't have it. 
- PowerShell module [Az.SQL 3.9.0 or higher](https://www.powershellgallery.com/packages/Az.Sql), or [Azure CLI 2.47.0 or higher](/cli/azure/install-azure-cli). Or preferably, use [Azure Cloud Shell](/azure/cloud-shell/overview) online from the web browser to run the commands, because it's always updated with the latest module versions.
- A properly [prepared environment](managed-instance-link-preparation.md).

Consider the following:

- The link feature supports one database per link. To replicate multiple databases on an instance, create a link for each individual database. For example, to replicate 10 databases to SQL Managed Instance, create 10 individual links.
- Collation between SQL Server and SQL Managed Instance should be the same. A mismatch in collation could cause a mismatch in server name casing and prevent a successful connection from SQL Server to SQL Managed Instance.
- Error 1475 on your initial SQL Server primary indicates that you need to start a new backup chain by creating a full backup without the `COPY ONLY` option.
- To establish a link, or fail over, from SQL Managed Instance to SQL Server 2022, your managed instance must be configured with the [SQL Server 2022 update policy](update-policy.md#sql-server-2022-update-policy). Data replication and failover from SQL Managed Instance to SQL Server 2022 is not supported by instances configured with the Always-up-to-date update policy. 
- While you can establish a link from SQL Server 2022 to a SQL managed instance configured with the Always-up-to-date update policy, after fail over to SQL Managed Instance, you will no longer be able to replicate data or fail back to SQL Server 2022. 


## Permissions

For SQL Server, you should have **sysadmin** permissions. 

For Azure SQL Managed Instance, you should be a member of the [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles#sql-managed-instance-contributor), or have the following custom role permissions: 

|Microsoft.Sql/ resource|Necessary permissions| 
|---- | ---- | 
|Microsoft.Sql/managedInstances| /read, /write|
|Microsoft.Sql/managedInstances/hybridCertificate | /action |
|Microsoft.Sql/managedInstances/databases| /read, /delete, /write, /completeRestore/action, /readBackups/action, /restoreDetails/read| 
|Microsoft.Sql/managedInstances/distributedAvailabilityGroups| /read, /write, /delete, /setRole/action| 
|Microsoft.Sql/managedInstances/endpointCertificates| /read|
|Microsoft.Sql/managedInstances/hybridLink| /read, /write, /delete|
|Microsoft.Sql/managedInstances/serverTrustCertificates | /write, /delete, /read | 


## Terminology and naming conventions

As you run scripts from this user guide, it's important not to mistake SQL Server and SQL Managed Instance names for their fully qualified domain names (FQDNs). The following table explains what the various names exactly represent and how to obtain their values:

| Terminology    | Description | How to find out |
| :----| :------------- | :------------- | 
| Initial primary <sup>1</sup> | The SQL Server or SQL Managed Instance where you initially create the link to replicate your database to the secondary replica. | 
| Primary replica | The SQL Server or SQL Managed Instance that currently hosts the primary database. |
| Secondary replica | The SQL Server or SQL Managed Instance that is receiving near-real time replicated data from the current primary replica. | 
| SQL Server name | Short, single-word SQL Server name. For example: *sqlserver1*. | Run `SELECT @@SERVERNAME` from T-SQL. |
| SQL Server FQDN | Fully qualified domain name (FQDN) of your SQL Server. For example: *sqlserver1.domain.com*. | See your network (DNS) configuration on-premises, or the server name if you're using an Azure virtual machine (VM). |
| SQL Managed Instance name | Short, single-word SQL Managed Instance name. For example: *managedinstance1*. | See the name of your managed instance in the Azure portal. |
| SQL Managed Instance FQDN | Fully qualified domain name (FQDN) of your SQL Managed Instance. For example: *managedinstance1.6d710bcf372b.database.windows.net*. | See the host name on the SQL Managed Instance overview page in the Azure portal. |
| Resolvable domain name | DNS name that can be resolved to an IP address. For example, running `nslookup sqlserver1.domain.com` should return an IP address such as 10.0.0.1. | Run `nslookup` command from the command prompt. |
| SQL Server IP | IP address of your SQL Server. In case of multiple IPs on SQL Server, choose IP address that is accessible from Azure. | Run `ipconfig` command from the command prompt of host OS running the SQL Server. |

<sup>1</sup> Configuring Azure SQL Managed Instance as your initial primary is currently in preview and only supported starting with [SQL Server 2022 CU10](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate10).

## Set up database recovery and backup

If SQL Server is your initial primary, then databases that will be replicated via the link must be in the full recovery model and have at least one backup.  Since Azure SQL Managed Instance takes backups automatically, skip this step if SQL Managed Instance is your initial primary. primary

Run the following code on SQL Server for all databases you wish to replicate. Replace `<DatabaseName>` with your actual database name.

```sql
-- Run on SQL Server
-- Set full recovery model for all databases you want to replicate.
ALTER DATABASE [<DatabaseName>] SET RECOVERY FULL
GO

-- Execute backup for all databases you want to replicate.
BACKUP DATABASE [<DatabaseName>] TO DISK = N'<DiskPath>'
GO
```

For more information, see [Create a Full Database Backup](/sql/relational-databases/backup-restore/create-a-full-database-backup-sql-server).

> [!NOTE]
> The link supports replication of user databases only. Replication of system databases is not supported. To replicate instance-level objects (stored in `master` or `msdb` databases), we recommend that you script them out and run T-SQL scripts on the destination instance.


## Establish trust between instances

First, you must establish trust between the two instances, and secure the endpoints used to communicate and encrypt data across the network. Distributed availability groups use the existing availability group [database mirroring endpoint](/sql/database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server), rather than having their own dedicated endpoint. As such, security and trust need to be configured between the two instances through the availability group database mirroring endpoint.

> [!NOTE]
> The link is based on the Always On availability group technology. The database mirroring endpoint is a special-purpose endpoint that is used exclusively by availability groups to receive connections from other instances. The term database mirroring endpoint should not be mistaken with the legacy SQL Server database mirroring feature.

Certificate-based trust is the only supported way to secure database mirroring endpoints for SQL Server and SQL Managed Instance. If you have existing availability groups that use Windows authentication, you need to add certificate-based trust to the existing mirroring endpoint as a secondary authentication option. You can do this by using the `ALTER ENDPOINT` statement, as shown later in this article.

> [!IMPORTANT]
> Certificates are generated with an expiration date and time. They must be renewed and rotated before they expire.

The following lists an overview of the process to secure database mirroring endpoints for both SQL Server and SQL Managed Instance:

1. Generate a certificate on SQL Server and obtain its public key.
1. Obtain a public key of the SQL Managed Instance certificate.
1. Exchange the public keys between SQL Server and SQL Managed Instance.
1. Import Azure-trusted root certificate authority keys to SQL Server

The following sections describe these steps in detail.

### Create a certificate on SQL Server and import its public key to SQL Managed Instance

First, create the database master key in the `master` database, if it's not already present. Insert your password in place of `<strong_password>` in the following script, and keep it in a confidential and secure place. Run this T-SQL script on SQL Server:

```sql
-- Run on SQL Server
-- Create a master key encryption password
-- Keep the password confidential and in a secure place
USE MASTER
IF NOT EXISTS (SELECT * FROM sys.symmetric_keys WHERE symmetric_key_id = 101)
BEGIN
    PRINT 'Creating master key.' + CHAR(13) + 'Keep the password confidential and in a secure place.'
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<strong_password>'
END
ELSE
    PRINT 'Master key already exists.'
GO
```

Then, generate an authentication certificate on SQL Server. In the following script replace:
- `@cert_expiry_date` with the desired certificate expiration date (future date).

Record this date and set a reminder to rotate (update) the SQL server certificate before its expiration date to ensure continuous operation of the link.

> [!IMPORTANT]
> It is strongly recommended to use the auto-generated certificate name from this script. While customizing your own certificate name on SQL Server is allowed, the name should not contain any `\` characters.

```sql
-- Create the SQL Server certificate for the instance link
USE MASTER

-- Customize SQL Server certificate expiration date by adjusting the date below
DECLARE @cert_expiry_date AS varchar(max)='03/30/2025'

-- Build the query to generate the certificate
DECLARE @sqlserver_certificate_name NVARCHAR(MAX) = N'Cert_' + @@servername  + N'_endpoint'
DECLARE @sqlserver_certificate_subject NVARCHAR(MAX) = N'Certificate for ' + @sqlserver_certificate_name
DECLARE @create_sqlserver_certificate_command NVARCHAR(MAX) = N'CREATE CERTIFICATE [' + @sqlserver_certificate_name + '] ' + char (13) +
'    WITH SUBJECT = ''' + @sqlserver_certificate_subject + ''',' + char (13) +
'    EXPIRY_DATE = '''+ @cert_expiry_date + ''''+ char (13)
IF NOT EXISTS (SELECT name from sys.certificates WHERE name = @sqlserver_certificate_name)
BEGIN
    PRINT (@create_sqlserver_certificate_command)
    -- Execute the query to create SQL Server certificate for the instance link
    EXEC sp_executesql @stmt = @create_sqlserver_certificate_command
END
ELSE
    PRINT 'Certificate ' + @sqlserver_certificate_name + ' already exists.'
GO
```

Then, use the following T-SQL query on SQL Server to verify the certificate has been created:

```sql
-- Run on SQL Server
USE MASTER
GO
SELECT * FROM sys.certificates WHERE pvt_key_encryption_type = 'MK'
```

In the query results, you'll see that the certificate has been encrypted with the master key.

Now, you can get the public key of the generated certificate on SQL Server:

```sql
-- Run on SQL Server
-- Show the name and the public key of generated SQL Server certificate
USE MASTER
GO
DECLARE @sqlserver_certificate_name NVARCHAR(MAX) = N'Cert_' + @@servername  + N'_endpoint'
DECLARE @PUBLICKEYENC VARBINARY(MAX) = CERTENCODED(CERT_ID(@sqlserver_certificate_name));
SELECT @sqlserver_certificate_name as 'SQLServerCertName'
SELECT @PUBLICKEYENC AS SQLServerPublicKey;
```

Save values of `SQLServerCertName` and `SQLServerPublicKey` from the output, because you'll need it for the next step when you import the certificate. 


First, ensure that you're logged in to Azure and that you've selected the subscription where your managed instance is hosted. Selecting the proper subscription is especially important if you have more than one Azure subscription on your account. 

Replace `<SubscriptionID>` with your Azure subscription ID. 


```powershell-interactive
# Run in Azure Cloud Shell (select PowerShell console)

# Enter your Azure subscription ID
$SubscriptionID = "<SubscriptionID>"

# Login to Azure and select subscription ID
if ((Get-AzContext ) -eq $null)
{
    echo "Logging to Azure subscription"
    Login-AzAccount
}
Select-AzSubscription -SubscriptionName $SubscriptionID
```

Then use either the [New-AzSqlInstanceServerTrustCertificate](/powershell/module/az.sql/new-azsqlinstanceservertrustcertificate) PowerShell or [az sql mi partner-cert create](/cli/azure/sql/mi/partner-cert#az-sql-mi-partner-cert-create) Azure CLI command to upload the public key of the authentication certificate from SQL Server to Azure, such as the following PowerShell sample. 

Fill out necessary user information, copy it, paste it, and then run the script. Replace:
- `<SQLServerPublicKey>` with the public portion of the SQL Server certificate in binary format, which you've recorded in the previous step. It's a long string value that starts with `0x`.
- `<SQLServerCertName>` with the SQL Server certificate name you've recorded in the previous step.
- `<ManagedInstanceName>` with the short name of your managed instance. 


```powershell-interactive
# Run in Azure Cloud Shell (select PowerShell console)
# ===============================================================================
# POWERSHELL SCRIPT TO IMPORT SQL SERVER PUBLIC CERTIFICATE TO SQL MANAGED INSTANCE
# ===== Enter user variables here ====

# Enter the name for the server SQLServerCertName certificate – for example, "Cert_sqlserver1_endpoint"
$CertificateName = "<SQLServerCertName>"

# Insert the certificate public key blob that you got from SQL Server – for example, "0x1234567..."
$PublicKeyEncoded = "<SQLServerPublicKey>"

# Enter your managed instance short name – for example, "sqlmi"
$ManagedInstanceName = "<ManagedInstanceName>"

# ==== Do not customize the below cmdlets====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Upload the public key of the authentication certificate from SQL Server to Azure.
New-AzSqlInstanceServerTrustCertificate -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -Name $CertificateName -PublicKey $PublicKeyEncoded 
```

The result of this operation is a summary of the uploaded SQL Server certificate to Azure.

If you need to see all SQL Server certificates uploaded to a managed instance, use the [Get-AzSqlInstanceServerTrustCertificate](/powershell/module/az.sql/get-azsqlinstanceservertrustcertificate) PowerShell or [az sql mi partner-cert list](/cli/azure/sql/mi/partner-cert#az-sql-mi-partner-cert-list) Azure CLI command in Azure Cloud Shell. To remove SQL Server certificate uploaded to a SQL managed instance, use the [Remove-AzSqlInstanceServerTrustCertificate](/powershell/module/az.sql/remove-azsqlinstanceservertrustcertificate) PowerShell or [az sql mi partner-cert delete](/cli/azure/sql/mi/partner-cert#az-sql-mi-partner-cert-delete) Azure CLI command in Azure Cloud Shell.


### Get the certificate public key from SQL Managed Instance and import it to SQL Server

The certificate to secure the link endpoint is automatically generated on Azure SQL Managed Instance. Get the certificate public key from SQL Managed Instance, and import it to SQL Server by using the [Get-AzSqlInstanceEndpointCertificate](/powershell/module/az.sql/get-azsqlinstanceendpointcertificate) PowerShell or [az sql mi endpoint-cert show](/cli/azure/sql/mi/endpoint-cert#az-sql-mi-endpoint-cert-show) Azure CLI command, such as the following PowerShell sample. 

> [!CAUTION]
> When using the Azure CLI, you'll need to manually add `0x` to the front of the PublicKey output when you use it in subsequent steps. For example, the PublicKey will look like "**0x**3082033E30...". 

Run the following script. Replace:
- `<SubscriptionID>` with your Azure subscription ID. 
- `<ManagedInstanceName>` with the short name of your managed instance. 

```powershell-interactive
# Run in Azure Cloud Shell (select PowerShell console)
# ===============================================================================
# POWERSHELL SCRIPT TO EXPORT MANAGED INSTANCE PUBLIC CERTIFICATE
# ===== Enter user variables here ====

# Enter your managed instance short name – for example, "sqlmi"
$ManagedInstanceName = "<ManagedInstanceName>"

# ==== Do not customize the following cmdlet ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Fetch the public key of the authentication certificate from Managed Instance. Outputs a binary key in the property PublicKey.
Get-AzSqlInstanceEndpointCertificate -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -EndpointType "DATABASE_MIRRORING" | out-string   
```

Copy the entire PublicKey output (starts with `0x`) as you'll require it in the next step.

Alternatively, if you encounter issues in copy-pasting the PublicKey, you could also run the T-SQL command `EXEC sp_get_endpoint_certificate 4` on the managed instance to obtain its public key for the link endpoint.

Next, import the obtained public key of the managed instance security certificate to SQL Server. Run the following query on SQL Server. Replace:
- `<ManagedInstanceFQDN>` with the fully qualified domain name of managed instance.
- `<PublicKey>` with the PublicKey value obtained in the previous step (from Azure Cloud Shell, starting with `0x`). You don't need to use quotation marks.

> [!IMPORTANT]
> The name of the certificate must be the SQL Managed Instance FQDN and should not be modified. The link will not be operational if using a custom name.

```sql
-- Run on SQL Server
USE MASTER
CREATE CERTIFICATE [<ManagedInstanceFQDN>]
FROM BINARY = <PublicKey> 
```

### Import Azure-trusted root certificate authority keys to SQL Server

Importing public root certificate keys of Microsoft and DigiCert certificate authorities (CA) to SQL Server is required for your SQL Server to trust certificates issued by Azure for database.windows.net domains.

> [!CAUTION]
> Ensure the PublicKey starts with an `0x`. You might need to add it manually to the beginning of the PublicKey if it's not already there. 

First, import Microsoft PKI root-authority certificate on SQL Server:

```sql
-- Run on SQL Server
-- Import Microsoft PKI root-authority certificate (trusted by Azure), if not already present
IF NOT EXISTS (SELECT name FROM sys.certificates WHERE name = N'MicrosoftPKI')
BEGIN
    PRINT 'Creating MicrosoftPKI certificate.'
    CREATE CERTIFICATE [MicrosoftPKI] FROM BINARY = 0x308205A830820390A00302010202101ED397095FD8B4B347701EAABE7F45B3

    --Trust certificates issued by Microsoft PKI root authority for Azure database.windows.net domains
    DECLARE @CERTID int
    SELECT @CERTID = CERT_ID('MicrosoftPKI')
    EXEC sp_certificate_add_issuer @CERTID, N'*.database.windows.net'
END
ELSE
    PRINT 'Certificate MicrosoftPKI already exsits.'
GO
```

Then, import DigiCert PKI root-authority certificate on SQL Server:

```sql
-- Run on SQL Server
-- Import DigiCert PKI root-authority certificate trusted by Azure to SQL Server, if not already present
IF NOT EXISTS (SELECT name FROM sys.certificates WHERE name = N'DigiCertPKI')
BEGIN
    PRINT 'Creating DigiCertPKI certificate.'
    CREATE CERTIFICATE [DigiCertPKI] FROM BINARY = 0x3082038E30820276A0030201020210033AF1E6A711A9A0BB2864B11D0

    --Trust certificates issued by DigiCert PKI root authority for Azure database.windows.net domains
    DECLARE @CERTID int
    SELECT @CERTID = CERT_ID('DigiCertPKI')
    EXEC sp_certificate_add_issuer @CERTID, N'*.database.windows.net'
END
ELSE
    PRINT 'Certificate DigiCertPKI already exsits.'
GO
```

Finally, verify all created certificates by using the following dynamic management view (DMV):

```sql
-- Run on SQL Server
SELECT * FROM sys.certificates
```

## Secure the database mirroring endpoint

If you don't have an existing availability group, or a database mirroring endpoint on SQL Server, the next step is to create a database mirroring endpoint on SQL Server and secure it with the previously generated SQL Server certificate. If you do have an existing availability group or mirroring endpoint, skip to the [Alter an existing endpoint](#alter-an-existing-endpoint) section. 

### Create and secure the database mirroring endpoint on SQL Server


To verify that you don't have an existing database mirroring endpoint created, use the following script:

```sql
-- Run on SQL Server
-- View database mirroring endpoints on SQL Server
SELECT * FROM sys.database_mirroring_endpoints WHERE type_desc = 'DATABASE_MIRRORING'
```

If the preceding query doesn't show an existing database mirroring endpoint, run the following script on SQL Server to obtain the name of the earlier generated SQL Server certificate. 

```sql
-- Run on SQL Server
-- Show the name and the public key of generated SQL Server certificate
USE MASTER
GO
DECLARE @sqlserver_certificate_name NVARCHAR(MAX) = N'Cert_' + @@servername  + N'_endpoint'
SELECT @sqlserver_certificate_name as 'SQLServerCertName'
```

Save SQLServerCertName from the output as you'll need it in the next step.

Use the following script to create a new database mirroring endpoint on port 5022 and secure the endpoint with the SQL Server certificate. Replace:
- `<SQL_SERVER_CERTIFICATE>` with the name of SQLServerCertName obtained in the previous step.

```sql
-- Run on SQL Server
-- Create a connection endpoint listener on SQL Server
USE MASTER
CREATE ENDPOINT database_mirroring_endpoint
    STATE=STARTED   
    AS TCP (LISTENER_PORT=5022, LISTENER_IP = ALL)
    FOR DATABASE_MIRRORING (
        ROLE=ALL,
        AUTHENTICATION = CERTIFICATE [<SQL_SERVER_CERTIFICATE>],
        ENCRYPTION = REQUIRED ALGORITHM AES
    )  
GO
```

Validate that the mirroring endpoint was created by running the following script on SQL Server:

```sql
-- Run on SQL Server
-- View database mirroring endpoints on SQL Server
SELECT
    name, type_desc, state_desc, role_desc,
    connection_auth_desc, is_encryption_enabled, encryption_algorithm_desc
FROM 
    sys.database_mirroring_endpoints
```

Successfully created endpoint state_desc column should state `STARTED`.

A new mirroring endpoint was created with certificate authentication and AES encryption enabled.

### Alter an existing endpoint

> [!NOTE]
> Skip this step if you've just created a new mirroring endpoint. Use this step only if you're using existing availability groups with an existing database mirroring endpoint.

If you're using existing availability groups for the link, or if there's an existing database mirroring endpoint, first validate that it satisfies the following mandatory conditions for the link:

- Type must be `DATABASE_MIRRORING`.
- Connection authentication must be `CERTIFICATE`.
- Encryption must be enabled.
- Encryption algorithm must be `AES`.

Run the following query on SQL Server to view details for an existing database mirroring endpoint:

```sql
-- Run on SQL Server
-- View database mirroring endpoints on SQL Server
SELECT
    name, type_desc, state_desc, role_desc, connection_auth_desc,
    is_encryption_enabled, encryption_algorithm_desc
FROM
    sys.database_mirroring_endpoints
```

If the output shows that the existing `DATABASE_MIRRORING` endpoint `connection_auth_desc` isn't `CERTIFICATE`, or `encryption_algorthm_desc` isn't `AES`, the *endpoint needs to be altered to meet the requirements*.

On SQL Server, the same database mirroring endpoint is used for both availability groups and distributed availability groups. If your `connection_auth_desc` endpoint is `NTLM` (Windows authentication) or `KERBEROS`, and you need Windows authentication for an existing availability group, it's possible to alter the endpoint to use multiple authentication methods by switching the authentication option to `NEGOTIATE CERTIFICATE`. This change allows the existing availability group to use Windows authentication, while using certificate authentication for SQL Managed Instance. 

Similarly, if encryption doesn't include AES and you need RC4 encryption, it's possible to alter the endpoint to use both algorithms. For details about possible options for altering endpoints, see the [documentation page for sys.database_mirroring_endpoints](/sql/relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql).

The following script is an example of how to alter your existing database mirroring endpoint on SQL Server. Replace:

- `<YourExistingEndpointName>` with your existing endpoint name. 
- `<SQLServerCertName>` with the name of the generated SQL Server certificate (obtained in one of the earlier steps above). 

Depending on your specific configuration, you might need to customize the script further. You can also use `SELECT * FROM sys.certificates` to get the name of the created certificate on SQL Server.

```sql
-- Run on SQL Server
-- Alter the existing database mirroring endpoint to use CERTIFICATE for authentication and AES for encryption
USE MASTER
ALTER ENDPOINT [<YourExistingEndpointName>]   
    STATE=STARTED   
    AS TCP (LISTENER_PORT=5022, LISTENER_IP = ALL)
    FOR DATABASE_MIRRORING (
        ROLE=ALL,
        AUTHENTICATION = WINDOWS NEGOTIATE CERTIFICATE [<SQLServerCertName>],
        ENCRYPTION = REQUIRED ALGORITHM AES
    )
GO
```

After you run the `ALTER` endpoint query and set the dual authentication mode to Windows and certificate, use this query again on SQL Server to show details for the database mirroring endpoint:

```sql
-- Run on SQL Server
-- View database mirroring endpoints on SQL Server
SELECT
    name, type_desc, state_desc, role_desc, connection_auth_desc,
    is_encryption_enabled, encryption_algorithm_desc
FROM
    sys.database_mirroring_endpoints
```

You've successfully modified your database mirroring endpoint for a SQL Managed Instance link.

## Create an availability group on SQL Server

If you don't have an existing availability group, the next step is to create one on SQL Server, regardless of which will be the initial primary. Commands to create the availability group are different if your SQL Managed Instance is the initial primary, which is only supported starting with [SQL Server 2022 CU10](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate10).

While it's possible to establish multiple links for the same database, the link only supports replication of one database per link. If you want to create multiple links for the same database, use the same availability group for all the links, but then create a new distributed availability group for each database link between SQL Server and SQL Managed Instance. 



### [SQL Server initial primary](#tab/sql-server)

If SQL Server is your initial primary, create an availability group with the following parameters for a link:

- Initial primary server name
- Database name
- A failover mode of `MANUAL`
- A seeding mode of `AUTOMATIC`

First, find out your SQL Server name by running the following T-SQL statement:

```sql
-- Run on the initial primary
SELECT @@SERVERNAME AS SQLServerName 
```



Then, use the following script to create the availability group on SQL Server. Replace:

- `<AGName>` with the name of your availability group. A Managed Instance link requires one database per availability group. For multiple databases, you'll need to create multiple availability groups. Consider naming each availability group so that its name reflects the corresponding database - for example, `AG_<db_name>`. 
- `<DatabaseName>` with the name of database that you want to replicate.
- `<SQLServerName>` with the name of your SQL Server instance obtained in the previous step. 
- `<SQLServerIP>` with the SQL Server IP address. You can use a resolvable SQL Server host machine name as an alternative, but you need to make sure that the name is resolvable from the SQL Managed Instance virtual network.

```sql
-- Run on SQL Server
-- Create the primary availability group on SQL Server
USE MASTER
CREATE AVAILABILITY GROUP [<AGName>]
WITH (CLUSTER_TYPE = NONE) -- <- Delete this line for SQL Server 2016 only. Leave as-is for all higher versions.
    FOR database [<DatabaseName>]  
    REPLICA ON   
        N'<SQLServerName>' WITH   
            (  
            ENDPOINT_URL = 'TCP://<SQLServerIP>:5022',
            AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
            FAILOVER_MODE = MANUAL,
            SEEDING_MODE = AUTOMATIC
            );
GO
```

> [!IMPORTANT]
> For SQL Server 2016, delete `WITH (CLUSTER_TYPE = NONE)` from the above T-SQL statement. Leave as-is for all later SQL Server versions.

Next, create the distributed availability group on SQL Server. If you plan to create multiple links, then you need to create a distributed availability group for each link, even if you're establishing multiple links for the same database. 

Replace the following values and then run the T-SQL script to create your distributed availability group. 

- `<DAGName>` with the name of your distributed availability group. Since you can configure multiple links for the same database by creating a distributed availability group for each link, consider naming each distributed availability group accordingly - for example, `DAG1_<db_name>`, `DAG2_<db_name>`. 
- `<AGName>` with the name of the availability group that you created in the previous step. 
- `<SQLServerIP>` with the IP address of SQL Server from the previous step. You can use a resolvable SQL Server host machine name as an alternative, but make sure the name is resolvable from the SQL Managed Instance virtual network (which requires configuring custom Azure DNS for the subnet of the managed instance). 
- `<ManagedInstanceName>` with the short name of your managed instance. 
- `<ManagedInstanceFQDN>` with the fully qualified domain name of your managed instance.

```sql
-- Run on SQL Server
-- Create a distributed availability group for the availability group and database
-- ManagedInstanceName example: 'sqlmi1'
-- ManagedInstanceFQDN example: 'sqlmi1.73d19f36a420a.database.windows.net'
USE MASTER
CREATE AVAILABILITY GROUP [<DAGName>]
WITH (DISTRIBUTED) 
    AVAILABILITY GROUP ON  
    N'<AGName>' WITH 
    (
      LISTENER_URL = 'TCP://<SQLServerIP>:5022',
      AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
      FAILOVER_MODE = MANUAL,
      SEEDING_MODE = AUTOMATIC,
      SESSION_TIMEOUT = 20
    ),
    N'<ManagedInstanceName>' WITH
    (
      LISTENER_URL = 'tcp://<ManagedInstanceFQDN>:5022;Server=[<ManagedInstanceName>]',
      AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
      FAILOVER_MODE = MANUAL,
      SEEDING_MODE = AUTOMATIC
    );
GO
```

### [SQL MI initial primary](#tab/sql-mi)

If SQL Managed Instance is your initial primary, create the availability group _on SQL Server_ with the following parameters for a link:

- Initial primary server name
- Database name
- A failover mode of `MANUAL`
- A seeding mode of `AUTOMATIC`

First, find out your SQL Server name by running the following T-SQL statement:

```sql
-- Run on the initial primary
SELECT @@SERVERNAME AS SQLServerName 
```

Then, use the following script to create the availability group on SQL Server. Replace:

- `<AGName>` with the name of your availability group. A Managed Instance link requires one database per availability group. For multiple databases, you'll need to create multiple availability groups. Consider naming each availability group so that its name reflects the corresponding database - for example, `AG_<db_name>`. 
- `<DatabaseName>` with the name of database that you want to replicate.
- `<SQLServerName>` with the name of your SQL Server instance obtained in the previous step. 
- `<SQLServerIP>` with the SQL Server IP address. You can use a resolvable SQL Server host machine name as an alternative, but you need to make sure that the name is resolvable from the SQL Managed Instance virtual network.


```sql
-- Run on SQL Server 
-- Create the availability group on SQL Server 

CREATE AVAILABILITY GROUP [<AGName>] 
WITH (CLUSTER_TYPE = NONE) 
FOR  
REPLICA ON N'<SQLServerName>' 
WITH ( 
    ENDPOINT_URL = N'TCP://<SQLServerIP>:5022', 
    FAILOVER_MODE = MANUAL, 
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
    SEEDING_MODE = AUTOMATIC); 

GO 
```

Since SQL Managed Instance is the initial primary, the database will be replicated from SQL Managed Instance to SQL Server. Grant the availability group permission to create the database on SQL Server by running the following script on SQL Server: 

```sql
-- Run on SQL Server 
-- Grant permission to the availability group to create databases 

ALTER AVAILABILITY GROUP [<AGName>] GRANT CREATE ANY DATABASE; 
```

Next, create the distributed availability group _on SQL Server_. If you plan to create multiple links, then you need to create a distributed availability group for each link, even if you're establishing multiple links for the same database. 

Replace the following values and then run the T-SQL script to create your distributed availability group. 

- `<DAGName>` with the name of your distributed availability group. Since you can configure multiple links for the same database by creating a distributed availability group for each link, consider naming each distributed availability group accordingly - for example, `DAG1_<db_name>`, `DAG2_<db_name>`. 
- `<AGName>` with the name of the availability group that you created in the previous step. 
- `<SQLServerIP>` with the IP address of SQL Server from the previous step. You can use a resolvable SQL Server host machine name as an alternative, but make sure the name is resolvable from the SQL Managed Instance virtual network (which requires configuring custom Azure DNS for the subnet of the managed instance). 
- `<ManagedInstanceName>` with the short name of your managed instance. 
- `<ManagedInstanceFQDN>` with the fully qualified domain name of your managed instance.

```sql
-- Run on SQL Server 
-- Create a distributed availability group for the availability group and database 
-- ManagedInstanceName example: 'sqlmi1' 
-- ManagedInstanceFQDN example: 'sqlmi1.73d19f36a420a.database.windows.net' 

USE MASTER 
CREATE AVAILABILITY GROUP [<DAGName>] 
WITH (DISTRIBUTED)  
    AVAILABILITY GROUP ON   
    N'<AGName>' WITH  
    ( 
      LISTENER_URL = 'TCP://<SQLServerIP>:5022', 
      AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT, 
      FAILOVER_MODE = MANUAL, 
      SEEDING_MODE = AUTOMATIC, 
      SESSION_TIMEOUT = 20 
    ), 
    N'<ManagedInstanceName>' WITH 
    ( 
      LISTENER_URL = 'tcp://<ManagedInstanceFQDN>:5022;Server=[<ManagedInstanceName>];Database=[<DatabaseName>]', 
      AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT, 
      FAILOVER_MODE = MANUAL, 
      SEEDING_MODE = AUTOMATIC 
    ); 
GO 
```

---


## Verify availability groups

Use the following script to list all availability groups and distributed availability groups on the SQL Server instance. At this point, the state of your availability group needs to be `connected`, and the state of your distributed availability groups needs to be `disconnected`. The state of the distributed availability group moves to `connected` only once it's joined with SQL Managed Instance. 

```sql
-- Run on SQL Server
-- This will show that the availability group and distributed availability group have been created on SQL Server.
SELECT * FROM sys.availability_groups
```

Alternatively, you can use SSMS Object Explorer to find availability groups and distributed availability groups. Expand the **Always On High Availability** folder and then the **Availability Groups** folder.

## Create a link

Finally, you can create the link. The commands differ based on which instance is the initial primary. Use the [New-AzSqlInstanceLink](/powershell/module/az.sql/new-azsqlinstancelink) PowerShell or [az sql mi link create](/cli/azure/sql/mi/link#az-sql-mi-link-create) Azure CLI command to create the link, such as the PowerShell example in this section. Creating the link from a SQL Managed Instance primary isn't currently supported with the Azure CLI. 

If you need to see all links on a managed instance, use the [Get-AzSqlInstanceLink](/powershell/module/az.sql/get-azsqlinstancelink) PowerShell or [az sql mi link show](/cli/azure/sql/mi/link#az-sql-mi-link-show) Azure CLI command in Azure Cloud Shell. 


### [SQL Server initial primary](#tab/sql-server)

To simplify the process, sign in to the Azure portal and run the following script from the Azure Cloud Shell. Replace:
- `<ManagedInstanceName>` with the short name of your managed instance. 
- `<AGName>` with the name of the availability group created on SQL Server. 
- `<DAGName>` with the name of the distributed availability group created on SQL Server. 
- `<DatabaseName>` with the database replicated in the availability group on SQL Server. 
- `<SQLServerIP>` with the IP address of your SQL Server. The provided IP address must be accessible by managed instance.
 

```powershell-interactive
#  Run in Azure Cloud Shell (select PowerShell console)
# =============================================================================
# POWERSHELL SCRIPT TO CREATE MANAGED INSTANCE LINK
# Instructs Managed Instance to join distributed availability group on SQL Server
# ===== Enter user variables here ====

# Enter your managed instance name – for example, "sqlmi1"
$ManagedInstanceName = "<ManagedInstanceName>"

# Enter the availability group name that was created on SQL Server
$AGName = "<AGName>"

# Enter the distributed availability group name that was created on SQL Server
$DAGName = "<DAGName>"

# Enter the database name that was placed in the availability group for replication
$DatabaseName = "<DatabaseName>"

# Enter the SQL Server IP
$SQLServerIP = "<SQLServerIP>"

# ==== Do not customize the following cmdlet ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Build properly formatted connection endpoint
$SourceIP = "TCP://" + $SQLServerIP + ":5022"

# Create link on managed instance. Join distributed availability group on SQL Server.
New-AzSqlInstanceLink -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -Name $DAGName |
-PrimaryAvailabilityGroupName $AGName -SecondaryAvailabilityGroupName $ManagedInstanceName |
-TargetDatabase $DatabaseName -SourceEndpoint $SourceIP
```


### [SQL MI initial primary](#tab/sql-mi)

To simplify the process, sign in to the Azure portal and run the following script from the Azure Cloud Shell. Replace:
- `<ManagedInstanceName>` with the short name of your managed instance. 
- `<AGName>` with the name of the availability group created on SQL Server. 
- `<DAGName>` with the name of the distributed availability group created on SQL Server. 
- `<DatabaseName>` with the database replicated in the availability group on SQL Server. 
- `<SQLServerIP>` with the IP address of your SQL Server. The provided IP address must be accessible by managed instance.


```powershell-interactive
#  Run in Azure Cloud Shell (select PowerShell console) 
# ============================================================================= 
# POWERSHELL SCRIPT TO CREATE MANAGED INSTANCE LINK 
# Instructs Managed Instance to join distributed availability group on SQL Server 
# ===== Enter user variables here ====  

# Enter your managed instance name – for example, "sqlmi1" 
$ManagedInstanceName = "<ManagedInstanceName>" 

# Enter the availability group name that was created on SQL Server 
$AGName = "<AGName>" 

# Enter the distributed availability group name that was created on SQL Server 
$DAGName = "<DAGName>"  

# Enter the database name that was placed in the availability group for replication 
$DatabaseName = "<DatabaseName>"  

# Enter the SQL Server IP 
$SQLServerIP = "<SQLServerIP>" 

# Enter the Azure subscription ID 
$SubscriptionID = "<SubscriptionID>" 
 
# ==== Do not customize the following cmdlet ==== 

# Find out the resource group name 
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName 

# Build properly formatted connection endpoint 
$DestinationIP = "TCP://" + $SQLServerIP + ":5022"  

# Create Azure REST API request header 
$azProfile = [Microsoft.Azure.Commands.Common.Authentication.Abstractions.AzureRmProfileProvider]::Instance.Profile 
$currentAzureContext = Get-AzContext 
$profileClient = New-Object Microsoft.Azure.Commands.ResourceManager.Common.RMProfileClient($azProfile) 
$token = $profileClient.AcquireAccessToken($currentAzureContext.Tenant.TenantId) 
$authToken = $token.AccessToken 
$headers = @{ } 
$headers.Add('Authorization', 'Bearer ' + $authToken)  

# Build Azure REST API URI 
$uri = "https://management.azure.com/subscriptions/"+$SubscriptionID+"/resourceGroups/"+$ResourceGroup+"/providers/Microsoft.Sql/managedInstances/"+$ManagedInstanceName+"/distributedAvailabilityGroups/"+$DAGName+"?api-version=2023-05-01-preview" 

# Build Azure REST API request body 
$body = "{ 
'properties': { 
    'Databases': [{ 
            'databaseName': '$DatabaseName' 
        } 
    ], 
    'PartnerEndpoint': '$DestinationIP', 
    'InstanceAvailabilityGroupName': '$ManagedInstanceName', 
    'PartnerAvailabilityGroupName': '$AGName', 
    'FailoverMode': 'Manual', 
    'SeedingMode': 'Automatic', 
    'InstanceLinkRole': 'Primary' 

}}" 

 
# Send link creation request to Azure REST API 
Invoke-RestMethod -Method POST -Headers $headers -Uri $uri -ContentType 'application/json' -Body $body 
```

---

The result of this operation is a time stamp of the successful execution of the create a link request.

## Verify the link

To verify the connection between SQL Managed Instance and SQL Server, run the following query on SQL Server. The connection won't be instantaneous. It can take up to a minute for the DMV to start showing a successful connection. Keep refreshing the DMV until the connection appears as CONNECTED for the SQL Managed Instance replica.

```sql
-- Run on SQL Server
SELECT
    r.replica_server_name AS [Replica],
    r.endpoint_url AS [Endpoint],
    rs.connected_state_desc AS [Connected state],
    rs.last_connect_error_description AS [Last connection error],
    rs.last_connect_error_number AS [Last connection error No],
    rs.last_connect_error_timestamp AS [Last error timestamp]
FROM
    sys.dm_hadr_availability_replica_states rs
    JOIN sys.availability_replicas r
    ON rs.replica_id = r.replica_id
```

After the connection is established, **Object Explorer** in SSMS might initially show the replicated database on the secondary replica in a **Restoring** state as the initial seeding phase moves and restores the full backup of the database. After the database is restored, replication has to catch up to bring the two databases to a synchronized state. The database will no longer be in **Restoring** after initial seeding finishes. Seeding small databases might be fast enough that you won't see the initial **Restoring** state in SSMS.

> [!IMPORTANT]
> - The link won't work unless network connectivity exists between SQL Server and SQL Managed Instance. To troubleshoot network connectivity, follow the steps in [Test network connectivity](managed-instance-link-preparation.md#test-network-connectivity).
> - Take regular backups of the log file on SQL Server. If the used log space reaches 100 percent, replication to SQL Managed Instance stops until space use is reduced. We highly recommend that you automate log backups by setting up a daily job. For details, see [Back up log files on SQL Server](managed-instance-link-best-practices.md#take-log-backups-regularly).


## Stop workload

To fail over your database to the secondary replica, first stop any application workloads on your primary during your maintenance hours. This enables database replication to catch up on the secondary o you can migrate or fail over to Azure without data loss. While the primary database is a part of an Always On availability group, you can't set it to read-only mode. You need to ensure that applications aren't committing transactions to the primary replica prior to failover.

## Switch the replication mode

Replication between SQL Server and SQL Managed Instance is asynchronous by default. Before you fail your database over to the secondary, switch the link to synchronous mode. Synchronous replication across large network distances might slow down transactions on the primary replica.

Switching from async to sync mode requires a replication mode change on both SQL Managed Instance and SQL Server.

### Switch replication mode (SQL Managed Instance)

Use either Azure PowerShell or the Azure CLI to switch the replication mode on SQL Managed Instance. 

First, ensure that you're logged in to Azure and that you've selected the subscription where your managed instance is hosted by using the [Select-AzSubscription](/powershell/module/servicemanagement/azure/select-azuresubscription) PowerShell or [az account set](/cli/azure/account#az-account-set) Azure CLI command. Selecting the proper subscription is especially important if you have more than one Azure subscription on your account. 

In the following PowerShell sample, replace `<SubscriptionID>` with your Azure subscription ID. 

```powershell-interactive
# Run in Azure Cloud Shell (select PowerShell console)

# Enter your Azure subscription ID
$SubscriptionID = "<SubscriptionID>"

# Login to Azure and select subscription ID
if ((Get-AzContext ) -eq $null)
{
    echo "Logging to Azure subscription"
    Login-AzAccount
}
Select-AzSubscription -SubscriptionName $SubscriptionID
```

Ensure you know the name of the link you would like to fail over. You can use the [Get-AzSqlInstanceLink](/powershell/module/az.sql/get-azsqlinstancelink) PowerShell or [az sql mi link list](/cli/azure/sql/mi/link#az-sql-mi-link-list) Azure CLI command. 

Use the following PowerShell script to list all active links on the SQL Managed Instance. Replace `<ManagedInstanceName>` with the short name of your managed instance. 
 
```powershell-interactive
# Run in Azure Cloud Shell (select PowerShell console)
# =============================================================================
# POWERSHELL SCRIPT TO LIST ALL LINKS ON MANAGED INSTANCE
# ===== Enter user variables here ====

# Enter your managed instance name – for example, "sqlmi1"
$ManagedInstanceName = "<ManagedInstanceName>"

# ==== Do not customize the following cmdlet ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# List all links on the specified managed instance
Get-AzSqlInstanceLink -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName 
```

From the output of the previous script, record the `Name` property of the link you'd like to fail over.

Then, switch the replication mode from async to sync on SQL Managed Instance for the identified link by using the [Update-AzSqlInstanceLink](/powershell/module/az.sql/update-azsqlinstancelink) PowerShell or [az sql mi link update](/cli/azure/sql/mi/link#az-sql-mi-link-update) Azure CLI command.

In the following PowerShell sample, replace:
- `<ManagedInstanceName>` with the short name of your managed instance. 
- `<DAGName>` with the name of the link you found out on the previous step (the `Name` property from the previous step).

```powershell-interactive
# Run in Azure Cloud Shell (select PowerShell console)
# =============================================================================
# POWERSHELL SCRIPT TO SWITCH LINK REPLICATION MODE (ASYNC\SYNC)
# ===== Enter user variables here ====

# Enter the link name 
$LinkName = "<DAGName>"  

# Enter your managed instance name – for example, "sqlmi1" 
$ManagedInstanceName = "<ManagedInstanceName>" 

# ==== Do not customize the following cmdlet ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Update replication mode of the specified link
Update-AzSqlInstanceLink -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName |
-Name $LinkName -ReplicationMode "Sync"
```

The previous command indicates success by displaying a summary of the operation, with the property `ReplicationMode` shown as `Sync`.

If you need to revert the operation, execute the previous script to switch the replication mode but replace the `Sync` string in the `-ReplicationMode` to `Async`.

### Switch replication mode (SQL Server)

Use the following T-SQL script on SQL Server to change the replication mode of the distributed availability group on SQL Server from async to sync. Replace:
- `<DAGName>` with the name of the distributed availability group (used to create the link).
- `<AGName>` with the name of the availability group created on SQL Server (used to create the link). 
- `<ManagedInstanceName>` with the name of your managed instance.

```sql
-- Run on SQL Server
-- Sets the distributed availability group to a synchronous commit.
-- ManagedInstanceName example: 'sqlmi1'
USE master
GO
ALTER AVAILABILITY GROUP [<DAGName>] 
MODIFY 
AVAILABILITY GROUP ON
    '<AGName>' WITH
    (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT),
    '<ManagedInstanceName>' WITH
    (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
```

To confirm that you've changed the link's replication mode successfully, use the following dynamic management view. Results indicate the `SYNCHRONOUS_COMIT` state.

```sql
-- Run on SQL Server
-- Verifies the state of the distributed availability group
SELECT
    ag.name, ag.is_distributed, ar.replica_server_name,
    ar.availability_mode_desc, ars.connected_state_desc, ars.role_desc,
    ars.operational_state_desc, ars.synchronization_health_desc
FROM
    sys.availability_groups ag
    join sys.availability_replicas ar
    on ag.group_id=ar.group_id
    left join sys.dm_hadr_availability_replica_states ars
    on ars.replica_id=ar.replica_id
WHERE
    ag.is_distributed=1
```

Now that you've switched both SQL Managed Instance and SQL Server to sync mode, replication between the two instances is synchronous. If you need to reverse this state, follow the same steps and set the state to `async` for both SQL Server and SQL Managed Instance.

## Check LSN values on both SQL Server and SQL Managed Instance

To complete the failover or migration, confirm that replication has finished. For this, ensure the log sequence numbers (LSNs) in the log records for both SQL Server and SQL Managed Instance are the same. 

Initially, it's expected that the LSN on the primary will be higher than the LSN on the secondary. Network latency might cause replication to lag somewhat behind the primary. Because the workload has been stopped on the primary, you should expect the LSNs to match and stop changing after some time. 

Use the following T-SQL query on SQL Server to read the LSN of the last recorded transaction log. Replace:
- `<DatabaseName>` with your database name and look for the last hardened LSN number.

```sql
-- Run on SQL Server
-- Obtain the last hardened LSN for the database on SQL Server.
SELECT
    ag.name AS [Replication group],
    db.name AS [Database name], 
    drs.database_id AS [Database ID], 
    drs.group_id, 
    drs.replica_id, 
    drs.synchronization_state_desc AS [Sync state], 
    drs.end_of_log_lsn AS [End of log LSN],
    drs.last_hardened_lsn AS [Last hardened LSN] 
FROM
    sys.dm_hadr_database_replica_states drs
    inner join sys.databases db on db.database_id = drs.database_id
    inner join sys.availability_groups ag on drs.group_id = ag.group_id
WHERE
    ag.is_distributed = 1 and db.name = '<DatabaseName>'
```

Use the following T-SQL query on SQL Managed Instance to read the last hardened LSN for your database. Replace `<DatabaseName>` with your database name.

This query works on a General Purpose SQL Managed Instance. For a Business Critical SQL Managed Instance, uncomment `and drs.is_primary_replica = 1` at the end of the script. On the Business Critical service tier, this filter ensures that details are only read from the primary replica.

```sql
-- Run on SQL managed instance
-- Obtain the LSN for the database on SQL Managed Instance.
SELECT
    db.name AS [Database name],
    drs.database_id AS [Database ID], 
    drs.group_id, 
    drs.replica_id, 
    drs.synchronization_state_desc AS [Sync state],
    drs.end_of_log_lsn AS [End of log LSN],
    drs.last_hardened_lsn AS [Last hardened LSN]
FROM
    sys.dm_hadr_database_replica_states drs
    inner join sys.databases db on db.database_id = drs.database_id
WHERE
    db.name = '<DatabaseName>'
    -- for Business Critical, add the following as well
    -- AND drs.is_primary_replica = 1
```

Alternatively, you could also use the [Get-AzSqlInstanceLink](/powershell/module/az.sql/get-azsqlinstancelink) PowerShell or [az sql mi link show](/cli/azure/sql/mi/link#az-sql-mi-link-show) Azure CLI command to fetch the `LastHardenedLsn` property for your link on SQL Managed Instance to provide the same information as the previous T-SQL query.


> [!IMPORTANT]
> Verify once again that your workload is stopped on the primary. Check that LSNs on both SQL Server and SQL Managed Instance match, and that they **remain matched** and unchanged for some time. Stable LSNs on both instances indicate the tail log has been replicated to the secondary and the workload is effectively stopped.

## Fail over a database

If you want to use PowerShell to fail over a database between SQL Server 2022 and SQL Managed Instance while still maintaining the link, or to perform a failover with data loss for any version of SQL Server,  use the [**Failover between SQL Server and Managed Instance** wizard in SSMS](managed-instance-link-configure-how-to-ssms.md#fail-over-a-database) to generate the script for your environment. You can perform a planned failover from either the primary or the secondary replica. To do a forced failover, connect to the secondary replica.

To break the link and stop replication when you fail over or migrate your database regardless of SQL Server version, use the [Remove-AzSqlInstanceLink](/powershell/module/az.sql/remove-azsqlinstancelink) PowerShell or [az sql mi link delete](/cli/azure/sql/mi/link#az-sql-mi-link-delete) Azure CLI command. 


> [!CAUTION]
> - Before failing over, stop the workload on the source database to allow the replicated database to completely catch up and failover without data loss. If you perform a forced failover, or if you break the link before LSNs match, you might lose data.
> - Failing over a database in SQL Server 2019 and earlier versions breaks and removes the link between the two replicas. You can't fail back to the initial primary.
> - Failing over a database while maintaining the link with SQL Server 2022 is currently in preview. 

The following sample script breaks the link and ends replication between your replicas, making the database read/write on both instances. Replace:
- `<ManagedInstanceName>` with the name of your managed instance. 
- `<DAGName>` with the name of the link you're failing over (output of the property `Name` from `Get-AzSqlInstanceLink` command executed earlier above).

```powershell-interactive
# Run in Azure Cloud Shell (select PowerShell console) 
# =============================================================================
# POWERSHELL SCRIPT TO FAIL OVER OR MIGRATE DATABASE TO AZURE
# ===== Enter user variables here ====

# Enter your managed instance name – for example, "sqlmi1"
$ManagedInstanceName = "<ManagedInstanceName>"
$LinkName = "<DAGName>"

# ==== Do not customize the following cmdlet ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Failover the specified link
Remove-AzSqlInstanceLink -ResourceGroupName $ResourceGroup |
-InstanceName $ManagedInstanceName -Name $LinkName -Force
```

When failover succeeds, the link is dropped and no longer exists. The SQL Server database and SQL Managed Instance database can both execute a read/write workload. They're completely independent. Repoint your application connection string to the database you want to actively use. 

> [!IMPORTANT]
> After successful fail over to SQL Managed Instance, manually repoint your application(s) connection string to the SQL managed instance FQDN to complete the migration or fail over process and continue running in Azure.


## Clean up availability groups

Since failing over with SQL Server 2022 doesn't break the link, you can choose to leave the link and availability groups in place. 

If you decide to break the link, or if you're failing over with SQL Server 2019 and earlier versions, you must drop the distributed availability group to remove link metadata from SQL Server. However, you can choose to keep the availability group on SQL Server. 

To clean up your availability group resources, replace the following values and then run the sample code: In the following code, replace:

- `<DAGName>` with the name of the distributed availability group on SQL Server (used to create the link). 
- `<AGName>` with the name of the availability group on SQL Server (used to create the link).

``` sql
-- Run on SQL Server
USE MASTER
GO
DROP AVAILABILITY GROUP <DAGName> --mandatory
GO
-- DROP AVAILABILITY GROUP <AGName> --optional
-- GO
```

## Troubleshoot 

The section provides guidance to address issues with configuring and using the link. 

### Errors 

If you encounter an error message when you create the link or fail over a database, review the error message in the query output window for more information.

If you encounter an error when working with the link, the query stops executing at the failed step. After the error condition is resolved, rerun the command again to proceed with your action.

### Inconsistent state after forced failover 

Using forced failover can result in an inconsistent state between the primary and secondary replicas, causing a split brain scenario from both replicas being in the same role. Data replication fails in this state until the user resolves the situation by manually designating one replica as primary and the other replica as secondary.


## Related content

For more information on the link feature, see the following resources:

- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Prepare your environment for a Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)
