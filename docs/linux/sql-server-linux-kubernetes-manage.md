---
title: Manage a SQL Server Always On availability group Kubernetes
description: This article explains how to manage a SQL Server Always On Availability Group in Kubernetes.
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 09/24/2018
ms.topic: article
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Manage SQL Server Always On Availability Group Kubernetes

To manage an Always On Availability Group on Kubernetes, create a manifest and apply it to the cluster. The manifest is a `.yaml` file.  

The examples in this article apply to all Kubernetes cluster. The scenarios in these examples are applied against a cluster on Azure Kubernetes Service.

See an example of the complete deployment in [Deploy a SQL Server Always On Availability Group on Kubernetes Cluster](sql-server-linux-kubernetes-deploy.md).

## Fail over - SQL Server availability group on Kubernetes

To fail over or move a primary replica to a different node in an availability group, complete the following steps:

1. Define a job in a manifest file.

  [`failover.yaml`](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files/failover.yaml) - in the [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-manifest-files) github repository describes a failover job.

  Copy the manifest file to your administration terminal.

  Update the file for your environment.

  - Replace `<containerName>` with the pod name (e.g. mssql2-0) of the expected availability group target.
  - If the availability group isn't in the `ag1` namespace, replace `ag1` with the namespace.

  This file defines a failover job named `manual-failover`.

1. To deploy the job, use `kubectl apply`. The following script deploys the job.

  ```azurecli
  kubectl apply -f failover.yaml
  ```

  After the job is deployed, kubernetes, with the SQL Server Operator, does the following tasks:
  
  - Demotes the primary replica to secondary
  
  - Promotes the specified replica to primary
  
  After you apply manifest file, Kubernetes runs the job. The job makes the supervisor select a new leader and moves the primary replica to the SQL Server instance of the leader.

1. Verify the job is completed.
  
  After Kubernetes runs the job, you can review the log.
  
  The following example returns the status of the job named `manual-failover`.

  ```azurecli
  kubectl describe jobs/manual-failover --namespace ag1
  ```

1. Delete the manual failover job. 

  >[!IMPORTANT]
  >You must delete the job manually before you issue another manual failover.
  > 
  >The job object in Kubernetes stays after completion so you can view its status. You need to manually delete old jobs after noting their status. Deleting the job also deletes the Kubernetes logs. If you don't delete the job, future failover jobs will fail unless you change the job name and the pod selector. For more information, see [Jobs - Run to Completion](https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/).

  The following command deletes the job.

  ```azurecli
  kubectl delete jobs manual-failover --namespace ag1
  ```

## Rotate credentials

Rotate credentials to reset the password for the SQL Server `sa` account and the SQL Server [service master key](../relational-databases/security/encryption/service-master-key.md). 

To complete this task, you will create new secrets in the Kubernetes cluster and then create a job to rotate the credentials.

Before rotating credentials, make a new secret for the password and the master key.

The following script creates a secret named `new-sql-secrets`. Before you run the script, replace `<>` with complex passwords for the `sapassword` and the `masterkeypassword`. Use different passwords for each respective value.

```azurecli
kubectl create secret generic new-sql-secrets --from-literal=sapassword="<>" --from-literal=masterkeypassword="<>"  --namespace ag1
```

Complete the following steps for every instance of SQL Server that needs the master key or `sa` password.

1. Copy [`rotate-creds.yaml`](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/high%20availability/Kubernetes/sample-manifest-files/rotate-creds.yaml) to your administration terminal.

  [`rotate-creds.yaml`](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/high%20availability/Kubernetes/sample-manifest-files/rotate-creds.yaml) in the [sql-server-samples](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/high%20availability/Kubernetes/sample-deployment-script/) github repository is an example of a manifest for this job.

  Before you apply this manifest, update the manifest for your environment. Review and change the following settings, as required.

  - Verify the namespace. Update if necessary. The following example in a manifest applies to a namespace named `ag1`.

    ```yaml
    metadata:
      name: rotate-creds
      namespace: ag1
    ```

  - Verify the name of the SQL Server instance. Update if necessary. The following example in a manifest spec applies to a SQL Server instance named `mssql1`.

    ```yaml
    env:
      - name: MSSQL_K8S_SQL_SERVER_NAME
        value: mssql1
    ```

  Save the updated manifest file to your workstation.

1. Use `kubectl` to deploy the job.

  ```azurecli
  kubectl apply -f rotate-creds.yaml --namespace ag1
  ```

  Kubernetes updates the master key and `sa` password for one instance of SQL Server in an availability group.

1. Verify that the job is completed. Run the following command: To verify that the job is completed, run 

  ```azcli
  kubectl describe job rotate-creds --namespace ag1
  ```

  After the job succeeds, the master key and `sa` password for one instance of SQL Server are updated.


1. Before you run the job again, delete the job. Each job name must be unique.

  ```azurecli
  kubectl delete job rotate-creds --namespace ag1
  ```

To set the same `sa` password for all instances of SQL Server, repeat the steps above for each instance of SQL Server.

## Next steps

[Access the Kubernetes dashboard with Azure Kubernetes Service (AKS)](https://docs.microsoft.com/azure/aks/kubernetes-dashboard)

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)
