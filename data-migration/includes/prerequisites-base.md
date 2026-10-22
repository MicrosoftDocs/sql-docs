---
author: abhims14
ms.author: abhishekum
ms.date: 02/19/2026
ms.service: azure-database-migration-service
ms.topic: include
---

- Create a target instance of [SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/create-sql-vm-portal).

  If you have an existing Azure VM, register it with the [SQL Server IaaS Agent extension in Full management mode](/azure/azure-sql/virtual-machines/windows/sql-server-iaas-agent-extension-automate-management#management-modes).

- Ensure that the logins you use to connect to the source SQL Server instance are members of the **sysadmin** server role, or have `CONTROL SERVER` permission.

- Provide an SMB network share, Azure Storage account file share, or Azure Storage account blob container that contains your full database backup files and subsequent transaction log backup files. Azure DMS uses the backup location during database migration.

  - Always use a dedicated storage account for migration. Sharing it with other workloads can lead to conflicts and security risks.

  - Once migration is done, either rotate the Storage Account Key to keep backups secure, or delete the storage account if it's no longer needed.

  - Azure DMS doesn't take database backups, and doesn't initiate any database backups on your behalf. Instead, the service uses existing database backup files for the migration.

  - If your database backup files are in an SMB network share, [create an Azure Storage account](/azure/storage/common/storage-account-create) that allows Azure DMS to upload the database backup files, and to migrate databases. Make sure you create the Azure Storage account in the same region where you create your instance of Azure DMS.

  - You can write each backup to either a separate backup file or to multiple backup files. Appending multiple backups such as full and transaction logs into a single backup media isn't supported.

  - You can provide compressed backups to reduce the likelihood of experiencing potential issues associated with migrating large backups.

- Ensure that the service account running the source SQL Server instance has read and write permissions on the SMB network share that contains database backup files.

- If you're migrating a database protected by transparent data encryption (TDE), migrate the certificate from the source SQL Server instance to SQL Server on an Azure VM before you migrate data. For more information, see [Move a TDE protected database to another SQL Server](/sql/relational-databases/security/encryption/move-a-tde-protected-database-to-another-sql-server).

  > [!TIP]  
  > If your database contains sensitive data protected by [Always Encrypted](/sql/relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio), the migration process automatically migrates your Always Encrypted keys to your target instance of SQL Server on an Azure VM.

- If your database backups are on a network file share, provide a computer on which you can install a [self-hosted integration runtime](/azure/data-factory/create-self-hosted-integration-runtime) to access and migrate database backups. The migration wizard gives you the download link and authentication keys to download and install your self-hosted integration runtime.

  In preparation for the migration, ensure that the computer on which you install the self-hosted integration runtime has the following outbound firewall rules and domain names enabled:

  | Domain names | Outbound port | Description |
  | --- | --- | --- |
  | Public cloud: `{datafactory}.{region}.datafactory.azure.net`<br />or`*.frontend.clouddatahub.net`<br /><br />Azure Government: `{datafactory}.{region}.datafactory.azure.us`<br />Microsoft Azure operated by 21Vianet: `{datafactory}.{region}.datafactory.azure.cn` | 443 | Required by the self-hosted integration runtime to connect to Azure DMS.<br />For a newly created data factory in a public cloud, locate the fully qualified domain name (FQDN) from your self-hosted integration runtime key, in the format `{datafactory}.{region}.datafactory.azure.net`.<br />For an existing data factory, if you don't see the FQDN in your self-hosted integration key, use `*.frontend.clouddatahub.net` instead. |
  | `download.microsoft.com` | 443 | Required by the self-hosted integration runtime for downloading the updates. If you disable autoupdate, you can skip configuring this domain. |
  | `*.core.windows.net` | 443 | Used by the self-hosted integration runtime that connects to the Azure Storage account to upload database backups from your network share |

  > [!TIP]  
  > If you already store your database backup files in an Azure Storage account, you don't need a self-hosted integration runtime during the migration process.

- If you use a self-hosted integration runtime, make sure that the computer on which the runtime is installed can connect to the source SQL Server instance and the network file share where backup files are located.

- Enable outbound port 445 to allow access to the network file share. For more information, see [Self-hosted integration runtime for database migrations](../sql-server/self-hosted-integration-runtime.md).

- If you're using Azure DMS for the first time, make sure that the `Microsoft.DataMigration` [resource provider is registered in your subscription](/azure/dms/quickstart-create-data-migration-service-portal#register-the-resource-provider).
