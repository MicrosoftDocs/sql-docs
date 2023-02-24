---
author: cynthn
ms.author: cynthn
ms.date: 10/26/2018
ms.service: virtual-machines
ms.topic: include
---
The availability group listener is an IP address and network name that the SQL Server availability group listens on. To create the availability group listener:

1. <a name="getnet"></a>Get the name of the cluster network resource:

    a. Use RDP to connect to the Azure virtual machine that hosts the primary replica. 

    b. Open **Failover Cluster Manager**.

    c. Select the **Networks** node, and note the cluster network name. Use this name in the `$ClusterNetworkName` variable in the PowerShell script. In the following image, the cluster network name is **Cluster Network 1**:

    :::image type="content" source="./media/virtual-machines-ag-listener-configure/90-cluster-network-name.png" alt-text="Screenshot that shows a cluster network name in Failover Cluster Manager.":::

1. <a name="addcap"></a>Add the client access point. The client access point is the network name that applications use to connect to the databases in an availability group.

    a. In **Failover Cluster Manager**, expand the cluster name, and then select **Roles**.

    b. On the **Roles** pane, right-click the availability group name, and then select **Add Resource** > **Client Access Point**.

    :::image type="content" source="./media/virtual-machines-ag-listener-configure/92-add-client-access-point.png" alt-text="Screenshot of Failover Cluster Manager that shows selecting the Client Access Point command on the shortcut menu for the availability group.":::

    c. In the **Name** box, create a name for this new listener. 
   The name for the new listener is the network name that applications use to connect to databases in the SQL Server availability group.

    d. To finish creating the listener, select **Next** twice, and then select **Finish**. Don't bring the listener or resource online at this point.

1. Take the cluster role for the availability group offline. In **Failover Cluster Manager**, under **Roles**, right-click the role, and then select **Stop Role**.

1. <a name="congroup"></a>Configure the IP resource for the availability group:

    a. Select the **Resources** tab, and then expand the client access point that you created. The client access point is offline.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/94-new-client-access-point.png" alt-text="Screenshot of Failover Cluster Manager that shows an offline status for a client access point.":::

    b. Right-click the IP resource, and then select **Properties**. Note the name of the IP address, and use it in the `$IPResourceName` variable in the PowerShell script.

    c. Under **IP Address**, select **Static IP Address**. Set the IP address as the same address that you used when you set the load balancer address on the Azure portal.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/96-ip-resource.png" alt-text="Screenshot of Failover Cluster Manager that shows the selection of an IP address.":::
    
1. <a name = "dependencyGroup"></a>Make the SQL Server availability group dependent on the client access point:

    a. In **Failover Cluster Manager**, select **Roles**, and then select your availability group.

    b. On the **Resources** tab, under **Other Resources**, right-click the availability group resource, and then select **Properties**.

    c. On the **Dependencies** tab, add the name of the client access point (the listener).

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/97-properties-dependencies.png" alt-text="Screenshot of Failover Cluster Manager that shows adding a name on the Dependencies tab.":::

    d. Select **OK**.

1. <a name="listname"></a>Make the client access point dependent on the IP address:

    a. In **Failover Cluster Manager**, select **Roles**, and then select your availability group.

    b. On the **Resources** tab, right-click the client access point under **Server Name**, and then select **Properties**.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/98-dependencies.png" alt-text="Screenshot of Failover Cluster Manager that shows the Properties menu option for the listener's name.":::

    c. Select the **Dependencies** tab. Verify that the IP address is a dependency. If it isn't, set a dependency on the IP address. If multiple resources are listed, verify that the IP addresses have **OR**, not **AND**, dependencies. Then select **OK**.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/98-properties-dependencies.png" alt-text="Screenshot of the Dependencies tab that shows an IP resource for an availability group.":::

    >[!TIP]
    >You can validate that the dependencies are correctly configured. In **Failover Cluster Manager**, go to **Roles**, right-click the availability group, select **More Actions**, and then select **Show Dependency Report**. When the dependencies are correctly configured, the availability group is dependent on the network name, and the network name is dependent on the IP address.


1. <a name="setparam"></a>Set the cluster parameters in PowerShell:

   a. Copy the following PowerShell script to one of your SQL Server instances. Update the variables for your environment.

   - `$ClusterNetworkName` find the name in the **Failover Cluster Manager** by selecting **Networks**, right-click the network and select **Properties**. The **$ClusterNetworkName** is under **Name** on the General tab.

   - `$IPResourceName` is the name given to the IP Address resource in the **Failover Cluster Manager**. This is found in the **Failover Cluster Manager** by selecting **Roles**, select the **SQL Server AG or FCI name**, select the **Resources** tab under **Server Name**, right-click the IP address resource and select **Properties**. The correct value is under **Name** on the General tab.

   - `$ListenerILBIP` is the IP address that you created on the Azure load balancer for the availability group listener. Find the **$ListenerILBIP** in the **Failover Cluster Manager** on the same properties page as the SQL Server AG/FCI Listener Resource Name.

   - `$ListenerProbePort` is the port that you configured on the Azure load balancer for the availability group listener, such as 59999. Any unused TCP port is valid.

   ```powershell
   $ClusterNetworkName = "<MyClusterNetworkName>" # The cluster network name. Use Get-ClusterNetwork on Windows Server 2012 or later to find the name.
   $IPResourceName = "<IPResourceName>" # The IP address resource name.
   $ListenerILBIP = "<n.n.n.n>" # The IP address of the internal load balancer. This is the static IP address for the load balancer that you configured in the Azure portal.
   [int]$ListenerProbePort = <nnnnn>
  
   Import-Module FailoverClusters

   Get-ClusterResource $IPResourceName | Set-ClusterParameter -Multiple @{"Address"="$ListenerILBIP";"ProbePort"=$ListenerProbePort;"SubnetMask"="255.255.255.255";"Network"="$ClusterNetworkName";"EnableDhcp"=0}
   ```

   b. Set the cluster parameters by running the PowerShell script on one of the cluster nodes.  

   > [!NOTE]
   > If your SQL Server instances are in separate regions, you need to run the PowerShell script twice. The first time, use the `$ListenerILBIP` and `$ListenerProbePort` values from the first region. The second time, use the `$ListenerILBIP` and `$ListenerProbePort` values from the second region. The cluster network name and the cluster IP resource name are also different for each region.

1. Bring the cluster role for the availability group online. In **Failover Cluster Manager**, under **Roles**, right-click the role, and then select **Start Role**.

If necessary, repeat the preceding steps to set the cluster parameters for the IP address of the Windows Server failover cluster:

1. Get the IP address name of the Windows Server failover cluster. In **Failover Cluster Manager**, under **Cluster Core Resources**, locate **Server Name**.

1. Right-click **IP Address**, and then select **Properties**.

1. Copy the name of the IP address from **Name**. It might be **Cluster IP Address**. 

1. <a name="setwsfcparam"></a>Set the cluster parameters in PowerShell:
  
   a. Copy the following PowerShell script to one of your SQL Server instances. Update the variables for your environment.

   - `$ClusterCoreIP` is the IP address that you created on the Azure load balancer for the Windows Server failover cluster's core cluster resource. It's different from the IP address for the availability group listener.

   - `$ClusterProbePort` is the port that you configured on the Azure load balancer for the Windows Server failover cluster's health probe. It's different from the probe for the availability group listener.

   ```powershell
   $ClusterNetworkName = "<MyClusterNetworkName>" # The cluster network name. Use Get-ClusterNetwork on Windows Server 2012 or later to find the name.
   $IPResourceName = "<ClusterIPResourceName>" # The IP address resource name.
   $ClusterCoreIP = "<n.n.n.n>" # The IP address of the cluster IP resource. This is the static IP address for the load balancer that you configured in the Azure portal.
   [int]$ClusterProbePort = <nnnnn> # The probe port from WSFCEndPointprobe in the Azure portal. This port must be different from the probe port for the availability group listener.
  
   Import-Module FailoverClusters
  
   Get-ClusterResource $IPResourceName | Set-ClusterParameter -Multiple @{"Address"="$ClusterCoreIP";"ProbePort"=$ClusterProbePort;"SubnetMask"="255.255.255.255";"Network"="$ClusterNetworkName";"EnableDhcp"=0}
   ```

   b. Set the cluster parameters by running the PowerShell script on one of the cluster nodes.  

If any SQL resource is configured to use a port between 49152 and 65536 (the [default dynamic port range for TCP/IP](/windows/client-management/troubleshoot-tcpip-port-exhaust#default-dynamic-port-range-for-tcpip)), add an exclusion for each port. Such resources might include:

- SQL Server database engine
- Always On availability group listener
- Health probe for the failover cluster instance
- Database mirroring endpoint
- Cluster core IP resource 

Adding an exclusion will prevent other system processes from being dynamically assigned to the same port. For this scenario, configure the following exclusions on all cluster nodes:

- `netsh int ipv4 add excludedportrange tcp startport=58888 numberofports=1 store=persistent`
- `netsh int ipv4 add excludedportrange tcp startport=59999 numberofports=1 store=persistent`

It's important to configure the port exclusion when the port is not in use. Otherwise, the command will fail with a message like "The process cannot access the file because it is being used by another process."
To confirm that the exclusions are configured correctly, use the following command: `netsh int ipv4 show excludedportrange tcp`.  

>[!WARNING]
>The port for the availability group listener's health probe has to be different from the port for the cluster core IP address's health probe. In these examples, the listener port is 59999 and the cluster core IP address's health probe port is 58888. Both ports require an "allow inbound" firewall rule.
