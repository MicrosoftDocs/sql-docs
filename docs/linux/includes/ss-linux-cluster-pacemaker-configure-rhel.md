---
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/15/2022
ms.service: sql
ms.subservice: linux
ms.topic: include
---
1. On all cluster nodes, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

   ```bash
   sudo firewall-cmd --permanent --add-service=high-availability
   sudo firewall-cmd --reload
   ```

   > If the firewall doesn't have a built-in high-availability configuration, open the following ports for Pacemaker.
   >
   > * TCP: Ports 2224, 3121, 21064
   > * UDP: Port 5405

1. Install Pacemaker packages on all nodes.

   ```bash
   sudo yum install pacemaker pcs fence-agents-all resource-agents
   ```

1. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on all nodes.

   ```bash
   sudo passwd hacluster
   ```

1. To allow nodes to rejoin the cluster after the reboot, enable and start `pcsd` service and Pacemaker. Run the following command on all nodes.

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   sudo systemctl enable pacemaker
   ```

1. Create the Cluster. To create the cluster, run the following command:

   **RHEL 7**

   ```bash
   sudo pcs cluster auth <node1> <node2> <node3> -u hacluster -p <password for hacluster>
   sudo pcs cluster setup --name <clusterName> <node1> <node2> <node3>
   sudo pcs cluster start --all
   sudo pcs cluster enable --all
   ```

   **RHEL8**

   For RHEL 8, you will need to authenticate the nodes separately. Manually enter in the Username and Password for hacluster when prompted.

   ```bash
   sudo pcs host auth <node1> <node2> <node3>
   sudo pcs cluster setup <clusterName> <node1> <node2> <node3>
   sudo pcs cluster start --all
   sudo pcs cluster enable --all
   ```

   > [!NOTE]  
   > If you previously configured a cluster on the same nodes, you need to use `--force` option when running `pcs cluster setup`. This option is equivalent to running `pcs cluster destroy`. To re-enable Pacemaker, run `sudo systemctl enable pacemaker`.

1. Install SQL Server resource agent for SQL Server. Run the following commands on all nodes.

   ```bash
   sudo yum install mssql-server-ha
   ```
