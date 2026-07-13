---
author: rwestMSFT
ms.author: randolphwest
ms.service: azure-database-migration-service
ms.topic: include
ms.date: 07/10/2026
---
You can get a sample script to create a login and provision it with the necessary permissions for [VMware](/azure/migrate/migrate-support-matrix-vmware#sql-server-instance-and-database-discovery-requirements), [Hyper-V](/azure/migrate/migrate-support-matrix-hyper-v#sql-server-instance-and-database-discovery-requirements), or [physical servers](/azure/migrate/migrate-support-matrix-physical#sql-server-instance-and-database-discovery-requirements), using Windows authentication or SQL Server authentication.

## Role assignment

To assign a role to a user or an app ID:

1. In the Azure portal, go to the resource.

1. In the left menu, select **Access control (IAM)**, and then scroll to find the custom roles you created.

1. Select the roles to assign, select the user or app ID, and then save the changes.

   The user or app ID now appears on the **Role assignments** tab.
