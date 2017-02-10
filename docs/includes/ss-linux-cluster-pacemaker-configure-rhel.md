1. **On both cluster nodes, create a file to store the SQL Server username and password for the Pacemaker login**. The following command creates and populates this file:

    ```bash
    sudo touch /var/opt/mssql/secrets/passwd
    sudo echo '<loginName>' >> /var/opt/mssql/secrets/passwd
    sudo echo '<loginPassword>' >> /var/opt/mssql/secrets/passwd
    sudo chown root:root /var/opt/mssql/secrets/passwd 
    sudo chmod 600 /var/opt/mssql/secrets/passwd
    ```
2. **All cluster nodes must be able to access each other via SSH**. Tools like hb_report or crm_report (for troubleshooting) and Hawk's History Explorer require passwordless SSH access between the nodes, otherwise they can only collect data from the current node. In case you use a non-standard SSH port, use the -X option (see man page). For example, if your SSH port is 3479, invoke an hb_report with:

    ```bash
    crm_report -X "-p 3479" [...]
    ```

    For more information, see [System Requirements and Recommendations in the SUSE documentation](https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_requirements_other.html).

3. **Install the High Availability extension**. To install the extension, follow the steps in the following SUSE topic:
    
    [Installation and Setup Quick Start](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html)

4. **Install the FCI resource agent for SQL Server**. Run the following commands on both nodes:

    ```bash
    sudo zypper addrepo -fc https://packages.microsoft.com/config/sles/12/mssql-server.repo
    sudo zypper --gpg-auto-import-keys refresh
    sudo zypper install mssql-server-ha
    ```

5. **Automatically set up the first node**. The next step is to setup a running one-node cluster by configuring the first node, SLES1. Follow the instructions in the SUSE topic, [Setting Up the First Node](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.setup.1st-node).

    When finished, check the cluster status with `crm status`:
    ```bash
    crm status
    ```

    It should show that one node, SLES1, is configured.

6. **Add nodes to an existing cluster**. Next join the SLES2 node to the cluster. Follow the instructions in the SUSE topic, [Adding the Second Node](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.setup.2nd-node).
    
    When finished, check the cluster status with **crm status**. If you have successfully added a second node, the output will be similar to the following:
        
    ```
    2 nodes configured
    1 resource configured
    Online: [ SLES1 SLES2 ]
    Full list of resources:
    admin_addr     (ocf::heartbeat:IPaddr2):       Started SLES1
    ```

    > [!NOTE]
    > **admin_addr** is the virtual IP cluster resource which is configured during initial one-node cluster setup.

7.	**Removal procedures**. If you need to remove a node from the cluster, use the **ha-cluster-remove** bootstrap script. For more information, see [Overview of the Bootstrap Scripts](https://www.suse.com/documentation/sle-ha-12/singlehtml/install-quick/install-quick.html#sec.ha.inst.quick.bootstrap).  

