---
title: Prepare Environment for a Managed Instance Link Migration
titleSuffix: SQL Server migration in Azure Arc
description: Prepare your SQL Server instance enabled by Azure Arc for migration to Azure SQL Managed Instance by using the Managed Instance link.
author: danimir
ms.author: danil
ms.reviewer: randolphwest, mathoma
ms.date: 06/25/2026
ms.topic: how-to
---

# Prepare environment for a Managed Instance link migration - SQL Server migration in Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article helps you prepare your environment for a [Managed Instance link migration](migrate-to-azure-sql-managed-instance.md#integrated-migration-methods) of your SQL Server instance enabled by Azure Arc to [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/sql-managed-instance-paas-overview) in the Azure portal.

With the link, you can migrate your SQL Server databases to Azure SQL Managed Instance by using real-time replication with a distributed availability group (online migration):

:::image type="content" source="media/migrate-to-azure-sql-managed-instance/mi-link-migration-method.png" alt-text="Diagram showing Managed Instance link migration.":::

> [!NOTE]  
> - You can provide feedback about your migration experience [directly to the product group](https://aka.ms/arc-migrations-feedback).
> - Migrate up to 10 databases at a time starting with Azure Extension for SQL Server version `1.1.3348.364`.

## Prerequisites

To migrate your SQL Server databases to Azure SQL Managed Instance through the Azure portal, you need the following prerequisites:

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- A [supported](#supported-sql-server-versions) instance of SQL Server [enabled by Azure Arc](overview.md) with the [latest version](release-notes.md) of the Azure extension for SQL Server. To upgrade your extension, see [Upgrade the extension](connect.md#upgrade-the-extension).

## Supported SQL Server versions

Both the General Purpose and Business Critical service tiers of Azure SQL Managed Instance support the Managed Instance link. Migration with the link feature works with the Enterprise, Developer, and Standard editions of SQL Server on Windows Server.

The following table lists the minimum supported SQL Server versions for the link:

| SQL Server version | Minimum required servicing update |
| --- | --- |
| SQL Server 2025 (17.x) | [SQL Server 2025 RTM (17.0.1000.7)](../sql-server-2025-release-notes.md) |
| SQL Server 2022 (16.x) | [SQL Server 2022 RTM (16.0.1000.6)](../sql-server-2022-release-notes.md) |
| SQL Server 2019 (15.x) | [SQL Server 2019 CU20 (15.0.4312.2)](https://support.microsoft.com/topic/kb5024276-cumulative-update-20-for-sql-server-2019-4b282be9-b559-46ac-9b6a-badbd44785d2) |
| SQL Server 2017 (14.x) | [SQL Server 2017 CU31 (14.0.3456.2)](/troubleshoot/sql/releases/sqlserver-2017/cumulativeupdate31) or later and the matching [SQL Server 2017 Azure Connect pack (14.0.3490.10)](/troubleshoot/sql/releases/sqlserver-2017/azureconnect) build |
| SQL Server 2016 (13.x) | [SQL Server 2016 SP3 (13.0.6300.2)](/troubleshoot/sql/releases/sqlserver-2016/build-versions#sql-server-2016-service-pack-3-sp3-cumulative-update-cu-builds) and the matching [SQL Server 2016 Azure Connect pack (13.0.7000.253)](/troubleshoot/sql/releases/sqlserver-2016/build-versions#sql-server-2016-service-pack-3-sp3-azure-connect-pack-builds) build |
| SQL Server 2014 (12.x) and earlier | Versions before SQL Server 2016 aren't supported. |

Reverse migration is only supported to SQL Server 2025 and SQL Server 2022 from SQL managed instances with the corresponding [update policy](/azure/azure-sql/managed-instance/update-policy). You can manually reverse a migration through other tools such as [native backup and restore](/azure/azure-sql/managed-instance/restore-database-to-sql-server), or [manually configuring a link in SSMS](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-ssms).

## Permissions

This section describes the permissions that you need to migrate your SQL Server instance to SQL Managed Instance through the Azure portal.

On the source SQL Server instance, you need the following permissions:

- If you enable [least privilege](configure-least-privilege.md), necessary permissions such as **sysadmin** are [granted](configure-windows-accounts-agent.md#database-migration) as needed during the database migration process.
- If you can't use least privilege, manually assign **sysadmin** permissions to the `NT AUTHORITY\SYSTEM` account for migration cutover or cancel operations to work.

To migrate with the Managed Instance link, you need one of the following permissions on the SQL Managed Instance target:

- [SQL Managed Instance Contributor](/azure/role-based-access-control/built-in-roles/databases#sql-managed-instance-contributor) role
- Subscription-level Contributor or Owner role

For minimum permissions, see [Custom permissions](/azure/azure-sql/managed-instance/managed-instance-link-preparation#permissions).

> [!NOTE]  
> Users with the `SqlServerAvailabilityGroups_CreateManagedInstanceLink`, `SqlServerAvailabilityGroups_failoverMiLink`, and `SqlServerAvailabilityGroups_deleteMiLink` permissions in Azure can perform actions on the **Database migration** pane during the migration process that elevate the SQL Server permissions of the account used by the extension, including the `sysadmin` role.

## Match performance capacity between replicas

When you use the link feature, it's important to match the performance capacity between SQL Server and SQL Managed Instance. This matching helps you avoid performance problems if the secondary replica can't keep up with replication from the primary replica, or after failover. Performance capacity includes CPU cores (or vCores in Azure), memory, and I/O throughput.  

## Prepare your SQL Server instance

To prepare your SQL Server instance, complete the following steps:

- [Validate you're on the supported version](#install-service-updates).
- [Create a database master key](#create-a-database-master-key-in-the-master-database) in the `master` database.
- [Enable the availability groups feature](#enable-availability-groups).
- [Add the proper trace flags at startup](#enable-startup-trace-flags).
- [Restart SQL Server and validate the configuration](#restart-sql-server-and-validate-the-configuration).
- [Set database to full recovery model](#set-database-to-full-recovery-model).
- [Import Azure-trusted root certificate authority keys to SQL Server](#import-azure-trusted-root-certificate-authority-keys-to-sql-server).
- [Enable accelerated database recovery](#enable-accelerated-database-recovery) if you're on SQL Server 2019 or later, and plan to use it on the target SQL managed instance.
- [Enable Service Broker](#enable-service-broker) if you plan to use it on the target SQL managed instance.

You need to [restart SQL Server](#restart-sql-server-and-validate-the-configuration) for these changes to take effect.

### Install service updates

Ensure that your SQL Server version has the appropriate servicing update installed, as listed in the [version supportability table](#supported-sql-server-versions). If you need to install any updates, you must restart your SQL Server instance during the update.

To check your SQL Server version, run the following Transact-SQL (T-SQL) script on SQL Server:

```sql
-- Run on SQL Server
-- Shows the version and CU of the SQL Server
USE master;
GO
SELECT @@VERSION as 'SQL Server version';
```

### Create a database master key in the master database

The link uses certificates to encrypt authentication and communication between SQL Server and SQL Managed Instance. The database master key protects the certificates used by the link. If you already have a database master key, you can skip this step.

Create a database master key in the `master` database. Insert your password in place of `<strong_password>` in the following script, and keep it in a confidential and secure place. Run this T-SQL script on SQL Server:

```sql
-- Run on SQL Server
-- Create a master key
USE master;
GO
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<strong_password>';
```

To make sure that you have the database master key, use the following T-SQL script on SQL Server:

```sql
-- Run on SQL Server
USE master;
GO
SELECT * FROM sys.symmetric_keys WHERE name LIKE '%DatabaseMasterKey%';
```

### Prepare SQL Server 2016 instances

For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you must complete the extra steps documented in [Prepare SQL Server 2016 prerequisites for the link](/azure/azure-sql/managed-instance/managed-instance-link-preparation-wsfc). These extra steps aren't required for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions supported by the link.

### Enable availability groups

The link feature relies on the Always On availability groups feature, which is disabled by default. For more information, see [Enable the Always On availability groups feature](../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).

To confirm the availability groups feature is enabled, run the following T-SQL script on SQL Server:

```sql
-- Run on SQL Server
-- Is the availability groups feature enabled on this SQL Server
DECLARE @IsHadrEnabled sql_variant = (select SERVERPROPERTY('IsHadrEnabled'))
SELECT
    @IsHadrEnabled as 'Is HADR enabled',
    CASE @IsHadrEnabled
        WHEN 0 THEN 'Availability groups DISABLED.'
        WHEN 1 THEN 'Availability groups ENABLED.'
        ELSE 'Unknown status.'
    END
    as 'HADR status'
```

If the availability groups feature isn't enabled, follow these steps to enable it:

1. Open [SQL Server Configuration Manager](../../tools/configuration-manager/sql-server-configuration-manager.md).
1. Select **SQL Server Services** from the left pane.
1. Right-click the SQL Server service, then select **Properties**:

   :::image type="content" source="media/migration-sql-mi-prepare-link/sql-server-configuration-manager-sql-server-properties.png" alt-text="Screenshot that shows SQL Server Configuration Manager, with selections for opening properties for the service.":::

1. Go to the **Always On Availability Groups** tab.
1. Select the **Enable Always On Availability Groups** checkbox, then select **OK**.

   :::image type="content" source="media/migration-sql-mi-prepare-link/always-on-availability-groups-properties.png" alt-text="Screenshot that shows the properties for Always On availability groups.":::

   - If you're using [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], and the **Enable Always On Availability Groups** option is disabled with the message `This computer is not a node in a failover cluster`, follow the steps described in [Prepare SQL Server 2016 prerequisites for the link](/azure/azure-sql/managed-instance/managed-instance-link-preparation-wsfc). After you complete these steps, return to this step and try again.

1. Select **OK** in the dialog.
1. Restart the SQL Server service.

### Enable startup trace flags

To optimize the performance of your link, enable the following trace flags at startup:

- `-T1800`: This trace flag optimizes performance when the log files for the primary and secondary replicas in an availability group are on disks with different sector sizes, such as 512 bytes and 4 KB. If both primary and secondary replicas use a disk sector size of 4 KB, you don't need this trace flag. For more information, see [KB3009974](https://support.microsoft.com/help/3009974).
- `-T9567`: This trace flag enables compression of the data stream for availability groups during automatic seeding. The compression increases the load on the processor but can significantly reduce transfer time during seeding.

To enable these trace flags at startup, use the following steps:

1. Open SQL Server Configuration Manager.
1. Select **SQL Server Services** from the left pane.
1. Right-click the SQL Server service, then select **Properties**.

   :::image type="content" source="media/migration-sql-mi-prepare-link/sql-server-configuration-manager-sql-server-properties.png" alt-text="Screenshot that shows SQL Server Configuration Manager.":::

1. Go to the **Startup Parameters** tab. In **Specify a startup parameter**, enter `-T1800` and select **Add** to add the startup parameter. Then enter `-T9567` and select **Add** to add the other trace flag. Select **Apply** to save your changes.

   :::image type="content" source="media/migration-sql-mi-prepare-link/startup-parameters-properties.png" alt-text="Screenshot that shows startup parameter properties.":::

1. Select **OK** to close the **Properties** window.

For more information, see the [syntax to enable trace flags](../../t-sql/database-console-commands/dbcc-traceon-transact-sql.md).

### Restart SQL Server and validate the configuration

If you didn't need to upgrade the version of SQL Server, enable the availability group feature, or add startup trace flags, you can skip this section.

After you ensure that you're on a supported version of SQL Server, enable the Always On availability groups feature, and add your startup trace flags, restart your SQL Server instance to apply all of these changes:

1. Open **SQL Server Configuration Manager**.
1. Select **SQL Server Services** from the left pane.
1. Right-click the SQL Server service, then select **Restart**.

   :::image type="content" source="media/migration-sql-mi-prepare-link/sql-server-configuration-manager-sql-server-restart.png" alt-text="Screenshot that shows the SQL Server restart command call.":::

After the restart, run the following T-SQL script on SQL Server to validate the configuration of your SQL Server instance:

```sql
-- Run on SQL Server
-- Shows the version and CU of SQL Server
USE master;
GO
SELECT @@VERSION as 'SQL Server version';
GO
-- Shows if the Always On availability groups feature is enabled
SELECT SERVERPROPERTY ('IsHadrEnabled') as 'Is Always On enabled? (1 true, 0 false)';
GO
-- Lists all trace flags enabled on SQL Server
DBCC TRACESTATUS;
```

Your SQL Server version should be one of the supported versions with the appropriate service updates applied. The Always On availability groups feature should be enabled, and you should have the `-T1800` and `-T9567` trace flags enabled. The following screenshot is an example of the expected outcome for a properly configured SQL Server instance:

:::image type="content" source="media/migration-sql-mi-prepare-link/ssms-results-expected-outcome.png" alt-text="Screenshot that shows the expected outcome in S S M S.":::

### Set database to full recovery model

Databases migrated through the link must be in the full recovery model and have at least one backup.

Run the following code on SQL Server for all databases you wish to migrate. Replace `<DatabaseName>` with your actual database name.

```sql
-- Run on SQL Server
-- Set full recovery model for all databases you want to migrate.
ALTER DATABASE [<DatabaseName>] SET RECOVERY FULL
GO

-- Execute backup for all databases you want to migrate.
BACKUP DATABASE [<DatabaseName>] TO DISK = N'<DiskPath>'
GO
```

### Import Azure-trusted root certificate authority keys to SQL Server

To trust the SQL Managed Instance public key certificates that Azure issues, you need to import Azure-trusted root certificate authority (CA) keys to SQL Server.

You can download the root CA keys from [Azure Certificate Authority details](/azure/security/fundamentals/azure-ca-details#root-certificate-authorities). At minimum, download the **DigiCert Global Root G2** and **Microsoft RSA Root Certificate Authority 2017** certificates and import them to your SQL Server instance.

> [!NOTE]  
> The root certificate in the certification path for a SQL Managed Instance public key certificate is issued by an Azure trusted root Certificate Authority (CA). The specific root CA can change over time as Azure updates its trusted CA list.
> For a simplified setup, install all root CA certificates listed in [Azure Root Certificate Authorities](/azure/security/fundamentals/azure-ca-details#root-certificate-authorities?tabs=root-and-subordinate-cas-list). You can install just the required CA key by identifying the issuer of a previously imported SQL Managed Instance public key.

Save the certificates local to the SQL Server instance, such as to the sample `C:\certs\<name of certificate>.crt` path, and then import the certificates from that path by using the following Transact-SQL script. Replace `<name of certificate>` with the actual certificate name: `DigiCert Global Root G2` and `Microsoft RSA Root Certificate Authority 2017`, which are the required names for these two certificates.

```sql
-- Run on SQL Server-- Import <name of certificate> root-authority certificate (trusted by Azure), if not already present
CREATE CERTIFICATE [DigiCertPKI] FROM FILE = 'C:\certs\DigiCertGlobalRootG2.crt'
DECLARE @CERTID int
SELECT @CERTID = CERT_ID('DigiCertPKI')
EXEC sp_certificate_add_issuer @CERTID, N'*.database.windows.net';
GO
CREATE CERTIFICATE [MicrosoftPKI] FROM FILE = 'C:\certs\Microsoft RSA Root Certificate Authority 2017.crt'
DECLARE @CERTID int
SELECT @CERTID = CERT_ID('MicrosoftPKI')
EXEC sp_certificate_add_issuer @CERTID, N'*.database.windows.net';
GO
```

> [!TIP]  
> If the `sp_certificate_add_issuer` stored procedure is missing from your SQL Server environment, your SQL Server instance likely doesn't have the [appropriate service update installed](#supported-sql-server-versions).

Finally, verify all the created certificates by using the following dynamic management view (DMV):

```sql
-- Run on SQL Server
USE master
SELECT * FROM sys.certificates
```

[!INCLUDE [prepare-database-for-migration](../../../azure-sql/includes/sql-managed-instance/prepare-database-for-migration.md)]

## Configure network connectivity

For the link to work, you must have network connectivity between SQL Server and SQL Managed Instance. The network option that you choose depends on whether or not your SQL Server instance is on an Azure network.

### SQL Server outside Azure

If you host your SQL Server instance outside Azure, you can establish a VPN connection between SQL Server and SQL Managed Instance by using either of these options:

- [Site-to-site VPN connection](/office365/enterprise/connect-an-on-premises-network-to-a-microsoft-azure-virtual-network)
- [Azure ExpressRoute connection](/azure/expressroute/expressroute-introduction)

> [!TIP]  
> For the best network performance when replicating data, use ExpressRoute. Provision a gateway with enough bandwidth for your use case.

### SQL Server on Azure Virtual Machines

Deploying SQL Server on Azure Virtual Machines in the same Azure virtual network that hosts SQL Managed Instance is the simplest method, because network connectivity automatically exists between the two instances. For more information, see [Quickstart: Configure an Azure VM to connect to Azure SQL Managed Instance](/azure/azure-sql/managed-instance/connect-vm-instance-configure).

If your SQL Server on Azure Virtual Machines instance is in a different virtual network from your SQL managed instance, you need to connect the two virtual networks. Virtual networks don't have to be in the same subscription for this scenario to work.

You have two options to connect virtual networks:

- [Azure virtual network peering](/azure/virtual-network/virtual-network-peering-overview)
- VNet-to-VNet VPN gateway ([Azure portal](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-resource-manager-portal), [PowerShell](/azure/vpn-gateway/vpn-gateway-vnet-vnet-rm-ps), [Azure CLI](/azure/vpn-gateway/vpn-gateway-howto-vnet-vnet-cli))

Peering is preferable because it uses the Microsoft backbone network. So, from a connectivity perspective, there's no noticeable difference in latency between virtual machines in a peered virtual network and in the same virtual network. Virtual network peering is supported between networks in the same region. Global virtual network peering is supported for instances hosted in subnets created after September 22, 2020. For more information, see [Frequently asked questions (FAQ)](/azure/azure-sql/managed-instance/frequently-asked-questions-faq#does-sql-managed-instance-support-global-vnet-peering).

### Network ports between the environments

Regardless of the connectivity mechanism, you must meet the following requirements for network traffic to flow between the environments:

The Network Security Group (NSG) rules on the subnet that hosts SQL Managed Instance must allow:

- Inbound port 5022 and port range 11000-11999 to receive traffic from the source SQL Server IP address
- Outbound port 5022 to send traffic to the destination SQL Server IP address

The 5022 port can't be changed on SQL Managed Instance.

All firewalls on the network that hosts SQL Server, and the host OS must allow:

- Inbound port 5022 opened to receive traffic from the source IP range of the MI subnet /24 (for example 10.0.0.0/24)
- Outbound ports 5022, and the port range 11000-11999 opened to send traffic to the destination IP range of MI subnet (example 10.0.0.0/24)

The 5022 port can be customized on the SQL Server side, but the port range 11000-11999 must be opened as is.

:::image type="content" source="media/migration-sql-mi-prepare-link/link-networking-requirements.png" alt-text="Diagram showing network requirements to set up the link between SQL Server and SQL managed instance." lightbox="media/migration-sql-mi-prepare-link/link-networking-requirements.png":::

The following table describes port actions for each environment:

| Environment | What to do |
| --- | --- |
| SQL Server (outside Azure) | Open both inbound and outbound traffic on port 5022 for the network firewall to the entire subnet IP range of SQL Managed Instance. If necessary, do the same on the SQL Server host OS Windows Firewall. |
| SQL Server (in Azure) | Open both inbound and outbound traffic on port 5022 for the network firewall to the entire subnet IP range of SQL Managed Instance. If necessary, do the same on the SQL Server host OS Windows Firewall. To allow communication on port 5022, create a network security group (NSG) rule in the virtual network that hosts the virtual machine (VM). |
| SQL Managed Instance | [Create an NSG rule](/azure/virtual-network/manage-network-security-group#create-a-security-rule) in the Azure portal to allow inbound and outbound traffic from the IP address and the networking that hosts SQL Server on port 5022 and port range 11000-11999. |

To open ports in Windows Firewall, use the following PowerShell script on the Windows host OS of the SQL Server instance:

```powershell
New-NetFirewallRule -DisplayName "Allow TCP port 5022 inbound" -Direction inbound -Profile Any -Action Allow -LocalPort 5022 -Protocol TCP
New-NetFirewallRule -DisplayName "Allow TCP port 5022 outbound" -Direction outbound -Profile Any -Action Allow -LocalPort 5022 -Protocol TCP
```

The following diagram shows an example of an on-premises network environment, indicating that *all firewalls in the environment need to have open ports*, including the OS firewall that hosts the SQL Server instance, and any corporate firewalls and gateways:

:::image type="content" source="media/migration-sql-mi-prepare-link/link-networking-infrastructure.png" alt-text="Diagram showing network infrastructure to set up the link between SQL Server and SQL managed instance." lightbox="media/migration-sql-mi-prepare-link/link-networking-infrastructure.png":::

> [!IMPORTANT]  
> - You need to open ports in every firewall in the networking environment, including the host server, and any corporate firewalls or gateways on the network. In corporate environments, you might need to show your network administrator the information in this section to help open additional ports in the corporate networking layer.
> - While you can choose to customize the endpoint on the SQL Server side, you can't change or customize port numbers for SQL Managed Instance.
> - IP address ranges of subnets hosting managed instances, and SQL Server must not overlap.

### Add URLs to allow list

Depending on your network security settings, you might need to add URLs to your allow list for the SQL Managed Instance FQDN and some of the Resource Management endpoints used by Azure.

Add the following resources to your allow list:

- The fully qualified domain name (FQDN) of your SQL Managed Instance. For example: `managedinstance.a1b2c3d4e5f6.database.windows.net`.
- Microsoft Entra Authority
- Microsoft Entra Endpoint Resource ID
- Resource Manager Endpoint
- Service Endpoint

Follow the steps in the [Configure SSMS for government clouds](/azure/azure-sql/managed-instance/managed-instance-link-preparation#configure-ssms-for-government-clouds) section to access the **Tools** interface in SQL Server Management Studio (SSMS) and identify specific URLs for the resources within your cloud you need to add to your allow list.

## Migrate a certificate of a TDE-protected database (optional)

If you're linking a SQL Server database protected by Transparent Data Encryption (TDE) to a SQL managed instance, you must migrate the corresponding encryption certificate from the on-premises or Azure VM SQL Server instance to the SQL managed instance before using the link. For detailed steps, see [Migrate a certificate of a TDE-protected database to Azure SQL Managed Instance](/azure/azure-sql/managed-instance/tde-certificate-migrate).

SQL Managed Instance databases that are encrypted with service-managed TDE keys can't be linked to SQL Server. You can only link an encrypted database to SQL Server if you encrypted it with a customer-managed key and the destination server has access to the same key used to encrypt the database. For more information, see [Set up SQL Server TDE with Azure Key Vault](../../relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault.md).

> [!NOTE]  
> Azure Key Vault is supported by SQL Server on Linux starting with [Cumulative Update 14 for SQL Server 2022](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate14).

## Test network connectivity

Before you start the migration, test network connectivity between your SQL Server instance and SQL Managed Instance. You can test connectivity directly from the Azure portal as part of the migration process. However, you can also test connectivity manually by using Transact-SQL and the SQL Server Agent. For more information, see [Test network connectivity](/azure/azure-sql/managed-instance/managed-instance-link-preparation#test-network-connectivity).

To test connectivity through the Azure portal, follow these steps:
1. Select **Migrate data** on the **Database migration** pane for your SQL Server instance resource.
1. Select the **MI link** option.
1. Select the target databases you want to migrate and then use **Next: Settings** to go to the next tab.
1. On the **Settings** tab, provide the name of the link and the source availability group. Then use **Test connection** to validate the network connectivity between SQL Server and SQL Managed Instance:

   :::image type="content" source="media/migration-sql-mi-prepare-link/test-mi-link-connectivity.png" alt-text="Screenshot that shows the Managed Instance link test connection button.":::

Consider the following points:
- To avoid false negatives, all firewalls along the network path must allow Internet Control Message Protocol (ICMP) traffic.
- To avoid false positives, all firewalls along the network path must allow traffic on the proprietary SQL Server UCS protocol. Blocking the protocol can lead to a successful connection test, but the link fails to create.
- Advanced firewall setups with packet-level guardrails need to be properly configured to allow traffic between SQL Server and SQL Managed Instance.

## Limitations

Consider the following limitations:
- The limitations of the [Managed Instance link](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview#limitations) apply to migrations through the Azure portal.
- Azure Extension for SQL Server version `1.1.3238.349` and earlier only supports migrating one database at a time through the link. To migrate multiple databases at the same time, upgrade to Azure Extension for SQL Server version `1.1.3348.364` or later.
- Canceling a migration requires **sysadmin** permissions on the source SQL Server instance. If your SQL Server instance isn't using least privilege, manually assign **sysadmin** permissions to the `NT AUTHORITY\SYSTEM` account.
- Configuring a link through the Azure portal for the purpose of migration isn't compatible with links created manually, either through SQL Server Management Studio (SSMS), or Transact-SQL (T-SQL). Review the [known issue](migrate-to-azure-sql-managed-instance-troubleshoot.md#known-interoperability-issue-with-existing-links) to learn more.
- Monitoring the migration through the Azure portal is available only to SQL Server instances that meet monitoring [licensing requirements](sql-monitoring.md#prerequisites).
- Migrating from a SQL Server failover cluster instance (FCI) is not currently supported.

## Troubleshoot common issues

To troubleshoot common issues when migrating to Azure SQL Managed Instance, see [Troubleshoot migration issues](migrate-to-azure-sql-managed-instance-troubleshoot.md).

## Next step

> [!div class="nextstepaction"]
> [Migrate to Azure SQL Managed Instance](migrate-to-azure-sql-managed-instance.md)

## Related content

- [Managed Instance link best practices - Azure SQL Managed Instance](/azure/azure-sql/managed-instance/managed-instance-link-best-practices)
- [SQL Server migration in Azure Arc Overview](migration-overview.md)
- [Prepare environment for LRS migration - SQL Server migration in Azure Arc](migration-sql-mi-prepare-log-replay-service.md)
- [SQL Server enabled by Azure Arc](overview.md)
- [Migration experience feedback directly to the product group](https://aka.ms/arc-migrations-feedback)
