---
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 09/14/2026
ms.service: sql
ms.subservice: linux
ms.topic: include
ms.custom:
  - linux-related-content
---
1. On all cluster nodes, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

   ```bash
   sudo firewall-cmd --permanent --add-service=high-availability
   sudo firewall-cmd --reload
   ```

   If the firewall doesn't have a built-in high-availability configuration, open the following ports for Pacemaker.

   - TCP: Ports 2224, 3121, 21064
   - UDP: Port 5405

1. Install Pacemaker packages on all nodes.

   ```bash
   sudo yum install pacemaker pcs fence-agents-all resource-agents
   ```

1. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on all nodes.

   ```bash
   sudo passwd hacluster
   ```

1. To allow nodes to rejoin the cluster after the restart, enable and start `pcsd` service and Pacemaker. Run the following command on all nodes.

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   sudo systemctl enable pacemaker
   ```

1. Create the cluster. Starting with Red Hat 8, you must authenticate the nodes separately. Run the following commands on a single node. Manually enter the username and password for `hacluster` when prompted.

   ```bash
   sudo pcs host auth <node1> <node2> <node3>
   sudo pcs cluster setup <clusterName> <node1> <node2> <node3>
   sudo pcs cluster start --all
   sudo pcs cluster enable --all
   ```

   > [!NOTE]  
   > If you previously configured a cluster on the same nodes, you need to use `--force` option when running `pcs cluster setup`. This option is equivalent to running `pcs cluster destroy`. To re-enable Pacemaker, run `sudo systemctl enable pacemaker`.

1. Install [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] resource agent for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Run the following commands on all nodes.

   ```bash
   sudo yum install mssql-server-ha
   ```
