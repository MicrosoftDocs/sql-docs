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