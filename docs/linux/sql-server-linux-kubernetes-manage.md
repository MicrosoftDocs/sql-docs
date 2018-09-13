---
title: SQL Server Always On availability group Kubernetes specification
description: This article explains the parameters for the SQL Server Kubernetes Always On availability group specification
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 08/09/2018
ms.topic: article
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# SQL Server Always On Availability Group - Kubernetes specification

To configure an Always On Availability Group on Kubernetes, create a specification. The specification is a `.yaml` file.  

This article shows how to create the specification, and explains the parameters the parameters. See an example of the of the end-to-end deployment in [this tutorial](tutorial-sql-server-ag-kubernetes.md).



## Create a manifest file for the specification

The following example of a manifest file describes a Kubernetes specification for SQL Server. Copy the contents of the example into a new file named `sqlserver.yaml` to create the SQL Server availability group StatefulSet in Kubernetes.

[sqlserver.yaml](https://sqlhelsinki.visualstudio.com/_git/pm-tools?path=%2Fkubernetes-ag-samples%2Fazure-kubernetes-service-sql-ag-example&version=GBmaster#path=%2Fkubernetes-ag-samples%2Fazure-kubernetes-service-sql-ag-example%2Fsqlserver.yaml&version=GBmaster)

To deploy the SQL Server instances and create the availability group, run the following command.

```azurecli
kubectl apply -f sqlserver.yaml
```

## Parameters

* `sqlServerContainer` 
  * Required
  * **Description**: SQL Container spec. Should at least contain image path. In addition, it can have any env variable available for SQL Server container.
  * **Example**

   ```yaml
     sqlServerContainer:
     image: private-repo.microsoft.com/mssql-private-preview/mssql-server
   ```

* `saPassword`
  * Required
  * **Description**: The `sa` password for the sql server. Sets the initial password for the SQL Server instance. Changing this password through this field is not supported. Rotate the password instead.
  * **Example**

   ```yaml
     saPassword:
      secretKeyRef:
        name: sql-secrets
        key: sapassword
   ```

* `masterKeyPassword` 
  * Required
  * **Description**:  The server master key password for sql server. Used and for db mirroring certificates, logins, and passwords. It is regeneratable. You can change it post-deployment. DB mirroring logins are generated automatically as well.
  * **Example**

   ```yaml
   masterKeyPassword:
     secretKeyRef:
       name: sql-secrets
       key: masterkeypassword
   ```

* `agentsContainerImage`
  * Required
  * **Description**: The Agent container image name.
  * **Example**

   ```yaml
    agentsContainerImage: private-repo.microsoft.com/mssql-private-preview/mssql-ha-supervisor
   ```

* `acceptEula`
  * Required
  * **Description**: Boolean acceptance of the SQL Server EULA.
  * **Example**

   ```yaml
    acceptEula: true
   ```

* `instanceRootVolume`
  * Required if `instanceRootVolumeClaimTemplate` is not present.
  * **Description**: Volume source for the sql server container. For more information, see [Kubernetes storage volumes](https://kubernetes.io/docs/concepts/storage/volumes/).
  * **Example**

   ```yaml
     instanceRootVolume:
      persistentVolumeClaim:
       claimName: <claim name> 
   ```

* `instanceRootVolumeClaimTemplate` 
  * Required if `instanceRootVolume` is not present.
  * **Description**: Persistent Volume claim template for instance root [Kubernetes storage persistent volumes](https://kubernetes.io/docs/concepts/storage/persistent-volumes/).

* `pid`
  * Optional
  * **Description**: The SQL Server edition or product key. Defaults to Developer edition. See [Environment variables](sql-server-linux-configure-environment-variables.md).

* `monitorPolicy`
  * Optional 
  * **Description**: Integer health level used with `sp_server_diagnostics` to trigger failovers. Minimum: 0. Maximum: 5. Same semantics as monitor policy levels in Pacemaker/WSFC. 2 does not exist in Linux.

* `sqlServerPod`
  * Optional
  * **Description**: Pod spec with overrides for the SQL Server StatefulSet pod. Used when you use a private image. For more information, see [Kubernetes pods](http://kubernetes.io/docs/concepts/workloads/pods/pod/).

* `initSQLPod`
  * Optional
  * **Description**: Pod spec with overrides for the init-sql job pod. Used when you use a private image.

* `connectionTimeoutSec`
  * Optional
  * **Description**: Connection time out in seconds for the HA supervisors to connect to SQL Server. Minimum: 1 second. Default: 30 seconds.

* `queryCommandTimeoutSec`
  * Optional
  * **Description**: The generic SQL query command time out in seconds for waiting for data from a query. Minimum: 1. Default value: 10.
  
* `joinCommandTimeoutSec`
  * Optional
  * **Description**: The sql query command time out in seconds for waiting for data from a longer `ALTER AVAILABILITY GROUP <group_name> JOIN;`. Minimum: 1. Default value: 60. Recommended minimum is 60.

* `transientDBHealthTimeoutSec`
  * Optional
  * **Description**: Time out in seconds to wait for transient database states to come online after becoming the primary before triggering a failover. Minimum: 1. Default: 180. Used when `db_failover` is `ON`. Kubernetes Availability Group is always created with this setting `ON`, it can be updated to `OFF`.`

* `sqlPostInitScript`
  * Optional
  * **Description**: Post initialization script to run after every update of the [Kubernetes custom resource](http://kubernetes.io/docs/concepts/extend-kubernetes/api-extension/custom-resources/). Operations that use the post initialization script include:
    * resource create
    * resource change property
    * resource upgrade

* `availabilityGroups` 
  * Optional
  * **Description**: List of availability groups the server is a part of and the server replica mode.
  * **Example**

   ```yaml
     availabilityGroups:
      name: <availabilityGroupName>
       availabilityMode: <synchronousCommit | asynchronousCommit | configurationOnly> 
   ```

## Fail over - SQL Server availability group on Kubernetes

To fail over an Always On availability group primary replica to a different node in Kubernetes, use a job. This article identifies the environment variables for this job.

## Create a manifest file to describe the job

The following example of a manifest file describes a job to manually fail over job for an availability group on a Kubernetes replica. Copy the contents of the example into a new file called `manualFailover.yaml`.

```yaml
---
apiVersion: v1
kind: ServiceAccount
metadata:
  name: manual-failover

---
apiVersion: rbac.authorization.k8s.io/v1
kind: Role
metadata:
  name: manual-failover
rules:
- resources: ["configmaps"]
  apiGroups: [""]
  verbs: ["get", "update"]
  resourceNames: ["ag1"]
- resources: ["endpoints"]
  apiGroups: [""]
  verbs: ["get"]
  resourceNames: ["ag1"]
- resources: ["pods"]
  apiGroups: [""]
  verbs: ["list"]

---
apiVersion: rbac.authorization.k8s.io/v1
kind: RoleBinding
metadata:
  name: manual-failover
  namespace: default
roleRef:
  name: manual-failover
  apiGroup: rbac.authorization.k8s.io
  kind: Role
subjects:
- name: manual-failover
  kind: ServiceAccount

---
apiVersion: batch/v1
kind: Job
metadata:
  name: manual-failover
spec:
  template:
    metadata:
      name: manual-failover
    spec:
      serviceAccount: manual-failover
      restartPolicy: Never
      containers:
      - name: manual-failover
        image: mssql-ha-supervisor:20.22
        command: ["/mssql-server-k8s-failover"]
        env:
        - name: MSSQL_K8S_AG_NAME
          value: ag1
        - name: MSSQL_K8S_NEW_PRIMARY
          value: sql-1-0
        - name: MSSQL_K8S_NAMESPACE
          valueFrom:
            fieldRef:
              fieldPath: metadata.namespace
```

When you run the job, the supervisor will elect a new leader and move the primary replica to the SQL Server instance of the leader. To run the job, run the following command:

```azurecli
kubectl apply -f manualFailover.yaml
```

After you run the job, delete it. The job object in Kubernetes remains after completion so you can view its status. You have to manually delete old jobs after noting their status. Deleting the job also deletes the Kubernetes logs. If you do not delete the job, future failover jobs will fail unless you change the job name and the pod selector. For more information, see [Jobs - Run to Completion](https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/).

## Failover job environment variables

* `MSSQL_K8S_AG_NAME`
  * Optional
  * **Description**: The availability group name.

* `MSSQL_K8S_NEW_PRIMARY`
  * Optional
  * **Description**: The target of the failover. Accepts the target SQL Server pod name, IP address, StatefulSet name, server name, or host name. Additionally the `!` prefix prevents failover to a  specified target. Example `!sql-1`.

* `FORCE_FAILOVER_ALLOW_DATA_LOSS` 
  * Optional
  * **Description**: Boolean to force the failover with potential data loss.

* `MSSQL_K8S_NAMESPACE`
  * Optional
  * **Description**: Kubernetes namespace of the availability group.

## Next steps

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)