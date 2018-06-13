---
title: Configure a SQL Server Always On availability group in Kubernetes for high availability | Microsoft Docs
description: This tutorial shows how to deploy a SQL Server always on availability group with Kubernetes on Azure Container Service.
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
ms.date: 07/16/2018
ms.topic: tutorial
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux,mvc"
ms.technology: linux
---
# Configure a SQL Server Always On availability group in Kubernetes for high availability

Learn how to configure a SQL Server Always On availability group on [Kubernetes](http://kubernetes.io) in [Azure Kubernetes Service (AKS)](http://docs.microsoft.com/azure/aks/). This solution provides the high availability (HA) and read-scale benefits of a [SQL Server Always On availability group](../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) with the container orchestration from Kubernetes.

This diagram represents the solution that you will complete with this tutorial:

![kubernetes-ag-cluster](media/tutorial-sql-server-ag-containers-kubernetes/KubernetesCluster.png)

In this tutorial, you will:

> [!div class="checklist"] 
> * Configure storage
> * Deploy the SQL Server operator to a Kubernetes cluster 
> * Deploy SQL Server instances and health agents
> * Verify failure and recovery

## Before you begin

You will need a Kubernetes cluster with four nodes. For instructions, refer to [Tutorial: Deploy an Azure Kubernetes Service (AKS) cluster](http://docs.microsoft.com/azure/aks/tutorial-kubernetes-deploy-cluster).

Install [`kubctl` CLI](https://docs.microsoft.com/en-us/azure/aks/tutorial-kubernetes-deploy-cluster#install-the-kubectl-cli). 

## Configure 

## Create storage

### Define the storage

Create a manifest file to define the storage class and the persistent volume claim for each of the 3 SQL Server instances that will be part of the AG. The Kubernetes cluster uses this manifest to create the persistent storage.

The following yaml file example, defines a storage class and persistent volume claims. The storage class provisioner is `azure-disk` because this Kubernetes cluster is in Azure. The persistent volume claims are `mssql-data-1`, `mssql-data-2`, `mssql-data-3`. The persistent volume claim metadata includes an annotation connecting it back to the storage class.

```yaml
kind: StorageClass
apiVersion: storage.k8s.io/v1beta1
metadata:
     name: azure-disk
provisioner: kubernetes.io/azure-disk
parameters:
  storageaccounttype: Standard_LRS
  kind: Managed
---
kind: PersistentVolumeClaim
apiVersion: v1
metadata:
  name: mssql-data-1
  annotations:
    volume.beta.kubernetes.io/storage-class: azure-disk
spec:
  accessModes:
  - ReadWriteOnce
  resources:
    requests:
      storage: 8Gi
---
kind: PersistentVolumeClaim
apiVersion: v1
metadata:
  name: mssql-data-2
  annotations:
    volume.beta.kubernetes.io/storage-class: azure-disk
spec:
  accessModes:
  - ReadWriteOnce
  resources:
    requests:
      storage: 8Gi
---
kind: PersistentVolumeClaim
apiVersion: v1
metadata:
  name: mssql-data-3
  annotations:
    volume.beta.kubernetes.io/storage-class: azure-disk
spec:
  accessModes:
  - ReadWriteOnce
  resources:
    requests:
      storage: 8Gi
```

Save the file `pvc.yaml`.

### Create the persistent volume claims in Kubernetes

```azurecli
kubectl apply -f <Path to pvc.yaml file>
```

Kubernetes creates the persistent volumes automatically as Azure managed storage accounts, and binds them to the persistent volume claims. 

### Verify the persistent volume claim

```azurecli
kubectl describe pvc <PersistentVolumeClaim>
```

In the preceding step, the persistent volume claim is named `mssql-data-<x>` where `<x>` is a number. For example, `mssql-data-1`. To see metadata about the persistent volume claim for the SQL Server instance `mssql-data-1`, run the following command: 

```azurecli
kubectl describe pvc mssql-data-1
```

### Verify the persistent volumes

```azurecli
kubectl describe pv
```

`kubectl` returns metadata about the persistent volumes that were automatically created and bound to the persistent volume claims.

## Deploy the operator, SQL Server instances, and health agents

The operator is deployed as an one-replica Kubernetes deployment.

### Create an imagePullSecrets secret in Kubernetes

This enables access to pull from the private preview registry where the `mssql-server` and `mssql-server-k8s-agents` images are. The credentials for the Docker registry (username, password and email) are provided to you in the EAP dashboard.

```azurecli
kubectl create secret docker-registry private-registry-key --docker-username="dockerUser" --docker-password="xxxxxx" --docker-email="username@example.com" --docker-server="private-repo.microsoft.com"
```

### Configure the deployment manifest

In this step, create a manifest to describe the operator container based on the mssql-server-k8s-agents Docker image. The manifest also describes the operator and SQL Server controller permissions. The controller runs inside the operator container and manages the SqlServer custom resource.

```yaml
---
apiVersion: v1

kind: ServiceAccount

metadata:
  name: mssql-operator

---
apiVersion: rbac.authorization.k8s.io/v1

kind: ClusterRole

metadata:
  name: mssql-operator

rules:
# operator permissions
- resources: ["configmaps"]
  apiGroups: [""]
  verbs: ["create"]
- resources: ["configmaps"]
  apiGroups: [""]
  resourceNames: ["sql-operator"]
  verbs: ["get", "update"]
- resources: ["customresourcedefinitions"]
  apiGroups: ["apiextensions.k8s.io"]
  verbs: ["create"]
- resources: ["customresourcedefinitions"]
  apiGroups: ["apiextensions.k8s.io"]
  resourceNames: ["sqlservers.mssql.microsoft.com"]
  verbs: ["delete", "get", "update"]

# sqlserver controller permissions
- resources: ["sqlservers"]
  apiGroups: ["mssql.microsoft.com"]
  verbs: ["get", "list", "watch"]
- resources: ["endpoints"]
  apiGroups: [""]
  verbs: ["create", "delete", "get", "update"]
- resources: ["jobs"]
  apiGroups: ["batch"]
  verbs: ["create", "delete", "get", "update"]
- resources: ["roles", "rolebindings"]
  apiGroups: ["rbac.authorization.k8s.io"]
  verbs: ["create", "delete", "get", "update"]
- resources: ["services"]
  apiGroups: [""]
  verbs: ["create", "delete", "update", "get"]
- resources: ["serviceaccounts"]
  apiGroups: [""]
  verbs: ["create", "delete", "update", "get"]
- resources: ["statefulsets"]
  apiGroups: ["apps"]
  verbs: ["create", "delete", "get", "update"]

# k8s-init-sql role permissions; required by operator to be able to create roles with these permissions
- apiGroups: [""]
  resources: ["pods"]
  verbs: ["get", "list"]
- apiGroups: [""]
  resources: ["secrets"]
  verbs: ["create", "get", "update"]

# k8s-health-agent role permissions; required by operator to be able to create roles with these permissions
- apiGroups: [""]
  resources: ["secrets"]
  verbs: ["get"]

# k8s-ag-agent-supervisor role permissions; required by operator to be able to create roles with these permissions
- resources: ["pod"]
  apiGroups: [""]
  verbs: ["get", "update"]

# k8s-ag-agent role permissions; required by operator to be able to create roles with these permissions
- resources: ["configmaps"]
  apiGroups: [""]
  verbs: ["create", "get", "update"]
- resources: ["endpoints"]
  apiGroups: [""]
  verbs: ["get"]
- resources: ["pods"]
  apiGroups: [""]
  verbs: ["list", "update"]

---
apiVersion: rbac.authorization.k8s.io/v1

kind: ClusterRoleBinding

metadata:
  name: mssql-operator

roleRef:
  name: mssql-operator
  apiGroup: rbac.authorization.k8s.io
  kind: ClusterRole

subjects:
- name: mssql-operator
  namespace: default
  kind: ServiceAccount

---
apiVersion: apps/v1beta2

kind: Deployment

metadata:
  name: mssql-operator

spec:
  selector:
    matchLabels:
      app: mssql-operator
  replicas: 1
  template:
    metadata:
      labels:
        app: mssql-operator
    spec:
      serviceAccount: mssql-operator
      containers:
      - name: mssql-operator
        image: private-repo.microsoft.com/mssql-private-preview/mssql-server-k8s-agents
        command: ["/mssql-server-k8s-operator"]
        env:
        - name: MSSQL_K8S_POD_NAMESPACE
          valueFrom:
            fieldRef:
              fieldPath: metadata.namespace
      imagePullSecrets:
      - name: private-registry-key
```

Save the file, for example `operator.yaml`.

### Create the operator deployment

```azurecli
kubectl apply -f <Path to operator.yaml file>
```

### Create the SQL Server AG Deployment

The following example illustrates a SQL Server deployment in an AG configuration. This deployment will result in 3 StatefulSets with one pod each. Every pod will include 3 containers, one for each of the following items:

* SQL Server instance
* Health agent
* AG agent

At deployment time, specify values for these parameters (see below sample configuration file):

* Instance root volume mount / PVCT
* `mssql-server` container image name
* `mssql-server-k8s-agents` container image name
* SA password. If new, set the SA Password. If mounting an existing instance, root.
* desired master key password
* `acceptEula: true` to indicate that EULA is accepted

Optionally, provide additional parameters like the example below (or use the specified default values):

* health check monitor policy – default is critical errors only (3)
* PID (same value as MSSQL_PID env var for regular container) – default is Developer
* additional properties of the `mssql-server` container
* additional properties of the pod, like (anti-)affinity
* additional properties for the `init-sql` job pod, like image pull secrets
* AG membership list
* post-init T-SQL script

After deployment, only AG membership list and post-init T-SQL script can be updated. Other properties cannot be updated - the resource must be deleted and recreated. Credentials for the auto-generated users can be rotated using a `mssql-server-k8s-rotate-creds` job.

## Create the Kubernetes secret to store the masterkey password and the sa password

```azurecli
kubectl create secret generic sql-secrets --from-literal=sapassword='MyC0m9l&xP@ssw0rd' --from-literal=masterkeypassword='MyC0m9l&xP@ssw0rd2'
```

## Configure the deployment manifest

In this step, create a manifest to describe the SQL Server container based on the mssql-server image, the health agent and AG agent containers based on the mssql-server-k8s-agents Docker image.

```yaml
---
apiVersion: mssql.microsoft.com/v1
kind: SqlServer
metadata:
  name: sql-1
  labels:
    type: sqlservr
spec:
  sqlServerContainer:
    image: private-repo.microsoft.com/mssql-private-preview/mssql-server

  saPassword:
    secretKeyRef:
      name: sql-secrets
      key: sapassword

  masterKeyPassword:
    secretKeyRef:
      name: sql-secrets
      key: masterkeypassword

  instanceRootVolume:
    persistentVolumeClaim:
      claimName: mssql-data-1

  agentsContainerImage: private-repo.microsoft.com/mssql-private-preview/mssql-server-k8s-agents

  acceptEula: true
  
  sqlServerPod:
    imagePullSecrets:
    - name: private-registry-key

  initSQLPod:
    imagePullSecrets:
    - name: private-registry-key

  availabilityGroups:
  - ag1

---
apiVersion: mssql.microsoft.com/v1
kind: SqlServer
metadata:
  name: sql-2
  labels:
    type: sqlservr
spec:
  sqlServerContainer:
    image: private-repo.microsoft.com/mssql-private-preview/mssql-server
  saPassword:
    secretKeyRef:
      name: sql-secrets
      key: sapassword

  masterKeyPassword:
    secretKeyRef:
      name: sql-secrets
      key: masterkeypassword

  instanceRootVolume:
    persistentVolumeClaim:
      claimName: mssql-data-2

  agentsContainerImage: private-repo.microsoft.com/mssql-private-preview/mssql-server-k8s-agents

  acceptEula: true

  sqlServerPod:
    imagePullSecrets:
    - name: private-registry-key

  initSQLPod:
    imagePullSecrets:
    - name: private-registry-key

  availabilityGroups:
  - ag1

---
apiVersion: mssql.microsoft.com/v1
kind: SqlServer
metadata:
  name: sql-3
  labels:
    type: sqlservr
spec:
  sqlServerContainer:
    image: private-repo.microsoft.com/mssql-private-preview/mssql-server

  saPassword:
    secretKeyRef:
      name: sql-secrets
      key: sapassword

  masterKeyPassword:
    secretKeyRef:
      name: sql-secrets
      key: masterkeypassword

  instanceRootVolume:
    persistentVolumeClaim:
      claimName: mssql-data-3

  agentsContainerImage: private-repo.microsoft.com/mssql-private-preview/mssql-server-k8s-agents

  acceptEula: true

  sqlServerPod:
    imagePullSecrets:
    - name: private-registry-key

  initSQLPod:
    imagePullSecrets:
    - name: private-registry-key

  availabilityGroups:
  - ag1

---
apiVersion: v1
kind: Service
metadata:
  name: ag1-primary
spec:
  ports:
  - name: tds
    port: 1433
    targetPort: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: primary
  type: LoadBalancer

```

Save the file, for example `sqlservers.yaml`.

### Create the SQL Server deployment 

```azurecli
kubectl apply -f <Path to sqlservers.yaml file>
```

##  Connect to the SQL Server instance hosting the primary replica

The configuration file also deploys an `ag1-primary` service that provides a selector that points to the SQL Server instance hosting the primary replica. Use the external IP address of the service as target server, `sa` account and the password you created earlier in the `mssql secret`. Use the password that you configured as the Kubernetes secret.

## Connect to the SQL Server instance hosting secondary replicas
Similarly, we provide built-in selectors that point to sync, async secondary replicas or both:

```yaml
---
apiVersion: v1
kind: Service
metadata:
  name: ag1-secondary
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    matchExpressions:
    - {key: role.ag.mssql.microsoft.com/ag1, operator: In, values: ["secondary-sync", "secondary-async", "secondary-config"]}
  type: LoadBalancer

---
apiVersion: v1
kind: Service
metadata:
  name: ag1-secondary-sync
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: secondary-sync
  type: LoadBalancer

---
apiVersion: v1
kind: Service
metadata:
  name: ag1-secondary-async
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: secondary-async
  type: LoadBalancer
```

## Verify failure and recovery

To verify failure detection and failover you can delete the pod hosting the primary replica. Do the following steps:

1. List the pod running SQL Server.

   ```azurecli
   kubectl get pods
   ```

2. Identify the pod running the primary replica.

   Either connect to the primary replica using the external IP and query `@@servername` or use `kubectl` to get the appropriate pod. This command will return the name of the pod that includes the container running the primary replica of the AG:

   ```azurecli
   kubectl get pod -o json | jq '.items[] | select(.metadata.annotations["role.ag.mssql.microsoft.com/ag1"] == "primary") | .metadata.name' -r
   ```

3. Delete the pod.

   ```azurecli
   kubectl delete pod <podName>
   ```

Replace `<podName>` with the value returned from the previous step for pod name. 

Kubernetes automatically fails over to one of the available sync secondary replicas as well as recreates the deleted pod.


## Summary

## Next steps

> [!div class="nextstepaction"]
>[Introduction to Kubernetes](http://docs.microsoft.com/en-us/azure/aks/intro-kubernetes)


