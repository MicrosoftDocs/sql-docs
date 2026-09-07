---
author: rothja
ms.author: jroth
ms.date: 12/12/2024
ms.service: azure-vm-sql-server
ms.topic: include
---

1. After the Azure virtual machine is created and running, select **Virtual machine**, and then choose your new VM. 

1. Select **Connect** and then choose **Connect via Bastion** from the dropdown list to go to the **Bastion** page for your VM. 

   ![Screenshot showing connect to VM in portal.](./media/virtual-machines-sql-server-remote-desktop-connect/azure-virtual-machine-connect.png)

1. Select **Deploy Bastion** and wait for the process to finish. 

1. After [Bastion](/azure/bastion/bastion-connect-vm-rdp-windows) is deployed successfully, choose the authentication type, enter authentication details, and then select **Connect**: 

   ![Screenshot showing connect with Bastion.](./media/virtual-machines-sql-server-remote-desktop-connect/remote-desktop-connect.png)

   You may need to disable the pop-up blocker in your browser to open the Bastion session in a new browser tab. 

