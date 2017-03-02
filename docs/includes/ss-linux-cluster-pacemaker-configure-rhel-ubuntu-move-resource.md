>[!IMPORTANT]
>After you manually failover a resource, you need to remove a location constraint that is automatically added during the move.

### Remove the location constraint

During a manual move, the `move` command adds a location constraint for the resource to be placed on the new target node. To see the new constraint, run the following command after manually moving the resource:

```bash
sudo pcs constraint --full
```

You need to remove the location constraint so future moves - including automatic failover - succeed. 

To remove the constraint run the following command. In the following command `ag_cluster-master` is the name of the resource that was moved. Replace this name with the name of your resource:

```bash
sudo pcs resource clear ag_cluster-master 
```

Alternatively, you can run the following command to remove the location constraint. In the following command `ag_cluster-master` is the name of the resource that was moved. `-INFINITY` is the ID of the constraint. `sudo pcs constraint --full` returns this ID. 

```bash
sudo pcs constraint remove location-ag_cluster-master -INFINITY 
```

>[!NOTE]
>Automatic failover does not add a location constraint, so no cleanup is necessary. 

For more information:
- [Red Hat - Managing Cluster Resources](http://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/6/html/Configuring_the_Red_Hat_High_Availability_Add-On_with_Pacemaker/ch-manageresource-HAAR.html)
- [Pacemaker - Move Resources Manaually](http://clusterlabs.org/doc/en-US/Pacemaker/1.1-pcs/html/Clusters_from_Scratch/_move_resources_manually.html)
