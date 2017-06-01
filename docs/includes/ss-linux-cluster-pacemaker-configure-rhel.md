
3. On all cluster nodes, open the Pacemaker firewall ports. To open these ports with `firewalld`, run the following command:

   ```bash
   sudo firewall-cmd --permanent --add-service=high-availability
   sudo firewall-cmd --reload
   ```

   > If you’re using another firewall that doesn’t have a built-in high-availability configuration, the following ports need to be opened for Pacemaker to be able to communicate with other nodes in the cluster
   >
   > * TCP: Ports 2224, 3121, 21064
   > * UDP: Port 5405

1. Install Pacemaker packages on all nodes.

   ```bash
   sudo yum install pacemaker pcs fence-agents-all resource-agents
   ```

   ​

2. Set the password for for the default user that is created when installing Pacemaker and Corosync packages. Use the same password on all nodes. 

   ```bash
   sudo passwd hacluster
   ```

   ​

3. Enable and start `pcsd` service and Pacemaker. This will allow nodes to rejoin the cluster after the reboot. Run the following command on all nodes.

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
   ```
   
   >[!NOTE]
   >If you previously configured a cluster on the same nodes, you need to use `--force` option when running `pcs cluster setup`. Note this is equivalent to running `pcs cluster destroy` and pacemaker service needs to be re-enabled using `sudo systemctl enable pacemaker`.

5. Install SQL Server resource agent for SQL Server. Run the following commands on both nodes. 

   ```bash
   sudo yum install mssql-server-ha
   ```
