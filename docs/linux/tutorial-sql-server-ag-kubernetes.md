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

Install [`kubctl`](https://docs.microsoft.com/en-us/azure/aks/tutorial-kubernetes-deploy-cluster#install-the-kubectl-cli). 

## Configure 

## Create storage

In Kubernetes, a [*persistent volume*](http://kubernetes.io/docs/concepts/storage/persistent-volumes/) is a piece of storage in the cluster. A *persistent volume claim* (PVC) is a request for storage by a user.

In Azure Kubernetes Service (AKS), you [create a persistent volume claim](http://docs.microsoft.com/azure/aks/azure-disks-dynamic-pv) to automatically provision storage based on a storage class.

The following section shows how to create the PVC for this example.

### Define the storage

Create a file named `pvc.yaml`, and copy in the following manifest.

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

Create the PVC with the [kubectl apply](https://kubernetes.io/docs/reference/generated/kubectl/kubectl-commands#apply) command.

```azure-cli
kubectl apply -f pvc.yaml
```

Kubernetes creates the persistent volumes automatically as Azure managed storage accounts, and binds them to the persistent volume claims.

### Verify the persistent volume claim

To see all of the PVCs in a Kubernetes cluster, run `kubectl describe pvc`.

```azurecli
kubectl describe pvc
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

## Deploy the operator

The operator is deployed as a one-replica Kubernetes deployment.

Create a file named `operator.yaml`, and copy in the following manifest.

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

Create the operator with the `kubectl apply` command.

```azure-cli
kubectl apply -f operator.yaml
```

## Create the SQL Server AG Deployment

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

## Create the secrets

To create Kubernetes secrets to store the passwords for the SQL Server SA account and the SQL Server master key, run the following command.

```azurecli
kubectl create secret generic sql-secrets --from-literal=sapassword="MyC0m9l&xP@ssw0rd" --from-literal=masterkeypassword="MyC0m9l&xP@ssw0rd2"
```

In a production environment use a different, complex password.

## Deploy the availability group

In this step, create a manifest to describe the SQL Server container based on the mssql-server image, the health agent and AG agent containers based on the mssql-server-k8s-agents Docker image.

Create a file named `sqlservers.yaml`.

Copy the manifest below into the file.

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

To deploy the SQL Server instances and create the availability group, run the following command.

```azurecli
kubectl apply -f sqlservers.yaml
```

##  Connect to the SQL Server instance hosting the primary replica

The configuration file also deploys an `ag1-primary` service that provides a selector that points to the SQL Server instance hosting the primary replica. Use the external IP address of the service as target server, `sa` account and the password you created earlier in the `mssql secret`. Use the password that you configured as the Kubernetes secret.

The `sqlservers.yaml` manifest file defines a load balancer service named `ag1-primary` that connects to the availability group primary replica. Use `kubectl get services` to get this IP address.

For example:

![Get service example](media\tutorial-sql-server-ag-containers-kubernetes\KubernetesGetServices.png)

In the image above, `ag1-primary` service has an external IP address of `104.42-50.138`. To connect to SQL Server with SQL authentication, use the `sa` account, the value for `sapassword` from the secret you created, and this IP address.

For example:

```cmd
sqlcmd -S 104.42.50.138 -U sa -P "MyC0m9l&xP@ssw0rd"
```

![Connect with sqlcmd](media\tutorial-sql-server-ag-containers-kubernetes\sqlcmdConnect.png)

## Create listener services for secondary replicas

To create services for secondary replicas, create a file named `listenerServices.yaml`.

Copy the manifest below into the file. 

```yaml
---
apiVersion: v1
kind: Service
metadata:
  name: ag1-primary
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: primary
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


---
apiVersion: v1
kind: Service
metadata:
  name: ag1-secondary-config
spec:
  ports:
  - name: tds
    port: 1433
  selector:
    type: sqlservr
    role.ag.mssql.microsoft.com/ag1: secondary-config
  type: LoadBalancer
  ```

To create the Kubernetes services and for the secondary replicas, run the following command.

```azurecli
kubectl apply -f secondaryListeners.yaml
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


