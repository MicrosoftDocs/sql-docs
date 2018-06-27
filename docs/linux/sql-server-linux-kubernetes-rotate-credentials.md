---
title: SQL Server Always On availability group Kubernetes rotate credentials environment variables
description: This article explains the environment variables to rotate credentials for a SQL Server Kubernetes Always On availability group on Kubernetes.
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

# Rotate credentials - SQL Server availability group on Kubernetes

Rotate credentials on a SQL Server Always On availability group in Kubernetes to update the SQL Server instance `sa` password, set a new master key password, or rotate the database mirroring endpoint certificates.

## Steps

To rotate the credentials:

1. Update the passwords for `sa` and the SQL Server master key in the Kubernetes Secrets. 

   For example:

   ```azurecli
   kubectl create secret generic new-sql-secrets --from-literal='sapassword="<ComplexPassword>"' --from-literal='masterkeypassword="<ComplexPasswordforMasterKey>"'
   ```

1. Create a manifest `.yaml` file that describes the job to rotate the credentials. Review the variables and example in this article. 

1. Apply the manifest file to the Kubernetes cluster. 


   ```azurecli
   kubectl apply -f rotate-creds.yaml
   ```

The Kubernetes cluster rotates the credentials as a job.

## Environment variables

* `MSSQL_K8S_SA_PASSWORD`
  * Required
  * **Description**: The current `sa` password of the SQL Server instance.

* `MSSQL_K8S_STATEFULSET_NAME `
  * Required
  * **Description**: The name of the SQL Server StatefulSet, the same as the custom resource name .

* `MSSQL_K8S_NAMESPACE`
  * Optional
  * **Description**: Kubernetes namespace of the availability group.

* `MSSQL_ROTATE_CERT` 
  * Optional
  * **Description**: Boolean to rotate the database mirroring endpoint certificate.

* `MSSQL_K8S_MASTER_KEY_PASSWORD`
  * Optional
  * **Description**: The master key password.

* `MSSQL_K8S_NEW_SA_PASSWORD`
  * Optional
  * **Description**: The new `sa` password the SQL Server instance. 

## Example 

The following example of a manifest is a `.yaml` file that creates a job to rotate the credentials of all of the instances of SQL Sever that participate in an availability group.

```yaml
---
apiVersion: v1

kind: ServiceAccount

metadata:
  name: rotate-creds

---
apiVersion: rbac.authorization.k8s.io/v1

kind: Role

metadata:
  name: rotate-creds

rules:
- resources: ["secrets"]
  apiGroups: [""]
  verbs: ["get", "update"]
- resources: ["pods"]
  apiGroups: [""]
  verbs: ["list"]
- resources: ["statefulsets"]
  apiGroups: ["apps"]
  verbs: ["get"]


---
apiVersion: rbac.authorization.k8s.io/v1

kind: RoleBinding

metadata:
  name: rotate-creds
  namespace: default

roleRef:
  name: rotate-creds
  apiGroup: rbac.authorization.k8s.io
  kind: Role

subjects:
- name: rotate-creds
  kind: ServiceAccount


---
apiVersion: batch/v1

kind: Job

metadata:
  name: rotate-creds

spec:
  template:
    metadata:
      name: rotate-creds
    spec:
      serviceAccount: rotate-creds
      restartPolicy: Never
      containers:
      - name: rotate-creds
        image: mssql-server-k8s-agents:20.22 
        command: ["/mssql-server-k8s-rotate-creds"]
        env:
        - name: MSSQL_K8S_STATEFULSET_NAME
          value: sql-0
        - name: MSSQL_ROTATE_CERT
          value: "True"
        - name: MSSQL_K8S_NAMESPACE
          valueFrom:
            fieldRef:
              fieldPath: metadata.namespace
        - name: MSSQL_K8S_MASTER_KEY_PASSWORD
          valueFrom:
            secretKeyRef:
              name: new-sql-secrets
              key: masterkeypassword
        - name: MSSQL_K8S_SA_PASSWORD
          valueFrom:
            secretKeyRef:
              name: sql-secrets
              key: sapassword
        - name: MSSQL_K8S_NEW_SA_PASSWORD
          valueFrom:
            secretKeyRef:
              name: new-sql-secrets
              key: sapassword
```

## Next steps

[SQL Server availability group on Kubernetes cluster](sql-server-ag-kubernetes.md)