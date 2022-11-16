---
author: cynthn
ms.author: cynthn
ms.date: 10/26/2018
ms.service: virtual-machines
ms.topic: include
---
The availability group listener is an IP address and network name that the SQL Server availability group listens on. To create the availability group listener, do the following:

1. <a name="getnet"></a>Get the name of the cluster network resource.

    a. Use RDP to connect to the Azure virtual machine that hosts the primary replica. 

    b. Open Failover Cluster Manager.

    c. Select the **Networks** node, and note the cluster network name. Use this name in the `$ClusterNetworkName` variable in the PowerShell script. In the following image the cluster network name is **Cluster Network 1**:

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/90-cluster-network-name.png" alt-text="Screenshot of Failover Cluster Manager, Networks tab, with the Cluster Network Name highlighted.":::

1. <a name="addcap"></a>Add the client access point.  
    The client access point is the network name that applications use to connect to the databases in an availability group. Create the client access point in Failover Cluster Manager.

    a. Expand the cluster name, and then click **Roles**.

    b. In the **Roles** pane, right-click the availability group name, and then select **Add Resource** > **Client Access Point**.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/92-add-client-access-point.png" alt-text="Screenshot of Failover Cluster Manager, Roles tab, with Client Access Point selected in the menu for the availability group.":::

    c. In the **Name** box, create a name for this new listener. 
   The name for the new listener is the network name that applications use to connect to databases in the SQL Server availability group.

    d. To finish creating the listener, click **Next** twice, and then click **Finish**. Do not bring the listener or resource online at this point.

1. Take the availability group cluster role offline. In **Failover Cluster Manager** under **Roles**, right-click the role, and select **Stop Role**.

1. <a name="congroup"></a>Configure the IP resource for the availability group.

    a. Click the **Resources** tab, and then expand the client access point you created. The client access point is offline.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/94-new-client-access-point.png" alt-text="Screenshot of Failover Cluster Manager, Roles tab, with the Client Access Point showing offline.":::

    b. Right-click the IP resource, and then click properties. Note the name of the IP address, and use it in the `$IPResourceName` variable in the PowerShell script.

    c. Under **IP Address**, click **Static IP Address**. Set the IP address as the same address that you used when you set the load balancer address on the Azure portal.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/96-ip-resource.png" alt-text="Screenshot of the IP Address: Address on Cluster Network 1 Properties showing where to set the IP address.":::

    <!-----------------------I don't see this option on server 2016
    1. Disable NetBIOS for this address and click **OK**. Repeat this step for each IP resource if your solution spans multiple Azure VNets.
    ------------------------->

1. <a name = "dependencyGroup"></a>Make the SQL Server availability group resource dependent on the client access point.

    a. In Failover Cluster Manager, click **Roles**, and then click your availability group.

    b. On the **Resources** tab, under **Other Resources**, right-click the availability resource group, and then click **Properties**.

    c. On the dependencies tab, add the name of the client access point (the listener) resource.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/97-properties-dependencies.png" alt-text="Screenshot of MyTestAG properties, showing where to add the name on the Dependencies tab.":::

    d. Click **OK**.

1. <a name="listname"></a>Make the client access point resource dependent on the IP address.

    a. In Failover Cluster Manager, click **Roles**, and then click your availability group.

    b. On the **Resources** tab, right-click the client access point resource under **Server Name**, and then click **Properties**.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/98-dependencies.png" alt-text="Screenshot of the Failover Cluster Manager, zoomed in the MyTestAG resource, showing the Properties menu option for the listener's name.":::

    c. Click the **Dependencies** tab. Verify that the IP address is a dependency. If it is not, set a dependency on the IP address. If there are multiple resources listed, verify that the IP addresses have OR, not AND, dependencies. Click **OK**.

   :::image type="content" source="./media/virtual-machines-ag-listener-configure/98-properties-dependencies.png" alt-text="Screenshot of the Name: MyAgListnr Properties window, showing the IP Resource for the availability group.":::

    >[!TIP]
    >You can validate that the dependencies are correctly configured. In Failover Cluster Manager, go to Roles, right-click the availability group, click **More Actions**, and then click  **Show Dependency Report**. When the dependencies are correctly configured, the availability group is dependent on the network name, and the network name is dependent on the IP address.


1. <a name="setparam"></a>Set the cluster parameters in PowerShell.

   a. Copy the following PowerShell script to one of your SQL Server instances. Update the variables for your environment.

   - `$ListenerILBIP` is the IP address that you created on the Azure load balancer for the availability group listener.
    
   - `$ListenerProbePort` is the port you configured on the Azure load balancer for the availability group listener.

   ```powershell
   $ClusterNetworkName = "<MyClusterNetworkName>" # the cluster network name (Use Get-ClusterNetwork on Windows Server 2012 of higher to find the name)
   $IPResourceName = "<IPResourceName>" # the IP Address resource name
   $ListenerILBIP = "<n.n.n.n>" # the IP Address of the Internal Load Balancer (ILB). This is the static IP address for the load balancer you configured in the Azure portal.
   [int]$ListenerProbePort = <nnnnn>
  
   Import-Module FailoverClusters

   Get-ClusterResource $IPResourceName | Set-ClusterParameter -Multiple @{"Address"="$ListenerILBIP";"ProbePort"=$ListenerProbePort;"SubnetMask"="255.255.255.255";"Network"="$ClusterNetworkName";"EnableDhcp"=0}
   ```

   b. Set the cluster parameters by running the PowerShell script on one of the cluster nodes.  

   > [!NOTE]
   > If your SQL Server instances are in separate regions, you need to run the PowerShell script twice. The first time, use the `$ListenerILBIP` and `$ListenerProbePort` from the first region. The second time, use the `$ListenerILBIP` and `$ListenerProbePort` from the second region. The cluster network name and the cluster IP resource name are also different for each region.

1. Bring the availability group cluster role online. In **Failover Cluster Manager** under **Roles**, right click the role, and select **Start Role**.

If necessary, repeat the steps above to set the cluster parameters for the WSFC cluster IP address.

1. Get the IP address name of the WSFC Cluster IP address. In **Failover Cluster Manager** under **Cluster Core Resources**, locate **Server Name**.

1. Right-click **IP Address**, and select **Properties**.

1. Copy the **Name** of the IP address. It may be `Cluster IP Address`. 

1. <a name="setwsfcparam"></a>Set the cluster parameters in PowerShell.
  
   a. Copy the following PowerShell script to one of your SQL Server instances. Update the variables for your environment.

   - `$ClusterCoreIP` is the IP address that you created on the Azure load balancer for the WSFC core cluster resource. It is different from the IP address for the availability group listener.

   - `$ClusterProbePort` is the port you configured on the Azure load balancer for the WSFC health probe. It is different from the probe for the availability group listener.

   ```powershell
   $ClusterNetworkName = "<MyClusterNetworkName>" # the cluster network name (Use Get-ClusterNetwork on Windows Server 2012 of higher to find the name)
   $IPResourceName = "<ClusterIPResourceName>" # the IP Address resource name
   $ClusterCoreIP = "<n.n.n.n>" # the IP Address of the Cluster IP resource. This is the static IP address for the load balancer you configured in the Azure portal.
   [int]$ClusterProbePort = <nnnnn> # The probe port from the WSFCEndPointprobe in the Azure portal. This port must be different from the probe port for the availability group listener probe port.
  
   Import-Module FailoverClusters
  
   Get-ClusterResource $IPResourceName | Set-ClusterParameter -Multiple @{"Address"="$ClusterCoreIP";"ProbePort"=$ClusterProbePort;"SubnetMask"="255.255.255.255";"Network"="$ClusterNetworkName";"EnableDhcp"=0}
   ```

   b. Set the cluster parameters by running the PowerShell script on one of the cluster nodes.  

If the SQL Server database engine, Always On availability group listener, failover cluster instance health probe, database mirroring endpoint, cluster core IP resource, or any other SQL resource is configured to use a port between 49,152 and 65,536 (the [default dynamic port range for TCP/IP](/windows/client-management/troubleshoot-tcpip-port-exhaust#default-dynamic-port-range-for-tcpip)), add an exclusion for each port. Doing so will prevent other system processes from being dynamically assigned to the same port. For this scenario, the following exclusions should be configured in all cluster nodes:

- `netsh int ipv4 add excludedportrange tcp startport=58888 numberofports=1 store=persistent`
- `netsh int ipv4 add excludedportrange tcp startport=59999 numberofports=1 store=persistent`

It is important to configure the port exclusion when the port is not in use, otherwise the command will fail with a message like "The process cannot access the file because it is being used by another process."
To confirm that the exclusions have been configured correctly, use the following command: `netsh int ipv4 show excludedportrange tcp`  

>[!WARNING]
>The availability group listener health probe port has to be different from the cluster core IP address health probe port. In these examples, the listener port is 59999 and the cluster core IP address health probe port is 58888. Both ports require an allow inbound firewall rule.
