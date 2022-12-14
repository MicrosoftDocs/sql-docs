---
title: Replicate a database with the link via T-SQL & PowerShell scripts
titleSuffix: Azure SQL Managed Instance
description: Learn how to use a Managed Instance link with T-SQL and PowerShell scripts to replicate a database from SQL Server to Azure SQL Managed Instance.
author: sasapopo
ms.author: sasapopo
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
---

# Replicate a database with the link feature via T-SQL and PowerShell scripts - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to use Transact-SQL (T-SQL) and PowerShell scripts to replicate your database from SQL Server to Azure SQL Managed Instance by using a [Managed Instance link](managed-instance-link-feature-overview.md).

> [!NOTE]
> - Some functionality of the link is generally available, while some is currently in preview. Review the [requirements](managed-instance-link-feature-overview.md#requirements) to learn more. 
> - You can also use a [SQL Server Management Studio (SSMS) wizard](managed-instance-link-use-ssms-to-replicate-database.md) to set up the link to replicate your database. 

## Prerequisites 

To replicate your databases to SQL Managed Instance, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#requirements) with required service update installed.
- Azure SQL Managed Instance. [Get started](instance-create-quickstart.md) if you don't have it. 
- PowerShell module [Az.SQL 3.9.0](https://www.powershellgallery.com/packages/Az.Sql), or higher
- A properly [prepared environment](managed-instance-link-preparation.md).

## Set up database recovery and backup

All databases that will be replicated via the link must be in full recovery mode and have at least one backup. Run the following code on SQL Server for all databases you wish to replicate. Replace `<DatabaseName>` with your actual database name.

```sql
-- Run on SQL Server
-- Set full recovery mode for all databases you want to replicate.
ALTER DATABASE [<DatabaseName>] SET RECOVERY FULL
GO

-- Execute backup for all databases you want to replicate.
BACKUP DATABASE [<DatabaseName>] TO DISK = N'<DiskPath>'
GO
```

For more information, see [Create a Full Database Backup](/sql/relational-databases/backup-restore/create-a-full-database-backup-sql-server).

## Replicate a database

Use the following instructions to manually set up the link between your SQL Server instance and managed instance. After the link is created, your source database gets a read-only replica copy on your target managed instance. 

> [!NOTE]
> The link supports replication of user databases only. Replication of system databases is not supported. To replicate instance-level objects (stored in master or msdb databases), we recommend that you script them out and run T-SQL scripts on the destination instance.

## Terminology and naming conventions

As you run scripts from this user guide, it's important not to mistake SQL Server and SQL Managed Instance names for their fully qualified domain names (FQDNs). The following table explains what the various names exactly represent and how to obtain their values:

| Terminology	| Description | How to find out |
| :----| :------------- | :------------- | 
| SQL Server name | Short, single-word SQL Server name. For example: *sqlserver1*. | Run `SELECT @@SERVERNAME` from T-SQL. |
| SQL Server FQDN | Fully qualified domain name (FQDN) of your SQL Server. For example: *sqlserver1.domain.com*. | See your network (DNS) configuration on-premises, or the server name if you're using an Azure virtual machine (VM). |
| SQL Managed Instance name | Short, single-word SQL Managed Instance name. For example: *managedinstance1*. | See the name of your managed instance in the Azure portal. |
| SQL Managed Instance FQDN | Fully qualified domain name (FQDN) of your SQL Managed Instance. For example: *managedinstance1.6d710bcf372b.database.windows.net*. | See the host name on the SQL Managed Instance overview page in the Azure portal. |
| Resolvable domain name | DNS name that can be resolved to an IP address. For example, running `nslookup sqlserver1.domain.com` should return an IP address such as 10.0.0.1. | Run `nslookup` command from the command prompt. |
| SQL Server IP | IP address of your SQL Server. In case of multiple IPs on SQL Server, choose IP address that is accessible from Azure. | Run `ipconfig` command from the command prompt of host OS running the SQL Server. |

## Establish trust between instances

The first step in setting up a link is to establish trust between the two instances and secure the endpoints that are used to communicate and encrypt data across the network. Distributed availability groups use the existing availability group [database mirroring endpoint](/sql/database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server), rather than having their own dedicated endpoint. This is why security and trust need to be configured between the two entities through the availability group database mirroring endpoint.

> [!NOTE]
> The link is based on the Always On technology. Database mirroring endpoint is a special-purpose endpoint that is used exclusively by Always On to receive connections from other server instances. The term database mirroring endpoint should not be mistaken with, and it's not the same as the legacy SQL Server database mirroring feature.

Certificate-based trust is the only supported way to secure database mirroring endpoints on SQL Server and SQL Managed Instance. If you've existing availability groups that use Windows authentication, you need to add certificate-based trust to the existing mirroring endpoint as a secondary authentication option. You can do this by using the `ALTER ENDPOINT` statement, as shown further in this article.

> [!IMPORTANT]
> Certificates are generated with an expiration date and time. They must be renewed and rotated before they expire.

Here's an overview of the process to secure database mirroring endpoints for both SQL Server and SQL Managed Instance:

1. Generate a certificate on SQL Server and obtain its public key.
1. Obtain a public key of the SQL Managed Instance certificate.
1. Exchange the public keys between SQL Server and SQL Managed Instance.
1. Import Azure-trusted root certificate authority keys to SQL Server

The following sections describe these steps in detail.

### Create a certificate on SQL Server and import its public key to SQL Managed Instance

First, create database master key in the master database, if not already present. Insert your password in place of `<strong_password>` in the script below, and keep it in a confidential and secure place. Run this T-SQL script on SQL Server:

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

Then, generate an authentication certificate on SQL Server. In the script below replace:
- `@cert_expiry_date` with the desired certificate expiration date (future date).

Record this date and set a self-reminder to rotate (update) SQL server certificate before its expiry to ensure continuous operation of the link.

> [!IMPORTANT]
> It is strongly recommended to use the auto-generated certificate name from this script. While customizing your own certificate name on SQL Server is allowed, this name should not contain any `\` characters.

```sql
-- Create the SQL Server certificate for the instance link
USE MASTER

-- Customize SQL Server certificate expiration date by adjusting the date below
DECLARE @cert_expiry_date AS varchar(max)='03/30/2025'

-- Build the query to generate the certificate
DECLARE @sqlserver_certificate_name NVARCHAR(MAX) = N'Cert_' + @@servername  + N'_endpoint'
DECLARE @sqlserver_certificate_subject NVARCHAR(MAX) = N'Certificate for ' + @sqlserver_certificate_name
DECLARE @create_sqlserver_certificate_command NVARCHAR(MAX) = N'CREATE CERTIFICATE [' + @sqlserver_certificate_name + '] ' + char (13) +
'	WITH SUBJECT = ''' + @sqlserver_certificate_subject + ''',' + char (13) +
'	EXPIRY_DATE = '''+ @cert_expiry_date + ''''+ char (13)
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

Then, use the following T-SQL query on SQL Server to verify that the certificate has been created:

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

Save values of `SQLServerCertName` and `SQLServerPublicKey` from the output, because you'll need it for the next step.

For the next step, use PowerShell with the installed [Az.Sql module 3.9.0](https://www.powershellgallery.com/packages/Az.Sql), or higher. Or preferably, use [Azure Cloud Shell](/azure/cloud-shell/overview) online from the web browser to run the commands, because it's always updated with the latest module versions.

First, ensure that you're logged in to Azure and that you've selected the subscription where your managed instance is hosted. Selecting the proper subscription is especially important if you have more than one Azure subscription on your account. Replace:
- `<SubscriptionID>` with your Azure subscription ID. 

```powershell
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

Then, run the following script in Azure Cloud Shell (PowerShell console). Fill out necessary user information, copy it, paste it, and then run the script. Replace:
- `<SQLServerPublicKey>` with the public portion of the SQL Server certificate in binary format, which you've recorded in the previous step. It's a long string value that starts with `0x`.
- `<SQLServerCertName>` with the SQL Server certificate name you've recorded in the previous step.
- `<ManagedInstanceName>` with the short name of your managed instance. 

```powershell
# Run in Azure Cloud Shell (select PowerShell console)
# ===============================================================================
# POWERSHELL SCRIPT TO IMPORT SQL SERVER PUBLIC CERTIFICATE TO MANAGED INSTANCE
# ===== Enter user variables here ====

# Enter the name for the server SQLServerCertName certificate – for example, "Cert_sqlserver1_endpoint"
$CertificateName = "<SQLServerCertName>"

# Insert the certificate public key blob that you got from SQL Server – for example, "0x1234567..."
$PublicKeyEncoded = "<SQLServerPublicKey>"

# Enter your managed instance short name – for example, "sqlmi"
$ManagedInstanceName = "<ManagedInstanceName>"

# ==== Do not customize the below ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Upload the public key of the authentication certificate from SQL Server to Azure.
New-AzSqlInstanceServerTrustCertificate -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -Name $CertificateName -PublicKey $PublicKeyEncoded 
```

The result of this operation will be a summary of the uploaded SQL Server certificate to Azure.

In case this is needed, to see all SQL Server certificates uploaded on a managed instance, use [Get-AzSqlInstanceServerTrustCertificate](/powershell/module/az.sql/get-azsqlinstanceservertrustcertificate) PowerShell command in Azure Cloud Shell. To remove SQL Server certificate uploaded on a managed instance, use [Remove-AzSqlInstanceServerTrustCertificate](/powershell/module/az.sql/remove-azsqlinstanceservertrustcertificate) PowerShell command in Azure Cloud Shell.

### Get the certificate public key from SQL Managed Instance and import it to SQL Server

The certificate for securing the link endpoint is automatically generated on Azure SQL Managed Instance. This section describes how to get the certificate public key from SQL Managed Instance, and how to import it to SQL Server.

Run the following script in Azure Cloud Shell. Replace:
- `<SubscriptionID>` with your Azure subscription ID. 
- `<ManagedInstanceName>` with the short name of your managed instance. 

```powershell
# Run in Azure Cloud Shell (select PowerShell console)
# ===============================================================================
# POWERSHELL SCRIPT TO EXPORT MANAGED INSTANCE PUBLIC CERTIFICATE
# ===== Enter user variables here ====

# Enter your managed instance short name – for example, "sqlmi"
$ManagedInstanceName = "<ManagedInstanceName>"

# ==== Do not customize the below ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Fetch the public key of the authentication certificate from Managed Instance. Outputs a binary key in the property PublicKey.
Get-AzSqlInstanceEndpointCertificate -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -EndpointType "DATABASE_MIRRORING" | out-string   
```

Copy the entire PublicKey output (starts with `0x`) from the Azure Cloud Shell as you'll require it in the next step.

Alternatively, if you encounter issues in copy-pasting the PublicKey from Azure Cloud Shell console, you could also run T-SQL command `EXEC sp_get_endpoint_certificate 4` on managed instance to obtain its public key for the link endpoint.

Next, import the obtained public key of managed instance security certificate to SQL Server. Run the following query on SQL Server. Replace:
- `<ManagedInstanceFQDN>` with the fully qualified domain name of managed instance.
- `<PublicKey>` with the PublicKey value obtained in the previous step (from Azure Cloud Shell, starting with `0x`). You don't need to use quotation marks.

> [!IMPORTANT]
> The name of the certificate must be SQL Managed Instance FQDN and should not be modified. The link will not be operational if using a custom name.

```sql
-- Run on SQL Server
USE MASTER
CREATE CERTIFICATE [<ManagedInstanceFQDN>]
FROM BINARY = <PublicKey> 
```

### Import Azure-trusted root certificate authority keys to SQL Server

Importing public root certificate keys of Microsoft and DigiCert certificate authorities (CA) to SQL Server is required for your SQL Server to trust certificates issued by Azure for database.windows.net domains.

First, import Microsoft PKI root-authority certificate on SQL Server:

```sql
-- Run on SQL Server
-- Import Microsoft PKI root-authority certificate (trusted by Azure), if not already present
IF NOT EXISTS (SELECT name FROM sys.certificates WHERE name = N'MicrosoftPKI')
BEGIN
	PRINT 'Creating MicrosoftPKI certificate.'
	CREATE CERTIFICATE [MicrosoftPKI] FROM BINARY = 0x308205A830820390A00302010202101ED397095FD8B4B347701EAABE7F45B3300D06092A864886F70D01010C05003065310B3009060355040613025553311E301C060355040A13154D6963726F736F667420436F72706F726174696F6E313630340603550403132D4D6963726F736F66742052534120526F6F7420436572746966696361746520417574686F726974792032303137301E170D3139313231383232353132325A170D3432303731383233303032335A3065310B3009060355040613025553311E301C060355040A13154D6963726F736F667420436F72706F726174696F6E313630340603550403132D4D6963726F736F66742052534120526F6F7420436572746966696361746520417574686F72697479203230313730820222300D06092A864886F70D01010105000382020F003082020A0282020100CA5BBE94338C299591160A95BD4762C189F39936DF4690C9A5ED786A6F479168F8276750331DA1A6FBE0E543A3840257015D9C4840825310BCBFC73B6890B6822DE5F465D0CC6D19CC95F97BAC4A94AD0EDE4B431D8707921390808364353904FCE5E96CB3B61F50943865505C1746B9B685B51CB517E8D6459DD8B226B0CAC4704AAE60A4DDB3D9ECFC3BD55772BC3FC8C9B2DE4B6BF8236C03C005BD95C7CD733B668064E31AAC2EF94705F206B69B73F578335BC7A1FB272AA1B49A918C91D33A823E7640B4CD52615170283FC5C55AF2C98C49BB145B4DC8FF674D4C1296ADF5FE78A89787D7FD5E2080DCA14B22FBD489ADBACE479747557B8F45C8672884951C6830EFEF49E0357B64E798B094DA4D853B3E55C428AF57F39E13DB46279F1EA25E4483A4A5CAD513B34B3FC4E3C2E68661A45230B97A204F6F0F3853CB330C132B8FD69ABD2AC82DB11C7D4B51CA47D14827725D87EBD545E648659DAF5290BA5BA2186557129F68B9D4156B94C4692298F433E0EDF9518E4150C9344F7690ACFC38C1D8E17BB9E3E394E14669CB0E0A506B13BAAC0F375AB712B590811E56AE572286D9C9D2D1D751E3AB3BC655FD1E0ED3740AD1DAAAEA69B897288F48C407F852433AF4CA55352CB0A66AC09CF9F281E1126AC045D967B3CEFF23A2890A54D414B92AA8D7ECF9ABCD255832798F905B9839C40806C1AC7F0E3D00A50203010001A3543052300E0603551D0F0101FF040403020186300F0603551D130101FF040530030101FF301D0603551D0E0416041409CB597F86B2708F1AC339E3C0D9E9BFBB4DB223301006092B06010401823715010403020100300D06092A864886F70D01010C05000382020100ACAF3E5DC21196898EA3E792D69715B813A2A6422E02CD16055927CA20E8BAB8E81AEC4DA89756AE6543B18F009B52CD55CD53396D624C8B0D5B7C2E44BF83108FF3538280C34F3AC76E113FE6E3169184FB6D847F3474AD89A7CEB9D7D79F846492BE95A1AD095333DDEE0AEA4A518E6F55ABBAB59446AE8C7FD8A2502565608046DB3304AE6CB598745425DC93E4F8E355153DB86DC30AA412C169856EDF64F15399E14A75209D950FE4D6DC03F15918E84789B2575A94B6A9D8172B1749E576CBC156993A37B1FF692C919193E1DF4CA337764DA19FF86D1E1DD3FAECFBF4451D136DCFF759E52227722B86F357BB30ED244DDC7D56BBA3B3F8347989C1E0F20261F7A6FC0FBB1C170BAE41D97CBD27A3FD2E3AD19394B1731D248BAF5B2089ADB7676679F53AC6A69633FE5392C846B11191C6997F8FC9D66631204110872D0CD6C1AF3498CA6483FB1357D1C1F03C7A8CA5C1FD9521A071C193677112EA8F880A691964992356FBAC2A2E70BE66C40C84EFE58BF39301F86A9093674BB268A3B5628FE93F8C7A3B5E0FE78CB8C67CEF37FD74E2C84F3372E194396DBD12AFBE0C4E707C1B6F8DB332937344166DE8F4F7E095808F965D38A4F4ABDE0A308793D84D00716245274B3A42845B7F65B76734522D9C166BAAA8D87BA3424C71C70CCA3E83E4A6EFB701305E51A379F57069A641440F86B02C91C63DEAAE0F84

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
-- Execute on SQL Server
-- Import DigiCert PKI root-authority certificate trusted by Azure to SQL Server, if not already present
IF NOT EXISTS (SELECT name FROM sys.certificates WHERE name = N'DigiCertPKI')
BEGIN
	PRINT 'Creating DigiCertPKI certificate.'
	CREATE CERTIFICATE [DigiCertPKI] FROM BINARY = 0x3082038E30820276A0030201020210033AF1E6A711A9A0BB2864B11D09FAE5300D06092A864886F70D01010B05003061310B300906035504061302555331153013060355040A130C446967694365727420496E6331193017060355040B13107777772E64696769636572742E636F6D3120301E06035504031317446967694365727420476C6F62616C20526F6F74204732301E170D3133303830313132303030305A170D3338303131353132303030305A3061310B300906035504061302555331153013060355040A130C446967694365727420496E6331193017060355040B13107777772E64696769636572742E636F6D3120301E06035504031317446967694365727420476C6F62616C20526F6F7420473230820122300D06092A864886F70D01010105000382010F003082010A0282010100BB37CD34DC7B6BC9B26890AD4A75FF46BA210A088DF51954C9FB88DBF3AEF23A89913C7AE6AB061A6BCFAC2DE85E092444BA629A7ED6A3A87EE054752005AC50B79C631A6C30DCDA1F19B1D71EDEFDD7E0CB948337AEEC1F434EDD7B2CD2BD2EA52FE4A9B8AD3AD499A4B625E99B6B00609260FF4F214918F76790AB61069C8FF2BAE9B4E992326BB5F357E85D1BCD8C1DAB95049549F3352D96E3496DDD77E3FB494BB4AC5507A98F95B3B423BB4C6D45F0F6A9B29530B4FD4C558C274A57147C829DCD7392D3164A060C8C50D18F1E09BE17A1E621CAFD83E510BC83A50AC46728F67314143D4676C387148921344DAF0F450CA649A1BABB9CC5B1338329850203010001A3423040300F0603551D130101FF040530030101FF300E0603551D0F0101FF040403020186301D0603551D0E041604144E2254201895E6E36EE60FFAFAB912ED06178F39300D06092A864886F70D01010B05000382010100606728946F0E4863EB31DDEA6718D5897D3CC58B4A7FE9BEDB2B17DFB05F73772A3213398167428423F2456735EC88BFF88FB0610C34A4AE204C84C6DBF835E176D9DFA642BBC74408867F3674245ADA6C0D145935BDF249DDB61FC9B30D472A3D992FBB5CBBB5D420E1995F534615DB689BF0F330D53E31E28D849EE38ADADA963E3513A55FF0F970507047411157194EC08FAE06C49513172F1B259F75F2B18E99A16F13B14171FE882AC84F102055D7F31445E5E044F4EA879532930EFE5346FA2C9DFF8B22B94BD90945A4DEA4B89A58DD1B7D529F8E59438881A49E26D56FADDD0DC6377DED03921BE5775F76EE3C8DC45D565BA2D9666EB33537E532B6

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

## Create a mirroring endpoint on SQL Server

If you don't have an existing availability group, or a mirroring endpoint on SQL Server, the next step is to create a mirroring endpoint on SQL Server and secure it with earlier generated SQL Server certificate. If you do have an existing availability group or mirroring endpoint, go straight to the next section, [Alter an existing endpoint](#alter-an-existing-endpoint).

To verify that you don't have an existing database mirroring endpoint created, use the following script:

```sql
-- Run on SQL Server
-- View database mirroring endpoints on SQL Server
SELECT * FROM sys.database_mirroring_endpoints WHERE type_desc = 'DATABASE_MIRRORING'
```

If the preceding query doesn't show an existing database mirroring endpoint, run the following script on SQL Server to obtain name of the earlier generated SQL Server certificate. 

```sql
-- Run on SQL Server
-- Show the name and the public key of generated SQL Server certificate
USE MASTER
GO
DECLARE @sqlserver_certificate_name NVARCHAR(MAX) = N'Cert_' + @@servername  + N'_endpoint'
SELECT @sqlserver_certificate_name as 'SQLServerCertName'
```

Save SQLServerCertName from the output as you'll need it in the next step.

Use the below script to create a new database mirroring endpoint on port 5022 and secure the endpoint with the SQL Server certificate. Replace:
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

On SQL Server, the same database mirroring endpoint is used for both availability groups and distributed availability groups. If your `connection_auth_desc` endpoint is `NTLM` (Windows authentication) or `KERBEROS`, and you need Windows authentication for an existing availability group, it's possible to alter the endpoint to use multiple authentication methods by switching the authentication option to `NEGOTIATE CERTIFICATE`. This change will allow the existing availability group to use Windows authentication, while using certificate authentication for SQL Managed Instance. 

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

If you don't have an existing availability group, the next step is to create one on SQL Server. Create an availability group with the following parameters for a link:

- SQL Server name
- Database name
- A failover mode of `MANUAL`
- A seeding mode of `AUTOMATIC`

First, find out your SQL Server name by running the following T-SQL statement:

```sql
-- Run on SQL Server
SELECT @@SERVERNAME AS SQLServerName 
```

Then, use the following script to create the availability group on SQL Server. Replace:

- `<AGName>` with the name of your availability group. For multiple databases, you'll need to create multiple availability groups. A Managed Instance link requires one database per availability group. Consider naming each availability group so that its name reflects the corresponding database - for example, `AG_<db_name>`. 
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

>[!IMPORTANT]
> For SQL Server 2016, delete `WITH (CLUSTER_TYPE = NONE)` from the above T-SQL statement. Leave as-is for all higher SQL Server versions.

Next, create distributed availability group on SQL Server. In the following code, replace:

- `<DAGName>` with the name of your distributed availability group. When you're replicating several databases, you need one availability group and one distributed availability group for each database. Consider naming each item accordingly - for example, `DAG_<db_name>`. 
- `<AGName>` with the name of the availability group that you created in the previous step. 
- `<SQLServerIP>` with the IP address of SQL Server from the previous step. You can use a resolvable SQL Server host machine name as an alternative, but make sure that the name is resolvable from the SQL Managed Instance virtual network (requires configuration of custom Azure DNS for managed instance's subnet). 
- `<ManagedInstanceName>` with the short name of your managed instance. 
- `<ManagedInstnaceFQDN>` with the fully qualified domain name of your managed instance.

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

### Verify availability groups

Use the following script to list all availability groups and distributed availability groups on the SQL Server instance. At this point, the state of your availability group needs to be `connected`, and the state of your distributed availability groups needs to be `disconnected`. The state of the distributed availability group will move to `connected` only when it has been joined with SQL Managed Instance. 

```sql
-- Run on SQL Server
-- This will show that the availability group and distributed availability group have been created on SQL Server.
SELECT * FROM sys.availability_groups
```

Alternatively, you can use SSMS Object Explorer to find availability groups and distributed availability groups. Expand the **Always On High Availability** folder and then the **Availability Groups** folder.

## Create a link

The final step of the setup process is to create the link. 

For simplicity of the process, sign in to the Azure portal and run the following PowerShell script from Azure Cloud Shell. Replace:
- `<ManagedInstanceName>` with the short name of your managed instance. 
- `<AGName>` with the name of the availability group created on SQL Server. 
- `<DAGName>` with the name of the distributed availability group created on SQL Server. 
- `<DatabaseName>` with the database replicated in the availability group on SQL Server. 
- `<SQLServerIP>` with the IP address of your SQL Server. The provided IP address must be accessible by managed instance.
 
```powershell
# Run in Azure Cloud Shell
# =============================================================================
# POWERSHELL SCRIPT FOR CREATING MANAGED INSTANCE LINK
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

# ==== Do not customize the below ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Build properly formatted connection endpoint
$SourceIP = "TCP://" + $SQLServerIP + ":5022"

# Create link on managed instance. Join distributed availability group on SQL Server.
New-AzSqlInstanceLink -ResourceGroupName $ResourceGroup -InstanceName $ManagedInstanceName -Name $DAGName -PrimaryAvailabilityGroupName $AGName -SecondaryAvailabilityGroupName $ManagedInstanceName -TargetDatabase $DatabaseName -SourceEndpoint $SourceIP
```

The result of this operation will be a time stamp of the successful execution of the request to create a link.

In case this is needed, to see all links on a managed instance, use [Get-AzSqlInstanceLink](/powershell/module/az.sql/get-azsqlinstancelink) PowerShell command in Azure Cloud Shell. To remove an existing link, use [Remove-AzSqlInstanceLink](/powershell/module/az.sql/remove-azsqlinstancelink) PowerShell command in Azure Cloud Shell.

> [!NOTE]
> The link feature supports one database per link. To replicate multiplate databases on an instance, create a link for each individual database. For example, to replicate 10 databases to SQL Managed Instance, create 10 individual links.

Consider the following:

- The link currently supports replicating one database per availability group. You can replicate multiple databases to SQL Managed Instance by setting up multiple links.
- Collation between SQL Server and SQL Managed Instance should be the same. A mismatch in collation could cause a mismatch in server name casing and prevent a successful connection from SQL Server to SQL Managed Instance.
- Error 1475 indicates that you need to start a new backup chain by creating a full backup without the `COPY ONLY` option.

## Verify the link

To verify that connection has been made between SQL Managed Instance and SQL Server, run the following query on SQL Server. The connection will not be instantaneous. It can take up to a minute for the DMV to start showing a successful connection. Keep refreshing the DMV until the connection appears as CONNECTED for the SQL Managed Instance replica.

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

After the connection is established, the **Managed Instance Databases** view in SSMS initially shows the replicated databases in a **Restoring** state as the initial seeding phase moves and restores the full backup of the database. After the database is restored, replication has to catch up to bring the two databases to a synchronized state. The database will no longer be in **Restoring** after the initial seeding finishes. Seeding small databases might be fast enough that you won't see the initial **Restoring** state in SSMS.

> [!IMPORTANT]
> - The link won't work unless network connectivity exists between SQL Server and SQL Managed Instance. To troubleshoot network connectivity, follow the steps in [Test bidirectional network connectivity](managed-instance-link-preparation.md#test-bidirectional-network-connectivity).
> - Take regular backups of the log file on SQL Server. If the used log space reaches 100 percent, replication to SQL Managed Instance stops until space use is reduced. We highly recommend that you automate log backups by setting up a daily job. For details, see [Back up log files on SQL Server](managed-instance-link-best-practices.md#take-log-backups-regularly).

## Next steps

For more information on the link feature, see the following resources:

- [Managed Instance link – connecting SQL Server to Azure reimagined](https://aka.ms/mi-link-techblog)
- [Prepare your environment for a Managed Instance link](./managed-instance-link-preparation.md)
- [Use a Managed Instance link with scripts to migrate a database](./managed-instance-link-use-scripts-to-failover-database.md)
- [Use a Managed Instance link via SSMS to replicate a database](./managed-instance-link-use-ssms-to-replicate-database.md)
- [Use a Managed Instance link via SSMS to migrate a database](./managed-instance-link-use-ssms-to-failover-database.md)
