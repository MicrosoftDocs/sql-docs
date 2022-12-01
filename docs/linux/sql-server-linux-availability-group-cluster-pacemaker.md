---
title: Configure a Pacemaker cluster for SQL Server availability groups
description: Learn to create a three-node cluster on Red Hat, SUSE, or Ubuntu, and add a previously created availability group resource to the cluster.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh-msft
ms.date: 11/28/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom: seo-lt-2019
---
# Configure a Pacemaker cluster for SQL Server availability groups

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to create a three-node cluster on Linux using Pacemaker, and add a previously created availability group as a resource in the cluster. For high availability, an availability group on Linux requires three nodes - see [High availability and data protection for availability group configurations](sql-server-linux-availability-group-ha.md).

[!INCLUDE [bias-sensitive-term-t](../includes/bias-sensitive-term-t.md)]

SQL Server isn't as tightly integrated with Pacemaker on Linux as it is with Windows Server failover clustering (WSFC). A SQL Server instance isn't aware of the cluster, and all orchestration is from the outside in. Pacemaker provides cluster resource orchestration. Also, the virtual network name is specific to Windows Server failover clustering; there is no equivalent in Pacemaker. Availability group dynamic management views (DMVs) that query cluster information return empty rows on Pacemaker clusters. To create a listener for transparent reconnection after failover, manually register the listener name in DNS with the IP used to create the virtual IP resource.

You can still [create a listener](sql-server-linux-availability-group-overview.md#the-listener-under-linux) for transparent reconnection after failover, but you have to manually register the listener name in the DNS server with the IP used to create the virtual IP resource (as explained in the following sections).

The following sections walk through the steps to set up a Pacemaker cluster and add an availability group as resource in the cluster for high availability, for each supported Linux distribution.

# [Red Hat Enterprise Linux](#tab/rhel)

The clustering layer is based on Red Hat Enterprise Linux (RHEL) [HA add-on](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/pdf/high_availability_add-on_overview/red_hat_enterprise_linux-7-high_availability_add-on_overview-en-us.pdf) built on top of [Pacemaker](https://clusterlabs.org/).

> [!NOTE]  
> Access to Red Hat full documentation requires a valid subscription.

For more information on cluster configuration, resource agents options, and management, visit [RHEL reference documentation](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_reference/index).

### Roadmap

The steps to create an availability group on Linux servers for high availability are different from the steps on a Windows Server failover cluster. The following list describes the high-level steps:

1. [Configure SQL Server on the cluster nodes](sql-server-linux-setup.md).

1. [Create the availability group](sql-server-linux-availability-group-configure-ha.md).

1. Configure a cluster resource manager, like Pacemaker. These instructions are in this article.

   The way to configure a cluster resource manager depends on the specific Linux distribution.

   > [!IMPORTANT]  
   > Production environments require a fencing agent, like STONITH for high availability. The demonstrations in this documentation do not use fencing agents. The demonstrations are for testing and validation only.

   > A Linux cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. Currently, fencing is not available in some cloud environments. For more information, see [Support Policies for RHEL High Availability Clusters - Virtualization Platforms](https://access.redhat.com/articles/29440).

1. [Add the availability group as a resource in the cluster](#create-availability-group-resource).

### Configure high availability for RHEL

To configure high availability for RHEL, enable the high availability subscription and then configure Pacemaker.

#### Enable the high availability subscription for RHEL

Each node in the cluster must have an appropriate subscription for RHEL and the High Availability Add on. Review the requirements at [How to install High Availability cluster packages in Red Hat Enterprise Linux](https://access.redhat.com/solutions/45930). Follow these steps to configure the subscription and repos:

1. Register the system.

   ```bash
   sudo subscription-manager register
   ```

   Provide your user name and password.

1. List the available pools for registration.

   ```bash
   sudo subscription-manager list --available
   ```

   From the list of available pools, note the pool ID for the high availability subscription.

1. Update the following script. Replace `<pool id>` with the pool ID for high availability from the preceding step. Run the script to attach the subscription.

   ```bash
   sudo subscription-manager attach --pool=<pool id>
   ```

1. Enable the repository.

   **RHEL 7**

   ```bash
   sudo subscription-manager repos --enable=rhel-ha-for-rhel-7-server-rpms
   ```

   **RHEL 8**

   ```bash
   sudo subscription-manager repos --enable=rhel-8-for-x86_64-highavailability-rpms
   ```

For more information, see [Pacemaker - The Open Source, High Availability Cluster](https://clusterlabs.org/pacemaker/).

After you have configured the subscription, complete the following steps to configure Pacemaker:

#### Configure Pacemaker

After you register the subscription, complete the following steps to configure Pacemaker:

[!INCLUDE [ss-linux-cluster-pacemaker-configure-rhel](includes/ss-linux-cluster-pacemaker-configure-rhel.md)]

After Pacemaker is configured, use `pcs` to interact with the cluster. Execute all commands on one node from the cluster.

[!INCLUDE [Considerations for multiple NICs](includes/sql-server-linux-availability-group-multiple-network-interfaces.md)]

### Configure fencing (STONITH)

Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. STONITH stands for "shoot the other node in the head." When the cluster resource manager can't determine the state of a node or of a resource on a node, fencing brings the cluster to a known state again.

A STONITH device provides a fencing agent. [Setting up Pacemaker on Red Hat Enterprise Linux in Azure](/azure/virtual-machines/workloads/sap/high-availability-guide-rhel-pacemaker/#1-create-the-stonith-devices) provides an example of how to create a STONITH device for this cluster in Azure. Modify the instructions for your environment.

Resource level fencing ensures that there is no data corruption in an outage by configuring a resource. For example, you can use resource level fencing to mark the disk on a node as outdated when the communication link goes down.

Node level fencing ensures that a node doesn't run any resources. This is done by resetting the node. Pacemaker supports a great variety of fencing devices. Examples include an uninterruptible power supply or management interface cards for servers.

For information about STONITH, and fencing, see the following articles:

- [Pacemaker Clusters from Scratch](https://clusterlabs.org/pacemaker/doc/en-US/Pacemaker/1.1/html/Clusters_from_Scratch/index.html)
- [Fencing and STONITH](https://clusterlabs.org/doc/crm_fencing.html)
- [Red Hat High Availability Add-On with Pacemaker: Fencing](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_administration/s1-fenceconfig-haaa)

> [!NOTE]  
> Because the node level fencing configuration depends heavily on your environment, disable it for this tutorial (it can be configured later). The following script disables node level fencing:
>
> ```bash
> sudo pcs property set stonith-enabled=false
> ```  
>
> Disabling STONITH is just for testing purposes. If you plan to use Pacemaker in a production environment, you should plan a STONITH implementation depending on your environment and keep it enabled.

### Set cluster property cluster-recheck-interval

`cluster-recheck-interval` indicates the polling interval at which the cluster checks for changes in the resource parameters, constraints or other cluster options. If a replica goes down, the cluster tries to restart the replica at an interval that is bound by the `failure-timeout` value and the `cluster-recheck-interval` value. For example, if `failure-timeout` is set to 60 seconds and `cluster-recheck-interval` is set to 120 seconds, the restart is tried at an interval that is greater than 60 seconds but less than 120 seconds. We recommend that you set failure-timeout to 60 seconds and `cluster-recheck-interval` to a value that is greater than 60 seconds. Setting `cluster-recheck-interval` to a small value isn't recommended.

To update the property value to `2 minutes` run:

```bash
sudo pcs property set cluster-recheck-interval=2min
```

If you already have an availability group resource managed by a Pacemaker cluster, Pacemaker package 1.1.18-11.el7 introduced a behavior change for the `start-failure-is-fatal` cluster setting when its value is `false`. This change affects the failover workflow. If a primary replica experiences an outage, the cluster is expected to fail over to one of the available secondary replicas. Instead, users will notice that the cluster keeps trying to start the failed primary replica. If that primary never comes online (because of a permanent outage), the cluster never fails over to another available secondary replica. Because of this change, a previously recommended configuration to set `start-failure-is-fatal` is no longer valid, and the setting needs to be reverted back to its default value of `true`.

Additionally, the AG resource needs to be updated to include the `failover-timeout` property.

To update the property value to `true` run:

```bash
sudo pcs property set start-failure-is-fatal=true
```

To update the `ag_cluster` resource property `failure-timeout` to `60s`, run:

```bash
pcs resource update ag_cluster meta failure-timeout=60s
```

For information on Pacemaker cluster properties, see [Pacemaker Clusters Properties](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_reference/ch-clusteropts-haar).

### Create a SQL Server login for Pacemaker

[!INCLUDE [ss-linux-cluster-pacemaker-create-login](includes/ss-linux-cluster-pacemaker-create-login.md)]

### Create availability group resource

To create the availability group resource, use `pcs resource create` command and set the resource properties. The following command creates a `ocf:mssql:ag` master/subordinate type resource for availability group with name `ag1`.

#### RHEL 7

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=ag1 meta failure-timeout=60s master notify=true
```

#### RHEL 8

With the availability of **RHEL 8**, the create syntax has changed. If you are using **RHEL 8**, the terminology `master` has changed to `promotable`. Use the following create command instead of the above command:

```bash
sudo pcs resource create ag_cluster ocf:mssql:ag ag_name=ag1 meta failure-timeout=60s promotable notify=true
```

[!INCLUDE [ss-linux-cluster-required-synchronized-secondaries-default](includes/ss-linux-cluster-required-synchronized-secondaries-default.md)]

### <a id="createIP"></a> Create virtual IP resource

To create the virtual IP address resource, run the following command on one node. Use an available static IP address from the network. Replace the IP address between `<10.128.16.240>` with a valid IP address.

```bash
sudo pcs resource create virtualip ocf:heartbeat:IPaddr2 ip=<10.128.16.240>
```

There is no virtual server name equivalent in Pacemaker. To use a connection string that points to a string server name instead of an IP address, register the virtual IP resource address and desired virtual server name in DNS. For DR configurations, register the desired virtual server name and IP address with the DNS servers on both primary and DR site.

### Add colocation constraint

Almost every decision in a Pacemaker cluster, like choosing where a resource should run, is done by comparing scores. Scores are calculated per resource. The cluster resource manager chooses the node with the highest score for a particular resource. If a node has a negative score for a resource, the resource can't run on that node.

On a pacemaker cluster, you can manipulate the decisions of the cluster with constraints. Constraints have a score. If a constraint has a score lower than `INFINITY`, Pacemaker regards it as recommendation. A score of `INFINITY` is mandatory.

To ensure that primary replica and the virtual ip resources run on the same host, define a colocation constraint with a score of INFINITY. To add the colocation constraint, run the following command on one node.

#### RHEL 7

When you create the `ag_cluster` resource in RHEL 7, it creates the resource as `ag_cluster-master`. Use the following command for RHEL 7:

```bash
sudo pcs constraint colocation add virtualip ag_cluster-master INFINITY with-rsc-role=Master
```

#### RHEL 8

When you create the `ag_cluster` resource in RHEL 8, it creates the resource as `ag_cluster-clone`. Use the following command for RHEL 8:

```bash
sudo pcs constraint colocation add virtualip with master ag_cluster-clone INFINITY with-rsc-role=Master
```

### Add ordering constraint

The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is:

1. User issues `pcs resource move` to the availability group primary from node1 to node2.
1. The virtual IP resource stops on node 1.
1. The virtual IP resource starts on node 2.

   > [!NOTE]  
   > At this point, the IP address temporarily points to node 2 while node 2 is still a pre-failover secondary.

1. The availability group primary on node 1 is demoted to secondary.
1. The availability group secondary on node 2 is promoted to primary.

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint.

To add an ordering constraint, run the following command on one node:

#### RHEL 7

```bash
sudo pcs constraint order promote ag_cluster-master then start virtualip
```

#### RHEL 8

```bash
sudo pcs constraint order promote ag_cluster-clone then start virtualip
```

> [!IMPORTANT]  
> After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs` and in SLES use `crm` tools.

Manually fail over the availability group with `pcs`. Don't initiate failover with Transact-SQL. For instructions, see [Failover](sql-server-linux-availability-group-failover-ha.md#failover).

## Next steps

- [Operate HA availability group](sql-server-linux-availability-group-failover-ha.md)

# [SUSE Linux Enterprise Server](#tab/sles)

The clustering layer is based on SUSE [High Availability Extension (HAE)](https://www.suse.com/products/highavailability) built on top of [Pacemaker](https://clusterlabs.org/).

For more information on cluster configuration, resource agent options, management, best practices, and recommendations, see [SUSE Linux Enterprise High Availability Extension 12 SP2](https://www.suse.com/documentation/sle-ha-12/index.html).

### Roadmap

The procedure for creating an availability group for high availability differs between Linux servers and a Windows Server failover cluster. The following list describes the high-level steps:

1. [Configure SQL Server on the cluster nodes](sql-server-linux-setup.md).

1. [Create the availability group](sql-server-linux-availability-group-failover-ha.md).

1. Configure a cluster resource manager, like Pacemaker. These instructions are in this article.

   The way to configure a cluster resource manager depends on the specific Linux distribution.

   > [!IMPORTANT]  
   > Production environments require a fencing agent, like STONITH for high availability. The examples in this article do not use fencing agents. They are for testing and validation only.
   >
   > A Pacemaker cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. At this time, fencing is not available in some cloud environments. See [SUSE Linux Enterprise High Availability Extension](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.fencing).

1. [Add the availability group as a resource in the cluster](#configure-an-availability-group)

### Prerequisites

To complete the following end-to-end scenario, you need three machines to deploy the three nodes cluster. The following steps outline how to configure these servers.

### Setup and configure the operating system on each cluster node

The first step is to configure the operating system on the cluster nodes. For this walk through, use SLES 12 SP3 with a valid subscription for the HA add-on.

#### Install and configure SQL Server service on each cluster node

1. Install and setup SQL Server service on all nodes. For detailed instructions, see [Install SQL Server on Linux](sql-server-linux-setup.md).

1. Designate one node as primary and other nodes as secondaries. Use these terms throughout this guide.

1. Make sure nodes that are going to be part of the cluster can communicate with each other.

   The following example shows `/etc/hosts` with additions for three nodes named SLES1, SLES2, and SLES3.

   ```output
   127.0.0.1   localhost
   10.128.16.33 SLES1
   10.128.16.77 SLES2
   10.128.16.22 SLES3
   ```

   All cluster nodes must be able to access each other via SSH. Tools like `hb_report` or `crm_report` (for troubleshooting) and Hawk's History Explorer require passwordless SSH access between the nodes, otherwise they can only collect data from the current node. In case you use a non-standard SSH port, use the -X option (see `man` page). For example, if your SSH port is 3479, invoke a `crm_report` with:

   ```bash
   sudo crm_report -X "-p 3479" [...]
   ```

   For more information, see the [SLES Administration Guide - Miscellaneous section](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#sec.ha.troubleshooting.misc).

### Create a SQL Server login for Pacemaker

[!INCLUDE [ss-linux-cluster-pacemaker-create-login](includes/ss-linux-cluster-pacemaker-create-login.md)]

### Configure an availability group

On Linux servers, configure the availability group and then configure the cluster resources. To configure the availability group, see [Configure Always On Availability Group for SQL Server on Linux](sql-server-linux-availability-group-configure-ha.md)

### Install and configure Pacemaker on each cluster node

1. Install the High Availability extension

   For reference, see [Installing SUSE Linux Enterprise Server and High Availability Extension](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.installation)

1. Install SQL Server resource agent package on both nodes.

   ```bash
   sudo zypper install mssql-server-ha
   ```

### Set up the first node

   Refer to [SLES installation instructions](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.setup.1st-node)

1. Sign in as `root` to the physical or virtual machine you want to use as cluster node.
1. Start the bootstrap script by executing:

   ```bash
   sudo ha-cluster-init
   ```

   If NTP hasn't been configured to start at boot time, a message appears.

   If you decide to continue anyway, the script automatically generates keys for SSH access and the Csync2 synchronization tool, and starts the services needed for both.

1. To configure the cluster communication layer (Corosync):

   1. Enter a network address to bind to. By default, the script proposes the network address of eth0. Alternatively, enter a different network address, for example the address of bond0.

   1. Enter a multicast address. The script proposes a random address that you can use as default.

   1. Enter a multicast port. The script proposes 5405 as default.

   1. To configure `SBD ()`, enter a persistent path to the partition of your block device that you want to use for SBD. The path must be consistent across all nodes in the cluster.  
   Finally, the script will start the Pacemaker service to bring the one-node cluster online and enable the Web management interface Hawk2. The URL to use for Hawk2 is displayed on the screen.

1. For any details of the setup process, check `/var/log/sleha-bootstrap.log`. You now have a running one-node cluster. Check the cluster status with crm status:

   ```bash
   sudo crm status
   ```

   You can also see cluster configuration with `crm configure show xml`  or `crm configure show`.

1. The bootstrap procedure creates a Linux user named hacluster with the password linux. Replace the default password with a secure one as soon as possible:

   ```bash
   sudo passwd hacluster
   ```

### Add nodes to the existing cluster

If you have a cluster running with one or more nodes, add more cluster nodes with the ha-cluster-join bootstrap script. The script only needs access to an existing cluster node and will complete the basic setup on the current machine automatically. Use the following steps:

If you have configured the existing cluster nodes with the `YaST` cluster module, make sure the following prerequisites are fulfilled before you run `ha-cluster-join`:

- The root user on the existing nodes has SSH keys in place for passwordless login.
- `Csync2` is configured on the existing nodes. For more information, see [Configuring Csync2 with YaST](https://documentation.suse.com/sle-ha/12-SP4/html/SLE-HA-all/cha-ha-setup.html#pro-ha-installation-setup-csync2-yast).

1. Sign in as `root` to the physical or virtual machine supposed to join the cluster.
1. Start the bootstrap script by executing:

   ```bash
   sudo ha-cluster-join
   ```

   If NTP hasn't been configured to start at boot time, a message appears.

1. If you decide to continue anyway, you will be prompted for the IP address of an existing node. Enter the IP address.

1. If you haven't already configured a passwordless SSH access between both machines, you will also be prompted for the root password of the existing node.

   After logging in to the specified node, the script copies the Corosync configuration, configures SSH and `Csync2`, and brings the current machine online as new cluster node. Apart from that, it starts the service needed for Hawk. If you have configured shared storage with `OCFS2`, it also automatically creates the mountpoint directory for the `OCFS2` file system.

1. Repeat the previous steps for all machines you want to add to the cluster.

1. For details of the process, check `/var/log/ha-cluster-bootstrap.log`.

1. Check the cluster status with `sudo crm status`. If you have successfully added a second node, the output will be similar to the following:

   ```bash
   sudo crm status
   ```

   You will see output similar to the following example.

   ```output
   3 nodes configured
   1 resource configured
   Online: [ SLES1 SLES2 SLES3]
   Full list of resources:
   admin_addr     (ocf::heartbeat:IPaddr2):       Started node1
   ```

   > [!NOTE]  
   > `admin_addr` is the virtual IP cluster resource which is configured during initial one-node cluster setup.

After adding all nodes, check if you need to adjust the no-quorum-policy in the global cluster options. This is especially important for two-node clusters.

### Set cluster property cluster-recheck-interval

`cluster-recheck-interval` indicates the polling interval at which the cluster checks for changes in the resource parameters, constraints or other cluster options. If a replica goes down, the cluster tries to restart the replica at an interval that is bound by the `failure-timeout` value and the `cluster-recheck-interval` value. For example, if `failure-timeout` is set to 60 seconds and `cluster-recheck-interval` is set to 120 seconds, the restart is tried at an interval that is greater than 60 seconds but less than 120 seconds. We recommend that you set failure-timeout to 60 seconds and `cluster-recheck-interval` to a value that is greater than 60 seconds. Setting `cluster-recheck-interval` to a small value isn't recommended.

To update the property value to `2 minutes` run:

```bash
crm configure property cluster-recheck-interval=2min
```

If you already have an availability group resource managed by a Pacemaker cluster, Pacemaker package 1.1.18-11.el7 introduced a behavior change for the `start-failure-is-fatal` cluster setting when its value is `false`. This change affects the failover workflow. If a primary replica experiences an outage, the cluster is expected to fail over to one of the available secondary replicas. Instead, users will notice that the cluster keeps trying to start the failed primary replica. If that primary never comes online (because of a permanent outage), the cluster never fails over to another available secondary replica. Because of this change, a previously recommended configuration to set `start-failure-is-fatal` is no longer valid, and the setting needs to be reverted back to its default value of `true`.  

Additionally, the AG resource needs to be updated to include the `failover-timeout` property.  

To update the property value to `true` run:

```bash
crm configure property start-failure-is-fatal=true
```

Update your existing AG resource property `failure-timeout` to `60s` run (replace `ag1` with the name of your availability group resource):

```bash
crm configure edit ag1
```

In the text editor, add `meta failure-timeout=60s` after any `param`s and before any `op`s.

For more information on Pacemaker cluster properties, see [Configuring Cluster Resources](https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_config_crm_resources.html).

[!INCLUDE [Considerations for multiple NICs](includes/sql-server-linux-availability-group-multiple-network-interfaces.md)]

### Configure fencing (STONITH)

Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. When the cluster resource manager can't determine the state of a node or of a resource on a node, fencing is used to bring the cluster to a known state again.

Resource level fencing ensures mainly that there is no data corruption during an outage by configuring a resource. You can use resource level fencing, for instance, with DRBD (Distributed Replicated Block Device) to mark the disk on a node as outdated when the communication link goes down.

Node level fencing ensures that a node doesn't run any resources. This is done by resetting the node and the Pacemaker implementation of it is called STONITH (which stands for "shoot the other node in the head"). Pacemaker supports a great variety of fencing devices, such as an uninterruptible power supply or management interface cards for servers.

For more information, see:

- [Pacemaker Clusters from Scratch](https://clusterlabs.org/pacemaker/doc/en-US/Pacemaker/1.1/html/Clusters_from_Scratch/)
- [Fencing and Stonith](https://clusterlabs.org/doc/crm_fencing.html)
- [SUSE HA documentation: Fencing and STONITH](https://www.suse.com/documentation/sle_ha/book_sleha/data/cha_ha_fencing.html)

At cluster initialization time, STONITH is disabled if no configuration is detected. It can be enabled later by running following command:

```bash
sudo crm configure property stonith-enabled=true
```

> [!IMPORTANT]  
> Disabling STONITH is just for testing purposes. If you plan to use Pacemaker in a production environment, you should plan a STONITH implementation depending on your environment and keep it enabled. SUSE does not provide fencing agents for any cloud environments (including Azure) or Hyper-V. Consequentially, the cluster vendor does not offer support for running production clusters in these environments. We are working on a solution for this gap that will be available in future releases.

### Configure the cluster resources for SQL Server

Refer to [SLES Administration Guid](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.manual_config)

### Enable Pacemaker

Enable Pacemaker so that it automatically starts.

Run the following command on every node in the cluster.

```bash
systemctl enable pacemaker
```

#### Create availability group resource

The following command creates and configures the availability group resource for three replicas of availability group [ag1]. The monitor operations and timeouts have to be specified explicitly in SLES based on the fact that timeouts are highly workload-dependent and need to be carefully adjusted for each deployment.
Run the command on one of the nodes in the cluster:

1. Run `crm configure` to open the crm prompt:

   ```bash
   sudo crm configure
   ```

1. In the crm prompt, run the following command to configure the resource properties.

   ```bash
   primitive ag_cluster \
      ocf:mssql:ag \
      params ag_name="ag1" \
      meta failure-timeout=60s \
      op start timeout=60s \
      op stop timeout=60s \
      op promote timeout=60s \
      op demote timeout=10s \
      op monitor timeout=60s interval=10s \
      op monitor timeout=60s interval=11s role="Master" \
      op monitor timeout=60s interval=12s role="Slave" \
      op notify timeout=60s
   ms ms-ag_cluster ag_cluster \
      meta master-max="1" master-node-max="1" clone-max="3" \
     clone-node-max="1" notify="true" \
   commit
      ```

[!INCLUDE [ss-linux-cluster-required-synchronized-secondaries-default](includes/ss-linux-cluster-required-synchronized-secondaries-default.md)]

#### Create virtual IP resource

If you didn't create the virtual IP resource when you ran `ha-cluster-init`, you can create this resource now. The following command creates a virtual IP resource. Replace `<**0.0.0.0**>` with an available address from your network and `<**24**>` with the number of bits in the CIDR subnet mask. Run on one node.

```bash
crm configure \
primitive admin_addr \
   ocf:heartbeat:IPaddr2 \
   params ip=<**0.0.0.0**> \
      cidr_netmask=<**24**>
```

#### Add colocation constraint

Almost every decision in a Pacemaker cluster, like choosing where a resource should run, is done by comparing scores. Scores are calculated per resource, and the cluster resource manager chooses the node with the highest score for a particular resource. (If a node has a negative score for a resource, the resource can't run on that node.) We can manipulate the decisions of the cluster with constraints. Constraints have a score. If a constraint has a score lower than INFINITY, it is only a recommendation. A score of INFINITY means it is a must. We want to ensure that primary of the availability group and the virtual ip resource are run on the same host, so we define a colocation constraint with a score of INFINITY.

To set colocation constraint for the virtual IP to run on same node as the primary node, run the following command on one node:

```bash
crm configure
colocation vip_on_master inf: \
    admin_addr ms-ag_cluster:Master
commit
```

#### Add ordering constraint

The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is:

1. User issues `resource migrate` to the availability group primary from node1 to node2.
1. The virtual IP resource stops on node 1.
1. The virtual IP resource starts on node 2. At this point, the IP address temporarily points to node 2 while node 2 is still a pre-failover secondary. 
1. The availability group master on node 1 is demoted.
1. The availability group on node 2 is promoted to master.

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint.  
To add an ordering constraint, run the following command on one node:

```bash
sudo crm configure \
   order ag_first inf: ms-ag_cluster:promote admin_addr:start
```

> [!IMPORTANT]  
> After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In SLES use `crm`.

Manually fail over the availability group with `crm`. Don't initiate failover with Transact-SQL. For more information, see [Failover](sql-server-linux-availability-group-failover-ha.md#failover).

For more information, see:

- [Managing cluster resources](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#sec.ha.config.crm)
- [HA Concepts](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.concepts)
- [Pacemaker Quick Reference](https://github.com/ClusterLabs/pacemaker/blob/master/doc/sphinx/Pacemaker_Administration/pcs-crmsh.rst)

## Next steps

- [Operate HA availability group](sql-server-linux-availability-group-failover-ha.md)

# [Ubuntu Linux](#tab/ubuntu)

### Roadmap

The steps to create an availability group on Linux servers for high availability are different from the steps on a Windows Server failover cluster. The following list describes the high-level steps:

1. [Configure SQL Server on the cluster nodes](sql-server-linux-setup.md).

1. [Create the availability group](sql-server-linux-availability-group-configure-ha.md).

1. Configure a cluster resource manager, like Pacemaker. These instructions are in this article.

   The way to configure a cluster resource manager depends on the specific Linux distribution.

   Production environments require a fencing agent, like [STONITH](#stonith) for high availability. The demonstrations in this documentation don't use fencing agents. The demonstrations are for testing and validation only.

   A Linux cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. At this time, fencing isn't available in some cloud environments. For more information, see [Support Policies for RHEL High Availability Clusters - Virtualization Platforms](https://access.redhat.com/articles/29440).

   Fencing is normally implemented at the operating system and is dependent on the environment. Find instructions for fencing in the operating system distributor documentation.

1. [Add the availability group as a resource in the cluster](#create-availability-group-resource).

### Install and configure Pacemaker on each cluster node

1. On all nodes, open the firewall ports. Open the port for the Pacemaker high-availability service, SQL Server instance, and the availability group endpoint. The default TCP port for server running SQL Server is 1433.

   ```bash
   sudo ufw allow 2224/tcp
   sudo ufw allow 3121/tcp
   sudo ufw allow 21064/tcp
   sudo ufw allow 5405/udp

   sudo ufw allow 1433/tcp # Replace with TDS endpoint
   sudo ufw allow 5022/tcp # Replace with DATA_MIRRORING endpoint

   sudo ufw reload
   ```

   Alternatively, you can disable the firewall, but this isn't recommended in a production environment:

   ```bash
   sudo ufw disable
   ```

1. Install Pacemaker packages. On all nodes, run the following commands:

   ```bash
   sudo apt-get install -y pacemaker pacemaker-cli-utils crmsh resource-agents fence-agents corosync python3-azure
   ```

1. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on all nodes.

   ```bash
   sudo passwd hacluster
   ```

### Enable and start pcsd service and Pacemaker

The following command enables and starts the `pcsd` service and Pacemaker. Run on all nodes. This allows the nodes to rejoin the cluster after reboot.

```bash
sudo systemctl enable pcsd
sudo systemctl start pcsd
sudo systemctl enable pacemaker
```

The `enable pacemaker` command may complete with the following error:

```error
pacemaker Default-Start contains no runlevels, aborting.
```

The error is harmless, and cluster configuration can continue.

You can also try the following command instead:

```bash
sudo systemctl start pacemaker
```

### Create the cluster

1. Prior to creating a cluster, you must create an authentication key on the primary server, and copy it to the other servers participating in the AG.

   Use the following script to create an authentication key on the primary server:

   ```bash
   sudo corosync-keygen
   ```

   You can use `scp` to copy the generated key to other servers:

   ```bash
   sudo scp /etc/corosync/authkey dbadmin@server-02:/etc/corosync
   sudo scp /etc/corosync/authkey dbadmin@server-03:/etc/corosync
   ```

2. To create the cluster, edit the `/etc/corosync/corosync.conf` file on the primary server:

   ```bash
   sudo vim /etc/corosync/corosync.conf
   ```

   The `corosync.conf` file should look similar to the following example:

   ```text
   totem {
       version: 2
       cluster_name: agclustername
       transport: udpu
       crypto_cipher: none
       crypto_hash: none
   }
   logging {
       fileline: off
       to_stderr: yes
       to_logfile: yes
       logfile: /var/log/corosync/corosync.log
       to_syslog: yes
       debug: off
       logger_subsys {
           subsys: QUORUM
           debug: off
       }
   }
   quorum {
       provider: corosync_votequorum
   }
   nodelist {
       node {
           name: server-01
           nodeid: 1
           ring0_addr: 10.0.0.4
       }
       node {
           name: server-02
           nodeid: 2
           ring0_addr: 10.0.0.5
       }
           node {
           name: server-03
           nodeid: 3
           ring0_addr: 10.0.0.6
       }
   }
   ```

   Replace the `corosync.conf` file on other nodes:

   ```bash
   sudo scp /etc/corosync/corosync.conf dbadmin@server-02:/etc/corosync
   sudo scp /etc/corosync/corosync.conf dbadmin@server-03:/etc/corosync
   ```

   Restart the `pacemaker` and `corosync` services:

   ```bash
   sudo systemctl restart pacemaker corosync
   ```

   Confirm the status of cluster and verify the configuration:

   ```bash
   sudo crm status
   ```

[!INCLUDE [Considerations for multiple NICs](includes/sql-server-linux-availability-group-multiple-network-interfaces.md)]

### <a id="stonith"></a> Configure fencing (STONITH)

Pacemaker cluster vendors require STONITH to be enabled and a fencing device configured for a supported cluster setup. (STONITH stands for "shoot the other node in the head".) When the cluster resource manager can't determine the state of a node or of a resource on a node, fencing is used to bring the cluster to a known state again.

Resource level fencing ensures that no data corruption occurs if there's an outage. You can use resource level fencing, for instance, with DRBD (Distributed Replicated Block Device) to mark the disk on a node as outdated when the communication link goes down.

Node level fencing ensures that a node doesn't run any resources. This is done by resetting the node, and the Pacemaker implementation of it is called STONITH. Pacemaker supports a great variety of fencing devices, for example, an uninterruptible power supply or management interface cards for servers.

For more information, see [Pacemaker Clusters from Scratch](https://clusterlabs.org/pacemaker/doc/en-US/Pacemaker/1.1/html/Clusters_from_Scratch/) and [Fencing and Stonith](https://clusterlabs.org/doc/crm_fencing.html).

Because the node level fencing configuration depends heavily on your environment, we disable it for this tutorial (it can be configured at a later time). Run the following script on the primary node:

```bash
sudo crm configure property stonith-enabled=false
```

In this example, disabling STONITH is just for testing purposes. If you plan to use Pacemaker in a production environment, you should plan a STONITH implementation depending on your environment and keep it enabled. Contact the operating system vendor for information about fencing agents for any specific distribution.

### Set cluster property cluster-recheck-interval

The `cluster-recheck-interval` property indicates the polling interval at which the cluster checks for changes in the resource parameters, constraints or other cluster options. If a replica goes down, the cluster tries to restart the replica at an interval that is bound by the `failure-timeout` value and the `cluster-recheck-interval` value. For example, if `failure-timeout` is set to 60 seconds and `cluster-recheck-interval` is set to 120 seconds, the restart is tried at an interval that is greater than 60 seconds but less than 120 seconds. You should set `failure-timeout` to 60 seconds, and `cluster-recheck-interval` to a value that is greater than 60 seconds. Setting `cluster-recheck-interval` to a smaller value isn't recommended.

To update the property value to `2 minutes` run:

```bash
sudo crm configure property cluster-recheck-interval=2min
```

If you already have an availability group resource managed by a Pacemaker cluster, Pacemaker package 1.1.18-11.el7 introduced a behavior change for the `start-failure-is-fatal` cluster setting when its value is `false`. This change affects the failover workflow. If a primary replica experiences an outage, the cluster is expected to fail over to one of the available secondary replicas. Instead, users will notice that the cluster keeps trying to start the failed primary replica. If that primary never comes online (because of a permanent outage), the cluster never fails over to another available secondary replica. Because of this change, a previously recommended configuration to set `start-failure-is-fatal` is no longer valid, and the setting needs to be reverted back to its default value of `true`.

Additionally, the AG resource needs to be updated to include the `failover-timeout` property.

To update the property value to `true` run:

```bash
sudo crm configure property start-failure-is-fatal=true
```

Update your existing AG resource property `failure-timeout` to `60s` run (replace `ag1` with the name of your availability group resource):

```bash
sudo crm configure meta failure-timeout=60s
```

### Install SQL Server resource agent for integration with Pacemaker

Run the following commands on all nodes.

```bash
sudo apt-get install mssql-server-ha
```

### Create a SQL Server login for Pacemaker

[!INCLUDE [ss-linux-cluster-pacemaker-create-login](includes/ss-linux-cluster-pacemaker-create-login.md)]

### Create availability group resource

To create the availability group resource, use the `sudo crm configure` command to set the resource properties. The following example creates a primary/replica type resource `ocf:mssql:ag` for an availability group with name `ag1`.

```console
~$ sudo crm

configure

primitive ag1_cluster \
ocf:mssql:ag \
params ag_name="ag1" \
meta failure-timeout=60s \
op start timeout=60s \
op stop timeout=60s \
op promote timeout=60s \
op demote timeout=10s \
op monitor timeout=60s interval=10s \
op monitor timeout=60s on-fail=demote interval=11s role="Master" \
op monitor timeout=60s interval=12s role="Slave" \
op notify timeout=60s
ms ms-ag1 ag1_cluster \
meta master-max="1" master-node-max="1" clone-max="3" \
clone-node-max="1" notify="true"

commit
```

[!INCLUDE [required-synchronized-secondaries-default](includes/ss-linux-cluster-required-synchronized-secondaries-default.md)]

### Create virtual IP resource

To create the virtual IP address resource, run the following command on one node. Use an available static IP address from the network. Before you run the script, replace the values between `< ... >` with a valid IP address.

```bash
sudo crm configure primitive virtualip \
ocf:heartbeat:IPaddr2 \
params ip=10.128.16.240
```

There's no virtual server name equivalent in Pacemaker. To use a connection string that points to a string server name and not use the IP address, register the IP resource address and desired virtual server name in DNS. For DR configurations, register the desired virtual server name and IP address with the DNS servers on both primary and DR site.

### Add colocation constraint

Almost every decision in a Pacemaker cluster, like choosing where a resource should run, is done by comparing scores. Scores are calculated per resource, and the cluster resource manager chooses the node with the highest score for a particular resource. (If a node has a negative score for a resource, the resource can't run on that node.)

Use constraints to configure the decisions of the cluster. Constraints have a score. If a constraint has a score lower than INFINITY, it's only a recommendation. A score of INFINITY means it's mandatory.

To ensure that primary replica and the virtual ip resource are on the same host, define a colocation constraint with a score of INFINITY. To add the colocation constraint, run the following command on one node.

```bash
sudo crm configure colocation ag-with-listener INFINITY: virtualip-group ms-ag1:Master
```

### Add ordering constraint

The colocation constraint has an implicit ordering constraint. It moves the virtual IP resource before it moves the availability group resource. By default the sequence of events is:

1. User issues `pcs resource move` to the availability group primary from `node1` to `node2`.

1. The virtual IP resource stops on `node1`.

1. The virtual IP resource starts on `node2`.

   At this point, the IP address temporarily points to `node2` while `node2` is still a pre-failover secondary.

1. The availability group primary on `node1` is demoted to secondary.

1. The availability group secondary on `node2` is promoted to primary.

To prevent the IP address from temporarily pointing to the node with the pre-failover secondary, add an ordering constraint.

To add an ordering constraint, run the following command on one node:

```bash
sudo crm configure order ag-before-listener Mandatory: ms-ag1:promote virtualip-group:start
```

After you configure the cluster and add the availability group as a cluster resource, you can't use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux aren't coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). The SQL Server service isn't aware of the presence of the cluster. All orchestration is done through the cluster management tools.

### Next steps

- [Operate HA availability group](sql-server-linux-availability-group-failover-ha.md)

---
