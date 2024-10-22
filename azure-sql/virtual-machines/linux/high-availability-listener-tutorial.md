---
title: Configure an availability group listener for SQL Server on Linux virtual machines in Azure
description: Learn about setting up an availability group listener in SQL Server on Linux virtual machines in Azure.
author: aravindmahadevan-ms
ms.author: armaha
ms.reviewer: amitkh-msft, randolphwest
ms.date: 11/29/2023
ms.service: azure-vm-sql-server
ms.subservice: hadr
ms.custom: linux-related-content
ms.topic: tutorial
---
# Tutorial: Configure an availability group listener on Linux virtual machines

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This tutorial will go over steps on how to create an availability group (AG) listener for your SQL Servers on Linux virtual machines (VMs) in Azure, for Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu.

You'll learn how to:

> [!div class="checklist"]
> - Create a load balancer in the Azure portal
> - Configure the back-end pool for the load balancer
> - Create a probe for the load balancer
> - Set the load balancing rules
> - Create the load balancer resource in the cluster
> - Create the AG listener
> - Test connecting to the listener
> - Testing a failover

[!INCLUDE [bias-sensitive-term-t](../../../docs/includes/bias-sensitive-term-t.md)]

## Prerequisite

## [Red Hat Enterprise Linux (RHEL)](#tab/redhat)

Complete the [Tutorial: Configure availability groups for SQL Server on RHEL virtual machines in Azure](rhel-high-availability-fencing-tutorial.md).

## [SUSE Linux Enterprise Server (SLES)](#tab/suse)

Complete the [Tutorial: Configure availability groups for SQL Server on SLES virtual machines in Azure](sles-high-availability-fencing-tutorial.md).

## [Ubuntu](#tab/ubuntu)

Complete the [Tutorial: Configure availability groups for SQL Server on Ubuntu virtual machines in Azure](ubuntu-high-availability-fencing-tutorial.md).

---

## Create the load balancer in the Azure portal

The following instructions take you through steps 1 through 4 from the [Create and configure the load balancer in the Azure portal](../windows/availability-group-load-balancer-portal-configure.md#create--configure-load-balancer) section of the [Configure a load balancer & availability group listener (SQL Server on Azure VMs)](../windows/availability-group-load-balancer-portal-configure.md) article.

### Create the load balancer

1. In the Azure portal, open the resource group that contains the SQL Server virtual machines.

1. In the resource group, select **Add**.

1. Search for **load balancer** and then, in the search results, select **Load Balancer**, which is published by **Microsoft**.

1. On the **Load Balancer** pane, select **Create**.

1. In the **Create load balancer** dialog box, configure the load balancer as follows:

   | Setting | Value |
   | --- | --- |
   | **Name** | A text name representing the load balancer. For example, `sqlLB`. |
   | **Type** | **Internal** |
   | **Virtual network** | The default virtual network that was created should be named `VM1VNET`. |
   | **Subnet** | Select the subnet that the SQL Server instances are in. The default should be `VM1Subnet`. |
   | **IP address assignment** | **Static** |
   | **Private IP address** | Use the `virtualip` IP address that was created in the cluster. |
   | **Subscription** | Use the subscription that was used for your resource group. |
   | **Resource group** | Select the resource group that the SQL Server instances are in. |
   | **Location** | Select the Azure location that the SQL Server instances are in. |

### Configure the back-end pool

Azure calls the back-end address pool *backend pool*. In this case, the back-end pool is the addresses of the three SQL Server instances in your AG.

1. In your resource group, select the load balancer that you created.

1. On **Settings**, select **Backend pools**.

1. On **Backend pools**, select **Add** to create a back-end address pool.

1. On **Add backend pool**, under **Name**, type a name for the back-end pool.

1. Under **Associated to**, select **Virtual machine**.

1. Select each virtual machine in the environment, and associate the appropriate IP address to each selection.

    :::image type="content" source="media/high-availability-listener-tutorial/add-backend-pool-small.png" alt-text="Screenshot showing how to add a backend pool.":::

1. Select **Add**.

### Create a probe

The probe defines how Azure verifies which of the SQL Server instances currently owns the AG listener. Azure probes the service based on the IP address on a port that you define when you create the probe.

1. On the load balancer **Settings** pane, select **Health probes**.

1. On the **Health probes** pane, select **Add**.

1. Configure the probe on the **Add probe** pane. Use the following values to configure the probe:

   | Setting | Value |
   | --- | --- |
   | **Name** | A text name representing the probe. For example, `SQLAlwaysOnEndPointProbe`. |
   | **Protocol** | `TCP` |
   | **Port** | You can use any available port. For example, `59999`. |
   | **Interval** | `5` |
   | **Unhealthy threshold** | `2` |

1. Select **OK**.

1. Sign in to all your virtual machines, and open the probe port using the following commands:

    ```bash
    sudo firewall-cmd --zone=public --add-port=59999/tcp --permanent
    sudo firewall-cmd --reload
    ```

Azure creates the probe and then uses it to test which SQL Server instance has the listener for the AG.

### Set the load-balancing rules

The load-balancing rules configure how the load balancer routes traffic to the SQL Server instances. For this load balancer, you enable direct server return because only one of the three SQL Server instances owns the AG listener resource at a time.

1. On the load balancer **Settings** pane, select **Load balancing rules**.

1. On the **Load balancing rules** pane, select **Add**.

1. On the **Add load balancing rules** pane, configure the load-balancing rule. Use the following settings:

   | Setting | Value |
   | --- | --- |
   | **Name** | A text name representing the load-balancing rules. For example, `SQLAlwaysOnEndPointListener`. |
   | **Protocol** | **TCP** |
   | **Port** | `1433` |
   | **Backend port** | `1433`. This value is ignored because this rule uses **Floating IP (direct server return)**. |
   | **Probe** | Use the name of the probe that you created for this load balancer. |
   | **Session persistence** | **None** |
   | **Idle timeout (minutes)** | `4` |
   | **Floating IP (direct server return)** | **Enabled** |

   :::image type="content" source="media/high-availability-listener-tutorial/add-load-balancing-rule-small.png" alt-text="Screenshot showing how to add a load balancing rule.":::

1. Select **OK**.
1. Azure configures the load-balancing rule. Now the load balancer is configured to route traffic to the SQL Server instance that hosts the listener for the AG.

At this point, the resource group has a load balancer that connects to all SQL Server machines. The load balancer also contains an IP address for the SQL Server Always On AG listener, so that any machine can respond to requests for the AGs.

## Create the availability group listener resource

Before creating a load balancer resource in Pacemaker, First create the listener resource:

```bash
sudo crm configure primitive virtualip \
ocf:heartbeat:IPaddr2 \
params ip=x.y.z.a
```

In the previous example, `x.y.z.a` refers to the load balancer front-end IP address.

## Create the load balancer resource in the cluster

Follow the instructions for the distribution you're configuring.

## [Red Hat Enterprise Linux (RHEL)](#tab/redhat)

1. Sign in to the primary virtual machine. We need to create the resource to enable the Azure load balancer probe port (59999 is used in our example). Run the following command:

    ```bash
    sudo pcs resource create azure_load_balancer azure-lb port=59999
    ```

1. Create a group that contains the `virtualip` and `azure_load_balancer` resource:

    ```bash
    sudo pcs resource group add virtualip_group azure_load_balancer virtualip
    ```

### Add constraints

1. A colocation constraint must be configured to ensure the Azure load balancer IP address and the AG resource are running on the same node. Run the following command:

    ```bash
    sudo pcs constraint colocation add azure_load_balancer ag_cluster-master INFINITY with-rsc-role=Master
    ```

1. Create an ordering constraint to ensure that the AG resource is up and running before the Azure load balancer IP address. While the colocation constraint implies an ordering constraint, this enforces it.

    ```bash
    sudo pcs constraint order promote ag_cluster-master then start azure_load_balancer
    ```

1. To verify the constraints, run the following command:

    ```bash
    sudo pcs constraint list --full
    ```

    You should see the following output:

    ```output
    Location Constraints:
    Ordering Constraints:
      promote ag_cluster-master then start virtualip (kind:Mandatory) (id:order-ag_cluster-master-virtualip-mandatory)
      promote ag_cluster-master then start azure_load_balancer (kind:Mandatory) (id:order-ag_cluster-master-azure_load_balancer-mandatory)
    Colocation Constraints:
      virtualip with ag_cluster-master (score:INFINITY) (with-rsc-role:Master) (id:colocation-virtualip-ag_cluster-master-INFINITY)
      azure_load_balancer with ag_cluster-master (score:INFINITY) (with-rsc-role:Master) (id:colocation-azure_load_balancer-ag_cluster-master-INFINITY)
    Ticket Constraints:
    ```

## [SUSE Linux Enterprise Server (SLES)](#tab/suse)

This tutorial uses SLES 15 for the following examples:

1. Sign in to the primary virtual machine. We need to create the resource to enable the Azure load balancer probe port (`59999` is used in our example). Run the following command:

    ```bash
    sudo crm configure
    primitive azure_load_balancer azure-lb port=59999 op monitor timeout=20s interval=10
    commit
    quit
    ```

1. Create a group that contains the `admin-ip` and `azure_load_balancer` resource:

    ```bash
    sudo crm configure group admin-ip-group admin-ip azure_load_balancer
    ```

   As soon as you create the group, you see that the colocation and order constraints are modified and updated to reflect for the *group* instead of the `admin-ip` resource, as shown in the following example:

   ```bash
   INFO: modified colocation:vip_on_master from admin-ip to admin-ip-group
   INFO: modified order:ag_first from admin-ip to admin-ip-group
   ```

   When you look at the status, you should see the group shown as follows:

   ```bash
   sudo crm status
   ```

   ```output
   Cluster Summary:
     * Stack: corosync
     * Current DC: sles1 (version 2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712) - partition with quorum
     * Last updated: Fri Mar 10 15:50:53 2023
     * Last change:  Fri Mar 10 15:50:48 2023 by root via cibadmin on sles1
     * 3 nodes configured
     * 6 resource instances configured

   Node List:
     * Online: [ sles1 sles2 sles3 ]

   Full List of Resources:
     * rsc_st_azure        (stonith:fence_azure_arm):              Started sles2
     * Clone Set: ms-ag_cluster [ag_cluster] (promotable):
       * Masters: [ sles1 ]
       * Slaves: [ sles2 sles3 ]
     * Resource Group: admin-ip-group:
       * admin-ip  (ocf::heartbeat:IPaddr2):                       Started sles1
       * azure_load_balancer       (ocf::heartbeat:azure-lb):      Started sles1
   ```

   You can view the modified constraints using the command `sudo crm configure show`:

   ```output
     node 1: sles1
     node 2: sles2
     node 3: sles3
     primitive admin-ip IPaddr2 \
            params ip=10.0.0.93 \
            op monitor interval=10 timeout=20
     primitive ag_cluster ocf:mssql:ag \
            params ag_name=ag1 \
            meta failure-timeout=60s \
            op start timeout=60s interval=0 \
            op stop timeout=60s interval=0 \
            op promote timeout=60s interval=0 \
            op demote timeout=10s interval=0 \
            op monitor timeout=60s interval=10s \
            op monitor timeout=60s interval=11s role=Master \
            op monitor timeout=60s interval=12s role=Slave \
            op notify timeout=60s interval=0
     primitive azure_load_balancer azure-lb \
            params port=59999 \
            op monitor timeout=20s interval=10
     primitive rsc_st_azure stonith:fence_azure_arm \
            params subscriptionId=<subscription_guid> resourceGroup=amvindomain tenantId=<tenant_guid> login=<login_guid> passwd="******" pcmk_monitor_retries=4 pcmk_action_limit=3 power_timeout=240 pcmk_reboot_timeout=900 pcmk_host_map="sles1:sles1;sles2:sles2;sles3:sles3" \
            op monitor interval=3600 timeout=120
     group admin-ip-group admin-ip azure_load_balancer
     ms ms-ag_cluster ag_cluster \
            meta master-max=1 master-node-max=1 clone-max=3 clone-node-max=1 notify=true
     order ag_first Mandatory: ms-ag_cluster:promote admin-ip-group:start
     colocation vip_on_master inf: admin-ip-group ms-ag_cluster:Master
     property cib-bootstrap-options: \
            have-watchdog=false \
            dc-version="2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712" \
            cluster-infrastructure=corosync \
            cluster-name=sqlcluster \
            stonith-enabled=true \
            concurrent-fencing=true \
            stonith-timeout=900
     rsc_defaults rsc-options: \
            resource-stickiness=1 \
            migration-threshold=3
     op_defaults op-options: \
            timeout=600 \
            record-pending=true
   ```

## [Ubuntu](#tab/ubuntu)

Run the below command to create the load balancer resource in Pacemaker:

```bash
sudo crm configure primitive azure-load-balancer azure-lb params port=59999
```

### Add the colocation and promotion constraints to the Pacemaker cluster

To ensure that the listener and AG resources always run on the same server in the cluster, you must create a colocation constraint:

```bash
sudo crm configure colocation ag-with-listener INFINITY: virtualip-group ms-ag1:Master
```

Create a promotion/ordering constraint. This constraint instructs Pacemaker to bring the availability group online on the new primary server during a failover,  and then start the listener on that server.

```bash
sudo crm configure order ag-before-listener Mandatory: ms-ag1:promote virtualip-group:start
```

View the colocation constraint:

```bash
sudo crm configure show ag-with-listener
```

The output is similar to the following example:

```output
Colocation ag-with-listener inf: virtual ip-group ms-ag1:Master
```

View the order constraint:

```bash
sudo crm configure show ag-before-listener
```

The output is similar to the following example:

```output
order ag-before-listener Mandatory: ms-ag1:promot virtual ip-group:start
```

---

## Create the availability group listener

1. On the primary node, run the following command in **sqlcmd** or SSMS. Replace the IP address used below with the `virtualip` IP address.

   - SQL Server 2022 and later versions:

     ```sql
     ALTER AVAILABILITY GROUP [ag1]
     ADD LISTENER 'ag1-listener' (
         WITH IP((
             '10.0.0.7',
             '0.0.0.0'
         )),
         PORT = 1433
     );
     GO
     ```

   - SQL Server 2017 and SQL Server 2019:

     ```sql
     ALTER AVAILABILITY GROUP [ag1]
     ADD LISTENER 'ag1-listener' (
         WITH IP((
             '10.0.0.7',
             '255.255.255.255'
         )),
         PORT = 1433
     );
     GO
     ```

1. Sign in to each VM node. Use the following command to open the hosts file and set up host name resolution for the `ag1-listener` on each machine.

    ```bash
    sudo vi /etc/hosts
    ```

    In the **vi** editor, enter `i` to insert text, and on a blank line, add the IP of the `ag1-listener`. Then add `ag1-listener` after a space next to the IP.

    ```output
    <IP of ag1-listener> ag1-listener
    ```

    To exit the **vi** editor, first hit the **Esc** key, and then enter the command `:wq` to write the file and quit. Do this on each node.

## Test the listener and a failover

This section covers logging into a SQL Server AG listener, and testing a failover.

## [Red Hat Enterprise Linux (RHEL)](#tab/redhat)

### Test logging in to SQL Server using the availability group listener

1. Use **sqlcmd** to sign in to the primary node of SQL Server using the AG listener name:

    - Use a login that was previously created and replace `<YourPassword>` with the correct password. The following example uses the `sa` login that was created with the SQL Server.

    ```bash
    sqlcmd -S ag1-listener -U sa -P <YourPassword>
    ```

1. Check the name of the server that you're connected to. Run the following command in **sqlcmd**:

    ```sql
    SELECT @@SERVERNAME;
    ```

    Your output should show the current primary node. This should be `VM1` if you have never tested a failover.

    Exit the SQL Server session by typing the `exit` command.

### Test a failover

1. Run the following command to manually fail over the primary replica to `<VM2>` or another replica. Replace `<VM2>` with the value of your server name.

    ```bash
    sudo pcs resource move ag_cluster-master <VM2> --master
    ```

1. If you check your constraints, you'll see that another constraint was added because of the manual failover:

    ```bash
    sudo pcs constraint list --full
    ```

    You see that a constraint with ID `cli-prefer-ag_cluster-master` was added.

1. Remove the constraint with ID `cli-prefer-ag_cluster-master` using the following command:

    ```bash
    sudo pcs constraint remove cli-prefer-ag_cluster-master
    ```

1. Check your cluster resources using the command `sudo pcs resource`, and you should see that the primary instance is now `<VM2>`.

    > [!NOTE]  
    > This article contains references to the term *slave*, a term that Microsoft no longer uses. When the term is removed from the software, we'll remove it from this article.

    ```output
    [<username>@<VM1> ~]$ sudo pcs resource
    Master/Slave Set: ag_cluster-master [ag_cluster]
        Masters: [ <VM2> ]
        Slaves: [ <VM1> <VM3> ]
    Resource Group: virtualip_group
        azure_load_balancer        (ocf::heartbeat:azure-lb):      Started <VM2>
        virtualip  (ocf::heartbeat:IPaddr2):       Started <VM2>
    ```

1. Use **sqlcmd** to sign in to your primary replica using the listener name:

    - Use a login that was previously created and replace `<YourPassword>` with the correct password. The following example uses the `sa` login that was created with the SQL Server.

    ```bash
    sqlcmd -S ag1-listener -U sa -P <YourPassword>
     ```

1. Check the server that you're connected to. Run the following command in **sqlcmd**:

    ```sql
    SELECT @@SERVERNAME;
    ```

    You should see that you're now connected to the VM that you failed-over to.

## [SUSE Linux Enterprise Server (SLES)](#tab/suse)

### Test logging in to SQL Server using the availability group listener

1. Use **sqlcmd** to sign in to the primary node of SQL Server using the AG listener name:

   - Use a login that was previously created and replace `<YourPassword>` with the correct password. The following example uses the `sa` login that was created with the SQL Server.

   ```bash
   sqlcmd -S ag1-listener -U sa -P <YourPassword>
   ```

1. Check the name of the server that you're connected to. Run the following command in **sqlcmd**:

   ```sql
   SELECT @@SERVERNAME;
   ```

   Your output should show the current primary node. This should be `VM1` if you have never tested a failover.

   Exit the SQL Server session by typing the `exit` command.

### Test a failover

1. Run the following command to manually fail over the primary replica to `<VM2>` or another replica. Replace `<VM2>` with the value of your server name.

   ```bash
   sudo crm resource move ms-ag_cluster sles2
   ```

1. If you check your constraints, you'll see that another constraint was added because of the manual failover:

   ```bash
   sudo crm configure show
   ```

   You'll see that a constraint with ID `cli-prefer-ms-ag_cluster` was added.

1. Remove the constraint with ID `cli-prefer-ms-ag_cluster` using the following command:

   ```bash
   sudo crm configure delete cli-prefer-ms-ag_cluster
   ```

1. Check your cluster resources using the command `sudo crm status`, and you should see that the primary instance is now `SLES2`.

   ```output
   Cluster Summary:
     * Stack: corosync
     * Current DC: sles1 (version 2.0.5+20201202.ba59be712-150300.4.30.3-2.0.5+20201202.ba59be712) - partition with quorum
     * Last updated: Fri Mar 10 15:55:50 2023
     * Last change:  Fri Mar 10 15:55:28 2023 by root via crm_resource on sles1
     * 3 nodes configured
     * 6 resource instances configured

   Node List:
     * Online: [ sles1 sles2 sles3 ]

   Full List of Resources:
     * rsc_st_azure        (stonith:fence_azure_arm):             Started sles2
     * Clone Set: ms-ag_cluster [ag_cluster] (promotable):
     * ag_cluster        (ocf::mssql:ag):         Slave sles1 (Monitoring)
     * ag_cluster        (ocf::mssql:ag):         Master sles2 (Monitoring)
     * Slaves: [ sles3 ]
   Resource Group: admin-ip-group:
     * admin-ip                  (ocf::heartbeat:IPaddr2):        Started sles2
     * azure_load_balancer       (ocf::heartbeat:azure-lb):       Started sles2
   ```

1. Use **sqlcmd** to sign in to your primary replica using the listener name:

    - Use a login that was previously created and replace `<YourPassword>` with the correct password. The following example uses the `sa` login that was created with the SQL Server.

    ```bash
    sqlcmd -S ag1-listener -U sa -P <YourPassword>
     ```

1. Check the server that you're connected to. Run the following command in **sqlcmd**:

    ```sql
    SELECT @@SERVERNAME;
    ```

    You should see that you're now connected to the VM that you failed-over to.

## [Ubuntu](#tab/ubuntu)

### Test logging in to SQL Server using the availability group listener

1. Use **sqlcmd** to sign in to the primary node of SQL Server using the AG listener name:

    - Use a login that was previously created and replace `<YourPassword>` with the correct password. The following example uses the `sa` login that was created with the SQL Server.

    ```bash
    sqlcmd -S <listenerIP> -U sa -P <YourPassword>
    ```

1. Check the name of the server that you're connected to. Run the following command in **sqlcmd**:

    ```sql
    SELECT @@SERVERNAME;
    ```

    Your output should show the current primary node. This should be `Ubuntu1` if you have never tested a failover.

    Exit the SQL Server session by typing the `exit` command.

### Test a failover

To test the failover, move the primary replica to a different node with the following command:

```bash
sudo crm configure delete cli-prefer-ms-ag1
sudo crm resource move ms-ag1 ubuntu2 
```

Confirm the status by running:

```bash
sudo crm status
```

The output looks similar to the following example:

```output
Cluster Summary:
  * Stack: corosync
  * Current DC: ubuntu1 (version 2.0.3-4b1f869f0f) - partition with quorum
  * Last updated: Wed Nov 29 07:01:32 2023
  * Last change:  Sun Nov 26 17:00:26 2023 by root via cibadmin on ubuntu1
  * 3 nodes configured
  * 6 resource instances configured

Node List:
  * Online: [ ubuntu1 ubuntu2 ubuntu3 ]

Full List of Resources:
  * Clone Set: ms-ag1 [ag1_cluster] (promotable):
    * Masters: [ ubuntu2 ]
    * Slaves : [ ubuntu1 ubuntu3 ]
  * Resource Group:  virtual ip-group:
    * azure-load-balancer  (ocf  :: heartbeat:azure-lb):           Started ubuntu2     
    * virtualip     (ocf :: heartbeat: IPaddr2):                   Started ubuntu2
  * fence-vm     (stonith:fence_azure_arm):                        Started ubuntu1
```

Now the resources except the fence have been moved to `ubuntu2` node, which is the new primary.

---

## Next step

In order to utilize an availability group listener for your SQL Server instances, you need to create and configure a load balancer.

> [!div class="nextstepaction"]
> [Configure a load balancer & availability group listener (SQL Server on Azure VMs)](../windows/availability-group-load-balancer-portal-configure.md)
