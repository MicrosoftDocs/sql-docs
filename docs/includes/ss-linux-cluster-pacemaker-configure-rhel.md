3. On all cluster nodes, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

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

2. Set the password for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on all nodes. 

   ```bash
   sudo passwd hacluster
   ```

3. To allow nodes to rejoin the cluster after the reboot, enable and start `pcsd` service and Pacemaker. Run the following command on all nodes.

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   sudo systemctl enable pacemaker
   ```

4. Create the Cluster. To create the cluster, run the following command:

   ```bash
   sudo pcs cluster auth <node1> <node2> <node3> -u hacluster -p <password for hacluster>
   sudo pcs cluster setup --name <clusterName> <node1> <node2> <node3> 
   sudo pcs cluster start --all
   sudo pcs cluster enable --all
   ```
   
   >[!NOTE]
   >If you previously configured a cluster on the same nodes, you need to use `--force` option when running `pcs cluster setup`. This option is equivalent to running `pcs cluster destroy`. To re-enable pacemaker, run `sudo systemctl enable pacemaker`.

5. Install SQL Server resource agent for SQL Server. Run the following commands on all nodes. 

   ```bash
   sudo yum install mssql-server-ha
   ```
