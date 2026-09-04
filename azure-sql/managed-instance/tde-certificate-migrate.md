---
title: Migrate TDE certificate from SQL Server
description: Learn how to migrate a SQL Server TDE certificate when you're migrating your SQL Server database to Azure SQL Managed Instance. 
author: MladjoA
ms.author: mlandzic
ms.reviewer: mathoma, jovanpop
ms.date: 03/31/2026
ms.service: azure-sql-managed-instance
ms.subservice: security
ms.topic: how-to
ms.custom:
  - sqldbrb=1
  - devx-track-azurepowershell
  - sfi-image-nochange
---

# Migrate a SQL Server TDE certificate to Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

In this article, learn how to migrate the certificate before you migrate your TDE-protected SQL Server database to Azure SQL Managed Instance by using the native restore option.

When you migrate a database protected by [Transparent Data Encryption (TDE)](/sql/relational-databases/security/encryption/transparent-data-encryption) from SQL Server to Azure SQL Managed Instance by using the _native restore option_, you must first migrate the corresponding certificate before you restore the database to the SQL managed instance.

Alternatively, you can use the fully managed [Azure Database Migration Service](/data-migration/sql-server/managed-instance/database-migration-service) to seamlessly migrate both a TDE-protected database and the corresponding certificate.

This article focuses on migrating databases from SQL Server to Azure SQL Managed Instance. To move databases between SQL managed instances, see: 
- [Copy-only backups](/sql/relational-databases/backup-restore/copy-only-backups-sql-server)
- [Point-in-time restore](point-in-time-restore.md)
- [Copy or move a database](database-copy-move-how-to.md)

## Prerequisites

To complete the steps in this article, you need the following prerequisites:

* [Pvk2Pfx](/windows-hardware/drivers/devtest/pvk2pfx) command-line tool installed on the on-premises server or other computer with access to the certificate exported as a file. The Pvk2Pfx tool is part of the [Enterprise Windows Driver Kit](/windows-hardware/drivers/download-the-wdk), a self-contained command-line environment.
* [Windows PowerShell](/powershell/scripting/install/installing-windows-powershell) version 5.0 or higher installed.

# [PowerShell](#tab/azure-powershell)

Make sure you have the following prerequisites:

* [Azure PowerShell module installed and updated](/powershell/azure/install-az-ps).
* [Az.Sql module](https://www.powershellgallery.com/packages/Az.Sql).


Run the following commands in PowerShell to install or update the module:

```azurepowershell
Install-Module -Name Az.Sql
Update-Module -Name Az.Sql
```

* * *

## Export the TDE certificate to a .pfx file

You can export the certificate directly from the source SQL Server instance, or from the certificate store if you're keeping it there.

### Export the certificate from the source SQL Server instance

The following steps export the certificate by using SQL Server Management Studio and convert it into .pfx format. The generic names *TDE_Cert* and *full_path* are placeholders for certificate names, file names, and paths. Replace them with the actual names.

1. In SSMS, open a new query window and connect to the source SQL Server instance.

1. Use the following script to list TDE-protected databases and get the name of the certificate protecting encryption of the database to be migrated:

   ```sql
   USE master
   GO
   SELECT db.name as [database_name], cer.name as [certificate_name]
   FROM sys.dm_database_encryption_keys dek
   LEFT JOIN sys.certificates cer
   ON dek.encryptor_thumbprint = cer.thumbprint
   INNER JOIN sys.databases db
   ON dek.database_id = db.database_id
   WHERE dek.encryption_state = 3
   ```

   :::image type="content" source="./media/tde-certificate-migrate/on-premises-certificate-list.png" alt-text="Screenshot in SSMS that shows a list of TDE certificates." lightbox="./media/tde-certificate-migrate/on-premises-certificate-list.png":::

1. Execute the following script to export the certificate to a pair of files (.cer and .pvk), keeping the public and private key information. Replace `<password>` with a strong password.

   ```sql
   USE master
   GO
   BACKUP CERTIFICATE TDE_Cert
   TO FILE = 'c:\full_path\TDE_Cert.cer'
   WITH PRIVATE KEY (
     FILE = 'c:\full_path\TDE_Cert.pvk',
     ENCRYPTION BY PASSWORD = '<password>'
   )
   ```

   :::image type="content" source="./media/tde-certificate-migrate/backup-on-premises-certificate.png" alt-text="Screenshot in SSMS that shows the backed up TDE certificate." lightbox="./media/tde-certificate-migrate/backup-on-premises-certificate.png":::

1. Use the PowerShell console to copy certificate information from a pair of newly created files to a .pfx file by using the Pvk2Pfx tool:

   ```cmd
   .\pvk2pfx -pvk c:/full_path/TDE_Cert.pvk  -pi "<password>" -spc c:/full_path/TDE_Cert.cer -pfx c:/full_path/TDE_Cert.pfx
   ```

### Export the certificate from a certificate store

If you keep the certificate in the SQL Server local machine certificate store, use the following steps to export it:

1. Open the PowerShell console and run the following command to open the Certificates snap-in of Microsoft Management Console:

   ```cmd
   certlm
   ```

2. In the Certificates MMC snap-in, expand the path **Personal** > **Certificates** to see the list of certificates.

3. Right-click the certificate and select **Export**.

4. Follow the wizard to export the certificate and private key to a .pfx format.

## Upload the certificate to Azure SQL Managed Instance by using an Azure PowerShell cmdlet

> [!IMPORTANT]
> Use a migrated certificate only to restore the TDE-protected database. Shortly after the restore finishes, the migrated certificate is replaced by a different protector. The new protector is either a service-managed certificate or an asymmetric key from the key vault, depending on the type of TDE you set on the instance.

# [PowerShell](#tab/azure-powershell)

1. Start with preparation steps in PowerShell:

   ```azurepowershell
   # import the module into the PowerShell session
   Import-Module Az
   # connect to Azure with an interactive dialog for sign-in
   Connect-AzAccount
   # list subscriptions available and copy id of the subscription target the managed instance belongs to
   Get-AzSubscription
   # set subscription for the session
   Select-AzSubscription <subscriptionId>
   ```

2. After you complete all preparation steps, run the following commands to upload the base-64 encoded certificate to the target SQL managed instance:

   ```azurepowershell
   # If you are using PowerShell 6.0 or higher, run this command:
   $fileContentBytes = Get-Content 'C:/full_path/TDE_Cert.pfx' -AsByteStream
   # If you are using PowerShell 5.x, uncomment and run this command instead of the one above:
   # $fileContentBytes = Get-Content 'C:/full_path/TDE_Cert.pfx' -Encoding Byte
   $base64EncodedCert = [System.Convert]::ToBase64String($fileContentBytes)
   $securePrivateBlob = $base64EncodedCert  | ConvertTo-SecureString -AsPlainText -Force
   $password = "<password>"
   $securePassword = $password | ConvertTo-SecureString -AsPlainText -Force
   Add-AzSqlManagedInstanceTransparentDataEncryptionCertificate -ResourceGroupName "<resourceGroupName>" `
       -ManagedInstanceName "<managedInstanceName>" -PrivateBlob $securePrivateBlob -Password $securePassword
   ```

* * *

The certificate is now available to the specified SQL managed instance, and you can restore the backup of the corresponding TDE-protected database.

> [!NOTE]
> The uploaded certificate isn't visible in the `sys.certificates` catalog view. To confirm successful upload of the certificate, run the [RESTORE FILELISTONLY](/sql/t-sql/statements/restore-statements-filelistonly-transact-sql) command.

## Related content

- [Restore a database backup to Azure SQL Managed Instance](restore-sample-database-quickstart.md)
