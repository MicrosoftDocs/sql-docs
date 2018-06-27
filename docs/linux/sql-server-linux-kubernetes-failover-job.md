---
title: SQL Server Always On availability group Kubernetes failover job environment variables
description: This article explains the environment variables for the SQL Server Kubernetes Always On availability group failover job
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 7/16/2018
ms.topic: article
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: linux
---

# Fail over - SQL Server availability group on Kubernetes

To fail over an Always On availability group primary replica to a different node in Kubernetes, use a job. This article identifies the environment variables for this job.

## Create a manifest file to describe the job

The following example of a manifest file describes a job to do a manual fail over job for an availabiltiy group on a Kubernetes replica. Copy the contents of the example into a new file called `manualFailover.yaml`.

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
        image: mssql-server-k8s-agents:20.22
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

When you run the job, the AG agents will elect a new leader and move the primary replica to the SQL Server instance of the leader. To run the job, run the following command:

```azurecli
kubectl apply -f manualFailover.yaml
```

After you run the job, delete it. The job object in Kubernetes remains after completion so you can view its status. You have to manually delete old jobs after noting their status. Deleting the job also deletes the Kubernetes logs. If you do not delete the job, future failover jobs will fail unless you change the job name and the pod selector. For more information see [Jobs - Run to Completion](https://kubernetes.io/docs/concepts/workloads/controllers/jobs-run-to-completion/).

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