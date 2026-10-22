---
author: abhims14
ms.author: abhishekum
ms.date: 12/10/2025
ms.service: azure-database-migration-service
ms.topic: include
---

Make sure your resources can access the Azure Storage account. Depending on your database backup location and desired storage account network settings, refer to the following table for the various migration scenarios and network configurations:

| Scenario | SMB network share | Azure Storage account container |
| --- | --- | --- |
| **Enabled from all networks** | No extra steps | No extra steps |
| **Enabled from selected virtual networks and IP addresses** | [On-premises self-hosted integration runtime (SHIR)](?tabs=dms-on-prem-shir#dms-backup-storage) | [Backups stored in Azure Storage container](?tabs=dms-backups-azure-storage#dms-backup-storage) |
| **Enabled from selected virtual networks and IP addresses + private endpoint** | [Azure VM self-hosted integration runtime (SHIR)](?tabs=dms-azure-vm-shir#dms-backup-storage) | [Backups stored in Azure Storage container (Private endpoint)](?tabs=dms-backups-private-endpoint#dms-backup-storage) |

<a id="dms-backup-storage"></a>

### [On-premises SHIR](#tab/dms-on-prem-shir)

#### On-premises self-hosted integration runtime (SHIR)

If you install your SHIR on your on-premises network, follow these steps:

1. Connect to the Azure portal from the SHIR machine.

1. Open your Azure Storage account, and go to the **Networking** pane.

1. Make sure **Public network access** is set to **Enabled from selected virtual networks and IP addresses**.

1. In the **Firewall** section, select the **Add your client IP address** checkbox.

1. Enter the client IP address of the host machine, and select **Save**.

### [Azure VM SHIR](#tab/dms-azure-vm-shir)

#### Azure VM self-hosted integration runtime (SHIR)

If you host your SHIR on an Azure VM, add the virtual network of the VM to the Azure Storage account, because the VM has a non-public IP address that you can't add to the IP address range section.

1. Connect to the Azure portal, and open your Azure Storage account.

1. Open your Azure Storage account, and go to the **Networking** pane.

1. Select the **Add existing virtual network** checkbox.

1. Select the subscription, virtual network, and subnet of the Azure VM hosting the SHIR. You can find this information on the **Overview** page of the Azure Virtual Machine. The subnet might say **Service endpoint required**. If so, select **Enable**.

1. Select **Save**.

### [Azure Storage](#tab/dms-backups-azure-storage)

#### Backups stored in Azure Storage container

If you place your backups directly into an Azure Storage container, you don't need to perform the preceding steps because there's no Integration Runtime communicating with the Azure Storage account.

However, you still need to ensure that the target SQL Server instance can communicate with the Azure Storage account to restore the backups from the container.

1. Connect to the Azure portal, and open your Azure Storage account.

1. Open your Azure Storage account, and go to the **Networking** pane.

1. Select the **Add existing virtual network** checkbox.

1. Specify the target SQL Server instance virtual network, and select **Save**.

### [Azure Storage (Private endpoint)](#tab/dms-backups-private-endpoint)

#### Backups stored in Azure Storage container (Private endpoint)

If you set up a private endpoint on your Azure Storage account, follow these steps:

1. Connect to the Azure portal, and open your Azure Storage account.

1. Open your Azure Storage account, and go to the **Networking** pane.

1. Select the **Add existing virtual network** checkbox.

1. Specify the subnet of the private endpoint, and select **Save**.

The private endpoint must be hosted in the same virtual network as the target SQL Server instance. If it isn't, create another private endpoint using the process in the Azure Storage account configuration section.

---
