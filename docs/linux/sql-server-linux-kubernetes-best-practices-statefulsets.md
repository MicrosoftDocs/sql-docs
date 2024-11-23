---
title: Deploy SQL Server containers on Kubernetes with StatefulSets
description: This article provides best practices and guidance for running SQL Server Linux containers on Kubernetes with StatefulSets.
author: amitkh-msft
ms.author: amitkh
ms.reviewer: randolphwest
ms.date: 05/03/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Deploy SQL Server Linux containers on Kubernetes with StatefulSets

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article contains best practices and guidance for running SQL Server containers on Kubernetes with StatefulSets. We recommend deploying one SQL Server container (instance) per pod in Kubernetes. Thus, you have one SQL Server instance deployed per pod in the Kubernetes cluster.

Similarly, the deployment script recommendation is to deploy one SQL Server instance by setting the `replicas` value to `1`. If you enter a number greater than `1` as the `replicas` value, you get that many SQL Server instances with correlated names. For example, in the below script, if you assigned the number `2` as the value for `replicas`, you would deploy two SQL Server pods, with the names `mssql-0` and `mssql-1` respectively.

Another reason we recommend one SQL Server per deployment script is to allow changes to configuration values, edition, trace flags, and other settings to be made independently for each SQL Server instance deployed.

In the following example, the StatefulSet workload name should match the `.spec.template.metadata.labels` value, which in this case is `mssql`. For more information, see [StatefulSets](https://kubernetes.io/docs/concepts/workloads/controllers/statefulset/).

> [!IMPORTANT]  
> The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

```yaml
apiVersion: apps/v1
kind: StatefulSet
metadata:
 name: mssql # name of the StatefulSet workload, the SQL Server instance name is derived from this. We suggest to keep this name same as the .spec.template.metadata.labels, .spec.selector.matchLabels and .spec.serviceName to avoid confusion.
spec:
 serviceName: "mssql" # serviceName is the name of the service that governs this StatefulSet. This service must exist before the StatefulSet, and is responsible for the network identity of the set.
 replicas: 1 # only one pod, with one SQL Server instance deployed.
 selector:
  matchLabels:
   app: mssql  # this has to be the same as .spec.template.metadata.labels
 template:
  metadata:
   labels:
    app: mssql # this has to be the same as .spec.selector.matchLabels, as documented [here](https://kubernetes.io/docs/concepts/workloads/controllers/statefulset/):
  spec:
   securityContext:
     fsGroup: 10001
   containers:
   - name: mssql # container name within the pod.
     image: mcr.microsoft.com/mssql/server:2022-latest
     ports:
     - containerPort: 1433
       name: tcpsql
     env:
     - name: ACCEPT_EULA
       value: "Y"
     - name: MSSQL_ENABLE_HADR
       value: "1"
     - name: MSSQL_AGENT_ENABLED
       value: "1"
     - name: MSSQL_SA_PASSWORD
       valueFrom:
         secretKeyRef:
          name: mssql
          key: MSSQL_SA_PASSWORD
     volumeMounts:
     - name: mssql
       mountPath: "/var/opt/mssql"
 volumeClaimTemplates:
   - metadata:
      name: mssql
     spec:
      accessModes:
      - ReadWriteOnce
      resources:
       requests:
        storage: 8Gi
```

If you still choose to deploy more than one replica of the SQL Server instance using the same deployment, that scenario is covered in next section. However, these are separate independent SQL Server instances and not replicas (unlike availability group replicas in SQL Server).

## Choose the workload type

Choosing the right [workload deployment type](https://kubernetes.io/docs/concepts/workloads/) doesn't affect performance, but the StatefulSet does provide identity stickiness requirements.

### StatefulSet workloads

SQL Server is a database application and thus mostly should be deployed as a [StatefulSet](https://kubernetes.io/docs/concepts/workloads/controllers/statefulset/) workload type. Deploying workloads as StatefulSet helps provide features like unique network identifies, persistent and stable storage and more. For more about this type of workload, refer to the [Kubernetes documentation](https://kubernetes.io/docs/concepts/workloads/controllers/statefulset/).

When deploying more than one replica of SQL Server containers using the same deployment YAML script as a StatefulSet workload, an important parameter to consider is [Pod management policies](https://kubernetes.io/docs/concepts/workloads/controllers/statefulset/#pod-management-policies), that is, `.spec.podManagementPolicy`.

There are two values possible for this setting:

- **OrderedReady**: This is the default value, and the behavior is as described in the [deployment and scaling guarantees](https://kubernetes.io/docs/concepts/workloads/controllers/statefulset/#deployment-and-scaling-guarantees).

- **Parallel**: This is the alternate policy that creates and launches the pods (in this case SQL Server pods) in parallel, without waiting for other pods to be created Similarly, all pods are deleted in parallel during termination. You can use this option when you're deploying SQL Server instances that are independent of each other, and when you don't intend to follow an order to start or delete the SQL Server instances.

  ```yaml
  apiVersion: apps/v1
  kind: StatefulSet
  metadata:
    name: mssql
  spec:
    serviceName: "mssql"
    replicas: 2 # two independent SQL Server instances to be deployed
    podManagementPolicy: Parallel
    selector:
      matchLabels:
        app: mssql
    template:
      metadata:
        labels:
          app: mssql
      spec:
        securityContext:
          fsGroup: 10001
        containers:
          - name: mssql
            image: mcr.microsoft.com/mssql/server:2022-latest
            ports:
              - containerPort: 1433
                name: tcpsql
            env:
              - name: ACCEPT_EULA
                value: "Y"
              - name: MSSQL_ENABLE_HADR
                value: "1"
              - name: MSSQL_AGENT_ENABLED
                value: "1"
              - name: MSSQL_SA_PASSWORD
                valueFrom:
                  secretKeyRef:
                    name: mssql
                    key: MSSQL_SA_PASSWORD
            volumeMounts:
              - name: mssql
                mountPath: "/var/opt/mssql"
    volumeClaimTemplates:
      - metadata:
          name: mssql
        spec:
          accessModes:
            - ReadWriteOnce
          resources:
            requests:
              storage: 8Gi
  ```

Because the SQL Server pods deployed on Kubernetes are independent of each other, `Parallel` is the value normally used for `podManagementPolicy`.

The following sample is the example output for `kubectl get all`, just after creating the pods using a parallel policy:

```output
NAME          READY   STATUS              RESTARTS   AGE
pod/mssql-0   0/1     ContainerCreating   0          4s
pod/mssql-1   0/1     ContainerCreating   0          4s

NAME                 TYPE        CLUSTER-IP   EXTERNAL-IP   PORT(S)   AGE
service/kubernetes   ClusterIP   201.0.0.1    <none>        443/TCP   61d

NAME                     READY   AGE
statefulset.apps/mssql   1/1     4s
```

### Deployment workloads

You can use the [deployment](https://kubernetes.io/docs/concepts/workloads/controllers/deployment/) type for SQL Server, in scenarios where you want to deploy SQL Server containers as stateless database applications, for example when data persistence isn't critical. Some such examples are for test/QA or CI/CD purposes.

## Isolation through namespaces

Namespaces provide a mechanism for isolating groups of resources within a single Kubernetes cluster. For more about namespaces and when to use them, see [Namespaces](https://kubernetes.io/docs/concepts/overview/working-with-objects/namespaces/).

From the SQL Server perspective, if you plan to run SQL Server pods on a Kubernetes cluster that is also hosting other resources, you should run the SQL Server pods in their own namespace, for ease of management and administration. For example, consider you have multiple departments sharing the same Kubernetes cluster, and you want to deploy a SQL Server instance for the Sales team and another one for the Marketing team. You'll create two namespaces called `sales` and `marketing`, as shown in the following example:

```powershell
kubectl create namespace sales
kubectl create namespace marketing
```

To check that the namespaces are created, run `kubectl get namespaces`, and you'll see a list similar to the following output.

```output
NAME              STATUS   AGE
default           Active   39d
kube-node-lease   Active   39d
kube-public       Active   39d
kube-system       Active   39d
marketing         Active   7s
sales             Active   26m
```

Now you can deploy SQL Server containers in each of these namespaces using the sample YAML shown in the following example. Notice the `namespace` metadata added to the deployment YAML, so all the containers and services of this deployment are deployed in the `sales` namespace.

```yaml
kind: StorageClass
apiVersion: storage.k8s.io/v1
metadata:
  name: azure-disk
provisioner: kubernetes.io/azure-disk
parameters:
  storageAccountType: Standard_LRS
  kind: Managed
---
apiVersion: apps/v1
kind: StatefulSet
metadata:
  name: mssql-sales
  namespace: sales
  labels:
    app: mssql-sales
spec:
  serviceName: "mssql-sales"
  replicas: 1
  selector:
    matchLabels:
      app: mssql-sales
  template:
    metadata:
      labels:
        app: mssql-sales
    spec:
      securityContext:
        fsGroup: 10001
      containers:
        - name: mssql-sales
          image: mcr.microsoft.com/mssql/server:2022-latest
          ports:
            - containerPort: 1433
              name: tcpsql
          env:
            - name: ACCEPT_EULA
              value: "Y"
            - name: MSSQL_ENABLE_HADR
              value: "1"
            - name: MSSQL_AGENT_ENABLED
              value: "1"
            - name: MSSQL_SA_PASSWORD
              valueFrom:
                secretKeyRef:
                  name: mssql
                  key: MSSQL_SA_PASSWORD
          volumeMounts:
            - name: mssql
              mountPath: "/var/opt/mssql"
  volumeClaimTemplates:
    - metadata:
        name: mssql
      spec:
        accessModes:
          - ReadWriteOnce
        resources:
          requests:
            storage: 8Gi
---
apiVersion: v1
kind: Service
metadata:
  name: mssql-sales-0
  namespace: sales
spec:
  type: LoadBalancer
  selector:
    statefulset.kubernetes.io/pod-name: mssql-sales-0
  ports:
    - protocol: TCP
      port: 1433
      targetPort: 1433
      name: tcpsql
```

To see the resources, you can run the `kubectl get all` command with the namespace specified to see these resources:

```powershell
kubectl get all -n sales
```

```output
NAME                READY   STATUS    RESTARTS   AGE
pod/mssql-sales-0   1/1     Running   0          17m

NAME                    TYPE           CLUSTER-IP     EXTERNAL-IP   PORT(S)          AGE
service/mssql-sales-0   LoadBalancer   10.0.251.120   20.23.79.52   1433:32052/TCP   17m

NAME                           READY   AGE
statefulset.apps/mssql-sales   1/1     17m
```

Namespaces can also be used to limit the resources and pods created within a namespace, using the [limit range](https://kubernetes.io/docs/concepts/policy/limit-range/) and/or [resource quota](https://kubernetes.io/docs/concepts/policy/resource-quotas/) policies, to manage the overall resources creation within a namespace.

## Configure pod Quality of Service

When deploying multiple pods on a single Kubernetes cluster, you must share resources appropriately, to ensure the efficient running of the Kubernetes cluster. You can configure pods so that they are assigned a particular Quality of Service (QoS).

Kubernetes uses QoS *classes* to make decisions about scheduling and evicting pods. For more information about the different QoS classes, see [Configure Quality of Service for Pods](https://kubernetes.io/docs/tasks/configure-pod-container/quality-service-pod/).

From the SQL Server perspective, we recommend that you deploy SQL Server pods using the QoS as `Guaranteed` for production-based workloads. Considering that a SQL Server pod only has one SQL Server container instance running to achieve guaranteed QoS for that pod, you need to specify the CPU and memory *requests* for the container that should be equal to the memory and CPU *limits*. This ensures that the nodes provide and commit the required resources specified during the deployment, and have predictable performance for the SQL Server pods.

Here is a sample deployment YAML that deploys one SQL Server container in the default namespace, and because the resource requests weren't specified but the limits were specified as per the guidelines in the [Guaranteed Quality of Service](https://kubernetes.io/docs/tasks/configure-pod-container/quality-service-pod/#create-a-pod-that-gets-assigned-a-qos-class-of-guaranteed) example, we see that the pod that is created in the following example has the QoS set as `Guaranteed`. When you don't specify the resource requests, then Kubernetes considers the resource *limits* equal to the resource *requests*.

```yaml
kind: StorageClass
apiVersion: storage.k8s.io/v1
metadata:
     name: azure-disk
provisioner: kubernetes.io/azure-disk
parameters:
  storageaccounttype: Standard_LRS
  kind: Managed
---
apiVersion: apps/v1
kind: StatefulSet
metadata:
 name: mssql
 labels:
  app: mssql
spec:
 serviceName: "mssql"
 replicas: 1
 selector:
  matchLabels:
   app: mssql
 template:
  metadata:
   labels:
    app: mssql
  spec:
   securityContext:
     fsGroup: 10001
   containers:
   - name: mssql
     command:
       - /bin/bash
       - -c
       - cp /var/opt/config/mssql.conf /var/opt/mssql/mssql.conf && /opt/mssql/bin/sqlservr
     image: mcr.microsoft.com/mssql/server:2022-latest
     resources:
      limits:
       memory: 2Gi
       cpu: '2'
     ports:
     - containerPort: 1433
     env:
     - name: ACCEPT_EULA
       value: "Y"
     - name: MSSQL_ENABLE_HADR
       value: "1"
     - name: MSSQL_SA_PASSWORD
       valueFrom:
         secretKeyRef:
          name: mssql
          key: MSSQL_SA_PASSWORD
     volumeMounts:
     - name: mssql
       mountPath: "/var/opt/mssql"
     - name: userdata
       mountPath: "/var/opt/mssql/userdata"
     - name: userlog
       mountPath: "/var/opt/mssql/userlog"
     - name: tempdb
       mountPath: "/var/opt/mssql/tempdb"
     - name: mssql-config-volume
       mountPath: "/var/opt/config"
   volumes:
     - name: mssql-config-volume
       configMap:
        name: mssql
 volumeClaimTemplates:
   - metadata:
      name: mssql
     spec:
      accessModes:
      - ReadWriteOnce
      resources:
       requests:
        storage: 8Gi
   - metadata:
      name: userdata
     spec:
      accessModes:
      - ReadWriteOnce
      resources:
       requests:
        storage: 8Gi
   - metadata:
      name: userlog
     spec:
      accessModes:
      - ReadWriteOnce
      resources:
       requests:
        storage: 8Gi
   - metadata:
      name: tempdb
     spec:
      accessModes:
      - ReadWriteOnce
      resources:
       requests:
        storage: 8Gi
```

You can run the command `kubectl describe pod mssql-0` to view the QoS as `Guaranteed`, with output similar to the following snippet.

```output
...
QoS Class:                 Guaranteed
Node-Selectors:            <none>
Tolerations:               node.kubernetes.io/memory-pressure:NoSchedule op=Exists
                           node.kubernetes.io/not-ready:NoExecute op=Exists for 300s
                           node.kubernetes.io/unreachable:NoExecute op=Exists for 300s
...
```

For non-production workloads, where performance and availability aren't a high priority, you can consider setting the QoS to `Burstable` or `BestEffort`.

### Burstable QoS sample

To define a `Burstable` YAML example, you specify the resource *requests*, not the resource *limits*; or you specify the *limits*, which is higher than *requests*. The following code displays only the difference from the previous example, in order to define a [burstable](https://kubernetes.io/docs/tasks/configure-pod-container/quality-service-pod/#create-a-pod-that-gets-assigned-a-qos-class-of-burstable) workload.

```yaml
apiVersion: apps/v1
kind: StatefulSet
metadata:
 name: mssql
 labels:
  app: mssql
spec:
 serviceName: "mssql"
 replicas: 1
 selector:
  matchLabels:
   app: mssql
 template:
  metadata:
   labels:
    app: mssql
  spec:
   securityContext:
     fsGroup: 10001
   containers:
   - name: mssql
     command:
       - /bin/bash
       - -c
       - cp /var/opt/config/mssql.conf /var/opt/mssql/mssql.conf && /opt/mssql/bin/sqlservr
     image: mcr.microsoft.com/mssql/server:2022-latest
     resources:
      requests:
       memory: 2Gi
       cpu: '2'
```

You can run the command `kubectl describe pod mssql-0` to view the QoS as `Burstable`, with output similar to the following snippet.

```output
...
QoS Class:                 Burstable
Node-Selectors:            <none>
Tolerations:               node.kubernetes.io/memory-pressure:NoSchedule op=Exists
                           node.kubernetes.io/not-ready:NoExecute op=Exists for 300s
                           node.kubernetes.io/unreachable:NoExecute op=Exists for 300s
...
```

### Best effort QoS sample

To define a `BestEffort` YAML example, remove the resource *requests* and resource *limits*. You'll end up with the best effort QoS, as defined in [Create a Pod that gets assigned a QoS class of BestEffort](https://kubernetes.io/docs/tasks/configure-pod-container/quality-service-pod/#create-a-pod-that-gets-assigned-a-qos-class-of-besteffort). As before, the following code displays only the difference from the `Guaranteed` example, in order to define a [best effort](https://kubernetes.io/docs/tasks/configure-pod-container/quality-service-pod/#create-a-pod-that-gets-assigned-a-qos-class-of-besteffort) workload. These are the least recommended options for SQL Server pods, as they would probably be the first ones to be terminated in the case of resource contention. Even for test and QA scenarios, we recommend using the Burstable option for SQL Server.

```yaml
apiVersion: apps/v1
kind: StatefulSet
metadata:
  name: mssql
  labels:
    app: mssql
spec:
  serviceName: "mssql"
  replicas: 1
  selector:
    matchLabels:
      app: mssql
  template:
    metadata:
      labels:
        app: mssql
    spec:
      securityContext:
        fsGroup: 10001
      containers:
        - name: mssql
          command:
            - /bin/bash
            - -c
            - cp /var/opt/config/mssql.conf /var/opt/mssql/mssql.conf && /opt/mssql/bin/sqlservr
          image: mcr.microsoft.com/mssql/server:2022-latest
          ports:
            - containerPort: 1433
```

You can run the command `kubectl describe pod mssql-0` to view the QoS as `BestEffort`, with output similar to the following snippet.

```output
...
QoS Class:                 BestEffort
Node-Selectors:            <none>
Tolerations:               node.kubernetes.io/memory-pressure:NoSchedule op=Exists
                           node.kubernetes.io/not-ready:NoExecute op=Exists for 300s
                           node.kubernetes.io/unreachable:NoExecute op=Exists for 300s
...
```

## Related content

- [Quickstart: Deploy a SQL Server container cluster on Azure](quickstart-sql-server-containers-azure.md)
- [Quickstart: Deploy a SQL Server Linux container to Kubernetes using Helm charts](sql-server-linux-containers-deploy-helm-charts-kubernetes.md)
- [Deploy availability group with DH2i for SQL Server containers on AKS](tutorial-sql-server-containers-kubernetes-dh2i.md)
