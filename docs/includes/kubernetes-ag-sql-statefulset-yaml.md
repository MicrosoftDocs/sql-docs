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