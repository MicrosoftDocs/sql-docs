---
title: Deploy SQL Server Big Data Cluster with high availability
titleSuffix: Deploy SQL Server Big Data Cluster with high availability 
description: Learn how to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview) to with high availability.
author: mihaelablendea
ms.author: mihaelab 
ms.reviewer: mikeray
ms.date: 08/28/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Deploy SQL Server Big Data Cluster with high availability

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

At the time of deploying a big data cluster (BDC), you can configure SQL Server master to be deployed in an availability group configuration. This configuration increases the reliability of SQL Server master, on top of what Kubernetes infrastructure enables with its built-in health monitoring, failure detection, and failover mechanisms. Availability groups (AG) add redundancy to the SQL Server instance. In this configuration, monitoring, failure detection, and failover tasks are managed by big data cluster management service, namely the control service.

In addition, other administration tasks like setting up database mirroring endpoints, creating the availability group and adding databases to the availability group are provided by the big data cluster platform.

Here are some of the capabilities that availability groups enable:

1. If the high availability settings are specified in the deployment configuration file, a single availability group named `containedag` is created. By default, `containedag` has three replicas, including primary. All CRUD operations for the availability group are managed internally.
1. All databases are automatically added to the availability group, including `master` and `msdb`. Polybase configuration databases are not included in the availability group because they include instance level metadata specific to each replica.
1. An external endpoint is automatically provisioned for connecting to the AG databases. This endpoint `master-svc-external` plays the role of the AG listener.
1. A second external endpoint is provisioned for read-only connections to the secondary replicas. 


# Deploy

To deploy SQL Server master in an availability group:

1. Enable the `hadr` feature
1. Specify the number of replicas for the AG (minimum is 3)
1. Configure the details of the second external endpoint created for connections to the read-only secondary replicas

The following steps show how to create a patch file that includes these settings and how to apply it to either `aks-dev-test` or `kubeadm-dev-test` configuration profiles. These steps walk through an example on how to patch the `aks-dev-test` profile to add the HA attributes.For a deployment on a kubeadm cluster, similar patch would apply, but make sure you are using *NodePort* for the **serviceType** in the  **endpoints** section.

1. Create a `patch.json` file

    ```json
    {
      "patch": [
        {
          "op": "replace",
          "path": "spec.resources.master.spec",
          "value": {
            "type": "Master",
            "replicas": 3,
            "endpoints": [
              {
                "name": "Master",
                "serviceType": "LoadBalancer",
                "port": 31433
              },
              {
                "name": "MasterSecondary",
                "serviceType": "LoadBalancer",
                "port": 31436
              }
            ],
            "settings": {
              "sql": {
                "hadr.enabled": "true"
              }
            }
          }
        }
      ]
    }
    ```

1. Clone your targeted profile

    ```bash
    azdata bdc config init --source aks-dev-test --target custom-aks
    ```

1. Apply the patch file to your custom profile

    ```bash
    azdata bdc config patch -c custom-aks/bdc.json --patch-file patch.json
    ```
1. Start cluster deployment using the cluster configuration profile created above

    ```bash
    azdata bdc create --config-profile custom-aks --accept-eula yes
    ```

## Connect to SQL Server databases

Depending on the type of workload you want to run against SQL Server master, you can connect either to the primary for read-write workloads or to the databases in the secondary replicas for read-only type of workloads. Here is an outline for each type of connection:

### Connect to databases on the primary replica

For connections to the primary replica, use `sql-server-master` endpoint. This endpoint is also the listener for the AG. All connections are in the context of the availability group. For example, a default connection using this endpoint will result in connecting to the `master` database within the AG, not the SQL Server instance `master` database.

```bash
azdata bdc endpoint list -e sql-server-master -o table
```

`Description                           Endpoint             Name               Protocol`
`------------------------------------  -------------------  -----------------  ----------`
`SQL Server Master Instance Front-End  13.64.235.192,31433  sql-server-master  tds`

> [!NOTE]
> Failover events can occur during a distributed query execution that is accessing data from remote data sources like HDFS or data pool. As a best practice, applications should be designed to have connection retry logic in case of disconnects caused by failover.  
>

### Connect to databases on the secondary replicas

For read-only connections to databases in secondary replicas, use the `sql-server-master-readonly` endpoint. This endpoint acts like a load balancer across all the secondary replicas. Provide the user database context in the connection string.

```bash
azdata bdc endpoint list -e sql-server-master-readonly -o table
```

`Description                                    Endpoint            Name                        Protocol`
`---------------------------------------------  ------------------  --------------------------  ----------`
`SQL Server Master Readable Secondary Replicas  13.64.238.24,31436  sql-server-master-readonly  tds`

### <a id="instance-connect"></a> Connect to SQL Server instance

For certain operations like setting server level configurations or manually adding a database to the availability group (in case database was created with a restore workflow), you need a connection to the instance. To provide this connection, expose an external endpoint. Here is an example that shows how to expose this endpoint and then add the database that was created with a restore workflow to the availability group.

- Determine the pod that hosts the primary replica by connecting to the `sql-server-master` endpoint and run:

    ```sql
    SELECT @@SERVERNAME
   ```

- Expose the external endpoint by creating a new Kubernetes service

    For a kubeadm cluster run below command. Replace `podName` with the name of the server returned at previous step, `serviceName` with the preferred name for the Kubernetes service created  and `namespaceName`* with the name of your BDC cluster.

    ```bash
    kubectl -n <namespaceName> expose pod <podName> --port=1533  --name=<serviceName> --type=NodePort
    ```

    For an aks cluster run, run the same command, except that the type of the service created will be `LoadBalancer`. For example: 

    ```bash
    kubectl -n <namespaceName> expose pod <podName> --port=1533  --name=<serviceName> --type=LoadBalancer
    ```

    Here is an example of this command run against aks, where the pod hosting the primary is `master-0`:

    ```bash
    kubectl -n mssql-cluster expose pod master-0 --port=1533  --name=master-sql-0 --type=LoadBalancer
    ```

    Get the IP of the Kubernetes service created:

    ```bash
    kubectl get services -n <namespaceName>
    ```

> [!IMPORTANT]
> As a best practice, you should cleanup by deleting the Kubernetes service created above by running this command:
>
>```bash
>kubectl delete svc master-sql-0 -n mssql-cluster
>```

- Add the database to the availability group.

    For the database to be added to the AG, it has to run in full recovery mode and a log backup has to be taken. Use the IP from the Kubernetes service created above and connect to the SQL Server instance then run the TSQL statements as shown below.

    ```sql
    ALTER DATABASE <databaseName> SET RECOVERY FULL;
    BACKUP DATABASE <databaseName> TO DISK='<filePath>'
    ALTER AVAILABILITY GROUP containedag ADD DATABASE <databaseName>
    ```

    The following example adds a database named `sales` that was restored on the instance:

    ```sql
    ALTER DATABASE sales SET RECOVERY FULL;
    BACKUP DATABASE sales TO DISK='/var/opt/mssql/data/sales.bak'
    ALTER AVAILABILITY GROUP containedag ADD DATABASE sales
    ```

## Known limitations

These are the known issues and limitations with availability groups for SQL Server master in big data cluster:

- Databases created as result of workflows other than `CREATE DATABASE` like `RESTORE`, `CREATE DATABASE FROM SNAPSHOT` are not automatically added to the availability group. [Connect to the instance](#instance-connect) and add the database to the availability group manually.
- Certain operations like running server configuration settings with `sp_configure` require a connection to the master instance. You cannot use the corresponding primary endpoint. Follow [the instructions](#instance-connect) to connect to the SQL Server instance and run `sp_configure`.
- The high availability configuration must be created when BDC is deployed. You cannot enable the high availability configuration with availability groups post deployment.

## Next steps

- For more information about using configuration files in big data cluster deployments, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md#configfile).
- For more information about Availability Groups feature for SQL Server, see [Overview of Always On Availability Groups (SQL Server)](https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server?view=sql-server-2017).
